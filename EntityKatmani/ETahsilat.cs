using System;

namespace EntityKatmani
{
    public class ETahsilat : IDisposable
    {
        #region Fields

        private int _tahsilatID;

        public int TahsilatID
        {
            get { return _tahsilatID; }
            set { _tahsilatID = value; }
        }

        private string _mzkno;

        public string Mzkno
        {
            get { return _mzkno; }
            set { _mzkno = value; }
        }

        private DateTime _tarih;

        public DateTime Tarih
        {
            get { return _tarih; }
            set { _tarih = value; }
        }

        private double _tutar;

        public double Tutar
        {
            get { return _tutar; }
            set { _tutar = value; }
        }

        private string _aciklama;

        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        private int _musteriID;

        public int MusteriID
        {
            get { return _musteriID; }
            set { _musteriID = value; }
        }

        #endregion Fields

        #region metotlar

        public ETahsilat()
        {
        }

        public ETahsilat(int tahsilatID, string mkzno, DateTime tarih, double tutar, string aciklama)
        {
            this._tahsilatID = tahsilatID;
            this._mzkno = mkzno;
            this._tarih = tarih;
            this._tutar = tutar;
            this._aciklama = aciklama;
        }

        public ETahsilat(string mkzno, DateTime tarih, double tutar, string aciklama)
        {
            this._mzkno = mkzno;
            this._tarih = tarih;
            this._tutar = tutar;
            this._aciklama = aciklama;
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