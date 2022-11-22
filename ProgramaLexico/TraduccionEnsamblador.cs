using System;
using System.Collections.Generic;
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
                         "\n.data";
        }

        public void addVars(string desc)
        {
            foreach(Identificador id in TablaSimbolos)
            {
                Variable var = new Variable();
                if (CheckIfID(id.Descripcion) || id.TipoDato == "int")
                {
                    var.Nombre = id.Descripcion;
                    var.Tipo = id.TipoDato;
                    ArchivoASM += id.Descripcion + " DWORD ? \n";
                }
                else if(id.Descripcion.Contains("CAD"))
                {
                    var.Nombre = id.Descripcion;
                    var.Tipo = id.TipoDato;

                    string aux = id.Valor;
                    aux = aux.Replace("\"","");
                    ArchivoASM += id.Descripcion + " BYTE \"" + aux + "\",0  \n";
                }
            }
        }

        public bool CheckIfID(string desc)
        {
            return !(desc.Contains("CNE") || desc.Contains("CAD") || desc.Contains("CNR") || desc.Contains("BLN") || desc.Contains("CHA"));
        }

        public struct Variable
        {
            public string Nombre { get; set; }
            public string Tipo { get; set; }
            public string Valor { get; set; }
        }
    }
}
