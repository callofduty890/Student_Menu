using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//引用自己创建的数据库-中间件
using Models;
using System.Data;
using System.Data.SqlClient;
using DAL.Helper;

namespace DAL
{
    public class AdminService
    {
      //登录
       public Admin AdminLogin(Admin objAdmin)
        {
            //构建查询语句
            string sql = "select LoginId,LoginPwd,AdminName from Admins where LoginId={0} and LoginPwd={1}";
            sql = string.Format(sql, objAdmin.LoginId, objAdmin.LoginPwd);

            //发起请求
            SqlDataReader objRead = SQLHelper.GetReader(sql);
            //判断是否读取成功
            if (objRead.Read())
            {
                //拿到用户名
                objAdmin.AdminName = objRead["AdminName"].ToString();
                //关闭对象
                objRead.Close();
            }
            else
            {
                objAdmin = null;
            }

            return objAdmin;
        }

       
    }
}
