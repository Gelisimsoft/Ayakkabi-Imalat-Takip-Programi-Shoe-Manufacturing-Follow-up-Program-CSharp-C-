using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class SatinalmaGuncelleme : Form
    {
        private bool degistirdinmicaniiiiimT, stoklardegisdumida = false;
        private int alisfuturaidisi;

        public SatinalmaGuncelleme()
        {
            InitializeComponent();
        }

        private void Temizle()
        {
            foreach (Control fatihresi in this.Controls)
            {
                if (fatihresi is TextBox)
                {
                    fatihresi.Text = string.Empty;
                }
            }
            tdrikci.SelectedIndex = 0;
            stokkrti.SelectedIndex = 0;
        }

        private void DataGrid()
        {
            OleDbCommand com = new OleDbCommand("SELECT satinalma.alisftID,satinalma.alistarih, satinalma.ftno, satinalma.birim, satinalma.miktar, satinalma.tutar, satinalma.kdv, satinalma.geneltoplam, satinalma.aciklama, tedarikciler.unvan, stoklar.stokadi FROM (satinalma INNER JOIN stoklar ON satinalma.stokid = stoklar.stokid) INNER JOIN tedarikciler ON satinalma.TedarikciID = tedarikciler.TedarikciID", con);
            OleDbDataAdapter adp = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["alisftID"].HeaderText = "No";
            dataGridView1.Columns["alistarih"].HeaderText = "Tarih";
            dataGridView1.Columns["ftno"].HeaderText = "Fatura No";
            dataGridView1.Columns["birim"].HeaderText = "Birim";
            dataGridView1.Columns["miktar"].HeaderText = "Miktar";
            dataGridView1.Columns["tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["kdv"].HeaderText = "Kdv";
            dataGridView1.Columns["geneltoplam"].HeaderText = "Genel Toplam";
            dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["unvan"].HeaderText = "Tedarikci";
            dataGridView1.Columns["stokadi"].HeaderText = "Stok Adı";
            dataGridView1.Columns[0].Visible = false;
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

        private void TedarikciDoldur()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", connect.connectroad);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            tdrikci.DataSource = dt;
            tdrikci.DisplayMember = "unvan";
            tdrikci.ValueMember = "TedarikciID";
        }

        private void tedarikcidegistir()
        {
            ESatinalma degistimmi = new ESatinalma();
            degistimmi._alisftID = alisfuturaidisi;
            FSatinalma.OComboT(degistimmi);
        }

        private void stokdegistimi()
        {
            ESatinalma degistimistoklarim = new ESatinalma();
            degistimistoklarim._alisftID = alisfuturaidisi;
            FSatinalma.OComboS(degistimistoklarim);
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
            OleDbCommand comted = new OleDbCommand("select TedarikciID from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            comted.Parameters.AddWithValue("@ID", alisfuturaidisi);
            int tedarikciidisi = Convert.ToInt32(comted.ExecuteScalar());
            tdrikci.SelectedValue = tedarikciidisi;
            //Satinalma tablosunda bulunan stokid için girilen değeri yakalıyoruz.stokidis nesnesine integer tipinde atama yapıyoruz.
            OleDbCommand comstok = new OleDbCommand("select stokid from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            comstok.Parameters.AddWithValue("@ID", alisfuturaidisi);
            int stokidisi = Convert.ToInt32(comstok.ExecuteScalar());
            stokkrti.SelectedValue = stokidisi;
        }

        private void SatinalmaGuncelleme_Load(object sender, EventArgs e)
        {
            TedarikciDoldur();
            StoklariGetir();
            DataGrid();
            FaturaKontrol();
        }

        private string tedarikcimiz, stoklarimiz;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            degistirdinmicaniiiiimT = true;
        }

        private void stokcombo_SelectedValueChanged(object sender, EventArgs e)
        {
            stoklardegisdumida = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tdrikci.SelectedIndex == 0)
            {
                tedarikcimiz = "- Tedarikci Firma Seçimini Yapınız.";
            }
            else
            {
                tedarikcimiz = "";
            }
            if (stokkrti.SelectedIndex == 0)
            {
                stoklarimiz = "- Stok Kartı Seçimini Yapınız.";
            }
            else
            {
                stoklarimiz = "";
            }

            if (tdrikci.SelectedIndex == 0 || stokkrti.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Aşağıdaki Alanları Doldurunuz.\r" + tedarikcimiz + "\r" + stoklarimiz + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show(ftno.Text + " " + "Numaralı Faturayı Güncellemek İstediğinize Eminin misiniz. ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    //Eğer Tedarikci combobox bir değişiklik olmuşsa kodları çalıştır
                    if (degistirdinmicaniiiiimT == true)
                    {
                        tedarikcidegistir();
                    }
                    if (stoklardegisdumida == true)
                    {
                        stokdegistimi();
                    }
                    //Esatinalmadan bilgileri çekiyoruz.Veritabanı bilgilerini ve formda bulunan nesneler ile eşleştiriyoruz.
                    ESatinalma Guncelleaci = new ESatinalma();
                    Guncelleaci._alisftID = alisfuturaidisi;
                    Guncelleaci._aciklama = acklama.Text;
                    Guncelleaci._alistarih = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    Guncelleaci._birim = birim.Text;
                    Guncelleaci._ftno = ftno.Text;
                    Guncelleaci._geneltoplam = Convert.ToDouble(gnltoplam.Text);
                    Guncelleaci._kdv = Convert.ToDouble(kdv.Text);
                    Guncelleaci._miktar = Convert.ToDouble(miktar.Text);
                    Guncelleaci._stokid = Convert.ToInt32(stokkrti.SelectedValue);
                    Guncelleaci._TedarikciID = Convert.ToInt32(tdrikci.SelectedValue);
                    Guncelleaci._tutar = Convert.ToDouble(tutar.Text);
                    FSatinalma.SGuncelle(Guncelleaci);
                    DataGrid();
                    Temizle();
                    this.Close();
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

        private void dataGridView1_MouseClick
(object sender, MouseEventArgs e)
        {
            try
            {
                alisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
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
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                alisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);
                ftno.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                idilerigetir();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SatinalmaGuncelleme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                DataGrid();
                FaturaKontrol();
            }
        }
    }
}