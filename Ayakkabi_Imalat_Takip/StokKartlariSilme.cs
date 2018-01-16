using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokKartlariSilme : Form
    {
        public StokKartlariSilme()
        {
            InitializeComponent();
        }

        private void StoklariGetir()
        {
            DataTable dt = FStok.sekstre();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "stokid";
            dataGridView1.Columns[1].HeaderText = "Stok Kodu";
            dataGridView1.Columns[2].HeaderText = "Stok Adı";
            dataGridView1.Columns[3].HeaderText = "Stok Açıklama";
            dataGridView1.Columns[4].HeaderText = "Stok Miktar";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private int stokidim;
        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_fcmedya;");

        private void StokKartlariSilme_Load(object sender, EventArgs e)
        {
            StoklariGetir();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Stok Kartını Silmek İstediğinize Emin misiniz. ?", "Stok Kartı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                EStok Stoksil = new EStok();
                Stoksil._stokid = stokidim;
                FStok.Ssil(Stoksil);
                StoklariGetir();
                Temizle();
            }
        }

        private void Temizle()
        {
            foreach (Control fatih in this.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                stokidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Stokkodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Stokadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                stokacikla.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                stokidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Stokkodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Stokadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                stokacikla.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}