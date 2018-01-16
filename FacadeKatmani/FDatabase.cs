using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FDatabase : IDisposable
    {
        public static void Ekle(EDatabase Geliyor)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand com = new OleDbCommand("insert into Databasem (Ad,Tarih,Timecik) values(@Ad,@Tarih,@Timecik)", con);
            com.Parameters.AddWithValue("@Ad", Geliyor.Ad);
            com.Parameters.AddWithValue("@Tarih", Geliyor.Tarih);
            com.Parameters.AddWithValue("@Timecik", Geliyor.Timecik);
            if (com.Connection.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                con.Close();
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