using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public class FTedarikci : IDisposable
    {
        public static void TKayiTEkle(ETedarikci TedarikciBilgileri)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand ekle = new OleDbCommand("insert into tedarikciler(unvan,adres,sehir,telefon,faks,vergidairesi,vergino) values(@unvan,@adres,@sehir,@telefon,@faks,@vdairesi,@vno)", baglanti);
            ekle.Parameters.AddWithValue("@unvan", TedarikciBilgileri._unvan);
            ekle.Parameters.AddWithValue("@adres", TedarikciBilgileri._adres);
            ekle.Parameters.AddWithValue("@sehir", TedarikciBilgileri._sehir);
            ekle.Parameters.AddWithValue("@telefon", TedarikciBilgileri._telefon);
            ekle.Parameters.AddWithValue("@faks", TedarikciBilgileri._faks);
            ekle.Parameters.AddWithValue("@vdairesi", TedarikciBilgileri._vergidairesi);
            ekle.Parameters.AddWithValue("@vno", TedarikciBilgileri._vergino);
            try
            {
                if (ekle.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { baglanti.Close(); }
        }

        public static DataTable TKayitlariGetir()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand comT = new OleDbCommand("SELECT tedarikciler.unvan, tedarikciler.adres, tedarikciler.sehir, tedarikciler.telefon, tedarikciler.faks, tedarikciler.vergidairesi, tedarikciler.vergino, tedarikciler.bakiye FROM tedarikciler WHERE (((tedarikciler.IsActive)=True)) order by unvan", baglanti);
            OleDbDataAdapter depT = new OleDbDataAdapter(comT);
            DataTable dtT = new DataTable();
            depT.Fill(dtT);
            baglanti.Close();
            return dtT;
        }

        public static DataTable TKayitGetirByID(ETedarikci SeciliOlan)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand Tcom2 = new OleDbCommand("SELECT tedarikciler.unvan, tedarikciler.adres, tedarikciler.sehir, tedarikciler.telefon, tedarikciler.faks, tedarikciler.vergidairesi, tedarikciler.vergino, tedarikciler.bakiye FROM tedarikciler WHERE (((tedarikciler.TedarikciID)=@ID))", baglanti);
            Tcom2.Parameters.AddWithValue("@ID", SeciliOlan._TedarikciID);
            OleDbDataAdapter adepT = new OleDbDataAdapter(Tcom2);
            DataTable Tdet = new DataTable();
            adepT.Fill(Tdet);
            baglanti.Close();
            return Tdet;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}