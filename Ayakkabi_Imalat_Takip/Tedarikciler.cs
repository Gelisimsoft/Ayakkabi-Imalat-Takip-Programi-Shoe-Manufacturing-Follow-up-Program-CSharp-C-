using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Tedarikciler : Form
    {
        public Tedarikciler()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private double tutarimiz;

        private void SellersCame()
        {
            OleDbDataAdapter Tdap = new OleDbDataAdapter("select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", con);
            DataTable Tdtt = new DataTable();
            Tdap.Fill(Tdtt);
            DataRow dr = Tdtt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            Tdtt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = Tdtt;
            comboBox1.ValueMember = "TedarikciID";
            comboBox1.DisplayMember = "unvan";
        }

        public void Listemidoldur()
        {
            listView1.Items.Clear();
            DataTable Tdet = FTedarikci.TKayitlariGetir();
            for (int i = 0; i < Tdet.Rows.Count; i++)
            {
                if (Tdet.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                {
                    tutarimiz = 0;
                }
                else
                {
                    tutarimiz = Convert.ToDouble(Tdet.Rows[i]["bakiye"].ToString());
                }
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

        private void Tedarikciler_Load(object sender, EventArgs e)
        {
            SellersCame();
            Listemidoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Tedarikci İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                ETedarikci ted = new ETedarikci();
                ted._TedarikciID = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable det = FTedarikci.TKayitGetirByID(ted);
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
                    listecik.SubItems.Add(det.Rows[i]["vergidairesi"].ToString());
                    listecik.SubItems.Add(det.Rows[i]["vergino"].ToString());
                    listecik.SubItems.Add(tutar);
                    listView1.Items.Add(listecik);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            OleDbDataAdapter daparama = new OleDbDataAdapter("SELECT tedarikciler.unvan, tedarikciler.adres, tedarikciler.sehir, tedarikciler.telefon, tedarikciler.faks, tedarikciler.vergidairesi, tedarikciler.vergino, tedarikciler.bakiye FROM tedarikciler WHERE(((tedarikciler.IsActive)=True) AND ((tedarikciler.unvan) like @unvan))", con);
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
                        listecik.SubItems.Add(det.Rows[i]["vergidairesi"].ToString());
                        listecik.SubItems.Add(det.Rows[i]["vergino"].ToString());
                        listecik.SubItems.Add(tutar);
                        listView1.Items.Add(listecik);
                    }
                }
            }
            else
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " Ünvanın'da Tedarikci Bulunamamıştır. !");
                Listemidoldur();
            }
        }
    }
}