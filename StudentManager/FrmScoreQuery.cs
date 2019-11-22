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
    public partial class FrmScoreQuery : Form
    {
        private DataSet ds = null;//保存全部查询结果的数据集
        //创建学生班级对象
        StudentClassService objstudentClassService = new StudentClassService();
        //创建学生成绩操作对象
        ScoreListService objScoreListService = new ScoreListService();

        public FrmScoreQuery()
        {
            InitializeComponent();
            //初始化班级下拉框
            this.cboClass.DataSource = objstudentClassService.GetAllClasses();
            //调整对应的list
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";//设置下拉框文本
            //不显示未封装属性
            this.dgvScoreList.AutoGenerateColumns = false;

            //显示全部考试成绩
            ds = objScoreListService.GetAllScoreList();
            this.dgvScoreList.DataSource = ds.Tables[0];
            //调整显示风格
            new Common.DataGridViewStyle().DgvStyle3(this.dgvScoreList);
        }     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //根据班级名称动态筛选
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //判断表是否为空
            if (ds==null)
            {
                return;
            }
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName='" + this.cboClass.Text.Trim()+"'";

        }
        //显示全部成绩
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName like '%%'";
        }
        //根据C#成绩动态筛选
        private void txtScore_TextChanged(object sender, EventArgs e)
        {
            //筛选成绩
            //判断下拉框是否有班级列表如果没有无需处理
            if (this.txtScore.Text.Trim().Length==0)
            {
                return;
            }
            //判断输入的内容是否为数字，不是数字返回
            if (!Common.DataValidate.IsInteger(this.txtScore.Text.Trim()))
            {
                return;
            }
            else
            {
                this.ds.Tables[0].DefaultView.RowFilter = "CSharp>" + this.txtScore.Text.Trim();
            }
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           // Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

        //打印当前的成绩信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }
    }
}
