using System;
using System.Runtime.CompilerServices;

[assembly: SuppressIldasm]

namespace EntityKatmani
{
    public class EEntity : IDisposable
    {
        public int SifreID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        public EEntity()
        {
        }

        public EEntity(int IDm, string AccontName, string Pass)
        {
            this.SifreID = IDm;
            this.KullaniciAdi = AccontName;
            this.Sifre = Pass;
        }

        public EEntity(string AccontName, string Pass)
        {
            this.KullaniciAdi = AccontName;
            this.Sifre = Pass;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}