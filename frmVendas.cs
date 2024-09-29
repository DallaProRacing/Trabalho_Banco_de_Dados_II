using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class frmVendas : Form
    {
        public frmVendas()
        {
            InitializeComponent();
        }

        private void ConsultarVeiculo()
        {
            if (CampoIdVeiculoObrigatorio())
            {
                SqlConnection conn = new SqlConnection(Conn.StrCon);
                SqlCommand comm = new SqlCommand("SELECT NomeVeiculo, Modelo, Ano, Fabricacao, Cor, Combustivel, Automatico, Valor, KM FROM Veiculos WHERE ID_Veiculo = @ID_Veiculo", conn);

                comm.Parameters.Add("@ID_Veiculo", SqlDbType.Int);
                comm.Parameters["@ID_Veiculo"].Value = Convert.ToInt32(txtIdVeiculo.Text);

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        txtNome.Text = reader["NomeVeiculo"].ToString();
                        txtModelo.Text = reader["Modelo"].ToString();
                        txtAno.Text = reader["Ano"].ToString();
                        txtFabricacao.Text = reader["Fabricacao"].ToString();
                        txtCor.Text = reader["Cor"].ToString();
                        txtCombustivel.Text = reader["Combustivel"].ToString();
                        txtAutomatico.Text = reader["Automatico"].ToString();
                        txtValor.Text = reader["Valor"].ToString();
                        txtKM.Text = reader["KM"].ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Erro ao tentar abrir a conexão com o BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void ConsultarCliente()
        {
            if (CampoIdClienteObrigatorio())
            {
                SqlConnection conn = new SqlConnection(Conn.StrCon);
                SqlCommand comm = new SqlCommand("SELECT NomeCli, CPF, Altura, Contato FROM Clientes WHERE ID_Cliente = @ID_Cliente", conn);

                comm.Parameters.Add("@ID_Cliente", SqlDbType.Int);
                comm.Parameters["@ID_Cliente"].Value = Convert.ToInt32(txtIdCliente.Text);

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        txtNomeCli.Text = reader["NomeCli"].ToString();
                        mtxCPF.Text = reader["CPF"].ToString();
                        txtAltura.Text = reader["Altura"].ToString();
                        mtxPhone.Text = reader["Contato"].ToString();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Erro ao tentar abrir a conexão com o BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void LoadDataVeiculos()
        {
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                string query = "SELECT * FROM vw_Veiculos WHERE Situacao = 'À Venda'";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                dataGridView5.DataSource = table;
            }
        }

        private void LoadDataClientes()
        {
            using (SqlConnection cn2 = new SqlConnection(Conn.StrCon))
            {
                cn2.Open();
                string query = "SELECT * FROM Clientes";

                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(query, cn2);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter2);
                DataTable table2 = new DataTable();
                dataAdapter2.Fill(table2);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = table2;

                dataGridView5.DataSource = bindingSource;
            }
        }

        private void btnConsultarVeiculo_Click(object sender, EventArgs e)
        {
            ConsultarVeiculo();
        }

        private void btnConsultarCliente_Click(object sender, EventArgs e)
        {
            ConsultarCliente();
        }

        private bool CampoIdVeiculoObrigatorio()
        {
            if (string.IsNullOrWhiteSpace(txtIdVeiculo.Text))
            {
                MessageBox.Show("Por favor, preencha o campo obrigatório.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CampoIdClienteObrigatorio()
        {
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Por favor, preencha o campo obrigatório.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CamposObrigatoriosPreenchidos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtAno.Text) ||
                string.IsNullOrWhiteSpace(txtCor.Text) ||
                string.IsNullOrWhiteSpace(txtFabricacao.Text) ||
                string.IsNullOrWhiteSpace(txtModelo.Text) ||
                string.IsNullOrWhiteSpace(txtValor.Text) ||
                string.IsNullOrWhiteSpace(txtAutomatico.Text) ||
                string.IsNullOrWhiteSpace(txtCombustivel.Text) ||
                string.IsNullOrWhiteSpace(txtNomeCli.Text) ||
                string.IsNullOrWhiteSpace(mtxCPF.Text) ||
                string.IsNullOrWhiteSpace(txtAltura.Text) ||
                string.IsNullOrWhiteSpace(dtpVenda.Text) ||
                string.IsNullOrWhiteSpace(txtDesconto.Text) ||
                string.IsNullOrWhiteSpace(mtxPhone.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void VenderVeiculo()
        {
            if (CamposObrigatoriosPreenchidos())
            {
                using (SqlConnection conn = new SqlConnection(Conn.StrCon))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand commInsert = new SqlCommand("sp_InserirVenda", conn, transaction);
                        commInsert.CommandType = CommandType.StoredProcedure;

                        commInsert.Parameters.Add("@ID_Veiculo", SqlDbType.Int).Value = Convert.ToInt32(txtIdVeiculo.Text);
                        commInsert.Parameters.Add("@ID_Cliente", SqlDbType.Int).Value = Convert.ToInt32(txtIdCliente.Text);
                        commInsert.Parameters.Add("@DataVenda", SqlDbType.Date).Value = Convert.ToDateTime(dtpVenda.Text);
                        commInsert.Parameters.Add("@Valor", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text);
                        commInsert.Parameters.Add("@Desconto", SqlDbType.Decimal).Value = Convert.ToDecimal(txtDesconto.Text);

                        commInsert.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Venda realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        MessageBox.Show(error.Message, "Erro ao tentar realizar a venda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void LimparCampos()
        {
            txtIdVeiculo.Clear();
            txtNome.Clear();
            txtModelo.Clear();
            txtAno.Clear();
            txtFabricacao.Clear();
            txtCor.Clear();
            txtCombustivel.Clear();
            txtAutomatico.Clear();
            txtValor.Clear();
            txtKM.Clear();
            txtIdCliente.Clear();
            txtNomeCli.Clear();
            mtxCPF.Clear();
            txtAltura.Clear();
            mtxPhone.Clear();
            dtpVenda.Value = DateTime.Now;
            txtDesconto.Clear();
        }

       

        private void btcLimparCampos_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void frmVendas_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnRegistrarVenda_Click(object sender, EventArgs e)
        {
            VenderVeiculo();
        }

        private void btnVerificarVeiculo_Click(object sender, EventArgs e)
        {
            LoadDataVeiculos();
        }

        private void btnVerificarCliente_Click(object sender, EventArgs e)
        {
            LoadDataClientes();
        }

        private void btnConsultarCliente_Click_1(object sender, EventArgs e)
        {
            ConsultarCliente();
        }
    }
}
