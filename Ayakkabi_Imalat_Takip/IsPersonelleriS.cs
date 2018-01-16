using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IsPersonelleriS : Form
    {
        public IsPersonelleriS()
        {
            InitializeComponent();
        }

        private int ID, PID;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
            }
        }

        private void Temizle()
        {
            foreach (Control fatih in this.groupBox6.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
            }
        }

        private void Getir()
        {
            DataTable dt = FIsPersonel.Ekstre();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["TakipNo"].HeaderText = "Takip No";
            dataGridView1.Columns["FisNo"].HeaderText = "Fiş No";
            dataGridView1.Columns["adsoyad"].HeaderText = "Ad Soyad";
            dataGridView1.Columns["ucret"].HeaderText = "Ödenen Ücret";
            dataGridView1.Columns["personelID"].Visible = false;
            dataGridView1.Columns["TakipID"].Visible = false;
            dataGridView1.Columns["IsID"].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Personel Ücretlerini Silmek İstediğinize Eminin misiniz. ?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                EIsPersonel EP = new EIsPersonel();
                EP.IsID = ID;
                EP.personelID = PID;
                EP.Ucret = Convert.ToDouble(ucrettxt.Text);
                FIsPersonel.Sil(EP);
                Temizle();
                Getir();
                FaturaKontrol();
            }
        }

        private void IsPersonelleriS_Load(object sender, EventArgs e)
        {
            Getir();
            FaturaKontrol();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ucrettxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            takipnotxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Adsoyadtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            PID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            button1.Enabled = true;
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            ucrettxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            takipnotxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Adsoyadtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            PID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            button1.Enabled = true;
        }
    }
}