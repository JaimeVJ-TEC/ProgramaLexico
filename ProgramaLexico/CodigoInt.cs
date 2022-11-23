using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramaLexico
{
    public partial class CodigoInt : Form
    {
        CodigoIntermedio GeneradorDeCodigoInt;
        AnalizadorLexico Lexico;
        public CodigoInt(AnalizadorLexico AnalisisLexico)
        {
            InitializeComponent();
            Lexico = AnalisisLexico;
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            TraductorEnsamblador ensamblador = new TraductorEnsamblador(GeneradorDeCodigoInt.Tripletas, Lexico.TablaSimbolos);
            txtEnsamblado.Text = ensamblador.ArchivoASM;
        }

        private void CodigoInt_Load(object sender, EventArgs e)
        {
            dtgTripletas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.BackColor = Color.FromArgb(45, 45, 48);

            GeneradorDeCodigoInt = new CodigoIntermedio(Lexico.ArchivoTokensNumero, Lexico.TablaSimbolos);
            string TextoTokens = "";
            foreach (string[] array in GeneradorDeCodigoInt.ArchivoTokensPostfijo)
            {
                foreach (string s in array)
                {
                    TextoTokens += " " + s;
                }
                TextoTokens += "\n";
            }
            txtTokens.Text = TextoTokens;

            for (int i = GeneradorDeCodigoInt.Tripletas.Count - 1; i > 0; i--)
            {
                int num = 0;
                foreach (Renglon r in GeneradorDeCodigoInt.Tripletas[i].Renglones)
                {
                    dtgTripletas.Rows.Add(GeneradorDeCodigoInt.Tripletas[i].Nombre, num, r.Argumento1, r.Argumento2, r.Operador);
                    num++;
                }
            }
        }
    }
}
