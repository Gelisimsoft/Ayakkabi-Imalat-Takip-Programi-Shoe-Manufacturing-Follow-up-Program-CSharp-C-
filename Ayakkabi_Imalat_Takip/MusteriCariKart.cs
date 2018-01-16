using EntityKatmani;
using System;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class MusteriCariKart : Form
    {
        public void Listemidoldur()
        {
            listView1.Items.Clear();
            FMusteri musterim = new FMusteri();
            DataTable det = FMusteri.MKayitlariGetir();
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["bakiye"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["unvan"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adres"].ToString());
                listecik.SubItems.Add(det.Rows[i]["sehir"].ToString());
                listecik.SubItems.Add(det.Rows[i]["telefon"].ToString());
                listecik.SubItems.Add(det.Rows[i]["faks"].ToString());
                listecik.SubItems.Add(det.Rows[i]["vdairesi"].ToString());
                listecik.SubItems.Add(det.Rows[i]["vno"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        public MusteriCariKart()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Temizle()
        {
            sehir.Text = string.Empty;
            unvantxt.Text = string.Empty;
            vdairesitxt.Text = string.Empty;
            vnotxt.Text = string.Empty;
            adres.Text = string.Empty;
            tlf.Text = string.Empty;
            fax.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult sor = MessageBox.Show("Cari Kartı Kaydetmek İstediğinize Emin misiniz ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sor == DialogResult.Yes)
            {
                EMusteri musterim = new EMusteri();
                musterim._adres = adres.Text;
                musterim._faks = fax.Text;
                musterim._sehir = sehir.Text;
                musterim._telefon = tlf.Text;
                musterim._unvan = unvantxt.Text;
                musterim._vergidairesi = vdairesitxt.Text;
                musterim._vergino = vnotxt.Text;
                FMusteri.MKayiTEkle(musterim);
                Temizle();
                Listemidoldur();
            }
        }

        private void MusteriCariKart_Load(object sender, EventArgs e)
        {
            Listemidoldur();
        }
    }
}