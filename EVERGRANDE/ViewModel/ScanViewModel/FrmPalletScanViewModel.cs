using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPalletScanViewModel:NotifyPropertyChanged
    {
        public FrmPalletScanViewModel()
        {
            this.ProductList = new BindingList<PalletProduct>();
        }

        private string pn = string.Empty;
        public string PN
        {
            get
            {
                return this.pn;
            }
            set
            {
                this.pn = value;
                this.OnPropertyChanged("PN");
            }
        }

        private string qty = string.Empty;
        public string Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
                this.OnPropertyChanged("Qty");
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

        private BindingList<PalletProduct> productList;
        public BindingList<PalletProduct> ProductList
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
            this.OnPropertyChanged("IsScanEnabled");
            this.OnPropertyChanged("IsExportEnabled");
            this.OnPropertyChanged("ListCount");
        }

        public bool IsScanEnabled
        {
            get
            {
                return string.IsNullOrEmpty(this.PN) == false && string.IsNullOrEmpty(this.Qty) == false && string.IsNullOrEmpty(this.SN) == false;
            }
        }

        public bool IsExportEnabled
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
                    count = this.ProductList.Select(a => a.PalletSN).Distinct().Count();
                }

                return string.Format("已扫描托盘数 {0} ", count);
            }
        }
    }
}
