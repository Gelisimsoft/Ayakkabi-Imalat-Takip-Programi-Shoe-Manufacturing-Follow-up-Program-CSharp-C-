using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Satinalma : Form
    {
        public void Listemidoldur()
        {
            listView1.Items.Clear();
            DataTable pdet = FSatinalma.Sekstre();
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                double tutarcik = Convert.ToDouble(pdet.Rows[i]["tutar"].ToString());
                double kdvcik = Convert.ToDouble(pdet.Rows[i]["kdv"].ToString());
                double genelcik = Convert.ToDouble(pdet.Rows[i]["geneltoplam"].ToString());
                string tutar = String.Format("{0:C}", tutarcik);
                string kdv = String.Format("{0:C}", kdvcik);
                string geneltutar = String.Format("{0:C}", genelcik);
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["alistarih"].ToString().Remove(11));
                listecik.SubItems.Add(pdet.Rows[i]["ftno"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["unvan"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["stokadi"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["birim"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["miktar"].ToString());
                listecik.SubItems.Add(tutar);
                listecik.SubItems.Add(kdv);
                listecik.SubItems.Add(geneltutar);
                listecik.SubItems.Add(pdet.Rows[i]["aciklama"].ToString());
                listView1.Items.Add(listecik);
            }
        }

        public Satinalma()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Temizle()
        {
            foreach (Control fatih in this.Controls)
            {
                if (fatih is TextBox)
                {
                    fatih.Text = string.Empty;
                }
                tdrikci.SelectedIndex = 0;
            }
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);
        private string tedarikcimiz, stoklarimiz;

        private void button1_Click(object sender, EventArgs e)
        {
            if (tdrikci.SelectedIndex == 0)
            {
                tedarikcimiz = "- Tedarikci Firma Seçimini Yapınız.";
            }
            else
            {
                tedarikcimiz = "";
            }
            if (stokkrti.SelectedIndex == 0)
            {
                stoklarimiz = "- Stok Kartı Seçimini Yapınız.";
            }
            else
            {
                stoklarimiz = "";
            }

            if (tdrikci.SelectedIndex == 0 || stokkrti.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Aşağıdaki Alanları Doldurunuz.\r" + tedarikcimiz + "\r" + stoklarimiz + "\r", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult soryigen = MessageBox.Show("Satinalma Faturasını Kaydetmek İstiyor musunuz ?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soryigen == DialogResult.Yes)
                {
                    ESatinalma aldikmal = new ESatinalma();
                    aldikmal._ftno = ftno.Text;
                    aldikmal._birim = birim.Text;
                    aldikmal._miktar = Convert.ToDouble(miktar.Text);
                    aldikmal._tutar = Convert.ToDouble(tutar.Text);
                    aldikmal._kdv = Convert.ToDouble(kdv.Text);
                    aldikmal._geneltoplam = Convert.ToDouble(gnltoplam.Text);
                    aldikmal._aciklama = acklama.Text;
                    aldikmal._TedarikciID = Convert.ToInt32(tdrikci.SelectedValue);
                    aldikmal._stokid = Convert.ToInt32(stokkrti.SelectedValue);
                    aldikmal._alistarih = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                    FSatinalma.Sekle(aldikmal);
                    Temizle();
                    Listemidoldur();
                }
            }
        }

        private void Satinalma_Load(object sender, EventArgs e)
        {
            //textbox'lar boş geçildiği zaman otomatik "0" vermesi için kod yazıyoruz.
            ftno.Text = Convert.ToString(0);
            miktar.Text = Convert.ToString(0);
            tutar.Text = Convert.ToString(0);
            kdv.Text = Convert.ToString(0);
            gnltoplam.Text = Convert.ToString(0);
            TedarikciGetir();
            StoklarGetir();
            Listemidoldur();
        }

        private void TedarikciGetir()
        {
            OleDbDataAdapter detda = new OleDbDataAdapter("select TedarikciID,unvan from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", con);
            DataTable teddt = new DataTable();
            detda.Fill(teddt);
            DataRow dr = teddt.NewRow();
            dr["unvan"] = "Lütfen Seçiniz";
            teddt.Rows.InsertAt(dr, 0);
            tdrikci.DataSource = teddt;
            tdrikci.ValueMember = "TedarikciID";
            tdrikci.DisplayMember = "unvan";
        }

        private void StoklarGetir()
        {
            OleDbDataAdapter stoda = new OleDbDataAdapter("select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokkodu", con);
            DataTable stodt = new DataTable();
            stoda.Fill(stodt);
            DataRow dr = stodt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz";
            stodt.Rows.InsertAt(dr, 0);
            stokkrti.DataSource = stodt;
            stokkrti.ValueMember = "stokid";
            stokkrti.DisplayMember = "stokadi";
        }
    }
}