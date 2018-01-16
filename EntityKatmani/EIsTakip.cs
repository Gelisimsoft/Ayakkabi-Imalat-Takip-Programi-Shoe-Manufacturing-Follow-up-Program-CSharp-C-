using System;

namespace EntityKatmani
{
    public class EIsTakip : IDisposable
    {
        #region Fields

        private int _TakipID;

        public int TakipID
        {
            get { return _TakipID; }
            set { _TakipID = value; }
        }

        private string _FisNo;

        public string FisNo
        {
            get { return _FisNo; }
            set { _FisNo = value; }
        }

        private string _Kalite;

        public string Kalite
        {
            get { return _Kalite; }
            set { _Kalite = value; }
        }

        private string _Renk;

        public string Renk
        {
            get { return _Renk; }
            set { _Renk = value; }
        }

        private string _Kalip;

        public string Kalip
        {
            get { return _Kalip; }
            set { _Kalip = value; }
        }

        private string _Okce;

        public string Okce
        {
            get { return _Okce; }
            set { _Okce = value; }
        }

        private string _Platfotm;

        public string Platfotm
        {
            get { return _Platfotm; }
            set { _Platfotm = value; }
        }

        private string _TakipNo;

        public string TakipNo
        {
            get { return _TakipNo; }
            set { _TakipNo = value; }
        }

        private DateTime _Tarih;

        public DateTime Tarih
        {
            get { return _Tarih; }
            set { _Tarih = value; }
        }

        private string _Garni;

        public string Garni
        {
            get { return _Garni; }
            set { _Garni = value; }
        }

        private string _Cift;

        public string Cift
        {
            get { return _Cift; }
            set { _Cift = value; }
        }

        private string _Asorti;

        public string Asorti
        {
            get { return _Asorti; }
            set { _Asorti = value; }
        }

        private int _MusteriID;

        public int MusteriID
        {
            get { return _MusteriID; }
            set { _MusteriID = value; }
        }

        private int _Personel1;

        public int Personel1
        {
            get { return _Personel1; }
            set { _Personel1 = value; }
        }

        private int _Personel2;

        public int Personel2
        {
            get { return _Personel2; }
            set { _Personel2 = value; }
        }

        private int _Personel3;

        public int Personel3
        {
            get { return _Personel3; }
            set { _Personel3 = value; }
        }

        private int _Personel4;

        public int Personel4
        {
            get { return _Personel4; }
            set { _Personel4 = value; }
        }

        #endregion Fields

        #region Methods

        public EIsTakip()
        {
        }

        public EIsTakip(int TakipID, string FisNo, string Kalite, string Renk, string Kalip, string Okce, string Platfotm, string TakipNo, DateTime Tarih, string Garni, string Cift, string Asorti, int MusteriID, int Personel1, int Personel2, int Personel3, int Personel4)
        {
            this._TakipID = TakipID;
            this._FisNo = FisNo;
            this._Kalite = Kalite;
            this._Renk = Renk;
            this._Kalip = Kalip;
            this._Okce = Okce;
            this._Platfotm = Platfotm;
            this._TakipNo = TakipNo;
            this._Tarih = Tarih;
            this._Garni = Garni;
            this._Cift = Cift;
            this._Asorti = Asorti;
            this._MusteriID = MusteriID;
            this._Personel1 = Personel1;
            this._Personel2 = Personel2;
            this._Personel3 = Personel3;
            this._Personel4 = Personel4;
        }

        public EIsTakip(string FisNo, string Kalite, string Renk, string Kalip, string Okce, string Platfotm, string TakipNo, DateTime Tarih, string Garni, string Cift, string Asorti, int MusteriID, int Personel1, int Personel2, int Personel3, int Personel4)
        {
            this._FisNo = FisNo;
            this._Kalite = Kalite;
            this._Renk = Renk;
            this._Kalip = Kalip;
            this._Okce = Okce;
            this._Platfotm = Platfotm;
            this._TakipNo = TakipNo;
            this._Tarih = Tarih;
            this._Garni = Garni;
            this._Cift = Cift;
            this._Asorti = Asorti;
            this._MusteriID = MusteriID;
            this._Personel1 = Personel1;
            this._Personel2 = Personel2;
            this._Personel3 = Personel3;
            this._Personel4 = Personel4;
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