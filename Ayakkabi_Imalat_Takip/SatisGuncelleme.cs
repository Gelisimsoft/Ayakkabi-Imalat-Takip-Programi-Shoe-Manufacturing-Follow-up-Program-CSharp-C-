using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class satisguncelle : Form
    {
        public satisguncelle()
        {
            InitializeComponent();
        }

        private string musterimiz, stoklarimiz;
        private int satisfuturaidisi, stokidisi, mstrisi;
        private bool musteridegistimi, stoklardegisdumida = false;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Temizle()
        {
            foreach (Control fatihresi in this.Controls)
            {
                if (fatihresi is TextBox)
                {
                    fatihresi.Text = string.Empty;
                }
            }
            stokkrti.SelectedIndex = 0;
            mstri.SelectedIndex = 0;
        }

        private void DataGrid()
        {
            OleDbCommand com = new OleDbCommand("SELECT satislar.satisftID, satislar.satistarih, satislar.ftno, satislar.birim, satislar.miktar, satislar.tutar, satislar.kdv, satislar.geneltoplam, satislar.aciklama, musteriler.unvan, stoklar.stokadi FROM (satislar INNER JOIN stoklar ON satislar.stokid = stoklar.stokid) INNER JOIN musteriler ON satislar.musteriID = musteriler.musteriID", con);
            OleDbDataAdapter adp = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["satisftID"].HeaderText = "No";
            dataGridView1.Columns["satistarih"].HeaderText = "Tarih";
            dataGridView1.Columns["ftno"].HeaderText = "Fatura No";
            dataGridView1.Columns["birim"].HeaderText = "Birim";
            dataGridView1.Columns["miktar"].HeaderText = "Miktar";
            dataGridView1.Columns["tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["kdv"].HeaderText = "Kdv";
            dataGridView1.Columns["geneltoplam"].HeaderText = "Genel Toplam";
            dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["unvan"].HeaderText = "Müşteri";
            dataGridView1.Columns["stokadi"].HeaderText = "Stok Adı";
            dataGridView1.Columns[0].Visible = false;
        }

        private void stokdegistimi()
        {
            ESatis degistimistoklarim = new ESatis();
            degistimistoklarim._satisftID = satisfuturaidisi;
            FSatis.OComboS(degistimistoklarim);
        }

        private void musteridegistir()
        {
            ESatis degistimmi = new ESatis();
            degistimmi._satisftID = satisfuturaidisi;
            FSatis.OComboT(degistimmi);
        }

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                dataGridView1.Enabled = false;
            }
        }

        private void idilerigetir()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //Satinlama tablosunda bulunan TedarikciID için girilen değeri yakalıyoruz.TedarikciID nesnesine integer tipinde atama yapıyoruz.
            OleDbCommand comted = new OleDbCommand("select musteriID from satislar WHERE ((satislar.satisftID)=@ID)", con);
            comted.Parameters.AddWithValue("@ID", satisfuturaidisi);
            mstrisi = Convert.ToInt32(comted.ExecuteScalar());
            mstri.SelectedValue = mstrisi;
            //Satinalma tablosunda bulunan stokid için girilen değeri yakalıyoruz.stokidis nesnesine integer tipinde atama yapıyoruz.
            OleDbCommand comstok = new OleDbCommand("select stokid from satislar WHERE ((satislar.satisftID)=@ID)", con);
            comstok.Parameters.AddWithValue("@ID", satisfuturaidisi);
            stokidisi = Convert.ToInt32(comstok.ExecuteScalar());
            stokkrti.SelectedValue = stokidisi;
            con.Close();
        }

        private void StoklariGetir()
        {
            OleDbDataAdapter adp3 = new OleDbDataAdapter("select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", connect.connectroad);
            DataTable dt2 = new DataTable();
            adp3.Fill(dt2);
            DataRow dr = dt2.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz";
            dt2.Rows.InsertAt(dr, 0);
            stokkrti.DataSource = dt2;
            stokkrti.DisplayMember = "stokadi";
            stokkrti.ValueMember = "stokid";
        }

        private void MusteriDoldur()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("select musteriID,unvan from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", connect.connectroad);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            mstri.DataSource = dt;
            mstri.DisplayMember = "unvan";
            mstri.ValueMember = "musteriID";
        }

        private void SatisGuncelleme_Load(object sender, EventArgs e)
        {
            MusteriDoldur();
            StoklariGetir();
            DataGrid();
            FaturaKontrol();
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            musteridegistimi = true;
        }

        private void stokcombo_SelectedValueChanged(object sender, EventArgs e)
        {
            stoklardegisdumida = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (mstri.SelectedIndex == 0)
            {
                musterimiz = "- Müşteri Firma Seçimini Yapınız.";
            }
            else
            {
                musterimiz = "";
            }
            if (stokkrti.SelectedIndex == 0)
            {
                stoklarimiz = "- Stok Kartı Seçimini Yapınız.";
            }
            else
            {
                stoklarimiz = "";
            }
            if (mstri.SelectedIndex == 0 || stokkrti.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Aşağıdaki Alanları Doldurunuz.\r" + musterimiz + "\r" + stoklarimiz + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show(ftno.Text + " " + "Numaralı Faturayı Güncellemek İstediğinize Eminin misiniz. ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    if (stoklardegisdumida == true)
                    {
                        stokdegistimi();
                    }
                    if (musteridegistimi == true)
                    {
                        musteridegistir();
                    }
                    ESatis Guncelleaci = new ESatis();
                    Guncelleaci._satisftID = satisfuturaidisi;
                    Guncelleaci._aciklama = acklama.Text;
                    Guncelleaci._satistarih = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    Guncelleaci._birim = birim.Text;
                    Guncelleaci._ftno = ftno.Text;
                    Guncelleaci._geneltoplam = Convert.ToDouble(gnltoplam.Text);
                    Guncelleaci._kdv = Convert.ToDouble(kdv.Text);
                    Guncelleaci._miktar = Convert.ToDouble(miktar.Text);
                    Guncelleaci._stokid = Convert.ToInt32(stokkrti.SelectedValue);
                    Guncelleaci._musteriID = Convert.ToInt32(mstri.SelectedValue);
                    Guncelleaci._tutar = Convert.ToDouble(tutar.Text);
                    FSatis.SGuncelle(Guncelleaci);
                    Temizle();
                    DataGrid();
                    this.Close();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                satisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                idilerigetir();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                satisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                idilerigetir();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void satisguncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                DataGrid();
                FaturaKontrol();
            }
        }
    }
}