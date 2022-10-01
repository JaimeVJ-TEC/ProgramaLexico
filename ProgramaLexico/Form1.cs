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
        AnalizadorLexico AnalisisLexico = new AnalizadorLexico();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgErrores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgIdentificadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            richTextBox1.BackColor = SystemColors.InactiveCaption;
            richTextBox1.ForeColor = Color.Black;

            richTextBox1.Font = txtCadena.Font;
            richTextBox1.WordWrap = false;
            txtCadena.WordWrap = false;
            txtCadena.Select();
            AgregarNumerosLinea();

        }

        private void btnEvaluar_Click(object sender, EventArgs e)
        {
            AnalisisLexico.Analizar(txtCadena.Text);
            LlenarTokens();
            LlenarErrores();
            MarcarErrores();
            LlenarID();
        }

        public void LlenarTokens()
        {
            txtTokens.Text = "";
            string TextoTokens = "";

            foreach(string[] array in AnalisisLexico.ArchivoTokens)
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
            foreach(Error e in AnalisisLexico.Errores)
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

            foreach (Error e in AnalisisLexico.Errores)
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
            foreach(Identificador id in AnalisisLexico.TablaSimbolos)
            {
                dtgIdentificadores.Rows.Add(id.Numero, id.Descripcion, id.TipoDato, id.Valor);
            }
        }

        #region Cargar Y Guardar Archivos
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
        #endregion

        #region GUI stuff

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

        #endregion
    }
}