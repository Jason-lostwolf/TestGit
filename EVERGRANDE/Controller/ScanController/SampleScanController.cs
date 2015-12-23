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
    public class SampleScanController : BaseController
    {
        public FrmSampleScanViewModel ViewModel;
        public SampleScanController()
        {
            this.ViewModel = new FrmSampleScanViewModel();
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
            string fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmSampleScanDataFileName);

            string fileData = string.Empty;
            if (System.IO.File.Exists(fileName) == true)
            {
                fileData = helper.LoadFile(fileName);
            }

            List<SampleProduct> list = new List<SampleProduct>();
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
                        SampleProduct record = SampleProduct.TryPrase(item, out errorMsg);
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
            this.ViewModel.ProductList = new System.ComponentModel.BindingList<SampleProduct>(list);
        }

        public void SaveBarcode()
        {
            //if (string.IsNullOrEmpty(this.ViewModel.Barcode) == true)
            //{
            //    Utility.ShowError("样板条码不能为空。");
            //    this.OnUIRefresh(ScanData.FirstBarcode);
            //}
            //else if (string.IsNullOrEmpty(this.ViewModel.SN) == true)
            //{
            //    Utility.ShowError("生产标签条码不能为空。");
            //    this.OnUIRefresh(ScanData.SecondBarcode);
            //}
            //else if (this.ViewModel.ProductList.FirstOrDefault(p => p.ProductionBarcode == this.ViewModel.SN) != null)
            //{
            //    Utility.ShowError("生产标签条码重复扫描。");
            //    this.OnUIRefresh(ScanData.SecondBarcode);
            //}
            //else
            //{
            //Trim标签和ToUpper

            
            this.ViewModel.SN = this.ViewModel.SN.Trim().ToUpper();

            //前面可能要进行截取操作
            string msg = string.Empty;
            string pn =  this.CheckBarcode();
            if (string.IsNullOrEmpty(pn) == true)
            {
                return;
            }

            string sn = base.ValidateSN(this.ViewModel.SN, "生产标签条码", out msg);
            if (string.IsNullOrEmpty(msg) == false)
            {
                Utility.ShowError(msg);
                this.OnUIRefresh(ScanData.SecondBarcode);
            }
            //else if (this.ViewModel.ProductList.FirstOrDefault(p => p.ProductionBarcode == this.ViewModel.SN) != null)
            //{
            //    Utility.ShowError("生产标签条码重复扫描。");
            //    this.OnUIRefresh(ScanData.SecondBarcode);
            //}
            else
            {

                //只要包含
                if (sn.Contains(pn) == true)
                //if (this.ViewModel.Barcode.Equals(this.ViewModel.SN))
                {

                    SampleProduct p = new SampleProduct();
                    p.SampleBarcode = this.ViewModel.Barcode;
                    p.ProductionBarcode = this.ViewModel.SN;
                    p.ScanAccount = StaticInfo.LoginUser.UserName;
                    p.ScanTime = DateTime.Now;
                    p.Seq = this.ViewModel.ProductList.Count + 1;
                    this.ViewModel.ProductList.Add(p);

                    //同时将记录写到文件中
                    this.SaveFile(false);

                    this.ViewModel.Barcode = string.Empty;
                    this.ViewModel.SN = string.Empty;

                    Label l = new Label();
                    l.Text = "一致";
                    l.ForeColor = Color.White;
                    l.BackColor = Color.YellowGreen;

                    this.OnUIRefresh(ScanData.FirstBarcode, l);
                }
                else
                {
                    Label l = new Label();
                    l.Text = "不一致";
                    l.ForeColor = Color.White;
                    l.BackColor = Color.Red;
                    this.OnUIRefresh(ScanData.SecondBarcode, l);
                }
            }

        }
        

        public string CheckBarcode()
        {
            string msg = string.Empty;
            string result = base.ValidatePN(this.ViewModel.Barcode, "样板条码", out msg);
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
                fileName = System.IO.Path.Combine(StaticInfo.ExportPath,StaticInfo.SampleExportFilePrefix +  DateTime.Now.ToString(StaticInfo.ExportFileFormat) + StaticInfo.ExportFileExtension);
            }
            else
            {
                if(System.IO.Directory.Exists(StaticInfo.ProgramPath)==false)
                {
                    System.IO.Directory.CreateDirectory(StaticInfo.ProgramPath);
                }
                fileName = System.IO.Path.Combine(StaticInfo.ProgramPath, StaticInfo.FrmSampleScanDataFileName);
            }
            StringBuilder sb = new StringBuilder();
            if (isExport == true)
            {
                sb.AppendLine(StaticInfo.FrmSampleScanExportTitle);
            }
            foreach (SampleProduct product in this.ViewModel.ProductList)
            {
                sb.AppendLine(product.ToString());
            }
            helper.SaveFile(sb.ToString(), fileName);
        }
            
            
    }
}
