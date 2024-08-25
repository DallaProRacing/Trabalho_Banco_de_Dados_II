using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class Veiculos : Form
    {
        public Veiculos()
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
            if (cbxBuscar.Text == "")
            {
                MessageBox.Show("Selecione o campo a pesquisar!");
                cbxBuscar.Focus();
                return false;
            }
            else if (txtbuscar.Text == "")
            {
                MessageBox.Show("Campo buscar é Obrigatório!");
                txtbuscar.Focus();
                return false;
            }
            return true;
        }

        private void Buscar()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    // Monta o comando SQL para selecionar os veículos usando a função dbo.fn_FiltrarVeiculos
                    string query = "SELECT * FROM dbo.fn_FiltrarVeiculos(" +
                                   "@ID_Veiculo, @NomeVeiculo, @Modelo, @Ano, @Fabricacao, " +
                                   "@Cor, @Combustivel, @Automatico, @Valor, @Situacao, @KM)";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Configura os parâmetros padrão como NULL
                        cmd.Parameters.AddWithValue("@ID_Veiculo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NomeVeiculo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Modelo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ano", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Fabricacao", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cor", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Combustivel", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Automatico", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Valor", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Situacao", DBNull.Value);
                        cmd.Parameters.AddWithValue("@KM", DBNull.Value);

                        // Adiciona o parâmetro correto baseado na seleção do ComboBox
                        switch (cbxBuscar.Text)
                        {
                            case "Marca":
                                cmd.Parameters["@NomeVeiculo"].Value = txtbuscar.Text;
                                break;

                            case "Modelo":
                                cmd.Parameters["@Modelo"].Value = txtbuscar.Text;
                                break;

                            case "Ano":
                                cmd.Parameters["@Ano"].Value = txtbuscar.Text;
                                break;

                            case "Fabricacao":
                                cmd.Parameters["@Fabricacao"].Value = txtbuscar.Text;
                                break;

                            case "Cor":
                                cmd.Parameters["@Cor"].Value = txtbuscar.Text;
                                break;

                            case "Valor":
                                // Converte o texto para decimal e aplica ao parâmetro Valor
                                if (decimal.TryParse(txtbuscar.Text, out decimal valor))
                                {
                                    query += " AND Valor >= @Valor";
                                    cmd.Parameters["@Valor"].Value = valor;
                                }
                                break;

                            case "KM":
                                // Converte o texto para decimal e aplica ao parâmetro KM
                                if (decimal.TryParse(txtbuscar.Text, out decimal km))
                                {
                                    query += " AND KM >= @KM";
                                    cmd.Parameters["@KM"].Value = km;
                                }
                                break;

                            case "Situacao":
                                cmd.Parameters["@Situacao"].Value = txtbuscar.Text;
                                break;
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                dataGridView1.DataSource = dt;
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
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                string query = "SELECT * FROM vw_Veiculos";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                dataGridView1.DataSource = table;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            FrmVeiculosAdicionar frm = new FrmVeiculosAdicionar(0);
            frm.ShowDialog();
            LoadData(); // Recarregar os dados após adicionar
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var situacao = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Situacao"].Value.ToString();
            if (situacao == "Vendido")
            {
                MessageBox.Show("Este veículo não pode ser excluído pois já foi vendido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
                FrmVeiculosAdicionar frm = new FrmVeiculosAdicionar(id, true);
                frm.ShowDialog();
                LoadData(); // Recarregar os dados após exclusão
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            var situacao = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Situacao"].Value.ToString();
            if (situacao == "Vendido")
            {
                MessageBox.Show("Este veículo não pode ser alterado pois já foi vendido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
                FrmVeiculosAdicionar frm = new FrmVeiculosAdicionar(id);
                frm.ShowDialog();
                LoadData(); // Recarregar os dados após alteração
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRecarregar_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
        private void cb_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void cbxbuscar_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e) { }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) { }
    }
}
