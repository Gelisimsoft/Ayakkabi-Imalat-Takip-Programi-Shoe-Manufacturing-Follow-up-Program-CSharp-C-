using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class TahsilatEkstre : Form
    {
        public TahsilatEkstre()
        {
            InitializeComponent();
        }

        private void TahsilatEkstre_Load(object sender, EventArgs e)
        {
            ListviewDoldur();
            MusteriBilgileriniGetir();
            Listboxdoldur();
        }

        private void Listboxdoldur()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select tahsilatID,mkzno from Tahsilatlar order by mkzno", connect.connectroad);
            DataTable dt = new DataTable();
            da.Fill(dt);
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "mkzno";
            listBox1.ValueMember = "tahsilatID";
        }

        private void MusteriBilgileriniGetir()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", connect.connectroad);
            DataTable dtt = new DataTable();
            dap.Fill(dtt);
            DataRow dr = dtt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dtt.Rows.InsertAt(dr, 0);
            mstricombo.DataSource = dtt;
            mstricombo.ValueMember = "musteriID";
            mstricombo.DisplayMember = "unvan";
        }

        private void ListviewDoldur()
        {
            listem.Items.Clear();
            DataTable dt = FTahsilat.TEkstre();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(dt.Rows[i]["tutar"].ToString());
                string tutar = String.Format("{0:C}", tutarimiz);
                //yada
                //string tutar=tutarimiz.ToString("C"); TL Olarak verir
                //string tutar=tutarimiz.ToString("N"); TL Vermez
                ListViewItem tahsilato = new ListViewItem(dt.Rows[i]["mkzno"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["tarih"].ToString().Remove(11));
                tahsilato.SubItems.Add(tutar);
                tahsilato.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["aciklama"].ToString());
                listem.Items.Add(tahsilato);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mstricombo.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listem.Items.Clear();
                ETahsilat Emusteri = new ETahsilat();
                Emusteri.MusteriID = Convert.ToInt32(mstricombo.SelectedValue);
                //FTahsilat.TEkstreByMusteri(Emusteri);
                DataTable det = FTahsilat.TEkstreByMusteri(Emusteri);

                if (det.Rows.Count != 0)
                {
                    for (int i = 0; i < det.Rows.Count; i++)
                    {
                        double tutarimiz = Convert.ToDouble(det.Rows[i]["tutar"].ToString());
                        string tutar = String.Format("{0:C}", tutarimiz);
                        ListViewItem tahsilamusteri = new ListViewItem(det.Rows[i]["mkzno"].ToString());
                        tahsilamusteri.SubItems.Add(det.Rows[i]["tarih"].ToString().Remove(11));
                        tahsilamusteri.SubItems.Add(tutar);
                        tahsilamusteri.SubItems.Add(det.Rows[i]["unvan"].ToString());
                        tahsilamusteri.SubItems.Add(det.Rows[i]["aciklama"].ToString());
                        listem.Items.Add(tahsilamusteri);
                    }
                }
                else
                {
                    MessageBox.Show("İlgili Cari Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListviewDoldur();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Tahsilatlar WHERE ((Tahsilatlar.mkzno) like @mkz)", connect.connectroad);
            da.SelectCommand.Parameters.AddWithValue("@mkz", textBox1.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "mkzno";
                listBox1.ValueMember = "tahsilatID";
                listBox1.Visible = true;
                label2.Visible = true;
            }
            else
            {
                MessageBox.Show("' " + textBox1.Text + "' " + "Numaralı Tahsilat Makbuzu Bulunamamıştır !");
                ListviewDoldur();
            }
            textBox1.Text = string.Empty;
        }

        private bool mkzum = false;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mkzum = true;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mkzum == true)
            {
                listem.Items.Clear();
            }
            ETahsilat Emusteri = new ETahsilat();
            Emusteri.TahsilatID = Convert.ToInt32(listBox1.SelectedValue);
            DataTable det = FTahsilat.TEkstreBymkzno(Emusteri);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["tutar"].ToString());
                string tutar = String.Format("{0:C}", tutarimiz);
                ListViewItem tahsilamusteri = new ListViewItem(det.Rows[i]["mkzno"].ToString());
                tahsilamusteri.SubItems.Add(det.Rows[i]["tarih"].ToString().Remove(11));
                tahsilamusteri.SubItems.Add(tutar);
                tahsilamusteri.SubItems.Add(det.Rows[i]["unvan"].ToString());
                tahsilamusteri.SubItems.Add(det.Rows[i]["aciklama"].ToString());
                listem.Items.Add(tahsilamusteri);
            }
        }
    }
}