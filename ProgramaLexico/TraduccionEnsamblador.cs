using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class TraductorEnsamblador
    {
        public List<Variable> Variables = new List<Variable>();
        public List<Triples> Tripletas = new List<Triples>();
        public List<Identificador> TablaSimbolos;
        public string ArchivoASM;

        public TraductorEnsamblador(List<Triples> ListaTripletas, List<Identificador> Simbolos)
        {
            Tripletas = ListaTripletas;
            TablaSimbolos = Simbolos;

            ArchivoASM = "INCLUDE C:\\Irvine32\\Irvine32.inc " +
                         "\n.data\n";
            ArchivoASM += addVars();
            ArchivoASM += ".code\n main PROC\n";
            string[] Body = WriteBody(ListaTripletas[ListaTripletas.Count - 1],0);
            ArchivoASM += Body[0]+Body[1];
            ArchivoASM += "exit\n main ENDP\n END main";

            if (File.Exists("Program.asm"))
                File.Delete("Program.asm");
            string[] Lines = ArchivoASM.Split('\n');
            File.WriteAllLines("Program.asm", Lines);

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "run.bat";
            proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            proc.Start();
        }

        public string addVars()
        {
            string Text = "";
            foreach(Identificador id in TablaSimbolos)
            {
                if (CheckIfID(id.Descripcion) && id.TipoDato == "int")
                {
                    Variable var = new Variable();
                    var.Nombre = "ID"+id.Numero.ToString();
                    var.Tipo = id.TipoDato;
                    Text += var.Nombre + " DWORD ? \n";
                    Variables.Add(var);
                }
                else if(id.Descripcion.Contains("CAD"))
                {
                    Variable var = new Variable();
                    var.Nombre = id.Descripcion;
                    var.Tipo = id.TipoDato;

                    string aux = id.Valor;
                    aux = aux.Replace("\"","");
                    Text += id.Descripcion + " BYTE \"" + aux + "\",0  \n";
                    Variables.Add(var);
                }
            }

            return Text;
        }

        public string[] WriteBody(Triples MainTripleta,int Unmarked)
        {
            Dictionary<int, int> UnmarkedTags = new Dictionary<int, int>();
            string LastRel = "";
            string TextStart = "";
            string Text = "";

            int NumLinea = 0;

            foreach(Renglon r in MainTripleta.Renglones)
            {
                string Argumento1 = r.Argumento1;
                string Argumento2 = "";
                if(!(r.Argumento2 is null))
                    Argumento2 = CheckIfID(r.Argumento2) || VarExists(r.Argumento2) ? r.Argumento2 : GetIntValueFromSymbols(r.Argumento2).ToString();

                if (r.Argumento1[0] == 'T' && !VarExists(r.Argumento1) && !r.Argumento1.Contains("TR"))
                {
                    Variable var = new Variable();
                    var.Nombre = Argumento1;
                    var.Tipo = "int";
                    Variables.Add(var);
                    TextStart += "LOCAL " + var.Nombre + ":DWORD\n";
                }

                if(UnmarkedTags.ContainsKey(NumLinea))
                {
                    Text += "U" + UnmarkedTags[NumLinea] + ":";
                }

                switch(r.Operador)
                {
                    case "OPAS":
                        Text += "MOV EAX," + Argumento2 + "\n";
                        Text += "MOV " + Argumento1 + ",EAX\n";
                        break;

                    case "OPAR1":
                        Text += "MOV EAX," + Argumento1 + "\n";
                        Text += "ADD EAX," + Argumento2 + "\n";
                        Text += "MOV " + Argumento1 + ",EAX\n";
                        break;

                    case "PR20":
                        if(r.Argumento1.Contains("CAD"))
                        {
                            Text += "LEA EDX,"+Argumento1+"\n";
                            Text += "CALL WriteString\n";
                        }
                        else
                        {
                            Text += "MOV EAX," + Argumento1 + "\n";
                            Text += "CALL WriteDec\n";
                        }
                        break;

                    case "PR21":
                        Text += "CALL ReadInt\n";
                        Text += "MOV " + Argumento1 + ",EAX\n";
                        break;


                    case "OPRE1":
                    case "OPRE2":
                    case "OPRE3":
                    case "OPRE4":
                    case "OPRE5":
                    case "OPRE6":
                        Text += "MOV EAX, "+Argumento1+"\n";
                        Text += "CMP EAX, "+Argumento2+"\n";
                        LastRel = r.Operador;
                        break;

                    default:
                        if(r.Argumento1.Contains("TR"))
                        {
                            string EtiquetaLine = MainTripleta.Renglones[int.Parse(r.Operador)].Argumento2;
                            if (!EtiquetaLine.Contains('L'))
                            {
                                EtiquetaLine = "U" + Unmarked;
                                UnmarkedTags.Add(int.Parse(r.Operador),Unmarked);
                                Unmarked++;
                            }

                            if (r.Argumento2 == "TRUE")
                            {
                                switch (LastRel)
                                {
                                    case "OPRE1":
                                        Text += "JE " + EtiquetaLine + "\n";
                                        break;
                                    case "OPRE2":
                                        Text += "JG " + EtiquetaLine + "\n";
                                        break;
                                    case "OPRE3":
                                        Text += "JL " + EtiquetaLine + "\n";
                                        break;
                                    case "OPRE4":
                                        Text += "JLE " + EtiquetaLine + "\n";
                                        break;
                                    case "OPRE5":
                                        Text += "JGE " + EtiquetaLine + "\n";
                                        break;
                                    case "OPRE6":
                                        Text += "JNE " + EtiquetaLine + "\n";
                                        break;
                                }
                            }
                            else
                            {
                                Text += "JMP " + EtiquetaLine + "\n";
                            }
                        }
                        else if(r.Argumento1 == "ET")
                        {
                            if(!(r.Argumento2 is null))
                            {
                                if (r.Argumento2.Contains('L'))
                                {
                                    Text += r.Argumento2 + ":\n";
                                    foreach (Triples T in Tripletas)
                                    {
                                        if (T.Nombre == r.Argumento2)
                                        {
                                            string[] AuxStrings = WriteBody(T,Unmarked);
                                            TextStart += AuxStrings[0];
                                            Text += AuxStrings[1];
                                        }
                                    }
                                }
                                else if (r.Argumento2.Contains('E'))
                                {
                                    Text += r.Argumento2 + ":\n";
                                }
                            }
                            else
                            {
                                Text += "JMP " + MainTripleta.Renglones[int.Parse(r.Operador)].Argumento2 + "\n";
                            }
                        }

                        break;
                }

                NumLinea++;
            }

            string[] TextRet = { TextStart, Text };
            return TextRet;
        }

        public bool CheckIfID(string desc)
        {
            return !(desc.Contains("CNE") || desc.Contains("CAD") || desc.Contains("CNR") || desc.Contains("BLN") || desc.Contains("CHA"));
        }

        public int GetIntValueFromSymbols(string Desc)
        {
            foreach(Identificador id in TablaSimbolos)
            {
                if (id.Descripcion == Desc)
                    return id.Valor;
            }
            return 0;
        }

        public bool VarExists(string Nombre)
        {
            foreach(Variable v in Variables)
            {
                if (v.Nombre == Nombre)
                    return true;
            }
            return false;
        }

        public struct Variable
        {
            public string Nombre { get; set; }
            public string Tipo { get; set; }
            public string Valor { get; set; }
        }
    }
}
