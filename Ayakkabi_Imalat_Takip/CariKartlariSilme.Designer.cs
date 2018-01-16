namespace Ayakkabi_Imalat_Takip
{
    partial class CariKartlariSilme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CariKartlariSilme));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Personel = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.personelbtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.musteri = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.musteribtn = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tedarikci = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.tedarikcibtn = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.Personel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.musteri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tedarikci.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Personel);
            this.tabControl1.Controls.Add(this.musteri);
            this.tabControl1.Controls.Add(this.tedarikci);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(855, 413);
            this.tabControl1.TabIndex = 1;
            // 
            // Personel
            // 
            this.Personel.BackColor = System.Drawing.SystemColors.Highlight;
            this.Personel.Controls.Add(this.button1);
            this.Personel.Controls.Add(this.personelbtn);
            this.Personel.Controls.Add(this.dataGridView1);
            this.Personel.Location = new System.Drawing.Point(4, 22);
            this.Personel.Name = "Personel";
            this.Personel.Padding = new System.Windows.Forms.Padding(3);
            this.Personel.Size = new System.Drawing.Size(847, 387);
            this.Personel.TabIndex = 0;
            this.Personel.Text = "Personel Cari Kartları";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.bilgiler;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(52, 357);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.button1.Size = new System.Drawing.Size(359, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Bilgileri Getir";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // personelbtn
            // 
            this.personelbtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.personelbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.personelbtn.ForeColor = System.Drawing.SystemColors.Window;
            this.personelbtn.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.Temizle;
            this.personelbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.personelbtn.Location = new System.Drawing.Point(435, 357);
            this.personelbtn.Name = "personelbtn";
            this.personelbtn.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.personelbtn.Size = new System.Drawing.Size(359, 23);
            this.personelbtn.TabIndex = 1;
            this.personelbtn.Text = "Sil";
            this.personelbtn.UseVisualStyleBackColor = false;
            this.personelbtn.Click += new System.EventHandler(this.personelbtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 24;
            this.dataGridView1.Location = new System.Drawing.Point(4, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(837, 344);
            this.dataGridView1.TabIndex = 0;
            // 
            // musteri
            // 
            this.musteri.BackColor = System.Drawing.Color.Maroon;
            this.musteri.Controls.Add(this.button2);
            this.musteri.Controls.Add(this.musteribtn);
            this.musteri.Controls.Add(this.dataGridView2);
            this.musteri.Location = new System.Drawing.Point(4, 22);
            this.musteri.Name = "musteri";
            this.musteri.Padding = new System.Windows.Forms.Padding(3);
            this.musteri.Size = new System.Drawing.Size(847, 387);
            this.musteri.TabIndex = 1;
            this.musteri.Text = "Müşteri Cari Kartları";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.bilgiler;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(56, 356);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.button2.Size = new System.Drawing.Size(359, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Bilgileri Getir";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // musteribtn
            // 
            this.musteribtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.musteribtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.musteribtn.ForeColor = System.Drawing.SystemColors.Window;
            this.musteribtn.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.Temizle;
            this.musteribtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.musteribtn.Location = new System.Drawing.Point(435, 356);
            this.musteribtn.Name = "musteribtn";
            this.musteribtn.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.musteribtn.Size = new System.Drawing.Size(359, 23);
            this.musteribtn.TabIndex = 2;
            this.musteribtn.Text = "Sil";
            this.musteribtn.UseVisualStyleBackColor = false;
            this.musteribtn.Click += new System.EventHandler(this.musteribtn_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView2.ColumnHeadersHeight = 24;
            this.dataGridView2.Location = new System.Drawing.Point(5, 6);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(848, 344);
            this.dataGridView2.TabIndex = 1;
            // 
            // tedarikci
            // 
            this.tedarikci.BackColor = System.Drawing.Color.OrangeRed;
            this.tedarikci.Controls.Add(this.button3);
            this.tedarikci.Controls.Add(this.tedarikcibtn);
            this.tedarikci.Controls.Add(this.dataGridView3);
            this.tedarikci.Location = new System.Drawing.Point(4, 22);
            this.tedarikci.Name = "tedarikci";
            this.tedarikci.Padding = new System.Windows.Forms.Padding(3);
            this.tedarikci.Size = new System.Drawing.Size(847, 387);
            this.tedarikci.TabIndex = 2;
            this.tedarikci.Text = "Tedarikci Cari Kartları";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.ForeColor = System.Drawing.SystemColors.Window;
            this.button3.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.bilgiler;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(53, 356);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.button3.Size = new System.Drawing.Size(359, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Bilgileri Getir";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tedarikcibtn
            // 
            this.tedarikcibtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tedarikcibtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tedarikcibtn.ForeColor = System.Drawing.SystemColors.Window;
            this.tedarikcibtn.Image = global::Ayakkabi_Imalat_Takip.Properties.Resources.Temizle;
            this.tedarikcibtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tedarikcibtn.Location = new System.Drawing.Point(436, 356);
            this.tedarikcibtn.Name = "tedarikcibtn";
            this.tedarikcibtn.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.tedarikcibtn.Size = new System.Drawing.Size(359, 23);
            this.tedarikcibtn.TabIndex = 2;
            this.tedarikcibtn.Text = "Sil";
            this.tedarikcibtn.UseVisualStyleBackColor = false;
            this.tedarikcibtn.Click += new System.EventHandler(this.tedarikcibtn_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView3.ColumnHeadersHeight = 24;
            this.dataGridView3.Location = new System.Drawing.Point(5, 6);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(850, 344);
            this.dataGridView3.TabIndex = 1;
            // 
            // CariKartlariSilme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(861, 419);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CariKartlariSilme";
            this.Text = "Cari Kartları Silme";
            this.tabControl1.ResumeLayout(false);
            this.Personel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.musteri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tedarikci.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Personel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button personelbtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage musteri;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button musteribtn;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tedarikci;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button tedarikcibtn;
        private System.Windows.Forms.DataGridView dataGridView3;

    }
}