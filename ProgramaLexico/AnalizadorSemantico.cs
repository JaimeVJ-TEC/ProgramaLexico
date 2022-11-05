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

        int PorPasoLinea = 0;
        bool PorPasBln = false;
        string[] TokensValidos = new string[] {"S","SWITCH", "WHILE", "CFOR", "CASE", "IF","ELIF", "ELSE", 
                                                "BEGIN", "END", "BREAK", "OPEN", "CLOSE", "NOAP", "DO"};

        public void LlenarSemanticatxt()
        {
            string[] lines = File.ReadAllLines("rsemanticas.txt");
            Semantica = new string[lines.GetLength(0), 2];

            for (int i = 0; i < Semantica.GetLength(0); i++)
            {
                string[] aux = lines[i].Split('>');
                Semantica[i, 0] = aux[0].Trim();
                Semantica[i, 1] = aux[1].Trim();
            }
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
            LlenarSemanticatxt();
        }

        public void Analizar()
        {
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                ArchivoTokens[i] = ArchivoTokens[i].Where(val => val != "COMENTARIOS").ToArray();

                if (ArchivoTokens[i].Length == 0)
                    continue;
                ArchivoTokens[i] = ReducirLinea(ArchivoTokens[i], false);

                if (!TokensValidos.Contains(ConcatenarArreglo(ArchivoTokens[i])))
                {
                    Error error = new Error();
                    error.Linea = i;
                    error.Descripcion = "Error de Tipo";
                    Errores.Add(error);
                }
            }
        }

        public void AnalizarPorPaso()
        {
            if (PorPasoLinea >= ArchivoTokensPorPaso.Count)
            {
                MessageBox.Show("Archivo de tokens vacio");
                return;
            }

            if (PorPasoLinea >= ArchivoTokensPorPaso.Count)
            {
                MessageBox.Show("Analisis semantico finalizado");
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ArchivoTokensPorPaso[PorPasoLinea].Where(val => val != "COMENTARIOS").ToArray();
            if (ArchivoTokensPorPaso[PorPasoLinea].Length == 0)
            {
                PorPasoLinea++;
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ReducirLinea(ArchivoTokensPorPaso[PorPasoLinea], true);

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

        public string[] ReducirLinea(string[] Linea, bool Paso)
        {
            string Resultado = "";
            int PosicionActual = 0;
            int CantidadTokens = Linea.Length;

            while (Resultado != "S" && Resultado != "Error semantica")
            {
                Resultado = ReducirCadena(Linea.SubArray(PosicionActual, CantidadTokens));

                while (PosicionActual + CantidadTokens <= Linea.Length)
                {
                    PorPasBln = false;
                    if (Resultado == "Error")
                    {
                        if (CantidadTokens == 1 && PosicionActual == Linea.Length - 1)
                        {
                            Resultado = "Error semantica";
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

        public string ReducirCadena(string[] Cadena)
        {
            string Linea = ConcatenarArreglo(Cadena);
            int i = 0;

            bool reducido = false;

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
