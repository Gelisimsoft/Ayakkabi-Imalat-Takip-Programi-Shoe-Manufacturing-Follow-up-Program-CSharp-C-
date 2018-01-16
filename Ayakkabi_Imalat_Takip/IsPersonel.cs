using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IsPersonel : Form
    {
        public IsPersonel()
        {
            InitializeComponent();
        }

        private int P1, P2, P3, P4, TID;
        private DataTable dr;
        private double ucret1, ucret2, ucret3, ucret4;
        private string per, takipcix;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Temizle()
        {
            foreach (Control fatih in this.groupBox6.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                prsnl1.SelectedIndex = 0;
                prsnl2.SelectedIndex = 0;
                prsnl3.SelectedIndex = 0;
                prsnl4.SelectedIndex = 0;
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Personel1Come()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl1.DataSource = dt;
            prsnl1.DisplayMember = "adsoyad";
            prsnl1.ValueMember = "personelID";
        }

        private void Personel2Come()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl2.DataSource = dt;
            prsnl2.DisplayMember = "adsoyad";
            prsnl2.ValueMember = "personelID";
        }

        private void Personel3Come()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl3.DataSource = dt;
            prsnl3.DisplayMember = "adsoyad";
            prsnl3.ValueMember = "personelID";
        }

        private void Personel4Come()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl4.DataSource = dt;
            prsnl4.DisplayMember = "adsoyad";
            prsnl4.ValueMember = "personelID";
        }

        private void IsPersonel_Load(object sender, EventArgs e)
        {
            Personel1Come();
            Personel2Come();
            Personel3Come();
            Personel4Come();
            Getir();
            p1ucretxt.Text = "0";
            p2ucretxt.Text = "0";
            p3ucretxt.Text = "0";
            p4ucretxt.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (prsnl1.SelectedIndex == 0 || prsnl2.SelectedIndex == 0 || prsnl3.SelectedIndex == 0 || prsnl4.SelectedIndex == 0)
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
            if (prsnl1.SelectedIndex == 0 || prsnl2.SelectedIndex == 0 || prsnl3.SelectedIndex == 0 || prsnl4.SelectedIndex == 0 || takipnotxt.Text == "")
            {
                MessageBox.Show("Lütfen Aşağıdaki Hataları Kontrol Ediniz.\r" + per + "\r" + takipcix + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show("Personel Ücretlerini Kaydetmek İstediğinize Eminin misiniz. ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    P1 = Convert.ToInt32(prsnl1.SelectedValue);
                    P2 = Convert.ToInt32(prsnl2.SelectedValue);
                    P3 = Convert.ToInt32(prsnl3.SelectedValue);
                    P4 = Convert.ToInt32(prsnl4.SelectedValue);
                    ucret1 = Convert.ToDouble(p1ucretxt.Text);
                    ucret2 = Convert.ToDouble(p2ucretxt.Text);
                    ucret3 = Convert.ToDouble(p3ucretxt.Text);
                    ucret4 = Convert.ToDouble(p4ucretxt.Text);
                    Ekle(P1, ucret1);
                    Ekle(P2, ucret2);
                    Ekle(P3, ucret3);
                    Ekle(P4, ucret4);
                    MessageBox.Show("Personel Ücretlendirilme İşlemi Yapılmıştır.", "Ücretlendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Temizle();
                    Getir();
                }
            }
        }

        private void Ekle(int ID, double ucret)
        {
            EIsPersonel IP = new EIsPersonel();
            IP.TakipID = TID;
            IP.personelID = ID;
            IP.Ucret = ucret;
            FIsPersonel.Ekle(IP);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            takipgrp.Visible = true;
            button4.Visible = false;
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
    }
}