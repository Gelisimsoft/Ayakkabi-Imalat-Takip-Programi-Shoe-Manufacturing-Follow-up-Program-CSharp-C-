using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FSatis : IDisposable
    {
        public static void Sekle(ESatis SatisFtEkle)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbTransaction tran = con.BeginTransaction();
            OleDbCommand com1 = new OleDbCommand("insert into satislar(satistarih,ftno,birim,miktar,tutar,kdv,geneltoplam,aciklama,musteriID,stokid) values(@satistarih,@ftno,@birim,@miktar,@tutar,@kdv,@geneltoplam,@aciklama,@musteriID,@stokid)", con);
            com1.Parameters.AddWithValue("@satistarih", SatisFtEkle._satistarih);
            com1.Parameters.AddWithValue("@ftno", SatisFtEkle._ftno);
            com1.Parameters.AddWithValue("@birim", SatisFtEkle._birim);
            com1.Parameters.AddWithValue("@miktar", SatisFtEkle._miktar);
            com1.Parameters.AddWithValue("@tutar", Convert.ToDouble(SatisFtEkle._tutar));
            com1.Parameters.AddWithValue("@kdv", Convert.ToDouble(SatisFtEkle._kdv));
            com1.Parameters.AddWithValue("@geneltoplam", Convert.ToDouble(SatisFtEkle._geneltoplam));
            com1.Parameters.AddWithValue("@aciklama", SatisFtEkle._aciklama);
            com1.Parameters.AddWithValue("@musteriID", SatisFtEkle._musteriID);
            com1.Parameters.AddWithValue("@stokid", SatisFtEkle._stokid);
            com1.Transaction = tran;

            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye+@tutar WHERE ((musteriler.musteriID)=@musteriID)", con);
            com2.Parameters.AddWithValue("@tutar", SatisFtEkle._geneltoplam);
            com2.Parameters.AddWithValue("@musteriID", SatisFtEkle._musteriID);
            com2.Transaction = tran;

            OleDbCommand com3 = new OleDbCommand("update stoklar set miktar=miktar-@miktar WHERE ((stoklar.stokid)=@sid)", con);
            com3.Parameters.AddWithValue("@miktar", SatisFtEkle._miktar);
            com3.Parameters.AddWithValue("@sid", SatisFtEkle._stokid);
            com3.Transaction = tran;
            try
            {
                if (com1.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(SatisFtEkle._ftno + " " + "Nolu Faturanın Kayıt İşlemi Yapılmıştır", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                tran.Commit();
            }
            catch (Exception Ex)
            {
                tran.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                con.Close();
            }
        }

        public static void Ssil(ESatis Silme)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbTransaction tracom = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("delete from satislar WHERE ((satislar.satisftID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Silme._satisftID);
            com.Transaction = tracom;

            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye-@ilkgirilentutar WHERE ((musteriler.musteriID)=@musteriID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", Silme._geneltoplam);
            com2.Parameters.AddWithValue("@musteriID", Silme._musteriID);
            com2.Transaction = tracom;

            OleDbCommand com3 = new OleDbCommand("update stoklar set miktar=miktar+@miktar WHERE ((stoklar.stokid)=@stokid)", con);
            com3.Parameters.AddWithValue("@miktar", Silme._miktar);
            com3.Parameters.AddWithValue("@stokid", Silme._stokid);
            com3.Transaction = tracom;

            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(Silme._ftno + " " + "Nolu Faturanın Silme İşlemi Yapılmıştır", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                tracom.Commit();
            }
            catch (Exception Ex)
            {
                tracom.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void SGuncelle(ESatis Guncelle)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //Veri tabanın'dan önceden girilen bilgileri yakalamamız lazım.Bunun için ne yapcaz hacı ? Saksıyı çalıştırıp ilgili kodları yazıp ilgili bilgileri çekcez.
            //Hacı önce miktarı çekicez ilgili değişkene zıplatıcaz.

            #region MiktariYakaliyoruz

            OleDbDataAdapter dam = new OleDbDataAdapter("select miktar,stokid from satislar WHERE ((satislar.satisftID)=@ID)", con);
            dam.SelectCommand.Parameters.AddWithValue("@ID", Guncelle._satisftID);
            DataTable dttem = new DataTable();
            dam.Fill(dttem);
            double oncedengirilenmiktar = Convert.ToDouble(dttem.Rows[0]["miktar"].ToString());
            int eskidicik = Convert.ToInt32(dttem.Rows[0]["stokid"].ToString());

            #endregion MiktariYakaliyoruz

            //hacı şimdide önceden girilen bakiyeyi çekelim ki, ne bakiye yazacağımızı bilelim

            #region BakiyeyiYakiliyoruz

            OleDbDataAdapter damidom = new OleDbDataAdapter("select geneltoplam,musteriID from satislar WHERE ((satislar.satisftID)=@ID)", con);
            damidom.SelectCommand.Parameters.AddWithValue("@ID", Guncelle._satisftID);
            DataTable datem = new DataTable();
            damidom.Fill(datem);
            double oncedengirilenbakiye = Convert.ToDouble(datem.Rows[0]["geneltoplam"].ToString());
            int eskitedid = Convert.ToInt32(datem.Rows[0]["musteriID"].ToString());

            #endregion BakiyeyiYakiliyoruz

            #region GuncellemeIslemlerineBasliyoruz

            OleDbTransaction transasor = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("update satislar set satistarih=@satistarih,ftno=@ftno,birim=@birim,miktar=@miktar,tutar=@tutar,kdv=@kdv,geneltoplam=@geneltoplam,aciklama=@aciklama,musteriID=@musteriID,stokid=@stokid WHERE ((satislar.satisftID)=@ID)", con);

            com.Parameters.AddWithValue("@satistarih", Guncelle._satistarih);
            com.Parameters.AddWithValue("@ftno", Guncelle._ftno);
            com.Parameters.AddWithValue("@birim", Guncelle._birim);
            com.Parameters.AddWithValue("@miktar", Guncelle._miktar);
            com.Parameters.AddWithValue("@tutar", Convert.ToDouble(Guncelle._tutar));
            com.Parameters.AddWithValue("@kdv", Convert.ToDouble(Guncelle._kdv));
            com.Parameters.AddWithValue("@geneltoplam", Convert.ToDouble(Guncelle._geneltoplam));
            com.Parameters.AddWithValue("@aciklama", Guncelle._aciklama);
            com.Parameters.AddWithValue("@musteriID", Guncelle._musteriID);
            com.Parameters.AddWithValue("@stokid", Guncelle._stokid);
            com.Parameters.AddWithValue("@ID", Guncelle._satisftID);
            com.Transaction = transasor;
            //Hacı şimdi daha öncesinden girilmiş olan satış faturasında bulunan miktarı yakalamıştık şimdi onu stoklar tablosunda ilgili stok tekrar ekliyoruz.Çünkü daha öncesinde o kadarlık miktarı sattığımız için azaltmıştık
            OleDbCommand com2 = new OleDbCommand("update stoklar set miktar=miktar+@miktar where stokid=@stokid", con);
            com2.Parameters.AddWithValue("@miktar", oncedengirilenmiktar);
            com2.Parameters.AddWithValue("@stokid", eskidicik);
            com2.Transaction = transasor;

            //Hacı hani bundan önceki com2 komutu ile stok arttırmıştık yaa şimdide kullanıcının yeni girdiği miktarı azaltıcaz ki güncelleme olsun daaaaa
            OleDbCommand com3 = new OleDbCommand("update stoklar set miktar=miktar-@miktar where stokid=@stokid", con);
            com3.Parameters.AddWithValue("@miktar", Guncelle._miktar);
            com3.Parameters.AddWithValue("@stokid", Guncelle._stokid);
            com3.Transaction = transasor;

            //Hacı şimdi geldi sıra bakiyeyi güncellemeye yine stok olduğu gibi düzenleyecez.Önceden girdiğimiz bakiyeyi yakaladığımız önceki nesne aracılığı ile silicez.
            OleDbCommand com4 = new OleDbCommand("update musteriler set bakiye=bakiye-@ilkgirilentutar where musteriID=@musteriID", con);
            com4.Parameters.AddWithValue("@ilkgirilentutar", oncedengirilenbakiye);
            com4.Parameters.AddWithValue("@musteriID", eskitedid);
            com4.Transaction = transasor;
            //Hacı şimdide kullanıcının yeni girdiği bakiyeyi ekleyecez.Hişşş sakin ol...
            OleDbCommand com5 = new OleDbCommand("update musteriler set bakiye=bakiye+@yenigirilentutar where musteriID=@musteriID", con);
            com5.Parameters.AddWithValue("@yenigirilentutar", Guncelle._geneltoplam);
            com5.Parameters.AddWithValue("@musteriID", Guncelle._musteriID);
            com5.Transaction = transasor;
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            if (com4.ExecuteNonQuery() > 0)
                            {
                                if (com5.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show(Guncelle._ftno + " " + "Nolu Faturanın Güncelle İşlemi Yapılmıştır", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                transasor.Commit();
            }
            catch (Exception Ex)
            {
                transasor.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }

            #endregion GuncellemeIslemlerineBasliyoruz
        }

        public static DataTable Sekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satislar.satistarih, satislar.ftno, satislar.birim, satislar.miktar, satislar.tutar, satislar.kdv, satislar.geneltoplam, satislar.aciklama, musteriler.unvan, stoklar.stokadi FROM (satislar INNER JOIN musteriler ON satislar.musteriID = musteriler.musteriID) INNER JOIN stoklar ON satislar.stokid = stoklar.stokid", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static void OComboT(ESatis SmusteriDegisir)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbDataAdapter da = new OleDbDataAdapter("select geneltoplam,musteriID from satislar WHERE ((satislar.satisftID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", SmusteriDegisir._satisftID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["geneltoplam"].ToString());
            int eskiID = Convert.ToInt32(dt.Rows[0]["musteriID"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbCommand com2 = new OleDbCommand("update musteriler set bakiye=bakiye-@ilkgirilentutar WHERE (musteriler.musteriID)=@musteriID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", fark);
            com2.Parameters.AddWithValue("@musteriID", eskiID);
            com2.ExecuteNonQuery();
            con.Close();
        }

        public static void OComboS(ESatis SStokDegisir)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbDataAdapter da = new OleDbDataAdapter("select miktar,stokid from satislar WHERE ((satislar.satisftID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", SStokDegisir._satisftID);
            DataTable dtttt = new DataTable();
            da.Fill(dtttt);
            double fark = Convert.ToDouble(dtttt.Rows[0]["miktar"].ToString());
            int eskiID = Convert.ToInt32(dtttt.Rows[0]["stokid"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbCommand com2 = new OleDbCommand("update stoklar set miktar=miktar-@fark WHERE ((stoklar.stokid)=@stokid)", con);
            com2.Parameters.AddWithValue("@fark", fark);
            com2.Parameters.AddWithValue("@stokid", eskiID);
            com2.ExecuteNonQuery();
            con.Close();
        }

        public static DataTable MEkstre(ESatis GelMus)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satislar.satistarih, satislar.ftno, satislar.birim, satislar.miktar, satislar.tutar, satislar.kdv, satislar.geneltoplam, satislar.aciklama, musteriler.unvan, stoklar.stokadi FROM (satislar INNER JOIN musteriler ON satislar.musteriID = musteriler.musteriID) INNER JOIN stoklar ON satislar.stokid = stoklar.stokid WHERE  ((satislar.musteriID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", GelMus._musteriID);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable StokEstre(ESatis Gelstok)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satislar.satistarih, satislar.ftno, satislar.birim, satislar.miktar, satislar.tutar, satislar.kdv, satislar.geneltoplam, satislar.aciklama, musteriler.unvan, stoklar.stokadi FROM (satislar INNER JOIN musteriler ON satislar.musteriID = musteriler.musteriID) INNER JOIN stoklar ON satislar.stokid = stoklar.stokid WHERE ((satislar.stokid)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Gelstok._stokid);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable FtnoEstre(ESatis Gelftno)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satislar.satistarih, satislar.ftno, satislar.birim, satislar.miktar, satislar.tutar, satislar.kdv, satislar.geneltoplam, satislar.aciklama, musteriler.unvan, stoklar.stokadi FROM (satislar INNER JOIN musteriler ON satislar.musteriID = musteriler.musteriID) INNER JOIN stoklar ON satislar.stokid = stoklar.stokid WHERE (((satislar.ftno) Like @ftno))", con);
            com.Parameters.AddWithValue("@ftno", Gelftno._ftno);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion IDisposable Members
    }
}