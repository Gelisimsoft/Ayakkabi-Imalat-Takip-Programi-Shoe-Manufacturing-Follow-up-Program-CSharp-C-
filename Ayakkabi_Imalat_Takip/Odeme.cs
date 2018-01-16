using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Odeme : Form
    {
        public Odeme()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TemizlikZamani();
        }

        private void TemizlikZamani()
        {
            foreach (Control abuzuttin in this.Controls)
            {
                if (abuzuttin is TextBox)
                {
                    abuzuttin.Text = string.Empty;
                }
            }
            acklama.Text = string.Empty;
            tdrikci.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tdrikci.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Tedarikci İçin Bir Firma Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soryigen = MessageBox.Show("Ödeme Makbuzunu Kaydetmek İstiyor musunuz ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soryigen == DialogResult.Yes)
                {
                    EOdemeler odemem = new EOdemeler();
                    odemem.Mzkno = mkzno.Text;
                    odemem.Tarih = Convert.ToDateTime(tarih.Value.ToShortDateString());
                    odemem.TedarikciID = Convert.ToInt32(tdrikci.SelectedValue);
                    odemem.Tutar = Convert.ToDouble(tutar.Text);
                    odemem.Aciklama = acklama.Text;
                    FOdemeler.OEkle(odemem);
                    TemizlikZamani();
                    ListviewDoldur();
                }
            }
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            TedarikciGetir();
            ListviewDoldur();
            tutar.Text = Convert.ToString(0);
            mkzno.Text = Convert.ToString(0);
        }

        private void TedarikciGetir()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", connect.connectroad);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            tdrikci.DataSource = dt;
            tdrikci.DisplayMember = "unvan";
            tdrikci.ValueMember = "TedarikciID";
        }

        private void ListviewDoldur()
        {
            listView1.Items.Clear();
            FOdemeler Odemeci = new FOdemeler();
            DataTable dt = Odemeci.OEkstre();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(dt.Rows[i]["tutar"].ToString());
                string tutar = String.Format("{0:C}", tutarimiz);
                ListViewItem tahsilato = new ListViewItem(dt.Rows[i]["mkzno"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["tarih"].ToString().Remove(11));
                tahsilato.SubItems.Add(tutar);
                tahsilato.SubItems.Add(dt.Rows[i]["unvan"].ToString());
                tahsilato.SubItems.Add(dt.Rows[i]["aciklama"].ToString());
                listView1.Items.Add(tahsilato);
            }
        }
    }
}