namespace BTL
{
    partial class frmBaoCao
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboThongKeTinhTrang = new System.Windows.Forms.ComboBox();
            this.cboThongKePhong = new System.Windows.Forms.ComboBox();
            this.btnNewReport = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rpvBaoCaoVatTu = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnThoat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnThoat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboThongKeTinhTrang);
            this.groupBox1.Controls.Add(this.cboThongKePhong);
            this.groupBox1.Controls.Add(this.btnNewReport);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(915, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều kiện";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lọc theo Tình Trạng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lọc theo Phòng :";
            // 
            // cboThongKeTinhTrang
            // 
            this.cboThongKeTinhTrang.FormattingEnabled = true;
            this.cboThongKeTinhTrang.Location = new System.Drawing.Point(455, 33);
            this.cboThongKeTinhTrang.Name = "cboThongKeTinhTrang";
            this.cboThongKeTinhTrang.Size = new System.Drawing.Size(140, 24);
            this.cboThongKeTinhTrang.TabIndex = 2;
            // 
            // cboThongKePhong
            // 
            this.cboThongKePhong.FormattingEnabled = true;
            this.cboThongKePhong.Location = new System.Drawing.Point(135, 33);
            this.cboThongKePhong.Name = "cboThongKePhong";
            this.cboThongKePhong.Size = new System.Drawing.Size(140, 24);
            this.cboThongKePhong.TabIndex = 1;
            // 
            // btnNewReport
            // 
            this.btnNewReport.Location = new System.Drawing.Point(661, 21);
            this.btnNewReport.Name = "btnNewReport";
            this.btnNewReport.Size = new System.Drawing.Size(171, 35);
            this.btnNewReport.TabIndex = 0;
            this.btnNewReport.Text = "&Tạo báo cáo";
            this.btnNewReport.UseVisualStyleBackColor = true;
            this.btnNewReport.Click += new System.EventHandler(this.BtnNewReport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rpvBaoCaoVatTu);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(915, 350);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả báo cáo";
            // 
            // rpvBaoCaoVatTu
            // 
            this.rpvBaoCaoVatTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvBaoCaoVatTu.Location = new System.Drawing.Point(3, 18);
            this.rpvBaoCaoVatTu.Name = "rpvBaoCaoVatTu";
            this.rpvBaoCaoVatTu.ServerReport.BearerToken = null;
            this.rpvBaoCaoVatTu.Size = new System.Drawing.Size(909, 329);
            this.rpvBaoCaoVatTu.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(661, 59);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(171, 35);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Th&oát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.BtnThoat_Click);
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBaoCao";
            this.Text = "Báo Cáo Vật Tư";
            this.Load += new System.EventHandler(this.FrmBaoCao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNewReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.Reporting.WinForms.ReportViewer rpvBaoCaoVatTu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboThongKeTinhTrang;
        private System.Windows.Forms.ComboBox cboThongKePhong;
        private System.Windows.Forms.Button btnThoat;
    }
}