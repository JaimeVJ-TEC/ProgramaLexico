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

        public TraductorEnsamblador(List<Triples> ListaTripletas, List<Identificador> Simbolos)
        {
            Tripletas = ListaTripletas;
            TablaSimbolos = Simbolos;
        }
    }
}
