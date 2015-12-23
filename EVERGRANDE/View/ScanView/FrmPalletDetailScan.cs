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
    public partial class FrmPalletDetailScan : FrmScanBase
    {
        public FrmPalletDetailScan(string pn,int qty,string sn,BindingList<PalletProduct> productList)
        {
            InitializeComponent();

            //数据绑定
            this.Controller = new PalletDetailScanController();

            this.Controller.ViewModel.PalletPN = pn;
            this.Controller.ViewModel.PalletQty = qty;
            this.Controller.ViewModel.PalletSN = sn;
            this.Controller.ViewModel.ProductList = productList;

            this.Text = this.Text + " - " + this.Controller.ViewModel.PalletSN;

            this.Load += new EventHandler(FrmScan_Load);
        }

        public PalletDetailScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {

            this.txtPN.DataBindings.Add("Text", this.Controller.ViewModel, "PN", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtQTY.DataBindings.Add("Text", this.Controller.ViewModel, "SNP", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtSN.DataBindings.Add("Text", this.Controller.ViewModel, "SN", false, DataSourceUpdateMode.OnPropertyChanged);
            this.lblCount.DataBindings.Add("Text", this.Controller.ViewModel, "ListCount", false, DataSourceUpdateMode.OnPropertyChanged);
          
            this.Controller.UIRefresh += new EventHandler<ScanEventArgs>(Controller_UIRefresh);
            this.Controller.UIHandler += new EventHandler(Controller_UIHandler);

            this.Controller.Init();
        }

        void Controller_UIHandler(object sender, EventArgs e)
        {
            this.Close();
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
    }
}