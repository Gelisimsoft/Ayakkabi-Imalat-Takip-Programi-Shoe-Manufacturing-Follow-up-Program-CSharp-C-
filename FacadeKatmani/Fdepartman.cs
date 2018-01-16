using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class Fdepartman : IDisposable
    {
        public static void departmanekle(Edepartman eklenecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand com = new OleDbCommand("insert into departman(adi,aciklama) values(@adi,@aciklama)", con);
            com.Parameters.AddWithValue("@adi", eklenecekler.Adi);
            com.Parameters.AddWithValue("@aciklama", eklenecekler.Aciklama);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void departmansil(Edepartman silinecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand com = new OleDbCommand("delete from departman WHERE ((departman.departmanID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", silinecekler._departmanID);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void departmanguncelle(Edepartman guncellenecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand com = new OleDbCommand("update departman set adi=@adi,aciklama=@aciklama where WHERE ((departman.departmanID)=@ID)", con);
            com.Parameters.AddWithValue("@adi", guncellenecekler.Adi);
            com.Parameters.AddWithValue("@aciklama", guncellenecekler.Aciklama);
            com.Parameters.AddWithValue("@ID", guncellenecekler._departmanID);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}