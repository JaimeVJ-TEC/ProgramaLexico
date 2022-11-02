using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;

namespace ProgramaLexico
{
    public class AnalizadorLexico
    {
        public Celda[,] MatrizTransicion;

        public List<string[]> ArchivoTokens;

        public List<Identificador> TablaSimbolos;

        public List<Error> Errores;

        string Texto;

        Dictionary<string, string> tokens = new Dictionary<string, string>();
        Dictionary<string, string> values = new Dictionary<string, string>();

        public AnalizadorLexico()
        {
            MatrizTransicion = LlenarMatriz();
            ArchivoTokens = new List<string[]>();
            TablaSimbolos = new List<Identificador>();
            Errores = new List<Error>();

            tokens.Add("CNE","CNE_");
            tokens.Add("CNR", "CNR_");
            tokens.Add("CNEX", "CNEX_");
            tokens.Add("CADENAS", "CAD_");
            tokens.Add("CHAR", "CHAR_");
            tokens.Add("PR23", "BLN_");
            tokens.Add("PR24", "BLN_");

            values.Add("CNE", "int");
            values.Add("CNR", "double");
            values.Add("CNRF", "float");
            values.Add("CNEX", "double");
            values.Add("CADENAS", "string");
            values.Add("CHAR", "char");
            values.Add("PR23", "bool");
            values.Add("PR24", "bool");
        }

        public Celda[,] LlenarMatriz()
        {
            Conexion cnn = new Conexion();
            DataTable dataTable = cnn.MatrizDeTransicion();

            Celda[,] Matriz = new Celda[dataTable.Rows.Count, dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Celda c = new Celda();

                    if (dataTable.Rows[i][j] != System.DBNull.Value)
                    {
                        if (dataTable.Rows[i][j] is string)
                        {
                            int tmp;

                            if (int.TryParse((string)dataTable.Rows[i][j], out tmp))
                            {
                                c.Numero = tmp;
                            }
                            else
                            {
                                c.Contenido = (string)dataTable.Rows[i][j];
                            }
                        }
                        else
                        {
                            c.Numero = (int)(double)dataTable.Rows[i][j];
                        }
                    }
                    Matriz[i, j] = c;
                }
            }

            return Matriz;
        }

        public void Analizar(string cadena)
        {
            Texto = cadena;
            LlenarMatriz();
            EvaluarTexto();
            AsignarTDIdentificador();
        }

        public string[] EvaluarCadena(string Cadena)
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

        public string[] SepararCadenas(string Texto)
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

        public string[] SepararLineas(string Texto)
        {
            Texto = Texto + " ";
            string[] Cadenas = Texto.Split('\n');

            return Cadenas;
        }

        public void EvaluarTexto()
        {
            string[] Lineas = SepararLineas(Texto);
            int ContadorID = 0;
            int ContadorLinea = 0;

            ArchivoTokens = new List<string[]>();
            TablaSimbolos = new List<Identificador>();
            Errores = new List<Error>();

            foreach (string Linea in Lineas)
            {
                string[] Cadenas = SepararCadenas(Linea);

                string[] TokensLinea = new string[Cadenas.Length];
                int ContadorToken = 0;

                foreach (string Cadena in Cadenas)
                {
                    string[] Resultado = EvaluarCadena(Cadena);

                    if (Resultado[0] == "Acepta")
                    {
                        Identificador Iden = new Identificador();

                        if (Resultado[1] == "ID Valido.")
                        {
                            bool existe = false;
                            string Identificador = Cadena.Replace(" ", "");

                            foreach (Identificador id in TablaSimbolos)
                            {
                                if (id.Descripcion == Identificador)
                                {
                                    existe = true;
                                    TokensLinea[ContadorToken] = "ID" + id.Numero;
                                    break;
                                }
                            }

                            if (!existe)
                            {
                                Iden.Numero = ContadorID;
                                Iden.Descripcion = Identificador;
                                TablaSimbolos.Add(Iden);

                                TokensLinea[ContadorToken] = "ID" + ContadorID;

                                ContadorID++;
                            }
                        }
                        else if (tokens.ContainsKey(Resultado[1]))
                        {
                            Iden = CrearId(ContadorID, Resultado[1], Cadena);
                            TablaSimbolos.Add(Iden);
                            TokensLinea[ContadorToken] = Resultado[1];
                            ContadorID++;
                        }
                        else
                        {
                            TokensLinea[ContadorToken] = Resultado[1];
                        }
                    }
                    else if (Resultado[0] == "Error")
                    {
                        Error error = new Error();
                        error.Linea = ContadorLinea;
                        error.Descripcion = Resultado[1];
                        error.Cadena = Cadena;
                        Errores.Add(error);
                        TokensLinea[ContadorToken] = Resultado[0];
                    }
                    ContadorToken++;
                }
                ContadorLinea++;
                ArchivoTokens.Add(TokensLinea);
            }
        }

        public void AsignarTDIdentificador()
        {
            foreach (string[] arreglo in ArchivoTokens)
            {
                string cadenaAux = "";
                foreach (string s in arreglo)
                {
                    string cadenaActual = s;

                    if (cadenaActual.Contains("ID"))
                    {
                        foreach (Identificador ID in TablaSimbolos)
                        {
                            if (cadenaActual == "ID" + ID.Numero)
                            {
                                switch (cadenaAux)
                                {
                                    case "PR13":
                                        ID.TipoDato = "int";
                                        break;
                                    case "PR14":
                                        ID.TipoDato = "double";
                                        break;
                                    case "PR15":
                                        ID.TipoDato = "float";
                                        break;
                                    case "PR16":
                                        ID.TipoDato = "char";
                                        break;
                                    case "PR17":
                                        ID.TipoDato = "string";
                                        break;
                                    case "PR18":
                                        ID.TipoDato = "bool";
                                        break;
                                    case "PR19":
                                        ID.TipoDato = "null";
                                        break;
                                    case "PR22":
                                        ID.TipoDato = "void";
                                        break;
                                }
                            }
                        }
                    }
                    cadenaAux = cadenaActual;
                }
            }
        }

        public Identificador CrearId(int contadorID, string Resultado, string cadena)
        {
            Identificador identificador = new Identificador();
            identificador.Numero = contadorID;
            identificador.Descripcion = tokens[Resultado] + contadorID;
            identificador.TipoDato = values[Resultado];
            switch(Resultado)
            {
                case "CNE":
                    identificador.Valor = int.Parse(cadena);
                    break;

                case "CNR":
                    if (cadena[cadena.Length - 2] == 'f')
                    {
                        identificador.Valor = float.Parse(cadena.Replace("f",""));
                        identificador.TipoDato = "float";
                    }
                    else
                    {
                        identificador.Valor = double.Parse(cadena);
                    }
                    break;

                case "CNEX":
                    identificador.Valor = double.Parse(cadena);
                    break;

                case "CADENAS":
                    identificador.Valor = cadena;
                    break;

                case "CHAR":
                    string temp = cadena;
                    temp = temp.Substring(0,temp.Length-1);
                    identificador.Valor = temp;
                    break;

                case "PR23":
                case "PR24":
                    identificador.Valor = bool.Parse(cadena);
                    break;
            }

            return identificador;
        }

    }
}
