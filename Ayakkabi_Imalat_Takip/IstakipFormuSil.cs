using EntityKatmani;
using FacadeKatmani;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class IstakipFormuSil : Form
    {
        public IstakipFormuSil()
        {
            InitializeComponent();
        }

        private int takipidim;
        private OleDbConnection con = new OleDbConnection(connect.connectroad);

        private void ListemiGetir()
        {
            OleDbCommand com = new OleDbCommand("select TakipID,Tarih,TakipNo,unvan,Fisno,Kalip,Okce,Platfotm,Garni,Cift,Asorti,Renk,Kalite,(select adsoyad from personeller where personeller.personelID=kesimci)as [kesimci],(select adsoyad from personeller where personeller.personelID=temizleme)as [temizleme],(select adsoyad from personeller where personeller.personelID=kalipci)as [kalipci],(select adsoyad from personeller where personeller.personelID=montaj)as [montaj]from IsTakipForm inner join musteriler on IsTakipForm.MusteriID=musteriler.musteriID WHERE ((IsTakipForm.IsActive)=True) order by TakipNo", con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["TakipID"].HeaderText = "ID";
            dataGridView1.Columns["Tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["TakipNo"].HeaderText = "Takip No";
            dataGridView1.Columns["unvan"].HeaderText = "Müşteri";
            dataGridView1.Columns["FisNo"].HeaderText = "Fiş No";
            dataGridView1.Columns["Kalip"].HeaderText = "Kalip";
            dataGridView1.Columns["Okce"].HeaderText = "Ökçe";
            dataGridView1.Columns["Platfotm"].HeaderText = "Platform";
            dataGridView1.Columns["Garni"].HeaderText = "Garni";
            dataGridView1.Columns["Cift"].HeaderText = "Çift";
            dataGridView1.Columns["Asorti"].HeaderText = "Asorti";
            dataGridView1.Columns["Renk"].HeaderText = "Renk";
            dataGridView1.Columns["Kalite"].HeaderText = "Kalite";
            dataGridView1.Columns["kesimci"].HeaderText = "Kesimci";
            dataGridView1.Columns["temizleme"].HeaderText = "Temizleme";
            dataGridView1.Columns["kalipci"].HeaderText = "Kalıpçı";
            dataGridView1.Columns["montaj"].HeaderText = "Montajcı";
            dataGridView1.Columns[0].Visible = false;
        }

        private void TemizleYigen()
        {
            cift.Text = string.Empty;
            fisno.Text = string.Empty;
            garni.Text = string.Empty;
            kalip.Text = string.Empty;
            kalite.Text = string.Empty;
            okce.Text = string.Empty;
            platfotm.Text = string.Empty;
            renk.Text = string.Empty;
            takip.Text = string.Empty;
        }

        //Ya Hacı Bumbala Elemanları Tektek Combobox doldurucaz ve daha sonra önceden oluşturmuş olduğum departman tablosundan sıra ile hangi personel karşılık geliyorsa ona göre kayıt yapıcan.
        private void FuturaKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void IstakipFormuSil_Load(object sender, EventArgs e)
        {
            ListemiGetir();
            FuturaKontrol();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                takipidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                takip.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                mstri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                fisno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                kalip.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                okce.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                platfotm.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                garni.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cift.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                asortitxt.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                renk.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                kalite.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                kesimcitxt.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                temizlemetxt.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
                kaliptxt.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                montajtxt.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                takipidim = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tarihtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Remove(11);
                takip.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                mstri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                fisno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                kalip.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                okce.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                platfotm.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                garni.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cift.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                asortitxt.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                renk.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                kalite.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                kesimcitxt.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                temizlemetxt.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
                kaliptxt.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                montajtxt.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Satır Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult soruyoruz = MessageBox.Show(takip.Text + " " + "Nolu İş Takip Formunu Silmek İstediğinize Eminin misiniz ?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soruyoruz == DialogResult.Yes)
            {
                EIsTakip gelidgelaman = new EIsTakip();
                gelidgelaman.TakipID = takipidim;
                gelidgelaman.TakipNo = takip.Text;
                FIsTakip.IsTakipSil(gelidgelaman);
                ListemiGetir();
                TemizleYigen();
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}