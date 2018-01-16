using System;

namespace EntityKatmani
{
    public class EIsPersonel : IDisposable
    {
        public int IsID { get; set; }
        public int TakipID { get; set; }
        public int personelID { get; set; }
        public double Ucret { get; set; }

        public EIsPersonel()
        {
        }

        public EIsPersonel(int IsIDim, int TakipimID, int PersonelcikID, double paracik)
        {
            this.IsID = IsIDim;
            this.TakipID = TakipimID;
            this.personelID = PersonelcikID;
            this.Ucret = paracik;
        }

        public EIsPersonel(int TakipimID, int PersonelcikID, double paracik)
        {
            this.TakipID = TakipimID;
            this.personelID = PersonelcikID;
            this.Ucret = paracik;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}