
namespace ProgramaLexico
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnEvaluar = new System.Windows.Forms.Button();
            this.txtCadena = new System.Windows.Forms.RichTextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.btnGuardarTokens = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgErrores = new System.Windows.Forms.DataGridView();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dtgIdentificadores = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSintax = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSintaxis = new System.Windows.Forms.Button();
            this.btnPasoPorPaso = new System.Windows.Forms.Button();
            this.btnLimpiarSintax = new System.Windows.Forms.Button();
            this.txtSem = new System.Windows.Forms.RichTextBox();
            this.btnSem = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSemPaso = new System.Windows.Forms.Button();
            this.btnLimpiarSem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgIdentificadores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEvaluar
            // 
            this.btnEvaluar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEvaluar.Location = new System.Drawing.Point(296, 450);
            this.btnEvaluar.Name = "btnEvaluar";
            this.btnEvaluar.Size = new System.Drawing.Size(80, 39);
            this.btnEvaluar.TabIndex = 3;
            this.btnEvaluar.Text = "Evaluar";
            this.btnEvaluar.UseVisualStyleBackColor = true;
            this.btnEvaluar.Click += new System.EventHandler(this.btnEvaluar_Click);
            // 
            // txtCadena
            // 
            this.txtCadena.AcceptsTab = true;
            this.txtCadena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCadena.DetectUrls = false;
            this.txtCadena.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadena.ForeColor = System.Drawing.SystemColors.Window;
            this.txtCadena.Location = new System.Drawing.Point(36, 39);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.ShowSelectionMargin = true;
            this.txtCadena.Size = new System.Drawing.Size(340, 405);
            this.txtCadena.TabIndex = 2;
            this.txtCadena.Text = "";
            this.txtCadena.WordWrap = false;
            this.txtCadena.SelectionChanged += new System.EventHandler(this.txtCadena_SelectionChanged);
            this.txtCadena.VScroll += new System.EventHandler(this.txtCadena_VScroll);
            this.txtCadena.TextChanged += new System.EventHandler(this.txtCadena_TextChanged);
            // 
            // btnCargar
            // 
            this.btnCargar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Location = new System.Drawing.Point(3, 450);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(86, 39);
            this.btnCargar.TabIndex = 4;
            this.btnCargar.Text = "Cargar Programa";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(150, 450);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(86, 39);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar Programa";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtTokens
            // 
            this.txtTokens.BackColor = System.Drawing.SystemColors.Window;
            this.txtTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTokens.DetectUrls = false;
            this.txtTokens.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTokens.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTokens.Location = new System.Drawing.Point(392, 39);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.ReadOnly = true;
            this.txtTokens.ShowSelectionMargin = true;
            this.txtTokens.Size = new System.Drawing.Size(312, 405);
            this.txtTokens.TabIndex = 7;
            this.txtTokens.Text = "";
            // 
            // btnGuardarTokens
            // 
            this.btnGuardarTokens.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarTokens.Location = new System.Drawing.Point(512, 450);
            this.btnGuardarTokens.Name = "btnGuardarTokens";
            this.btnGuardarTokens.Size = new System.Drawing.Size(86, 39);
            this.btnGuardarTokens.TabIndex = 8;
            this.btnGuardarTokens.Text = "Guardar Archivo";
            this.btnGuardarTokens.UseVisualStyleBackColor = true;
            this.btnGuardarTokens.Click += new System.EventHandler(this.btnGuardarTokens_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Programa fuente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(389, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Archivo de tokens";
            // 
            // dtgErrores
            // 
            this.dtgErrores.AllowUserToAddRows = false;
            this.dtgErrores.AllowUserToDeleteRows = false;
            this.dtgErrores.AllowUserToResizeColumns = false;
            this.dtgErrores.AllowUserToResizeRows = false;
            this.dtgErrores.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgErrores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Linea,
            this.Error});
            this.dtgErrores.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtgErrores.Location = new System.Drawing.Point(496, 519);
            this.dtgErrores.Name = "dtgErrores";
            this.dtgErrores.RowHeadersVisible = false;
            this.dtgErrores.Size = new System.Drawing.Size(525, 144);
            this.dtgErrores.TabIndex = 13;
            // 
            // Linea
            // 
            this.Linea.HeaderText = "Linea";
            this.Linea.Name = "Linea";
            // 
            // Error
            // 
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(2, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(34, 405);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            this.richTextBox1.Enter += new System.EventHandler(this.richTextBox1_Enter);
            // 
            // dtgIdentificadores
            // 
            this.dtgIdentificadores.AllowUserToAddRows = false;
            this.dtgIdentificadores.AllowUserToDeleteRows = false;
            this.dtgIdentificadores.AllowUserToResizeColumns = false;
            this.dtgIdentificadores.AllowUserToResizeRows = false;
            this.dtgIdentificadores.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgIdentificadores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgIdentificadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgIdentificadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.Nombre,
            this.TD,
            this.Valor});
            this.dtgIdentificadores.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtgIdentificadores.Location = new System.Drawing.Point(12, 519);
            this.dtgIdentificadores.Name = "dtgIdentificadores";
            this.dtgIdentificadores.RowHeadersVisible = false;
            this.dtgIdentificadores.Size = new System.Drawing.Size(463, 144);
            this.dtgIdentificadores.TabIndex = 15;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // TD
            // 
            this.TD.HeaderText = "TD";
            this.TD.Name = "TD";
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 500);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tabla de simbolos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(493, 500);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Errores";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(717, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 19;
            this.label5.Text = "Analisis Sintactico";
            // 
            // txtSintax
            // 
            this.txtSintax.BackColor = System.Drawing.SystemColors.Window;
            this.txtSintax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSintax.DetectUrls = false;
            this.txtSintax.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSintax.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSintax.Location = new System.Drawing.Point(720, 40);
            this.txtSintax.Name = "txtSintax";
            this.txtSintax.ReadOnly = true;
            this.txtSintax.ShowSelectionMargin = true;
            this.txtSintax.Size = new System.Drawing.Size(301, 360);
            this.txtSintax.TabIndex = 18;
            this.txtSintax.Text = "";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(392, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 39);
            this.button1.TabIndex = 20;
            this.button1.Text = "Cargar Archivo de Tokens";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSintaxis
            // 
            this.btnSintaxis.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSintaxis.Location = new System.Drawing.Point(618, 450);
            this.btnSintaxis.Name = "btnSintaxis";
            this.btnSintaxis.Size = new System.Drawing.Size(86, 39);
            this.btnSintaxis.TabIndex = 21;
            this.btnSintaxis.Text = "Analizar Sintaxis";
            this.btnSintaxis.UseVisualStyleBackColor = true;
            this.btnSintaxis.Click += new System.EventHandler(this.btnSintaxis_Click);
            // 
            // btnPasoPorPaso
            // 
            this.btnPasoPorPaso.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasoPorPaso.Location = new System.Drawing.Point(720, 406);
            this.btnPasoPorPaso.Name = "btnPasoPorPaso";
            this.btnPasoPorPaso.Size = new System.Drawing.Size(97, 39);
            this.btnPasoPorPaso.TabIndex = 22;
            this.btnPasoPorPaso.Text = "Sintaxis Paso por Paso";
            this.btnPasoPorPaso.UseVisualStyleBackColor = true;
            this.btnPasoPorPaso.Click += new System.EventHandler(this.btnPasoPorPaso_Click);
            // 
            // btnLimpiarSintax
            // 
            this.btnLimpiarSintax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarSintax.Location = new System.Drawing.Point(924, 406);
            this.btnLimpiarSintax.Name = "btnLimpiarSintax";
            this.btnLimpiarSintax.Size = new System.Drawing.Size(97, 39);
            this.btnLimpiarSintax.TabIndex = 23;
            this.btnLimpiarSintax.Text = "Limpiar";
            this.btnLimpiarSintax.UseVisualStyleBackColor = true;
            this.btnLimpiarSintax.Click += new System.EventHandler(this.btnLimpiarSintax_Click);
            // 
            // txtSem
            // 
            this.txtSem.BackColor = System.Drawing.SystemColors.Window;
            this.txtSem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSem.DetectUrls = false;
            this.txtSem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSem.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSem.Location = new System.Drawing.Point(1039, 39);
            this.txtSem.Name = "txtSem";
            this.txtSem.ReadOnly = true;
            this.txtSem.ShowSelectionMargin = true;
            this.txtSem.Size = new System.Drawing.Size(293, 360);
            this.txtSem.TabIndex = 24;
            this.txtSem.Text = "";
            // 
            // btnSem
            // 
            this.btnSem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSem.Location = new System.Drawing.Point(924, 451);
            this.btnSem.Name = "btnSem";
            this.btnSem.Size = new System.Drawing.Size(97, 39);
            this.btnSem.TabIndex = 25;
            this.btnSem.Text = "Analisis Semantico";
            this.btnSem.UseVisualStyleBackColor = true;
            this.btnSem.Click += new System.EventHandler(this.btnSem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1036, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 18);
            this.label6.TabIndex = 26;
            this.label6.Text = "Analisis Semantico";
            // 
            // btnSemPaso
            // 
            this.btnSemPaso.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSemPaso.Location = new System.Drawing.Point(1039, 405);
            this.btnSemPaso.Name = "btnSemPaso";
            this.btnSemPaso.Size = new System.Drawing.Size(106, 39);
            this.btnSemPaso.TabIndex = 27;
            this.btnSemPaso.Text = "Semantico Paso por Paso";
            this.btnSemPaso.UseVisualStyleBackColor = true;
            this.btnSemPaso.Click += new System.EventHandler(this.btnSemPaso_Click);
            // 
            // btnLimpiarSem
            // 
            this.btnLimpiarSem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarSem.Location = new System.Drawing.Point(1235, 406);
            this.btnLimpiarSem.Name = "btnLimpiarSem";
            this.btnLimpiarSem.Size = new System.Drawing.Size(97, 39);
            this.btnLimpiarSem.TabIndex = 28;
            this.btnLimpiarSem.Text = "Limpiar";
            this.btnLimpiarSem.UseVisualStyleBackColor = true;
            this.btnLimpiarSem.Click += new System.EventHandler(this.btnLimpiarSem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(1344, 675);
            this.Controls.Add(this.btnLimpiarSem);
            this.Controls.Add(this.btnSemPaso);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSem);
            this.Controls.Add(this.txtSem);
            this.Controls.Add(this.btnLimpiarSintax);
            this.Controls.Add(this.btnPasoPorPaso);
            this.Controls.Add(this.btnSintaxis);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSintax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtgIdentificadores);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dtgErrores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarTokens);
            this.Controls.Add(this.txtTokens);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnEvaluar);
            this.Controls.Add(this.txtCadena);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Lexico";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgIdentificadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEvaluar;
        private System.Windows.Forms.RichTextBox txtCadena;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.Button btnGuardarTokens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dtgIdentificadores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn TD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtSintax;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSintaxis;
        private System.Windows.Forms.Button btnPasoPorPaso;
        private System.Windows.Forms.Button btnLimpiarSintax;
        private System.Windows.Forms.RichTextBox txtSem;
        private System.Windows.Forms.Button btnSem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSemPaso;
        private System.Windows.Forms.Button btnLimpiarSem;
    }
}

