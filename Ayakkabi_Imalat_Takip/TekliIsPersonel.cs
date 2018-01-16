using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class TekliIsPersonel : Form
    {
        public TekliIsPersonel()
        {
            InitializeComponent();
        }

        private int TID;
        private DataTable dr;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private string takipcix, per;

        private void Temizle()
        {
            foreach (Control fatih in this.groupBox6.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                prsnl.SelectedIndex = 0;
                takipnotxt.Visible = false;
                button4.Visible = true;
            }
        }

        private void Getir()
        {
            listView1.Items.Clear();
            FIsPersonel Isp = new FIsPersonel();
            DataTable det = FIsPersonel.Ekstre();
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["ucret"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["TakipNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["FisNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void Personel1Come()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl.DataSource = dt;
            prsnl.DisplayMember = "adsoyad";
            prsnl.ValueMember = "personelID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (prsnl.SelectedIndex == 0)
            {
                per = "- Lütfen Personel Seçimi Yapınız.";
            }
            else
            {
                per = "";
            }
            if (takipnotxt.Text == "")
            {
                takipcix = "- Lütfen Takip No Seçiniz.";
            }
            else
            {
                takipcix = "";
            }
            if (prsnl.SelectedIndex == 0 || takipnotxt.Text == "")
            {
                MessageBox.Show("Lütfen Aşağıdaki Hataları Kontrol Ediniz.\r" + per + "\r" + takipcix + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show("Personel Ücretini Kaydetmek İstediğinize Eminin misiniz. ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    EIsPersonel IP = new EIsPersonel();
                    IP.TakipID = TID;
                    IP.personelID = Convert.ToInt32(prsnl.SelectedValue);
                    IP.Ucret = Convert.ToDouble(ucretxt.Text);
                    FIsPersonel.Ekle(IP);
                    MessageBox.Show("Personel Ücretlendirme İşlemi Yapılmıştır.", "Ücretlendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Temizle();
                    Getir();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            takipgrp.Visible = true;
            button4.Visible = false;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            object gel = listBox1.SelectedItem;
            takipnotxt.Text = listBox1.GetItemText(gel).ToString();
            TID = Convert.ToInt32(listBox1.SelectedValue);
            takipnotxt.Visible = true;
            takipgrp.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }

        private void TekliIsPersonel_Load(object sender, EventArgs e)
        {
            Personel1Come();
            Getir();
            ucretxt.Text = "0";
        }

        private void tnotxt_Leave(object sender, EventArgs e)
        {
            OleDbCommand TakipNocom = new OleDbCommand("select * from IsTakipForm WHERE (((IsTakipForm.IsActive)=True) AND ((IsTakipForm.TakipNo) like @no))", con);
            TakipNocom.Parameters.AddWithValue("@no", tnotxt.Text + "%");
            OleDbDataAdapter da = new OleDbDataAdapter(TakipNocom);
            dr = new DataTable();
            da.Fill(dr);
            if (TakipNocom.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (dr.Rows.Count != 0)
            {
                listBox1.DataSource = dr;
                listBox1.DisplayMember = "TakipNo";
                listBox1.ValueMember = "TakipID";
                tnotxt.Clear();
            }
            else
            {
                MessageBox.Show(tnotxt.Text + " Numaralı Takip Numarası Bulunamamıştır.!!!", "Takip No", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tnotxt.Clear();
            }
            con.Close();
            dr.Clone();
        }
    }
}