using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class SatinalmaEkstresi : Form
    {
        public SatinalmaEkstresi()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Tedarikci()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "TedarikciID";
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
            listView1.Items.Clear();
            DataTable pdet = FSatinalma.Sekstre();
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                double tutarcik = Convert.ToDouble(pdet.Rows[i]["tutar"].ToString());
                double kdvcik = Convert.ToDouble(pdet.Rows[i]["kdv"].ToString());
                double genelcik = Convert.ToDouble(pdet.Rows[i]["geneltoplam"].ToString());
                string tutar = String.Format("{0:C}", tutarcik);
                string kdv = String.Format("{0:C}", kdvcik);
                string geneltutar = String.Format("{0:C}", genelcik);
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["alistarih"].ToString().Remove(11));
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

        private void SatinalmaEkstresi_Load(object sender, EventArgs e)
        {
            Tedarikci();
            Stoklar();
            Listemidoldur();
            ForAllSatinalma();
            ForAllInvoice();
            ForAllOdeme();
            Balance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Tedarikci İçin Bir Firmayı Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                ESatinalma satn = new ESatinalma();
                satn._TedarikciID = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable pdet = FSatinalma.Tedekstre(satn);
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
                        ListViewItem listecik = new ListViewItem(pdet.Rows[i]["alistarih"].ToString().Remove(11));
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
                    MessageBox.Show("İlgili Cari Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listemidoldur();
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
                MessageBox.Show("Lütfen Stok Kartını Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                textBox1.Enabled = true;
                ESatinalma sat = new ESatinalma();
                sat._stokid = Convert.ToInt32(comboBox2.SelectedValue);
                DataTable pdet = FSatinalma.StokEstre(sat);
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
                        ListViewItem listecik = new ListViewItem(pdet.Rows[i]["alistarih"].ToString().Remove(11));
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
                    MessageBox.Show("İlgili Stok Hakkında Kayıt Bulunamamıştır !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listemidoldur();
                }
            }
            label4.ForeColor = Color.Black;
            label2.Text = "0,00 TL";
            label3.Text = "0,00 TL";
            label4.Text = "0,00 TL";
            label11.Text = "0";
        }

        private void faturagetir()
        {
            listView1.Items.Clear();
            comboBox2.Enabled = true;
            ESatinalma sat = new ESatinalma();
            sat._ftno = textBox1.Text;
            DataTable pdet = FSatinalma.FtnoEstre(sat);
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
                    ListViewItem listecik = new ListViewItem(pdet.Rows[i]["alistarih"].ToString().Remove(11));
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
                MessageBox.Show("'" + textBox1.Text + "'" + " Numaralı Fatura Hakkında Kayıt Bulunamamıştır !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Listemidoldur();
            }
            label4.ForeColor = Color.Black;
            textBox1.Text = string.Empty;
            label2.Text = "0,00 TL";
            label3.Text = "0,00 TL";
            label4.Text = "0,00 TL";
            label11.Text = "0";
        }

        //Şimdi form açıldığı zaman gelecek listede bulunan bütün faturaların geneltoplam tutarlarının toplamını alıcak methods yazicaz.
        private double gelensatinalma, gelenodeme, gelensatinalmaT, gelenodemeT;

        private object gelen = 0;

        private void ForAllSatinalma()
        {
            OleDbCommand comAll = new OleDbCommand("select Sum(geneltoplam) from satinalma", con);
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelensatinalma = Convert.ToDouble(comAll.ExecuteScalar());
                label2.Text = String.Format("{0:C}", gelensatinalma);
            }
            con.Close();
        }

        private void ForAllOdeme()
        {
            OleDbCommand comAll = new OleDbCommand("select Sum(tutar) from Odemeler", con);
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
            OleDbCommand comAll = new OleDbCommand("select Count(ftno) from satinalma", con);
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

        private void Balance()
        {
            double kalan = gelensatinalma - gelenodeme;
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
            OleDbCommand comAll = new OleDbCommand("select Sum(geneltoplam) from satinalma WHERE ((satinalma.TedarikciID)=@ID)", con);
            comAll.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelensatinalmaT = Convert.ToDouble(comAll.ExecuteScalar());
                label2.Text = String.Format("{0:C}", gelensatinalmaT);
            }
            con.Close();
        }

        private void ForAllOdemeT()
        {
            OleDbCommand comAll = new OleDbCommand("select Sum(tutar) from Odemeler WHERE ((Odemeler.TedarikciID)=@ID)", con);
            comAll.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (comAll.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (comAll.ExecuteScalar() != gelen && comAll.ExecuteScalar() != DBNull.Value)
            {
                gelenodemeT = Convert.ToDouble(comAll.ExecuteScalar());
                label3.Text = String.Format("{0:C}", gelenodemeT);
            }
            con.Close();
        }

        private void ForAllInvoiceT()
        {
            OleDbCommand comAll = new OleDbCommand("select Count(ftno) from satinalma WHERE ((satinalma.TedarikciID)=@ID)", con);
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
        }

        private void BalanceT()
        {
            double kalanT = gelensatinalmaT - gelenodemeT;
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
    }
}