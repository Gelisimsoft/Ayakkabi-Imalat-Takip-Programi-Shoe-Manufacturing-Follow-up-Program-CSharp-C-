namespace Ayakkabi_Imalat_Takip
{
    partial class Personeller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Personeller));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dprtman = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bilgigetir = new System.Windows.Forms.Button();
            this.tckimlik = new System.Windows.Forms.TextBox();
            this.adsyod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dprtman);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 514);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personel Arama Seçenekleri";
            // 
            // dprtman
            // 
            this.dprtman.BackColor = System.Drawing.SystemColors.HotTrack;
            this.dprtman.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dprtman.ForeColor = System.Drawing.SystemColors.Window;
            this.dprtman.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.application_cascade;
            this.dprtman.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dprtman.Location = new System.Drawing.Point(150, 14);
            this.dprtman.Name = "dprtman";
            this.dprtman.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.dprtman.Size = new System.Drawing.Size(198, 50);
            this.dprtman.TabIndex = 2;
            this.dprtman.Text = "Personellerini Getir";
            this.dprtman.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dprtman.UseVisualStyleBackColor = false;
            this.dprtman.Click += new System.EventHandler(this.dprtman_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Departman Seçiniz";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(384, 8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(130, 56);
            this.listBox1.TabIndex = 7;
            this.listBox1.Visible = false;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // bilgigetir
            // 
            this.bilgigetir.BackColor = System.Drawing.SystemColors.HotTrack;
            this.bilgigetir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bilgigetir.ForeColor = System.Drawing.SystemColors.Window;
            this.bilgigetir.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.application_cascade;
            this.bilgigetir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bilgigetir.Location = new System.Drawing.Point(240, 12);
            this.bilgigetir.Name = "bilgigetir";
            this.bilgigetir.Size = new System.Drawing.Size(138, 51);
            this.bilgigetir.TabIndex = 6;
            this.bilgigetir.Text = "Bilgileri Getir";
            this.bilgigetir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bilgigetir.UseVisualStyleBackColor = false;
            this.bilgigetir.Click += new System.EventHandler(this.bilgigetir_Click_1);
            // 
            // tckimlik
            // 
            this.tckimlik.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tckimlik.Location = new System.Drawing.Point(122, 43);
            this.tckimlik.Name = "tckimlik";
            this.tckimlik.Size = new System.Drawing.Size(111, 20);
            this.tckimlik.TabIndex = 5;
            this.tckimlik.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tckimlik_MouseClick);
            // 
            // adsyod
            // 
            this.adsyod.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.adsyod.Location = new System.Drawing.Point(122, 16);
            this.adsyod.Name = "adsyod";
            this.adsyod.Size = new System.Drawing.Size(111, 20);
            this.adsyod.TabIndex = 4;
            this.adsyod.MouseClick += new System.Windows.Forms.MouseEventHandler(this.adsyod_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "TcKimlik No Giriniz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ad Soyad Giriniz";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.bilgigetir);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tckimlik);
            this.groupBox2.Controls.Add(this.adsyod);
            this.groupBox2.Location = new System.Drawing.Point(372, 514);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 70);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Arama Seçenekleri";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listView1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(982, 505);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Departmanı";
            this.columnHeader1.Width = 77;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Adı Soyadı";
            this.columnHeader2.Width = 106;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tc Kimlik No";
            this.columnHeader3.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Adres";
            this.columnHeader4.Width = 237;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Cep Telefonu";
            this.columnHeader5.Width = 87;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ev Telefonu";
            this.columnHeader6.Width = 79;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "İşe Giriş Tarihi";
            this.columnHeader7.Width = 82;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Çıkış Tarihi";
            this.columnHeader8.Width = 72;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Bakiye";
            this.columnHeader9.Width = 79;
            // 
            // Personeller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(986, 586);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Personeller";
            this.Text = "Sistemde Kayıtlı Personellerin Listesi";
            this.Load += new System.EventHandler(this.Personeller_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button dprtman;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button bilgigetir;
        private System.Windows.Forms.TextBox tckimlik;
        private System.Windows.Forms.TextBox adsyod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
    }
}