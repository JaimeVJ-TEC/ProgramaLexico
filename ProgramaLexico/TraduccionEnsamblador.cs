using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class TraductorEnsamblador
    {
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
                if(CheckIfID(id.Descripcion) || id.TipoDato == "int")
                {
                    ArchivoASM += id.Descripcion + " DWORD ? \n";
                }
                else if(id.Descripcion.Contains("CAD"))
                {
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

    }
}
