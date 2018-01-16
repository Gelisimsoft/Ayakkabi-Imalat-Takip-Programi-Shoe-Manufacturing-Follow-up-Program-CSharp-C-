using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Personeller : Form
    {
        public Personeller()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void departman()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select departmanID,adi from departman", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr[1] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "departmanID";
            comboBox1.DisplayMember = "adi";
        }

        private double tutarimiz;

        public void Listemidoldur()
        {
            DataTable pdet = Fpersonel.Pekstre();
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                if (pdet.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                {
                    tutarimiz = 0;
                }
                else
                {
                    tutarimiz = Convert.ToDouble(pdet.Rows[i]["bakiye"].ToString());
                }
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["adi"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["tcno"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["adres"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["ceptlf"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["evtlf"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["isegiris"].ToString().Remove(11));
                listecik.SubItems.Add(pdet.Rows[i]["cikisdate"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void Personeller_Load(object sender, EventArgs e)
        {
            departman();
            Listemidoldur();
        }

        private void dprtman_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Departman İçin Bir Bölüm Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                Epersonel persi = new Epersonel();
                persi._departman = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable pdet = Fpersonel.PDeparmant(persi);
                if (pdet.Rows.Count != 0)
                {
                    for (int i = 0; i < pdet.Rows.Count; i++)
                    {
                        if (pdet.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                        {
                            tutarimiz = 0;
                        }
                        else
                        {
                            tutarimiz = Convert.ToDouble(pdet.Rows[i]["bakiye"].ToString());
                        }
                        string tutar = string.Format("{0:C}", tutarimiz);
                        ListViewItem listecik = new ListViewItem(pdet.Rows[i]["adi"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["adsoyad"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["tcno"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["adres"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["ceptlf"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["evtlf"].ToString());
                        listecik.SubItems.Add(pdet.Rows[i]["isegiris"].ToString().Remove(11));
                        listecik.SubItems.Add(pdet.Rows[i]["cikisdate"].ToString());
                        listecik.SubItems.Add(tutar);
                        listView1.Items.Add(listecik);
                    }
                }
                else
                {
                    MessageBox.Show("İlgili Personel Hakkında Kayıt Bulunamamıştır !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Listemidoldur();
                }
            }
        }

        private void AdSoyad()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select * from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.adsoyad) like @ad))", con);
            da.SelectCommand.Parameters.AddWithValue("@ad", adsyod.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "adsoyad";
                listBox1.ValueMember = "personelID";
                tckimlik.Enabled = true;
            }
            else
            {
                MessageBox.Show(" " + adsyod.Text + " " + "İsimli Personel Bulunamamıştır !");
            }
            adsyod.Text = string.Empty;
            tckimlik.Enabled = true;
        }

        private void Tckimlik()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select * from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.tcno) like @no))", con);
            da.SelectCommand.Parameters.AddWithValue("@no", tckimlik.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "tcno";
                listBox1.ValueMember = "personelID";
                adsyod.Enabled = true;
            }
            else
            {
                MessageBox.Show(" " + tckimlik.Text + " " + "Numaralı Tckimlik Numarası  Bulunamamıştır !");
            }
            tckimlik.Text = string.Empty;
            adsyod.Enabled = true;
        }

        private string hangisi;

        private void bilgigetir_Click_1(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            switch (hangisi)
            {
                case "adsoyad":
                    AdSoyad();
                    break;

                case "tcno":
                    Tckimlik();
                    break;
            }
        }

        private void adsyod_MouseClick(object sender, MouseEventArgs e)
        {
            hangisi = "adsoyad";
            tckimlik.Enabled = false;
        }

        private void tckimlik_MouseClick(object sender, MouseEventArgs e)
        {
            hangisi = "tcno";
            adsyod.Enabled = false;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView1.Items.Clear();
            Epersonel person = new Epersonel();
            person._personelID = Convert.ToInt32(listBox1.SelectedValue);
            DataTable pdet = Fpersonel.PersonelID(person);
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                if (pdet.Rows[i]["bakiye"].ToString() == DBNull.Value.ToString())
                {
                    tutarimiz = 0;
                }
                else
                {
                    tutarimiz = Convert.ToDouble(pdet.Rows[i]["bakiye"].ToString());
                }
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["adi"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["tcno"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["adres"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["ceptlf"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["evtlf"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["isegiris"].ToString().Remove(11));
                listecik.SubItems.Add(pdet.Rows[i]["cikisdate"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }
    }
}