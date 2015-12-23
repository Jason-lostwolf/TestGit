using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVERGRANDE.Controller;
using ENPOT.View;
using Symbol.Barcode;
using Symbol;

namespace EVERGRANDE
{
    public partial class FrmPalletDeliveryScan : BaseForm // Form // FrmScanBase
    {
        public FrmPalletDeliveryScan()
        {
            InitializeComponent();

            this.Load += new EventHandler(FrmScan_Load);
            this.txtOrderNo.GotFocus += new EventHandler(txtOrderNo_GotFocus);
            this.txtOrderNo.KeyDown += new KeyEventHandler(txtOrderNo_KeyDown);
            this.txtQTY.KeyDown += new KeyEventHandler(txtQTY_KeyDown);
        }

        private PalletDeliveryScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {
            //数据绑定
            this.Controller = new PalletDeliveryScanController();

            this.txtOrderNo.DataBindings.Add("Text", this.Controller.ViewModel, "OrderNo", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtQTY.DataBindings.Add("Text", this.Controller.ViewModel, "Qty", false, DataSourceUpdateMode.OnPropertyChanged);
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
                this.txtOrderNo.Focus();
                this.txtOrderNo.SelectAll();
            }
            else
            {
                this.txtQTY.Focus();
                this.txtQTY.SelectAll();
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

        private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.CheckOrderNo();
            }
        }

        private void txtOrderNo_GotFocus(object sender, EventArgs e)
        {
            this.Controller.IsOrderNoChecked = false;
        }

        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Controller.ScanDetail();
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
        protected override void OnRead(ReaderData readerData)
        {
            if (readerData.Result == Results.SUCCESS)
            {
                if (this.txtOrderNo.Focused)
                {
                    this.txtOrderNo.Text = readerData.Text;
                    this.Controller.CheckOrderNo();
                }
                else if (this.txtQTY.Focused)
                {
                    this.txtQTY.Text = readerData.Text;
                    this.Controller.ScanDetail();
                }
                else
                {

                }
            }
        }
        #endregion


        private void btnScan_Click(object sender, EventArgs e)
        {
            this.Controller.ScanDetail();
        }
    }
}