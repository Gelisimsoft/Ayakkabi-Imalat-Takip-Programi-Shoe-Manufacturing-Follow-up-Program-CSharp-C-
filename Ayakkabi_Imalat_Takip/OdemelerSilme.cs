using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class OdemelerSilme : Form
    {
        public OdemelerSilme()
        {
            InitializeComponent();
        }

        private int odemeidisi, tedarikcimiz;

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void OdemelerSilme_Load(object sender, EventArgs e)
        {
            Datagrid();
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
                acklama.Text = string.Empty;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OleDbCommand comcuk = new OleDbCommand("select TedarikciID from Odemeler WHERE ((Odemeler.odemeID)=@ID)", con);
            comcuk.Parameters.AddWithValue("@ID", odemeidisi);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            tedarikcimiz = Convert.ToInt32(comcuk.ExecuteScalar());
            DialogResult soruyoruz = MessageBox.Show(mkzno.Text + " " + "Numaralı Ödeme Makbuzunu Silmek İstediğinize Eminin misiniz ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                EOdemeler odemem = new EOdemeler();
                odemem.odemeID = odemeidisi;
                odemem.TedarikciID = tedarikcimiz;
                odemem.Tutar = Convert.ToDouble(tutar.Text);
                odemem.Mzkno = mkzno.Text;
                FOdemeler.OSilme(odemem);
                Temizle();
                Datagrid();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                odemeidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Remove(11);
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                tdrkcirxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
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
                odemeidisi = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                mkzno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                tutar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                tdrkcirxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                acklama.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Datagrid()
        {
            OleDbCommand com = new OleDbCommand("SELECT Odemeler.odemeID, Odemeler.mkzno, Odemeler.tarih, Odemeler.tutar, tedarikciler.unvan, Odemeler.aciklama FROM Odemeler INNER JOIN tedarikciler ON Odemeler.TedarikciID = tedarikciler.TedarikciID", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
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

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                button1.Enabled = false;
                dataGridView1.Enabled = false;
            }
        }
    }
}