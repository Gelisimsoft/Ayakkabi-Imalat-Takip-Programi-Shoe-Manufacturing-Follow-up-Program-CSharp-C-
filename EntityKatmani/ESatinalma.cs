using System;

namespace EntityKatmani
{
    public class ESatinalma : IDisposable
    {
        #region Fields

        private int __alisftID;

        public int _alisftID
        {
            get { return __alisftID; }
            set { __alisftID = value; }
        }

        private string __ftno;

        public string _ftno
        {
            get { return __ftno; }
            set { __ftno = value; }
        }

        private string __birim;

        public string _birim
        {
            get { return __birim; }
            set { __birim = value; }
        }

        private double __miktar;

        public double _miktar
        {
            get { return __miktar; }
            set { __miktar = value; }
        }

        private double __tutar;

        public double _tutar
        {
            get { return __tutar; }
            set { __tutar = value; }
        }

        private double __kdv;

        public double _kdv
        {
            get { return __kdv; }
            set { __kdv = value; }
        }

        private double __geneltoplam;

        public double _geneltoplam
        {
            get { return __geneltoplam; }
            set { __geneltoplam = value; }
        }

        private string __aciklama;

        public string _aciklama
        {
            get { return __aciklama; }
            set { __aciklama = value; }
        }

        private int __TedarikciID;

        public int _TedarikciID
        {
            get { return __TedarikciID; }
            set { __TedarikciID = value; }
        }

        private int __stokid;

        public int _stokid
        {
            get { return __stokid; }
            set { __stokid = value; }
        }

        private DateTime __alistarih;

        public DateTime _alistarih
        {
            get { return __alistarih; }
            set { __alistarih = value; }
        }

        #endregion Fields

        #region metotlar

        public ESatinalma()
        {
        }

        public ESatinalma(int alisftID, string ftno, string birim, double miktar, double tutar, double kdv, double geneltoplam, string aciklama, int TedarikciID, int stokid, DateTime alistarih)
        {
            this.__alisftID = alisftID;
            this.__ftno = ftno;
            this.__birim = birim;
            this.__miktar = miktar;
            this.__tutar = tutar;
            this.__kdv = kdv;
            this.__geneltoplam = geneltoplam;
            this.__aciklama = aciklama;
            this._TedarikciID = TedarikciID;
            this._stokid = stokid;
            this.__alistarih = alistarih;
        }

        public ESatinalma(string ftno, string birim, double miktar, double tutar, double kdv, double geneltoplam, string aciklama, int TedarikciID, int stokid, DateTime alistarih)
        {
            this.__ftno = ftno;
            this.__birim = birim;
            this.__miktar = miktar;
            this.__tutar = tutar;
            this.__kdv = kdv;
            this.__geneltoplam = geneltoplam;
            this.__aciklama = aciklama;
            this._TedarikciID = TedarikciID;
            this._stokid = stokid;
            this.__alistarih = alistarih;
        }

        #endregion metotlar

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}