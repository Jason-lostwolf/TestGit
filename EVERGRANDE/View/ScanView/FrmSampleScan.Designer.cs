namespace EVERGRANDE
{
    partial class FrmSampleScan
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
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblBackground = new System.Windows.Forms.Label();
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
            this.label1.Location = new System.Drawing.Point(10, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 26);
            this.label1.Text = "样板条码";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(10, 82);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(303, 29);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
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
            this.label2.Location = new System.Drawing.Point(10, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 20);
            this.label2.Text = "UPG生产标签条码";
            // 
            // txtSN
            // 
            this.txtSN.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtSN.Location = new System.Drawing.Point(10, 141);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(303, 29);
            this.txtSN.TabIndex = 2;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCount.Location = new System.Drawing.Point(10, 180);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(303, 30);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.BackColor = System.Drawing.Color.Red;
            this.lblError.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(22, 226);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(273, 32);
            this.lblError.Text = "不一致";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBackground
            // 
            this.lblBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackground.BackColor = System.Drawing.Color.Red;
            this.lblBackground.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.lblBackground.ForeColor = System.Drawing.Color.White;
            this.lblBackground.Location = new System.Drawing.Point(10, 219);
            this.lblBackground.Name = "lblBackground";
            this.lblBackground.Size = new System.Drawing.Size(303, 45);
            // 
            // FrmSampleScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(322, 274);
            this.ControlBox = false;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblBackground);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSampleScan";
            this.Text = "样本比对";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblBackground;
    }
}

