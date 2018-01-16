using System;

namespace EntityKatmani
{
    public class Edepartman : IDisposable
    {
        private int __departmanID;

        public int _departmanID
        {
            get { return __departmanID; }
            set { __departmanID = value; }
        }

        private string _adi;

        public string Adi
        {
            get { return _adi; }
            set { _adi = value; }
        }

        private string _aciklama;

        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        public Edepartman()
        { }

        public Edepartman(int departmanid, string adi, string aciklama)
        {
            this.__departmanID = _departmanID;
            this._adi = adi;
            this._aciklama = aciklama;
        }

        public Edepartman(string adi, string aciklama)
        {
            this._adi = adi;
            this._aciklama = aciklama;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}