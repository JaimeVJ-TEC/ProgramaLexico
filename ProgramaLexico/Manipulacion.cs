using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public static class Manipulacion
    {
        public static string[] EvaluarCadena(string Cadena)
        {
            int Fila = 0;
            int Contador = 0;

            bool Estado = true;

            string[] Resultado = new string[2];

            while (Estado)
            {
                int NumeroSimbolo = (int)Cadena[Contador];

                if (NumeroSimbolo < 32 || NumeroSimbolo > 126)
                {
                    Resultado[0] = "Error";
                    Resultado[1] = "Simbolo no definido";
                    Estado = false;
                }
                else
                {
                    Fila = MatrizTransicion[Fila, NumeroSimbolo - 31].Numero;
                    if (MatrizTransicion[Fila, 1].Contenido == "Acepta" || MatrizTransicion[Fila, 1].Contenido == "Error")
                    {
                        Resultado[0] = MatrizTransicion[Fila, 1].Contenido;
                        Resultado[1] = MatrizTransicion[Fila, 0].Contenido;
                        Estado = false;
                    }
                }

                Contador++;
            }

            return Resultado;
        }

        public static string[] SepararCadenas(string Texto)
        {
            Texto = Texto.Replace("\n", " ");
            Texto = Texto.Trim();
            Texto += " ";

            List<string> ListaTemp = new List<string>();

            if (Texto != "" && Texto != " ")
            {
                if (Texto.Substring(0, 2) == "//")
                {
                    int FinCadena = Texto.Length;
                    ListaTemp.Add(Texto.Substring(0, FinCadena));
                }
                else if (Texto.Substring(0, 2) == "/*")
                {
                    int Cierre = Texto.IndexOf("*/", 2);
                    if (Cierre == -1)
                    {
                        Texto = Texto.Trim();
                        Texto += "*/ ";
                        ListaTemp.Add(Texto);
                    }
                    else
                    {
                        Cierre = Texto.IndexOf(" ", Cierre + 2);
                        ListaTemp.Add(Texto.Substring(0, Cierre + 1));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(Cierre, Texto.Length - Cierre)));
                    }
                }
                else if (Texto[0] == '\"')
                {
                    int Cierre = Texto.IndexOf('\"', 1);

                    if (Cierre == -1)
                    {
                        Texto = Texto.Trim();
                        Texto += "\" ";
                        ListaTemp.Add(Texto);
                    }
                    else
                    {
                        Cierre = Texto.IndexOf(" ", Cierre + 1);
                        ListaTemp.Add(Texto.Substring(0, Cierre + 1));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(Cierre, Texto.Length - Cierre)));
                    }
                }
                else if (Texto[0] == '\'')
                {
                    int Cierre = Texto.IndexOf('\'', 1);

                    if (Cierre == -1)
                    {
                        Texto = Texto.Trim();
                        Texto += " ";
                        ListaTemp.Add(Texto);
                    }
                    else
                    {
                        Cierre = Texto.IndexOf(" ", Cierre + 1);
                        ListaTemp.Add(Texto.Substring(0, Cierre + 1));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(Cierre, Texto.Length - Cierre)));
                    }
                }
                else
                {
                    int FinCadena = Texto.IndexOf(" ") + 1;
                    ListaTemp.Add(Texto.Substring(0, FinCadena));
                    ListaTemp.AddRange(SepararCadenas(Texto.Substring(FinCadena, Texto.Length - FinCadena)));
                }
            }
            return ListaTemp.ToArray();
        }

        public static string[] SepararLineas(string Texto)
        {
            Texto = Texto + " ";
            string[] Cadenas = Texto.Split('\n');

            return Cadenas;
        }


    }
}
