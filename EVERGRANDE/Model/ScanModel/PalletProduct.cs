using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Common;

namespace EVERGRANDE
{
    public class PalletProduct
    {
        //序号，其实用不用也没有所谓
        public int Seq { get; set; }
        //托盘标签PN
        public string PalletPN { get; set; }
        //托盘标签SN
        public string PalletSN { get; set; }
        //托盘标签QTY
        public int PalletQty { get; set; }

        //部品PN
        public string ProductPN { get; set; }
        //部品SN
        public string ProductSN { get; set; }
        //部品SNP
        public int ProductQty { get; set; }

        public DateTime ScanTime { get; set; }
        public string ScanAccount { get; set; }

        public static PalletProduct TryPrase(string itemString, out string errorMsg)
        {
            PalletProduct product = new PalletProduct();
            errorMsg = string.Empty;
            string[] items = itemString.Split(StaticInfo.SplitChat);
            if (items.Length < 9)
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

                #region 校验PalletPN

                product.PalletPN = items[1];
                if (string.IsNullOrEmpty(product.PalletPN) == true)
                {
                    errorMsg = "托盘标签PN不能为空。";
                    return null;
                }
                #endregion

                #region 校验PalletQty

                if (string.IsNullOrEmpty(items[2]) == true)
                {
                    errorMsg = "托盘标签QTY不能为空。";
                    return null;
                }
                else
                {
                    try
                    {
                        product.PalletQty = Convert.ToInt32(items[2]);
                    }
                    catch(Exception)
                    {
                        errorMsg = "托盘标签QTY格式有误。";
                        return null;
                    }
                }
                #endregion

                #region 校验托盘标签SN

                product.PalletSN = items[3];
                if (string.IsNullOrEmpty(product.PalletSN) == true)
                {
                    errorMsg = "托盘标签SN不能为空。";
                    return null;
                }
                #endregion

                #region 校验ProductPN

                product.ProductPN = items[4];
                if (string.IsNullOrEmpty(product.ProductPN) == true)
                {
                    errorMsg = "部品PN不能为空。";
                    return null;
                }
                #endregion

                #region 校验部品SNP

                if (string.IsNullOrEmpty(items[5]) == true)
                {
                    errorMsg = "部品SNP不能为空。";
                    return null;
                }
                else
                {
                    try
                    {
                        product.ProductQty = Convert.ToInt32(items[5]);
                    }
                    catch (Exception)
                    {
                        errorMsg = "部品SNP格式有误。";
                        return null;
                    }
                }
                #endregion

                #region 校验部品SN

                product.ProductSN = items[6];
                if (string.IsNullOrEmpty(product.ProductSN) == true)
                {
                    errorMsg = "部品SN不能为空。";
                    return null;
                }
                #endregion

                #region 检验ScanTime
                string timeString = items[7];
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
                product.ScanAccount = items[8];
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
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}", StaticInfo.SplitChat, this.Seq, this.PalletPN,this.PalletQty ,this.PalletSN, this.ProductPN, this.ProductQty, this.ProductSN, this.ScanTime.ToString(StaticInfo.DateTimeFormat), this.ScanAccount);
        }
    }
}
