using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.ViewModel;
using EVERGRANDE.Common;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using ENPOT.View;

namespace EVERGRANDE.Controller
{
    public class PalletDeliveryDetailScanController : BaseController
    {
        public FrmPalletDeliveryDetailScanViewModel ViewModel;
        public PalletDeliveryDetailScanController()
        {
            this.ViewModel = new FrmPalletDeliveryDetailScanViewModel();
        }        

        public void Init()
        {
            this.OnUIRefresh(ScanData.FirstBarcode);

        }

        public void SaveBarcode()
        {

            //Trim标签和ToUpper
            this.ViewModel.CartonNo = this.ViewModel.CartonNo.Trim().ToUpper();


            //进入下一个扫描界面
            string cartonNo = this.ViewModel.CartonNo;
            string msg = string.Empty;
            // 不为空
            string productSNPString = base.ValidateBarcode(cartonNo, "箱号", out msg);
            // Add by Howe 符合箱码指定长度
            base.ValidateCartonLength(cartonNo, "箱号", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.FirstBarcode);
                return;
            }

            //判断不能重复
            PalletDeliveryProduct pallet = this.ViewModel.ProductList.FirstOrDefault(p => p.ProductSN == cartonNo);
            if (pallet != null)
            {
                if (pallet.OrderNo.Equals(this.ViewModel.OrderNo) == false)
                {
                    Utility.ShowError(string.Format("箱号扫描重复。\r\n出库单号:{0}", pallet.OrderNo));
                    return;
                }
                else
                {
                    Utility.ShowError("箱号扫描重复。");
                    return;
                }
            }

            int palletQty = this.ViewModel.ProductList.Where(a => a.OrderNo == this.ViewModel.OrderNo).Count();
            if (palletQty >= this.ViewModel.OrderQty)
            {
                Utility.ShowError("超出计划数量。");
                this.OnUIRefresh(ScanData.FirstBarcode);
                return;
            }

            PalletDeliveryProduct product = new PalletDeliveryProduct();
            product.OrderNo = this.ViewModel.OrderNo;
            product.OrderQty = this.ViewModel.OrderQty;
            product.ProductSN = this.ViewModel.CartonNo;
            product.ScanAccount = StaticInfo.LoginUser.UserName;
            product.ScanTime = DateTime.Now;
            this.ViewModel.ProductList.Add(product);

            this.SaveFile(false);

            int planQty = this.ViewModel.OrderQty;
            int scanQty = 0;
            scanQty = this.ViewModel.ProductList.Where(a => a.OrderNo == this.ViewModel.OrderNo).Count();

            if (planQty > 0 && planQty == scanQty)
            {
                Utility.ShowMsg("扫描完成。");
                this.OnUIHandler();
            }
            else
            {
                this.ViewModel.CartonNo = string.Empty;
                this.OnUIRefresh(ScanData.FirstBarcode);
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
                fileName = System.IO.Path.Combine(StaticInfo.ExportPath, StaticInfo.PalletDeliveryExportFilePrefix + DateTime.Now.ToString(StaticInfo.ExportFileFormat) + StaticInfo.ExportFileExtension);
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
