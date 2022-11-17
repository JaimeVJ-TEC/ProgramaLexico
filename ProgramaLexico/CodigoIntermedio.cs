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
        Dictionary<string, int> PrioridadDeOperadores = new Dictionary<string, int>() { {"OPAR1",4 }, { "OPAR2", 4 }, { "OPAR3", 5 },
                                                                                        {"OPAR4",5 }, { "OPAR5", 6 }, { "OPAR6", 3 },
                                                                                        {"OPAR7",3 }, { "OPAR8", 7 }, { "OPAR9", 7 },
                                                                                        {"OPRE1",2 }, { "OPRE2", 2 }, { "OPRE3", 2 },
                                                                                        {"OPRE4",2 }, { "OPRE5", 2 },{ "OPRE6", 2 },
                                                                                        {"OPL1", 1 }, {"OPL2",1 }, { "OPL3", 1 },
                                                                                        {"CAE11",0 }, {"CAE12",0 }, { "OPAS", 0 }};


        string[] OperadoresAritmeticos = new string[] { "OPAR1", "OPAR2", "OPAR3", "OPAR4", "OPAR5", "OPAR6", "OPAR7", "OPAR8", "OPAR9" };
        string[] OperadoresRelacionales = new string[] { "OPRE1", "OPRE2", "OPRE3", "OPRE4", "OPRE5", "OPRE6" };
        string[] OperadoresLogicos = new string[] { "OPL1", "OPL2", "OPL3" };

        public List<Triples> Tripletas = new List<Triples>();

        public CodigoIntermedio(List<string[]> ArchivoTokensN)
        { 
            ArchivoTokensNumero = ArchivoTokensN;
            ArchivoTokensPostfijo = new List<string[]>();
            foreach (string[] Linea in ArchivoTokensNumero)
            {
                string[] Copia = new string[Linea.Length];
                Linea.CopyTo(Copia, 0);
                ArchivoTokensPostfijo.Add(Copia);
            }
            ArchivoTokensPostfijo = ConversionPostfija(ArchivoTokensPostfijo);
            GeneracionTripletas(LineasStacks);
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

        public void GeneracionTripletas(List<Stack<string>> ListaStacks)
        {
            int EtiquetaCont = 0;
            Triples MainTripleta = new Triples();

            foreach (Stack<string> Linea in ListaStacks)
            {
                Stack<string> Operandos = new Stack<string>();
                int TempCont = 1;
                Renglon renglon;

                while (Linea.Count > 0)
                {
                    string Token = Linea.Pop();

                    if (OperadoresAritmeticos.Contains(Token))
                    {
                        if (Token == "OPAR8" || Token == "OPAR9")
                        {
                            renglon = new Renglon();
                            renglon.DatoObjeto = Operandos.Pop();
                            renglon.Operador = Token;
                        }
                        else
                        {
                            string datoObjeto = "T" + TempCont;
                            string datoFuente2 = Operandos.Pop();
                            string datoFuente1 = Operandos.Pop();

                            if (datoFuente1 == datoObjeto)
                            {
                                renglon = new Renglon();
                                renglon.DatoObjeto = datoFuente1;
                                renglon.DatoFuente = datoFuente2;
                                renglon.Operador = Token;
                                MainTripleta.Renglones.Add(renglon);
                                Operandos.Push(datoObjeto);
                            }
                            else
                            {
                                renglon = new Renglon();
                                renglon.DatoObjeto = datoObjeto;
                                renglon.DatoFuente = datoFuente1;
                                renglon.Operador = "OPAS";
                                MainTripleta.Renglones.Add(renglon);

                                renglon = new Renglon();
                                renglon.DatoObjeto = datoObjeto;
                                renglon.DatoFuente = datoFuente2;
                                renglon.Operador = Token;
                                MainTripleta.Renglones.Add(renglon);

                                Operandos.Push(datoObjeto);
                            }

                        }
                    }
                    else if(OperadoresRelacionales.Contains(Token))
                    {

                        TempCont++;
                    }
                    else if(OperadoresLogicos.Contains(Token))
                    {

                        TempCont++;
                    }
                    else if(Token == "OPAS")
                    {
                        string datoFuente2 = Operandos.Pop();
                        string datoFuente1 = Operandos.Pop();

                        renglon = new Renglon();
                        renglon.DatoObjeto = datoFuente1;
                        renglon.DatoFuente = datoFuente2;
                        renglon.Operador = "OPAS";
                        MainTripleta.Renglones.Add(renglon);
                        TempCont++;
                    }
                    else
                    {
                        Operandos.Push(Token);
                    }
                }
            }
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
