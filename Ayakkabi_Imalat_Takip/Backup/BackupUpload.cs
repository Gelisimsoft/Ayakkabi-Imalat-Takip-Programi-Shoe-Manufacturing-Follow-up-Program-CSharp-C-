using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip.Backup
{
    public partial class BackupUpload : Form
    {
        public BackupUpload()
        {
            InitializeComponent();
        }

        private string yol;

        private void DosyadakiAttributelariSil(string dosya)
        {
            File.SetAttributes(dosya, FileAttributes.Offline);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != null)
            {
                yol = openFileDialog1.FileName;
                textBox1.Text = yol;
            }
        }

        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void button2_Click(object sender, EventArgs e)
        {
            string DegisecekDosyayol = Application.StartupPath + "\\Veritabani\\Data.mdb";
            // string lbdosyayol = Application.StartupPath + "\\Veritabani\\Data.ldb";
            //if (File.Exists(lbdosyayol) == true)
            // {
            //     DosyadakiAttributelariSil(lbdosyayol);
            //     File.Delete(lbdosyayol);
            // }
            try
            {
                button2.Text = "Lütfen Bekleyiniz...";
                button2.Enabled = false;
                if (File.Exists(DegisecekDosyayol) == true)
                {
                    DosyadakiAttributelariSil(DegisecekDosyayol);
                    File.Delete(DegisecekDosyayol);
                    System.IO.File.Copy(yol, DegisecekDosyayol);
                    MessageBox.Show("Yedek  '" + yol + "' geri yüklendi.\n - Geri yüklenen veritabanının kullanılabilmesi için programı yeniden başlatınız.", "Yedek Geri Yükleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    System.IO.File.Copy(yol, DegisecekDosyayol);
                    MessageBox.Show("Yedek  '" + yol + "' geri yüklendi.\n - Geri yüklenen veritabanının kullanılabilmesi için programı yeniden başlatınız.", "Yedek Geri Yükleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yedek geri yükleme işlemi sırasında bir hata oluştu!\n" + ex.Message);
            }
            finally
            {
                con.Close();
                this.Close();
                button2.Text = "Başlat";
                button2.Enabled = false;
            }
        }

        private void BackupUpload_Load(object sender, EventArgs e)
        {
            GetirBilgileri();
            con.Close();
            con.Dispose();
        }

        private void GetirBilgileri()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbDataAdapter dap = new OleDbDataAdapter("select * from Databasem where ID=(select MAX(ID) from Databasem)", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            try
            {
                if (dt.Rows.Count != 0)
                {
                    textBox2.Text = Convert.ToString(dt.Rows[0][1].ToString());
                    textBox3.Text = Convert.ToString(dt.Rows[0][2].ToString());
                    textBox4.Text = Convert.ToString(dt.Rows[0][3].ToString());
                }
                else
                {
                    MessageBox.Show("Database Yedeği Bulunamadı.Geri Yükleme İşlemi Yapılamaz.\n - Lütfen Database Yedeğini Alınız.", "Database Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = false;
                    button2.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = false;
                button2.Enabled = false;
                textBox1.Enabled = false;
            }
            finally { con.Close(); }
        }
    }
}