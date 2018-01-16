using EntityKatmani;
using System;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class TedarikciCariKarti : Form
    {
        public TedarikciCariKarti()
        {
            InitializeComponent();
        }

        public void Listemidoldur()
        {
            listView1.Items.Clear();
            DataTable Tdet = FTedarikci.TKayitlariGetir();
            for (int i = 0; i < Tdet.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(Tdet.Rows[i]["bakiye"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(Tdet.Rows[i]["unvan"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["adres"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["sehir"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["telefon"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["faks"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["vergidairesi"].ToString());
                listecik.SubItems.Add(Tdet.Rows[i]["vergino"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void Temizle()
        {
            foreach (Control fatih in groupBox1.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                adrestxt.Clear();
                tlftxt.Clear();
                fakstxt.Clear();
            }
        }

        private void TedarikciCariKarti_Load(object sender, EventArgs e)
        {
            Listemidoldur();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult sor = MessageBox.Show("Cari Kartı Kaydetmek İstediğinize Emin misiniz. ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sor == DialogResult.Yes)
            {
                ETedarikci saticim = new ETedarikci();
                saticim._adres = adrestxt.Text;
                saticim._faks = fakstxt.Text;
                saticim._sehir = sehirtxt.Text;
                saticim._telefon = tlftxt.Text;
                saticim._unvan = unvantxt.Text;
                saticim._vergidairesi = vdairesitxt.Text;
                saticim._vergino = vnotxt.Text;
                FTedarikci.TKayiTEkle(saticim);
                Temizle();
                Listemidoldur();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}