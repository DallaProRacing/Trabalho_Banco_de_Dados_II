using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class frmCliente : Form
    {
        int id = 0;

        public frmCliente(int id)
        {
            InitializeComponent();
            this.id = id;

            // Alteração
            if (this.id > 0)
                GetCliente(id);
        }

        
        private void TravarControles()
        {
            txtNomeCli.Enabled = false;
            mtxCPF.Enabled = false;
            txtAltura.Enabled = false;
            mtxPhone.Enabled = false;
        }

        private bool CamposObrigatoriosPreenchidos()
        {
            if (string.IsNullOrWhiteSpace(txtAltura.Text) ||
                string.IsNullOrWhiteSpace(txtNomeCli.Text) ||
                string.IsNullOrWhiteSpace(mtxCPF.Text) ||
                string.IsNullOrWhiteSpace(mtxPhone.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidarCPF(string cpf)
        {
            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Verifica se o CPF é inválido
            var invalidNumbers = new string[]
            {
                "00000000000", "11111111111", "22222222222", "33333333333",
                "44444444444", "55555555555", "66666666666", "77777777777",
                "88888888888", "99999999999"
            };

            if (invalidNumbers.Contains(cpf))
                return false;

            // Valida os dígitos do CPF
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf, digito;
            int soma, resto;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private void GetCliente(int id)
        {
            try
            {
                using (SqlConnection cn2 = new SqlConnection(Conn.StrCon))
                {
                    cn2.Open();
                    var sql = "SELECT * FROM Clientes WHERE ID_Cliente = @ID_Cliente";

                    using (SqlCommand cmd2 = new SqlCommand(sql, cn2))
                    {
                        cmd2.Parameters.AddWithValue("@ID_Cliente", id);

                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                        {
                            if (dr2.HasRows)
                            {
                                if (dr2.Read())
                                {
                                    txtNomeCli.Text = dr2["NomeCli"].ToString();
                                    mtxCPF.Text = dr2["CPF"].ToString();
                                    txtAltura.Text = dr2["Altura"].ToString();
                                    mtxPhone.Text = dr2["Contato"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao buscar o Cliente!\n\n" + ex.Message);
            }
        }

        private bool SalvarCliente()
        {
            if (!CamposObrigatoriosPreenchidos())
            {
                return false;
            }

            if (!ValidarCPF(mtxCPF.Text))
            {
                MessageBox.Show("CPF inválido. Por favor, verifique o CPF informado.", "CPF inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (SqlConnection cn2 = new SqlConnection(Conn.StrCon))
                {
                    cn2.Open();

                    // Verifica se o CPF já existe no banco de dados
                    string checkCpfSql = "SELECT COUNT(*) FROM Clientes WHERE CPF = @CPF AND ID_Cliente <> @ID";
                    using (SqlCommand checkCmd = new SqlCommand(checkCpfSql, cn2))
                    {
                        checkCmd.Parameters.AddWithValue("@CPF", mtxCPF.Text);
                        checkCmd.Parameters.AddWithValue("@ID", this.id);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("CPF já cadastrado. Por favor, verifique o CPF informado.", "CPF Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    using (SqlCommand cmd2 = new SqlCommand(this.id == 0 ? "sp_InserirCliente" : "sp_AtualizarCliente", cn2))
                    {
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@NomeCli", txtNomeCli.Text);
                        cmd2.Parameters.AddWithValue("@CPF", mtxCPF.Text);
                        cmd2.Parameters.AddWithValue("@Altura", int.Parse(txtAltura.Text));
                        cmd2.Parameters.AddWithValue("@Contato", mtxPhone.Text);

                        if (this.id != 0)
                            cmd2.Parameters.AddWithValue("@ID_Cliente", this.id);

                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível salvar os dados!\n\n" + ex.Message);
                return false;
            }
        }

        

        private void btnSalvarC_Click(object sender, EventArgs e)
        {
            if (SalvarCliente())
            {
                this.Close();
            }
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            // Evento Load do formulário
        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Evento ItemClicked do statusStrip2
        }




private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            // Evento TextChanged do txtPeso
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            // Evento Click do toolStripStatusLabel1
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
