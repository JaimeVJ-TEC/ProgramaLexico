using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class Triples
    {
        public string Nombre { get; set; }

        public List<Renglon> Renglones = new List<Renglon>();
    }

    public struct Renglon
    {
        public string DatoObjeto { get; set; }

        public string DatoFuente { get; set; }

        public string Operador { get; set; }
    }
}
