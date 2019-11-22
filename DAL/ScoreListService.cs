using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL.Helper;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ScoreListService
    {
        //获取所有学院成绩
        public DataSet GetAllScoreList()
        {
            //构建SQL语句
            string sql = "select Students.StudentId,StudentName,Gender,ClassName,PhoneNumber,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId ";
            //传入执行SQL语句
            return SQLHelper.GetDataSet(sql);
        }
    }
}
