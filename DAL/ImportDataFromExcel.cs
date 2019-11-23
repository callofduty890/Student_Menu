using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data;
using DAL;
using DAL.Helper;

namespace DAL
{
    public class ImportDataFromExcel
    {
        //从Excel中读取数据
        public List<Student> GetStudentsByExcel(string path)
        {
            //创建数据集接受返回值
            List<Student> list = new List<Student>();
            //执行查询操作
            DataSet ds = OleDBHelper.GetDataSet("select * from [Student$]", path);
            //拿到表格数据
            DataTable dt = ds.Tables[0];
            //遍历创建对象
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Student()
                {
                    StudentName = row["姓名"].ToString(),
                    Gender = row["性别"].ToString(),
                    Birthday = Convert.ToDateTime(row["出生日期"]),
                    Age = DateTime.Now.Year - Convert.ToDateTime(row["出生日期"]).Year,
                    CardNo = row["考勤卡号"].ToString(),
                    StudentIdNo = row["身份证号"].ToString(),
                    PhoneNumber = row["电话号码"].ToString(),
                    StudentAddress = row["家庭住址"].ToString(),
                    ClassId = Convert.ToInt32(row["班级编号"])
                }
                    );
            }
            return list;
        }

        //将集合中的对象插入到数据库当中
        public bool Import(List<Student> list)
        {
            //构建SQL处理语句
            List<string> sqlList = new List<string>();
            //构建sql语句结构
            string sql;
            //循环形成
            foreach (Student objStudent in list)
            {
            //构建sql语句结构
            sql = "insert into Students (StudentName,Gender,Birthday,StudentIdNo,Age,PhoneNumber,StudentAddress,CardNo,ClassId)";
            sql += "values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}',{8})";
            sql = string.Format(sql,
                    objStudent.StudentName,
                    objStudent.Gender,
                    objStudent.Birthday,
                    objStudent.StudentIdNo.ToString(),
                    objStudent.Age,
                    objStudent.PhoneNumber,
                    objStudent.StudentAddress,
                    objStudent.CardNo,
                    objStudent.ClassId);
            //添加进入SQL语句操作数组
            sqlList.Add(sql);
            }
            //将listSQL语句批量执行
            return SQLHelper.UpdateByTran(sqlList);
        }

    }
}
