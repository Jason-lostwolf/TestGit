using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.ViewModel;
using EVERGRANDE.Common;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace EVERGRANDE.Controller
{
    public class PalletDetailScanController : BaseController
    {
        public FrmPalletDetailScanViewModel ViewModel;
        public PalletDetailScanController()
        {
            this.ViewModel = new FrmPalletDetailScanViewModel();
        }        

        public void Init()
        {
            this.OnUIRefresh(ScanData.FirstBarcode);

            //加载记录
            //this.GetProductList();
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


        public void SaveBarcode()
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
            this.ViewModel.SNP = this.ViewModel.SNP.Trim().ToUpper();
            this.ViewModel.SN = this.ViewModel.SN.Trim().ToUpper();

            string pn = this.CheckPN();
            if (string.IsNullOrEmpty(pn) == true)
            {
                return;
            }

            string snpString = this.CheckQTY();
            int productSNP = 0;
            if (string.IsNullOrEmpty(snpString) == false)
            {
                productSNP = Convert.ToInt32(snpString);
            }
            else
            {
                return;
            }

            //进入下一个扫描界面
            string productPN = this.ViewModel.PN;
            string msg = string.Empty;
            string productSNPString = base.ValidateSN(this.ViewModel.SN, "SN", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.ThirdBarcode);
                return;
            }
            string productSN = this.ViewModel.SN;

            //判断不能重复
            PalletProduct pallet = this.ViewModel.ProductList.FirstOrDefault(p => p.ProductSN == productSN);
            if (pallet != null)
            {
                if (pallet.PalletPN.Equals(this.ViewModel.PalletPN) == false)
                {
                    Utility.ShowError(string.Format("看板扫描重复。\r\nPN:{0}",pallet.PalletPN));
                    return;
                }
                else
                {
                    Utility.ShowError("看板扫描重复。");
                    return;
                }
            }

            int palletQty = this.ViewModel.ProductList.Where(a => a.PalletSN == this.ViewModel.PalletSN).Sum(p => p.ProductQty);
            if (palletQty + productSNP > this.ViewModel.PalletQty)
            {
                Utility.ShowError("超出计划数量。");
                this.OnUIRefresh(ScanData.SecondBarcode);
                return;
            }

            PalletProduct product = new PalletProduct();
            product.PalletPN = this.ViewModel.PalletPN;
            product.PalletQty = this.ViewModel.PalletQty;
            product.PalletSN = this.ViewModel.PalletSN;
            product.ProductPN = productPN;
            product.ProductQty = productSNP;
            product.ProductSN = productSN;
            product.ScanAccount = StaticInfo.LoginUser.UserName;
            product.ScanTime = DateTime.Now;
            product.Seq = this.ViewModel.ProductList.Count + 1;
            this.ViewModel.ProductList.Add(product);

            this.SaveFile(false);

            int planQty = 0;
            int scanQty = 0;
            if (this.ViewModel.ProductList != null && this.ViewModel.ProductList.Count > 0 && this.ViewModel.ProductList.FirstOrDefault(p => p.PalletSN == this.ViewModel.PalletSN) != null)
            {
                planQty = this.ViewModel.ProductList.First(p => p.PalletSN == this.ViewModel.PalletSN).PalletQty;
                scanQty = this.ViewModel.ProductList.Where(p => p.PalletSN == this.ViewModel.PalletSN).Sum(a => a.ProductQty);
            }

            if (planQty > 0 && planQty == scanQty)
            {
                Utility.ShowMsg("扫描完成。");
                this.OnUIHandler();
            }

            this.ViewModel.PN = string.Empty;
            this.ViewModel.SNP = string.Empty;
            this.ViewModel.SN = string.Empty;

            this.OnUIRefresh(ScanData.FirstBarcode);

            

            //Utility.ShowMsg("记录内容。");

        }

        #region 校验数据
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
            else if (this.ViewModel.PN.Equals(this.ViewModel.PalletPN) == false)
            {
                Utility.ShowError("PN与托盘标签不一致。");
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
            //else if (this.ViewModel.PN.Equals(this.ViewModel.PalletPN) == false)
            //{
            //    Utility.ShowError("PN与托盘标签不一致。");
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
            string result = base.ValidateQTY(this.ViewModel.SNP, "SNP", out msg);
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

            //if (string.IsNullOrEmpty(this.ViewModel.SNP) == true)
            //{
            //    Utility.ShowError("SNP不能为空。");
            //    this.OnUIRefresh(ScanData..SecondBarcode);
            //}
            //else if(RegexUtil.IsPositiveInteger(this.ViewModel.SNP)==false)
            //{
            //    Utility.ShowError("SNP不是正整数。");
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
            //if (string.IsNullOrEmpty(this.ViewModel.SN) == true)
            //{
            //    Utility.ShowError("SN不能为空。");
            //    this.OnUIRefresh(ScanData..ThirdBarcode);
            //}
            //else if (this.ViewModel.ProductList.FirstOrDefault(p => p.ProductSN.Equals(this.ViewModel.SN)) != null)
            //{
            //    Utility.ShowError("SN重复。");
            //    this.OnUIRefresh(ScanData..ThirdBarcode);
            //}
            //else
            //{
            //    this.IsSNChecked = true;


            Application.DoEvents();
                this.SaveBarcode();
            //}
        }
        #endregion

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
