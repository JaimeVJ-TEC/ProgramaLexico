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
    public class Conexion
    {
        SqlConnection Connection;
        SqlCommand CommandGramatica; 
        SqlCommand CommandLexico;

        public Conexion()
        {
            Connection = new SqlConnection(@"Data Source=DESKTOP-ENTQP32;Database=Automatas;Integrated Security=True");
            CommandGramatica = new SqlCommand(@"SELECT * FROM AG$", Connection);
            CommandLexico = new SqlCommand(@"SELECT * FROM BD$", Connection);
        }

        public DataTable MatrizDeTransicion()
        {
            DataTable dataTable = new DataTable();

            try
            {
                Connection.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(CommandLexico);
                Adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con la conexion a la base de datos", "Error");
            }
            finally
            {
                Connection.Close();
            }

            return dataTable;
        }

        public DataTable Gramaticas()
        {
            DataTable dataTable = new DataTable();
            try
            {
                Connection.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(CommandGramatica);
                Adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema con la conexion a la base de datos", "Error");
            }
            finally
            {
                Connection.Close();
            }

            return dataTable;
        }
    }
}
