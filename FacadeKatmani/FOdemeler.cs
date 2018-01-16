using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FOdemeler : IDisposable
    {
        public static void OEkle(EOdemeler OdemeYeni)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbTransaction tranco = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("insert into Odemeler(mkzno,tarih,tutar,aciklama,TedarikciID) values(@mkzno,@tarih,@tutar,@aciklama,@TedarikciID)", con);
            com.Parameters.AddWithValue("@mkzno", OdemeYeni.Mzkno);
            com.Parameters.AddWithValue("@tarih", OdemeYeni.Tarih);
            com.Parameters.AddWithValue("@tutar", OdemeYeni.Tutar);
            com.Parameters.AddWithValue("@aciklama", OdemeYeni.Aciklama);
            com.Parameters.AddWithValue("@TedarikciID", OdemeYeni.TedarikciID);
            com.Transaction = tranco;

            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye-@ilkgirilentutar WHERE  ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@ilkgirilentutar", OdemeYeni.Tutar);
            com2.Parameters.AddWithValue("@TedarikciID", OdemeYeni.TedarikciID);
            com2.Transaction = tranco;
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(OdemeYeni.Mzkno + " " + "Nolu Ödeme Makbuzu Kaydedilmiştir.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                tranco.Commit();
            }
            catch (Exception Ex)
            {
                tranco.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void OEkstre(EOdemeler OdemeListe)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select mkzno,tarih,tutar,aciklama,unvan from Odemeler inner join tedarikciler on Odemeler.TedarikciID=tedarikciler.TedarikciID", con);
            OleDbDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ListViewItem listem = new ListViewItem();
                    listem.Text = dr["mkzno"].ToString();
                    listem.SubItems.Add(dr["tarih"].ToString());
                    listem.SubItems.Add(dr["tutar"].ToString());
                    listem.SubItems.Add(dr["unvan"].ToString());
                    listem.SubItems.Add(dr["aciklama"].ToString());
                }
            }
            con.Close();
        }

        public static void OGuncelle(EOdemeler OdememGuncelle)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            //Öncelikle kullanıcının ilk girmiş olduğu bakiyeyi yakalamak için tekrar ödemeler tablosuna bağlanıp ilgili tutarı yakalayıp onuda double cinsinden fark nesnesine atıyoruz kii, ilgili store produce çalışsın.
            OleDbDataAdapter da = new OleDbDataAdapter("select tutar,TedarikciID from odemeler WHERE ((odemeler.odemeID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", OdememGuncelle.odemeID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["tutar"].ToString());
            int eskiID = Convert.ToInt32(dt.Rows[0]["TedarikciID"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbTransaction trar = con.BeginTransaction();
            //Bu store produce ödemeler tablosunu güncelliyoruz sadece
            OleDbCommand com = new OleDbCommand("update Odemeler set mkzno=@mkzno,tarih=@tarih,tutar=@tutar,aciklama=@aciklama,TedarikciId=@TedarikciID WHERE ((odemeler.odemeID)=@ID)", con);
            com.Parameters.AddWithValue("@mkzno", OdememGuncelle.Mzkno);
            com.Parameters.AddWithValue("@tarih", OdememGuncelle.Tarih);
            com.Parameters.AddWithValue("@tutar", OdememGuncelle.Tutar);
            com.Parameters.AddWithValue("@aciklama", OdememGuncelle.Aciklama);
            com.Parameters.AddWithValue("@TedarikciID", OdememGuncelle.TedarikciID);
            com.Parameters.AddWithValue("@ID", OdememGuncelle.odemeID);
            com.Transaction = trar;
            //Burada bulunan store produ ise kullanıcının daha önceden girmiş olduğu bakiyeyi üstte bulunan kodlarla yakalamıştık şimdi bunu ilgili fonksiyonlara atayarak update işlemini gerçekleştiriyoruz.İlgili rakam kadar tedarikcinin bakiyesinden siliyoruz.
            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye+@yenigirilentutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@yenigirilentutar", fark);
            com2.Parameters.AddWithValue("@TedarikciID", eskiID);
            com2.Transaction = trar;
            //Bu store produce ise bir önceki produde tedarikciden silmiş olduğumuz bakiyenin yerine kullanıcının yeni girmiş olduğu bakiyeyi ekliyoruz.böylelikle update işlemi gerçekşmiş oluyor.
            OleDbCommand com3 = new OleDbCommand("update tedarikciler set bakiye=bakiye-@ilkgirilentutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com3.Parameters.AddWithValue("@ilkgirilentutar", OdememGuncelle.Tutar);
            com3.Parameters.AddWithValue("@TedarikciID", OdememGuncelle.TedarikciID);
            com3.Transaction = trar;
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        if (com3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show(OdememGuncelle.Mzkno + " " + "Nolu Ödeme Makbuzu Güncellenmiştir.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                trar.Commit();
            }
            catch (Exception Ex)
            {
                trar.Rollback();
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void OSilme(EOdemeler OSilme)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbTransaction trancim = con.BeginTransaction();
            OleDbCommand com = new OleDbCommand("delete from Odemeler WHERE ((Odemeler.odemeID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", OSilme.odemeID);
            com.Transaction = trancim;
            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye+@tutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@tutar", OSilme.Tutar);
            com2.Parameters.AddWithValue("@TedarikciID", OSilme.TedarikciID);
            com2.Transaction = trancim;
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(OSilme.Mzkno + " " + "Nolu Ödeme Makbuzunun Silme İşlemi Yapılmıştır", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public static void OCombo(EOdemeler OTedarikciDegisir)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");

            #region MakbuzaOncededenGirilenTutarıYakaliyoruz

            OleDbDataAdapter da = new OleDbDataAdapter("select tutar,TedarikciID from odemeler WHERE ((Odemeler.odemeID)=@ID)", con);
            da.SelectCommand.Parameters.AddWithValue("@ID", OTedarikciDegisir.odemeID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            double fark = Convert.ToDouble(dt.Rows[0]["tutar"].ToString());
            int eskiID = Convert.ToInt32(dt.Rows[0]["TedarikciID"].ToString());

            #endregion MakbuzaOncededenGirilenTutarıYakaliyoruz

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com2 = new OleDbCommand("update tedarikciler set bakiye=bakiye+@yenigirilentutar WHERE ((tedarikciler.TedarikciID)=@TedarikciID)", con);
            com2.Parameters.AddWithValue("@yenigirilentutar", fark);
            com2.Parameters.AddWithValue("@TedarikciID", eskiID);
            con.Close();
        }

        public static DataTable OEkstreByTedarikci(EOdemeler ByTedarikci)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand tedarikcicom = new OleDbCommand("select mkzno,tarih,tutar,aciklama,unvan from Odemeler inner join tedarikciler on Odemeler.TedarikciID=tedarikciler.TedarikciID  where Odemeler.TedarikciID=@ID", con);
            tedarikcicom.Parameters.AddWithValue("@ID", ByTedarikci.TedarikciID);
            OleDbDataAdapter dap = new OleDbDataAdapter(tedarikcicom);
            DataTable dett = new DataTable();
            dap.Fill(dett);
            con.Close();
            return dett;
        }

        public static DataTable OEkstreBymkzno(EOdemeler Bymkzno)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand mkzcom = new OleDbCommand("select mkzno,tarih,tutar,aciklama,unvan from Odemeler inner join tedarikciler on Odemeler.TedarikciID=tedarikciler.TedarikciID  WHERE ((Odemeler.odemeID)=@ID)", con);
            mkzcom.Parameters.AddWithValue("@ID", Bymkzno.odemeID);
            OleDbDataAdapter dap = new OleDbDataAdapter(mkzcom);
            DataTable dtet = new DataTable();
            dap.Fill(dtet);
            con.Close();
            return dtet;
        }

        public DataTable OEkstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select mkzno,tarih,tutar,aciklama,unvan from Odemeler inner join tedarikciler on Odemeler.TedarikciID=tedarikciler.TedarikciID", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
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