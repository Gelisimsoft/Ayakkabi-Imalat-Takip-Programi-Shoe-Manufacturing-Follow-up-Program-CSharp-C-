using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FTahsilat : IDisposable
    {
        public static void TEkle(ETahsilat Ekle)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbTransaction trancim = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("insert into Tahsilatlar(mkzno,tarih,tutar,aciklama,musteriID) values(@mkzno,@tarih,@tutar,@aciklama,@musteriID)", con);
            com.Parameters.AddWithValue("@mkzno", Ekle.Mzkno);
            com.Parameters.AddWithValue("@tarih", Ekle.Tarih);
            com.Parameters.AddWithValue("@tutar", Ekle.Tutar);
            com.Parameters.AddWithValue("@aciklama", Ekle.Aciklama);
            com.Parameters.AddWithValue("@musteriID", Ekle.MusteriID);
            com.Transaction = trancim;

            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye-@ilkgirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", Ekle.Tutar);
            com2.Parameters.AddWithValue("@musteriID", Ekle.MusteriID);
            com2.Transaction = trancim;

            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(Ekle.Mzkno + " " + "Nolu Tahsilat Makbuzu Kaydedilmiştir.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                trancim.Commit();
            }
            catch (Exception Ex)
            {
                trancim.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void TGuncelle(ETahsilat GuncelleGayriBilgileri)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region HaciOncedenGirilenTutariEskiMakbuzdanAliyoruz

            OleDbDataAdapter dap = new OleDbDataAdapter("SELECT Tahsilatlar.musteriID, Tahsilatlar.tutar FROM Tahsilatlar WHERE (((Tahsilatlar.tahsilatID)=@ID))", con);
            dap.SelectCommand.Parameters.AddWithValue("@ID", GuncelleGayriBilgileri.TahsilatID);
            DataTable dttt = new DataTable();
            dap.Fill(dttt);
            double fark = Convert.ToDouble(dttt.Rows[0]["tutar"].ToString());
            int iskiIDlerimeBakayim = Convert.ToInt32(dttt.Rows[0]["musteriID"].ToString());

            #endregion HaciOncedenGirilenTutariEskiMakbuzdanAliyoruz

            OleDbTransaction trancimiz = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("update Tahsilatlar set mkzno=@mkzno,tarih=@tarih,tutar=@tutar,aciklama=@aciklama,musteriID=@musteriID WHERE ((Tahsilatlar.tahsilatID)=@ID)", con, trancimiz);
            com.Parameters.AddWithValue("@mkzno", GuncelleGayriBilgileri.Mzkno);
            com.Parameters.AddWithValue("@tarih", GuncelleGayriBilgileri.Tarih);
            com.Parameters.AddWithValue("@tutar", GuncelleGayriBilgileri.Tutar);
            com.Parameters.AddWithValue("@aciklama", GuncelleGayriBilgileri.Aciklama);
            com.Parameters.AddWithValue("@musteriID", GuncelleGayriBilgileri.MusteriID);
            com.Parameters.AddWithValue("@ID", GuncelleGayriBilgileri.TahsilatID);
            //com.Transaction = trancimiz;
            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye+@yenigirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con, trancimiz);
            com2.Parameters.AddWithValue("@yenigirilentutar", fark);
            com2.Parameters.AddWithValue("@musteriID", iskiIDlerimeBakayim);
            OleDbCommand com3 = new OleDbCommand("update musteriler set bakiye=bakiye-@ilkgirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con, trancimiz);
            com3.Parameters.AddWithValue("@ilkgirilentutar", GuncelleGayriBilgileri.Tutar);
            com3.Parameters.AddWithValue("@musteriID", GuncelleGayriBilgileri.MusteriID);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(GuncelleGayriBilgileri.Mzkno + " " + "Nolu Ödeme Makbuzu Güncellenmiştir.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                trancimiz.Commit();
            }
            catch (Exception Ex)
            {
                trancimiz.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void TSilme(ETahsilat GilGayriBilgileri)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region HaciOncedenGirilenTahsilatMakbuzuTutariYakala

            OleDbDataAdapter dap = new OleDbDataAdapter("SELECT Tahsilatlar.tahsilatID, Tahsilatlar.tutar FROM Tahsilatlar WHERE (((Tahsilatlar.tahsilatID)=@ID))", con);
            dap.SelectCommand.Parameters.AddWithValue("@ID", GilGayriBilgileri.TahsilatID);
            DataTable dtt = new DataTable();
            dap.Fill(dtt);
            double fark = Convert.ToDouble(dtt.Rows[0]["tutar"].ToString());

            #endregion HaciOncedenGirilenTahsilatMakbuzuTutariYakala

            OleDbTransaction troo = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("delete from Tahsilatlar WHERE ((Tahsilatlar.tahsilatID)=@ID)", con, troo);
            com.Parameters.AddWithValue("@ID", GilGayriBilgileri.TahsilatID);
            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye+@yenigirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con, troo);
            com2.Parameters.AddWithValue("@yenigirilentutar", fark);
            com2.Parameters.AddWithValue("@musteriID", GilGayriBilgileri.MusteriID);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(GilGayriBilgileri.Mzkno + " " + "Nolu Tahsilat Makbuzunun Silme İşlemi Yapılmıştır", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                troo.Commit();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                troo.Rollback();
            }
        }

        public static void TCombo(ETahsilat GayriDegistirMusteriyi)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region comboboxdadeğişimolursaburasicalisacak

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Tahsilatlar.musteriID, Tahsilatlar.tutar FROM Tahsilatlar WHERE (((Tahsilatlar.tahsilatID)=@ID))", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", GayriDegistirMusteriyi.TahsilatID);
            DataTable combodt = new DataTable();
            da.Fill(combodt);
            int eskiciIDGeldiHanim = Convert.ToInt32(combodt.Rows[0]["musteriID"].ToString());
            double Eskiparalaralinir = Convert.ToDouble(combodt.Rows[0]["tutar"].ToString());
            //Şimdi değişimde eski seçilmiş olan cariyi yakalayıp bakiyesini silecek olan store produce yazıyoruz
            OleDbCommand comcucom = new OleDbCommand("update musteriler set bakiye=bakiye+@yenigirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con);
            comcucom.Parameters.AddWithValue("@yenigirilentutar", Eskiparalaralinir);
            comcucom.Parameters.AddWithValue("@musteriID", eskiciIDGeldiHanim);

            #endregion comboboxdadeğişimolursaburasicalisacak

            con.Close();
        }

        public static DataTable TEkstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT Tahsilatlar.mkzno, Tahsilatlar.tarih, Tahsilatlar.tutar, Tahsilatlar.aciklama, musteriler.unvan FROM musteriler INNER JOIN Tahsilatlar ON musteriler.musteriID = Tahsilatlar.musteriID", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
            con.Close();
            return dtt;
        }

        public static DataTable TEkstreByMusteri(ETahsilat ByMusteri)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand mustericom = new OleDbCommand("SELECT Tahsilatlar.mkzno, Tahsilatlar.tarih, Tahsilatlar.tutar, Tahsilatlar.aciklama, musteriler.unvan FROM musteriler INNER JOIN Tahsilatlar ON musteriler.musteriID = Tahsilatlar.musteriID WHERE (((Tahsilatlar.musteriID)=@ID))", con);
            mustericom.Parameters.AddWithValue("@ID", ByMusteri.MusteriID);
            OleDbDataAdapter dap = new OleDbDataAdapter(mustericom);
            DataTable dtt = new DataTable();
            dap.Fill(dtt);
            con.Close();
            return dtt;
        }

        public static DataTable TEkstreBymkzno(ETahsilat Bymkzno)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand mustericom = new OleDbCommand("SELECT Tahsilatlar.mkzno, Tahsilatlar.tarih, Tahsilatlar.tutar, Tahsilatlar.aciklama, musteriler.unvan FROM musteriler INNER JOIN Tahsilatlar ON musteriler.musteriID = Tahsilatlar.musteriID WHERE (((Tahsilatlar.tahsilatID)=@ID))", con);
            mustericom.Parameters.AddWithValue("@ID", Bymkzno.TahsilatID);
            OleDbDataAdapter dap = new OleDbDataAdapter(mustericom);
            DataTable dtt = new DataTable();
            dap.Fill(dtt);
            con.Close();
            return dtt;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}