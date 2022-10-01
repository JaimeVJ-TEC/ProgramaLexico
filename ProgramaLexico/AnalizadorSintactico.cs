using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProgramaLexico
{
    public class AnalizadorSintactico
    {
        public List<string[]> ArchivoTokens;

        public string[,] Gramaticas;

        public List<Error> Errores = new List<Error>();

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

        public AnalizadorSintactico(List<string[]> Tokens)
        {
            ArchivoTokens = Tokens;
            LlenarGramaticas();
        }

        /**
        public void Analizar()
        {
            foreach(string[] linea in ArchivoTokens)
            {
                int numTokens = linea.Length;
                int numActual = linea.Length;

                for (int i = linea.Length - 1; i >= 0; i--)
                {
                    for (int j = 0; j < linea.Length; j++)
                    {

                    }
                }
            }
        }

        public void ReducirLineas()
        {
            int posActual = 0;
            int cantTokens = 0;
            string Resultado = "";

            foreach(string[] Linea in ArchivoTokens)
            {
                cantTokens = Linea.Length;
                posActual = 0;
                
                while(Resultado != "S")
                {
                    posActual = 0;
                    while(posActual + cantTokens <= Linea.Length)
                    {
                        Resultado = ReducirCadena(Linea.SubArray(posActual,cantTokens));
                        if (Resultado == "Error")
                        {
                            if(cantTokens == 1 && posActual == Linea.Length)
                            {
                                Resultado = "Error de sintaxis";
                                    break;
                            }

                            cantTokens--;
                            break;
                        }
                        else
                        {

                        }

                        posActual++;
                    }
                }
            }
        }
        **/

        public void Analizar()
        {
            for (int i = 0; i < ArchivoTokens.Count; i++)
            {
                ArchivoTokens[i] = ReducirLinea(ArchivoTokens[i]);

                if(ArchivoTokens[i][0] != "S")
                {
                    Error error = new Error();
                    error.Linea = i;
                    error.Descripcion = "Error de sintaxis";
                    Errores.Add(error);
                }
            }
        }

        public string[] ReducirLinea(string[] Linea)
        {
            string Resultado = "";
            int PosicionActual = 0;
            int CantidadTokens = Linea.Length;

            while(Resultado != "S" && Resultado != "Error sintaxis")
            {
                Resultado = ReducirCadena(Linea.SubArray(PosicionActual, CantidadTokens));

                while (PosicionActual + CantidadTokens <= Linea.Length)
                {
                    if (Resultado == "Error")
                    {
                        if (CantidadTokens == 1 && PosicionActual == Linea.Length-1)
                        {
                            Resultado = "Error sintaxis";
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
                        PosicionActual = 0;
                        CantidadTokens = Linea.Length;
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
