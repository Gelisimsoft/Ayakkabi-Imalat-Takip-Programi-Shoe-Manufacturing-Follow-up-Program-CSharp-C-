using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IsTakipFormlariRaporlama : Form
    {
        public IsTakipFormlariRaporlama()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void ListeleriGetir()
        {
            listView1.Items.Clear();
            DataTable dt = FIsTakip.IsTakipEkstre();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem listeler = new ListViewItem(dt.Rows[i]["Tarih"].ToString().Remove(11));
                listeler.SubItems.Add(dt.Rows[i]["TakipNo"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["FisNo"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Kalip"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Okce"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Platfotm"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Garni"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Cift"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Asorti"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Renk"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["Kalite"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["kesimci"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["temizleme"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["kalipci"].ToString());
                listeler.SubItems.Add(dt.Rows[i]["montaj"].ToString());
                listView1.Items.Add(listeler);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                EIsTakip istakiplerno = new EIsTakip();
                istakiplerno.MusteriID = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable dt = FIsTakip.IsTakipEkstreMusteri(istakiplerno);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem listeler = new ListViewItem(dt.Rows[i]["Tarih"].ToString().Remove(11));
                        listeler.SubItems.Add(dt.Rows[i]["TakipNo"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["FisNo"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Kalip"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Okce"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Platfotm"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Garni"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Cift"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Asorti"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Renk"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["Kalite"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["kesimci"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["temizleme"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["kalipci"].ToString());
                        listeler.SubItems.Add(dt.Rows[i]["montaj"].ToString());
                        listView1.Items.Add(listeler);
                    }
                }
                else
                {
                    MessageBox.Show("İlgili Müşteri Hakkında Kayıt Bulunamamıştır. !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListeleriGetir();
                }
            }
        }

        private void IsTakipFormlariRaporlama_Load(object sender, EventArgs e)
        {
            ListeleriGetir();
            musterileriGetir();
        }

        private void musterileriGetir()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select unvan,musteriID from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "unvan";
            comboBox1.ValueMember = "musteriID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            EIsTakip istakiplerno = new EIsTakip();
            istakiplerno.TakipNo = textBox1.Text;
            DataTable dt = FIsTakip.IsTakipEkstreByNo(istakiplerno);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem listeler = new ListViewItem(dt.Rows[i]["Tarih"].ToString().Remove(11));
                    listeler.SubItems.Add(dt.Rows[i]["TakipNo"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["FisNo"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Kalip"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Okce"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Platfotm"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Garni"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Cift"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Asorti"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Renk"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Kalite"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["kesimci"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["temizleme"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["kalipci"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["montaj"].ToString());
                    listView1.Items.Add(listeler);
                }
            }
            else
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " " + "İş Takip Numaralı İş Takip Formu Bulunamamıştır. !");
                ListeleriGetir();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            EIsTakip istakiplerno = new EIsTakip();
            istakiplerno.FisNo = textBox2.Text;
            DataTable dt = FIsTakip.IsTakipEkstreByFisno(istakiplerno);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem listeler = new ListViewItem(dt.Rows[i]["Tarih"].ToString().Remove(11));
                    listeler.SubItems.Add(dt.Rows[i]["TakipNo"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["FisNo"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Kalip"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Okce"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Platfotm"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Garni"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Cift"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Asorti"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Renk"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["Kalite"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["kesimci"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["temizleme"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["kalipci"].ToString());
                    listeler.SubItems.Add(dt.Rows[i]["montaj"].ToString());
                    listView1.Items.Add(listeler);
                }
            }
            else
            {
                MessageBox.Show("'" + textBox2.Text + "'" + " " + "Fiş Numaralı İş Takip Formu Bulunamamıştır. !");
                ListeleriGetir();
            }
        }
    }
}