using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Trabalho_Banco_De_Dados
{
    public partial class login : Form
    {
        // referencia a conexão

        public login()
        {
            InitializeComponent();
            txtUsuario.Select();


            txtUsuario.KeyDown += new KeyEventHandler(btnLogin_KeyDown);
            txtSenha.KeyDown += new KeyEventHandler(btnLogin_KeyDown);

            
            this.KeyPreview = true;
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {


                    cn.Open();
                    string query = "SELECT * FROM tb_user WHERE usuario = @usuario AND senha = @senha";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    command.Parameters.AddWithValue("@senha", txtSenha.Text);

                    SqlDataAdapter dp = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    dp.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        frm_menu menu = new frm_menu();
                        this.Hide();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = "";
                        txtSenha.Text = "";
                        txtUsuario.Select();
                    }
                    cn.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro durante a execução da consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginCriarConta criarConta = new loginCriarConta();
            criarConta.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtSenha.PasswordChar = '\0';
            }
            else
            {
                txtSenha.PasswordChar = '*';
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Dispara o clique do botão de login
                btnLogin.PerformClick();
                // Impede o som padrão do Windows ao pressionar Enter
                e.SuppressKeyPress = true;
            }

        }
    }
}
