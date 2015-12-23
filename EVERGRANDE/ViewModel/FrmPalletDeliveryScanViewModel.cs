using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPalletDeliveryScanViewModel:NotifyPropertyChanged
    {
        public FrmPalletDeliveryScanViewModel()
        {
            this.ProductList = new BindingList<PalletDeliveryProduct>();
        }

        private string orderNo = string.Empty;
        public string OrderNo
        {
            get
            {
                return this.orderNo;
            }
            set
            {
                this.orderNo = value;
                this.OnPropertyChanged("OrderNo");
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

        private BindingList<PalletDeliveryProduct> productList;
        public BindingList<PalletDeliveryProduct> ProductList
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
                return string.IsNullOrEmpty(this.OrderNo) == false && string.IsNullOrEmpty(this.Qty) == false;
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
                    count = this.ProductList.Select(a => a.OrderNo).Distinct().Count();
                }

                return string.Format("已扫描出库单：{0} ", count);
            }
        }
    }
}
