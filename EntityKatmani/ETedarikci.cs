using System;

namespace EntityKatmani
{
    public class ETedarikci : IDisposable
    {
        #region Fields

        private int __TedarikciID;

        public int _TedarikciID
        {
            get { return __TedarikciID; }
            set { __TedarikciID = value; }
        }

        private string __unvan;

        public string _unvan
        {
            get { return __unvan; }
            set { __unvan = value; }
        }

        private string __adres;

        public string _adres
        {
            get { return __adres; }
            set { __adres = value; }
        }

        private string __sehir;

        public string _sehir
        {
            get { return __sehir; }
            set { __sehir = value; }
        }

        private string __telefon;

        public string _telefon
        {
            get { return __telefon; }
            set { __telefon = value; }
        }

        private string __faks;

        public string _faks
        {
            get { return __faks; }
            set { __faks = value; }
        }

        private string __vergidairesi;

        public string _vergidairesi
        {
            get { return __vergidairesi; }
            set { __vergidairesi = value; }
        }

        private string __vergino;

        public string _vergino
        {
            get { return __vergino; }
            set { __vergino = value; }
        }

        #endregion Fields

        #region Methods

        public ETedarikci()
        {
        }

        public ETedarikci(int TedarikciID, string unvan, string adres, string sehir, string telefon, string faks, string vergidairesi, string vergino)
        {
            this._TedarikciID = TedarikciID;
            this.__unvan = _unvan;
            this.__adres = adres;
            this.__sehir = sehir;
            this.__telefon = telefon;
            this.__faks = faks;
            this.__vergidairesi = vergidairesi;
            this.__vergino = vergino;
        }

        public ETedarikci(string unvan, string adres, string sehir, string telefon, string faks, string vergidairesi, string vergino)
        {
            this.__unvan = _unvan;
            this.__adres = adres;
            this.__sehir = sehir;
            this.__telefon = telefon;
            this.__faks = faks;
            this.__vergidairesi = vergidairesi;
            this.__vergino = vergino;
        }

        #endregion Methods

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}