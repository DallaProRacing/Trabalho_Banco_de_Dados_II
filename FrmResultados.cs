using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Trabalho_Banco_De_Dados
{
    public partial class FrmResultados : Form
    {
        public FrmResultados()
        {
            InitializeComponent();
            LoadData();

        }

        private DataGridViewRow selectedRow;
        private void LoadData()
        {
            using (SqlConnection cn = new SqlConnection(Conn.StrCon))
            {
                cn.Open();
                string query = "SELECT * FROM Vendas";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = table;

                dataGridViewVendas.DataSource = bindingSource;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (selectedRow == null)
            {
                MessageBox.Show("Por favor, selecione uma linha para gerar a nota de venda.");
                return;
            }

            DialogResult resp = MessageBox.Show("Deseja mesmo gerar uma nota?", "Sair",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                GerarNotaVenda(selectedRow);
            }
        }

        private void GerarNotaVenda(DataGridViewRow selectedRow)
        {
            try
            {
                // Pegando o ID da venda selecionada
                int vendaId = Convert.ToInt32(selectedRow.Cells["ID_Venda"].Value);

                // Query para buscar os dados necessários na tabela Vendas
                string query = @"SELECT ID_Venda, ID_Cliente, ID_Veiculo, 
                         DataVenda, Valor, Desconto, ValorVenda
                         FROM Vendas
                         WHERE ID_Venda = @ID_Venda";

                using (SqlConnection conn = new SqlConnection(Conn.StrCon))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adicionando o parâmetro para a query
                        cmd.Parameters.AddWithValue("@ID_Venda", vendaId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Dados recuperados
                                int clienteId = Convert.ToInt32(reader["ID_Cliente"]);
                                int veiculoId = Convert.ToInt32(reader["ID_Veiculo"]);
                                DateTime dataVenda = Convert.ToDateTime(reader["DataVenda"]);
                                decimal valor = Convert.ToDecimal(reader["Valor"]);
                                decimal desconto = Convert.ToDecimal(reader["Desconto"]);
                                decimal valorVenda = Convert.ToDecimal(reader["ValorVenda"]);

                                // Gerando o PDF com iTextSharp
                                Document document = new Document();
                                string filePath = $@"D:\Documents\FACULDADE URI\QUARTO_SEMESTRE\NOTAS_VENDAS_C#\NotaVenda_{vendaId}.pdf";
                                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                                document.Open();

                                // Adicionando cabeçalho
                                Paragraph header = new Paragraph("Concessionária DALLA ROSA AUTO CAR");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header);
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n"));

                                // Adicionando identificadores
                                document.Add(new Paragraph($"Nota de Venda - ID: {vendaId}"));
                                document.Add(new Paragraph($"Data da Emissão: {DateTime.Now.ToString("dd/MM/yyyy")}"));
                                document.Add(new Paragraph($"ID do Cliente: {clienteId}"));
                                document.Add(new Paragraph($"ID do Veículo: {veiculoId}"));

                                // Espaço
                                document.Add(new Paragraph("\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));

                                // Adicionando resumo da nota
                                document.Add(new Paragraph("Resumo da Nota"));
                                document.Add(new Paragraph($"Veículo: ID {veiculoId}"));
                                document.Add(new Paragraph($"Valor da Venda: R$ {valor:F2}"));
                                document.Add(new Paragraph($"Desconto Aplicado: {desconto}%"));
                                document.Add(new Paragraph($"Valor Final: R$ {valorVenda:F2}"));

                                // Espaço
                                document.Add(new Paragraph("\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));
                                document.Add(new Paragraph("\n\n"));

                                // Rodapé ou qualquer informação adicional
                                Paragraph footer1 = new Paragraph("Obrigado por comprar na Dalla Rosa AUTO CAR!");
                                footer1.Alignment = Element.ALIGN_CENTER;
                                document.Add(footer1);

                                Paragraph footer2 = new Paragraph("Estamos à disposição para atender você!");
                                footer2.Alignment = Element.ALIGN_CENTER;
                                document.Add(footer2);

                                // Fechando o documento
                                document.Close();
                                MessageBox.Show("Nota de venda gerada com sucesso!");
                            }
                            else
                            {
                                MessageBox.Show("Nenhuma venda encontrada para o ID fornecido.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao gerar a nota de venda: {ex.Message}");
            }
        }





        private void FrmResultados_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridViewVendas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewVendas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = dataGridViewVendas.Rows[e.RowIndex]; // Armazenar a linha selecionada
            }
        }

        private void FrmResultados_Load_1(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            ValidarBuscar();
            Buscar();
        }

        private bool ValidarBuscar()
        {
            if (cbxFiltro.Text == "")
            {
                MessageBox.Show("Selecione o campo a pesquisar!");
                cbxFiltro.Focus();
                return false;
            }
            else if (txtFiltro.Text == "")
            {
                MessageBox.Show("Campo buscar é Obrigatorio!");
                txtFiltro.Focus();
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

                        var sqlQuery = "SELECT * FROM Vendas where ";

                        switch (cbxFiltro.Text)
                        {
                            case "ID_Veiculo":
                                sqlQuery += "ID_Veiculo like '%" + txtFiltro.Text + "%'";
                                break;

                            case "ID_Cliente":
                                sqlQuery += "ID_Cliente like '%" + txtFiltro.Text + "%'";
                                break;

                            case "DataVenda":
                                sqlQuery += "DataVenda like '%" + txtFiltro.Text + "%'";
                                break;



                        }
                        sqlQuery += "Order By ID_Venda";


                        using (SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                dataGridViewVendas.DataSource = dt;
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

        private void btnRecarregar_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}



