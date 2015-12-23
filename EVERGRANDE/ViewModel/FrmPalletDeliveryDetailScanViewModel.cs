using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPalletDeliveryDetailScanViewModel:NotifyPropertyChanged
    {
        public FrmPalletDeliveryDetailScanViewModel()
        {
            this.OrderNo = string.Empty;
            this.ProductList = new BindingList<PalletDeliveryProduct>();
        }

        #region 存放上一层的临时数据

        public string OrderNo { get; set; }
        public int OrderQty { get; set; }

        #endregion
        
        private string cartonNo = string.Empty;
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
            this.OnPropertyChanged("ListCount");
        }

        public string ListCount
        {
            get
            {
                int palletQty = this.OrderQty;
                int scanQty = 0;
                if (this.ProductList != null && this.ProductList.Count > 0 && this.ProductList.FirstOrDefault(p => p.OrderNo == this.OrderNo) != null)
                {
                    scanQty = this.ProductList.Where(p => p.OrderNo == this.OrderNo).Count();
                }

                return string.Format("出库计划：{0}\r\n已扫箱数：{1}\r\n未扫箱数：{2} ", palletQty, scanQty, palletQty - scanQty);
            }
        }
    }
}
