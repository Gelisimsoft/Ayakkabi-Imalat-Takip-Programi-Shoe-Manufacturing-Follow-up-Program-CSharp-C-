using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class CariKartlariguncelle : Form
    {
        public CariKartlariguncelle()
        {
            InitializeComponent();
        }

        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani\\Data.mdb;Jet OLEDB:Database Password=ayakkabi_fcmedya;");

        private void personelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Personel Cari Kartı Güncellemek İstediğinize Emin misiniz. ?", "Güncelleme Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    OleDbCommandBuilder prslscb = new OleDbCommandBuilder(da);
                    da.Update(dt);
                    MessageBox.Show("Güncelleme Yapıldı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Programı Yeniden Başlatın,", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void musteribtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Müşteri Cari Kartı Güncellemek İstediğinize Emin misiniz ?", "Güncelleme Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    OleDbCommandBuilder mstriscb = new OleDbCommandBuilder(mstrida);
                    mstrida.Update(mstridt);
                    MessageBox.Show("Güncelleme Yapıldı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Programı Yeniden Başlatın,", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void tedarikcibtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Tedarikci Cari Kartı Güncellemek İstediğinize Emin misiniz ?", "Güncelleme Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    OleDbCommandBuilder tdrkciscb = new OleDbCommandBuilder(tdrkcida);
                    tdrkcida.Update(tdrkcidt);
                    MessageBox.Show("Güncelleme Yapıldı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Programı Yeniden Başlatın,", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private OleDbDataAdapter da;
        private DataTable dt;

        private void button1_Click(object sender, EventArgs e)
        {
            GelPersonel();
        }

        private void GelPersonel()
        {
            da = new OleDbDataAdapter("select * from personeller WHERE ((personeller.IsActive)=True) order by adsoyad", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["personelID"].HeaderText = "No";
            dataGridView1.Columns["adsoyad"].HeaderText = "Adı Soyadı ";
            dataGridView1.Columns["tcno"].HeaderText = "Tc Kimlik No";
            dataGridView1.Columns["adres"].HeaderText = "Adres";
            dataGridView1.Columns["ceptlf"].HeaderText = "Cep Tel";
            dataGridView1.Columns["evtlf"].HeaderText = "Ev Tel";
            dataGridView1.Columns["isegiris"].HeaderText = "İşe Giriş Tarihi";
            dataGridView1.Columns["cikisdate"].HeaderText = "İşten Çıkış Tarihi";
            dataGridView1.Columns["departmanID"].HeaderText = "Departman No";
            dataGridView1.Columns["bakiye"].HeaderText = "Bakiye";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }

        private OleDbDataAdapter mstrida;
        private DataTable mstridt;

        private void button2_Click(object sender, EventArgs e)
        {
            GelMusteri();
        }

        private void GelMusteri()
        {
            mstrida = new OleDbDataAdapter("select * from musteriler WHERE ((musteriler.IsActive)=True) order by unvan", con);
            mstridt = new DataTable();
            mstrida.Fill(mstridt);
            dataGridView2.DataSource = mstridt;
            dataGridView2.Columns["musteriID"].HeaderText = "No";
            dataGridView2.Columns["unvan"].HeaderText = "Ünvan";
            dataGridView2.Columns["adres"].HeaderText = "Adres";
            dataGridView2.Columns["sehir"].HeaderText = "Şehir";
            dataGridView2.Columns["telefon"].HeaderText = "Telefon";
            dataGridView2.Columns["faks"].HeaderText = "Faks";
            dataGridView2.Columns["vdairesi"].HeaderText = "Vergi Dairesi";
            dataGridView2.Columns["vno"].HeaderText = "Vergi No";
            dataGridView2.Columns["bakiye"].HeaderText = "Bakiye";
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[9].Visible = false;
        }

        private OleDbDataAdapter tdrkcida;
        private DataTable tdrkcidt;

        private void button3_Click(object sender, EventArgs e)
        {
            GelTedarikci();
        }

        private void GelTedarikci()
        {
            tdrkcida = new OleDbDataAdapter("select * from tedarikciler WHERE ((tedarikciler.IsActive)=True) order by unvan", con);
            tdrkcidt = new DataTable();
            tdrkcida.Fill(tdrkcidt);
            dataGridView3.DataSource = tdrkcidt;
            dataGridView3.Columns["TedarikciID"].HeaderText = "No";
            dataGridView3.Columns["unvan"].HeaderText = "Ünvan";
            dataGridView3.Columns["adres"].HeaderText = "Adres";
            dataGridView3.Columns["sehir"].HeaderText = "Şehir";
            dataGridView3.Columns["telefon"].HeaderText = "Telefon";
            dataGridView3.Columns["faks"].HeaderText = "Faks";
            dataGridView3.Columns["vergidairesi"].HeaderText = "Vergi Dairesi";
            dataGridView3.Columns["vergino"].HeaderText = "Vergi No";
            dataGridView3.Columns["bakiye"].HeaderText = "Bakiye";
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[9].Visible = false;
        }

        private void CariKartlariguncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                GelPersonel();
                GelTedarikci();
                GelMusteri();
            }
        }
    }
}