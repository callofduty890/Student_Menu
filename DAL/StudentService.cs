using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helper;
using Models;

namespace DAL
{
    public class StudentService
    {
        //查询学生的身份证号码是否已存在
        public bool IsIdNoExisted(string studentIdNo)
        {
            //构建SQL语句
            string sql = "select COUNT(*) from Students where StudentIdNo="+ studentIdNo;
            //执行查询
            int result = Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            //判断是否存在
            if (result==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //判断卡号是否已经存在
        public bool IsCardNoExisted(string cardNo)
        {
            //构建SQL语句
            string sql = string.Format("select count(*) from Students where CardNo='{0}'", cardNo);
            //接收返回的结果
            int result = Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            //判断是否存在
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //添加学员
        public int AddStudent(Student objstudent)
        {
            //构建SQL语句
            StringBuilder sqlstringBuilder = new StringBuilder();
            sqlstringBuilder.Append("insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId,StuImage)");
            sqlstringBuilder.Append("values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}',{8},'{9}');select @@Identity");
            //对占位符进行填值
            string sql = string.Format(sqlstringBuilder.ToString(),
                objstudent.StudentName,
                objstudent.Gender,
                objstudent.Birthday.ToString("yyy-MM-dd"),
                objstudent.Age,
                objstudent.StudentIdNo,
                objstudent.CardNo,
                objstudent.PhoneNumber,
                objstudent.StudentAddress,
                objstudent.ClassId,
                objstudent.StuImage
                );
            //传入sql语句执行返回结果
            return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
        }

    }
}
