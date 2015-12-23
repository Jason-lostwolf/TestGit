using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EVERGRANDE.ViewModel
{
    public class FrmPalletDetailScanViewModel:NotifyPropertyChanged
    {
        public FrmPalletDetailScanViewModel()
        {
            this.ProductList = new BindingList<PalletProduct>();
        }

        #region 存放上一层的临时数据

        public string PalletPN { get; set; }
        public int PalletQty { get; set; }
        public string PalletSN { get; set; }

        #endregion

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

        private string snp = string.Empty;
        public string SNP
        {
            get
            {
                return this.snp;
            }
            set
            {
                this.snp = value;
                this.OnPropertyChanged("SNP");
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
            this.OnPropertyChanged("ListCount");
        }

        public string ListCount
        {
            get
            {
                int palletQty = this.PalletQty;
                int scanQty = 0;
                if (this.ProductList != null && this.ProductList.Count > 0 && this.ProductList.FirstOrDefault(p=>p.PalletSN==this.PalletSN)!=null)
                {
                    //palletQty = this.ProductList.First(p => p.PalletSN == this.PalletSN).PalletQty;
                    scanQty = this.ProductList.Where(p => p.PalletSN == this.PalletSN).Sum(a => a.ProductQty);
                }

                return string.Format("计划数: {0}\r\n未扫数: {1} ", palletQty, palletQty-scanQty);
            }
        }
    }
}
