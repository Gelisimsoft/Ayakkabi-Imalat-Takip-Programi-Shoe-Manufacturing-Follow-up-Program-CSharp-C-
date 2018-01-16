using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokMG : Form
    {
        public StokMG()
        {
            InitializeComponent();
        }

        private int ID;
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
                Uretilencombo.SelectedIndex = 0;
                Kullanilancombo.SelectedIndex = 0;
            }
        }

        private void KStoklar()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz.";
            dt.Rows.InsertAt(dr, 0);
            Kullanilancombo.DataSource = dt;
            Kullanilancombo.ValueMember = "stokid";
            Kullanilancombo.DisplayMember = "stokadi";
        }

        private void UStoklar()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz.";
            dt.Rows.InsertAt(dr, 0);
            Uretilencombo.DataSource = dt;
            Uretilencombo.ValueMember = "stokid";
            Uretilencombo.DisplayMember = "stokadi";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void StokMG_Load(object sender, EventArgs e)
        {
            Getir();
            UStoklar();
            KStoklar();
            FKontrol();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value);
            Kullanilancombo.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
            miktar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            Uretilencombo.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value);
            Kullanilancombo.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value);
            miktar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            birim.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            Uretilencombo.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Stok Maliyet İşlemini Güncellemek İstediğinize Eminin misiniz. ?", "Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                ESM s = new ESM();
                s.SMID = ID;
                s.UStok = Convert.ToInt32(Uretilencombo.SelectedValue);
                s.KStok = Convert.ToInt32(Kullanilancombo.SelectedValue);
                s.Miktar = Convert.ToDouble(miktar.Text);
                s.Birim = birim.Text;
                s.Tarih = Convert.ToDateTime(dateTimePicker1.Value);
                FSM.Guncelle(s);
            }
            Getir();
            FKontrol();
            Temizle();
            this.Close();
        }

        private void StokMG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Getir();
                FKontrol();
            }
        }
    }
}