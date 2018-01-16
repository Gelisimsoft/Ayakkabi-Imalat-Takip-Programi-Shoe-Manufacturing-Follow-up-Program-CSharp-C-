using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IsPersonelR : Form
    {
        public IsPersonelR()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void PCGetir()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("select personelID,adsoyad from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["adsoyad"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            pcombo.DataSource = dt;
            pcombo.DisplayMember = "adsoyad";
            pcombo.ValueMember = "personelID";
        }

        private void FCGetir()
        {
            OleDbDataAdapter daF = new OleDbDataAdapter("select FisNo,TakipID from IsTakipForm WHERE ((IsTakipForm.IsActive)=True) order by Fisno", con);
            DataTable dt = new DataTable();
            daF.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["FisNo"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            Fcombo.DataSource = dt;
            Fcombo.DisplayMember = "FisNo";
            Fcombo.ValueMember = "TakipID";
        }

        private void TCGetir()
        {
            OleDbDataAdapter daT = new OleDbDataAdapter("select TakipNo,TakipID from IsTakipForm WHERE ((IsTakipForm.IsActive)=True) order by TakipNo", con);
            DataTable dt = new DataTable();
            daT.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["TakipNo"] = "Lütfen Seçiniz";
            dt.Rows.InsertAt(dr, 0);
            Tcombo.DataSource = dt;
            Tcombo.DisplayMember = "TakipNo";
            Tcombo.ValueMember = "TakipID";
        }

        private void Getir()
        {
            listView1.Items.Clear();
            DataTable det = FIsPersonel.Ekstre();
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["ucret"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["TakipNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["FisNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void GetirPer()
        {
            listView1.Items.Clear();
            EIsPersonel s = new EIsPersonel();
            s.personelID = Convert.ToInt32(pcombo.SelectedValue);
            DataTable det = FIsPersonel.PEkstre(s);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["ucret"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["TakipNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["FisNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void GetirT()
        {
            listView1.Items.Clear();
            EIsPersonel s = new EIsPersonel();
            s.TakipID = Convert.ToInt32(Tcombo.SelectedValue);
            DataTable det = FIsPersonel.FEkstre(s);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["ucret"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["TakipNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["FisNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void GetirF()
        {
            listView1.Items.Clear();
            EIsPersonel s = new EIsPersonel();
            s.TakipID = Convert.ToInt32(Fcombo.SelectedValue);
            DataTable det = FIsPersonel.FEkstre(s);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                double tutarimiz = Convert.ToDouble(det.Rows[i]["ucret"].ToString());
                string tutar = string.Format("{0:C}", tutarimiz);
                ListViewItem listecik = new ListViewItem(det.Rows[i]["TakipNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["FisNo"].ToString());
                listecik.SubItems.Add(det.Rows[i]["adsoyad"].ToString());
                listecik.SubItems.Add(tutar);
                listView1.Items.Add(listecik);
            }
        }

        private void GT()
        {
            object geli = FIsPersonel.GT();
            if (geli == null)
            {
                textBox1.Text = "0";
            }
            else
            {
                double turarli = FIsPersonel.GT();
                string tutar = string.Format("{0:C}", turarli);
                textBox1.Text = tutar;
            }
        }

        private void GTF()
        {
            EIsPersonel E = new EIsPersonel();
            E.TakipID = Convert.ToInt32(Fcombo.SelectedValue);
            FIsPersonel.GTF(E);
            object gelen = FIsPersonel.GTF(E);
            if (gelen == null)
            {
                textBox1.Text = "0";
            }
            else
            {
                double turarli = Convert.ToInt32(gelen);
                string tutarimiz = string.Format("{0:C}", turarli);
                textBox1.Text = tutarimiz;
            }
        }

        private void GTT()
        {
            EIsPersonel E = new EIsPersonel();
            E.TakipID = Convert.ToInt32(Tcombo.SelectedValue);
            FIsPersonel.GTF(E);
            double gelen = FIsPersonel.GTF(E);
            if (gelen == null)
            {
                textBox1.Text = "0";
            }
            else
            {
                double turarli = gelen;
                string tutarimiz = string.Format("{0:C}", turarli);
                textBox1.Text = tutarimiz;
            }
        }

        private void GTP()
        {
            EIsPersonel Elo = new EIsPersonel();
            Elo.personelID = Convert.ToInt32(pcombo.SelectedValue);
            FIsPersonel.GTP(Elo);
            object geliyo = FIsPersonel.GTP(Elo);
            if (geliyo == null)
            {
                textBox1.Text = "";
            }
            else
            {
                double tutarcik = Convert.ToDouble(geliyo);
                string himm = string.Format("{0:C}", tutarcik);
                textBox1.Text = himm;
            }
        }

        private void IsPersonelR_Load(object sender, EventArgs e)
        {
            Getir();
            PCGetir();
            FCGetir();
            TCGetir();
            GT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Fcombo.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Fiş No Seçimi Yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GTF();
                GetirF();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Tcombo.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Takip No Seçimi Yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GTT();
                GetirT();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pcombo.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Personel Seçimi Yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GTP();
                GetirPer();
            }
        }
    }
}