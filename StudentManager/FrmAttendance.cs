using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmAttendance : Form
    {
        //实例化考勤类
        AttendanceService ObjAttendanceService = new AttendanceService();

        public FrmAttendance()
        {
            InitializeComponent();
            //启动时间
            timer1_Tick(null, null);
            //显示总考勤总人数
            this.lblCount.Text = ObjAttendanceService.GetAllStudents();
            //显示已签到的人-显示出勤人数
            ShowStat();
        }

        private void ShowStat()
        {
            //显示实际出勤人数
            this.lblReal.Text = ObjAttendanceService.GetAttendStudents(DateTime.Now, true);
            //显示缺勤人数 总人数-实到人数
            this.lblAbsenceCount.Text = 
                (
                (Convert.ToInt32(this.lblCount.Text.Trim())) - (Convert.ToInt32(this.lblReal.Text.Trim()))
                ).ToString();
        }
        //显示当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblYear.Text = DateTime.Now.Year.ToString();
            this.lblMonth.Text = DateTime.Now.Month.ToString();
            this.lblDay.Text = DateTime.Now.Day.ToString();
            this.lblTime.Text = DateTime.Now.ToLongTimeString();

            //显示中文礼拜几
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.lblWeek.Text = "一";
                    break;
                case DayOfWeek.Tuesday:
                    this.lblWeek.Text = "二";
                    break;
                case DayOfWeek.Wednesday:
                    this.lblWeek.Text = "三";
                    break;
                case DayOfWeek.Thursday:
                    this.lblWeek.Text = "四";
                    break;
                case DayOfWeek.Friday:
                    this.lblWeek.Text = "五";
                    break;
                case DayOfWeek.Saturday:
                    this.lblWeek.Text = "六";
                    break;
                case DayOfWeek.Sunday:
                    this.lblWeek.Text = "日";
                    break;
            }
        }
        //学员打卡        
        private void txtStuCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            //判断当用户输入的内容不为空并且按下回车键后启动相应
            if (this.txtStuCardNo.Text.Trim().Length !=0 && e.KeyValue==13)
            {
                //显示学员信息
                Student objstudent = new StudentService().GetStudentByCardNo(this.txtStuCardNo.Text.Trim());
                //如果返回为null提示
                if (objstudent==null)
                {
                    MessageBox.Show("卡号不正确!", "信息提示");
                    //界面提示
                    this.lblInfo.Text = "打卡失败";
                    //返回
                    return;
                }
                //显示个人信息
                this.lblStuName.Text = objstudent.StudentName;
                this.lblStuClass.Text = objstudent.ClassName;
                this.lblStuId.Text = objstudent.StudentId.ToString();
                this.pbStu.Image = Image.FromFile("default.png");
                //添加打卡信息
                int result = ObjAttendanceService.AddRecord(this.txtStuCardNo.Text.Trim());
                if (result==1)
                {
                    this.lblInfo.Text = "打卡成功";
                    ShowStat();
                }
                else
                {
                    this.lblInfo.Text = "打卡失败";
                }

            }
        }
        //结束打卡
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
