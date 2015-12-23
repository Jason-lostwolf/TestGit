using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ENPOT.View;

namespace EVERGRANDE.Controller
{
    public class BaseController
    {
        #region 界面更新事件
        public event EventHandler<ScanEventArgs> UIRefresh;
        public void OnUIRefresh(ScanData s)
        {
            if (this.UIRefresh != null)
            {
                this.UIRefresh(this, new ScanEventArgs(s));
            }
        }

        public void OnUIRefresh(ScanData s, Label l)
        {
            if (this.UIRefresh != null)
            {
                this.UIRefresh(this, new ScanEventArgs(s, l));
            }
        }
        #endregion

        #region 暂时的操作只是退出窗口
        public event EventHandler UIHandler;
        public void OnUIHandler()
        {
            if (this.UIHandler != null)
            {
                this.UIHandler(this, null);
            }
        }
        #endregion

        public string ValidatePN(string productPN, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(productPN))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            else if (productPN.ToUpper().StartsWith("P") == false)
            {
                msg = string.Format("当前条码不是{0}。", displayName);
            }
            else
            {
                result = productPN.TrimStart(new char[] { 'p', 'P' });
            }

            return result;
        }

        public string ValidateQTY(string qty, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(qty))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            else if (qty.ToUpper().StartsWith("Q") == false)
            {
                msg = string.Format("当前条码不是{0}。", displayName);
            }
            else
            {
                string trimStartQty = qty.TrimStart(new char[] { 'q', 'Q' });
                result = trimStartQty;
            }

            return result;
        }

        public string ValidateSN(string sn, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(sn))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            //else if (sn.ToUpper().StartsWith("S") == false)
            //{
            //    msg = string.Format("当前条码不是{0}。", displayName);
            //}
            else
            {
                result = sn;
            }

            return result;
        }

        public string ValidatePalletSN(string sn, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(sn))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            else if (sn.ToUpper().StartsWith("M") == false)
            {
                msg = string.Format("当前条码不是{0}。", displayName);
            }
            else
            {
                result = sn;
            }

            return result;
        }

        /// <summary>
        /// 只判断不为空
        /// </summary>
        public string ValidateBarcode(string barcode, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(barcode))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            else
            {
                result = barcode;
            }

            return result;
        }

        // Add By Howe

        public int CartonNoLength
        {
            get { return 12; }
        }

        public int CartonNoLength_V2
        {
            get { return 16; }
        }

        /// <summary>
        /// 箱码长度校验
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="displayName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string ValidateCartonLength(string barcode, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (barcode.Length != this.CartonNoLength && barcode.Length != this.CartonNoLength_V2)
            {
                msg = string.Format("{0}长度不符合要求。", displayName);
            }
            else
            {
                result = barcode;
            }

            return result;
        }

        /// <summary>
        /// 罐码校验
        /// 
        /// 进行16位字段的锁定，扫描其他位数字段和扫描重复均需要报错
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="displayName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string ValidateCanBarcode(string barcode, string displayName, out string msg)
        {
            string result = string.Empty;
            msg = string.Empty;
            if (string.IsNullOrEmpty(barcode))
            {
                msg = string.Format("{0}不能为空。", displayName);
            }
            else if (barcode.Length != 16)
            {
                msg = string.Format("{0}长度不符合要求。", displayName);
            }
            else
            {
                result = barcode;
            }

            return result;
        }
    }
}
