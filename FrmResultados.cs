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

            string Id_venda = selectedRow.Cells["Id_Venda"].Value.ToString();
            string filePath = $@"D:\Documents\FACULDADE URI\TERCEITO_ SEMESTRE\Notas Vendas projeto c#\NotaVenda_{Id_venda}.pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();


            // Cabeçalho da Nota de Venda
            doc.Add(new Paragraph("Nota de Venda"));
            doc.Add(new Paragraph(""));
            doc.Add(new Paragraph("CONCESSIONÁRIA DALLA ROSA AUTO CAR"));
            doc.Add(new Paragraph($"Data da emissão da nota: {DateTime.Now}"));

            for (int i = 0; i < 3; i++)
            {
                doc.Add(Chunk.NEWLINE);
            }

            // Dados do Cliente          

            
            string dataVenda = selectedRow.Cells["DataVenda"].Value.ToString();
            string nomeCliente1 = selectedRow.Cells["NomeCli"].Value.ToString();          
            string cpfCliente = selectedRow.Cells["CPF"].Value.ToString();
            string alturaCliente = selectedRow.Cells["Altura"].Value.ToString();
            string contatoCliente = selectedRow.Cells["Contato"].Value.ToString();



            // Cabeçalho da Nota de Venda
            
            Paragraph header = new Paragraph("");
            header.Alignment = Element.ALIGN_CENTER;
            doc.Add(header);

            Paragraph paragraph = new Paragraph($"Data da Venda: {dataVenda}");
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);

            // Dados do Cliente
            Paragraph cliente = new Paragraph($"Cliente: {nomeCliente1}");
            cliente.Alignment = Element.ALIGN_CENTER;
            doc.Add(cliente);

           
            Paragraph cpf = new Paragraph($"CPF: {cpfCliente}");
            cpf.Alignment = Element.ALIGN_CENTER;
            doc.Add(cpf);

            Paragraph altura = new Paragraph($"Altura: {alturaCliente}");
            altura.Alignment = Element.ALIGN_CENTER;
            doc.Add(altura);

            Paragraph contato = new Paragraph($"Contato: {contatoCliente}");
            contato.Alignment = Element.ALIGN_CENTER;
            doc.Add(contato);

            for (int i = 0; i < 3; i++)
            {
                doc.Add(Chunk.NEWLINE);
            }
            // Dados da Venda
            doc.Add(new Paragraph("Detalhes do veículo vendido:"));


            PdfPTable table = new PdfPTable(9); // Ajustar o número de colunas conforme necessário
            
            PdfPCell headerCell = new PdfPCell(new Phrase("Detalhes da Venda"));
            headerCell.Colspan = 9; // Define a célula para abranger todas as colunas
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alinha o conteúdo centralizado
            table.AddCell(headerCell);

            for (int i = 0; i < 3; i++)
            {
                doc.Add(Chunk.NEWLINE);
            }

            table.AddCell("Id_Veiculo");
            table.AddCell("Nome");
            table.AddCell("Modelo");
            table.AddCell("Ano");
            table.AddCell("Fabricacao");
            table.AddCell("Cor");
            table.AddCell("Combustivel");
            table.AddCell("Automatico");
            table.AddCell("Valor");

            table.AddCell(selectedRow.Cells["Id_Veiculo"].Value.ToString());
            table.AddCell(selectedRow.Cells["Nome"].Value.ToString());
            table.AddCell(selectedRow.Cells["Modelo"].Value.ToString());
            table.AddCell(selectedRow.Cells["Ano"].Value.ToString());
            table.AddCell(selectedRow.Cells["Fabricacao"].Value.ToString());
            table.AddCell(selectedRow.Cells["Cor"].Value.ToString());
            table.AddCell(selectedRow.Cells["Combustivel"].Value.ToString());
            table.AddCell(selectedRow.Cells["Automatico"].Value.ToString());
            table.AddCell(Convert.ToDecimal(selectedRow.Cells["Valor"].Value).ToString("C"));

            doc.Add(table);
            doc.Close();

            MessageBox.Show("Nota de venda gerada com sucesso!");
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
                            case "NomeVeiculo":
                                sqlQuery += "Nome like '%" + txtFiltro.Text + "%'";
                                break;

                            case "NomeCliente":
                                sqlQuery += "NomeCli like '%" + txtFiltro.Text + "%'";
                                break;

                            case "DataVenda":
                                sqlQuery += "DataVenda like '%" + txtFiltro.Text + "%'";
                                break;



                        }
                        sqlQuery += "Order By Nome";


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


    }
}



