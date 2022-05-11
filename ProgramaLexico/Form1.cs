using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaLexico
{
    public partial class Form1 : Form
    {
        public Celda[,] MatrizTransicion;

        public List<string[]> ArchivoTokens = new List<string[]>();

        public List<Identificador> TablaSimbolos = new List<Identificador>();

        public List<Error> Errores = new List<Error>();

        public Form1()
        {
            InitializeComponent();

            MatrizTransicion = LlenarMatriz();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgErrores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            richTextBox1.BackColor = SystemColors.InactiveCaption;
            richTextBox1.ForeColor = Color.Black;
            richTextBox2.BackColor = SystemColors.InactiveCaption;
            richTextBox2.ForeColor = Color.Black;

        }


        private void btnEvaluar_Click(object sender, EventArgs e)
        {
            EvaluarTexto();
            LlenarTokens();
            LlenarErrores();
            MarcarErrores();
        }

        public void EvaluarTexto()
        {
            string Texto = txtCadena.Text;
            string[] Lineas = SepararLineas(Texto);
            int ContadorID = 0;
            int ContadorLinea = 0;

            ArchivoTokens = new List<string[]>();
            TablaSimbolos = new List<Identificador>();
            Errores = new List<Error>();

            foreach (string Linea in Lineas)
            {
                string[] Cadenas = SepararCadenas(Linea);

                string[] TokensLinea = new string[Cadenas.Length];
                int ContadorToken = 0;

                foreach (string Cadena in Cadenas)
                {
                    string[] Resultado = EvaluarCadena(Cadena);

                    if (Resultado[0] == "Acepta")
                    {
                        if (Resultado[1] == "ID Valido.")
                        {
                            string Identificador = Cadena.Replace(" ", "");
                            bool existe = false;

                            foreach (Identificador id in TablaSimbolos)
                            {
                                if (id.Descripcion == Identificador)
                                {
                                    existe = true;
                                    TokensLinea[ContadorToken] = "ID" + id.Numero;
                                    break;
                                }
                            }

                            if (!existe)
                            {
                                Identificador Iden = new Identificador();
                                Iden.Numero = ContadorID;
                                Iden.Descripcion = Identificador;
                                TablaSimbolos.Add(Iden);

                                TokensLinea[ContadorToken] = "ID" + ContadorID;

                                ContadorID++;
                            }
                        }
                        else
                        {
                            TokensLinea[ContadorToken] = Resultado[1];
                        }
                    }
                    else if (Resultado[0] == "Error")
                    {
                        Error error = new Error();
                        error.Linea = ContadorLinea;
                        error.Descripcion = Resultado[1];
                        error.Cadena = Cadena;
                        Errores.Add(error);
                        TokensLinea[ContadorToken] = Resultado[0];
                    }
                    ContadorToken++;
                }
                ContadorLinea++;
                ArchivoTokens.Add(TokensLinea);
            }
        }

        public void LlenarTokens()
        {
            txtTokens.Text = "";
            string TextoTokens = "";

            foreach(string[] array in ArchivoTokens)
            {
                foreach(string s in array)
                {
                    TextoTokens += " "+ s;
                }
                TextoTokens += "\n";
            }

            txtTokens.Text = TextoTokens;
        }

        public void LlenarErrores()
        {
            dtgErrores.Rows.Clear();
            foreach(Error e in Errores)
            {
                dtgErrores.Rows.Add(e.Linea + 1, e.Descripcion);
            }
        }

        public void MarcarErrores()
        {
            int Aux = 0;

            txtCadena.SelectionStart = 0;
            txtCadena.SelectionLength = txtCadena.Text.Length;
            txtCadena.SelectionColor = Color.Black;

            foreach (Error e in Errores)
            {
                string cadena = e.Cadena.Replace(" ", "");
                Aux = txtCadena.Text.IndexOf(cadena);
                txtCadena.SelectionStart = Aux;
                txtCadena.SelectionLength = cadena.Length;

                if(txtCadena.SelectionColor == Color.Red)
                {
                    bool Marcado = true;
                    while (Marcado)
                    {
                        Aux = txtCadena.Text.IndexOf(cadena, Aux + cadena.Length-1);
                        txtCadena.SelectionStart = Aux;
                        Marcado = txtCadena.SelectionColor == Color.Red;
                    }
                }

                txtCadena.SelectionColor = Color.Red;

                txtCadena.SelectionStart = txtCadena.Text.Length;
                txtCadena.SelectionLength = 1;
                txtCadena.SelectionColor = Color.Black;
            }

            txtCadena.SelectionStart = txtCadena.Text.Length;
            txtCadena.SelectionColor = Color.Black;
            txtCadena.SelectionFont = new Font(txtCadena.SelectionFont, FontStyle.Regular);
        }


        /// <summary>
        /// Metodo que llena la matriz y magia negra
        /// </summary>
        /// <returns></returns>
        public Celda[,] LlenarMatriz()
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=JAIMEPC\MSSQLSERVER01;Database=Automatas;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM BD$", cnn);
            DataTable dataTable = new DataTable();

            try
            {
                cnn.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                Adapter.Fill(dataTable);
            }
            finally
            {
                cnn.Close();
            }

            Celda[,] Matriz = new Celda[dataTable.Rows.Count,dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count -1; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count -1; j++)
                {
                    Celda c = new Celda();

                    if (dataTable.Rows[i][j] != System.DBNull.Value)
                    {
                        if (dataTable.Rows[i][j] is string)
                        {
                            int tmp;

                            if (int.TryParse((string)dataTable.Rows[i][j], out tmp))
                            {
                                c.Numero = tmp;
                            }
                            else
                            {
                                c.Contenido = (string)dataTable.Rows[i][j];
                            }
                        }
                        else
                        {
                            c.Numero = (int)(double)dataTable.Rows[i][j];
                        }
                    }
                    Matriz[i, j] = c;
                }
            }

            return Matriz;
        }

        //Metodo que separa texto de entrada en "palabras"
        public string[] SepararCadenas(string Texto)
        {
            Texto = Texto.Replace("\n", " ");
            Texto = Texto + " ";
            string[] Cadenas = Regex.Split(Texto, @"(?<=[ ])");

            Cadenas = Cadenas.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            Cadenas = Cadenas.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return Cadenas;
        }

        //Metodo que separa todo el texto en lineas
        public string[] SepararLineas(string Texto)
        {
            Texto = Texto + " ";
            string[] Cadenas = Texto.Split('\n');

            return Cadenas;
        }

        //Devuelve si se acepta o es error
        public string[] EvaluarCadena(string Cadena)
        {
            int Fila = 0;
            int Contador = 0;

            bool Estado = true;

            string[] Resultado = new string[2];

            while(Estado)
            {
                int NumeroSimbolo = (int)Cadena[Contador];

                if (NumeroSimbolo < 32 || NumeroSimbolo > 125)
                {
                    Resultado[0] = "Error";
                    Resultado[1] = "Simbolo no definido";
                    Estado = false;
                }
                else
                {
                    Fila = MatrizTransicion[Fila, NumeroSimbolo - 31].Numero;
                    if (MatrizTransicion[Fila, 1].Contenido == "Acepta" || MatrizTransicion[Fila, 1].Contenido == "Error")
                    {
                        Resultado[0] = MatrizTransicion[Fila, 1].Contenido;
                        Resultado[1] = MatrizTransicion[Fila, 0].Contenido;
                        Estado = false;
                    }
                }

                Contador++;
            }

            return Resultado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|*.txt";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, txtCadena.Text);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "txt files (*.txt)|*.txt";
            opfd.FilterIndex = 1;
            opfd.RestoreDirectory = true;

            if (opfd.ShowDialog() == DialogResult.OK)
            {
                txtCadena.Text = File.ReadAllText(opfd.FileName);
            }
        }

        private void btnGuardarTokens_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|*.txt";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, txtTokens.Text);
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            txtCadena.Focus();
        }

        private void richTextBox2_Enter(object sender, EventArgs e)
        {
            txtTokens.Focus();
        }

        //Magia negra, agrega el numero de linea a la izquierda del richtextbox, toma en cuenta el Wrapping
        public void AgregarNumerosLinea()
        {
            richTextBox1.Text = "";
            int Aux = -1;
            int Aux1 = txtCadena.GetLineFromCharIndex(txtCadena.TextLength) + 1;
            int NumeroLineas = SepararLineas(txtCadena.Text).Length;
            int LineasActuales = 0;
            int temp = 0;

            for (int i = 0; i < NumeroLineas; i++)
            {
                richTextBox1.Text += (i + 1);
                Aux = txtCadena.Text.IndexOf('\n', Aux + 1);
                temp = LineasActuales;
                LineasActuales = txtCadena.GetLineFromCharIndex(Aux) + 1;
                if (i == NumeroLineas - 1)
                    LineasActuales = txtCadena.GetLineFromCharIndex(txtCadena.TextLength) + 1;
                richTextBox1.Text += new string('\n', LineasActuales - temp);
            }
        }
        
        private void txtCadena_TextChanged(object sender, EventArgs e)
        {
            AgregarNumerosLinea();
        }
    }
}