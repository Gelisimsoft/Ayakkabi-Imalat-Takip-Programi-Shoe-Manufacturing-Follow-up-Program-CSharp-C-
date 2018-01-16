using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class PersonelCariKarti : Form
    {
        public PersonelCariKarti()
        {
            InitializeComponent();
        }

        public void Listemidoldur()
        {
            listView1.Items.Clear();
            DataTable pdet = Fpersonel.Pekstre();
            for (int i = 0; i < pdet.Rows.Count; i++)
            {
                ListViewItem listecik = new ListViewItem(pdet.Rows[i]["adi"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["tcno"].ToString());
                listecik.SubItems.Add(pdet.Rows[i]["isegiris"].ToString().Remove(11));
                listecik.SubItems.Add(pdet.Rows[i]["cikisdate"].ToString());
                listView1.Items.Add(listecik);
            }
        }

        private void Temizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = string.Empty;
                }
                adres.Clear();
                ceptlf.Clear();
                evtlf.Clear();
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Departman İçin Bir Bölüm Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                DialogResult sor = MessageBox.Show("Personel Kartı Kaydı Yapmak İstiyor musunuz ?", "Personel Kaydı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sor == DialogResult.Yes)
                {
                    Epersonel prsnl = new Epersonel();
                    prsnl._adsoyad = adsoyad.Text;
                    prsnl._tcno = tcno.Text;
                    prsnl._adres = adres.Text;
                    prsnl._ceptlf = ceptlf.Text;
                    prsnl._evtlf = evtlf.Text;
                    prsnl._isegiris = Convert.ToDateTime(isegiris.Value.ToShortDateString());
                    prsnl._departman = Convert.ToInt32(comboBox1.SelectedValue);
                    Fpersonel.PEkle(prsnl);
                    Temizle();
                    Listemidoldur();
                }
            }
        }

        private void departman()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select departmanID,adi from departman", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr[1] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "departmanID";
            comboBox1.DisplayMember = "adi";
        }

        private void PersonelCariKarti_Load(object sender, EventArgs e)
        {
            departman();
            Listemidoldur();
        }
    }
}