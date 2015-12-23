namespace EVERGRANDE
{
    partial class FrmPalletDeliveryDetailScan
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 26);
            this.label1.Text = "出库单号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.YellowGreen;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(10, 162);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(219, 97);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 30);
            this.label3.Text = "箱号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSN
            // 
            this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSN.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtSN.Location = new System.Drawing.Point(107, 103);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(122, 29);
            this.txtSN.TabIndex = 2;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblOrderNo.ForeColor = System.Drawing.Color.White;
            this.lblOrderNo.Location = new System.Drawing.Point(107, 61);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(122, 26);
            // 
            // FrmPalletDeliveryDetailScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.ControlBox = false;
            this.Controls.Add(this.lblOrderNo);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPalletDeliveryDetailScan";
            this.Text = "出货扫描";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label lblOrderNo;
    }
}

