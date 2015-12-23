using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPackagingScanViewModel:NotifyPropertyChanged
    {
        public FrmPackagingScanViewModel()
        {
            this.CartonNo = string.Empty;
            this.SN = string.Empty;

            this.ProductList = new BindingList<PackagingProduct>();
        }

        private string cartonNo;
        public string CartonNo
        {
            get
            {
                return this.cartonNo;
            }
            set
            {
                this.cartonNo = value;
                this.OnPropertyChanged("CartonNo");
            }
        }

        private string sn;
        public string SN
        {
            get
            {
                return this.sn;
            }
            set
            {
                this.sn = value;
                this.OnPropertyChanged("SN");
            }
        }

        private BindingList<PackagingProduct> productList;
        public BindingList<PackagingProduct> ProductList
        {
            get
            {
                return this.productList;
            }
            set
            {
                if (this.productList != value)
                {
                    this.productList = value;
                    this.productList.ListChanged += new ListChangedEventHandler(productList_ListChanged);
                }
                this.NotifyProperties();
            }
        }

        void productList_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.NotifyProperties();
        }

        private void NotifyProperties()
        {
            this.OnPropertyChanged("IsSaveEnable");
            this.OnPropertyChanged("ListCount");
        }

        public bool IsSaveEnable
        {
            get
            {
                return this.ProductList != null && this.ProductList.Count > 0;
            }
        }

        public string ListCount
        {
            get
            {
                int totalCartonCount = 0;
                int totalSNCount = 0;
                int currentSNCount = 0;
                
                if (this.ProductList != null && this.ProductList.Count > 0)
                {
                    totalSNCount = this.ProductList.Count;
                    totalCartonCount = this.ProductList.Select(p => p.SampleBarcode).Distinct().Count();
                    currentSNCount = this.ProductList.Where(p => p.SampleBarcode == this.CartonNo).Count();
                }

                return string.Format("已扫罐数：{0}\r\n已扫总箱数：{1}\r\n已扫总罐数：{2}", currentSNCount, totalCartonCount, totalSNCount);
            }
        }
    }
}
