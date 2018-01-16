using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[assembly: SuppressIldasm]

namespace FacadeKatmani
{
    public class FIsTakip : IDisposable
    {
        public static void IsTakipEkle(EIsTakip Ekleyelim)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region İş Takibi için ekleme kodları

            OleDbCommand com = new OleDbCommand("insert into IsTakipForm(Fisno,Kalite,Renk,Kalip,Okce,Platfotm,TakipNo,Tarih,Garni,Cift,Asorti,MusteriID,kesimci,temizleme,kalipci,montaj) values(@FisNo,@Kalite,@Renk,@Kalip,@Okce,@Platfotm,@TakipNo,@Tarih,@Garni,@Cift,@Asorti,@MusteriID,@kesimci,@temizleme,@kalipci,@montaj)", con);
            com.Parameters.AddWithValue("@FisNo", Ekleyelim.FisNo);
            com.Parameters.AddWithValue("@Kalite", Ekleyelim.Kalite);
            com.Parameters.AddWithValue("@Renk", Ekleyelim.Renk);
            com.Parameters.AddWithValue("@Kalip", Ekleyelim.Kalip);
            com.Parameters.AddWithValue("@Okce", Ekleyelim.Okce);
            com.Parameters.AddWithValue("@Platfotm", Ekleyelim.Platfotm);
            com.Parameters.AddWithValue("@TakipNo", Ekleyelim.TakipNo);
            com.Parameters.AddWithValue("@Tarih", Ekleyelim.Tarih);
            com.Parameters.AddWithValue("@Garni", Ekleyelim.Garni);
            com.Parameters.AddWithValue("@Cift", Ekleyelim.Cift);
            com.Parameters.AddWithValue("@Asorti", Ekleyelim.Asorti);
            com.Parameters.AddWithValue("@MusteriID", Ekleyelim.MusteriID);
            com.Parameters.AddWithValue("@kesimci", Ekleyelim.Personel1);
            com.Parameters.AddWithValue("@temizleme", Ekleyelim.Personel2);
            com.Parameters.AddWithValue("@kalipci", Ekleyelim.Personel3);
            com.Parameters.AddWithValue("@montaj", Ekleyelim.Personel4);

            #endregion İş Takibi için ekleme kodları

            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show(Ekleyelim.TakipNo + "Numaralı İş Takip Formunun Kaydı Yapılmıştır.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void IsTakipGuncelle(EIsTakip Guncelleme)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand comGun = new OleDbCommand("Update IsTakipForm set Tarih=@Tarih,TakipNo=@T,MusteriID=@M,Fisno=@F,Kalip=@Ka,Okce=@O,Platfotm=@P,Garni=@G,Cift=@C,Asorti=@A,Renk=@R,Kalite=@K,kesimci=@kes,temizleme=@tem,kalipci=@kal,montaj=@mon WHERE ((IsTakipForm.TakipID)=@ID)", con);
            comGun.Parameters.AddWithValue("@Tarih", Guncelleme.Tarih);
            comGun.Parameters.AddWithValue("@T", Guncelleme.TakipNo);
            comGun.Parameters.AddWithValue("@M", Guncelleme.MusteriID);
            comGun.Parameters.AddWithValue("@F", Guncelleme.FisNo);
            comGun.Parameters.AddWithValue("@Ka", Guncelleme.Kalip);
            comGun.Parameters.AddWithValue("@O", Guncelleme.Okce);
            comGun.Parameters.AddWithValue("@P", Guncelleme.Platfotm);
            comGun.Parameters.AddWithValue("@G", Guncelleme.Garni);
            comGun.Parameters.AddWithValue("@C", Guncelleme.Cift);
            comGun.Parameters.AddWithValue("@A", Guncelleme.Asorti);
            comGun.Parameters.AddWithValue("@R", Guncelleme.Renk);
            comGun.Parameters.AddWithValue("@K", Guncelleme.Kalite);
            comGun.Parameters.AddWithValue("@kes", Guncelleme.Personel1);
            comGun.Parameters.AddWithValue("@tem", Guncelleme.Personel2);
            comGun.Parameters.AddWithValue("@kal", Guncelleme.Personel3);
            comGun.Parameters.AddWithValue("@mon", Guncelleme.Personel4);
            comGun.Parameters.AddWithValue("@ID", Guncelleme.TakipID);
            try
            {
                if (comGun.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show(Guncelleme.TakipNo + "Numaralı İş Takip Formunun Güncellemesi Yapılmıştır.", "Güncelleme Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Test");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static void IsTakipSil(EIsTakip Silme)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            #region İş Takibi için Güncelleme Kodları

            OleDbCommand com = new OleDbCommand("update IsTakipForm set IsActive=0 where TakipID=@ID", con);
            com.Parameters.AddWithValue("@ID", Silme.TakipID);

            #endregion İş Takibi için Güncelleme Kodları

            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show(Silme.TakipNo + "Numaralı İş Takip Formunu Silinmiştir.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static DataTable IsTakipEkstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Fisno,Kalite,Renk,Kalip,Okce,Platfotm,TakipNo,Tarih,Garni,Cift,Asorti,unvan,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj]from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID where ((IsTakipForm.IsActive)=True) order by TakipNo", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable IsTakipEkstreMusteri(EIsTakip ByMusteri)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Fisno,Kalite,Renk,Kalip,Okce,Platfotm,TakipNo,Tarih,Garni,Cift,Asorti,unvan,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj]from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID WHERE (((IsTakipForm.IsActive)=True) AND ((IsTakipForm.MusteriID)=@ID))", con);
            com.Parameters.AddWithValue("@ID", ByMusteri.MusteriID);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable IsTakipEkstreByNo(EIsTakip ByNo)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Fisno,Kalite,Renk,Kalip,Okce,Platfotm,TakipNo,Tarih,Garni,Cift,Asorti,unvan,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj] from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID WHERE (((IsTakipForm.IsActive)=True) AND ((IsTakipForm.TakipNo) like @No))", con);
            com.Parameters.AddWithValue("@No", ByNo.TakipNo + "%");
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable IsTakipEkstreByFisno(EIsTakip ByFisno)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Fisno,Kalite,Renk,Kalip,Okce,Platfotm,TakipNo,Tarih,Garni,Cift,Asorti,unvan,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj] from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID WHERE (((IsTakipForm.IsActive)=True) AND ((IsTakipForm.FisNo) like @No))", con);
            com.Parameters.AddWithValue("@No", ByFisno.FisNo + "%");
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