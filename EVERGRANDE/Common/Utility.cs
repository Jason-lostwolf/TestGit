using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace EVERGRANDE
{
    public static class Utility
    {
        /// <summary>
        /// 信息提示框
        /// </summary>
        public static void ShowMsg(string msg)
        {
            Cursor.Current = Cursors.Default;
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 错误提示框
        /// </summary>
        public static void ShowError(string msg)
        {
            Cursor.Current = Cursors.Default;

            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 问题提示框
        /// </summary>
        public static DialogResult ShowQuestion(string msg)
        {
            return MessageBox.Show(msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static bool IsAuthorization()
        {
            return IsAuthorization(@"\Application\Help.txt");
        }

        public static bool IsAuthorization(string fileFullName)
        {
            bool isAbout = false;
            bool isSystemVersions = false;

            if (System.IO.File.Exists(fileFullName))
            {
                System.IO.StreamReader sr = null;
                int row = 1;
                string line = "";

                try
                {
                    sr = new System.IO.StreamReader(fileFullName);

                    do
                    {
                        line = sr.ReadLine();

                        if (row == 1 || line == "[About...]")
                        {
                            isAbout = true;
                        }
                        else if (row == 4 || line == "[System Versions...]")
                        {
                            isSystemVersions = true;
                        }

                        row += 1;
                    } while (row <= 4);
                }
                catch
                {
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }

            return (isAbout && isSystemVersions);

        }

        public static bool IsBarcodePass(string barcodeA,string barcodeB)
        {
            //if (Common.StaticInfo.IsPMiss == false)
            //{
            //    return barcodeA == barcodeB;
            //}
            //else
            {
                return barcodeB == barcodeA || barcodeA.ToUpper() == "P" + barcodeB || "P" + barcodeA.ToUpper() == barcodeB;
            }
        }

        public static bool IsOrderBarcodePass(string barcodeA, string barcodeB)
        {
            //if (Common.StaticInfo.IsPMiss == false)
            //{
                return barcodeA == barcodeB;
            //}
            //else
            //{
            //    return barcodeB == barcodeA || barcodeA.ToUpper() == "P" + barcodeB || "P" + barcodeA.ToUpper() == barcodeB;
            //}
        }
    }
}
