using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private string musterimiz, stoklarimiz;

        public void Listemidoldur()
        {
            listView1.Items.Clear();
            DataTable pdet = FSatis.Sekstre();
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                double tutarcik = Convert.ToDouble(pdet.Rows[i]["tutar"].ToString());
                double kdvcik = Convert.ToDouble(pdet.Rows[i]["kdv"].ToString());
                double genelcik = Convert.ToDouble(pdet.Rows[i]["geneltoplam"].ToString());
                string tutar = String.Format("{0:C}", tutarcik);
                string kdv = String.Format("{0:C}", kdvcik);
                string geneltutar = String.Format("{0:C}", genelcik);
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["satistarih"].ToString().Remove(11));
                listecik.SubItems.Add(pdet.Rows[i]["ftno"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["unvan"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["stokadi"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["birim"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["miktar"].ToString());
                listecik.SubItems.Add(tutar);
                listecik.SubItems.Add(kdv);
                listecik.SubItems.Add(geneltutar);
                listecik.SubItems.Add(pdet.Rows[i]["aciklama"].ToString());
                listView1.Items.Add(listecik);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Temizle()
        {
            foreach (Control fatih in this.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                mstri.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void musteriiGetir()
        {
            OleDbDataAdapter detda = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by musteriler.unvan", con);
            DataTable teddt = new DataTable();
            detda.Fill(teddt);
            DataRow dr = teddt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            teddt.Rows.InsertAt(dr, 0);
            mstri.DataSource = teddt;
            mstri.ValueMember = "musteriID";
            mstri.DisplayMember = "unvan";
        }

        private void StoklarGetir()
        {
            OleDbDataAdapter stoda = new OleDbDataAdapter("select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stoklar.stokkodu", con);
            DataTable stodt = new DataTable();
            stoda.Fill(stodt);
            DataRow dr = stodt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz";
            stodt.Rows.InsertAt(dr, 0);
            stokkrti.DataSource = stodt;
            stokkrti.ValueMember = "stokid";
            stokkrti.DisplayMember = "stokadi";
        }

        private void Satis_Load(object sender, EventArgs e)
        {
            Listemidoldur();
            musteriiGetir();
            StoklarGetir();
            mstri.SelectedIndex = 0;
            stokkrti.SelectedIndex = 0;
            miktar.Text = Convert.ToString(0);
            gnltoplam.Text = Convert.ToString(0);
            kdv.Text = Convert.ToString(0);
            tutar.Text = Convert.ToString(0);
            ftno.Text = Convert.ToString(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mstri.SelectedIndex == 0)
            {
                musterimiz = "- Müşteri Firma Seçimini Yapınız.";
            }
            else
            {
                musterimiz = "";
            }
            if (stokkrti.SelectedIndex == 0)
            {
                stoklarimiz = "- Stok Kartı Seçimini Yapınız.";
            }
            else
            {
                stoklarimiz = "";
            }

            if (mstri.SelectedIndex == 0 || stokkrti.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Aşağıdaki Alanları Doldurunuz.\r" + musterimiz + "\r" + stoklarimiz + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soryigen = MessageBox.Show("Satış İşlemini Kaydetmek İstiyor musunuz. ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soryigen == DialogResult.Yes)
                {
                    ESatis sattikmal = new ESatis();
                    sattikmal._ftno = ftno.Text;
                    sattikmal._birim = birim.Text;
                    sattikmal._miktar = Convert.ToDouble(miktar.Text);
                    sattikmal._tutar = Convert.ToDouble(tutar.Text);
                    sattikmal._kdv = Convert.ToDouble(kdv.Text);
                    sattikmal._geneltoplam = Convert.ToDouble(gnltoplam.Text);
                    sattikmal._aciklama = acklama.Text;
                    sattikmal._musteriID = Convert.ToInt32(mstri.SelectedValue);
                    sattikmal._stokid = Convert.ToInt32(stokkrti.SelectedValue);
                    sattikmal._satistarih = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    FSatis.Sekle(sattikmal);
                    Temizle();
                    Listemidoldur();
                }
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