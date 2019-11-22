using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helper;
using Models;
using System.Data.SqlClient;

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

        //根据班级查询学员信息
        public List<Student> GetStudentByClass(string className)
        {
            //形成SQL查询语句
            string sql = "select StudentId,StudentName,Gender,PhoneNumber,StudentIdNo,Birthday,ClassName from Students ";
            sql += "inner join StudentClass on Students.ClassId=StudentClass.ClassId";
            sql += " where ClassName='{0}'";
            sql = string.Format(sql, className);

            //执行查询语句
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //接受返回结果
            List<Student> list = new List<Student>();
            //循环接收
            while (objReader.Read())
            {
                list.Add(new Student
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    ClassName = objReader["ClassName"].ToString()
                });
            }
            objReader.Close();
            return list;


        }

        //通过学生ID查询
        public Student GetStudentById(string studentId)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,PhoneNumber,StudentIdNo,CardNo,Birthday,StudentAddress,ClassName,StuImage from Students ";
            sql += "inner join StudentClass on Students.ClassId=StudentClass.ClassId";
            sql += " where StudentId={0}";
            sql = string.Format(sql, studentId);

            //执行查询语句
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //接受返回结果
            Student objStudent = null;
            //循环接收
            while (objReader.Read())
            {
                objStudent = new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    StudentAddress = objReader["StudentAddress"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    StuImage = objReader["StuImage"] == null ? "" : objReader["StuImage"].ToString()
                };
            }
            objReader.Close();
            return objStudent;


        }

        //通过学生考勤卡号
        public Student GetStudentByCardNo(string CardNo)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,PhoneNumber,StudentIdNo,CardNo,Birthday,StudentAddress,ClassName,StuImage from Students ";
            sql += "inner join StudentClass on Students.ClassId=StudentClass.ClassId";
            sql += " where CardNo='{0}'";
            sql = string.Format(sql, CardNo);

            //执行查询语句
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //接受返回结果
            Student objStudent = null;
            //循环接收
            while (objReader.Read())
            {
                objStudent = new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    StudentAddress = objReader["StudentAddress"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    StuImage = objReader["StuImage"] == null ? "" : objReader["StuImage"].ToString()
                };
            }
            objReader.Close();
            return objStudent;
        }

        //修改学生信息
        public int ModifyStudent(Student objStudent)
        {
            //编写SQL语句 
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update Students");
            stringBuilder.Append(" set studentName='{0}',Gender='{1}',Birthday='{2}',StudentIdNo={3},Age={4},PhoneNumber='{5}',StudentAddress='{6}',CardNo='{7}',ClassId={8},StuImage='{9}'");
            stringBuilder.Append(" where StudentId={10}");
            //构建SQL语句
            string sql = string.Format(stringBuilder.ToString(), objStudent.StudentName,
                     objStudent.Gender, objStudent.Birthday.ToString("yyyy-MM-dd"),
                    objStudent.StudentIdNo, objStudent.Age,
                    objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.CardNo,
                    objStudent.ClassId, objStudent.StuImage, objStudent.StudentId);
            //执行SQL语句
            return Convert.ToInt32(SQLHelper.Upadate(sql));

        }

        //删除学生信息
        public int DelectStudentById(string studentId)
        {
            //构建SQL语句
            string sql = "delete from Students where StudentId=" + studentId;
            //执行SQL语句
            return SQLHelper.Upadate(sql);
        }
    }
}
