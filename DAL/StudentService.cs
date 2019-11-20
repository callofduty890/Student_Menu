using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helper;

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
    }
}
