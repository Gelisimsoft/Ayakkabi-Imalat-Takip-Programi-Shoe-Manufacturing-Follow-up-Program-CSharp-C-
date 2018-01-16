using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokKartlariGuncelle : Form
    {
        public StokKartlariGuncelle()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private int stokidim;

        private void StokKartlariGuncelle_Load(object sender, EventArgs e)
        {
            StoklariGetir();
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

        private void Temizle()
        {
            foreach (Control fatih in this.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                checkBox1.Checked = false;
            }
        }

        private void FuturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                double gel = 0;
                DialogResult soralim = MessageBox.Show("Güncelleme İşlemini Yapmak İstiyor musunuz. ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soralim == DialogResult.Yes)
                {
                    EStok stokguncelle = new EStok();
                    stokguncelle._stokid = stokidim;
                    stokguncelle._stokkodu = Stokkodu.Text;
                    stokguncelle._stokadi = Stokadi.Text;
                    stokguncelle._stokaciklama = stokacikla.Text;
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        stokguncelle._bakiye = Convert.ToDouble(textBox4.Text);
                    }
                    if (textBox4.Text == "" && textBox4.Text == null)
                    {
                        stokguncelle._bakiye = gel;
                    }
                    else
                    {
                        stokguncelle._bakiye = Convert.ToDouble(textBox4.Text);
                    }
                    FStok.Sguncelle(stokguncelle);
                    Temizle();
                    StoklariGetir();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen Bakiyeyi Kontrol Ediniz.\n" + ex.ToString(), "Bakiye Kontrol", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                stokidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Stokkodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Stokadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                stokacikla.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
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
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DialogResult dr = MessageBox.Show("Lütfen Üretimi Tamamlanmış Olan Ürünlerin Miktarında Değişiklik Yapınız.Diğer Stoklarda Değişiklik Yapmanız Halinde Program Envanter Bilgisi ile Fiili Envanter Arasında Fark Oluşabilir.Devam Etmek İstiyor musunuz ?", "Dikkat !!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    textBox4.Visible = true;
                    label4.Visible = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            else
            {
                textBox4.Visible = false;
                label4.Visible = false;
            }
        }

        private void StokKartlariGuncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                StoklariGetir();
            }
        }
    }
}