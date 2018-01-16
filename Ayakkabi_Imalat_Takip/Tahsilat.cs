using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Tahsilat : Form
    {
        public Tahsilat()
        {
            InitializeComponent();
        }

        private void ListviewDoldur()
        {
            listView1.Items.Clear();
            DataTable dt = FTahsilat.TEkstre();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(dt.Rows[i]["tutar"].ToString());
                string tutar = String.Format("{0:C}", tutarimiz);
                ListViewItem tahsilato = new ListViewItem(dt.Rows[i]["mkzno"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["tarih"].ToString().Remove(11));
                tahsilato.SubItems.Add(tutar);
                tahsilato.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["aciklama"].ToString());
                listView1.Items.Add(tahsilato);
            }
        }

        private void TimizleUsagum()
        {
            foreach (Control Abdurahman in this.Controls)
            {
                if (Abdurahman is TextBox)
                {
                    Abdurahman.Text = string.Empty;
                }
            }
            acklama.Text = string.Empty;
            mstri.SelectedIndex = 0;
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Tahsilat_Load(object sender, EventArgs e)
        {
            MusteriGetir();
            ListviewDoldur();
            mkbzno.Text = Convert.ToString(0);
            tutar.Text = Convert.ToString(0);
        }

        private void MusteriGetir()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr[1] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            mstri.DataSource = dt;
            mstri.DisplayMember = "unvan";
            mstri.ValueMember = "musteriID";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mstri.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soryigen = MessageBox.Show("Tahsilat Makbuzunu Kaydetmek İstiyor musunuz ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (soryigen == DialogResult.Yes)
                {
                    ETahsilat ekle = new ETahsilat();
                    ekle.MusteriID = Convert.ToInt32(mstri.SelectedValue);
                    ekle.Mzkno = mkbzno.Text;
                    ekle.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    ekle.Tutar = Convert.ToDouble(tutar.Text);
                    ekle.Aciklama = acklama.Text;
                    FTahsilat.TEkle(ekle);
                    TimizleUsagum();
                    ListviewDoldur();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            TimizleUsagum();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}