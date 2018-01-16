using System;
using System.Windows.Forms;

namespace Ayakkabi_Imalat_Takip
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private string secilen;
        public string LinkAdresi;

        public void Altaacilsin(Form alt)
        {
            bool durumcuk = false;
            foreach (Form personels in this.MdiChildren)
            {
                if (personels.Text == alt.Text)
                {
                    durumcuk = true;
                    personels.Activate();
                }
            }
            if (durumcuk == false)
            {
                alt.MdiParent = MdiParent;
                alt.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Müşteriler");
            treeView1.Nodes[0].Nodes.Add("0", "Yeni Müşteri Cari Kartı", 1, 1);
            treeView1.Nodes[0].Nodes.Add("1", "Müşteri Cari Kartı Güncelle", 1, 1);
            treeView1.Nodes[0].Nodes.Add("2", "Müşteri Cari Kartı Sil", 1, 1);
            secilen = "Mus";
            //treeView1.Nodes.Add(gelberi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Tedarikçiler");
            treeView1.Nodes[0].Nodes.Add("0", "Yeni Tedarikci Cari Kartı", 2, 2);
            treeView1.Nodes[0].Nodes.Add("1", "Tedarikci Cari Kartı Güncelle", 2, 2);
            treeView1.Nodes[0].Nodes.Add("2", "Tedarikci Cari Kartı Sil", 2, 2);
            //treeView1.Nodes.Add(gelberi);
            secilen = "Ted";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Personeller");
            gelberi.Nodes.Add("0", "Yeni Personel Cari Kartı", 3, 3);
            gelberi.Nodes.Add("1", "Personel Cari Kartı Güncelle", 3, 3);
            gelberi.Nodes.Add("2", "Personel Cari Kartı Sil", 3, 3);
            treeView1.Nodes.Add(gelberi);
            secilen = "Per";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Stoklar");
            gelberi.Nodes.Add("0", "Yeni Stok Cari Kartı", 4, 4);
            gelberi.Nodes.Add("1", "Stok Cari Kartı Güncelle", 4, 4);
            gelberi.Nodes.Add("2", "Stok Cari Kartı Sil", 4, 4);
            gelberi.Nodes.Add("3", "Stok Maliyetlendirme", 12, 12);
            gelberi.Nodes.Add("4", "Stok Maliyet Güncelle", 12, 12);
            gelberi.Nodes.Add("5", "Stok Maliyet Kartı Sil", 12, 12);
            treeView1.Nodes.Add(gelberi);
            secilen = "ok";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Satınlalma");
            gelberi.Nodes.Add("0", "Yeni Satınalma Faturası", 5, 5);
            gelberi.Nodes.Add("1", "Satınalma Faturası Güncelle", 5, 5);
            gelberi.Nodes.Add("2", "Satınalma Faturası Sil", 5, 5);
            treeView1.Nodes.Add(gelberi);
            secilen = "Alma";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Satış");
            gelberi.Nodes.Add("0", "Yeni Satış Faturası", 6, 6);
            gelberi.Nodes.Add("1", "Satış Faturası Güncelle", 6, 6);
            gelberi.Nodes.Add("2", "Satış Faturası Sil", 6, 6);
            treeView1.Nodes.Add(gelberi);
            secilen = "S";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Ödeme");
            gelberi.Nodes.Add("0", "Yeni Ödeme Makbuzu", 7, 7);
            gelberi.Nodes.Add("1", "Ödeme Makbuzu Güncelle", 7, 7);
            gelberi.Nodes.Add("2", "Ödeme Makbuzu Sil", 7, 7);
            treeView1.Nodes.Add(gelberi);
            secilen = "O";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Tahsilat");
            gelberi.Nodes.Add("0", "Yeni Tahsilat Makbuzu", 8, 8);
            gelberi.Nodes.Add("1", "Tahsilat Makbuzu Güncelle", 8, 8);
            gelberi.Nodes.Add("2", "Tahsilat Makbuzu Sil", 8, 8);
            treeView1.Nodes.Add(gelberi);
            secilen = "T";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("İş Takip Formu");
            gelberi.Nodes.Add("0", "Yeni İş Takip Formu", 9, 9);
            gelberi.Nodes.Add("1", "Yeni İş Takip Formu Güncelle", 9, 9);
            gelberi.Nodes.Add("2", "Yeni İş Takip Formu Sil", 9, 9);
            treeView1.Nodes.Add(gelberi);
            secilen = "I";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Raporlama");
            gelberi.Nodes.Add("0", "Müşteri Raporları", 1, 1);
            gelberi.Nodes.Add("1", "Tedarikci Raporları", 2, 2);
            gelberi.Nodes.Add("2", "Personel Raporları", 3, 3);
            gelberi.Nodes.Add("3", "Stok Raporları", 4, 4);
            gelberi.Nodes.Add("4", "Satınalma Raporları", 5, 5);
            gelberi.Nodes.Add("5", "Satış Raporları", 6, 6);
            gelberi.Nodes.Add("6", "Ödeme Raporları", 7, 7);
            gelberi.Nodes.Add("7", "Tahsilat Raporları", 8, 8);
            gelberi.Nodes.Add("8", "İş Takip Form Raporları", 9, 9);
            gelberi.Nodes.Add("9", "Personel Ücret Raporları", 11, 11);
            treeView1.Nodes.Add(gelberi);
            secilen = "R";
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            switch (secilen)
            {
                case "Mus":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        MusteriCariKart mstt = new MusteriCariKart();
                        Altaacilsin(mstt);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        CariKartlariguncelle gncl = new CariKartlariguncelle();
                        Altaacilsin(gncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        CariKartlariSilme silme = new CariKartlariSilme();
                        Altaacilsin(silme);
                    }
                    break;

                case "Ted":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        TedarikciCariKarti tdrikci = new TedarikciCariKarti();
                        Altaacilsin(tdrikci);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        CariKartlariguncelle gncl = new CariKartlariguncelle();
                        Altaacilsin(gncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        CariKartlariSilme silme = new CariKartlariSilme();
                        Altaacilsin(silme);
                    }
                    break;

                case "Per":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        PersonelCariKarti prsnl = new PersonelCariKarti();
                        Altaacilsin(prsnl);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        CariKartlariguncelle gncl = new CariKartlariguncelle();
                        Altaacilsin(gncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        CariKartlariSilme silme = new CariKartlariSilme();
                        Altaacilsin(silme);
                    }
                    break;

                case "ok":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        StokKarti stok = new StokKarti();
                        Altaacilsin(stok);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        StokKartlariGuncelle stokgncl = new StokKartlariGuncelle();
                        Altaacilsin(stokgncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        StokKartlariSilme stoksil = new StokKartlariSilme();
                        Altaacilsin(stoksil);
                    }
                    else if (treeView1.Nodes[0].Nodes["3"].IsSelected)
                    {
                        StokMaliyet sm = new StokMaliyet();
                        Altaacilsin(sm);
                    }
                    else if (treeView1.Nodes[0].Nodes["4"].IsSelected)
                    {
                        StokMG smg = new StokMG();
                        Altaacilsin(smg);
                    }
                    else if (treeView1.Nodes[0].Nodes["5"].IsSelected)
                    {
                        StokMS sms = new StokMS();
                        Altaacilsin(sms);
                    }
                    break;

                case "Alma":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Satinalma stn = new Satinalma();
                        Altaacilsin(stn);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        SatinalmaGuncelleme stngncl = new SatinalmaGuncelleme();
                        Altaacilsin(stngncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        SatinalmaSilme stnsil = new SatinalmaSilme();
                        Altaacilsin(stnsil);
                    }
                    break;

                case "S":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Satis sts = new Satis();
                        Altaacilsin(sts);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        satisguncelle stsgncl = new satisguncelle();
                        Altaacilsin(stsgncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        SatisSilme stssilme = new SatisSilme();
                        Altaacilsin(stssilme);
                    }
                    break;

                case "O":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Odeme odm = new Odeme();
                        Altaacilsin(odm);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        OdemelerGuncelleme odmgncl = new OdemelerGuncelleme();
                        Altaacilsin(odmgncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        OdemelerSilme odmsilme = new OdemelerSilme();
                        Altaacilsin(odmsilme);
                    }
                    break;

                case "T":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Tahsilat ths = new Tahsilat();
                        Altaacilsin(ths);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        TahsilatGuncelleme thsgncl = new TahsilatGuncelleme();
                        Altaacilsin(thsgncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        TahsilatlarSilme thssilme = new TahsilatlarSilme();
                        Altaacilsin(thssilme);
                    }
                    break;

                case "I":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Istakipformu istk = new Istakipformu();
                        Altaacilsin(istk);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        IstakipFormguncelleme istkgncl = new IstakipFormguncelleme();
                        Altaacilsin(istkgncl);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        IstakipFormuSil istksilme = new IstakipFormuSil();
                        Altaacilsin(istksilme);
                    }
                    break;

                case "R":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        Musteriler mstrirpr = new Musteriler();
                        Altaacilsin(mstrirpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        Tedarikciler tdrkcrpr = new Tedarikciler();
                        Altaacilsin(tdrkcrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        Personeller prsnl = new Personeller();
                        Altaacilsin(prsnl);
                    }
                    else if (treeView1.Nodes[0].Nodes["3"].IsSelected)
                    {
                        StoklarEkstresi stkrpr = new StoklarEkstresi();
                        Altaacilsin(stkrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["4"].IsSelected)
                    {
                        SatinalmaEkstresi stsrpr = new SatinalmaEkstresi();
                        Altaacilsin(stsrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["5"].IsSelected)
                    {
                        SatislarEkstre stsrpr = new SatislarEkstre();
                        Altaacilsin(stsrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["6"].IsSelected)
                    {
                        OdemelerEkstresi odmrpr = new OdemelerEkstresi();
                        Altaacilsin(odmrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["7"].IsSelected)
                    {
                        TahsilatEkstre thsrpr = new TahsilatEkstre();
                        Altaacilsin(thsrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["8"].IsSelected)
                    {
                        IsTakipFormlariRaporlama Istrpr = new IsTakipFormlariRaporlama();
                        Altaacilsin(Istrpr);
                    }
                    else if (treeView1.Nodes[0].Nodes["9"].IsSelected)
                    {
                        IsPersonelR Isr = new IsPersonelR();
                        Altaacilsin(Isr);
                    }
                    break;

                case "U":
                    if (treeView1.Nodes[0].Nodes["0"].IsSelected)
                    {
                        IsPersonel Is = new IsPersonel();
                        Altaacilsin(Is);
                    }
                    else if (treeView1.Nodes[0].Nodes["1"].IsSelected)
                    {
                        TekliIsPersonel tekli = new TekliIsPersonel();
                        Altaacilsin(tekli);
                    }
                    else if (treeView1.Nodes[0].Nodes["2"].IsSelected)
                    {
                        IsPersonelG Isg = new IsPersonelG();
                        Altaacilsin(Isg);
                    }
                    else if (treeView1.Nodes[0].Nodes["3"].IsSelected)
                    {
                        IsPersonelleriS Iss = new IsPersonelleriS();
                        Altaacilsin(Iss);
                    }
                    break;

                default:
                    break;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode gelberi = new TreeNode("Ücretlendirme");
            gelberi.Nodes.Add("0", "Çoklu Personel Ücretlendirme İşlemleri", 11, 11);
            gelberi.Nodes.Add("1", "Tekli Personel Ücretlendirme İşlemleri", 11, 11);
            gelberi.Nodes.Add("2", "Personel Ücret Güncelleme", 11, 11);
            gelberi.Nodes.Add("3", "Personel Ücret Silme", 11, 11);
            treeView1.Nodes.Add(gelberi);
            secilen = "U";
        }

        private void Menu_Load(object sender, EventArgs e)
        {
        }

        private void VersiyonBilgiLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(LinkAdresi);
        }
    }
}