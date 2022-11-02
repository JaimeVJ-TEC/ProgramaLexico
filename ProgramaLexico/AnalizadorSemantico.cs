using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaLexico
{
    public class AnalizadorSemantico
    {
        public List<string[]> ArchivoTokens;
        public List<string[]> ArchivoTokensTipos;
        public List<Identificador> TablaSimbolosSem;

        public AnalizadorSemantico(List<string[]> ArchivoTokensConNumeros, List<Identificador> TablaSimbolos)
        {
            ArchivoTokens = ArchivoTokensConNumeros;
            TablaSimbolosSem = TablaSimbolos;
            ArchivoTokensTipos = ConvertirTipos(ArchivoTokensConNumeros);
        }

        public List<string[]> ConvertirTipos(List<string[]> ArchivoTokensConNumeros)
        {
            Dictionary<string, bool> Tipos = new Dictionary<string, bool>() { { "CNR", true }, { "CNE", true }, { "CAD", true }, { "CHA", true }, { "BLN", true } };

            List<string[]> ArchivoTokensT = ArchivoTokensConNumeros;

            foreach(string[] linea in ArchivoTokensT)
            {
                for (int i = 0; i < linea.Length; i++)
                {
                    if(Tipos.TryGetValue(linea[i].Substring(0, 3),out bool value) || linea[i].Substring(0,2) == "ID")
                    {
                        int numero = int.Parse(linea[i][linea[i].Length - 1].ToString());
                        linea[i] = TablaSimbolosSem[numero].TipoDato;
                    }

                }
            }

            return ArchivoTokensT;
        }
    }
}
