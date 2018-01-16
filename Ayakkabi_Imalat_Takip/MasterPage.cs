using System;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class MasterPage : Form
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        public void AltacilanFormlar(Form altformcuklar)
        {
            //Pencerenin açık mı ? Kapalı mı ? Olduğunu tespit edicek mantıksal tipte durumlar değişkenini oluşturuyoruz.
            bool durumlar = false;
            //Alt forum özelliği veren MDI children özellikleri elemanlar değişkenine tanımlıyoruz.
            foreach (Form elemanlar in this.MdiChildren)
            {
                //Aktif halde çalışan form olarak tanımladığımız elemanlar değişkeni o anda açık olan bir form var mı ? kontrolü yapıyor
                if (elemanlar.Text == altformcuklar.Text)
                {
                    //Eğer varsa durumlar değişkenin durumunu true çeviriyor
                    durumlar = true;
                    //Ve formlar active hale getiriyor.
                    elemanlar.Activate();
                }
            }
            //Eğer durumlar ilk halindeki gibi ise
            if (durumlar == false)
            {
                //Açılmak istenen form'un mdi özellğine this özelliği atanır
                altformcuklar.MdiParent = this;
                //Pencere açılır
                altformcuklar.Show();
            }
        }

        private void yeniMüşteriCariKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriCariKart musteri = new MusteriCariKart();
            AltacilanFormlar(musteri);
        }

        private void yeniTedarikciCariKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TedarikciCariKarti tedarikci = new TedarikciCariKarti();
            AltacilanFormlar(tedarikci);
        }

        private void yeniPersonelCariKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelCariKarti personel = new PersonelCariKarti();
            AltacilanFormlar(personel);
        }

        private void cariKartıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CariKartlariguncelle carikartguncelle = new CariKartlariguncelle();
            AltacilanFormlar(carikartguncelle);
        }

        private void cariKartıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CariKartlariSilme carikartlarisil = new CariKartlariSilme();
            AltacilanFormlar(carikartlarisil);
        }

        private void yeniStokKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokKarti stokkarti = new StokKarti();
            AltacilanFormlar(stokkarti);
        }

        private void yeniSatınalmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Satinalma satinalma = new Satinalma();
            AltacilanFormlar(satinalma);
        }

        private void güncelleToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SatinalmaGuncelleme satinalmaguncelle = new SatinalmaGuncelleme();
            AltacilanFormlar(satinalmaguncelle);
        }

        private void silToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SatinalmaSilme satinalmasilme = new SatinalmaSilme();
            AltacilanFormlar(satinalmasilme);
        }

        private void yeniSatışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Satis satislar = new Satis();
            AltacilanFormlar(satislar);
        }

        private void güncelleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            satisguncelle satislariguncelle = new satisguncelle();
            AltacilanFormlar(satislariguncelle);
        }

        private void silToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SatisSilme satissilme = new SatisSilme();
            AltacilanFormlar(satissilme);
        }

        private void yeniTahsilatKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tahsilat tahsilat = new Tahsilat();
            AltacilanFormlar(tahsilat);
        }

        private void yeniÖdemeKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Odeme odemeler = new Odeme();
            AltacilanFormlar(odemeler);
        }

        private void yeniİşTakipFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Istakipformu istakipformlari = new Istakipformu();
            AltacilanFormlar(istakipformlari);
        }

        private void güncelleToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            IstakipFormguncelleme istakipformlariguncelle = new IstakipFormguncelleme();
            AltacilanFormlar(istakipformlariguncelle);
        }

        private void silToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            IstakipFormuSil istakipformusil = new IstakipFormuSil();
            AltacilanFormlar(istakipformusil);
        }

        private void müşterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            AltacilanFormlar(musteriler);
        }

        private void tedarikcilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tedarikciler tedarikci = new Tedarikciler();
            AltacilanFormlar(tedarikci);
        }

        private void personellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personeller personel = new Personeller();
            AltacilanFormlar(personel);
        }

        private void yapılanSatışlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SatislarEkstre satislarraporlama = new SatislarEkstre();
            AltacilanFormlar(satislarraporlama);
        }

        private void yapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SatinalmaEkstresi satinalmaekstre = new SatinalmaEkstresi();
            AltacilanFormlar(satinalmaekstre);
        }

        private void stoklarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoklarEkstresi stoklariekstre = new StoklarEkstresi();
            AltacilanFormlar(stoklariekstre);
        }

        private void işTakipFormlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsTakipFormlariRaporlama istakipleri = new IsTakipFormlariRaporlama();
            AltacilanFormlar(istakipleri);
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About hakkinida = new About();
            AltacilanFormlar(hakkinida);
        }

        private void tahsilatlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TahsilatEkstre tahsilatim = new TahsilatEkstre();
            AltacilanFormlar(tahsilatim);
        }

        private void ödemelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OdemelerEkstresi odemelerim = new OdemelerEkstresi();
            AltacilanFormlar(odemelerim);
        }

        private void ödemeGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OdemelerGuncelleme odemelerguncelleme = new OdemelerGuncelleme();
            AltacilanFormlar(odemelerguncelleme);
        }

        private void tahsilatGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TahsilatGuncelleme tahguncelle = new TahsilatGuncelleme();
            AltacilanFormlar(tahguncelle);
        }

        private void ödemeMakbuzuSilmeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OdemelerSilme odemesil = new OdemelerSilme();
            AltacilanFormlar(odemesil);
        }

        private void tahsilatMakbuzuSilmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TahsilatlarSilme tahsilsil = new TahsilatlarSilme();
            AltacilanFormlar(tahsilsil);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MusteriCariKart iconmusteri = new MusteriCariKart();
            AltacilanFormlar(iconmusteri);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TedarikciCariKarti icontedarikci = new TedarikciCariKarti();
            AltacilanFormlar(icontedarikci);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PersonelCariKarti iconpersonel = new PersonelCariKarti();
            AltacilanFormlar(iconpersonel);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            StokKarti iconstok = new StokKarti();
            AltacilanFormlar(iconstok);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Satis iconsatis = new Satis();
            AltacilanFormlar(iconsatis);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Satinalma iconsatinalma = new Satinalma();
            AltacilanFormlar(iconsatinalma);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Tahsilat icontahsilat = new Tahsilat();
            AltacilanFormlar(icontahsilat);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Odeme iconodeme = new Odeme();
            AltacilanFormlar(iconodeme);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Istakipformu iconistakip = new Istakipformu();
            AltacilanFormlar(iconistakip);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\WINDOWS\\system32\\calc.exe");
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Takvim tkvm = new Takvim();
            AltacilanFormlar(tkvm);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            DialogResult sor = MessageBox.Show("Çıkmak İstediğinize Emin siniz ?", "Çıkış", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (sor == DialogResult.OK)
            {
                Application.ExitThread();
            }
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            About iconhkdnd = new About();
            AltacilanFormlar(iconhkdnd);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Sifre iconsfre = new Sifre();
            AltacilanFormlar(iconsfre);
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            Menu iconmenumuzu = new Menu();
            iconmenumuzu.Show();
        }

        private void şifreAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifre iconsifre = new Sifre();
            AltacilanFormlar(iconsifre);
        }

        private void MasterPage_Load(object sender, EventArgs e)
        {
            Menu menucuk = new Menu();
            AltacilanFormlar(menucuk);

            //Giris gir = new Giris();
            //gir.ShowDialog();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            BackupTake iconbackup = new BackupTake();
            AltacilanFormlar(iconbackup);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            Backup.BackupUpload iconback = new Backup.BackupUpload();
            AltacilanFormlar(iconback);
        }

        private void yedekAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupTake backup = new BackupTake();
            AltacilanFormlar(backup);
        }

        private void yedekGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backup.BackupUpload back = new Backup.BackupUpload();
            AltacilanFormlar(back);
        }

        private void toolStripButton16_Click_1(object sender, EventArgs e)
        {
            Menu m = new Menu();
            AltacilanFormlar(m);
        }

        private void persoenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPersonel S = new IsPersonel();
            AltacilanFormlar(S);
        }

        private void güncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPersonelG IG = new IsPersonelG();
            AltacilanFormlar(IG);
        }

        private void silmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPersonelleriS IS = new IsPersonelleriS();
            AltacilanFormlar(IS);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gelisimsoft.com");
        }

        private void stokMaliyetlendirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokMaliyet sm = new StokMaliyet();
            AltacilanFormlar(sm);
        }

        private void stokKartıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokKartlariGuncelle stokkartiguncelle = new StokKartlariGuncelle();
            AltacilanFormlar(stokkartiguncelle);
        }

        private void maliyetStokKartıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokMG stm = new StokMG();
            AltacilanFormlar(stm);
        }

        private void stokKartıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokKartlariSilme stokkartisil = new StokKartlariSilme();
            AltacilanFormlar(stokkartisil);
        }

        private void stokMaliyetKartıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokMS stms = new StokMS();
            AltacilanFormlar(stms);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            StokMaliyet sm = new StokMaliyet();
            AltacilanFormlar(sm);
        }

        private void programHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About iconhkdnd = new About();
            AltacilanFormlar(iconhkdnd);
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("Yardım.chm");
            Help.ShowHelp(this, Application.StartupPath + "\\" + "Yardim.chm");
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            // System.Diagnostics.Process.Start("Yardım.chm");
            Help.ShowHelp(this, Application.StartupPath + "\\" + "Yardim.chm");
        }

        private void personelÜcretlendirmeleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPersonelR r = new IsPersonelR();
            AltacilanFormlar(r);
        }

        private void tekliPersonelÜcretlendirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekliIsPersonel tek = new TekliIsPersonel();
            AltacilanFormlar(tek);
        }
    }
}