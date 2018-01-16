using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class StoklarEkstresi : Form
    {
        public StoklarEkstresi()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void StoklarEkstresi_Load(object sender, EventArgs e)
        {
            ListemiGetir();
            Stoklar();
            ForReserveAll();
        }

        private void Stoklar()
        {
            OleDbDataAdapter adp = new OleDbDataAdapter("Select stokid,stokadi from stoklar WHERE ((stoklar.IsActive)=True) order by stokadi", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["stokadi"] = "Lütfen Seçiniz.";
            dt.Rows.InsertAt(dr, 0);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "stokid";
            comboBox1.DisplayMember = "stokadi";
        }

        private void ListemiGetir()
        {
            DataTable dtt = FStok.sekstre();
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                ListViewItem lolo = new ListViewItem(dtt.Rows[i][1].ToString());
                lolo.SubItems.Add(dtt.Rows[i][2].ToString());
                lolo.SubItems.Add(dtt.Rows[i][3].ToString());
                lolo.SubItems.Add(dtt.Rows[i][4].ToString());
                listView1.Items.Add(lolo);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen Stok Kartı Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                listView1.Items.Clear();
                EStok MyReserve = new EStok();
                MyReserve._stokid = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable dtt = FStok.StokID(MyReserve);
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    ListViewItem lolo = new ListViewItem(dtt.Rows[i][1].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][2].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][3].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][4].ToString());
                    listView1.Items.Add(lolo);
                }
                ForReserveID();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            EStok MyReserve = new EStok();
            MyReserve._stokadi = textBox1.Text;
            DataTable dtt = FStok.StokName(MyReserve);
            if (dtt.Rows.Count != 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    ListViewItem lolo = new ListViewItem(dtt.Rows[i][1].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][2].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][3].ToString());
                    lolo.SubItems.Add(dtt.Rows[i][4].ToString());
                    listView1.Items.Add(lolo);
                }
            }
            else
            {
                MessageBox.Show("'" + textBox1.Text + "'" + " Adında Stok Kaydı Bulunamamıştır !", "Arama Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListemiGetir();
            }
            textBox1.Text = string.Empty;
        }

        private int miktarall, miktarID;
        private object gelen = 0;

        private void ForReserveAll()
        {
            OleDbCommand com = new OleDbCommand("select sum(miktar) from stoklar WHERE ((stoklar.IsActive)=True)", con);
            if (com.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (com.ExecuteScalar() != gelen && com.ExecuteScalar() != DBNull.Value)
            {
                miktarall = Convert.ToInt32(com.ExecuteScalar());
                if (miktarall < 0)
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;
                }
                label2.Text = Convert.ToString(miktarall);
            }
            con.Close();
        }

        private void ForReserveID()
        {
            OleDbCommand com = new OleDbCommand("select sum(miktar) from stoklar WHERE (((stoklar.IsActive)=True) AND ((stoklar.stokid)=@ID))", con);
            com.Parameters.AddWithValue("@ID", Convert.ToInt32(comboBox1.SelectedValue));
            if (com.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (com.ExecuteScalar() != gelen && com.ExecuteScalar() != DBNull.Value)
            {
                miktarID = Convert.ToInt32(com.ExecuteScalar());
                if (miktarID < 0)
                {
                    label2.ForeColor = Color.Red;
                }
                else
                {
                    label2.ForeColor = Color.Black;
                }
                label2.Text = Convert.ToString(com.ExecuteScalar());
            }
            con.Close();
        }
    }
}