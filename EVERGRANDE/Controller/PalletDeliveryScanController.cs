using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.ViewModel;
using EVERGRANDE.Common;
using System.Windows.Forms;
using System.Drawing;
using ENPOT.View;

namespace EVERGRANDE.Controller
{
    public class PalletDeliveryScanController : BaseController
    {
        public FrmPalletDeliveryScanViewModel ViewModel;
        public PalletDeliveryScanController()
        {
            this.ViewModel = new FrmPalletDeliveryScanViewModel();
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
            string fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPalletDeliveryScanDataFileName);

            string fileData = string.Empty;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileData = helper.LoadFile(fileName);
            }

            List<PalletDeliveryProduct> list = new List<PalletDeliveryProduct>();
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
                        PalletDeliveryProduct record = PalletDeliveryProduct.TryPrase(item, out errorMsg);
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
            this.ViewModel.ProductList = new System.ComponentModel.BindingList<PalletDeliveryProduct>(list);
        }

        public void ScanDetail()
        {
            //全部重新来过
            //Trim标签和ToUpper
            this.ViewModel.OrderNo = this.ViewModel.OrderNo.Trim().ToUpper();
            this.ViewModel.Qty = this.ViewModel.Qty.Trim().ToUpper();

            string pn = this.CheckOrderNo();
            if (string.IsNullOrEmpty(pn) == true)
            {
                return;
            }

            //string qtyString = this.CheckQTY();
            string msg = string.Empty;
            string result = base.ValidateBarcode(this.ViewModel.Qty, "出库数量", out msg);
            int qty = 0;
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.SecondBarcode);
                return;
            }
            else
            {
                try
                {
                    qty = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    Utility.ShowError("数量格式出错。");
                    this.OnUIRefresh(ScanData.SecondBarcode);
                    return;
                }
            }

            //进入下一个扫描界面

            //校验数据
            PalletDeliveryProduct pallet = this.ViewModel.ProductList.FirstOrDefault(p => p.OrderNo == this.ViewModel.OrderNo);
            if (pallet != null && pallet.OrderQty>qty)
            {
                Utility.ShowError(string.Format("已扫箱数{0}\r\n出库数量过小。", pallet.OrderQty));
                
            }
            else if (pallet != null && pallet.OrderQty == qty)
            {
                Utility.ShowMsg("出库单号已扫描完毕。");
            }
            else
            {
                //FrmPalletDetailScan frm = new FrmPalletDetailScan(this.ViewModel.PN, qty, this.ViewModel.SN, this.ViewModel.ProductList);
                //frm.ShowDialog();
                //Utility.ShowMsg("未实现。");
                FrmPalletDeliveryDetailScan frm = new FrmPalletDeliveryDetailScan(this.ViewModel.OrderNo, qty, this.ViewModel.ProductList);
                frm.ShowDialog();
            }

            //不知道要不要刷新数据
            this.ViewModel.OrderNo = string.Empty;
            this.ViewModel.Qty = string.Empty;
            this.OnUIRefresh(ScanData.FirstBarcode);
        }

        public bool IsOrderNoChecked { get; set; }
        public string CheckOrderNo()
        {
            string msg = string.Empty;
            string result = base.ValidateBarcode(this.ViewModel.OrderNo, "出库单号", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.FirstBarcode);
            }
            else
            {
                this.IsOrderNoChecked = true;
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

        //public bool IsQTYChecked { get; set; }
        //public string CheckQTY()
        //{
        //    string msg = string.Empty;
        //    string result = base.ValidateBarcode(this.ViewModel.Qty, "出库数量", out msg);
        //    if (string.IsNullOrEmpty(msg) == false)
        //    {
        //        Utility.ShowError(msg);
        //        this.OnUIRefresh(ScanData..SecondBarcode);
        //    }
        //    else
        //    {
        //        this.IsQTYChecked = true;
        //        this.ScanDetail();
        //    }


        //    Application.DoEvents();
        //    return result;
        //    //if (string.IsNullOrEmpty(this.ViewModel.Qty) == true)
        //    //{
        //    //    Utility.ShowError("QTY不能为空。");
        //    //    this.OnUIRefresh(ScanData..SecondBarcode);
        //    //}
        //    //else if(RegexUtil.IsPositiveInteger(this.ViewModel.Qty)==false)
        //    //{
        //    //    Utility.ShowError("QTY不是正整数。");
        //    //    this.OnUIRefresh(ScanData..SecondBarcode);
        //    //}
        //    //else
        //    //{
        //    //    this.IsQTYChecked = true;
        //    //    this.OnUIRefresh(ScanData..ThirdBarcode);
        //    //}
        //}

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
                fileName = System.IO.Path.Combine(StaticInfo.ExportPath, StaticInfo.PalletDeliveryExportFilePrefix +  DateTime.Now.ToString(StaticInfo.ExportFileFormat) + StaticInfo.ExportFileExtension);
            }
            else
            {
                if(System.IO.Directory.Exists(StaticInfo.ProgramPath)==false)
                {
                    System.IO.Directory.CreateDirectory(StaticInfo.ProgramPath);
                }
                fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPalletDeliveryScanDataFileName);
            }
            StringBuilder sb = new StringBuilder();
            if (isExport == true)
            {
                sb.AppendLine(StaticInfo.FrmPalletDeliveryScanExportTitle);
            }

            this.ViewModel.ProductList.ToList().ForEach(p => sb.AppendLine(p.ToString()));
            helper.SaveFile(sb.ToString(), fileName);
        }
            
            
    }
}
