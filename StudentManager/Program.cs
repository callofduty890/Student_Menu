using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


using System.Diagnostics;
using Models;

namespace StudentManager
{
    static class Program
    {
        //生成一个变量，用来存用户名信息
        public static Admin currentAdmin = null;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //显示登录窗体
            FrmUserLogin frmUserLogin = new FrmUserLogin();
            //拿到登录窗体返回值
            DialogResult result = frmUserLogin.ShowDialog();
            //判断返回值，从而确定是否登录成功
            if (result==DialogResult.OK)
            {
                //启动运行主窗体
                Application.Run(new FrmMain());
            }
            else
            {
                //关闭所有程序
                Application.Exit();
            }

           

        }

    }
}
