using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class TahsilatlarSilme : Form
    {
        public TahsilatlarSilme()
        {
            InitializeComponent();
        }

        private int tahsilatidisi, musterimiz;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void TemizlikZamani()
        {
            foreach (Control Armut in this.Controls)
            {
                if (Armut is TextBox)
                {
                    Armut.Text = string.Empty;
                    acklama.Text = string.Empty;
                }
                musteritxt.Text = "";
                tarihtxt.Text = "";
            }
        }

        private void GetirMstri()
        {
            OleDbCommand comcuk = new OleDbCommand("select musteriID from Tahsilatlar where tahsilatID=@ID", con);
            comcuk.Parameters.AddWithValue("@ID", tahsilatidisi);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            musterimiz = Convert.ToInt32(comcuk.ExecuteScalar());
        }

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                button1.Enabled = false;
                dataGridView1.Enabled = false;
            }
        }

        private void Datagrid()
        {
            OleDbCommand com = new OleDbCommand("select  tahsilatID,mkzno,tarih,tutar,aciklama,unvan from tahsilatlar inner join musteriler on Tahsilatlar.musteriID=musteriler.musteriID", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["tahsilatID"].HeaderText = "No";
            dataGridView1.Columns["mkzno"].HeaderText = "Makbuz No";
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["unvan"].HeaderText = "Müşteri Firma";
            dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns[0].Visible = false;
        }

        private void TahsilatlarSilme_Load(object sender, EventArgs e)
        {
            Datagrid();
            FaturaKontrol();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                tahsilatidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Remove(11);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                GetirMstri();
                musteritxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); ;
                acklama.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                tahsilatidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Remove(11);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                GetirMstri();
                musteritxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); ;
                acklama.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show(mkzno.Text + " " + "Numaralı Tahsilat Makbuzunu Silmek İstediğinize Eminin misiniz. ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                ETahsilat tahsilatsil = new ETahsilat();
                tahsilatsil.TahsilatID = tahsilatidisi;
                tahsilatsil.MusteriID = musterimiz;
                tahsilatsil.Mzkno = mkzno.Text;
                FTahsilat.TSilme(tahsilatsil);
                TemizlikZamani();
                Datagrid();
                FaturaKontrol();
                this.Close();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}