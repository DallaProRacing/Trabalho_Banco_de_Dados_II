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

                    var sqlQuery = "SELECT * FROM Clientes where ";

                    switch (cbxBuscarC.Text)
                    {
                        case "NomeCli":
                            sqlQuery += "NomeCli like '%" + txtbuscarC.Text + "%'";
                            break;

                        case "Idade":
                            sqlQuery += "Idade  like " + txtbuscarC.Text ;
                            break;

                        case "CPF":
                            sqlQuery += "CPF like '%" + txtbuscarC.Text + "%'";
                            break;

                        case "Altura":
                            sqlQuery += "Altura like " + txtbuscarC.Text;
                            break;

                        case "Contato":
                            sqlQuery += "Contato like '%" + txtbuscarC.Text + "%'";
                            break;

                    }
                    sqlQuery += "Order By NomeCli";


                    using (SqlDataAdapter da2 = new SqlDataAdapter(sqlQuery, cn2))
                    {
                        using (DataTable dt2 = new DataTable())
                        {
                            da2.Fill(dt2);
                            dataGridView2.DataSource = dt2;
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
    }
 }

