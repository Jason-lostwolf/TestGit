using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPackageScanViewModel:NotifyPropertyChanged
    {
        public FrmPackageScanViewModel()
        {
            this.ProductList = new BindingList<PackageProduct>();
        }

        private string barcode = string.Empty;
        public string Barcode
        {
            get
            {
                return this.barcode;
            }
            set
            {
                this.barcode = value;
                this.OnPropertyChanged("Barcode");
            }
        }

        private string sn = string.Empty;
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

        private BindingList<PackageProduct> productList;
        public BindingList<PackageProduct> ProductList
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
                int count = 0;
                if (this.ProductList != null && this.ProductList.Count > 0)
                {
                    count = this.ProductList.Count;
                }

                return string.Format("已扫 {0} 组记录。", count);
            }
        }
    }
}
