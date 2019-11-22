using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DAL;
using Models;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        //实例化成绩操作对象
        ScoreListService objScoreListService = new ScoreListService();

        //实例化学生班级操作对象
        StudentClassService objStudentClass = new StudentClassService();

        public FrmScoreManage()
        {
            InitializeComponent();
            //断开事件
            this.cboClass.SelectedIndexChanged -=
                  new EventHandler(this.cboClass_SelectedIndexChanged);

            //静止自动生成列
            this.dgvScoreList.AutoGenerateColumns = false;

            //初始化班级下拉框
            this.cboClass.DataSource = objStudentClass.GetAllClasses();
            //调整对应的list
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";//设置下拉框文本

            //绑定事件
            this.cboClass.SelectedIndexChanged +=
                  new EventHandler(this.cboClass_SelectedIndexChanged);

        }     
        //根据班级查询      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断是有有值
            if (this.cboClass.SelectedIndex==-1)
            {
                MessageBox.Show("请首先选择要查询的班级", "查询提示");
                return;
            }
            //按班级查询
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList(this.cboClass.Text.Trim());
            //调试显示样式
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //同步显示班级考试信息
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]考试成绩统计";

            //接受查询结果
            Dictionary<string, string> dic =objScoreListService.GetScoreInfoByClassId(this.cboClass.SelectedValue.ToString());

            //依次取值
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //显示缺考人员姓名
            List<string> list = objScoreListService.GetAbsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //统计全校考试成绩
        private void btnStat_Click(object sender, EventArgs e)
        {
            //查询全部的考试成绩
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList("");
            //调试显示样式
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //显示考试情况
            Dictionary<string, string> dic = objScoreListService.GetScoreInfo();
            //显示考试信息
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //显示缺考人数
            List<string> list = objScoreListService.GetAbsetList();
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }


        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //选择框选择改变处理
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
         
        }

       
    }
}