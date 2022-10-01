using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProgramaLexico
{
    public class AnalizadorSintactico
    {
        public List<string[]> ArchivoTokens;

        public string[,] Gramaticas;

        public List<Error> Errores;

        public void LlenarGramaticas()
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=JaimePC\MSSQLSERVER01;Database=Automatas;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM GR$", cnn);
            DataTable dataTable = new DataTable();

            try
            {
                cnn.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                Adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con la conexion a la base de datos", "Error");
            }
            finally
            {
                cnn.Close();
            }

            Gramaticas = new string[dataTable.Rows.Count, dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Gramaticas[i, j] = (string)dataTable.Rows[i][j];
                }
            }
        }

        public AnalizadorSintactico(List<string[]> Tokens)
        {
            ArchivoTokens = Tokens;
            LlenarGramaticas();
        }

        /**
        public void Analizar()
        {
            foreach(string[] linea in ArchivoTokens)
            {
                int numTokens = linea.Length;
                int numActual = linea.Length;

                for (int i = linea.Length - 1; i >= 0; i--)
                {
                    for (int j = 0; j < linea.Length; j++)
                    {

                    }
                }
            }
        }

        public void ReducirLineas()
        {
            int posActual = 0;
            int cantTokens = 0;
            string Resultado = "";

            foreach(string[] Linea in ArchivoTokens)
            {
                cantTokens = Linea.Length;
                posActual = 0;
                
                while(Resultado != "S")
                {
                    posActual = 0;
                    while(posActual + cantTokens <= Linea.Length)
                    {
                        Resultado = ReducirCadena(Linea.SubArray(posActual,cantTokens));
                        if (Resultado == "Error")
                        {
                            if(cantTokens == 1 && posActual == Linea.Length)
                            {
                                Resultado = "Error de sintaxis";
                                    break;
                            }

                            cantTokens--;
                            break;
                        }
                        else
                        {

                        }

                        posActual++;
                    }
                }
            }
        }
        **/

        public string ReducirCadena(string[] Cadena)
        {
            string Linea = ConcatenarArreglo(Cadena);
            int i = 0;

            bool reducido = false;

            while(reducido == false)
            {
                if (Linea == Gramaticas[1,i])
                {
                    Linea = Gramaticas[0, i];
                    reducido = true;
                }
                else if(i >= Gramaticas.GetLength(1))
                {
                    return "Error";
                }
                i++;
            }
            return Linea;
        }

        public string ConcatenarArreglo(string[] Cadena)
        {
            string Linea = "";
            for (int i = 0; i < Cadena.Length; i++)
            {
                Linea += Cadena[i].Contains("ID") ? "ID " : Cadena[i];
            }
            return Linea.Trim();
        }
    }
}
