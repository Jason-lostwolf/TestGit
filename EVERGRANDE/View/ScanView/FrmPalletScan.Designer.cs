namespace EVERGRANDE
{
    partial class FrmPalletScan
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
            this.txtPN = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.YellowGreen;
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnExport.ForeColor = System.Drawing.Color.White;
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
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 26);
            this.label1.Text = "PN";
            // 
            // txtPN
            // 
            this.txtPN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPN.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtPN.Location = new System.Drawing.Point(52, 61);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(261, 29);
            this.txtPN.TabIndex = 2;
            this.txtPN.GotFocus += new System.EventHandler(this.txtPN_GotFocus);
            this.txtPN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPN_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.YellowGreen;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(80, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(10, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 30);
            this.label2.Text = "QTY";
            // 
            // txtQTY
            // 
            this.txtQTY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQTY.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtQTY.Location = new System.Drawing.Point(52, 100);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(261, 29);
            this.txtQTY.TabIndex = 2;
            this.txtQTY.GotFocus += new System.EventHandler(this.txtQTY_GotFocus);
            this.txtQTY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQTY_KeyDown);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCount.Location = new System.Drawing.Point(10, 186);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(303, 42);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.BackColor = System.Drawing.Color.YellowGreen;
            this.btnScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Location = new System.Drawing.Point(242, 0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(80, 40);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "确认";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(10, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 30);
            this.label3.Text = "SN";
            // 
            // txtSN
            // 
            this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSN.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtSN.Location = new System.Drawing.Point(52, 140);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(261, 29);
            this.txtSN.TabIndex = 2;
            this.txtSN.GotFocus += new System.EventHandler(this.txtSN_GotFocus);
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // FrmPalletScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(322, 274);
            this.ControlBox = false;
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.txtPN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPalletScan";
            this.Text = "IPO托盘标签";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPN;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSN;
    }
}

