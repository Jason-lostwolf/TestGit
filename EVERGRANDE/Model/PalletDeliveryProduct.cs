using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Common;

namespace EVERGRANDE
{
    public class PalletDeliveryProduct
    {
        //出库单号
        public string OrderNo { get; set; }
        //出库数量
        public int OrderQty { get; set; }
        //箱号
        public string ProductSN { get; set; }

        public DateTime ScanTime { get; set; }
        public string ScanAccount { get; set; }

        public static PalletDeliveryProduct TryPrase(string itemString, out string errorMsg)
        {
            PalletDeliveryProduct product = new PalletDeliveryProduct();
            errorMsg = string.Empty;
            string[] items = itemString.Split(StaticInfo.SplitChat);
            if (items.Length < 5)
            {
                errorMsg = "导入格式有误。";
                return null;
            }
            else
            {

                #region 校验OrderNo

                product.OrderNo = items[0];
                if (string.IsNullOrEmpty(product.OrderNo) == true)
                {
                    errorMsg = "出库单号不能为空。";
                    return null;
                }
                #endregion

                #region 校验OrderQty

                if (string.IsNullOrEmpty(items[1]) == true)
                {
                    errorMsg = "出库数量不能为空。";
                    return null;
                }
                else
                {
                    try
                    {
                        product.OrderQty = Convert.ToInt32(items[1]);
                    }
                    catch(Exception)
                    {
                        errorMsg = "出库数量格式有误。";
                        return null;
                    }
                }
                #endregion

                #region 校验部品SN

                product.ProductSN = items[2];
                if (string.IsNullOrEmpty(product.ProductSN) == true)
                {
                    errorMsg = "箱号不能为空。";
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
                    catch (Exception)
                    {
                        errorMsg = "扫描时间格式出错。";
                        return null;
                    }
                }
                #endregion

                #region 检验ScanAccount
                product.ScanAccount = items[4];
                if (string.IsNullOrEmpty(product.ScanAccount) == true)
                {
                    errorMsg = "作业员不能为空。";
                    return null;
                }
                #endregion

                return product;

            }
        }

        public override string ToString()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", StaticInfo.SplitChat,this.OrderNo,this.OrderQty ,this.ProductSN, this.ScanTime.ToString(StaticInfo.DateTimeFormat), this.ScanAccount);
        }
    }
}
