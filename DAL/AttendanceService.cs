using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//引用动态链接库
using Models;
using DAL.Helper;
using Models.Ext;
using System.Data.SqlClient;

namespace DAL
{
    public class AttendanceService
    {
        //添加打卡信息
        public int AddRecord(string cardNo)
        {
            //构建SQL
            string sql = "insert into Attendance(CardNo,DTime) values ('{0}',getdate())";
            sql = string.Format(sql, cardNo);
            //发起请求
            return  SQLHelper.Upadate(sql);
        }

        //获取学生总人数
        public string GetAllStudents()
        {
            //构建SQL语句
            string sql = "select count(*) from Students";
            //执行拿到返回结果
            return SQLHelper.GetSingleResult(sql).ToString();
        }

        //获取当天已签到学员总数

        public string GetAttendStudents(DateTime dt,bool isToday)
        {
            DateTime dt1;
            //判断获取当天的时间或指定时间
            if (isToday)
            {
                //获取数据库的时间
                dt1 = Convert.ToDateTime(SQLHelper.GetServerTime());
            }
            else
            {
                dt1 = dt;
            }

            //构建SQL语句发起时间段是否签到查询
            string sql = "select count(distinct CardNo) from Attendance where DTime between '{0}' and '{1}'";
            //使用时间类叠加一天
            DateTime dt2 = dt1.AddDays(1);
            sql = string.Format(sql, dt1, dt2);
            //返回查询结果
            return SQLHelper.GetSingleResult(sql).ToString();

        }

        //根据时间和姓名进行考勤查询
        public List<SutdentEx> GetStudentsByDate(DateTime beginDate,DateTime endDate,string StudentName)
        {
            //构建SQl语句
            string sql = "select StudentId,StudentName,Gender,DTime,ClassName,Attendance.CardNo from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId";
            sql += " inner join Attendance on Students.CardNo=Attendance.CardNo";
            sql += " where DTime between '{0}' and '{1}'";
            sql = string.Format(sql, beginDate, endDate);
            //判断是否加入了名字
            if (StudentName!=null && StudentName.Length!=0)
            {
                sql+= string.Format(" and StudentName='{0}'", StudentName);
            }
            //排序
            sql += "Order By DTime ASC";
            //发起请求
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //接受数据对象
            List<SutdentEx> list = new List<SutdentEx>();
            //循环接受数据
            while (objReader.Read())
            {
                list.Add(new SutdentEx()
                {
                    StudentId=Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    DTime = Convert.ToDateTime(objReader["DTime"])
                });
            }
            objReader.Close();
            return list;

        }
    }
}
