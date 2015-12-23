using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Model;

namespace EVERGRANDE.Common
{
    public class StaticInfo
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        public const char SplitChat = ',';

        public const string ProgramPath = @"\Application\ENPOT\EVERGRANDE";
        public const string ExportPath = @"\Application\Export";

        #region FrmLogin
        public const string UserFile = @"\Application\User.csv";
        public static User LoginUser { get; set; }
        public static List<User> UserList { get; set; }
        #endregion

        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string ExportFileFormat = "yyyyMMddHHmmss";
        public const string ExportFileExtension = ".csv";

        #region FrmSampleScan
        public const string SampleExportFilePrefix = @"Sample";
        public const string FrmSampleScanDataFileName = @"FrmSampleScanData.csv";
        public const string FrmSampleScanExportTitle = @"序号,样本条码,UPG生产标签条码,扫描时间,作业员";
        #endregion

        #region FrmPackageScan
        public const string PackageExportFilePrefix = @"Package";
        public const string FrmPackageScanDataFileName = @"FrmPackageScanData.csv";
        public const string FrmPackageScanExportTitle = @"序号,纳入标签,UPG生产标签条码,扫描时间,作业员";
        #endregion

        #region FrmPalletScan
        public const string PalletExportFilePrefix = @"Pallet";
        public const string FrmPalletScanDataFileName = @"FrmPalletScanData.csv";
        public const string FrmPalletScanExportTitle = @"序号,托盘标签PN,托盘标签QTY,托盘标签SN,部品PN,部品QTY,部品SN,扫描时间,作业员";
        #endregion

        #region FrmPackagingScan
        public const string PackagingExportFilePrefix = @"Packaging";
        public const string FrmPackagingScanDataFileName = @"FrmPackagingScanData.csv";
        public const string FrmPackagingScanExportTitle = @"箱号,罐码,扫描时间,作业员";
        #endregion

        #region FrmPalletDeliveryScan
        public const string PalletDeliveryExportFilePrefix = @"PalletDelivery";
        public const string FrmPalletDeliveryScanDataFileName = @"FrmPalletDeliveryScanData.csv";
        public const string FrmPalletDeliveryScanExportTitle = @"出库单号,出库数量,箱号,扫描时间,作业员";
        #endregion

        #region Rule相关的内容
        /// <summary>
        /// 正则表达式配置文件为嵌入资源时的参数,为执行exe的程序集
        /// </summary>
        public static System.Reflection.Assembly asm { get; set; }
        /// <summary>
        /// 正则表达式配置文件路径
        /// </summary>
        public static string RulesConfigResourcePath = "EVERGRANDE.ConfigResource.RulesConfig.xml";
        #endregion

    }
}
