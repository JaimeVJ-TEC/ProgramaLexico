using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class Identificador
    {
        public int Numero { get; set; }

        public string Descripcion { get; set; }

        public string TipoDato { get; set; }

        //Tipo de dato  dinamico para que pueda ser cualquiera (funciona como en js)
        public dynamic Valor { get; set; }

    }
}
