using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public class personel:IDisposable
    {
        #region Fields
        private string __adsoyad;

        public string _adsoyad
        {
            get { return __adsoyad; }
            set { __adsoyad = value; }
        }
        private string __tcno;

        public string _tcno
        {
            get { return __tcno; }
            set { __tcno = value; }
        }
        private string __adres;

        public string _adres
        {
            get { return __adres; }
            set { __adres = value; }
        }
        private string __ceptlf;

        public string _ceptlf
        {
            get { return __ceptlf; }
            set { __ceptlf = value; }
        }
        private string __evtlf;

        public string _evtlf
        {
            get { return __evtlf; }
            set { __evtlf = value; }
        }
        private DateTime __isegiris;

        public DateTime _isegiris
        {
            get { return __isegiris; }
            set { __isegiris = value; }
        }
        private DateTime __cikisdate;

        public DateTime _cikisdate
        {
            get { return __cikisdate; }
            set { __cikisdate = value; }
        }
        private int __departman;

        public int _departman
        {
            get { return __departman; }
            set { __departman = value; }
        }

        #endregion;

        #region metots
        SqlConnection con = new SqlConnection(connect.connectroad);
        public void calisanlar()
        {
            SqlCommand com = new SqlCommand("insert into personeller(adsoyad,tcno,adres,ceptlf,evtlf,isegiris,cikisdate,departmanID) values(@adsoyad,@tcno,@adres,@ceptlf,@evtlf,@isegiris,@cikisdate,@departman)", con);
            com.Parameters.AddWithValue("@adsoyad", __adsoyad);
            com.Parameters.AddWithValue("@tcno", __tcno);
            com.Parameters.AddWithValue("@adres", __adres);
            com.Parameters.AddWithValue("@ceptlf", __ceptlf);
            com.Parameters.AddWithValue("@evtlf", __evtlf);
            com.Parameters.AddWithValue("@isegiris", __isegiris);
            com.Parameters.AddWithValue("@cikisdate", __cikisdate);
            com.Parameters.AddWithValue("@departmanID", _departman);
            if (con.State!=ConnectionState.Open)
            {
                con.Open();
            }
            DialogResult sor = MessageBox.Show("Personel Kartı Kaydı Yapmak İstiyor musunuz ?", "Personel Kaydı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sor == DialogResult.Yes)
                 {
                if (com.ExecuteNonQuery()>0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır.","Sonuc",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt İşlemi Yapılamadı !!!.", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
                else
	        {
        MessageBox.Show("Kayıt İşlemi İptal Edildi","İptal",MessageBoxButtons.OK,MessageBoxIcon.Stop);
	        }
   }
        #endregion;

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
