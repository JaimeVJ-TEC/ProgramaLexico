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
        Dictionary<string, int> PrioridadDeOperadores = new Dictionary<string, int>() { {"OPAR1",4 }, { "OPAR2", 4 }, { "OPAR3", 5 },
                                                                                        {"OPAR4",5 }, { "OPAR5", 6 }, { "OPAR6", 3 },
                                                                                        {"OPAR7",3 }, { "OPAR8", 7 }, { "OPAR9", 7 },
                                                                                        {"OPRE1",2 }, { "OPRE2", 2 }, { "OPRE3", 2 },
                                                                                        {"OPRE4",2 }, { "OPRE5", 2 },{ "OPRE6", 2 },
                                                                                        {"OPL1", 1 }, {"OPL2",1 }, { "OPL3", 1 },
                                                                                        {"CAE11",0 }, {"CAE12",0 }, { "OPAS", 0 }};

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

                ArchivoTokensN[i] = ResultadoAux.ToArray();
            }
            return ArchivoTokensN;
        }
    }
}
