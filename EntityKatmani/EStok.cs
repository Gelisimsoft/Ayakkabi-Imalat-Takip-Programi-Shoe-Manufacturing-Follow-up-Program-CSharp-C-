using System;

namespace EntityKatmani
{
    public class EStok : IDisposable
    {
        private int __stokid;

        public int _stokid
        {
            get { return __stokid; }
            set { __stokid = value; }
        }

        private string __stokadi;

        public string _stokadi
        {
            get { return __stokadi; }
            set { __stokadi = value; }
        }

        private string __stokaciklama;

        public string _stokaciklama
        {
            get { return __stokaciklama; }
            set { __stokaciklama = value; }
        }

        private string __stokkodu;

        public string _stokkodu
        {
            get { return __stokkodu; }
            set { __stokkodu = value; }
        }

        private double __bakiye;

        public double _bakiye
        {
            get { return __bakiye; }
            set { __bakiye = value; }
        }

        private double _miktar;

        public double Miktar
        {
            get { return _miktar; }
            set { _miktar = value; }
        }

        public EStok()
        {
        }

        public EStok(int stokid, string stokadi, string stokaciklama, string stokkodu, double bakiye)
        {
            this.__stokid = stokid;
            this.__stokadi = stokadi;
            this.__stokaciklama = stokaciklama;
            this.__stokkodu = stokkodu;
            this.__bakiye = bakiye;
        }

        public EStok(string stokadi, string stokaciklama, string stokkodu, double bakiye)
        {
            this.__stokadi = stokadi;
            this.__stokaciklama = stokaciklama;
            this.__stokkodu = stokkodu;
            this.__bakiye = bakiye;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}