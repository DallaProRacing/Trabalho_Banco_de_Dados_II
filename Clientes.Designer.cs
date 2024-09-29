namespace Trabalho_Banco_De_Dados
{
    partial class Clientes
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clientes));
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.btnBuscarC = new System.Windows.Forms.Button();
            this.btnRecarregarC = new System.Windows.Forms.Button();
            this.cbxBuscarC = new System.Windows.Forms.ComboBox();
            this.btnAlterarC = new System.Windows.Forms.Button();
            this.btnAdicionarC = new System.Windows.Forms.Button();
            this.txtbuscarC = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnExcluir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.toolStripContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(195, 41);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(559, 340);
            this.dataGridView2.TabIndex = 16;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(-3, 260);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 15;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // btnBuscarC
            // 
            this.btnBuscarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnBuscarC.Location = new System.Drawing.Point(475, 12);
            this.btnBuscarC.Name = "btnBuscarC";
            this.btnBuscarC.Size = new System.Drawing.Size(76, 23);
            this.btnBuscarC.TabIndex = 14;
            this.btnBuscarC.Text = "Buscar";
            this.btnBuscarC.UseVisualStyleBackColor = true;
            this.btnBuscarC.Click += new System.EventHandler(this.btnBuscarC_Click);
            // 
            // btnRecarregarC
            // 
            this.btnRecarregarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRecarregarC.Location = new System.Drawing.Point(679, 12);
            this.btnRecarregarC.Name = "btnRecarregarC";
            this.btnRecarregarC.Size = new System.Drawing.Size(75, 23);
            this.btnRecarregarC.TabIndex = 24;
            this.btnRecarregarC.Text = "Recarregar";
            this.btnRecarregarC.UseVisualStyleBackColor = true;
            this.btnRecarregarC.Click += new System.EventHandler(this.btnRecarregarC_Click);
            // 
            // cbxBuscarC
            // 
            this.cbxBuscarC.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cbxBuscarC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBuscarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxBuscarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbxBuscarC.FormattingEnabled = true;
            this.cbxBuscarC.Items.AddRange(new object[] {
            "NomeCli",
            "CPF",
            "Altura",
            "Contato"});
            this.cbxBuscarC.Location = new System.Drawing.Point(277, 12);
            this.cbxBuscarC.Name = "cbxBuscarC";
            this.cbxBuscarC.Size = new System.Drawing.Size(90, 23);
            this.cbxBuscarC.TabIndex = 21;
            // 
            // btnAlterarC
            // 
            this.btnAlterarC.BackColor = System.Drawing.Color.Yellow;
            this.btnAlterarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAlterarC.Location = new System.Drawing.Point(608, 383);
            this.btnAlterarC.Name = "btnAlterarC";
            this.btnAlterarC.Size = new System.Drawing.Size(75, 23);
            this.btnAlterarC.TabIndex = 20;
            this.btnAlterarC.Text = "Alterar";
            this.btnAlterarC.UseVisualStyleBackColor = false;
            this.btnAlterarC.Click += new System.EventHandler(this.btnAlterarC_Click);
            // 
            // btnAdicionarC
            // 
            this.btnAdicionarC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAdicionarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAdicionarC.Location = new System.Drawing.Point(689, 383);
            this.btnAdicionarC.Name = "btnAdicionarC";
            this.btnAdicionarC.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionarC.TabIndex = 19;
            this.btnAdicionarC.Text = "Adicionar";
            this.btnAdicionarC.UseVisualStyleBackColor = false;
            this.btnAdicionarC.Click += new System.EventHandler(this.btnAdicionarC_Click);
            // 
            // txtbuscarC
            // 
            this.txtbuscarC.AccessibleDescription = "";
            this.txtbuscarC.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtbuscarC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbuscarC.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtbuscarC.Location = new System.Drawing.Point(373, 12);
            this.txtbuscarC.Name = "txtbuscarC";
            this.txtbuscarC.Size = new System.Drawing.Size(96, 23);
            this.txtbuscarC.TabIndex = 18;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBox1.Location = new System.Drawing.Point(3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(954, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Clientes";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(0, 410);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 25);
            this.panel2.TabIndex = 90;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-233, -139);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1199, 574);
            this.pictureBox2.TabIndex = 91;
            this.pictureBox2.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBox2.Location = new System.Drawing.Point(195, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(559, 23);
            this.textBox2.TabIndex = 22;
            this.textBox2.Text = "     Filtrar por:";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Red;
            this.btnExcluir.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnExcluir.Location = new System.Drawing.Point(527, 383);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 92;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // Clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 435);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.btnBuscarC);
            this.Controls.Add(this.btnRecarregarC);
            this.Controls.Add(this.cbxBuscarC);
            this.Controls.Add(this.btnAlterarC);
            this.Controls.Add(this.btnAdicionarC);
            this.Controls.Add(this.txtbuscarC);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.Clientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button btnBuscarC;
        private System.Windows.Forms.Button btnRecarregarC;
        private System.Windows.Forms.ComboBox cbxBuscarC;
        private System.Windows.Forms.Button btnAlterarC;
        private System.Windows.Forms.Button btnAdicionarC;
        private System.Windows.Forms.TextBox txtbuscarC;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnExcluir;
    }
}