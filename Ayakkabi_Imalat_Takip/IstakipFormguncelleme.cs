using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IstakipFormguncelleme : Form
    {
        public IstakipFormguncelleme()
        {
            InitializeComponent();
        }

        private string kesimler, temizlemeler, montajlar, kaliplar, musteriler, asorti, asortcik;
        private int takipidim, musteridim, kesimciID, temizlemeID, kalipciID, montajID;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void ListemiGetir()
        {
            OleDbCommand com = new OleDbCommand("select TakipID,Tarih,TakipNo,unvan,Fisno,Kalip,Okce,Platfotm,Garni,Cift,Asorti,Renk,Kalite,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj]from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID where ((IsTakipForm.IsActive)=True) order by TakipNo", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["TakipID"].HeaderText = "ID";
            dataGridView1.Columns["Tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["TakipNo"].HeaderText = "Takip No";
            dataGridView1.Columns["unvan"].HeaderText = "Müşteri";
            dataGridView1.Columns["FisNo"].HeaderText = "Fiş No";
            dataGridView1.Columns["Kalip"].HeaderText = "Kalip";
            dataGridView1.Columns["Okce"].HeaderText = "Ökçe";
            dataGridView1.Columns["Platfotm"].HeaderText = "Platfotm";
            dataGridView1.Columns["Garni"].HeaderText = "Garni";
            dataGridView1.Columns["Cift"].HeaderText = "Çift";
            dataGridView1.Columns["Asorti"].HeaderText = "Asorti";
            dataGridView1.Columns["Renk"].HeaderText = "Renk";
            dataGridView1.Columns["Kalite"].HeaderText = "Kalite";
            dataGridView1.Columns["kesimci"].HeaderText = "Kesimci";
            dataGridView1.Columns["temizleme"].HeaderText = "Temizleme";
            dataGridView1.Columns["kalipci"].HeaderText = "Kalıpçı";
            dataGridView1.Columns["montaj"].HeaderText = "Montajcı";
            dataGridView1.Columns[0].Visible = false;
        }

        //Ya Hacı Bumbala Elemanları Tektek Combobox doldurucaz ve daha sonra önceden oluşturmuş olduğum departman tablosundan sıra ile hangi personel karşılık geliyorsa ona göre kayıt yapıcan.
        private void FuturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void musteridimmetot()
        {
            #region Müşteri ID'sini Yakalayalım

            OleDbCommand com = new OleDbCommand("select MusteriID from IsTakipForm WHERE ((IsTakipForm.TakipID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", takipidim);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            musteridim = Convert.ToInt32(com.ExecuteScalar());

            #endregion Müşteri ID'sini Yakalayalım

            #region 1.Personel Icin ID Yakaliyoruz.

            OleDbCommand kesimcicom = new OleDbCommand("select kesimci from IsTakipForm WHERE ((IsTakipForm.TakipID)=@ID)", con);
            kesimcicom.Parameters.AddWithValue("@ID", takipidim);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            kesimciID = Convert.ToInt32(kesimcicom.ExecuteScalar());

            #endregion 1.Personel Icin ID Yakaliyoruz.

            #region 2.Personel Icin ID Yakaliyoruz.

            OleDbCommand temizlemecom = new OleDbCommand("select temizleme from IsTakipForm WHERE ((IsTakipForm.TakipID)=@ID)", con);
            temizlemecom.Parameters.AddWithValue("@ID", takipidim);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            temizlemeID = Convert.ToInt32(temizlemecom.ExecuteScalar());

            #endregion 2.Personel Icin ID Yakaliyoruz.

            #region 3.Personel Icin ID Yakaliyoruz.

            OleDbCommand Kalipcicom = new OleDbCommand("select kalipci from IsTakipForm WHERE ((IsTakipForm.TakipID)=@ID)", con);
            Kalipcicom.Parameters.AddWithValue("@ID", takipidim);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            kalipciID = Convert.ToInt32(Kalipcicom.ExecuteScalar());

            #endregion 3.Personel Icin ID Yakaliyoruz.

            #region 4.Personel Icin ID Yakaliyoruz.

            OleDbCommand montajcom = new OleDbCommand("select montaj from IsTakipForm WHERE ((IsTakipForm.TakipID)=@ID)", con);
            montajcom.Parameters.AddWithValue("@ID", takipidim);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            montajID = Convert.ToInt32(montajcom.ExecuteScalar());

            #endregion 4.Personel Icin ID Yakaliyoruz.
        }

        private void DprtmnPrsnlKesim1()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=5) or ((personeller.departmanID)=1)) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr[1] = "Lütfen Seçiniz";
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
            //dr["personelID"] = DBNull.Value; Database identity özelliğine sahip olduğu için yani bir bir artmak zorunda olduğu için null değeri kabul etmiyor.
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
            dr[1] = "Lütfen Seçiniz";
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
            dr[1] = "Lütfen Seçiniz";
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
            dr[1] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            mstri.DataSource = dt;
            mstri.DisplayMember = "unvan";
            mstri.ValueMember = "musteriID";
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
        }

        private void IstakipFormguncelleme_Load(object sender, EventArgs e)
        {
            DprtmnPrsnlKalip3();
            DprtmnPrsnlKesim1();
            DprtmnPrsnlMontaj4();
            DprtmnPrsnlTemizleme2();
            musteriiGetir();
            ListemiGetir();
            FuturaKontrol();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);
                takip.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                takipidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                musteridimmetot();
                mstri.SelectedValue = musteridim;
                fisno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                kalip.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                okce.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                platfotm.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                garni.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cift.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                renk.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                kalite.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                prsnl1.SelectedValue = kesimciID;
                prsnl2.SelectedValue = temizlemeID;
                prsnl3.SelectedValue = kalipciID;
                prsnl4.SelectedValue = montajID;
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);
                takip.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                takipidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                musteridimmetot();
                mstri.SelectedValue = musteridim;
                fisno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                kalip.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                okce.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                platfotm.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                garni.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cift.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                renk.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                kalite.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                prsnl1.SelectedValue = kesimciID;
                prsnl2.SelectedValue = temizlemeID;
                prsnl3.SelectedValue = kalipciID;
                prsnl4.SelectedValue = montajID;
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
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
                DialogResult soruyoruz = MessageBox.Show(takip.Text + " " + "Nolu İş Takip Formunu Güncellemek İstediğinize Eminin misiniz ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    EIsTakip iscimGuncel = new EIsTakip();
                    iscimGuncel.TakipID = takipidim;
                    iscimGuncel.Asorti = asorti;
                    iscimGuncel.Cift = cift.Text;
                    iscimGuncel.FisNo = fisno.Text;
                    iscimGuncel.Garni = garni.Text;
                    iscimGuncel.Kalip = kalip.Text;
                    iscimGuncel.Kalite = kalite.Text;
                    iscimGuncel.MusteriID = Convert.ToInt32(mstri.SelectedValue);
                    iscimGuncel.Okce = okce.Text;
                    iscimGuncel.Personel1 = Convert.ToInt32(prsnl1.SelectedValue);
                    iscimGuncel.Personel2 = Convert.ToInt32(prsnl2.SelectedValue);
                    iscimGuncel.Personel3 = Convert.ToInt32(prsnl3.SelectedValue);
                    iscimGuncel.Personel4 = Convert.ToInt32(prsnl4.SelectedValue);
                    iscimGuncel.Platfotm = platfotm.Text;
                    iscimGuncel.Renk = renk.Text;
                    iscimGuncel.TakipNo = takip.Text;
                    iscimGuncel.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    FIsTakip.IsTakipGuncelle(iscimGuncel);
                    TemizleYigen();
                    ListemiGetir();
                    this.Close();
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            TemizleYigen();
        }

        private void radio5_Click(object sender, EventArgs e)
        {
            asorti = "5";
        }

        private void radio6_Click(object sender, EventArgs e)
        {
            asorti = "6";
        }

        private void radio7_Click_1(object sender, EventArgs e)
        {
            asorti = "7";
        }

        private void radio8_Click_1(object sender, EventArgs e)
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

        private void radio3_Click(object sender, EventArgs e)
        {
            asorti = "3";
        }

        private void radio4_Click(object sender, EventArgs e)
        {
            asorti = "4";
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

        private void IstakipFormguncelleme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ListemiGetir();
                FuturaKontrol();
            }
        }
    }
}