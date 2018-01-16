using System;

namespace Ayakkabi_Imalat_Takip
{
    public class Epersonel : IDisposable
    {
        #region Fields

        private int __personelID;

        public int _personelID
        {
            get { return __personelID; }
            set { __personelID = value; }
        }

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

        private int __departmanID;

        public int _departman
        {
            get { return __departmanID; }
            set { __departmanID = value; }
        }

        #endregion Fields

        #region baglantilar

        public Epersonel()
        {
        }

        public Epersonel(int personelID, string adsoyad, string tcno, string adres, string ceptlf, string evtlf, DateTime isegiris, DateTime cikisdate, int departmanID)
        {
            this.__personelID = personelID;
            this.__adsoyad = adsoyad;
            this.__tcno = tcno;
            this.__adres = adres;
            this.__ceptlf = ceptlf;
            this.__evtlf = evtlf;
            this.__isegiris = isegiris;
            this.__cikisdate = cikisdate;
            this.__departmanID = departmanID;
        }

        public Epersonel(string adsoyad, string tcno, string adres, string ceptlf, string evtlf, DateTime isegiris, DateTime cikisdate, int departmanID)
        {
            this.__adsoyad = adsoyad;
            this.__tcno = tcno;
            this.__adres = adres;
            this.__ceptlf = ceptlf;
            this.__evtlf = evtlf;
            this.__isegiris = isegiris;
            this.__cikisdate = cikisdate;
            this.__departmanID = departmanID;
        }

        #endregion baglantilar

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}