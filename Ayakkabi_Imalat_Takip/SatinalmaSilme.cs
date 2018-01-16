using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class SatinalmaSilme : Form
    {
        public SatinalmaSilme()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private int alisfuturaidisi, stokidisi, tedarikciidisi;

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
                sil.Enabled = false;
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
            tedarikciidisi = Convert.ToInt32(comted.ExecuteScalar());
            //Satinalma tablosunda bulunan stokid için girilen değeri yakalıyoruz.stokidis nesnesine integer tipinde atama yapıyoruz.
            OleDbCommand comstok = new OleDbCommand("select stokid from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            comstok.Parameters.AddWithValue("@ID", alisfuturaidisi);
            stokidisi = Convert.ToInt32(comstok.ExecuteScalar());
        }

        private void SatinalmaSilme_Load(object sender, EventArgs e)
        {
            DataGrid();
            FaturaKontrol();
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            idilerigetir();
            DialogResult soruyoruz = MessageBox.Show(ftno.Text + " " + "Numaralı Faturayı Silmek İstediğinize Eminin misiniz. ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                //Esatinalmadan bilgileri çekiyoruz.Veritabanı bilgilerini ve formda bulunan nesneler ile eşleştiriyoruz.
                ESatinalma Silaci = new ESatinalma();
                Silaci._alisftID = alisfuturaidisi;
                Silaci._ftno = ftno.Text;
                Silaci._stokid = stokidisi;
                Silaci._TedarikciID = tedarikciidisi;
                FSatinalma.Ssil(Silaci);
                Temizle();
                DataGrid();
                this.Close();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                alisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tarih.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                tdrikci.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                stkkrti.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
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
                tarih.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                tdrikci.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                stkkrti.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}