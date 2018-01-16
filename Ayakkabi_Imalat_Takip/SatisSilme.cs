using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class SatisSilme : Form
    {
        public SatisSilme()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private int satisfuturaidisi, stokidisi, musteriidisi;

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
            dataGridView1.Columns["unvan"].HeaderText = "Tedarikci";
            dataGridView1.Columns["stokadi"].HeaderText = "Stok Adı";
            dataGridView1.Columns[0].Visible = false;
        }

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                sil.Enabled = false;
                dataGridView1.Enabled = false;
                MessageBox.Show("Sistem Kayıtlı Fatura Bulunamamıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            musteriidisi = Convert.ToInt32(comted.ExecuteScalar());
            //Satinalma tablosunda bulunan stokid için girilen değeri yakalıyoruz.stokidis nesnesine integer tipinde atama yapıyoruz.
            OleDbCommand comstok = new OleDbCommand("select stokid from satislar WHERE ((satislar.satisftID)=@ID)", con);
            comstok.Parameters.AddWithValue("@ID", satisfuturaidisi);
            stokidisi = Convert.ToInt32(comstok.ExecuteScalar());
        }

        private void SatisSilme_Load(object sender, EventArgs e)
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

        private void sil_Click(object sender, EventArgs e)
        {
            idilerigetir();
            DialogResult soruyoruz = MessageBox.Show(ftno.Text + " " + "Numaralı Faturayı Silmek İstediğinize Eminin misiniz ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                ESatis silelimhaci = new ESatis();
                silelimhaci._satisftID = satisfuturaidisi;
                silelimhaci._stokid = stokidisi;
                silelimhaci._musteriID = musteriidisi;
                silelimhaci._geneltoplam = Convert.ToDouble(gnltoplam.Text);
                silelimhaci._miktar = Convert.ToDouble(miktar.Text);
                silelimhaci._ftno = ftno.Text;
                FSatis.Ssil(silelimhaci);
                Temizle();
                DataGrid();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                satisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tarih.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                mstritxt.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                stkkrti.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                satisfuturaidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tarih.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                ftno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                miktar.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                kdv.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                gnltoplam.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                mstritxt.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                stkkrti.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}