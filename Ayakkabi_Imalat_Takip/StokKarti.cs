using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokKarti : Form
    {
        public StokKarti()
        {
            InitializeComponent();
        }

        private void ListemiGetir()
        {
            listView1.Items.Clear();
            DataTable dtt = FStok.sekstre();
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                ListViewItem lolo = new ListViewItem(dtt.Rows[i][1].ToString());
                lolo.SubItems.Add(dtt.Rows[i][2].ToString());
                lolo.SubItems.Add(dtt.Rows[i][3].ToString());
                lolo.SubItems.Add(dtt.Rows[i][4].ToString());
                listView1.Items.Add(lolo);
            }
            textBox4.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult sor = MessageBox.Show("Stok Kartını Kaydetmek İstiyor musunuz. ?", "Kaydet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sor == DialogResult.Yes)
            {
                EStok stokekle = new EStok();
                stokekle._stokkodu = textBox1.Text;
                stokekle._stokadi = textBox2.Text;
                stokekle._stokaciklama = textBox3.Text;
                stokekle._bakiye = Convert.ToDouble(textBox4.Text);
                FStok.Sekle(stokekle);
                Temizle();
                ListemiGetir();
            }
        }

        private void StokKarti_Load(object sender, EventArgs e)
        {
            ListemiGetir();
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DialogResult dr = MessageBox.Show("Lütfen Üretimi Tamamlanmış Olan Ürünlerin Miktarını Giriniz.Diğer Stokların Miktar Girişini Satınalma Modülünde Yapınız.Aksi Halde Program Envanteri ile Fiili Envanter Arasında Fark Oluşabilir.Devam Etmek İstiyor musunuz ?", "Dikkat !!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
    }
}