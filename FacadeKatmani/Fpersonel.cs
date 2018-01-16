using Ayakkabi_Imalat_Takip;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class Fpersonel
    {
        public static void PEkle(Epersonel eklenecekler)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            OleDbCommand com = new OleDbCommand("insert into personeller(adsoyad,tcno,adres,ceptlf,evtlf,isegiris,departmanID) values(@adsoyad,@tcno,@adres,@ceptlf,@evtlf,@isegiris,@departman)", con);
            com.Parameters.AddWithValue("@adsoyad", eklenecekler._adsoyad);
            com.Parameters.AddWithValue("@tcno", eklenecekler._tcno);
            com.Parameters.AddWithValue("@adres", eklenecekler._adres);
            com.Parameters.AddWithValue("@ceptlf", eklenecekler._ceptlf);
            com.Parameters.AddWithValue("@evtlf", eklenecekler._evtlf);
            com.Parameters.AddWithValue("@isegiris", eklenecekler._isegiris);
            com.Parameters.AddWithValue("@departman", eklenecekler._departman);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt İşlemi Yapılmıştır.", "Sonuc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
        }

        public static DataTable Pekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select adsoyad,tcno,adres,ceptlf,evtlf,isegiris,cikisdate,bakiye,departman.adi from personeller inner join departman on personeller.departmanID=departman.departmanId WHERE ((personeller.IsActive)=True)", con);
            OleDbDataAdapter dappersn = new OleDbDataAdapter(com);
            DataTable dtpersnl = new DataTable();
            dappersn.Fill(dtpersnl);
            con.Close();
            return dtpersnl;
        }

        public static DataTable PDeparmant(Epersonel GetirDprtman)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand comdept = new OleDbCommand("select personeller.adsoyad,personeller.tcno,personeller.adres,personeller.ceptlf,personeller.evtlf,personeller.isegiris,personeller.cikisdate,personeller.bakiye,departman.adi from personeller inner join departman on personeller.departmanID=departman.departmanId WHERE (((personeller.IsActive)=True) AND ((personeller.departmanID)=@ID))", con);
            comdept.Parameters.AddWithValue("@ID", GetirDprtman._departman);
            OleDbDataAdapter dappersn = new OleDbDataAdapter(comdept);
            DataTable dtdptmn = new DataTable();
            dappersn.Fill(dtdptmn);
            con.Close();
            return dtdptmn;
        }

        public static DataTable PersonelID(Epersonel GetirID)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand comdept = new OleDbCommand("select personeller.adsoyad,personeller.tcno,personeller.adres,personeller.ceptlf,personeller.evtlf,personeller.isegiris,personeller.cikisdate,personeller.bakiye,departman.adi from personeller inner join departman on personeller.departmanID=departman.departmanId WHERE ((personeller.personelID)=@ID)", con);
            comdept.Parameters.AddWithValue("@ID", GetirID._personelID);
            OleDbDataAdapter dappersn = new OleDbDataAdapter(comdept);
            DataTable dtdptmn = new DataTable();
            dappersn.Fill(dtdptmn);
            con.Close();
            return dtdptmn;
        }
    }
}