using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ENPOT.View;
using Symbol.Barcode;
using Symbol;

namespace EVERGRANDE.View
{
    public partial class FrmTest : BaseForm
    {

        public FrmTest()
        {
            InitializeComponent();
            this.initUI();
            this.initEvent();
        }

        private void initUI()
        {

        }

        private void initEvent()
        {
            this.button1.Click += (s, e) =>
            {
                this.Close();
            };

        }

        protected override void OnRead(ReaderData data)
        {
            if (this.textBox1.Focused && data.Result == Results.SUCCESS)
            {
                this.label1.Text = data.Text;
                this.textBox1.Text = data.Text.Replace("http://", "");
            }
        }

    }
}