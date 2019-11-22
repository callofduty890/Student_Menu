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

        //执行单一查询结果
        public static object GetSingleResult(string sql)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection(connString);
            //创建操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            //打开数据库
            conn.Open();
            //执行SQL语句
            object result = cmd.ExecuteScalar();
            //返回执行的结果
            return result;
        }

        //更新数据 
        public static int Upadate(string sql)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection(connString);
            //创建操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            //打开数据库
            conn.Open();
            //返回执行受影响的行数
            return cmd.ExecuteNonQuery();
        }

        //获取系统时间-数据库 原因:电脑本地时间有可能尚未同步北京时间
        public static DateTime GetServerTime()
        {
            string sql = "select getdate()";
            return Convert.ToDateTime(SQLHelper.GetSingleResult(sql));
        }

        //执行返回数据集以DataSet的形式
        public static DataSet GetDataSet(string sql)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection(connString);
            //创建操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            //创建数据适配器
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //打开数据库
            conn.Open();
            //创建内存表用以存数据
            DataSet ds = new DataSet();
            //填充数据
            da.Fill(ds);
            //返回填充数据
            return ds;

        }

    }

}
