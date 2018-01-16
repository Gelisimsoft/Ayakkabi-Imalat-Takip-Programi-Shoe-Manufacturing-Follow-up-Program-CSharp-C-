using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FSatinalma : IDisposable
    {
        public static void Sekle(ESatinalma SatinalmaFtEkle)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbTransaction tran = con.BeginTransaction();
            OleDbCommand com1 = new OleDbCommand("Insert Into satinalma(alistarih,ftno,birim,miktar,tutar,kdv,geneltoplam,aciklama,TedarikciID,stokid) values(@alistarih,@ftno,@birim,@miktar,@tutar,@kdv,@geneltoplam,@aciklama,@TedarikciID,@stokid)", con);
            com1.Parameters.AddWithValue("@alistarih", Convert.ToDateTime(SatinalmaFtEkle._alistarih));
            com1.Parameters.AddWithValue("@ftno", SatinalmaFtEkle._ftno);
            com1.Parameters.AddWithValue("@birim", SatinalmaFtEkle._birim);
            com1.Parameters.AddWithValue("@miktar", SatinalmaFtEkle._miktar);
            com1.Parameters.AddWithValue("@tutar", SatinalmaFtEkle._tutar);
            com1.Parameters.AddWithValue("@kdv", SatinalmaFtEkle._kdv);
            com1.Parameters.AddWithValue("@geneltoplam", SatinalmaFtEkle._geneltoplam);
            com1.Parameters.AddWithValue("@aciklama", SatinalmaFtEkle._aciklama);
            com1.Parameters.AddWithValue("@TedarikciID", SatinalmaFtEkle._TedarikciID);
            com1.Parameters.AddWithValue("@stokid", SatinalmaFtEkle._stokid);
            com1.Transaction = tran;
            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye+@tutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@tutar", SatinalmaFtEkle._geneltoplam);
            com2.Parameters.AddWithValue("@TedarikciID", SatinalmaFtEkle._TedarikciID);
            com2.Transaction = tran;
            OleDbCommand com3 = new OleDbCommand("update stoklar set miktar=miktar+@miktar WHERE ((stoklar.stokid)=@stokid)", con);
            com3.Parameters.AddWithValue("@miktar", SatinalmaFtEkle._miktar);
            com3.Parameters.AddWithValue("@stokid", SatinalmaFtEkle._stokid);
            com3.Transaction = tran;
            try
            {
                if (com1.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(SatinalmaFtEkle._ftno + " " + "Nolu Faturanın Kayıt İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public static void Ssil(ESatinalma Silme)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            #region VeriTabanindanStokBilgisiniCekiyoruz

            //Stok miktarını çekecek ilgili OleDbDataAdapter yazıyoruz.
            OleDbDataAdapter da = new OleDbDataAdapter("select miktar from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", Silme._alisftID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["miktar"].ToString());

            #endregion VeriTabanindanStokBilgisiniCekiyoruz

            #region VeriTabanindanTedarikciTablosundanBakiyeyiCekiyoruz

            //Genel Toplam miktarını çekecek ilgili OleDbDataAdapter yazıyoruz.
            OleDbDataAdapter da2 = new OleDbDataAdapter("select geneltoplam from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            da2.SelectCommand.Parameters.AddWithValue("@ID", Silme._alisftID);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            double bakiyefark = Convert.ToDouble(dt2.Rows[0]["geneltoplam"].ToString());

            #endregion VeriTabanindanTedarikciTablosundanBakiyeyiCekiyoruz

            OleDbTransaction trastor = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("delete from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Silme._alisftID);
            com.Transaction = trastor;
            OleDbCommand com1 = new OleDbCommand("update stoklar set miktar=miktar-@fark WHERE ((stoklar.stokid)=@stokid)", con);
            com1.Parameters.AddWithValue("@fark", fark);
            com1.Parameters.AddWithValue("@stokid", Silme._stokid);
            com1.Transaction = trastor;
            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye-@ilkgirilentutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", bakiyefark);
            com2.Parameters.AddWithValue("@TedarikciID", Silme._TedarikciID);
            com2.Transaction = trastor;
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com1.ExecuteNonQuery() > 0)
                    {
                        if (com2.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(Silme._ftno + " " + "Nolu Faturanın Silme İşlemi Yapılmıştır", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                trastor.Commit();
            }
            catch (Exception Ex)
            {
                trastor.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void SGuncelle(ESatinalma ftGuncelle)
        {
            OleDbCommand com, com1, com2, com3, com4;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            //Veritabanından stok miktarını çekiyoruz.

            #region VeriTabanindanStokBilgisiniCekiyoruz

            //Stok miktarını çekecek ilgili OleDbDataAdapter yazıyoruz.
            OleDbDataAdapter da = new OleDbDataAdapter("select miktar,stokid from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", ftGuncelle._alisftID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["miktar"].ToString());
            int eskistokid = Convert.ToInt32(dt.Rows[0]["stokid"].ToString());

            #endregion VeriTabanindanStokBilgisiniCekiyoruz

            #region VeriTabanindanTedarikciTablosundanBakiyeyiCekiyoruz

            //Toplam tutarı çekecek ilgili OleDbDataAdapter yazıyoruz.
            OleDbDataAdapter da2 = new OleDbDataAdapter("select TedarikciID,geneltoplam from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            da2.SelectCommand.Parameters.AddWithValue("@ID", ftGuncelle._alisftID);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            double bakiyefark = Convert.ToDouble(dt2.Rows[0]["geneltoplam"].ToString());
            int eskitedarikciid = Convert.ToInt32(dt2.Rows[0]["TedarikciID"].ToString());

            #endregion VeriTabanindanTedarikciTablosundanBakiyeyiCekiyoruz

            //Bağlantıyı kontrol ediyoruz.Açık mı diye
            //Transaction yazmaya başlıyoruz.Birden fazla store produce çalıştırabilmek için
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbTransaction trancim = con.BeginTransaction();

            #region SatinalmaFtGuncellemeIslemleri

            com = new OleDbCommand("update satinalma set alistarih=@alistarih,ftno=@ftno,birim=@birim,miktar=@miktar,tutar=@tutar,kdv=@kdv,geneltoplam=@geneltoplam,aciklama=@aciklama,TedarikciID=@TedarikciID,stokid=@stokid where alisftID=@ID", con);
            com.Parameters.AddWithValue("@alistarih", ftGuncelle._alistarih);
            com.Parameters.AddWithValue("@ftno", ftGuncelle._ftno);
            com.Parameters.AddWithValue("@birim", ftGuncelle._birim);
            com.Parameters.AddWithValue("@miktar", ftGuncelle._miktar);
            com.Parameters.AddWithValue("@tutar", ftGuncelle._tutar);
            com.Parameters.AddWithValue("@kdv", ftGuncelle._kdv);
            com.Parameters.AddWithValue("@geneltoplam", ftGuncelle._geneltoplam);
            com.Parameters.AddWithValue("@aciklama", ftGuncelle._aciklama);
            com.Parameters.AddWithValue("@TedarikciID", ftGuncelle._TedarikciID);
            com.Parameters.AddWithValue("@stokid", ftGuncelle._stokid);
            com.Parameters.AddWithValue("@ID", ftGuncelle._alisftID);
            com.Transaction = trancim;

            #endregion SatinalmaFtGuncellemeIslemleri

            #region StokMiktarindaIlgiliDegisiklerIcinIslemler

            //Satinalma formu için gerekli store produce yazıyoruz.
            //Ilk önce veritabanından daha önce girilen stok miktarını eksiltiyoruz.fark nesnesine OleDbdataadapter ile getirdiğimiz veriyi işliyoruz
            com1 = new OleDbCommand("update stoklar set miktar=miktar-@fark where stokid=@stokid", con);
            com1.Parameters.AddWithValue("@fark", fark);
            com1.Parameters.AddWithValue("@stokid", eskistokid);
            com1.Transaction = trancim;
            //2.İşlem olarak yeni girilen miktarı işliyoruz.
            com2 = new OleDbCommand("update stoklar set miktar=miktar+@yenigirilenmiktar where stokid=@stokid", con);
            com2.Parameters.AddWithValue("@yenigirilenmiktar", ftGuncelle._miktar);
            com2.Parameters.AddWithValue("@stokid", ftGuncelle._stokid);
            com2.Transaction = trancim;

            #endregion StokMiktarindaIlgiliDegisiklerIcinIslemler

            #region TedarikciCarisiIcinGerekliStoreProduce

            //Önceden satınalma menüsünden girilen tutar miktar alınarak store produce yazmak için ilgili nesneye aktarılacaktır.
            //ilk girilen tutar geri çekilip siliniyor.
            com3 = new OleDbCommand("update tedarikciler set bakiye=bakiye-@ilkgirilentutar where TedarikciID=@TedarikciID", con);
            com3.Parameters.AddWithValue("@ilkgirilentutar", bakiyefark);
            com3.Parameters.AddWithValue("@TedarikciID", eskitedarikciid);
            com3.Transaction = trancim;
            //2.İşlem olarak yeni girilen tutarı işliyoruz.
            com4 = new OleDbCommand("update tedarikciler set bakiye=bakiye+@yenigirilentutar where TedarikciID=@TedarikciID", con);
            com4.Parameters.AddWithValue("@yenigirilentutar", ftGuncelle._geneltoplam);
            com4.Parameters.AddWithValue("@TedarikciID", ftGuncelle._TedarikciID);
            com4.Transaction = trancim;

            #endregion TedarikciCarisiIcinGerekliStoreProduce

            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com1.ExecuteNonQuery() > 0)
                    {
                        if (com2.ExecuteNonQuery() > 0)
                        {
                            if (com3.ExecuteNonQuery() > 0)
                            {
                                if (com4.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show(ftGuncelle._ftno + " " + "Numaralı Fatura Güncellenmiştir.", "Güncelleme Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                trancim.Commit();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }//Sonuç ne olursa olsun bağlantıyı kapatıyoruz
            finally { con.Close(); }
        }

        public static DataTable Sekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satinalma.alistarih, satinalma.ftno, satinalma.birim, satinalma.miktar, satinalma.tutar, satinalma.kdv, satinalma.geneltoplam, satinalma.aciklama, stoklar.stokadi, tedarikciler.unvan FROM (satinalma INNER JOIN stoklar ON satinalma.stokid = stoklar.stokid) INNER JOIN tedarikciler ON satinalma.TedarikciID = tedarikciler.TedarikciID", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static void OComboT(ESatinalma STedarikciDegisir)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbDataAdapter da = new OleDbDataAdapter("select geneltoplam,TedarikciID from satinalma WHERE ((satinalma.alisftID)=@ID)k", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", STedarikciDegisir._alisftID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["geneltoplam"].ToString());
            int eskiID = Convert.ToInt32(dt.Rows[0]["TedarikciID"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye-@ilkgirilentutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", fark);
            com2.Parameters.AddWithValue("@TedarikciID", eskiID);
            con.Close();
        }

        public static void OComboS(ESatinalma SStokDegisir)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbDataAdapter da = new OleDbDataAdapter("select miktar,stokid from satinalma WHERE ((satinalma.alisftID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", SStokDegisir._alisftID);
            DataTable dtttt = new DataTable();
            da.Fill(dtttt);
            double fark = Convert.ToDouble(dtttt.Rows[0]["miktar"].ToString());
            int eskiID = Convert.ToInt32(dtttt.Rows[0]["stokid"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbCommand com2 = new OleDbCommand("update stoklar set miktar=miktar-@fark WHERE ((stoklar.stokid)=@stokid)", con);
            com2.Parameters.AddWithValue("@fark", fark);
            com2.Parameters.AddWithValue("@stokid", eskiID);
            con.Close();
        }

        public static DataTable Tedekstre(ESatinalma Gelted)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satinalma.alistarih, satinalma.ftno, satinalma.birim, satinalma.miktar, satinalma.tutar, satinalma.kdv, satinalma.geneltoplam, satinalma.aciklama, stoklar.stokadi, tedarikciler.unvan FROM (satinalma INNER JOIN stoklar ON satinalma.stokid = stoklar.stokid) INNER JOIN tedarikciler ON satinalma.TedarikciID = tedarikciler.TedarikciID WHERE ((satinalma.TedarikciID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Gelted._TedarikciID);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable StokEstre(ESatinalma Gelstok)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satinalma.alistarih, satinalma.ftno, satinalma.birim, satinalma.miktar, satinalma.tutar, satinalma.kdv, satinalma.geneltoplam, satinalma.aciklama, stoklar.stokadi, tedarikciler.unvan FROM (satinalma INNER JOIN stoklar ON satinalma.stokid = stoklar.stokid) INNER JOIN tedarikciler ON satinalma.TedarikciID = tedarikciler.TedarikciID WHERE ((satinalma.stokid)=@ID)", con);
            com.Parameters.AddWithValue("@ID", Gelstok._stokid);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable FtnoEstre(ESatinalma Gelftno)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT satinalma.alistarih, satinalma.ftno, satinalma.birim, satinalma.miktar, satinalma.tutar, satinalma.kdv, satinalma.geneltoplam, satinalma.aciklama, stoklar.stokadi, tedarikciler.unvan FROM (satinalma INNER JOIN stoklar ON satinalma.stokid = stoklar.stokid) INNER JOIN tedarikciler ON satinalma.TedarikciID = tedarikciler.TedarikciID WHERE (((satinalma.ftno) Like @ftno))", con);
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
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}