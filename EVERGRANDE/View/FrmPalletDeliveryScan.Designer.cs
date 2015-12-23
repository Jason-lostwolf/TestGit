namespace EVERGRANDE
{
    partial class FrmPalletDeliveryScan
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.YellowGreen;
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(0, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 40);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 26);
            this.label1.Text = "出库单号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.YellowGreen;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(79, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.Text = "出库数量";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(9, 152);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(219, 108);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.BackColor = System.Drawing.Color.YellowGreen;
            this.btnScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnScan.ForeColor = System.Drawing.Color.Black;
            this.btnScan.Location = new System.Drawing.Point(158, 0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(80, 40);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "确认";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtOrderNo.Location = new System.Drawing.Point(109, 61);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(119, 29);
            this.txtOrderNo.TabIndex = 6;
            // 
            // txtQTY
            // 
            this.txtQTY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQTY.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtQTY.Location = new System.Drawing.Point(109, 103);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(119, 29);
            this.txtQTY.TabIndex = 7;
            // 
            // FrmPalletDeliveryScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.ControlBox = false;
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPalletDeliveryScan";
            this.Text = "出货扫描";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.TextBox txtQTY;
    }
}

