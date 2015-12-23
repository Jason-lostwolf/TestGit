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
    public partial class FrmPackagingScan : BaseForm // FrmScanBase //Form //
    {
        public FrmPackagingScan()
        {
            InitializeComponent();

            this.Load += new EventHandler(FrmScan_Load);
            this.txtCartonNo.KeyDown += new KeyEventHandler(txtBarcode_KeyDown);
            this.txtSN.KeyDown += new KeyEventHandler(txtSN_KeyDown);
        }

        private PackagingScanController Controller = null;
        void FrmScan_Load(object sender, EventArgs e)
        {
            //数据绑定
            this.Controller = new PackagingScanController();

            this.txtCartonNo.DataBindings.Add("Text", this.Controller.ViewModel, "CartonNo", false, DataSourceUpdateMode.OnPropertyChanged);
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
                this.txtCartonNo.Focus();
                this.txtCartonNo.SelectAll();
            }
            else
            {
                this.txtSN.Focus();
                this.txtSN.SelectAll();
            }

            //if (e.RefreshLabel != null)
            //{
            //    this.lblError.Text = e.RefreshLabel.Text;
            //    this.lblError.BackColor = e.RefreshLabel.BackColor;
            //    this.lblBackground.BackColor = e.RefreshLabel.ForeColor;
            //    this.lblError.ForeColor = e.RefreshLabel.ForeColor;
            //}
            //else
            //{
            //    this.lblError.Text = string.Empty;
            //    this.lblError.BackColor = Color.Transparent;
            //    this.lblError.ForeColor = Color.Transparent;
            //    this.lblBackground.BackColor = Color.Transparent;
            //}
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
                this.Controller.CheckCartonNo();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Controller.Export();
            this.txtCartonNo.Text = string.Empty;
            this.txtSN.Text = string.Empty;            
        }
        #endregion

        #region 扫描触发事件

        protected override void OnRead(ReaderData readerData)
        {
            if (readerData.Result == Results.SUCCESS)
            {
                if (this.txtCartonNo.Focused)
                {
                    this.txtCartonNo.Text = readerData.Text;
                    this.Controller.CheckCartonNo();
                }
                else if (this.txtSN.Focused)
                {
                    string original = readerData.Text;
                    int index = original.LastIndexOf('?') + 1;
                    string lastest = original.Substring(index, original.Length - index);
                    this.txtSN.Text = lastest;
                    this.Controller.SaveBarcode();
                    this.txtSN.Text = lastest;
                    //this.txtSN.SelectAll();
                }
                //else
                //{
                //    MessageBox.Show("请确认扫描焦点", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                //}
            }
        }

        #endregion
    }
}