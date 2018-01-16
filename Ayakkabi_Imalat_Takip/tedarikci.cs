using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
   public class tedarikci:Firma
    {
        SqlConnection baglanti = new SqlConnection(connect.connectroad);
        public void musterisp()
        {
            SqlCommand ekle = new SqlCommand("insert into tedarikciler(unvan,adres,sehir,telefon,faks,vergidairesi,vergino) values(@unvan,@adres,@sehir,@telefon,@faks,@vergidairesi,@vergino)", baglanti);
            ekle.Parameters.AddWithValue("@unvan", _unvan);
            ekle.Parameters.AddWithValue("@adres", _adres);
            ekle.Parameters.AddWithValue("@sehir", _sehir);
            ekle.Parameters.AddWithValue("@telefon", _telefon);
            ekle.Parameters.AddWithValue("@faks", _faks);
            ekle.Parameters.AddWithValue("@vergidairesi", _vergidairesi);
            ekle.Parameters.AddWithValue("vergino", _vergino);
            if (ekle.Connection.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DialogResult sor = MessageBox.Show("Cari Kartı Kaydetmek İstediğinize Emin misiniz ?", "Kayıt", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (sor == DialogResult.Yes)
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
            else
            {
                MessageBox.Show("Kayıt İşlemi İptal Edildi.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglanti.Close();
        }
    }
}
