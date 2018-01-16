using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FStok : IDisposable
    {
        public static void Sekle(EStok Stoktakiler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("insert into stoklar(stokadi,stokaciklama,stokkodu,miktar) values(@stokadi,@stokaciklama,@stokkodu,@miktar)", con);
            com.Parameters.AddWithValue("@stokadi", Stoktakiler._stokadi);
            com.Parameters.AddWithValue("@stokaciklama", Stoktakiler._stokaciklama);
            com.Parameters.AddWithValue("@stokkodu", Stoktakiler._stokkodu);
            com.Parameters.AddWithValue("@miktar", Stoktakiler._bakiye);
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
            finally { con.Close(); }
        }

        public static void Ssil(EStok Silinecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("update stoklar set stoklar.IsActive=0 WHERE ((stoklar.stokid)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Silinecekler._stokid);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Stok Kartı Silme İşlemi Yapılmıştır.", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Stok Kartı Silinemez.Silmek İstediğiniz Stok Kartı Kullanılmaktadır.\n - Stok Maliyetlendirme İşlemleri\n - Satış Faturası\n - Satınalma Faturası", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { con.Close(); }
        }

        public static void Sguncelle(EStok guncellenecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("Update stoklar set stokadi=@adi,stokaciklama=@aciklama,stokkodu=@stokkodu,miktar=@miktar WHERE ((stoklar.stokid)=@stokID)", con);
            com.Parameters.AddWithValue("@adi", guncellenecekler._stokadi);
            com.Parameters.AddWithValue("@aciklama", guncellenecekler._stokaciklama);
            com.Parameters.AddWithValue("@stokkodu", guncellenecekler._stokkodu);
            com.Parameters.AddWithValue("@miktar", guncellenecekler._bakiye);
            com.Parameters.AddWithValue("@stokID", guncellenecekler._stokid);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Güncelleme İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static DataTable sekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select * from stoklar WHERE ((stoklar.IsActive)=True) order by stoklar.stokkodu", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable StokID(EStok GelIDim)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select * from stoklar WHERE ((stoklar.stokid)=@ID)", con);
            com.Parameters.AddWithValue("@ID", GelIDim._stokid);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable StokName(EStok GelIDim)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT * FROM stoklar WHERE (((stoklar.IsActive)=True) AND ((stoklar.stokadi) Like @ad))", con);
            com.Parameters.AddWithValue("@ad", GelIDim._stokadi + "%");
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}