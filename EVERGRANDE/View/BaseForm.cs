using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EVERGRANDE;

namespace ENPOT.View
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.initScanDevice();
            this.initUI();
            this.initEvent();
        }

        #region Scan Device

        private Barcode.Barcode barScan;

        private void initScanDevice()
        {
            this.barScan = new Barcode.Barcode();

            // 扫描枪开启
            this.barScan.EnableScanner = false; // 

            // 设置扫描枪读取条码编码
            this.barScan.DecoderParameters.CODABAR = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.CODE128 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.CODE39 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.CODE39Params.Code32Prefix = false;
            this.barScan.DecoderParameters.CODE39Params.Concatenation = false;
            this.barScan.DecoderParameters.CODE39Params.ConvertToCode32 = false;
            this.barScan.DecoderParameters.CODE39Params.FullAscii = false;
            this.barScan.DecoderParameters.CODE39Params.Redundancy = false;
            this.barScan.DecoderParameters.CODE39Params.ReportCheckDigit = false;
            this.barScan.DecoderParameters.CODE39Params.VerifyCheckDigit = false;
            this.barScan.DecoderParameters.D2OF5 = Barcode.DisabledEnabled.Disabled;
            this.barScan.DecoderParameters.EAN13 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.EAN8 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.I2OF5 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.KOREAN_3OF5 = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.MSI = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.UPCA = Barcode.DisabledEnabled.Enabled;
            this.barScan.DecoderParameters.UPCE0 = Barcode.DisabledEnabled.Enabled;

            // 扫描设置
            this.barScan.ScanParameters.BeepFrequency = 2670;
            this.barScan.ScanParameters.BeepTime = 200;
            this.barScan.ScanParameters.CodeIdType = Barcode.CodeIdTypes.None;
            this.barScan.ScanParameters.LedTime = 3000;
            this.barScan.ScanParameters.ScanType = Barcode.ScanTypes.Foreground;
            this.barScan.ScanParameters.WaveFile = "";

            this.barScan.OnRead += new Barcode.Barcode.ScannerReadEventHandler(this.barScan_OnRead);
        }

        /// <summary>
        /// 扫描头在窗体激活状态时候可以扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanFormActivated(object sender, EventArgs e)
        {
            if (this.barScan.EnableScanner != true)
            {
                this.barScan.EnableScanner = true;
            }
        }

        /// <summary>
        /// 扫描头在窗体无效状态时候不能扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanFormDeactivate(object sender, EventArgs e)
        {
            if (this.barScan.EnableScanner != false)
            {
                Application.DoEvents();
                this.barScan.EnableScanner = false;
            }
        }

        #endregion

        #region BaseForm UI

        private void initUI()
        {
            this.BackColor = Color.White;
        }

        private void initEvent()
        {
            this.Activated += new EventHandler(this.ScanFormActivated);
            this.Deactivate += new EventHandler(this.ScanFormDeactivate);
            this.Paint += new PaintEventHandler(BaseForm_Reset); // BaseForm 移动后复位
            this.TopMost = true; // 窗口位于最前
        }

        /// <summary>
        /// Form.Paint
        /// 作用 ：窗口移动后复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Reset(object sender, PaintEventArgs e)
        {
            if (this.Top != 0 || this.Left != 0)
            {
                this.Top = 0;
                this.Left = 0;
            }
        }

        #endregion

        /// <summary>
        /// 继承Form重写此方法
        /// 
        /// 扫描条码处理方法
        /// </summary>
        protected virtual void OnRead(Symbol.Barcode.ReaderData readerData)
        {

        }

        /// <summary>
        /// 继承Form重写此方法
        /// 
        /// 扫描OK码
        /// </summary>
        protected virtual void OnScanOK()
        {
        }

        private void barScan_OnRead(object sender, Symbol.Barcode.ReaderData readerData)
        {
            this.OnRead(readerData);

            //string barcode = readerData.Text.Trim();

            //switch (barcode)
            //{
            //    case "OK":
            //        this.OnScanOK();
            //        break;
            //    default:
            //        this.OnScan(readerData.Text.Trim());
            //        break;
            //}
        }

        #region 软键盘

        /// <summary>
        /// 软键盘关闭 
        /// </summary>
        private static uint SIPF_OFF = 0x00;

        /// <summary>
        /// 软键盘打开
        /// </summary>
        private static uint SIPF_ON = 0x01;

        [DllImport("coredll.dll")]
        private extern static void SipShowIM(uint dwFlag); //软键盘操作函数

        public static void ShowIM(bool isShow)
        {
            if (isShow)
            {
                BaseForm.SipShowIM(BaseForm.SIPF_ON);
            }
            else
            {
                BaseForm.SipShowIM(BaseForm.SIPF_OFF);
            }
        }

        public void ShowIM()
        {
            BaseForm.ShowIM(true);
        }

        public void HideIM()
        {
            BaseForm.ShowIM(false);
        }

        #endregion

        public event EventHandler OnLoaded;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Show();
            Application.DoEvents();
            if (this.OnLoaded != null)
            {
                this.OnLoaded(null, null);
            }
        }

        //public MsgBoxHelper MsgBoxHelper;

        //public void Play(SoundType type)
        //{
        //    try
        //    {
        //        MsgBoxHelper.PlaySound(type);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}



    }

    public enum ScanData
    {
        FirstBarcode,
        SecondBarcode,
        ThirdBarcode
    }
}