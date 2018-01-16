using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class OdemelerGuncelleme : Form
    {
        public OdemelerGuncelleme()
        {
            InitializeComponent();
        }

        private bool degistimiula = false;
        private int odemeidisi, tedarikcimiz;
        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_fcmedya;");

        private void TemizleYigen()
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

        private void OdemelerGuncelleme_Load(object sender, EventArgs e)
        {
            TedarikciGetir();
            Datagrid();
            FaturaKontrol();
        }

        private void TedarikciGetir()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", con);
            DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            tdrikci.DataSource = dt;
            tdrikci.DisplayMember = "unvan";
            tdrikci.ValueMember = "TedarikciID";
        }

        private void TedarikciDegisir()
        {
            EOdemeler degistimi = new EOdemeler();
            degistimi.odemeID = tedarikcimiz;
            FOdemeler.OCombo(degistimi);
        }

        private void tdrikci_SelectedValueChanged(object sender, EventArgs e)
        {
            degistimiula = true;
        }

        private void GetirTdrci()
        {
            OleDbCommand comcuk = new OleDbCommand("select TedarikciID from Odemeler WHERE ((Odemeler.odemeID)=@ID)", con);
            comcuk.Parameters.AddWithValue("@ID", odemeidisi);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            tedarikcimiz = Convert.ToInt32(comcuk.ExecuteScalar());
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Odemeler.odemeID, Odemeler.mkzno, Odemeler.tarih, Odemeler.tutar, tedarikciler.unvan, Odemeler.aciklama FROM Odemeler INNER JOIN tedarikciler ON Odemeler.TedarikciID = tedarikciler.TedarikciID", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["odemeID"].HeaderText = "No";
            dataGridView1.Columns["mkzno"].HeaderText = "Makbuz No";
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["unvan"].HeaderText = "Tedarikci Firma";
            dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                odemeidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                GetirTdrci();
                tdrikci.SelectedValue = tedarikcimiz;
                acklama.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            odemeidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tarih.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
            tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            GetirTdrci();
            tdrikci.SelectedValue = tedarikcimiz;
            acklama.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tdrikci.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Tedarikci İçin Bir Firma Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soruyoruz = MessageBox.Show(mkzno.Text + " " + "Nolu Ödeme Makbuzunu Güncellemek İstediğinize Eminin misiniz ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soruyoruz == DialogResult.Yes)
                {
                    if (degistimiula == true)
                    {
                        TedarikciDegisir();
                    }
                    EOdemeler odemem = new EOdemeler();
                    odemem.Aciklama = acklama.Text;
                    odemem.Mzkno = mkzno.Text;
                    odemem.odemeID = odemeidisi;
                    odemem.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    odemem.TedarikciID = Convert.ToInt32(tdrikci.SelectedValue);
                    odemem.Tutar = Convert.ToDouble(tutar.Text);
                    FOdemeler.OGuncelle(odemem);
                    TemizleYigen();
                    Datagrid();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TemizleYigen();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OdemelerGuncelleme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Datagrid();
                FaturaKontrol();
            }
        }
    }
}