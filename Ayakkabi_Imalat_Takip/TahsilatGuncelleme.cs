using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class TahsilatGuncelleme : Form
    {
        public TahsilatGuncelleme()
        {
            InitializeComponent();
        }

        private void Temizleusagun()
        {
            foreach (Control abuzuttin in this.Controls)
            {
                if (abuzuttin is TextBox)
                {
                    abuzuttin.Text = string.Empty;
                    acklama.Text = string.Empty;
                }
            }
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private bool musteridegisirse = false;
        private int tahsilatidisi, musterimiz;

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

        private void GetirMstri()
        {
            OleDbCommand comcuk = new OleDbCommand("select musteriID from Tahsilatlar WHERE ((Tahsilatlar.tahsilatID)=@ID)", con);
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
                button2.Enabled = false;
                dataGridView1.Enabled = false;
            }
        }

        private void Datagrid()
        {
            OleDbCommand com = new OleDbCommand("SELECT Tahsilatlar.tahsilatID, Tahsilatlar.mkzno, Tahsilatlar.tarih, Tahsilatlar.tutar, Tahsilatlar.aciklama, musteriler.unvan FROM Tahsilatlar INNER JOIN musteriler ON Tahsilatlar.musteriID = musteriler.musteriID", con);
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

        private void TahsilatGuncelleme_Load(object sender, EventArgs e)
        {
            Datagrid();
            musteriiGetir();
            FaturaKontrol();
        }

        private void mstri_SelectedValueChanged(object sender, EventArgs e)
        {
            musteridegisirse = true;
        }

        private void degisimolursamusteri()
        {
            ETahsilat tahsilatcigim = new ETahsilat();
            tahsilatcigim.TahsilatID = tahsilatidisi;
            FTahsilat.TCombo(tahsilatcigim);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mstri.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Müşteri İçin Bir Firma Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show(mkzno.Text + " " + "Nolu Tahsilat Makbuzunu Güncellemek İstediğinize Eminin misiniz ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    if (musteridegisirse == true)
                    {
                        degisimolursamusteri();
                    }
                    ETahsilat tahsilatim = new ETahsilat();
                    tahsilatim.Aciklama = acklama.Text;
                    tahsilatim.MusteriID = Convert.ToInt32(mstri.SelectedValue);
                    tahsilatim.Mzkno = mkzno.Text;
                    tahsilatim.TahsilatID = tahsilatidisi;
                    tahsilatim.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    tahsilatim.Tutar = Convert.ToDouble(tutar.Text);
                    FTahsilat.TGuncelle(tahsilatim);
                    Temizleusagun();
                    Datagrid();
                    FaturaKontrol();
                    this.Close();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Temizleusagun();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                tahsilatidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                GetirMstri();
                mstri.SelectedValue = musterimiz;
                acklama.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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
                tahsilatidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                GetirMstri();
                mstri.SelectedValue = musterimiz;
                acklama.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TahsilatGuncelleme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Datagrid();
                FaturaKontrol();
            }
        }
    }
}