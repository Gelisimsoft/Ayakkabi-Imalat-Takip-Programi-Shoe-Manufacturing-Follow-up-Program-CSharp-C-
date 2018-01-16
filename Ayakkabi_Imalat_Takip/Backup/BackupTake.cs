using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class BackupTake : Form
    {
        public BackupTake()
        {
            InitializeComponent();
        }

        private string yol;

        private void button1_Click(object sender, EventArgs e)
        {
            string buugun = Convert.ToString(DateTime.Now.ToShortDateString().Replace(".", ""));
            string bugun = buugun.Replace("/", "");

            saveFileDialog1.FileName = "AyakkabiImalat" + "-" + bugun;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != null)
            {
                yol = saveFileDialog1.FileName;
                textBox1.Text = yol;
            }
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void button2_Click(object sender, EventArgs e)
        {
            string AnaDosyayol = Application.StartupPath + "\\Veritabani\\Data.mdb";
            try
            {
                int saat, dakika;
                DateTime date = DateTime.Now;
                saat = date.Hour;
                dakika = date.Minute;
                EDatabase dt = new EDatabase();
                dt.Ad = yol;
                dt.Tarih = date.Date;
                dt.Timecik = Convert.ToString(saat + ":" + dakika);
                FDatabase.Ekle(dt);
                System.IO.File.Copy(AnaDosyayol, yol + ".mdb");
                MessageBox.Show("Yedek  '" + yol + "' kaydedildi.", "Yedek Alma İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yedekleme işlemi sırasında bir hata oluştu!" + ex.Message);
            }
            finally
            {
                con.Close();
                this.Close();
            }
        }

        private void BackupTake_Load(object sender, EventArgs e)
        {
            YedeklemeBilgileriGetir();
        }

        private void YedeklemeBilgileriGetir()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbDataAdapter dt = new OleDbDataAdapter("select * from Databasem where Ad=(select MAX(Ad) from Databasem)", con);
            DataTable adp = new DataTable();
            dt.Fill(adp);
            try
            {
                textBox2.Text = Convert.ToString(adp.Rows[0]["Ad"].ToString());
                textBox3.Text = Convert.ToString(adp.Rows[0]["Tarih"].ToString().Remove(11));
                textBox4.Text = Convert.ToString(adp.Rows[0]["Timecik"].ToString().Remove(5));
            }
            catch (Exception)
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            finally { con.Close(); }
        }
    }
}