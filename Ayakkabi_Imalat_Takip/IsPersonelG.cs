using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IsPersonelG : Form
    {
        public IsPersonelG()
        {
            InitializeComponent();
        }

        private int TID, ID;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void FaturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
            }
        }

        private void PersonelCome()
        {
            OleDbDataAdapter dap = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            prsnl1.DataSource = dt;
            prsnl1.DisplayMember = "adsoyad";
            prsnl1.ValueMember = "personelID";
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

        private void Temizle()
        {
            foreach (Control fatih in this.groupBox6.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                prsnl1.SelectedIndex = 0;
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Personel Ücretlerini Güncellemek İstediğinize Eminin misiniz. ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                EIsPersonel Is = new EIsPersonel();
                Is.IsID = ID;
                Is.personelID = Convert.ToInt32(prsnl1.SelectedValue);
                Is.TakipID = TID;
                Is.Ucret = double.Parse(ucrettxt.Text);
                FIsPersonel.Guncelle(Is);
                Temizle();
                Getir();
                FaturaKontrol();
            }
        }

        private void IsPersonelG_Load(object sender, EventArgs e)
        {
            Getir();
            PersonelCome();
            FaturaKontrol();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ucrettxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            takiptxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            prsnl1.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            TID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            button1.Enabled = true;
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            ucrettxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            takiptxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            prsnl1.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            TID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
            button1.Enabled = true;
        }

        private void IsPersonelG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Getir();
                FaturaKontrol();
            }
        }
    }
}