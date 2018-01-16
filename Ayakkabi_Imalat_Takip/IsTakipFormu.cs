using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Istakipformu : Form
    {
        public Istakipformu()
        {
            InitializeComponent();
        }

        private void ListemiGetir()
        {
            listView1.Items.Clear();
            DataTable dt = new DataTable();
            dt = FIsTakip.IsTakipEkstre();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem listem = new ListViewItem(dt.Rows[i]["Tarih"].ToString().Remove(11));
                listem.SubItems.Add(dt.Rows[i]["TakipNo"].ToString());
                listem.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                listem.SubItems.Add(dt.Rows[i]["FisNo"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Kalip"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Okce"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Platfotm"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Garni"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Cift"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Asorti"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Renk"].ToString());
                listem.SubItems.Add(dt.Rows[i]["Kalite"].ToString());
                listem.SubItems.Add(dt.Rows[i]["kesimci"].ToString());
                listem.SubItems.Add(dt.Rows[i]["temizleme"].ToString());
                listem.SubItems.Add(dt.Rows[i]["kalipci"].ToString());
                listem.SubItems.Add(dt.Rows[i]["montaj"].ToString());
                listView1.Items.Add(listem);
            }
        }

        private string kesimler, temizlemeler, montajlar, kaliplar, musteriler, asorti, asortcik;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        //Ya Hacı Bumbala Elemanları Tektek Combobox doldurucaz ve daha sonra önceden oluşturmuş olduğum departman tablosundan sıra ile hangi personel karşılık geliyorsa ona göre kayıt yapıcan.
        private void DprtmnPrsnlKesim1()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=5) or ((personeller.departmanID)=1)) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl1.DataSource = dt;
            prsnl1.DisplayMember = "adsoyad";
            prsnl1.ValueMember = "personelID";
        }

        private void DprtmnPrsnlTemizleme2()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=5) or ((personeller.departmanID)=2)) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl2.DataSource = dt;
            prsnl2.DisplayMember = "adsoyad";
            prsnl2.ValueMember = "personelID";
        }

        private void DprtmnPrsnlKalip3()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=5) or ((personeller.departmanID)=3)) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl3.DataSource = dt;
            prsnl3.DisplayMember = "adsoyad";
            prsnl3.ValueMember = "personelID";
        }

        private void DprtmnPrsnlMontaj4()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=5) or ((personeller.departmanID)=4)) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl4.DataSource = dt;
            prsnl4.DisplayMember = "adsoyad";
            prsnl4.ValueMember = "personelID";
        }

        private void musteriiGetir()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            mstri.DataSource = dt;
            mstri.DisplayMember = "unvan";
            mstri.ValueMember = "musteriID";
        }

        private void Istakipformu_Load(object sender, EventArgs e)
        {
            DprtmnPrsnlKalip3();
            DprtmnPrsnlKesim1();
            DprtmnPrsnlMontaj4();
            DprtmnPrsnlTemizleme2();
            musteriiGetir();
            ListemiGetir();
        }

        private void TemizleYigen()
        {
            cift.Text = string.Empty;
            fisno.Text = string.Empty;
            garni.Text = string.Empty;
            kalip.Text = string.Empty;
            kalite.Text = string.Empty;
            okce.Text = string.Empty;
            platfotm.Text = string.Empty;
            renk.Text = string.Empty;
            takip.Text = string.Empty;
            prsnl1.SelectedIndex = 0;
            prsnl2.SelectedIndex = 0;
            prsnl3.SelectedIndex = 0;
            prsnl4.SelectedIndex = 0;
            mstri.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TemizleYigen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //kontrol ediyoruz.Asorti için değer girilmiş mi ? Girilmemişse mesagebox göstermek üzere string değerde asortcik nesnesini gönderiyoruz.
            if (asorti == null)
            {
                asortcik = "- Asorti İçin Bir Sayı Seçiniz.";
            }
            //Eğer asorti seçilmiş ise bu sefer asortcik nesnesini boş gönderiyoruz.Messagebox'ta gereksiz hata göstermesin.
            else
            {
                asortcik = "";
            }
            //Tek tek bütün combobox'ları check ediyoruz.Seçili mi ?Değil mi ?
            if (asorti == null || prsnl4.SelectedIndex == 0 || prsnl3.SelectedIndex == 0 || prsnl2.SelectedIndex == 0 || prsnl1.SelectedIndex == 0 || mstri.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Aşağıdaki Alanları Doldurunuz.\r" + musteriler + "\r" + montajlar + "\r" + kaliplar + "\r" + temizlemeler + "\r" + kesimler + "\r" + asortcik + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show(takip.Text + " " + "Nolu İş Takip Formunu Kaydetmek İstediğinize Eminin misiniz ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    EIsTakip iscim = new EIsTakip();
                    iscim.Asorti = asorti;
                    iscim.Cift = cift.Text;
                    iscim.FisNo = fisno.Text;
                    iscim.Garni = garni.Text;
                    iscim.Kalip = kalip.Text;
                    iscim.Kalite = kalite.Text;
                    iscim.MusteriID = Convert.ToInt32(mstri.SelectedValue);
                    iscim.Okce = okce.Text;
                    iscim.Personel1 = Convert.ToInt32(prsnl1.SelectedValue);
                    iscim.Personel2 = Convert.ToInt32(prsnl2.SelectedValue);
                    iscim.Personel3 = Convert.ToInt32(prsnl3.SelectedValue);
                    iscim.Personel4 = Convert.ToInt32(prsnl4.SelectedValue);
                    iscim.Platfotm = platfotm.Text;
                    iscim.Renk = renk.Text;
                    iscim.TakipNo = takip.Text;
                    iscim.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    FIsTakip.IsTakipEkle(iscim);
                    TemizleYigen();
                    ListemiGetir();
                }
            }
        }

        private void radio5_Click(object sender, EventArgs e)
        {
            asorti = "5";
        }

        private void radio6_Click(object sender, EventArgs e)
        {
            asorti = "6";
        }

        private void radio7_Click(object sender, EventArgs e)
        {
            asorti = "7";
        }

        private void radio8_Click(object sender, EventArgs e)
        {
            asorti = "8";
        }

        private void radio9_Click(object sender, EventArgs e)
        {
            asorti = "9";
        }

        private void radio0_Click(object sender, EventArgs e)
        {
            asorti = "0";
        }

        private void radio1_Click(object sender, EventArgs e)
        {
            asorti = "1";
        }

        private void radio2_Click(object sender, EventArgs e)
        {
            asorti = "2";
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            asorti = "4";
        }

        private void radio3_Click(object sender, EventArgs e)
        {
            asorti = "3";
        }

        private void mstri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mstri.SelectedIndex == 0)
            {
                musteriler = "- Müşteri İçin Bir Firma Seçiniz.";
            }
            else
            {
                musteriler = "";
            }
        }

        private void prsnl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prsnl1.SelectedIndex == 0)
            {
                kesimler = "- Kesim Elemanı İçin Bir Eleman Seçiniz.";
            }
            else
            {
                kesimler = "";
            }
        }

        private void prsnl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prsnl2.SelectedIndex == 0)
            {
                temizlemeler = "- Temizleme Elemanı İçin Bir Eleman Seçiniz.";
            }
            else
            {
                temizlemeler = "";
            }
        }

        private void prsnl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prsnl3.SelectedIndex == 0)
            {
                kaliplar = "- Kalip Elemanı İçin Bir Eleman Seçiniz.";
            }
            else
            {
                kaliplar = "";
            }
        }

        private void prsnl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prsnl4.SelectedIndex == 0)
            {
                montajlar = "- Montaj Elemanı İçin Bir Eleman Seçiniz.";
            }
            else
            {
                montajlar = "";
            }
        }
    }
}