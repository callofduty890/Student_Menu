using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL.Helper;
using System.Data.SqlClient;
using System.Data;
using Models;
using Models.Ext;

namespace DAL
{
    public class ScoreListService
    {
        //获取所有学员成绩
        public DataSet GetAllScoreList()
        {
            //构建SQL语句
            string sql = "select Students.StudentId,StudentName,Gender,ClassName,PhoneNumber,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId ";
            //传入执行SQL语句
            return SQLHelper.GetDataSet(sql);
        }

        //获取所有学员考试成绩
        public List<SutdentEx> GetScoreList(string ClassName)
        {
            //构建SQL语句
            string sql = "select Students.StudentId,StudentName,Gender,ClassName,PhoneNumber,CSharp,SQLServerDB from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId ";
            //可以根据班级修改
            if (ClassName!=null && ClassName.Length!=0)
            {
                sql += "where ClassName='" + ClassName + "'";
            }
            SqlDataReader objReader = SQLHelper.GetReader(sql);

            List<SutdentEx> list = new List<SutdentEx>();

            while (objReader.Read())
            {
                list.Add(new SutdentEx()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    CSharp = objReader["CSharp"].ToString(),
                    SQLServerDB = objReader["SQLServerDB"].ToString(),
                    cc = false
                });
            }
            objReader.Close();
            return list;

        }

        //获取考试信息
        public Dictionary<string,string>GetScoreInfo()
        {
            //构建SQL语句
            string sql = "select stuCount=count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from ScoreList;";
            sql += "select absectCount=count(*) from Students where StudentId not in (select StudentId from ScoreList)";

            //执行SQL语句
            SqlDataReader objSqlDataReader = SQLHelper.GetReader(sql);

            //接受返回数据
            Dictionary<string, string> scoreInfo = null;

            //判断读取
            if (objSqlDataReader.Read())
            {
                //初始化信息
                scoreInfo = new Dictionary<string, string>();
                //保存信息
                scoreInfo.Add("stuCount", objSqlDataReader["stucount"].ToString());
                scoreInfo.Add("avgCSharp", objSqlDataReader["avgCSharp"].ToString());
                scoreInfo.Add("avgDB", objSqlDataReader["avgDB"].ToString());

            }
            //第二个SQL语句读取
            if (objSqlDataReader.NextResult())
            {
                if (objSqlDataReader.Read())
                {
                    scoreInfo.Add("absectCount", objSqlDataReader["absectCount"].ToString());
                }
            }
            objSqlDataReader.Close();
            return scoreInfo;
        }

        //获取尚未参加考试人员名单
        public List<string> GetAbsetList()
        {
            //构建SQL语句
            string sql = "select StudentName from Students where StudentId not in (select StudentId from ScoreList)";
            //执行SQL语句
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //形成列表接收结果
            List<string> list = new List<string>();
            //循环接收结果
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }

        //按班级获取尚未参加考试的人员数量
        public Dictionary<string, string> GetScoreInfoByClassId(string classId)
        {
            //构建SQL语句
            string sql = "select stuCount=count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from ScoreList inner join Students on Students.StudentId = ScoreList.StudentId where ClassId = {0}; ";
            sql += "select absectCount=count(*) from Students where StudentId not in (select StudentId from ScoreList) and ClassId={1}";
            sql = string.Format(sql, classId, classId);
            //创建对象
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //保存信息
            Dictionary<string, string> scoreInfo = null;
            //读取考试成绩统计结果
            if (objReader.Read())
            {
                scoreInfo = new Dictionary<string, string>();
                scoreInfo.Add("stuCount", objReader["stucount"].ToString());
                scoreInfo.Add("avgCSharp", objReader["avgCSharp"].ToString());
                scoreInfo.Add("avgDB", objReader["avgDB"].ToString());
            }
            if (objReader.NextResult())//读取缺考人数列表
            {
                if (objReader.Read())
                {
                    scoreInfo.Add("absectCount", objReader["absectCount"].ToString());
                }
            }
            objReader.Close();
            return scoreInfo;
        }

        //按班级获取尚未参加考试的人员名单
        public List<string> GetAbsentListByClassId(string classId)
        {
            //构建SQL语句
            string sql = "select StudentName from Students where StudentId not in (select StudentId from ScoreList) and ClassId="+ classId;
            //执行SQL语句 
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //形成列表接收结果
            List<string> list = new List<string>();
            //循环接收结果
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }
    }
}
