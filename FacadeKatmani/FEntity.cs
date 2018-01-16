using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FEntity
    {
        public static void Guncelle(EEntity Degistir)
        {
            OleDbConnection concon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand comcom = new OleDbCommand("update Entriy set KullaniciAdi=@Kullanici,Sifre=@Sifre WHERE ((Entriy.SifreID)=1)", concon);
            comcom.Parameters.AddWithValue("@Kullanici", Degistir.KullaniciAdi);
            comcom.Parameters.AddWithValue("@Sifre", Degistir.Sifre);
            OleDbDataAdapter adp = new OleDbDataAdapter(comcom);
            if (concon.State == ConnectionState.Closed)
            {
                concon.Open();
            }
            try
            {
                if (comcom.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kullanıcı Adı ve Güncelleme İşlemi Yapılmıştır.", "Güncelleme İşlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}