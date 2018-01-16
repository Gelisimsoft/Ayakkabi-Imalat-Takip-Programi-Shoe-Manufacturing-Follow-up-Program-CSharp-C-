using System;

namespace EntityKatmani
{
    public class ESatis : IDisposable
    {
        #region Fields

        private int __satisftID;

        public int _satisftID
        {
            get { return __satisftID; }
            set { __satisftID = value; }
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

        private int __musteriID;

        public int _musteriID
        {
            get { return __musteriID; }
            set { __musteriID = value; }
        }

        private int __stokid;

        public int _stokid
        {
            get { return __stokid; }
            set { __stokid = value; }
        }

        private DateTime __satistarih;

        public DateTime _satistarih
        {
            get { return __satistarih; }
            set { __satistarih = value; }
        }

        #endregion Fields

        #region metotlar

        public ESatis()
        {
        }

        public ESatis(int satisftID, string ftno, string birim, double miktar, double tutar, double kdv, double geneltoplam, string aciklama, int musteriID, int stokid, DateTime alistarih)
        {
            this.__satisftID = satisftID;
            this.__ftno = ftno;
            this.__birim = birim;
            this.__miktar = miktar;
            this.__tutar = tutar;
            this.__kdv = kdv;
            this.__geneltoplam = geneltoplam;
            this.__aciklama = aciklama;
            this._musteriID = musteriID;
            this._stokid = stokid;
            this.__satistarih = alistarih;
        }

        public ESatis(string ftno, string birim, double miktar, double tutar, double kdv, double geneltoplam, string aciklama, int musteriID, int stokid, DateTime satistarih)
        {
            this.__ftno = ftno;
            this.__birim = birim;
            this.__miktar = miktar;
            this.__tutar = tutar;
            this.__kdv = kdv;
            this.__geneltoplam = geneltoplam;
            this.__aciklama = aciklama;
            this._musteriID = musteriID;
            this._stokid = stokid;
            this.__satistarih = satistarih;
        }

        #endregion metotlar

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion IDisposable Members
    }
}