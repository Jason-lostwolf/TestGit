using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ENPOT.View;

namespace EVERGRANDE.View
{
    public partial class FrmMain : BaseForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnPackagingScan_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPackagingScan frm = new FrmPackagingScan();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPackageScan_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FrmPackageScan frm = new FrmPackageScan();
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    Utility.ShowError(ex.Message);
            //}

        }

        private void btnDeliveryScan_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPalletDeliveryScan frm = new FrmPalletDeliveryScan();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex.Message);
            }
        }
    }
}