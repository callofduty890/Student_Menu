using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;

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
            #region 数据验证-输入账号是否按照格式
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

            #region 封装接受到的数据：登录账号/登录密码

            #endregion
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }
    }
}
