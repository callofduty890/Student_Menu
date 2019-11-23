using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

namespace DAL.Helper
{
    public class OleDBHelper
    {
        //Excel的连接语句
        private static string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0";

        //执行返回数据集
        public static DataSet GetDataSet(string sql)
        {
            //连接语句
            OleDbConnection conn = new OleDbConnection(connString);
            //创建操作对象
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            //创建数据适配器对象
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //创建内存数据集表
            DataSet ds = new DataSet();
            //打开连接
            conn.Open();
            //填充数据集
            da.Fill(ds);
            //返回数据集结果
            return ds;
        }

        public static DataSet GetDataSet(string sql,string path)
        {
            //形成新的连接语句
            connString = string.Format(connString, path);
            //连接语句
            OleDbConnection conn = new OleDbConnection(connString);
            //创建操作对象
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            //创建数据适配器对象
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //创建内存数据集表
            DataSet ds = new DataSet();
            //打开连接
            conn.Open();
            //填充数据集
            da.Fill(ds);
            //返回数据集结果
            return ds;
        }

    }
}
