using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class CodigoIntermedio
    {
        public List<string[]> ArchivoTokensNumero;
        public List<string[]> ArchivoTokensPostfijo;
        public List<Stack<string>> LineasStacks = new List<Stack<string>>();
        public List<Identificador> TablaSimbolos;
        int EtiquetaCont = 0;
        int TempCont = 0;
        int TempRel = 0;

        Dictionary<string, int> PrioridadDeOperadores = new Dictionary<string, int>() { {"OPAR1",4 }, { "OPAR2", 4 }, { "OPAR3", 5 },
                                                                                        {"OPAR4",5 }, { "OPAR5", 6 }, { "OPAR6", 3 },
                                                                                        {"OPAR7",3 }, { "OPAR8", 7 }, { "OPAR9", 7 },
                                                                                        {"OPRE1",2 }, { "OPRE2", 2 }, { "OPRE3", 2 },
                                                                                        {"OPRE4",2 }, { "OPRE5", 2 },{ "OPRE6", 2 },
                                                                                        {"OPL1", 1 }, {"OPL2",0 }, { "OPL3", -1 },
                                                                                        {"CAE11",-2 }, {"CAE12",-2 }, { "OPAS", -2 },
                                                                                        { "PR3", -3 }, { "PR21", -3 }, { "PR20", -3 }};
        string[] OperadoresAritmeticos = new string[] { "OPAR1", "OPAR2", "OPAR3", "OPAR4", "OPAR5", "OPAR6", "OPAR7", "OPAR8", "OPAR9" };
        string[] OperadoresRelacionales = new string[] { "OPRE1", "OPRE2", "OPRE3", "OPRE4", "OPRE5", "OPRE6" };
        string[] OperadoresLogicos = new string[] { "OPL1", "OPL2", "OPL3" };

        public List<Triples> Tripletas = new List<Triples>();

        public CodigoIntermedio(List<string[]> ArchivoTokensN, List<Identificador> TablaS)
        {
            TablaSimbolos = TablaS;
            ArchivoTokensNumero = ArchivoTokensN;
            ArchivoTokensPostfijo = new List<string[]>();
            foreach (string[] Linea in ArchivoTokensNumero)
            {
                string[] Copia = new string[Linea.Length];
                Linea.CopyTo(Copia, 0);
                ArchivoTokensPostfijo.Add(Copia);
            }
            ArchivoTokensPostfijo = ConversionPostfija(ArchivoTokensPostfijo);
            GeneracionTripletas(LineasStacks,"Main");
        }

        public List<string[]> ConversionPostfija(List<string[]> ArchivoTokensN)
        {
            Stack<string> Operadores = new Stack<string>();
            Stack<string> Resultado = new Stack<string>();
            Stack<string> ResultadoAux = new Stack<string>();

            for (int i = 0; i < ArchivoTokensN.Count; i++)
            {
                Operadores = new Stack<string>();
                Resultado = new Stack<string>();
                ResultadoAux = new Stack<string>();

                for (int j = 0; j < ArchivoTokensN[i].Length; j++)
                {
                    string Token = ArchivoTokensN[i][j].Trim();

                    if(!PrioridadDeOperadores.ContainsKey(Token))
                    {
                        Resultado.Push(Token);
                    }
                    else if(Token == "CAE11")
                    {
                        Operadores.Push(Token);
                    }
                    else if(Token == "CAE12")
                    {
                        while(Operadores.Count > 0 && Operadores.Peek() != "CAE11")
                        {
                            Resultado.Push(Operadores.Pop());
                        }

                        if(!(Operadores.Count > 0 && Operadores.Peek() != "CAE11"))
                        {
                            Operadores.Pop();
                        }
                    }
                    else
                    {
                        while(Operadores.Count > 0 && PrioridadDeOperadores[Token] <= PrioridadDeOperadores[Operadores.Peek()])
                        {
                            Resultado.Push(Operadores.Pop());
                        }

                        Operadores.Push(Token);
                    }
                }

                while(Operadores.Count > 0)
                {
                    Resultado.Push(Operadores.Pop());
                }

                while(Resultado.Count > 0)
                {
                    ResultadoAux.Push(Resultado.Pop());
                }
                LineasStacks.Add(new Stack<string>(new Stack<string>(ResultadoAux)));

                ArchivoTokensN[i] = ResultadoAux.ToArray();
            }
            return ArchivoTokensN;
        }

        public void GeneracionTripletas(List<Stack<string>> ListaStacks,string Nombre)
        {
            Triples MainTripleta = new Triples();
            MainTripleta.Nombre = Nombre;
            string LastObjeto = "";
            int NumLinea = 0;

            for (int i = 0; i < ListaStacks.Count; i++)
            {
                Stack<string> Linea = ListaStacks[i];
                Stack<string> Operandos = new Stack<string>();
                Renglon renglon;

                while (Linea.Count > 0)
                {
                    string Token = Linea.Pop();

                    if (OperadoresAritmeticos.Contains(Token))
                    {
                        if (Token == "OPAR8" || Token == "OPAR9")
                        {
                            renglon = new Renglon();
                            renglon.Argumento1 = Operandos.Pop();
                            renglon.Operador = Token;
                            MainTripleta.Renglones.Add(renglon);
                        }
                        else
                        {
                            string datoObjeto = "T" + TempCont;
                            string datoFuente2 = Operandos.Pop();
                            string datoFuente1 = Operandos.Pop();

                            if (datoFuente1 == LastObjeto)
                            {
                                renglon = new Renglon();
                                renglon.Argumento1 = datoFuente1;
                                renglon.Argumento2 = datoFuente2;
                                renglon.Operador = Token;
                                MainTripleta.Renglones.Add(renglon);
                                Operandos.Push(LastObjeto);
                            }
                            else if (datoFuente2 == LastObjeto)
                            {
                                renglon = new Renglon();
                                renglon.Argumento1 = datoFuente1;
                                renglon.Argumento2 = datoFuente2;
                                renglon.Operador = Token;
                                MainTripleta.Renglones.Add(renglon);
                                Operandos.Push(datoFuente1);
                            }
                            else
                            {
                                renglon = new Renglon();
                                renglon.Argumento1 = datoObjeto;
                                renglon.Argumento2 = datoFuente1;
                                renglon.Operador = "OPAS";
                                MainTripleta.Renglones.Add(renglon);

                                renglon = new Renglon();
                                renglon.Argumento1 = datoObjeto;
                                renglon.Argumento2 = datoFuente2;
                                renglon.Operador = Token;
                                MainTripleta.Renglones.Add(renglon);

                                Operandos.Push(datoObjeto);
                                LastObjeto = datoObjeto;
                                TempCont++;
                            }
                        }
                    }
                    else if(OperadoresRelacionales.Contains(Token))
                    {
                        string datoObjeto = "T" + TempCont;
                        string datoFuente2 = Operandos.Pop();
                        string datoFuente1 = Operandos.Pop();

                        renglon = new Renglon();
                        renglon.Argumento1 = datoObjeto;
                        renglon.Argumento2 = datoFuente1;
                        renglon.Operador = "OPAS";
                        MainTripleta.Renglones.Add(renglon);

                        renglon = new Renglon();
                        renglon.Argumento1 = datoObjeto;
                        renglon.Argumento2 = datoFuente2;
                        renglon.Operador = Token;
                        MainTripleta.Renglones.Add(renglon);

                        Operandos.Push("TR" + TempRel);
                        LastObjeto = datoObjeto;
                        TempCont++;

                        if (ContarOperadoresLogicos(Linea) != 0)
                        {
                            renglon = new Renglon();
                            renglon.Argumento1 = "TR" + TempRel;
                            renglon.Argumento2 = "TRUE";
                            MainTripleta.Renglones.Add(renglon);
                            renglon = new Renglon();
                            renglon.Argumento1 = "TR" + TempRel;
                            renglon.Argumento2 = "FALSE";
                            MainTripleta.Renglones.Add(renglon);
                            TempRel++;
                        }
                        else
                        {
                            renglon = new Renglon();
                            renglon.Argumento1 = "TR" + TempRel;
                            renglon.Argumento2 = "TRUE";
                            renglon.Operador = (MainTripleta.Renglones.Count + 2).ToString();
                            MainTripleta.Renglones.Add(renglon);
                            renglon = new Renglon();
                            renglon.Argumento1 = "TR" + TempRel;
                            renglon.Argumento2 = "FALSE";
                            renglon.Operador = (MainTripleta.Renglones.Count + 3).ToString();
                            MainTripleta.Renglones.Add(renglon);
                            TempRel++;
                        }
                    }
                    else if(OperadoresLogicos.Contains(Token))
                    {
                        int CantidadOPL = ContarOperadoresLogicos(Linea);
                        int Celse = BuscarElseCorrespondiente(NumLinea+1, ListaStacks);

                        int aux1 = Celse == -1 ? 1 : 2;
                        string datoFuente2 = Operandos.Pop();
                        string datoFuente1 = Operandos.Pop();

                        int NumRenglon1 = GetLineNumber(MainTripleta,datoFuente1, "TRUE");
                        int NumRenglon2 = GetLineNumber(MainTripleta, datoFuente2, "TRUE");

                        if (Token == "OPL2")
                        {
                            //TR1
                            renglon = MainTripleta.Renglones[NumRenglon1];
                            renglon.Operador = (NumRenglon2 - 2).ToString();
                            MainTripleta.Renglones[NumRenglon1] = renglon;
                            //TR1
                            renglon = MainTripleta.Renglones[NumRenglon1+1];
                            renglon.Operador = (MainTripleta.Renglones.Count+aux1).ToString();
                            MainTripleta.Renglones[NumRenglon1+1] = renglon;

                            //TR2
                            renglon = MainTripleta.Renglones[NumRenglon2];
                            renglon.Operador = (MainTripleta.Renglones.Count).ToString();
                            MainTripleta.Renglones[NumRenglon2] = renglon;
                            //TR2
                            renglon = MainTripleta.Renglones[NumRenglon2+1];
                            renglon.Operador = (MainTripleta.Renglones.Count + aux1).ToString();
                            MainTripleta.Renglones[NumRenglon2 + 1] = renglon;

                        }
                        else if (Token == "OPL3")
                        {
                            //TR1
                            renglon = MainTripleta.Renglones[NumRenglon1];
                            renglon.Operador = (MainTripleta.Renglones.Count).ToString();
                            MainTripleta.Renglones[NumRenglon1] = renglon;
                            //TR1
                            renglon = MainTripleta.Renglones[NumRenglon1 + 1];
                            renglon.Operador = (NumRenglon2 - 2).ToString(); ;
                            MainTripleta.Renglones[NumRenglon1 + 1] = renglon;

                            //TR2
                            renglon = MainTripleta.Renglones[NumRenglon2];
                            renglon.Operador = (MainTripleta.Renglones.Count).ToString();
                            MainTripleta.Renglones[NumRenglon2] = renglon;
                            //TR2
                            renglon = MainTripleta.Renglones[NumRenglon1 + 1];
                            renglon.Operador = (MainTripleta.Renglones.Count + 2).ToString();
                            MainTripleta.Renglones[NumRenglon2 + 1] = renglon;
                        }

                        TempCont++;
                    }
                    else if (Token == "PR3")
                    {
                        int Else = BuscarElseCorrespondiente(NumLinea, ListaStacks);
                        int Close = BuscarLineaLlaveCorrespondiente(NumLinea + 1, ListaStacks);
                        int Count = Close - (NumLinea + 1);

                        renglon = new Renglon();
                        renglon.Argumento1 = "ET";
                        renglon.Argumento2 = "L" + EtiquetaCont;
                        EtiquetaCont++;
                        MainTripleta.Renglones.Add(renglon);

                        List<Stack<string>> SubList = ListaStacks.GetRange(NumLinea + 1, Count);
                        GeneracionTripletas(SubList, "L" + (EtiquetaCont - 1));

                        if (Else != -1)
                        {
                            renglon = new Renglon();
                            renglon.Argumento1 = "ET";
                            renglon.Operador = (MainTripleta.Renglones.Count + 2).ToString();
                            MainTripleta.Renglones.Add(renglon);

                            int CloseE = BuscarLineaLlaveCorrespondiente(Else + 1, ListaStacks);
                            int CountE = CloseE - (Else + 1);

                            renglon = new Renglon();
                            renglon.Argumento1 = "ET";
                            renglon.Argumento2 = "L" + EtiquetaCont;
                            MainTripleta.Renglones.Add(renglon);

                            List<Stack<string>> SubList1 = ListaStacks.GetRange(Else + 1, CountE);
                            ListaStacks = ListaStacks.Except(SubList1).ToList();
                            GeneracionTripletas(SubList1, "L" + EtiquetaCont);
                        }

                        ListaStacks = ListaStacks.Except(SubList).ToList();
                        renglon = new Renglon();
                        EtiquetaCont++;
                        renglon.Argumento1 = "ET";
                        renglon.Argumento2 = "E" + EtiquetaCont;
                        MainTripleta.Renglones.Add(renglon);
                    }
                    else if(Token == "OPAS")
                    {
                        string datoFuente2 = Operandos.Pop();
                        string datoFuente1 = Operandos.Pop();

                        renglon = new Renglon();
                        renglon.Argumento1 = datoFuente1;
                        renglon.Argumento2 = datoFuente2;
                        renglon.Operador = "OPAS";
                        MainTripleta.Renglones.Add(renglon);
                    }
                    else if(Token == "PR20" || Token == "PR21")
                    {
                        renglon = new Renglon();
                        renglon.Argumento1 = Operandos.Pop();
                        renglon.Operador = Token;
                        MainTripleta.Renglones.Add(renglon);
                    }
                    else
                    {
                        Operandos.Push(Token);
                    }
                }
                NumLinea++;
            }

            int aux = 0;
            foreach(Renglon r in MainTripleta.Renglones)
            {
                Debug.WriteLine(aux+"|  "+r.Argumento1 +" | "+ r.Operador + " | " + r.Argumento2);
                aux++;
            }

            Tripletas.Add(MainTripleta);
        }

        public int GetLineNumber(Triples Main,string Arg1,string Arg2)
        {
            for (int i = 0; i < Main.Renglones.Count; i++)
            {
                if (Main.Renglones[i].Argumento1 == Arg1 && Main.Renglones[i].Argumento2== Arg2)
                    return i;
            }
            return -1;
        }

        public int BuscarLineaLlaveCorrespondiente(int numLinea, List<Stack<string>> ArchivoStacks)
        {
            Stack<string> StackAux = new Stack<string>();
            for (int i = numLinea; i < ArchivoStacks.Count; i++)
            {
                string[] TempArray = ArchivoStacks[i].ToArray();
                foreach(string s in TempArray)
                {
                    if (s == "CAE9")
                    {
                        StackAux.Push(s);
                    }
                    else if (s == "CAE10")
                    {
                        StackAux.Pop();

                        if (StackAux.Count == 0)
                            return i;
                    }
                }
            }
            return -1;
        }

        public int BuscarElseCorrespondiente(int numLinea, List<Stack<string>> ArchivoStacks)
        {
            Stack<string> StackAux = new Stack<string>();
            StackAux.Push("PR3");

            for (int i = numLinea; i < ArchivoStacks.Count; i++)
            {
                string[] TempArray = ArchivoStacks[i].ToArray();
                foreach (string s in TempArray)
                {
                    if (s == "PR3")
                    {
                        StackAux.Push(s);
                    }
                    else if (s == "PR4")
                    {
                        StackAux.Pop();

                        if (StackAux.Count == 0)
                            return i;
                    }
                }
            }
            return -1;
        }

        public int ContarOperadoresLogicos(Stack<string> Linea)
        {
            string[] ArregloLinea = Linea.ToArray();

            int CantidadOPL = 0;

            for (int i = 0; i < ArregloLinea.Length; i++)
            {
                if (ArregloLinea[i] == "OPL2" || ArregloLinea[i] == "OPL3")
                    CantidadOPL++;
            }

            return CantidadOPL;
        }

        public string SiguienteOperadorLogico(Stack<string> Linea)
        {
            string[] ArregloLinea = Linea.ToArray();

            for (int i = 0; i < ArregloLinea.Length; i++)
            {
                if (ArregloLinea[i] == "OPL2" || ArregloLinea[i] == "OPL3")
                    return "&";
                else if (ArregloLinea[i] == "OPL3")
                    return "|";
                else if (ArregloLinea[i] == "OPL1")
                    return "!";
            }

            return "0";
        }

        public string ConcatenarArreglo(string[] Cadena)
        {
            string Linea = "";
            for (int i = 0; i < Cadena.Length; i++)
            {
                Linea += Cadena[i] + " ";
            }
            return Linea.Trim();
        }

    }
}
