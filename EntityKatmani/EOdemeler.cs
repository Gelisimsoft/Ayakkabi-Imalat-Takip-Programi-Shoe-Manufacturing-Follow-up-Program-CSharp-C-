using System;

namespace EntityKatmani
{
    public class EOdemeler : IDisposable
    {
        #region Fields

        private int _odemeID;

        public int odemeID
        {
            get { return _odemeID; }
            set { _odemeID = value; }
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

        private int _TedarikciID;

        public int TedarikciID
        {
            get { return _TedarikciID; }
            set { _TedarikciID = value; }
        }

        #endregion Fields

        #region metotlar

        public EOdemeler()
        {
        }

        public EOdemeler(int odemeID, string mkzno, DateTime tarih, double tutar, string aciklama)
        {
            this._odemeID = odemeID;
            this._mzkno = mkzno;
            this._tarih = tarih;
            this._tutar = tutar;
            this._aciklama = aciklama;
        }

        public EOdemeler(string mkzno, DateTime tarih, double tutar, string aciklama)
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