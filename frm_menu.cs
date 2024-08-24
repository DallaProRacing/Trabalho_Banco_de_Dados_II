using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_Banco_De_Dados
{
    public partial class frm_menu : Form
    {
        public frm_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Veiculos veiculos = new Veiculos();
            veiculos.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void frm_menu_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("Deseja mesmo sair?", "Sair",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                login login = new login();
                this.Close();
                login.ShowDialog();
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            frmVendas frmVendas = new frmVendas();
            frmVendas.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmResultados frmResultados = new FrmResultados();
            frmResultados.ShowDialog();
        }
    }
}
