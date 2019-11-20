using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Models;
using DAL.Helper;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class StudentClassService
    {
        //获取全部班级信息
        public  List<StudentClass> GetAllClasses()
        {
            //构建SQL语句
            string sql = "select ClassName,ClassId from StudentClass";
            //执行查询语句
            SqlDataReader objRead = SQLHelper.GetReader(sql);
            //接受返回结果
            List<StudentClass> list = new List<StudentClass>();
            //循环接收
            while (objRead.Read())
            {
                list.Add(new StudentClass
                {
                    ClassId =Convert.ToInt32(objRead["ClassId"]),
                    ClassName = objRead["ClassName"].ToString()
                });
            }
            objRead.Close();
            return list;
        }
    }
}
