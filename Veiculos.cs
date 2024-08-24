using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class Veiculos : Form
    {
        private string txtBuscar_Text;

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
                MessageBox.Show("Campo buscar é Obrigatorio!");
                txtbuscar.Focus();
                return false;
            }
            return true;

        }
        private void Buscar()
        {
            {
               

                try
                {
                    using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                    {
                        cn.Open();

                        var sqlQuery = "SELECT * FROM tb_veiculos where ";

                        switch (cbxBuscar.Text)
                        {
                            case "Marca":
                                sqlQuery += "Nome like '%" + txtbuscar.Text + "%'";
                                break;

                            case "Modelo":
                                sqlQuery += "Modelo like '%" + txtbuscar.Text + "%'";
                                break;

                            case "Ano":
                                sqlQuery += "Ano >= " + txtbuscar.Text;
                                break;

                            case "Fabricacao":
                                sqlQuery += "Fabricacao >=" + txtbuscar.Text;
                                break;


                            case "Cor":
                                sqlQuery += "Cor like '%" + txtbuscar.Text + "%'";
                                break;

                            case "Valor":
                                sqlQuery += "Valor >=" + txtbuscar.Text;
                                break;

                            case "Situacao":
                                sqlQuery += "Situacao like '%" + txtbuscar.Text + "%'";
                                break;

                        }
                        sqlQuery += "Order By Nome";


                        using (SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                dataGridView1.DataSource = dt;
                            }
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Falha ao tentar conectar\n\n" + ex.Message);
                }
            }
        }
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                string query = "SELECT * FROM tb_veiculos";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = table;

                dataGridView1.DataSource = bindingSource;
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            FrmVeiculosAdicionar frm = new FrmVeiculosAdicionar(0);
            frm.ShowDialog();
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
            }
        }


        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cbxbuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
       

       

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void btnRecarregar_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}