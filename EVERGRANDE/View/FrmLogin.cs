using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVERGRANDE.Common;
using EVERGRANDE.BLL;
using ENPOT.View;

namespace EVERGRANDE.View
{
    public partial class FrmLogin : BaseForm //Form // FrmScanBase
    {
        //管理登陆
        private LoginBLL loginBll = null;

        public FrmLogin()
        {
            InitializeComponent();

            this.initUI();
        }

        private void initUI()
        {
            this.loginBll = new LoginBLL();
            this.ShowVersion();

            //配置界面
            //this.lnkUpdatePassword.Visible = System.Convert.ToBoolean(ConfigObject.UIConfigDic["UpdatePassword"]);
            //this.lblLogin.Text = ConfigObject.SystemConfigDic["ScanBarcodeSystemName"];

            //配置事件
            this.txtUserName.KeyDown += new KeyEventHandler(txtUserName_KeyDown);
            this.txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
            this.btnExit.Click += new EventHandler(btnExit_Click);
            this.Load += new EventHandler(FrmLogin_Load);



            this.txtUserName.GotFocus += (s, e) =>
            {
                FrmBase.ShowIM(false);
            };

            this.txtPassword.GotFocus += (s, e) =>
            {
                FrmBase.ShowIM(false);
            };
        }

        void FrmLogin_Load(object sender, EventArgs e)
        {
            //获取用户信息
            try
            {
                StaticInfo.UserList = this.loginBll.GetUserList();
                if (StaticInfo.UserList.Count == 0)
                {
                    Utility.ShowMsg("没有登录用户。");
                    this.Close();
                }
                //this.Show();
                Application.DoEvents();
                //this.EnableScan(false);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                //Application.DoEvents();
                this.Close();

            }

            // TODO : 快速登录
            //this.txtUserName.Text = "1";
            //this.txtPassword.Text = "1";

            this.txtUserName.Focus();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.UserLogin();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.UserLogin();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.DialogResult = DialogResult.Cancel;
        }

        private void lnkUpdatePassword_Click(object sender, EventArgs e)
        {
            //更新密码
            //FrmUpdatePassword frmUpdatePassword = new FrmUpdatePassword();
            //frmUpdatePassword.ShowDialog();

        }

        private void UserLogin()
        {
            try
            {
                if (this.IsValidate())
                {
                    string userName = this.txtUserName.Text.Trim();
                    string password = this.txtPassword.Text.Trim();

                    //if (this.Controller.IsUserExist(userName))
                    //{
                    //    Utility.ShowMsg("用户名不存在。");
                    //    this.txtUserName.Focus();
                    //    this.txtUserName.SelectAll();
                    //}

                    //登录成功,更新用户名
                    StaticInfo.LoginUser = this.loginBll.Login(userName, password);

                    if (StaticInfo.LoginUser == null)
                    {
                        Utility.ShowMsg("用户名或密码错误。");
                    }
                    else
                    {
                        FrmMain frmMain = new FrmMain();
                        frmMain.ShowDialog();
                        this.txtPassword.Text = string.Empty;
                        this.txtUserName.Text = string.Empty;
                        this.txtUserName.Focus();
                        StaticInfo.LoginUser = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex.Message);
            }
        }

        //验证非空和新密码一致
        private bool IsValidate()
        {
            //读取textBox上面的内容
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(userName) == true)
            {
                Utility.ShowMsg("用户名不能为空。");
                this.txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password) == true)
            {
                Utility.ShowMsg("密码不能为空。");
                this.txtPassword.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 显示版本号
        /// </summary>
        private void ShowVersion()
        {
            this.lblVersion.Text = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmPassword frm = new FrmPassword();
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowError(ex.Message);
            }
        }

    }
}
