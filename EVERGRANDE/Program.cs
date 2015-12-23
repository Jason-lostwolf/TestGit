using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using EVERGRANDE.View;
using System.Reflection;

namespace EVERGRANDE
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            if (Utility.IsAuthorization())
            {
                EVERGRANDE.Common.StaticInfo.asm = Assembly.GetExecutingAssembly();

                string temp = RuleManager.Check("Reference", "1");

                Application.Run(new FrmLogin());
            }
            else
            {
                Utility.ShowError("系统文件缺失，请与条码供应商联系。");
            }
        }
    }
}