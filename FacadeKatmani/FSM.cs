using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FSM
    {
        private static double EskiMiktar;
        private static int EskiID;

        public static void Ekle(ESM Eklemeli)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand VayComHaci = new OleDbCommand("insert into StokMaliyet(UStok,KStok,Miktar,Tarih,Birim) values(@U,@K,@M,@T,@B)", con);
            VayComHaci.Parameters.AddWithValue("@U", Eklemeli.UStok);
            VayComHaci.Parameters.AddWithValue("@K", Eklemeli.KStok);
            VayComHaci.Parameters.AddWithValue("@M", Eklemeli.Miktar);
            VayComHaci.Parameters.AddWithValue("@T", Eklemeli.Tarih);
            VayComHaci.Parameters.AddWithValue("@B", Eklemeli.Birim);
            try
            {
                if (VayComHaci.ExecuteNonQuery() > 0)
                {
                    OleDbCommand ComHaci = new OleDbCommand("update stoklar set miktar=miktar-@M WHERE ((stoklar.stokid)=@K)", con);
                    ComHaci.Parameters.AddWithValue("@M", Eklemeli.Miktar);
                    ComHaci.Parameters.AddWithValue("@K", Eklemeli.KStok);
                    if (ComHaci.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Stok Maliyetlendirme İşlemi Yapılmıştır.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static DataTable Ekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT StokMaliyet.Tarih,(SELECT stoklar.stokadi FROM stoklar WHERE (stoklar.stokid = StokMaliyet.KStok)) AS K, StokMaliyet.Miktar, StokMaliyet.Birim,(SELECT stoklar.stokadi  FROM stoklar WHERE (stoklar.stokid = StokMaliyet.UStok)) AS U, StokMaliyet.SMID, StokMaliyet.UStok, StokMaliyet.KStok FROM StokMaliyet", con);
            OleDbDataAdapter dep = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            dep.Fill(dt);
            con.Close();
            return dt;
        }

        public static void Guncelle(ESM Eklemeli)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region VeriTabanindanStokBilgisiniCekiyoruz

            //Stok miktarını çekecek ilgili OleDbDataAdapter yazıyoruz.
            OleDbCommand da = new OleDbCommand("select Miktar,KStok from StokMaliyet WHERE ((StokMaliyet.SMID)=@ID)", con);
            da.Parameters.AddWithValue("@ID", Eklemeli.SMID);
            OleDbDataReader dr = da.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    EskiMiktar = Convert.ToDouble(dr["Miktar"].ToString());
                    EskiID = Convert.ToInt32(dr["KStok"].ToString());
                }
            }

            #endregion VeriTabanindanStokBilgisiniCekiyoruz

            OleDbCommand VayComHaci = new OleDbCommand("update StokMaliyet set UStok=@U,KStok=@K,Miktar=@M,Tarih=@T,Birim=@B WHERE ((StokMaliyet.SMID)=@ID)", con);
            VayComHaci.Parameters.AddWithValue("@U", Eklemeli.UStok);
            VayComHaci.Parameters.AddWithValue("@K", Eklemeli.KStok);
            VayComHaci.Parameters.AddWithValue("@M", Eklemeli.Miktar);
            VayComHaci.Parameters.AddWithValue("@T", Eklemeli.Tarih);
            VayComHaci.Parameters.AddWithValue("@B", Eklemeli.Birim);
            VayComHaci.Parameters.AddWithValue("@ID", Eklemeli.SMID);
            try
            {
                if (VayComHaci.ExecuteNonQuery() > 0)
                {
                    OleDbCommand ComHaci = new OleDbCommand("update stoklar set miktar=miktar+@EM WHERE ((stoklar.stokid)=@EK)", con);
                    ComHaci.Parameters.AddWithValue("@EM", EskiMiktar);
                    ComHaci.Parameters.AddWithValue("@EK", EskiID);
                    if (ComHaci.ExecuteNonQuery() > 0)
                    {
                        OleDbCommand ComHaci2 = new OleDbCommand("update stoklar set miktar=miktar-@YM WHERE ((stoklar.stokid)=@YK)", con);
                        ComHaci2.Parameters.AddWithValue("@YM", Eklemeli.Miktar);
                        ComHaci2.Parameters.AddWithValue("@YK", Eklemeli.KStok);
                        if (ComHaci2.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Stok Maliyet Güncelleme İşlemi Yapılmıştır.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void Sil(ESM EES)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("delete from StokMaliyet WHERE ((StokMaliyet.SMID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EES.SMID);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    OleDbCommand com2 = new OleDbCommand("update stoklar set miktar=miktar+@Miktar WHERE ((stoklar.stokid)=@KID)", con);
                    com2.Parameters.AddWithValue("@Miktar", EES.Miktar);
                    com2.Parameters.AddWithValue("@KID", EES.KStok);
                    if (com2.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Stok Maliyet Kartı Silinme İşlemi Yapılmıştır.", "Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }
    }
}