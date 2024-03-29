﻿using System;
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

        public List<List<string>> ListaINS = new List<List<string>>();

        public List<string>[] Tabla = new List<string>[2];

        public Stack<string> TokenStack = new Stack<string>();

        public string cadenaTokenTest = "begin { TIPO ID OPAS ( ID + ( CN + CN ) ) + CN } end";

        //public string cadenaTokenTest = "begin { INSTR } end";

        public Form1()
        {
            InitializeComponent();

            MatrizTransicion = LlenarMatriz();

            //#INSTRUCCIONES PARA EL ANALIZADOR SINTACTICO
            //ListaINS.Add(new List<string> { "IN", "begin { INSTR } end" });
            //ListaINS.Add(new List<string> { "INSTR", "INID_2", "INSTR INSTR"});
            //ListaINS.Add(new List<string> { "INID","TIPO ID"});
            //ListaINS.Add(new List<string> { "INID_2", "TIPO INAS" });
            //ListaINS.Add(new List<string> { "INAS", "ID OPAS ID", "ID OPAS PR24", "ID OPAS PR23", "ID OPAS CN", "ID OPAS INAR", "ID OPAS COND" });
            //ListaINS.Add(new List<string> { "EXPRESION", "ID", "CADENA", "PR24", "PR23", "CN", "INAR","COND" });
            //ListaINS.Add(new List<string> { "INAR", "ID + ID", "ID + ( INAR )", "ID + CN", "CN + ID", "CN + CN", "CN + ( INAR )", "( INAR ) + ID", "( INAR ) + CN", "( INAR ) + ( INAR )" });
            //ListaINS.Add(new List<string> { "OPERANDO", "ID", "CN", "( INAR )" });
            //AnalizadorSintactico();
        }

        //METODO PRINCIPAL DEL ANALIZADOR SINTACTICO
        public void AnalizadorSintactico()
        {
            MessageBox.Show(cadenaTokenTest);
            string strTokenStack = "";
            string[] TokenBuffer = cadenaTokenTest.Split(' ');
            int posicion = 0;

            while(true)
            {
                strTokenStack += TokenBuffer[posicion] + " ";
                string[] AuxArray = strTokenStack.Split(' ');
                string AuxCadena = "";

                AuxArray = AuxArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                AuxArray = AuxArray.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                for (int i = 0; i < AuxArray.Length; i++)
                {
                    AuxCadena = AuxCadena.Insert(0, AuxArray[(AuxArray.Length - 1) - i]);
                    string Ins = BuscarInstruccion(AuxCadena);

                    if (Ins != "")
                    {
                        MessageBox.Show(strTokenStack);

                        strTokenStack = strTokenStack.Replace(AuxCadena, Ins);
                        AuxCadena = Ins;
                        Ins = BuscarInstruccion(AuxCadena);

                        MessageBox.Show(strTokenStack);
                        if (Ins != "")
                        {
                            strTokenStack = strTokenStack.Replace(AuxCadena, Ins);
                            AuxCadena = Ins;

                            MessageBox.Show(strTokenStack);
                        }
                    }
                    AuxCadena = AuxCadena.Insert(0, " ");
                }

                posicion++;

                if (strTokenStack == "IN ")
                    break;
            }
        }

        //METODO AUXILIAR DEL ANALIZADOR SINTACTICO
        public string BuscarInstruccion(string Cadena)
        {
            bool Encontrada = false;
            string InsID = "";

            foreach(List<string> ins in ListaINS)
            {
                for (int i = 1; i < ins.Count; i++)
                {
                    if (Cadena == ins[i])
                    {
                        Encontrada = true;
                        InsID = ins[0];
                        break;
                    }
                }
                if (Encontrada)
                    break;
            }
            return InsID;
        }

        //public bool VerificarInstruccion(string INS, string Tokens)
        //{
        //    bool Coincide = false;
        //    foreach(List<string> ls in ListaINS)
        //    {
        //        if(INS == ls[0])
        //        {
        //            foreach(string s in ls)
        //            {
        //                if (Tokens == s)
        //                    Coincide = true;
        //            }
        //        }
        //    }
        //    return Coincide;
        //}

        //public void AnalizadorSintactico(string Cadena)
        //{
        //    Stack<string> TokenStack = new Stack<string>();
        //    string[] TokenBuffer = Cadena.Split(' ');
        //    string LookAhead = "";
        //    int Posicion = 0;
        //    bool Fin = false;

        //    while (!Fin)
        //    {
        //        TokenStack.Push(TokenBuffer[Posicion]);
        //        if (Posicion + 1 < TokenBuffer.Length)
        //            LookAhead = TokenBuffer[Posicion + 1];

        //        foreach (List<string> ins in ListaINS)
        //        {
        //            bool found = false;
        //            for (int i = 1; i < ins.Count; i++)
        //            {
        //                string[] AuxArray = ins[i].Split(' ');
        //                string AuxString = "";
        //                if (AuxArray.Length != 1)
        //                    AuxString = AuxArray[0] + " " + AuxArray[1];

        //                if (TokenStack.Peek() == ins[i] && TokenStack.Peek() + " " + LookAhead != AuxString)
        //                {
        //                    TokenStack.Pop();
        //                    TokenStack.Push(ins[0]);
        //                    Posicion++;
        //                    found = true;
        //                }
        //                else if (TokenStack.Peek() + " " + LookAhead == AuxString)
        //                {
        //                    Posicion++;
        //                    TokenStack.Push(LookAhead);
        //                }
        //            }
        //            if (!found)
        //            {
        //                Posicion++;
        //                TokenStack.Push(LookAhead);
        //            }
        //        }
        //    }
        //}

        //public void AnalizadorSintactico()
        //{
        //    int Posicion = 0;
        //    List<string> Tokens = cadenaTokenTest.Split(' ').ToList();
        //    int CantTokens = Tokens.Count;

        //    foreach (List<string> ins in ListaINS)
        //    {
        //        for (int i = 1; i < ins.Count; i++)
        //        {
        //            string[] ArregloIns = ins[i].Split(' ');
        //            int ContAux = ArregloIns.Length;
        //            string TokensAux = "";

        //            for (int j = Posicion; j < Posicion + ContAux; j++)
        //            {
        //                TokensAux += Tokens[j] + " ";
        //            }
        //            TokensAux = TokensAux.Trim();

        //            if (TokensAux == ins[i])
        //            {
        //                for (int j = Posicion; j < Posicion + ContAux; j++)
        //                {
        //                    Tokens.RemoveAt(j);
        //                }
        //                Tokens.Insert(Posicion, ins[0]);
        //            }
        //        }
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgErrores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgIdentificadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            richTextBox1.BackColor = SystemColors.InactiveCaption;
            richTextBox1.ForeColor = Color.Black;

            richTextBox1.Font = txtCadena.Font;
            txtCadena.Select();
            AgregarNumerosLinea();

        }


        private void btnEvaluar_Click(object sender, EventArgs e)
        {
            EvaluarTexto();
            LlenarTokens();
            LlenarErrores();
            MarcarErrores();
            LlenarID();
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
                string cadena = e.Cadena.Trim();
                Aux = txtCadena.Text.IndexOf(cadena);
                txtCadena.SelectionStart = Aux;
                txtCadena.SelectionLength = cadena.Length;

                if(txtCadena.SelectionColor == Color.Red)
                {
                    bool Marcado = true;
                    while (Marcado)
                    {
                        Aux = txtCadena.Text.IndexOf(cadena, Aux + cadena.Length);
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

        public void LlenarID()
        {
            dtgIdentificadores.Rows.Clear();
            foreach(Identificador id in TablaSimbolos)
            {
                dtgIdentificadores.Rows.Add(id.Numero, id.Descripcion, id.TipoDato, id.Valor);
            }
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
            catch(Exception ex)
            {
                MessageBox.Show("Problema con la conexion a la base de datos","Error");
            }
            finally
            {
                cnn.Close();
            }

            Celda[,] Matriz = new Celda[dataTable.Rows.Count,dataTable.Columns.Count];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
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

        //Metodo nuevo que separa texto de entrada en "palabras"
        public string[] SepararCadenas(string Texto)
        {
            Texto = Texto.Replace("\n", " ");
            Texto = Texto.Trim();
            Texto += " ";

            List<string> ListaTemp = new List<string>();

            if (Texto != "" && Texto != " ")
            {
                if (Texto.Substring(0, 2) == "//")
                {
                    int FinCadena = Texto.Length;
                    ListaTemp.Add(Texto.Substring(0, FinCadena));
                }
                else if (Texto.Substring(0, 2) == "/*")
                {
                    int Cierre = Texto.IndexOf("*/", 2);
                    if (Cierre == -1)
                    {
                        Texto = Texto.Trim();
                        Texto += "*/ ";
                        ListaTemp.Add(Texto);
                    }
                    else
                    {
                        Cierre = Texto.IndexOf(" ", Cierre + 2);
                        ListaTemp.Add(Texto.Substring(0, Cierre+1));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(Cierre, Texto.Length - Cierre)));
                    }
                }
                else if (Texto[0] == '\"')
                {
                    int Cierre = Texto.IndexOf('\"', 1);

                    if (Texto.IndexOf(" ", 1) == 1)
                    {
                        ListaTemp.Add(Texto.Substring(0, 2));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(2, Texto.Length - 2)));
                    }
                    else if (Cierre == -1)
                    {
                        Texto = Texto.Trim();
                        Texto += "\" ";
                        ListaTemp.Add(Texto);
                    }
                    else
                    {
                        Cierre = Texto.IndexOf(" ", Cierre + 1);
                        ListaTemp.Add(Texto.Substring(0, Cierre + 1));
                        ListaTemp.AddRange(SepararCadenas(Texto.Substring(Cierre, Texto.Length - Cierre)));
                    }
                }
                else
                {
                    int FinCadena = Texto.IndexOf(" ") + 1;
                    ListaTemp.Add(Texto.Substring(0, FinCadena));
                    ListaTemp.AddRange(SepararCadenas(Texto.Substring(FinCadena, Texto.Length - FinCadena)));
                }
            }
            return ListaTemp.ToArray();
        }

        //Metodo Viejo
        public string[] SepararCadenasS(string Texto)
        {
            Texto = Texto.Replace("\n", " ");
            Texto = Texto.Trim();
            Texto += " ";

            List<string> ListaTemp = new List<string>();
            string[] Cadenas = Regex.Split(Texto, @"(?<=[ ])");

            //IFs necesarios para separar correctamente los comentarios y cadenas
            if (Texto.IndexOf("//") != -1)
            {
                int InicioCom = Texto.IndexOf("//");
                string NoCom = Texto.Substring(0, InicioCom);
                string Comentario = Texto.Substring(InicioCom, Texto.Length - InicioCom);

                ListaTemp.AddRange(SepararCadenas(NoCom));
                ListaTemp.Add(Comentario.Replace(" ", " "));
                Cadenas = ListaTemp.ToArray();
            }
            else if (Texto.IndexOf("/*") != -1)
            {
                int InicioCom = Texto.IndexOf("/*");
                int FinCom = Texto.IndexOf("*/", InicioCom + 1);
                string NoCadena = "";
                if (FinCom != -1)
                    NoCadena = Texto.Substring(0, InicioCom);
                string Cadena = "";
                string CadenaDer = "";
                ListaTemp.AddRange(SepararCadenas(NoCadena));

                if (FinCom == -1)
                {
                    Cadena = Texto;
                    Cadena = Cadena.Trim();
                    Cadena += " ";
                    ListaTemp.AddRange(Regex.Split(Cadena, @"(?<=[ ])"));
                }
                else
                {
                    int SigEspacio = Texto.IndexOf(" ", FinCom + 1);
                    Cadena = Texto.Substring(InicioCom, SigEspacio + 1 - InicioCom);
                    ListaTemp.Add(Cadena);
                    CadenaDer = Texto.Substring(SigEspacio, Texto.Length - SigEspacio);
                    ListaTemp.AddRange(SepararCadenas(CadenaDer));
                }

                Cadenas = ListaTemp.ToArray();
            }
            else if (Texto.IndexOf("\"") != -1)
            {
                int InicioCom = Texto.IndexOf("\"");
                int FinCom = Texto.IndexOf("\"", InicioCom + 1);
                string NoCadena = "";
                if(FinCom != -1)
                    NoCadena = Texto.Substring(0, InicioCom);
                string Cadena = "";
                string CadenaDer = "";
                ListaTemp.AddRange(SepararCadenas(NoCadena));

                if (FinCom== -1)
                {
                    //Cadena = Texto.Substring(InicioCom, Texto.Length-1);
                    //Cadena = Cadena.Insert(InicioCom + 1, " ");
                    //Cadena = Cadena.Insert(InicioCom, " ");
                    Cadena = Texto;
                    Cadena = Cadena.Trim();
                    Cadena += " ";
                    ListaTemp.AddRange(Regex.Split(Cadena, @"(?<=[ ])"));
                }
                else
                {
                    int SigEspacio = Texto.IndexOf(" ", FinCom + 1);
                    Cadena = Texto.Substring(InicioCom, SigEspacio+1 - InicioCom);
                    ListaTemp.Add(Cadena);
                    CadenaDer = Texto.Substring(SigEspacio, Texto.Length - SigEspacio);
                    string[] TextoDer = SepararCadenas(CadenaDer);
                    ListaTemp.AddRange(SepararCadenas(CadenaDer));
                }

                Cadenas = ListaTemp.ToArray();
            }

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

                if (NumeroSimbolo < 32 || NumeroSimbolo > 126)
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
            Point pt = new Point(0, 0);

            int First_Index = txtCadena.GetCharIndexFromPosition(pt);
            int First_Line = txtCadena.GetLineFromCharIndex(First_Index);

            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            int Last_Index = txtCadena.GetCharIndexFromPosition(pt);
            int Last_Line = txtCadena.GetLineFromCharIndex(Last_Index);

            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

            richTextBox1.Text = "";
            richTextBox1.Width = getWidth();

            for (int i = First_Line; i <= Last_Line; i++)
            {
                richTextBox1.Text += i + 1 + "\n";
            }
        }

        public int getWidth()
        {
            int w = 25;
            int line = txtCadena.Lines.Length;

            if(line <= 99)
            {
                w = 20 + (int)txtCadena.Font.Size;
            }   
            else if(line <= 999)
            {
                w = 30 + (int)txtCadena.Font.Size;
            }
            else
            {
                w = 50 + (int)txtCadena.Font.Size;
            }

            return w;
        }
        
        private void txtCadena_TextChanged(object sender, EventArgs e)
        {
            AgregarNumerosLinea();
        }

        private void txtCadena_VScroll(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            AgregarNumerosLinea();
            richTextBox1.Invalidate();
        }

        private void txtCadena_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = txtCadena.GetPositionFromCharIndex(txtCadena.SelectionStart);
            if(pt.X == 1)
            {
                AgregarNumerosLinea();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }
    }
}