using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Sifre : Form
    {
        public Sifre()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand comEski = new OleDbCommand("select Sifre from Entriy WHERE ((Entriy.Sifre)=@Geliyoo)", con);
            comEski.Parameters.AddWithValue("@Geliyoo", textBox1.Text);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                OleDbDataReader rd = comEski.ExecuteReader();
                if (rd.Read())
                {
                    EEntity ent = new EEntity();
                    ent.KullaniciAdi = kullanici.Text;
                    ent.Sifre = sifretxt.Text;
                    FEntity.Guncelle(ent);
                    kullanici.Text = string.Empty;
                    sifretxt.Text = string.Empty;
                    textBox1.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Girdiğiniz Şifre Hatalı !!!", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }
    }
}