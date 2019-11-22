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
    public partial class FrmAttendanceQuery : Form
    {
        //实例化签到操作对象
        AttendanceService objAttendanceService = new AttendanceService();

        public FrmAttendanceQuery()
        {
            InitializeComponent();
            //禁止表格自动添加列信息
            this.dgvStudentList.AutoGenerateColumns = false;


        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //查询考勤结果到表格当中
            //形成时间 dt1 起始时间  dt2 终止时间
            DateTime dt1 = Convert.ToDateTime(this.dtpTime.Text);
            DateTime dt2 = dt1.AddDays(1);

            //查询指定时间内签到的学院信息
            this.dgvStudentList.DataSource = objAttendanceService.GetStudentsByDate(dt1, dt2, this.txtName.Text.Trim());
            //界面美化
            new Common.DataGridViewStyle().DgvStyle3(this.dgvStudentList);


            //显示总考勤总人数
            this.lblCount.Text = objAttendanceService.GetAllStudents();
            //显示实际出勤人数
            this.lblReal.Text = objAttendanceService.GetAttendStudents(Convert.ToDateTime(this.dtpTime.Text), false);
            //显示缺勤人数 总人数-实到人数
            this.lblAbsenceCount.Text =
                (
                (Convert.ToInt32(this.lblCount.Text.Trim())) - (Convert.ToInt32(this.lblReal.Text.Trim()))
                ).ToString();

        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
