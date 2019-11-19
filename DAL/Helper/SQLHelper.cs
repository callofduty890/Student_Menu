using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//引用数据库的动态链接库
using System.Data.SqlClient;
using System.Data;

namespace DAL.Helper
{
    //SQL方位通用底层类
    public class SQLHelper
    {
        //连接语句
        private static readonly string connString = 
        @"Server=DESKTOP-CTV4ATU\SQLSERVER;DataBase=SMDB;Uid=sa;Pwd=123456";

        //执行SQL语句获取结果
        public static SqlDataReader GetReader(string sql)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection(connString);
            //创建操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            //打开数据库
            conn.Open();
            //执行SQL语句
            SqlDataReader objReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //返回执行的结果
            return objReader;
        }

    }
}
