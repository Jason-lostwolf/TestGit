using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.ViewModel;
using EVERGRANDE.Common;
using System.Windows.Forms;
using System.Drawing;

namespace EVERGRANDE.Controller
{
    public class PalletScanController : BaseController
    {
        public FrmPalletScanViewModel ViewModel;
        public PalletScanController()
        {
            this.ViewModel = new FrmPalletScanViewModel();
        }

        public void Init()
        {
            this.OnUIRefresh(ScanData.FirstBarcode);

            //加载记录
            this.GetProductList();
        }

        private void GetProductList()
        {
            FileHelper helper = new FileHelper();
            string fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPalletScanDataFileName);

            string fileData = string.Empty;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileData = helper.LoadFile(fileName);
            }

            List<PalletProduct> list = new List<PalletProduct>();
            if (fileData != null && string.IsNullOrEmpty(fileData.Trim()) == false)
            {
                fileData = fileData.Trim().Replace("\r\n", "\n").Trim();
                var tempList = fileData.Trim().Split(new char[] { '\n' });

                if (tempList != null)
                {
                    foreach (string item in tempList)
                    {
                        //赋值
                        string errorMsg = string.Empty;
                        PalletProduct record = PalletProduct.TryPrase(item, out errorMsg);
                        if (string.IsNullOrEmpty(errorMsg) == false)
                        {
                            throw new Exception(errorMsg);
                        }
                        else
                        {
                            list.Add(record);
                        }
                    }
                }
            }
            this.ViewModel.ProductList = new System.ComponentModel.BindingList<PalletProduct>(list);
        }

        public void ScanDetail()
        {
            //if (this.IsPNChecked == false)
            //{
            //    this.CheckPN();
            //}

            //if (this.IsPNChecked == false)
            //{
            //    return;
            //}

            //if (this.IsQTYChecked == false)
            //{
            //    this.CheckQTY();
            //}

            //if (this.IsQTYChecked == false)
            //{
            //    return;
            //}

            //全部重新来过
            //Trim标签和ToUpper
            this.ViewModel.PN = this.ViewModel.PN.Trim().ToUpper();
            this.ViewModel.Qty = this.ViewModel.Qty.Trim().ToUpper();
            this.ViewModel.SN = this.ViewModel.SN.Trim().ToUpper();

            string pn = this.CheckPN();
            if (string.IsNullOrEmpty(pn) == true)
            {
                return;
            }

            string qtyString = this.CheckQTY();
            int qty  = 0;
            if (string.IsNullOrEmpty(qtyString) == false)
            {
                qty = Convert.ToInt32(qtyString);
            }
            else
            {
                return;
            }

            string msg = string.Empty;
            string sn = base.ValidatePalletSN(this.ViewModel.SN, "SN", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.ThirdBarcode);
                return;
            }

            //进入下一个扫描界面

            //string pn = this.ViewModel.PN;
            //int qty = this.CheckQTY();
            //string sn = this.ViewModel.SN;

            //校验数据
            PalletProduct pallet = this.ViewModel.ProductList.FirstOrDefault(p => p.PalletSN == this.ViewModel.SN && p.PalletQty != qty);
            if (pallet != null)
            {
                Utility.ShowError(string.Format("QTY有误。\r\n该SN已扫描。\r\nPN:{0}\r\nQTY:{1} ", pallet.PalletPN, pallet.PalletQty));
                
            }
            else if (qty == this.ViewModel.ProductList.Where(a => a.PalletSN == sn).Sum(p => p.ProductQty))
            {
                Utility.ShowMsg("看板已扫描完毕。");
            }
            else
            {
                FrmPalletDetailScan frm = new FrmPalletDetailScan(this.ViewModel.PN, qty, this.ViewModel.SN, this.ViewModel.ProductList);
                frm.ShowDialog();
            }

            //不知道要不要刷新数据
            this.ViewModel.PN = string.Empty;
            this.ViewModel.Qty = string.Empty;
            this.ViewModel.SN = string.Empty;
            this.OnUIRefresh(ScanData.FirstBarcode);
        }

        public bool IsPNChecked { get; set; }
        public string CheckPN()
        {
            string msg = string.Empty;
            string result = base.ValidatePN(this.ViewModel.PN, "PN", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.FirstBarcode);
            }
            else
            {
                this.IsPNChecked = true;
                this.OnUIRefresh(ScanData.SecondBarcode);
            }


            Application.DoEvents();
            return result;

            //if (string.IsNullOrEmpty(this.ViewModel.PN) == true)
            //{
            //    Utility.ShowError("PN不能为空。");
            //    this.OnUIRefresh(ScanData..FirstBarcode);
            //}
            //else
            //{
            //    this.IsPNChecked = true;
            //    this.OnUIRefresh(ScanData..SecondBarcode);
            //}
        }

        public bool IsQTYChecked { get; set; }
        public string CheckQTY()
        {
            string msg = string.Empty;
            string result = base.ValidateQTY(this.ViewModel.Qty, "QTY", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.SecondBarcode);
            }
            else
            {
                this.IsQTYChecked = true;
                this.OnUIRefresh(ScanData.ThirdBarcode);
            }


            Application.DoEvents();
            return result;
            //if (string.IsNullOrEmpty(this.ViewModel.Qty) == true)
            //{
            //    Utility.ShowError("QTY不能为空。");
            //    this.OnUIRefresh(ScanData..SecondBarcode);
            //}
            //else if(RegexUtil.IsPositiveInteger(this.ViewModel.Qty)==false)
            //{
            //    Utility.ShowError("QTY不是正整数。");
            //    this.OnUIRefresh(ScanData..SecondBarcode);
            //}
            //else
            //{
            //    this.IsQTYChecked = true;
            //    this.OnUIRefresh(ScanData..ThirdBarcode);
            //}
        }

        public bool IsSNChecked { get; set; }
        public void CheckSN()
        {
            //string msg = string.Empty;
            //string result = base.ValidatePalletSN(this.ViewModel.SN, "SN", out msg);
            //if (string.IsNullOrEmpty(msg) == false)
            //{
            //    Utility.ShowError(msg);
            //    this.OnUIRefresh(ScanData..ThirdBarcode);
            //}
            //else
            //{
            //    this.IsSNChecked = true;

            Application.DoEvents();
                this.ScanDetail();
            //}


            //if (string.IsNullOrEmpty(this.ViewModel.SN) == true)
            //{
            //    Utility.ShowError("SN不能为空。");
            //    this.OnUIRefresh(ScanData..ThirdBarcode);
            //}
            //else
            //{
            //    this.IsSNChecked = true;
            //    this.ScanDetail();
            //}
        }

        public void Export()
        {
            try
            {
                if (Utility.ShowQuestion("确认导出？") == DialogResult.Yes)
                {
                    //导出内容
                    this.SaveFile(true);
                    this.ViewModel.ProductList.Clear();
                    this.SaveFile(false);                    

                    Utility.ShowMsg("导出成功。 ");
                }
                //更新UI
                this.OnUIRefresh(ScanData.FirstBarcode);
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex.Message);
            }
        }

        private void SaveFile(bool isExport)
        {
            FileHelper helper = new FileHelper();
            string fileName = string.Empty;
            if (isExport == true)
            {
                if (System.IO.Directory.Exists(StaticInfo.ExportPath) == false)
                {
                    System.IO.Directory.CreateDirectory(StaticInfo.ExportPath);
                }
                fileName = System.IO.Path.Combine(StaticInfo.ExportPath, StaticInfo.PalletExportFilePrefix +  DateTime.Now.ToString(StaticInfo.ExportFileFormat) + StaticInfo.ExportFileExtension);
            }
            else
            {
                if(System.IO.Directory.Exists(StaticInfo.ProgramPath)==false)
                {
                    System.IO.Directory.CreateDirectory(StaticInfo.ProgramPath);
                }
                fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPalletScanDataFileName);
            }
            StringBuilder sb = new StringBuilder();
            if (isExport == true)
            {
                sb.AppendLine(StaticInfo.FrmPalletScanExportTitle);
            }
            foreach (PalletProduct product in this.ViewModel.ProductList)
            {
                sb.AppendLine(product.ToString());
            }
            helper.SaveFile(sb.ToString(), fileName);
        }
            
            
    }
}
