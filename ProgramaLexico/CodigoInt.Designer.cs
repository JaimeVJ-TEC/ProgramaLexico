
namespace ProgramaLexico
{
    partial class CodigoInt
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtEnsamblado = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgTripletas = new System.Windows.Forms.DataGridView();
            this.btnCompilar = new System.Windows.Forms.Button();
            this.Tripleta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arg1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arg2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTripletas)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(694, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 18);
            this.label5.TabIndex = 23;
            this.label5.Text = "Codigo Ensamblador";
            // 
            // txtEnsamblado
            // 
            this.txtEnsamblado.BackColor = System.Drawing.SystemColors.Window;
            this.txtEnsamblado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEnsamblado.DetectUrls = false;
            this.txtEnsamblado.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnsamblado.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txtEnsamblado.Location = new System.Drawing.Point(697, 33);
            this.txtEnsamblado.Name = "txtEnsamblado";
            this.txtEnsamblado.ReadOnly = true;
            this.txtEnsamblado.ShowSelectionMargin = true;
            this.txtEnsamblado.Size = new System.Drawing.Size(318, 405);
            this.txtEnsamblado.TabIndex = 22;
            this.txtEnsamblado.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "Archivo de Tokens PostFijo";
            // 
            // txtTokens
            // 
            this.txtTokens.BackColor = System.Drawing.SystemColors.Window;
            this.txtTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTokens.DetectUrls = false;
            this.txtTokens.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTokens.ForeColor = System.Drawing.SystemColors.MenuText;
            this.txtTokens.Location = new System.Drawing.Point(12, 33);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.ReadOnly = true;
            this.txtTokens.ShowSelectionMargin = true;
            this.txtTokens.Size = new System.Drawing.Size(316, 405);
            this.txtTokens.TabIndex = 20;
            this.txtTokens.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(343, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "Tripletas";
            // 
            // dtgTripletas
            // 
            this.dtgTripletas.AllowUserToAddRows = false;
            this.dtgTripletas.AllowUserToDeleteRows = false;
            this.dtgTripletas.AllowUserToResizeColumns = false;
            this.dtgTripletas.AllowUserToResizeRows = false;
            this.dtgTripletas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgTripletas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgTripletas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTripletas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tripleta,
            this.Num,
            this.Arg1,
            this.Arg2,
            this.Operador});
            this.dtgTripletas.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtgTripletas.Location = new System.Drawing.Point(346, 33);
            this.dtgTripletas.Name = "dtgTripletas";
            this.dtgTripletas.RowHeadersVisible = false;
            this.dtgTripletas.Size = new System.Drawing.Size(331, 410);
            this.dtgTripletas.TabIndex = 24;
            // 
            // btnCompilar
            // 
            this.btnCompilar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompilar.Location = new System.Drawing.Point(697, 459);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(130, 39);
            this.btnCompilar.TabIndex = 31;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // Tripleta
            // 
            this.Tripleta.HeaderText = "Tripleta";
            this.Tripleta.Name = "Tripleta";
            // 
            // Num
            // 
            this.Num.HeaderText = "Num";
            this.Num.Name = "Num";
            // 
            // Arg1
            // 
            this.Arg1.HeaderText = "Arg1";
            this.Arg1.Name = "Arg1";
            // 
            // Arg2
            // 
            this.Arg2.HeaderText = "Arg2";
            this.Arg2.Name = "Arg2";
            // 
            // Operador
            // 
            this.Operador.HeaderText = "Operador";
            this.Operador.Name = "Operador";
            // 
            // CodigoInt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1027, 510);
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtgTripletas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEnsamblado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTokens);
            this.Name = "CodigoInt";
            this.Text = "CodioInt";
            this.Load += new System.EventHandler(this.CodigoInt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTripletas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtEnsamblado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dtgTripletas;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tripleta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arg1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arg2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operador;
    }
}