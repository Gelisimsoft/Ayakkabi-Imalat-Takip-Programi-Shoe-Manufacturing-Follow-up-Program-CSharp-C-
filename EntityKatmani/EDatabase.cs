using System;

namespace EntityKatmani
{
    public class EDatabase : IDisposable
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public DateTime Tarih { get; set; }
        public string Timecik { get; set; }

        public EDatabase()
        {
        }

        public EDatabase(int IDsi, string Adi, DateTime tarihi, string Timecik)
        {
            this.Ad = Adi;
            this.ID = IDsi;
            this.Tarih = tarihi;
            this.Timecik = Timecik;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}