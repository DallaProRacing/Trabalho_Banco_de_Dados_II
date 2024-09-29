using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class Clientes : Form
    {
        private string txtBuscarC_text;
        public Clientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarBuscar())
                Buscar();
        }

        private bool ValidarBuscar()
        {
            if (cbxBuscarC.Text == "")
            {
                MessageBox.Show("Selecione o campo a pesquisar!");
                cbxBuscarC.Focus();
                return false;
            }
            else if (txtbuscarC.Text == "")
            {
                MessageBox.Show("Campo buscar é Obrigatorio!");
                txtbuscarC.Focus();
                return false;
            }
            return true;
        }

        private void Buscar()
        {
            try
            {
                using (SqlConnection cn2 = new SqlConnection(Conn.StrCon))
                {
                    cn2.Open();

                    // Variáveis para armazenar os valores de busca
                    int? idCliente = null;
                    string nomeCli = null;
                    string cpf = null;
                    int? altura = null;
                    string contato = null;

                    // Verifica qual campo está sendo usado para a busca e atribui o valor correspondente
                    switch (cbxBuscarC.Text)
                    {
                        case "ID_Cliente":
                            idCliente = string.IsNullOrEmpty(txtbuscarC.Text) ? (int?)null : int.Parse(txtbuscarC.Text);
                            break;
                        case "NomeCli":
                            nomeCli = txtbuscarC.Text;
                            break;
                        case "CPF":
                            cpf = txtbuscarC.Text;
                            break;
                        case "Altura":
                            altura = string.IsNullOrEmpty(txtbuscarC.Text) ? (int?)null : int.Parse(txtbuscarC.Text);
                            break;
                        case "Contato":
                            contato = txtbuscarC.Text;
                            break;
                    }

                    // Consulta usando a função filtradora
                    var sqlQuery = "SELECT * FROM dbo.fn_FiltrarClientes(@ID_Cliente, @NomeCli, @CPF, @Altura, @Contato)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cn2))
                    {
                        // Define os parâmetros com os valores de busca
                        cmd.Parameters.AddWithValue("@ID_Cliente", idCliente.HasValue ? (object)idCliente.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NomeCli", (object)nomeCli ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CPF", (object)cpf ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Altura", altura.HasValue ? (object)altura.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Contato", (object)contato ?? DBNull.Value);

                        using (SqlDataAdapter da2 = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt2 = new DataTable())
                            {
                                da2.Fill(dt2);
                                dataGridView2.DataSource = dt2;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao tentar conectar\n\n" + ex.Message);
            }
        }


        private void LoadData()
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

                dataGridView2.DataSource = bindingSource;
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {           
            LoadData();
        }

        private void btnAdicionarC_Click(object sender, EventArgs e)
        {
            frmCliente novocliente = new frmCliente(0);
            novocliente.ShowDialog();
            LoadData();
        }

        private void btnAlterarC_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell != null && dataGridView2.CurrentCell.RowIndex >= 0)
            {
                var id2 = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value);

                frmCliente frm = new frmCliente(id2);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para alterar.");
            }
            LoadData();
        }

        private void btnRecarregarC_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnBuscarC_Click(object sender, EventArgs e)
        {
            if (ValidarBuscar())
                Buscar();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView2.CurrentCell != null && dataGridView2.CurrentCell.RowIndex >= 0)
                {
                    var idCliente = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value);

                    DialogResult result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação de exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(Conn.StrCon))
                        {
                            conn.Open();
                            SqlTransaction transaction = conn.BeginTransaction();

                            try
                            {
                                SqlCommand commDelete = new SqlCommand("DELETE FROM Clientes WHERE ID_Cliente = @ID_Cliente", conn, transaction);
                                commDelete.Parameters.Add("@ID_Cliente", SqlDbType.Int).Value = idCliente;

                                commDelete.ExecuteNonQuery();
                                transaction.Commit();

                                MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            catch (SqlException ex)
                            {
                                transaction.Rollback();

                                if (ex.Number == 50000) // Número do erro definido no RAISERROR na trigger
                                {
                                    MessageBox.Show(ex.Message, "Erro ao excluir cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    MessageBox.Show("Ocorreu um erro inesperado ao tentar excluir o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para excluir.");
                }
            }
            }
    }
 }

