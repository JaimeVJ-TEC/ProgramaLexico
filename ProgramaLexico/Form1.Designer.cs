
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
            this.btnEvaluar = new System.Windows.Forms.Button();
            this.txtCadena = new System.Windows.Forms.RichTextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.btnGuardarTokens = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgErrores = new System.Windows.Forms.DataGridView();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dtgIdentificadores = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgIdentificadores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEvaluar
            // 
            this.btnEvaluar.Location = new System.Drawing.Point(335, 38);
            this.btnEvaluar.Name = "btnEvaluar";
            this.btnEvaluar.Size = new System.Drawing.Size(69, 33);
            this.btnEvaluar.TabIndex = 3;
            this.btnEvaluar.Text = "Evaluar";
            this.btnEvaluar.UseVisualStyleBackColor = true;
            this.btnEvaluar.Click += new System.EventHandler(this.btnEvaluar_Click);
            // 
            // txtCadena
            // 
            this.txtCadena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCadena.DetectUrls = false;
            this.txtCadena.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCadena.Location = new System.Drawing.Point(47, 38);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.ShowSelectionMargin = true;
            this.txtCadena.Size = new System.Drawing.Size(278, 405);
            this.txtCadena.TabIndex = 2;
            this.txtCadena.Text = "";
            this.txtCadena.SelectionChanged += new System.EventHandler(this.txtCadena_SelectionChanged);
            this.txtCadena.VScroll += new System.EventHandler(this.txtCadena_VScroll);
            this.txtCadena.TextChanged += new System.EventHandler(this.txtCadena_TextChanged);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(36, 449);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(86, 39);
            this.btnCargar.TabIndex = 4;
            this.btnCargar.Text = "Cargar Programa";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(124, 449);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 39);
            this.btnEditar.TabIndex = 5;
            this.btnEditar.Text = "Editar Programa";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(212, 449);
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
            this.txtTokens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTokens.DetectUrls = false;
            this.txtTokens.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTokens.Location = new System.Drawing.Point(420, 38);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.ReadOnly = true;
            this.txtTokens.ShowSelectionMargin = true;
            this.txtTokens.Size = new System.Drawing.Size(312, 246);
            this.txtTokens.TabIndex = 7;
            this.txtTokens.Text = "";
            // 
            // btnGuardarTokens
            // 
            this.btnGuardarTokens.Location = new System.Drawing.Point(420, 290);
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
            this.label1.Location = new System.Drawing.Point(35, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Programa fuente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Archivo de tokens";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Errores";
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
            this.dtgErrores.Location = new System.Drawing.Point(760, 39);
            this.dtgErrores.Name = "dtgErrores";
            this.dtgErrores.RowHeadersVisible = false;
            this.dtgErrores.Size = new System.Drawing.Size(257, 245);
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
            this.richTextBox1.Location = new System.Drawing.Point(13, 38);
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
            this.dtgIdentificadores.Location = new System.Drawing.Point(420, 351);
            this.dtgIdentificadores.Name = "dtgIdentificadores";
            this.dtgIdentificadores.RowHeadersVisible = false;
            this.dtgIdentificadores.Size = new System.Drawing.Size(501, 137);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 514);
            this.Controls.Add(this.dtgIdentificadores);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dtgErrores);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarTokens);
            this.Controls.Add(this.txtTokens);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnEvaluar);
            this.Controls.Add(this.txtCadena);
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
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.Button btnGuardarTokens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtgErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dtgIdentificadores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn TD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
    }
}

