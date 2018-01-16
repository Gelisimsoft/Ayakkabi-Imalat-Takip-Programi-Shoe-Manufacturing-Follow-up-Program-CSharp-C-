using EntityKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FacadeKatmani
{
    public class FIsPersonel
    {
        public static void Ekle(EIsPersonel Eklemeli)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand VayComHaci = new OleDbCommand("insert into IsPersonel(TakipID,personelID,Ucret) values (@TID,@PID,@Ucret)", con);
            VayComHaci.Parameters.AddWithValue("@TID", Eklemeli.TakipID);
            VayComHaci.Parameters.AddWithValue("@PID", Eklemeli.personelID);
            VayComHaci.Parameters.AddWithValue("@Ucret", Eklemeli.Ucret);
            OleDbCommand VayComPersonel = new OleDbCommand("update personeller set bakiye=bakiye+@Ucret WHERE ((personeller.personelID)=@PID)", con);
            VayComPersonel.Parameters.AddWithValue("@Ucret", Eklemeli.Ucret);
            VayComPersonel.Parameters.AddWithValue("@PID", Eklemeli.personelID);
            try
            {
                if (VayComHaci.ExecuteNonQuery() > 0)
                {
                    VayComPersonel.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static DataTable Ekstre()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("SELECT (SELECT TakipNo FROM IsTakipForm WHERE ((IsTakipForm.TakipID)=IsPersonel.TakipID)) AS TakipNo,(SELECT FisNo FROM IsTakipForm AS IsTakipForm WHERE (TakipID = IsPersonel.TakipID)) AS FisNo,(SELECT adsoyad FROM personeller WHERE (personelID = IsPersonel.personelID)) AS adsoyad, Ucret, personelID, TakipID, IsID FROM IsPersonel", con);
            OleDbDataAdapter dep = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            dep.Fill(dt);
            con.Close();
            return dt;
        }

        private static double GeldiPara;
        private static int GeldiID;

        public static void Guncelle(EIsPersonel EIs)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand comGel = new OleDbCommand("select IsPersonel.Ucret,IsPersonel.personelID from IsPersonel WHERE IsPersonel.IsID=@ID", con);
            comGel.Parameters.AddWithValue("@ID", EIs.IsID);
            OleDbDataReader dr = comGel.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    GeldiPara = Convert.ToDouble(dr["Ucret"].ToString());
                    GeldiID = Convert.ToInt32(dr["personelID"]);
                }
            }
            dr.Dispose();
            OleDbCommand com = new OleDbCommand("update personeller set bakiye=personeller.bakiye-@Gelen WHERE ((personeller.personelID)=@PID)", con);
            com.Parameters.AddWithValue("@Gelen", GeldiPara);
            com.Parameters.AddWithValue("@PID", GeldiID);
            OleDbCommand comIsPersonel = new OleDbCommand("Update IsPersonel set TakipID=@TID, Ucret=@Ucret,personelID=@PID WHERE ((IsPersonel.IsID)=@ID)", con);
            comIsPersonel.Parameters.AddWithValue("@TID", EIs.TakipID);
            comIsPersonel.Parameters.AddWithValue("@Ucret", EIs.Ucret);
            comIsPersonel.Parameters.AddWithValue("@PID", EIs.personelID);
            comIsPersonel.Parameters.AddWithValue("@ID", EIs.IsID);
            OleDbCommand comPersonel = new OleDbCommand("update personeller set bakiye=bakiye+@Ucret WHERE ((personeller.personelID)=@PID)", con);
            comPersonel.Parameters.AddWithValue("@Ucret", EIs.Ucret);
            comPersonel.Parameters.AddWithValue("@PID", EIs.personelID);
            //try
            //{
            if (com.ExecuteNonQuery() > 0)
            {
                if (comIsPersonel.ExecuteNonQuery() > 0)
                {
                    if (comPersonel.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Personel Ücret Güncelleme İşlemi Yapılmıştır.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //finally { }
            con.Close();
        }

        public static void Sil(EIsPersonel EES)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("delete from IsPersonel WHERE ((IsPersonel.IsID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EES.IsID);
            OleDbCommand comPersonelUcret = new OleDbCommand("update personeller set bakiye=bakiye-@Ucret WHERE ((personeller.personelID)=@PID)", con);
            comPersonelUcret.Parameters.AddWithValue("@Ucret", EES.Ucret);
            comPersonelUcret.Parameters.AddWithValue("@PID", EES.personelID);
            try
            {
                if (com.ExecuteNonQuery() > 0)
                {
                    if (comPersonelUcret.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Personel Ücret Silme İşlemi Yapılmıştır.", "Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { con.Close(); }
        }

        public static DataTable FEkstre(EIsPersonel EE)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Ucret,(select TakipNo from IsTakipForm where IsTakipForm.TakipID=IsPersonel.TakipID)as [TakipNo],(select FisNo from IsTakipForm where IsTakipForm.TakipID=IsPersonel.TakipID) as [FisNo],(select adsoyad from personeller where personeller.personelID=IsPersonel.personelID) as [adsoyad] from IsPersonel WHERE ((IsPersonel.TakipID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EE.TakipID);
            OleDbDataAdapter dep = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            dep.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable PEkstre(EIsPersonel EE)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select Ucret,(select TakipNo from IsTakipForm where IsTakipForm.TakipID=IsPersonel.TakipID)as [TakipNo],(select FisNo from IsTakipForm where IsTakipForm.TakipID=IsPersonel.TakipID) as [FisNo],(select adsoyad from personeller where personeller.personelID=IsPersonel.personelID) as [adsoyad] from IsPersonel WHERE ((IsPersonel.personelID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EE.personelID);
            OleDbDataAdapter dep = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            dep.Fill(dt);
            con.Close();
            return dt;
        }

        private static double gidenGT;

        public static double GT()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select SUM(Ucret) as [Ucret] from IsPersonel", con);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    gidenGT = Convert.ToDouble(dr["Ucret"].ToString());
                }
                else
                {
                    gidenGT = 0;
                }
            }
            con.Close();
            return gidenGT;
        }

        private static double gidenGTF;

        public static double GTF(EIsPersonel EeE)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select SUM(Ucret) as [Ucret] from IsPersonel WHERE ((IsPersonel.TakipID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EeE.TakipID);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    gidenGTF = Convert.ToDouble(dr["Ucret"].ToString());
                }
                else
                {
                    gidenGTF = 0;
                }
            }
            con.Close();
            return gidenGTF;
        }

        private static double gidenGTP;

        public static double GTP(EIsPersonel EeO)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_gelisimsoft;");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand com = new OleDbCommand("select SUM(Ucret) as [Ucret] from IsPersonel WHERE ((IsPersonel.personelID)=@ID)", con);
            com.Parameters.AddWithValue("@ID", EeO.personelID);
            OleDbDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    gidenGTP = Convert.ToDouble(dr["Ucret"].ToString());
                }
                else
                {
                    gidenGTP = 0;
                }
            }
            con.Close();
            return gidenGTP;
        }
    }
}