using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EVERGRANDE
{
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            InitializeComponent();
            this.initUI();
        }

        private void initUI()
        {
            this.TopMost = true;
            //this.Paint += new PaintEventHandler(FrmScanBase_Paint);
        }

        void FrmScanBase_Paint(object sender, PaintEventArgs e)
        {
            if (this.Top != 0 || this.Left != 0)
            {
                this.Top = 0;
                this.Left = 0;
            }
        }



        private void initAudio()
        {

            //this.audioDevice = (Symbol.Audio.Device)Symbol.StandardForms.SelectDevice.Select(
            //    Symbol.Audio.Controller.Title, Symbol.Audio.Device.AvailableDevices);

            //if (this.audioDevice.AudioType == Symbol.Audio.AudioType.StandardAudio)
            //{
            //    this.myAudio = new Symbol.Audio.StandardAudio(this.audioDevice);
            //}
            //else if (this.audioDevice.AudioType == Symbol.Audio.AudioType.SimulatedAudio)
            //{
            //    this.myAudio = new Symbol.Audio.SimulatedAudio(this.audioDevice);
            //}


            //this.Closing += new CancelEventHandler(FrmScanBase_Closing);

        }


        //public enum ScanData
        //{
        //    FirstBarcode,
        //    SecondBarcode,
        //    ThirdBarcode
        //}

        #region 软键盘

        private static uint SIPF_OFF = 0x00;//软键盘关闭 
        private static uint SIPF_ON = 0x01;//软键盘打开

        [DllImport("coredll.dll")]
        private extern static void SipShowIM(uint dwFlag); //软键盘操作函数

        public static void ShowIM(bool isShow)
        {
            if (isShow)
            {
                SipShowIM(SIPF_ON);
            }
            else
            {
                SipShowIM(SIPF_OFF);
            }
        }

        #endregion
    }
}