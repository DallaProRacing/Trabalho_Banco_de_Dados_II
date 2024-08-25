using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class FrmVeiculosAdicionar : Form
    {
        int id = 0;

        public FrmVeiculosAdicionar(int id)
        {
            InitializeComponent();
            this.id = id;

            // Alteração
            if (this.id > 0)
            {
                GetVeiculo(id);
            }
        }

        private bool CamposObrigatoriosPreenchidos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtAno.Text) ||
                string.IsNullOrWhiteSpace(txtCor.Text) ||
                string.IsNullOrWhiteSpace(txtFabricacao.Text) ||
                string.IsNullOrWhiteSpace(txtModelo.Text) ||
                string.IsNullOrWhiteSpace(txtValor.Text) ||
                string.IsNullOrWhiteSpace(cbxAutomatico.Text) ||
                string.IsNullOrWhiteSpace(cbxCombustivel.Text)||
                string.IsNullOrWhiteSpace(cbxSituacao.Text) ||
                string.IsNullOrWhiteSpace(txtKM.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public FrmVeiculosAdicionar(int id, bool excluir)
        {
            InitializeComponent();
            this.id = id;

            // Exclusão
            if (excluir)
            {
                if (this.id > 0)
                {
                    GetVeiculo(id);
                    TravarControles();
                    btnSalvar.Visible = false;
                    btnExcluir.Visible = true;
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void TravarControles()
        {
            txtNome.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            txtFabricacao.Enabled = false;
            txtCor.Enabled = false;
            cbxAutomatico.Enabled = false;
            cbxCombustivel.Enabled = false;
            txtValor.Enabled = false;
            cbxSituacao.Enabled = false;
            txtKM.Enabled = false;
        }

        private void GetVeiculo(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    var sql = "SELECT * FROM Veiculos WHERE ID_Veiculo= " + id;

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    txtNome.Text = dr["NomeVeiculo"].ToString();
                                    txtModelo.Text = dr["Modelo"].ToString();
                                    txtAno.Text = dr["Ano"].ToString();
                                    txtFabricacao.Text = dr["Fabricacao"].ToString();
                                    txtCor.Text = dr["Cor"].ToString();
                                    txtKM.Text = dr["KM"].ToString();
                                    cbxSituacao.Text = dr["KM"].ToString();

                                    switch (Convert.ToInt32(dr["Combustivel"]))
                                    {
                                        case 1:
                                            cbxCombustivel.Text = "1 Gasolina";
                                            break;
                                        case 2:
                                            cbxCombustivel.Text = "2 Álcool";
                                            break;
                                        case 3:
                                            cbxCombustivel.Text = "3 Flex";
                                            break;
                                        case 4:
                                            cbxCombustivel.Text = "4 Diesel";
                                            break;
                                        case 5:
                                            cbxCombustivel.Text = "5 Gás Natural";
                                            break;
                                    }

                                    if (Convert.ToBoolean(dr["Automatico"]))
                                        cbxAutomatico.Text = "Sim";
                                    else
                                        cbxAutomatico.Text = "Não";

                                    txtValor.Text = dr["Valor"].ToString();
                                    cbxSituacao.Text = dr["Situacao"].ToString();

                                    if (cbxSituacao.Text == "Vendido")
                                    {
                                        TravarControles();
                                        MessageBox.Show("Este veículo não pode ser alterado, pois já foi vendido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        btnSalvar.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao buscar o veículo!\n\n" + ex.Message);
            }
        }

        private bool SalvarVeiculo()
        {
            if (CamposObrigatoriosPreenchidos())
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                    {
                        cn.Open();

                        if (this.id == 0)
                        {
                            // Utiliza a Stored Procedure para inserir um novo veículo
                            using (SqlCommand cmd = new SqlCommand("sp_InserirVeiculo", cn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@NomeVeiculo", txtNome.Text);
                                cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                                cmd.Parameters.AddWithValue("@Ano", txtAno.Text);
                                cmd.Parameters.AddWithValue("@Fabricacao", txtFabricacao.Text);
                                cmd.Parameters.AddWithValue("@Cor", txtCor.Text);
                                cmd.Parameters.AddWithValue("@Combustivel", cbxCombustivel.Text.Substring(0, 1));
                                cmd.Parameters.AddWithValue("@Automatico", cbxAutomatico.Text.Substring(0, 1) == "S" ? 1 : 0);
                                cmd.Parameters.AddWithValue("@Valor", txtValor.Text.Replace(",", "."));
                                cmd.Parameters.AddWithValue("@Situacao", cbxSituacao.Text);
                                cmd.Parameters.AddWithValue("@KM", txtKM.Text.Replace(",", "."));
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Atualiza diretamente os dados no banco de dados
                            
                            using (SqlCommand cmd = new SqlCommand("sp_AtualizarVeiculo", cn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID_Veiculo", this.id);
                                cmd.Parameters.AddWithValue("@NomeVeiculo", txtNome.Text);
                                cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                                cmd.Parameters.AddWithValue("@Ano", txtAno.Text);
                                cmd.Parameters.AddWithValue("@Fabricacao", txtFabricacao.Text);
                                cmd.Parameters.AddWithValue("@Cor", txtCor.Text);
                                cmd.Parameters.AddWithValue("@Combustivel", cbxCombustivel.Text.Substring(0, 1));
                                cmd.Parameters.AddWithValue("@Automatico", cbxAutomatico.Text.Substring(0, 1) == "S" ? 1 : 0);
                                cmd.Parameters.AddWithValue("@Valor", txtValor.Text.Replace(",", "."));
                                cmd.Parameters.AddWithValue("@Situacao", cbxSituacao.Text);
                                cmd.Parameters.AddWithValue("@KM", txtKM.Text.Replace(",", "."));
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    return true; // Indica que o salvamento foi bem-sucedido
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível salvar os dados!\n\n" + ex.Message);
                    return false; // Indica que houve um erro durante o salvamento
                }
            }
            return false; // Indica que os campos obrigatórios não foram preenchidos
        }

        private void FrmVeiculosAdicionar_Load(object sender, EventArgs e)
        {
            // Evento Load do formulário
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // Evento Click do label10
        }

        private void label14_Click(object sender, EventArgs e)
        {
            // Evento Click do label14
        }

        private void label12_Click(object sender, EventArgs e)
        {
            // Evento Click do label12
        }

        private void label15_Click(object sender, EventArgs e)
        {
            // Evento Click do label15
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Evento TextChanged do textBox6
        }

        private void label16_Click(object sender, EventArgs e)
        {
            // Evento Click do label16
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Evento TextChanged do textBox2
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (SalvarVeiculo())
            {
                this.Close();
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            // Evento TextChanged do txtNome
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("Deseja Excluir?", "Excluir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                ExcluirVeiculo();
                this.Close();
            }
        }

        private void ExcluirVeiculo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    
                    using (SqlCommand cmd = new SqlCommand("sp_ExcluirVeiculo", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Veiculo", this.id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível excluir o veículo!\n\n" + ex.Message);
            }
        }
    }
}
