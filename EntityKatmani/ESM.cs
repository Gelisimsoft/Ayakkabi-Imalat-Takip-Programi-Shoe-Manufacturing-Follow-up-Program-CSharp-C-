using System;

namespace EntityKatmani
{
    public class ESM : IDisposable
    {
        public int SMID { get; set; }
        public int UStok { get; set; }
        public int KStok { get; set; }
        public double Miktar { get; set; }
        public DateTime Tarih { get; set; }
        public string Birim { get; set; }

        public ESM()
        {
        }

        public ESM(int SMID, int UStok, int KStok, double Miktar, DateTime Tarih, string Birim)
        {
            this.SMID = SMID;
            this.UStok = UStok;
            this.KStok = KStok;
            this.Miktar = Miktar;
            this.Tarih = Tarih;
            this.Birim = Birim;
        }

        public ESM(int UStok, int KStok, double Miktar, DateTime Tarih, string Birim)
        {
            this.UStok = UStok;
            this.KStok = KStok;
            this.Miktar = Miktar;
            this.Tarih = Tarih;
            this.Birim = Birim;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}