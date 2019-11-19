using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//引用自己写的动态链接库
using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmUserLogin : Form
    {
        public FrmUserLogin()
        {
            InitializeComponent();
        }


        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 1. 数据验证-输入账号是否按照格式
            //检查 账号-密码 是否有输入内容
            if (this.txtLoginId.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入登录的账号!", "提示信息");
                //调整winfrom控件焦点
                this.txtLoginId.Focus();
                return;
            }
            if (this.txtLoginPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录的密码", "提示信息");
                //调整winfrom控件焦点
                this.txtLoginPwd.Focus();
                return;
            }
            #endregion

            #region 2. 封装接受到的数据：登录账号/登录密码
            Admin objAdmin = new Admin()
            {
                LoginId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                LoginPwd = this.txtLoginPwd.Text.Trim()
            };
            #endregion

            #region 3.将数据对象送到数据处理层，接收返回数据
            Program.currentAdmin = new AdminService().AdminLogin(objAdmin);
            if (Program.currentAdmin!=null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("用户名或密码错误!", "提示信息");
            }
            #endregion
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
