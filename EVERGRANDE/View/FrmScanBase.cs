using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bt;
using System.Threading;

namespace EVERGRANDE
{
    public partial class FrmScanBase : Form
    {
        public FrmScanBase()
        {
            InitializeComponent();
            this.MsgWin = new MsgWindow();		// 生成消息窗口
            this.initEvent_BaseScan();
            this.openScanDevice();
        }

        private void initEvent_BaseScan()
        {
            this.Activated += new EventHandler(this.ScanFormActivated);
            this.Deactivate += new EventHandler(this.ScanFormDeactivate);
            this.Paint += new PaintEventHandler(BaseForm_Reset); // BaseForm 移动后复位
            this.KeyDown += new KeyEventHandler(FrmScanBase_KeyDown);

            this.MsgWin.ReadBarcode += new EventHandler<ReaderData>(this.barScan_OnRead);
            this.Closed += new EventHandler(FrmScanBase_Closed);
        }

        void FrmScanBase_Closed(object sender, EventArgs e)
        {
            if (this.IsScanDeviceOpen)
            {
                this.closeScanDevice();
            }
        }

        #region BaseForm UI

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

        #region Barcode Scan

        private bool isScanDeviceOpen = false;

        public bool IsScanDeviceOpen
        {
            get { return isScanDeviceOpen; }
            set { isScanDeviceOpen = value; }
        }

        /// <summary>
        /// 消息窗口
        /// </summary>
        public MsgWindow MsgWin;

        /// <summary>
        /// 扫描模式
        /// 1:逐个
        /// 2:全体
        /// </summary>
        public static Int32 ScanMode = 2;


        void FrmScanBase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 153) // 设备上Scan按钮按下
                {
                    this.onScanBarcode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "错误");
            }
        }

        private void onScanBarcode()
        {
            ReaderData args = null;

            Int32 ret = 0;
            String disp = "";
            Int32 markerSet = 1;
            Int32 ledSet = 1;
            try
            {
                // 开启瞄准器
                ret = Bt.ScanLib.Control.btScanMarker(markerSet, ledSet);
                if (ret != LibDef.BT_OK)
                {
                    disp = "btScanMarker error ret[" + ret + "]";
                    args = new ReaderData(Results.FAIL, string.Empty, disp);
                    this.OnRead(args);
                    return;
                }

                //ret = Bt.ScanLib.Control.btScanSoftTrigger(1);
                //if (ret != Bt.LibDef.BT_OK)
                //{
                //    disp = "btScanSoftTrigger error ret[" + ret + "]";
                //    args = new ReaderData(Results.FAIL, string.Empty, disp);
                //    this.OnRead(args);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                args = new ReaderData(Results.FAIL, string.Empty, ex.StackTrace);
                this.OnRead(args);
                return;
            }
        }

        public class MsgWindow : Microsoft.WindowsCE.Forms.MessageWindow
        {
            public EventHandler<ReaderData> ReadBarcode;

            private void OnReadBarcode(ReaderData args)
            {
                if (this.ReadBarcode != null)
                {
                    this.ReadBarcode.Invoke(this, args);
                }
            }

            public MsgWindow()
            {
            }

            protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message msg)
            {
                switch (msg.Msg)
                {
                    case (Int32)Bt.LibDef.WM_BT_SCAN: // 读取成功的场合
                        if (msg.WParam.ToInt32() == (Int32)Bt.LibDef.BTMSG_WPARAM.WP_SCN_SUCCESS)
                        {
                            if (FrmScanBase.ScanMode == 2) // 读取(一次性)
                            {
                                this.getBarcodeData();
                            }
                        }
                        break;
                }
                base.WndProc(ref msg);
            }

            /// <summary>
            /// 一次性取得读取到的条码数据
            /// </summary>
            public void getBarcodeData()
            {
                ReaderData args = null;
                try
                {
                    Int32 ret = 0;
                    String disp = "";
                    Byte[] codedataGet;
                    String strCodedata = "";
                    Int32 codeLen = 0;
                    UInt16 symbolGet = 0;

                    // 获取条码长度
                    codeLen = Bt.ScanLib.Control.btScanGetStringSize();
                    if (codeLen <= 0)
                    {
                        disp = "btScanGetStringSize error ret[" + codeLen + "]";
                        //args = new ReaderData(Results.FAIL, string.Empty, disp);
                        //this.OnReadBarcode(args);
                        goto L_END;
                    }

                    // 根据条码长度获取条码信息
                    codedataGet = new Byte[codeLen];
                    ret = Bt.ScanLib.Control.btScanGetString(codedataGet, ref symbolGet);
                    if (ret != Bt.LibDef.BT_OK)
                    {
                        disp = "btScanGetString error ret[" + ret + "]";
                        //args = new ReaderData(Results.FAIL, string.Empty, disp);
                        //this.OnReadBarcode(args);
                        goto L_END;
                    }

                    // 条码信息转为String，并获取条码种类等信息
                    strCodedata = System.Text.Encoding.GetEncoding(932).GetString(codedataGet, 0, codeLen);
                    disp =
                        "数据尺寸        :" + codeLen + "\r\n" +
                        "条码种类        :" + symbolGet + "\r\n" +
                        "代码字符内容  :" + strCodedata + "\r\n";

                    args = new ReaderData(Results.SUCCESS, strCodedata, disp);
                    this.OnReadBarcode(args);
                    return;

                L_END:
                    //ret = Bt.ScanLib.Control.btScanDisable();
                    //if (ret != Bt.LibDef.BT_OK)
                    //{
                    //    disp += "\r\nbtScanDisable error ret[" + ret + "]";
                    //    args = new ReaderData(Results.FAIL, string.Empty, disp);
                    //    this.OnReadBarcode(args);
                    //}
                    this.OnReadBarcode(args);

                }
                catch (Exception e)
                {
                    args = new ReaderData(Results.FAIL, string.Empty, e.StackTrace);
                    this.OnReadBarcode(args);
                }
            }
        }

        private void openScanDevice()
        {
            // 开启扫描设备
            Int32 ret = Bt.ScanLib.Control.btScanEnable();
            if (ret != Bt.LibDef.BT_OK)
            {
                this.IsScanDeviceOpen = false;
                string disp = "btScanEnable error ret[" + ret + "]";
                MessageBox.Show(disp, "错误");
                return;
            }
            else
            {
                this.IsScanDeviceOpen = true;
            }
        }

        private void closeScanDevice()
        {
            // 停止扫描设备
            Int32 ret = Bt.ScanLib.Control.btScanDisable();
            if (ret != Bt.LibDef.BT_OK)
            {
                this.IsScanDeviceOpen = false;
                string disp = "btScanEnable error ret[" + ret + "]";
                MessageBox.Show(disp, "错误");
                return;
            }
            else
            {
                this.IsScanDeviceOpen = true;
            }
        }

        /// <summary>
        /// 扫描头在窗体激活状态时候可以扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanFormActivated(object sender, EventArgs e)
        {
            if (this.IsScanDeviceOpen)
            {
                this.openScanDevice();
            }
        }

        /// <summary>
        /// 扫描头在窗体无效状态时候不能扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanFormDeactivate(object sender, EventArgs e)
        {
            if (!this.IsScanDeviceOpen)
            {
                this.closeScanDevice();
            }
        }

        #endregion

        private void barScan_OnRead(object sender, ReaderData readerData)
        {
            this.OnRead(readerData);
        }

        /// <summary>
        /// 继承Form重写此方法
        /// 
        /// 扫描条码处理方法
        /// </summary>
        protected virtual void OnRead(ReaderData barcode)
        {

        }
    }

    public class ReaderData : EventArgs
    {
        /// <summary>
        /// 读取条码状态
        /// </summary>
        public Results Result { get; private set; }

        /// <summary>
        /// 条码信息
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 条码信息详细
        /// 包含 条码长度，码制，条码信息
        /// </summary>
        public string DataDescription { get; private set; }

        public ReaderData(Results result, string text, string dataDescription)
        {
            this.Result = result;
            this.Text = text;
            this.DataDescription = dataDescription;
        }
    }

    /// <summary>
    /// 读取条码状态
    /// </summary>
    public enum Results
    {
        /// <summary>
        /// 读取成功
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// 读取失败
        /// </summary>
        FAIL = 1
    }


    public enum ScanData
    {
        FirstBarcode,
        SecondBarcode,
        ThirdBarcode
    }
}