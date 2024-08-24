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

namespace Trabalho_Banco_De_Dados
{
    public partial class loginCriarConta : Form
    {
        int id = 0;
        public loginCriarConta()
        {
            InitializeComponent();
        }

        private bool CamposObrigatoriosPreenchidos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuarioCadastro.Text) ||
                string.IsNullOrWhiteSpace(txtSenhaCadastro.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void SalvarUsuario()
        {
            if (CamposObrigatoriosPreenchidos())
            {


                try
                {
                    using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                    {
                        cn.Open();
                        var sql = "";

                        sql = "INSERT INTO tb_user(usuario,senha) VALUES (@usuario,@senha)";


                        using (SqlCommand cmd = new SqlCommand(sql, cn))
                        {
                            cmd.Parameters.AddWithValue("@usuario", txtUsuarioCadastro.Text);
                            cmd.Parameters.AddWithValue("@senha", txtSenhaCadastro.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível salvar os dados!\n\n" + ex.Message);
                }
            }
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }

        private void btmCadastrarU_Click(object sender, EventArgs e)
        {
            SalvarUsuario();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtSenhaCadastro.PasswordChar = '\0';
            }
            else
            {
                txtSenhaCadastro.PasswordChar = '*';
            }
        }

        private void loginCriarConta_Load(object sender, EventArgs e)
        {

        }
    }
}
