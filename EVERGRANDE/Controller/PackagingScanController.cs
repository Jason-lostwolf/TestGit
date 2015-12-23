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
    public class PackagingScanController : BaseController
    {
        public FrmPackagingScanViewModel ViewModel;
        public PackagingScanController()
        {
            this.ViewModel = new FrmPackagingScanViewModel();
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
            string fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPackagingScanDataFileName);

            string fileData = string.Empty;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileData = helper.LoadFile(fileName);
            }

            List<PackagingProduct> list = new List<PackagingProduct>();
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
                        PackagingProduct record = PackagingProduct.TryPrase(item, out errorMsg);
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
            this.ViewModel.ProductList = new System.ComponentModel.BindingList<PackagingProduct>(list);
        }

        public void SaveBarcode()
        {
            this.ViewModel.SN = this.ViewModel.SN.Trim().ToUpper();

            //前面可能要进行截取操作
            string msg = string.Empty;
            string pn = this.CheckCartonNo();
            if (string.IsNullOrEmpty(pn) == true)
            {
                return;
            }

            string sn = base.ValidateCanBarcode(this.ViewModel.SN, "罐码", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.SecondBarcode);
            }
            else if (this.ViewModel.ProductList.FirstOrDefault(p => p.ProductionBarcode == this.ViewModel.SN) != null)
            {
                Utility.ShowError("罐码重复扫描。");
                this.OnUIRefresh(ScanData.SecondBarcode);
            }
            else
            {

                PackagingProduct p = new PackagingProduct();
                p.SampleBarcode = this.ViewModel.CartonNo;
                p.ProductionBarcode = this.ViewModel.SN;
                p.ScanAccount = StaticInfo.LoginUser.UserName;
                p.ScanTime = DateTime.Now;

                int count = this.ViewModel.ProductList.Count(i => i.SampleBarcode == p.SampleBarcode);
                if (count < 6)
                {

                    this.ViewModel.ProductList.Add(p);

                    //同时将记录写到文件中
                    this.SaveFile(false);
                    if (count == 5)
                    {
                        this.ViewModel.CartonNo = string.Empty;
                        this.ViewModel.SN = string.Empty;

                        //跳
                        this.OnUIRefresh(ScanData.FirstBarcode);
                        return;
                    }
                }
                else
                {
                    Utility.ShowError("扫描出错，已扫6罐。");
                    this.OnUIRefresh(ScanData.FirstBarcode);
                    return;
                }

                //this.ViewModel.CartonNo = string.Empty;
                this.ViewModel.SN = string.Empty;
                this.OnUIRefresh(ScanData.SecondBarcode);
            }
        }
        

        public string CheckCartonNo()
        {
            string msg = string.Empty;
            string result = base.ValidateBarcode(this.ViewModel.CartonNo, "箱号", out msg);
            result = base.ValidateCartonLength(this.ViewModel.CartonNo, "箱号", out msg);

            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.FirstBarcode);
            }
            else
            {
                this.OnUIRefresh(ScanData.SecondBarcode);
            }

            return result;
            //if (string.IsNullOrEmpty(this.ViewModel.Barcode) == true)
            //{
            //    Utility.ShowError("条码不能为空。");
            //    this.OnUIRefresh(ScanData.FirstBarcode);
            //}
            //else
            //{
            //    this.OnUIRefresh(ScanData.SecondBarcode);
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
                fileName = System.IO.Path.Combine(StaticInfo.ExportPath, StaticInfo.PackagingExportFilePrefix + DateTime.Now.ToString(StaticInfo.ExportFileFormat) + StaticInfo.ExportFileExtension);
            }
            else
            {
                if(System.IO.Directory.Exists(StaticInfo.ProgramPath)==false)
                {
                    System.IO.Directory.CreateDirectory(StaticInfo.ProgramPath);
                }
                fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmPackagingScanDataFileName);
            }
            StringBuilder sb = new StringBuilder();
            if (isExport == true)
            {
                sb.AppendLine(StaticInfo.FrmPackagingScanExportTitle);
            }
            this.ViewModel.ProductList.ToList().ForEach(p => sb.AppendLine(p.ToString()));
            helper.SaveFile(sb.ToString(), fileName);
        }
            
            
    }
}
