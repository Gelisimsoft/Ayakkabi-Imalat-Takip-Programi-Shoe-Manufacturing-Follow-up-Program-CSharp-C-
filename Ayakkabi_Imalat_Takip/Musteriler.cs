using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Musteriler_Load(object sender, EventArgs e)
        {
            CustomersCame();
            Listemidoldur();
        }

        private void CustomersCame()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True)", con);
            DataTable dtt = new DataTable();
            dap.Fill(dtt);
            DataRow dr = dtt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dtt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dtt;
            comboBox1.ValueMember = "musteriID";
            comboBox1.DisplayMember = "unvan";
        }

        private double tutarimiz;

        public void Listemidoldur()
        {
            listView1.Items.Clear();
            FMusteri musterim = new FMusteri();
            DataTable det = FMusteri.MKayitlariGetir();
            for (int i = 0; i < det.Rows.Count; i++)
            {
                if (det.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                {
                    tutarimiz = 0;
                }
                else
                {
                    tutarimiz = Convert.ToDouble(det.Rows[i]["bakiye"].ToString());
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri için Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                EMusteri musterimByMusteri = new EMusteri();
                musterimByMusteri.MusteriID = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable det = FMusteri.MKayitGetirByID(musterimByMusteri);
                if (det.Rows.Count != 0)
                {
                    for (int i = 0; i < det.Rows.Count; i++)
                    {
                        if (det.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                        {
                            tutarimiz = 0;
                        }
                        else
                        {
                            tutarimiz = Convert.ToDouble(det.Rows[i]["bakiye"].ToString());
                        }
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
                else
                {
                    MessageBox.Show("İlgili Müşteri Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listemidoldur();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            OleDbDataAdapter daparama = new OleDbDataAdapter("SELECT musteriler.unvan, musteriler.adres, musteriler.sehir, musteriler.telefon, musteriler.faks, musteriler.vdairesi, musteriler.vno, musteriler.bakiye, musteriler.musteriID FROM musteriler WHERE(((musteriler.IsActive)=True) AND ((musteriler.unvan) like @unvan))", con);
            daparama.SelectCommand.Parameters.AddWithValue("@unvan", textBox1.Text + "%");
            DataTable det = new DataTable();
            daparama.Fill(det);
            if (det.Rows.Count != 0)
            {
                if (det != null)
                {
                    for (int i = 0; i < det.Rows.Count; i++)
                    {
                        if (det.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                        {
                            tutarimiz = 0;
                        }
                        else
                        {
                            tutarimiz = Convert.ToDouble(det.Rows[i]["bakiye"].ToString());
                        }
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
            }
            else
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " Ünvanın'da Müşteri Bulunamamıştır. !");
                Listemidoldur();
            }
        }
    }
}