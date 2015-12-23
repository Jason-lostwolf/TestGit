using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVERGRANDE.Controller;

namespace EVERGRANDE
{
    public partial class FrmPackageScan : FrmScanBase
    {
        public FrmPackageScan()
        {
            InitializeComponent();

            this.Load += new EventHandler(FrmScan_Load);
        }

        private PackageScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {
            //数据绑定
            this.Controller = new PackageScanController();

            this.txtBarcode.DataBindings.Add("Text", this.Controller.ViewModel, "Barcode", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtSN.DataBindings.Add("Text", this.Controller.ViewModel, "SN", false, DataSourceUpdateMode.OnPropertyChanged);
            this.lblCount.DataBindings.Add("Text", this.Controller.ViewModel, "ListCount", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnExport.DataBindings.Add("Enabled", this.Controller.ViewModel, "IsSaveEnable");

            this.Controller.UIRefresh += new EventHandler<ScanEventArgs>(Controller_UIRefresh);

            this.Controller.Init();
        }

        #region UI更新
        void Controller_UIRefresh(object sender, ScanEventArgs e)
        {
            if (e.ScanData == ScanData.FirstBarcode)
            {
                this.txtBarcode.Focus();
                this.txtBarcode.SelectAll();
            }
            else
            {
                this.txtSN.Focus();
                this.txtSN.SelectAll();
            }

            if (e.RefreshLabel != null)
            {
                this.lblError.Text = e.RefreshLabel.Text;
                this.lblError.BackColor = e.RefreshLabel.BackColor;
                this.lblBackground.BackColor = e.RefreshLabel.ForeColor;
                this.lblError.ForeColor = e.RefreshLabel.ForeColor;
            }
            else
            {
                this.lblError.Text = string.Empty;
                this.lblError.BackColor = Color.Transparent;
                this.lblError.ForeColor = Color.Transparent;
                this.lblBackground.BackColor = Color.Transparent;
            }
        }
        #endregion

        #region 事件
        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.SaveBarcode();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.CheckBarcode();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Controller.Export();
        }
        #endregion

        #region 扫描触发事件
        //protected override void OnRead(Symbol.Barcode.ReaderData readerData)
        //{
        //    if (readerData.Result == Symbol.Results.SUCCESS)
        //    {
        //        if (this.txtBarcode.Focused)
        //        {
        //            this.txtBarcode.Text = readerData.Text;
        //            this.Controller.CheckBarcode();
        //        }
        //        else
        //        {
        //            this.txtSN.Text = readerData.Text;
        //            this.Controller.SaveBarcode();
        //        }
        //    }
        //}
        #endregion
    }
}