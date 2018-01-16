using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public class FMusteri : IDisposable
    {
        public static void MKayiTEkle(EMusteri MusteriBilgileri)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand ekle = new OleDbCommand("insert into musteriler(unvan,adres,sehir,telefon,faks,vdairesi,vno) values(@unvan,@adres,@sehir,@telefon,@faks,@vdairesi,@vno)", baglanti);
            ekle.Parameters.AddWithValue("@unvan", MusteriBilgileri._unvan);
            ekle.Parameters.AddWithValue("@adres", MusteriBilgileri._adres);
            ekle.Parameters.AddWithValue("@sehir", MusteriBilgileri._sehir);
            ekle.Parameters.AddWithValue("@telefon", MusteriBilgileri._telefon);
            ekle.Parameters.AddWithValue("@faks", MusteriBilgileri._faks);
            ekle.Parameters.AddWithValue("@vdairesi", MusteriBilgileri._vergidairesi);
            ekle.Parameters.AddWithValue("@vno", MusteriBilgileri._vergino);
            if (ekle.Connection.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            try
            {
                if (ekle.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt İşlemi Yapılamadı !!!", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { baglanti.Close(); }
        }

        public static DataTable MKayitlariGetir()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT musteriler.unvan, musteriler.adres, musteriler.sehir, musteriler.telefon, musteriler.faks, musteriler.vdairesi, musteriler.vno, musteriler.bakiye FROM musteriler WHERE (((musteriler.IsActive)=True)) order by musteriler.unvan", con);
            OleDbDataAdapter dep = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            dep.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable MKayitGetirByID(EMusteri SeciliOlan)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Close();
            }
            OleDbCommand com2 = new OleDbCommand("SELECT musteriler.unvan, musteriler.adres, musteriler.sehir, musteriler.telefon, musteriler.faks, musteriler.vdairesi, musteriler.vno, musteriler.bakiye, musteriler.musteriID FROM musteriler WHERE (((musteriler.musteriID)=@ID))", con);
            com2.Parameters.AddWithValue("@ID", SeciliOlan.MusteriID);
            OleDbDataAdapter adep = new OleDbDataAdapter(com2);
            DataTable det = new DataTable();
            adep.Fill(det);
            con.Close();
            return det;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}