using System;

namespace Ayakkabi_Imalat_Takip
{
    public class connect : IDisposable
    {
        public static string connectroad = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;";

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}