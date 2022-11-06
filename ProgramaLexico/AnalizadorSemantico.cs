using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaLexico
{
    public class AnalizadorSemantico
    {
        public List<string[]> ArchivoTokens;
        public List<string[]> ArchivoTokensPorPaso;
        public List<string[]> ArchivoTokensCopia;
        public List<Identificador> TablaSimbolosSem;
        public List<Error> Errores = new List<Error>();
        public string[,] Semantica;
        public string[,] ReglasVertical;

        int OpenCant = -1;
        List<string> InstruccionesComp = new List<string>();
        int PorPasoLinea = 0;
        bool PorPasBln = false;
        string[] TokensValidos = new string[] {"S","SWITCH", "WHILE", "CFOR", "CASE", "IF","ELIF", "ELSE", 
                                                "BEGIN", "END", "BREAK", "OPEN", "CLOSE", "NOAP", "DO"};

        public string[,] LlenarSemanticatxt(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName);
            string[,] SemanticaT = new string[lines.GetLength(0), 2];

            for (int i = 0; i < SemanticaT.GetLength(0); i++)
            {
                string[] aux = lines[i].Split('>');
                SemanticaT[i, 0] = aux[0].Trim();
                SemanticaT[i, 1] = aux[1].Trim();
            }
            return SemanticaT;
        }

        public List<string[]> ConvertirTipos(List<string[]> ArchivoTokensConNumeros)
        {
            Dictionary<string, bool> Tipos = new Dictionary<string, bool>() { { "CNR", true }, { "CNE", true }, { "CAD", true }, { "CHA", true }, { "BLN", true } };

            List<string[]> ArchivoTokensT = ArchivoTokensConNumeros;

            foreach (string[] linea in ArchivoTokensT)
            {
                for (int i = 0; i < linea.Length; i++)
                {
                    if (Tipos.TryGetValue(linea[i].Substring(0, 3), out bool value) || linea[i].Substring(0, 2) == "ID")
                    {
                        int numero = int.Parse(linea[i][linea[i].Length - 1].ToString());
                        linea[i] = TablaSimbolosSem[numero].TipoDato == null ? "SINTIPO" : TablaSimbolosSem[numero].TipoDato.ToUpper();
                    }

                }
            }

            return ArchivoTokensT;
        }

        public AnalizadorSemantico(List<string[]> ArchivoTokensConNumeros, List<Identificador> TablaSimbolos)
        {
            ArchivoTokensCopia = new List<string[]>();
            foreach(string[] Linea in ArchivoTokensConNumeros)
            {
                string[] Copia = new string[Linea.Length];
                Linea.CopyTo(Copia, 0);
                ArchivoTokensCopia.Add(Copia);
            }

            TablaSimbolosSem = TablaSimbolos;
            ArchivoTokensPorPaso = ArchivoTokensCopia;
            ArchivoTokens = ConvertirTipos(ArchivoTokensCopia);
            Semantica = LlenarSemanticatxt("rsemanticas.txt");
            ReglasVertical = LlenarSemanticatxt("semanticavertical.txt");
        }

        public void Analizar()
        {
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                ArchivoTokens[i] = ArchivoTokens[i].Where(val => val != "COMENTARIOS").ToArray();

                if (ArchivoTokens[i].Length == 0)
                    continue;
                ArchivoTokens[i] = ReducirLinea(ArchivoTokens[i], false,true);

                if (!TokensValidos.Contains(ConcatenarArreglo(ArchivoTokens[i])))
                {
                    Error error = new Error();
                    error.Linea = i;
                    error.Descripcion = "Error de Tipo";
                    Errores.Add(error);
                }
            }

            if (Errores.Count == 0)
            {
                AnalisisVertical();
            }
        }

        public void AnalisisVertical()
        {
            OpenCant = -1;

            //Convetimos las S que se generaron en el analisis horizontal a CON para empezar el analisis Vertical
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                for (int j = 0; j < ArchivoTokens[i].Length; j++)
                {
                    if(ArchivoTokens[i][j]=="S")
                    {
                        ArchivoTokens[i][j] = "CON";
                    }
                }
            }

            //Analisis vertical JELU
            string[] ArregloJELU = new string[ArchivoTokens.Count];
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                ArregloJELU[i] = ConcatenarArreglo(ArchivoTokens[i]);
            }
            ArregloJELU = ReducirLinea(ArregloJELU, false, false);

            if(ArregloJELU[0] != "S")
            {
                int CantError = 0;
                int Contador = 1;

                foreach (string s in ArregloJELU)
                {
                    Contador++;
                    if (s.Contains("ER"))
                    {
                        CantError++;
                        Error error = new Error();
                        error.Linea = -1;
                        error.Descripcion = "Error Instruccion: " + s.Substring(2) + " no se abrio";
                        Errores.Add(error);
                    }
                }
                
                if(OpenCant>=0 && CantError == 0)
                {
                    Error error = new Error();
                    error.Linea = -1;
                    error.Descripcion = "Error Instruccion: "+InstruccionesComp[OpenCant]+" no se cerro";
                    Errores.Add(error);
                }
                else if (CantError < 0)
                {
                    Error error = new Error();
                    error.Linea = -1;
                    error.Descripcion = "Error instruccion compuesta se cerro pero no se abrio";
                    Errores.Add(error);
                }
            }
        }

        public void AnalizarPorPaso()
        {
            if (PorPasoLinea >= ArchivoTokensPorPaso.Count)
            {
                MessageBox.Show("Analisis semantico horizontal finalizado");
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ArchivoTokensPorPaso[PorPasoLinea].Where(val => val != "COMENTARIOS").ToArray();
            if (ArchivoTokensPorPaso[PorPasoLinea].Length == 0)
            {
                PorPasoLinea++;
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ReducirLinea(ArchivoTokensPorPaso[PorPasoLinea], true, true);

            if (!TokensValidos.Contains(ConcatenarArreglo(ArchivoTokens[PorPasoLinea])) && PorPasBln)
            {
                Error error = new Error();
                error.Linea = PorPasoLinea;
                error.Descripcion = "Error de Tipo";
                Errores.Add(error);
                PorPasoLinea++;
            }
            else if (TokensValidos.Contains(ArchivoTokensPorPaso[PorPasoLinea][0]))
            {
                PorPasoLinea++;
            }
        }

        public string[] ReducirLinea(string[] Linea, bool Paso, bool Horizontal)
        {
            string Resultado = "";
            int PosicionActual = 0;
            int CantidadTokens = Linea.Length;

            while (Resultado != "S" && Resultado != "Error semantico")
            {
                Resultado = ReducirCadena(Linea.SubArray(PosicionActual, CantidadTokens),Horizontal);

                while (PosicionActual + CantidadTokens <= Linea.Length)
                {
                    PorPasBln = false;
                    if (Resultado == "Error")
                    {
                        if (CantidadTokens == 1 && PosicionActual == Linea.Length - 1)
                        {
                            Resultado = "Error semantico";
                            PorPasBln = true;
                            break;
                        }
                        else if (PosicionActual != Linea.Length - 1 && PosicionActual + CantidadTokens == Linea.Length)
                        {
                            PosicionActual = 0;
                            CantidadTokens--;
                            break;
                        }
                        else
                        {
                            PosicionActual++;
                            break;
                        }
                    }
                    else
                    {
                        if (!Horizontal)
                        {
                            if (Resultado == "CONC" || Resultado == "S")
                            {
                                OpenCant--;
                            }
                            else if (Resultado.Contains("ER"))
                            {

                            }
                            else if (Resultado != "CON" && Resultado != "CONC" && Resultado != "CLOSE")
                            {
                                OpenCant++;
                                InstruccionesComp.Add(Linea[PosicionActual]);
                            }
                        }
                        Linea = ReemplazarArreglo(Linea, Resultado, Enumerable.Range(PosicionActual, CantidadTokens).ToArray());
                        Debug.WriteLine(ConcatenarArreglo(Linea));
                        PosicionActual = 0;
                        CantidadTokens = Linea.Length;

                        if (Paso)
                        {
                            return Linea;
                        }

                        break;
                    }
                }
            }

            return Linea;
        }

        public string ReducirCadena(string[] Cadena,bool horizontal)
        {
            string Linea = ConcatenarArreglo(Cadena);
            int i = 0;

            bool reducido = false;

            if (horizontal)
            {
                while (reducido == false)
                {
                    if (i >= Semantica.GetLength(0))
                    {
                        return "Error";
                    }
                    else if (Linea == Semantica[i, 1])
                    {
                        Linea = Semantica[i, 0].Trim();
                        reducido = true;
                    }
                    i++;
                }
            }
            else
            {
                while (reducido == false)
                {
                    if (i >= ReglasVertical.GetLength(0))
                    {
                        return "Error";
                    }
                    else if (Linea == ReglasVertical[i, 1])
                    {
                        Linea = ReglasVertical[i, 0].Trim();
                        reducido = true;
                    }
                    i++;
                }
            }
            return Linea;
        }

        public string ConcatenarArreglo(string[] Cadena)
        {
            string Linea = "";
            for (int i = 0; i < Cadena.Length; i++)
            {
                Linea += Cadena[i].Contains("ID") ? "ID " : Cadena[i] + " ";
            }
            return Linea.Trim();
        }

        public string[] ReemplazarArreglo(string[] Cadena, string Palabra, int[] indices)
        {
            List<string> lista = new List<string>();
            bool Espacio = false;

            for (int i = 0; i < Cadena.Length; i++)
            {
                if (indices.Contains(i))
                {
                    if (Espacio == false)
                    {
                        lista.Add(Palabra);
                        Espacio = true;
                    }
                }
                else
                {
                    lista.Add(Cadena[i]);
                }
            }

            return lista.ToArray();
        }
    }
}
