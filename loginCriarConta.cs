using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class loginCriarConta : Form
    {
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

        private bool UsuarioJaExiste(string usuario)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();
                    var sql = "SELECT COUNT(*) FROM Users WHERE Usuario = @usuario";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar usuário existente!\n\n" + ex.Message);
                return false;
            }
        }

        private void SalvarUsuario()
        {
            if (CamposObrigatoriosPreenchidos())
            {
                if (UsuarioJaExiste(txtUsuarioCadastro.Text))
                {
                    MessageBox.Show("Usuário já cadastrado. Escolha um nome de usuário diferente.", "Usuário existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                    {
                        cn.Open();

                        using (SqlCommand cmd = new SqlCommand("sp_InserirUser", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@usuario", txtUsuarioCadastro.Text);
                            cmd.Parameters.AddWithValue("@senha", txtSenhaCadastro.Text); // Note: Senha deve ser armazenada de forma segura
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Usuário cadastrado com sucesso!", "Cadastro realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Evento Load do formulário
        }
    }
}
