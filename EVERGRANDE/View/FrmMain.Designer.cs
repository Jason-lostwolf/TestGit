namespace EVERGRANDE.View
{
    partial class FrmMain
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
            this.btnPackagingScan = new System.Windows.Forms.Button();
            this.btnDeliveryScan = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPackagingScan
            // 
            this.btnPackagingScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPackagingScan.BackColor = System.Drawing.Color.Orange;
            this.btnPackagingScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnPackagingScan.ForeColor = System.Drawing.Color.Black;
            this.btnPackagingScan.Location = new System.Drawing.Point(29, 20);
            this.btnPackagingScan.Name = "btnPackagingScan";
            this.btnPackagingScan.Size = new System.Drawing.Size(181, 40);
            this.btnPackagingScan.TabIndex = 0;
            this.btnPackagingScan.Text = "装箱扫描";
            this.btnPackagingScan.Click += new System.EventHandler(this.btnPackagingScan_Click);
            // 
            // btnDeliveryScan
            // 
            this.btnDeliveryScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeliveryScan.BackColor = System.Drawing.Color.Orange;
            this.btnDeliveryScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnDeliveryScan.ForeColor = System.Drawing.Color.Black;
            this.btnDeliveryScan.Location = new System.Drawing.Point(29, 70);
            this.btnDeliveryScan.Name = "btnDeliveryScan";
            this.btnDeliveryScan.Size = new System.Drawing.Size(181, 40);
            this.btnDeliveryScan.TabIndex = 1;
            this.btnDeliveryScan.Text = "出货扫描";
            this.btnDeliveryScan.Click += new System.EventHandler(this.btnDeliveryScan_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Orange;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(29, 201);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 40);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDeliveryScan);
            this.Controls.Add(this.btnPackagingScan);
            this.Name = "FrmMain";
            this.Text = "条码采集系统";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPackagingScan;
        private System.Windows.Forms.Button btnDeliveryScan;
        private System.Windows.Forms.Button btnExit;
    }
}