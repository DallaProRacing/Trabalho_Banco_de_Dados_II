using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Trabalho_Banco_De_Dados
{
    public partial class frmVendas : Form
    {
        public frmVendas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void ConsultarVeiculo()
        {
            if (CampoIdVeiculoObrigatorio())
            {



                SqlConnection conn;
                SqlCommand comm;
                SqlDataReader reader;

                bool bIsOperationOK = true;


                conn = new SqlConnection(Conn.StrCon);

                comm = new SqlCommand("SELECT Nome,Modelo,Ano,Fabricacao,Cor,Combustivel, Automatico,Valor FROM tb_Veiculos WHERE Id = @Id", conn);

                comm.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                comm.Parameters["@Id"].Value = Convert.ToInt32(txtIdVeiculo.Text);



                try
                {
                    try
                    {

                        conn.Open();
                    }
                    catch (Exception error)
                    {
                        bIsOperationOK = false;
                        MessageBox.Show(error.Message, "Erro ao tentar abrir a conexao com o BD",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        reader = comm.ExecuteReader();
                        if (reader.Read())
                        {
                            txtNome.Text = reader["Nome"].ToString();
                            txtModelo.Text = reader["Modelo"].ToString();
                            txtAno.Text = reader["Ano"].ToString();
                            txtFabricacao.Text = reader["Fabricacao"].ToString();
                            txtCor.Text = reader["Cor"].ToString();
                            txtCombustivel.Text = reader["Combustivel"].ToString();
                            txtAno.Text = reader["Ano"].ToString();
                            txtAutomatico.Text = reader["Automatico"].ToString();
                            txtValor.Text = reader["Valor"].ToString();
                        }


                    }
                    catch (Exception error)
                    {
                        bIsOperationOK = false;
                        MessageBox.Show(error.Message, "Erro ao tentar abrir a conexao com o BD",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {


                }
                finally
                {
                    //Fecha a conexão
                    conn.Close();


                }
            }
        }

       
        

        private void ConsultarCliente()
        {
            if (CampoIdClienteObrigatorio())
            {

            
            SqlConnection conn;
            SqlCommand comm;
            SqlDataReader reader;

            bool bIsOperationOK = true;


            conn = new SqlConnection(Conn.StrCon);

            comm = new SqlCommand("SELECT NomeCli,CPF,Altura,Contato FROM Clientes WHERE ID_Cliente = @ID_Cliente", conn);

            comm.Parameters.Add("@ID_Cliente", System.Data.SqlDbType.Int);
            comm.Parameters["@ID_Cliente"].Value = Convert.ToInt32(txtIdCliente.Text);



                try
                {
                    try
                    {

                        conn.Open();
                    }
                    catch (Exception error)
                    {
                        bIsOperationOK = false;
                        MessageBox.Show(error.Message, "Erro ao tentar abrir a conexao com o BD",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        reader = comm.ExecuteReader();
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
                        bIsOperationOK = false;
                        MessageBox.Show(error.Message, "Erro ao tentar abrir a conexao com o BD",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {


                }
                finally
                {
                    //Fecha a conexão
                    conn.Close();

                }
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
                string.IsNullOrWhiteSpace(txtValor.Text) ||
                string.IsNullOrWhiteSpace(txtNomeCli.Text) ||               
                string.IsNullOrWhiteSpace(mtxCPF.Text) ||
                string.IsNullOrWhiteSpace(txtAltura.Text) ||
                string.IsNullOrWhiteSpace(dtpVenda.Text) ||
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
                SqlConnection conn = new SqlConnection(Conn.StrCon);
                SqlCommand commInsert = new SqlCommand();
                SqlCommand commUpdate = new SqlCommand();

                try
                {
                    conn.Open();

                    SqlTransaction transaction = conn.BeginTransaction();

                    commInsert = new SqlCommand("INSERT INTO Vendas (Id_Veiculo, Nome, Modelo, Ano, Fabricacao, Cor, Combustivel, Automatico, Valor, ID_Cliente, NomeCli, CPF, Altura, Contato, DataVenda) " +
                        "VALUES (@Id_Veiculo, @Nome, @Modelo, @Ano, @Fabricacao, @Cor, @Combustivel, @Automatico, @Valor, @ID_Cliente, @NomeCli, @CPF, @Altura, @Contato, @DataVenda)", conn, transaction);

                    commInsert.Parameters.Add("@Id_Veiculo", SqlDbType.NVarChar).Value = txtIdVeiculo.Text;
                    commInsert.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = txtNome.Text;
                    commInsert.Parameters.Add("@Modelo", SqlDbType.NVarChar).Value = txtModelo.Text;
                    commInsert.Parameters.Add("@Ano", SqlDbType.Int).Value = Convert.ToInt32(txtAno.Text);
                    commInsert.Parameters.Add("@Fabricacao", SqlDbType.Int).Value = Convert.ToInt32(txtFabricacao.Text);
                    commInsert.Parameters.Add("@Cor", SqlDbType.NVarChar).Value = txtCor.Text;
                    commInsert.Parameters.Add("@Combustivel", SqlDbType.NVarChar).Value = txtCombustivel.Text;
                    commInsert.Parameters.Add("@Automatico", SqlDbType.Bit).Value = Convert.ToBoolean(txtAutomatico.Text);
                    commInsert.Parameters.Add("@Valor", SqlDbType.Decimal).Value = Convert.ToDecimal(txtValor.Text);
                    commInsert.Parameters.Add("@ID_Cliente", SqlDbType.Int).Value = Convert.ToInt32(txtIdCliente.Text);
                    commInsert.Parameters.Add("@NomeCli", SqlDbType.NVarChar).Value = txtNomeCli.Text;
                    commInsert.Parameters.Add("@CPF", SqlDbType.NVarChar).Value = mtxCPF.Text;
                    commInsert.Parameters.Add("@Altura", SqlDbType.Decimal).Value = Convert.ToDecimal(txtAltura.Text);
                    commInsert.Parameters.Add("@Contato", SqlDbType.NVarChar).Value = mtxPhone.Text;
                    commInsert.Parameters.Add("@DataVenda", SqlDbType.NVarChar).Value = dtpVenda.Text;

                    commInsert.ExecuteNonQuery();

                    commUpdate = new SqlCommand("UPDATE tb_Veiculos SET Situacao = 'Vendido' WHERE Id = @Id", conn, transaction);
                    commUpdate.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtIdVeiculo.Text);
                    commUpdate.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Venda registrada com sucesso!", "Registro de Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao registrar a venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnRegistrarVenda_Click(object sender, EventArgs e)
        {
            VenderVeiculo();           
            
            
        }


        private void LoadDataVeiculos()
        {
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                // Alterando a consulta SQL para filtrar apenas veículos "À venda"
                string query = "SELECT * FROM tb_veiculos WHERE Situacao = 'À venda'";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = table;

                dataGridView5.DataSource = bindingSource;
            }
        }
        private void LoadDataClientes()
        {
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                string query = "SELECT * FROM Clientes";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = table;

                dataGridView5.DataSource = bindingSource;
            }
        }

        private void btnVerificarVeiculo_Click(object sender, EventArgs e)
        {
            LoadDataVeiculos();
        }

        private void btnVerificarCliente_Click(object sender, EventArgs e)
        {
            LoadDataClientes();
        }

        private void LimparForm()
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
            txtIdCliente.Clear();            
            txtNomeCli.Clear();
            txtAltura.Clear();
            mtxCPF.Clear();
            mtxPhone.Clear();
            





        }
        private void btcLimparCampos_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        private void frmVendas_Load(object sender, EventArgs e)
        {

        }
    }
}