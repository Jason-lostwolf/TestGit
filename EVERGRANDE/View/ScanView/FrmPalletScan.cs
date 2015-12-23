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
    public partial class FrmPalletScan : FrmScanBase
    {
        public FrmPalletScan()
        {
            InitializeComponent();

            this.Load += new EventHandler(FrmScan_Load);
        }

        private PalletScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {
            //数据绑定
            this.Controller = new PalletScanController();

            this.txtPN.DataBindings.Add("Text", this.Controller.ViewModel, "PN", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtQTY.DataBindings.Add("Text", this.Controller.ViewModel, "Qty", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtSN.DataBindings.Add("Text", this.Controller.ViewModel, "SN", false, DataSourceUpdateMode.OnPropertyChanged);
            this.lblCount.DataBindings.Add("Text", this.Controller.ViewModel, "ListCount", false, DataSourceUpdateMode.OnPropertyChanged);
            this.btnExport.DataBindings.Add("Enabled", this.Controller.ViewModel, "IsExportEnabled");
            this.btnScan.DataBindings.Add("Enabled", this.Controller.ViewModel, "IsScanEnabled");

            this.Controller.UIRefresh += new EventHandler<ScanEventArgs>(Controller_UIRefresh);

            this.Controller.Init();
        }

        #region UI更新
        void Controller_UIRefresh(object sender, ScanEventArgs e)
        {
            if (e.ScanData == ScanData.FirstBarcode)
            {
                this.txtPN.Focus();
                this.txtPN.SelectAll();
            }
            else if (e.ScanData == ScanData.SecondBarcode)
            {
                this.txtQTY.Focus();
                this.txtQTY.SelectAll();
            }
            else
            {
                this.txtSN.Focus();
                this.txtSN.SelectAll();
            }

            //if (e.RefreshLabel != null)
            //{
            //    //this.lblError.Text = e.RefreshLabel.Text;
            //    //this.lblError.BackColor = e.RefreshLabel.BackColor;
            //    //this.lblBackground.BackColor = e.RefreshLabel.ForeColor;
            //    //this.lblError.ForeColor = e.RefreshLabel.ForeColor;
            //}
            //else
            //{
            //    //this.lblError.Text = string.Empty;
            //    //this.lblError.BackColor = Color.Transparent;
            //    //this.lblError.ForeColor = Color.Transparent;
            //    //this.lblBackground.BackColor = Color.Transparent;
            //}
        }
        #endregion

        #region 事件

        private void txtPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.CheckPN();
            }
        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.CheckQTY();
            }
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.CheckSN();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            
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
        //        if (this.txtPN.Focused)
        //        {
        //            this.txtPN.Text = readerData.Text;
        //            this.Controller.CheckPN();
        //        }
        //        else if (this.txtQTY.Focused)
        //        {
        //            this.txtQTY.Text = readerData.Text;
        //            this.Controller.CheckQTY();
        //        }
        //        else
        //        {
        //            this.txtSN.Text = readerData.Text;
        //            this.Controller.CheckSN();
        //        }
        //    }
        //}
        #endregion

        private void txtPN_GotFocus(object sender, EventArgs e)
        {
            this.Controller.IsPNChecked = false;
        }

        private void txtQTY_GotFocus(object sender, EventArgs e)
        {
            this.Controller.IsQTYChecked = false;
        }

        private void txtSN_GotFocus(object sender, EventArgs e)
        {
            this.Controller.IsSNChecked = false;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            this.Controller.ScanDetail();
        }
    }
}