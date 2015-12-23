using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Common;

namespace EVERGRANDE
{
    public class SampleProduct
    {
        public string SampleBarcode { get; set; }
        public string ProductionBarcode { get; set; }
        public int Seq { get; set; }

        public DateTime ScanTime { get; set; }
        public string ScanAccount { get; set; }

        public static SampleProduct TryPrase(string itemString,out string errorMsg)
        {
            SampleProduct product = new SampleProduct();
            errorMsg = string.Empty;
            string[] items = itemString.Split(StaticInfo.SplitChat);
            if (items.Length < 5)
            {
                errorMsg = "导入格式有误。";
                return null;
            }
            else
            {
                #region 校验序号
                string seqString = items[0];
                if (RegexUtil.IsInteger(seqString) == false)
                {
                    errorMsg = "序号必须为整数。";
                    return null;
                }
                else
                {
                    product.Seq = Int32.Parse(items[0]);
                }
                #endregion

                #region 校验SampleBarcode

                product.SampleBarcode = items[1];
                if (string.IsNullOrEmpty(product.SampleBarcode) == true)
                {
                    errorMsg = "样品条码不能为空。";
                    return null;
                }
                #endregion

                #region 校验ProductionBarcode

                product.ProductionBarcode = items[2];
                if (string.IsNullOrEmpty(product.ProductionBarcode) == true)
                {
                    errorMsg = "生产标签条码不能为空。";
                    return null;
                }
                #endregion

                #region 检验ScanTime
                string timeString = items[3];
                if (string.IsNullOrEmpty(timeString) == true)
                {
                    errorMsg = "扫描时间不能为空。";
                    return null;
                }
                else
                {
                    try
                    {
                        product.ScanTime = Convert.ToDateTime(timeString);
                    }
                    catch (Exception ex)
                    {
                        errorMsg = "扫描时间格式出错。";
                        return null;
                    }
                }
                #endregion

                #region 检验ScanAccount
                string scanAccount = items[4];
                if (string.IsNullOrEmpty(scanAccount) == true)
                {
                    errorMsg = "作业员不能为空。";
                    return null;
                }
                else
                {
                    product.ScanAccount = scanAccount;
                }
                #endregion

                return product;

            }
        }

        public override string ToString()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", StaticInfo.SplitChat, this.Seq, this.SampleBarcode, this.ProductionBarcode, this.ScanTime.ToString(StaticInfo.DateTimeFormat), this.ScanAccount);
        }
    }
}
