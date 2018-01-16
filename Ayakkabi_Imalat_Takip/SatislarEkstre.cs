using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class SatislarEkstre : Form
    {
        public SatislarEkstre()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void SatislarEkstre_Load(object sender, EventArgs e)
        {
            Stoklar();
            Listemidoldur();
            ForAllSatinalma();
            ForAllInvoice();
            ForAllOdeme();
            Balance();
            Musteri();
        }

        private void Musteri()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "musteriID";
            comboBox1.DisplayMember = "unvan";
        }

        private void Stoklar()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "stokid";
            comboBox2.DisplayMember = "stokadi";
        }

        public void Listemidoldur()
        {
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

        private double gelensatis, gelenodeme, gelensatinalmaM, gelenodemeM;

        private void ForAllSatinalma()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Sum(geneltoplam) from satislar", con);
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelensatis = Convert.ToDouble(comAll.ExecuteScalar());
                label2.Text = String.Format("{0:C}", gelensatis);
            }
            con.Close();
        }

        private void ForAllOdeme()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Sum(tutar) from Tahsilatlar", con);
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelenodeme = Convert.ToDouble(comAll.ExecuteScalar());
                label3.Text = String.Format("{0:C}", gelenodeme);
            }
            con.Close();
        }

        private void ForAllInvoice()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Count(ftno) from satislar", con);
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                double gelelerft = Convert.ToDouble(comAll.ExecuteScalar());
                label11.Text = Convert.ToString(gelelerft);
            }
            con.Close();
        }

        private void Balance()
        {
            double kalan = gelensatis - gelenodeme;
            if (kalan < 0)
            {
                kalantutar.Text = "Fazla Yapılan Ödeme";
                label4.ForeColor = Color.Red;
                label4.Text = String.Format("{0:C}", kalan);
            }
            else
            {
                kalantutar.Text = "Kalan Tutar";
                label4.ForeColor = Color.Black;
                label4.Text = String.Format("{0:C}", kalan);
            }
        }

        private void ForAllSatinalmaT()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Sum(geneltoplam) from satislar WHERE ((satislar.musteriID)=@ID)", con);
            comAll.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelensatinalmaM = Convert.ToDouble(comAll.ExecuteScalar());
                label2.Text = String.Format("{0:C}", gelensatinalmaM);
            }
            con.Close();
        }

        private void ForAllOdemeT()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Sum(tutar) from Tahsilatlar WHERE ((Tahsilatlar.musteriID)=@ID)", con);
            comAll.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelenodemeM = Convert.ToDouble(comAll.ExecuteScalar());
                label3.Text = String.Format("{0:C}", gelenodemeM);
            }
            con.Close();
        }

        private void ForAllInvoiceT()
        {
            object gelen = 0;
            OleDbCommand comAll = new OleDbCommand("select Count(ftno) from satislar WHERE ((satislar.musteriID)=@ID)", con);
            comAll.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                double gelenft = Convert.ToDouble(comAll.ExecuteScalar());
                label11.Text = Convert.ToString(gelenft);
            }
            con.Close();
        }

        private void BalanceT()
        {
            double kalanT = gelensatinalmaM - gelenodemeM;
            if (kalanT < 0)
            {
                kalantutar.Text = "Fazla Yapılan Ödeme";
                label4.ForeColor = Color.Red;
                label4.Text = String.Format("{0:C}", kalanT);
            }
            else
            {
                kalantutar.Text = "Kalan Tutar";
                label4.ForeColor = Color.Black;
                label4.Text = String.Format("{0:C}", kalanT);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                ESatis satn = new ESatis();
                satn._musteriID = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable pdet = FSatis.MEkstre(satn);
                if (pdet.Rows.Count != 0)
                {
                    ForAllInvoiceT();
                    ForAllOdemeT();
                    ForAllSatinalmaT();
                    BalanceT();
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
                else
                {
                    MessageBox.Show("İlgili Cari Hakkında Kayıt Bulunamamıştır !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label4.ForeColor = Color.Black;
                    label2.Text = "0,00 TL";
                    label3.Text = "0,00 TL";
                    label4.Text = "0,00 TL";
                    label11.Text = "0";
                }
            }
        }

        private string sonuc;

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Enabled = false;
            sonuc = "fatura";
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Enabled = false;
            sonuc = "stok";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (sonuc)
            {
                case "fatura":
                    faturagetir();
                    break;

                case "stok":
                    stoklarigetir();
                    break;
            }
        }

        private void stoklarigetir()
        {
            if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Stok Kartı Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                textBox1.Enabled = true;
                ESatis sat = new ESatis();
                sat._stokid = Convert.ToInt32(comboBox2.SelectedValue);
                DataTable pdet = FSatis.StokEstre(sat);
                if (pdet.Rows.Count != 0)
                {
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
                else
                {
                    MessageBox.Show("İlgili Stok Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listemidoldur();
                }
                label4.ForeColor = Color.Black;
                label2.Text = "0,00 TL";
                label3.Text = "0,00 TL";
                label4.Text = "0,00 TL";
                label11.Text = "0";
            }
        }

        private void faturagetir()
        {
            listView1.Items.Clear();
            comboBox2.Enabled = true;
            ESatis sat = new ESatis();
            sat._ftno = textBox1.Text;
            DataTable pdet = FSatis.FtnoEstre(sat);
            if (pdet.Rows.Count != 0)
            {
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
            else
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " Numaralı Fatura Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Listemidoldur();
            }
            label4.ForeColor = Color.Black;
            textBox1.Text = string.Empty;
            label2.Text = "0,00 TL";
            label3.Text = "0,00 TL";
            label4.Text = "0,00 TL";
            label11.Text = "0";
        }
    }
}