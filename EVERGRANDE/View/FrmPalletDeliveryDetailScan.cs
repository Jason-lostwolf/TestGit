using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVERGRANDE.Controller;
using Symbol.Barcode;
using Symbol;
using ENPOT.View;

namespace EVERGRANDE
{
    public partial class FrmPalletDeliveryDetailScan : BaseForm //FrmScanBase // FrmScanBase // Form
    {
        public FrmPalletDeliveryDetailScan(string orderNo, int qty, BindingList<PalletDeliveryProduct> productList)
        {
            InitializeComponent();

            //数据绑定
            this.Controller = new PalletDeliveryDetailScanController();

            this.Controller.ViewModel.OrderNo = orderNo;
            this.Controller.ViewModel.OrderQty = qty;
            this.Controller.ViewModel.ProductList = productList;

            this.Load += new EventHandler(FrmScan_Load);
        }

        public PalletDeliveryDetailScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {

            this.lblOrderNo.DataBindings.Add("Text", this.Controller.ViewModel, "OrderNo", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtSN.DataBindings.Add("Text", this.Controller.ViewModel, "CartonNo", false, DataSourceUpdateMode.OnPropertyChanged);
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
                this.txtSN.Focus();
                this.txtSN.SelectAll();
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
            //DialogResult dr = MessageBox.Show("确认退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //if (dr == DialogResult.Yes)
            //{
            //    this.Close();
            //}
        }

        #endregion

        #region 扫描触发事件

        protected override void OnRead(ReaderData readerData)
        {
            if (readerData.Result == Results.SUCCESS)
            {
                if (this.txtSN.Focused)
                {
                    this.txtSN.Text = readerData.Text;
                    this.Controller.SaveBarcode();
                }
            }
        }
        #endregion
    }
}