using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public class musteri:Firma
    {
        SqlConnection baglanti = new SqlConnection(connect.connectroad);
        public void musterisp()
        {
            SqlCommand ekle = new SqlCommand("insert into musteriler(unvan,adres,sehir,telefon,faks,vdairesi,vno) values(@unvan,@adres,@sehir,@telefon,@faks,@vdairesi,@vno)", baglanti);
            ekle.Parameters.AddWithValue("@unvan",_unvan );
            ekle.Parameters.AddWithValue("@adres", _adres);
            ekle.Parameters.AddWithValue("@sehir", _sehir);
            ekle.Parameters.AddWithValue("@telefon", _telefon);
            ekle.Parameters.AddWithValue("@faks", _faks);
            ekle.Parameters.AddWithValue("@vdairesi", _vergidairesi);
            ekle.Parameters.AddWithValue("vno", _vergino);
            if (ekle.Connection.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DialogResult sor = MessageBox.Show("Cari Kartı Kaydetmek İstediğinize Emin misiniz ?", "Kayıt", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (sor==DialogResult.Yes)
            {
                if (ekle.ExecuteNonQuery()>0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır","Sonuc",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
