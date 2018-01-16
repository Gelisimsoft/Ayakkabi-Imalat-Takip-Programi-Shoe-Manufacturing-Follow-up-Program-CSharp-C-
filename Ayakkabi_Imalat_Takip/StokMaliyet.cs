using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StokMaliyet : Form
    {
        public StokMaliyet()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void Getir()
        {
            listView1.Items.Clear();
            DataTable det = FSM.Ekstre();
            for (int i = 0; i < det.Rows.Count; i++)
            {
                ListViewItem listecik = new ListViewItem(det.Rows[i]["Tarih"].ToString().Remove(11));
                listecik.SubItems.Add(det.Rows[i]["K"].ToString());
                listecik.SubItems.Add(det.Rows[i]["Miktar"].ToString());
                listecik.SubItems.Add(det.Rows[i]["Birim"].ToString());
                listecik.SubItems.Add(det.Rows[i]["U"].ToString());
                listView1.Items.Add(listecik);
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
                Ucombo.SelectedIndex = 0;
                Kcombo.SelectedIndex = 0;
                maskedTextBox1.Clear();
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
            Kcombo.DataSource = dt;
            Kcombo.ValueMember = "stokid";
            Kcombo.DisplayMember = "stokadi";
        }

        private void UStoklar()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz.";
            dt.Rows.InsertAt(dr, 0);
            Ucombo.DataSource = dt;
            Ucombo.ValueMember = "stokid";
            Ucombo.DisplayMember = "stokadi";
        }

        private void StokMaliyet_Load(object sender, EventArgs e)
        {
            UStoklar();
            KStoklar();
            Getir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show("Stok Maliyet İşlemini Kaydetmek İstediğinize Eminin misiniz. ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                try
                {
                    ESM s = new ESM();
                    s.UStok = Convert.ToInt32(Ucombo.SelectedValue);
                    s.KStok = Convert.ToInt32(Kcombo.SelectedValue);
                    s.Miktar = Convert.ToDouble(miktar.Text);
                    s.Birim = birim.Text;
                    s.Tarih = Convert.ToDateTime(maskedTextBox1.Text);
                    FSM.Ekle(s);
                    Temizle();
                    Getir();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}