using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokMS : Form
    {
        public StokMS()
        {
            InitializeComponent();
        }

        private int ID, KID;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Getir()
        {
            DataTable dtg = FSM.Ekstre();
            for (int i = 0; i < dtg.Rows.Count; i++)
            {
                dataGridView1.DataSource = dtg;
                dataGridView1.Columns["Tarih"].HeaderText = "Tarih";
                dataGridView1.Columns["K"].HeaderText = "Üretimde Kullanılan Stok";
                dataGridView1.Columns["Miktar"].HeaderText = "Miktar";
                dataGridView1.Columns["Birim"].HeaderText = "Birim";
                dataGridView1.Columns["U"].HeaderText = "Üretimi Tamamlanan Stok";
                dataGridView1.Columns["SMID"].Visible = false;
                dataGridView1.Columns["UStok"].Visible = false;
                dataGridView1.Columns["KStok"].Visible = false;
            }
        }

        private void FKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void Temizle()
        {
            foreach (Control fatih in this.groupBox1.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                KStok.Text = "";
                TStok.Text = "";
                Tarihtxt.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Stok Maliyet İşlemini Silmek İstediğinize Eminin misiniz. ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                ESM s = new ESM();
                s.SMID = ID;
                s.Miktar = Convert.ToDouble(miktar.Text);
                s.KStok = KID;
                FSM.Sil(s);
                Getir();
                FKontrol();
                Temizle();
                Getir();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StokMS_Load(object sender, EventArgs e)
        {
            Getir();
            FKontrol();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            Tarihtxt.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value.ToString().Remove(11));
            KStok.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            miktar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TStok.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            KID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            Tarihtxt.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value.ToString().Remove(11));
            KStok.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            miktar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TStok.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            KID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
        }
    }
}