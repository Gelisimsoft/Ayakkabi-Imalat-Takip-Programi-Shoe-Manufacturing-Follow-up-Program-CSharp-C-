using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayakkabi_Imalat_Takip
{
    public abstract class Firma:IDisposable
    {
        private string __unvan;

public string _unvan
{
  get { return __unvan; }
  set { __unvan = value; }
}
        private string __adres;

public string _adres
{
  get { return __adres; }
  set { __adres = value; }
}
        private string __sehir;

public string _sehir
{
  get { return __sehir; }
  set { __sehir = value; }
}
        private string __telefon;

public string _telefon
{
  get { return __telefon; }
  set { __telefon = value; }
}
        private string __faks;

public string _faks
{
  get { return __faks; }
  set { __faks = value; }
}
        private string __vergidairesi;

public string _vergidairesi
{
  get { return __vergidairesi; }
  set { __vergidairesi = value; }
}
        private string __vergino;

public string _vergino
{
  get { return __vergino; }
  set { __vergino = value; }
}

#region IDisposable Members

public void Dispose()
{
    GC.SuppressFinalize(this);
}

#endregion
    }
}
