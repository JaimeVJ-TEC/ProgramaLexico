using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ProgramaLexico
{
    public class AnalizadorSintactico
    {
        public List<string[]> ArchivoTokensCopia;

        public List<string[]> ArchivoTokens;

        public List<string[]> ArchivoTokensPorPaso;

        public string[,] Gramaticas;

        public List<Error> Errores = new List<Error>();

        int PorPasoLinea = 0;
        bool PorPasBln = false;

        public void LlenarGramaticas()
        {
            Conexion cnn = new Conexion();
            DataTable dataTable = cnn.Gramaticas();


            Gramaticas = new string[dataTable.Rows.Count, dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Gramaticas[i, j] = (string)dataTable.Rows[i][j];
                }
            }
        }

        //Metodo que llena las gramaticas por medio de un archivo txt ("gramaticas.txt")
        public void LlenarGramaticastxt()
        {
            string[] lines = File.ReadAllLines("gramaticas.txt");
            Gramaticas = new string[lines.GetLength(0), 2];

            for (int i = 0; i < Gramaticas.GetLength(0); i++)
            {
                string[] aux = lines[i].Split('>');
                Gramaticas[i,0] = aux[0].Trim();
                Gramaticas[i, 1] = aux[1].Trim();
            }
        }

        public AnalizadorSintactico(List<string[]> Tokens)
        {
            ArchivoTokensCopia = Tokens;
            ArchivoTokensPorPaso = Tokens;
            LlenarGramaticastxt();
        }

        public void Analizar()
        {
            ArchivoTokens = ArchivoTokensCopia;
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                ArchivoTokens[i] = ArchivoTokens[i].Where(val => val != "COMENTARIOS").ToArray();

                if (ArchivoTokens[i].Length == 0)
                    continue;
                ArchivoTokens[i] = ReducirLinea(ArchivoTokens[i],false);

                if(ConcatenarArreglo(ArchivoTokens[i]) != "S")
                {
                    Error error = new Error();
                    error.Linea = i;
                    error.Descripcion = "Error de sintaxis";
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
                MessageBox.Show("Analisis sintactico finalizado");
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ArchivoTokensPorPaso[PorPasoLinea].Where(val => val != "COMENTARIOS").ToArray();
            if (ArchivoTokensPorPaso[PorPasoLinea].Length == 0)
            {
                PorPasoLinea++;
                return;
            }

            ArchivoTokensPorPaso[PorPasoLinea] = ReducirLinea(ArchivoTokensPorPaso[PorPasoLinea], true);

            if (ConcatenarArreglo(ArchivoTokensPorPaso[PorPasoLinea]) != "S" && PorPasBln)
            {
                Error error = new Error();
                error.Linea = PorPasoLinea;
                error.Descripcion = "Error de sintaxis";
                Errores.Add(error);
                PorPasoLinea++;
            }
            else if (ArchivoTokensPorPaso[PorPasoLinea][0] == "S")
            {
                PorPasoLinea++;
            }
        }

        public string[] ReducirLinea(string[] Linea, bool Paso)
        {
            string Resultado = "";
            int PosicionActual = 0;
            int CantidadTokens = Linea.Length;

            while(Resultado != "S" && Resultado != "Error sintaxis")
            {
                Resultado = ReducirCadena(Linea.SubArray(PosicionActual, CantidadTokens));

                while (PosicionActual + CantidadTokens <= Linea.Length)
                {
                    PorPasBln = false;
                    if (Resultado == "Error")
                    {
                        if (CantidadTokens == 1 && PosicionActual == Linea.Length-1)
                        {
                            Resultado = "Error sintaxis";
                            PorPasBln = true;
                            break;
                        }
                        else if(PosicionActual != Linea.Length - 1 && PosicionActual + CantidadTokens == Linea.Length)
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

                        if(Paso)
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

            while(reducido == false)
            {
                if (i >= Gramaticas.GetLength(0))
                {
                    return "Error";
                }
                else if (Linea == Gramaticas[i,1])
                {
                    Linea = Gramaticas[i, 0].Trim();
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
                    if(Espacio == false)
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