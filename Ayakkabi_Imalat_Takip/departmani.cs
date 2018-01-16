using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    class departmani:IDisposable
    {
        private string __adi;

        public string _adi
        {
            get { return __adi; }
            set { __adi = value; }
        }
        private string __aciklama;

        public string _aciklama
        {
            get { return __aciklama; }
            set { __aciklama = value; }
        }
        SqlConnection con = new SqlConnection(connect.connectroad);
        public void bolum()
        {
            SqlCommand com = new SqlCommand("insert into departman(adi,aciklama) values(@adi,@aciklama)", con);
        com.Parameters.AddWithValue("@adi", __adi);
        com.Parameters.AddWithValue("@aciklama", __aciklama);
        if (com.Connection.State!=ConnectionState.Open)
        {
            con.Open();
        }
        DialogResult sor = MessageBox.Show("Departmanı Kaydetmek İstediğinize Emin misiniz ?", "Kayıt", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (sor == DialogResult.Yes)
        {
            if (com.ExecuteNonQuery() > 0)
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
       con.Close();
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
