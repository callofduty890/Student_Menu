using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmStudentManage : Form
    {
        //实例化学生班级操作对象
        StudentClassService objStudentClass = new StudentClassService();
        //实例化学生的操作对象
        StudentService objstudentService = new StudentService();
       


        public FrmStudentManage()
        {
            InitializeComponent();
            //初始化班级下拉框
            this.cboClass.DataSource = objStudentClass.GetAllClasses();
            //调整对应的list
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";//设置下拉框文本
            //不显示未封装属性
            this.dgvStudentList.AutoGenerateColumns = false;
        }
        //按照班级查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region 验证数据
            if (this.cboClass.SelectedIndex==-1)
            {
                MessageBox.Show("请选则班级!", "提示");
                return;
            }
            #endregion

            #region 执行数据查询
            //查询数据并绑定显示
            this.dgvStudentList.DataSource = objstudentService.GetStudentByClass(this.cboClass.Text);
            #endregion

            //界面美化
            new Common.DataGridViewStyle().DgvStyle1(this.dgvStudentList);

        }
        //根据学号查询
        private void btnQueryById_Click(object sender, EventArgs e)
        {
          
        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //双击选中的学员对象并显示详细信息
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //修改学员对象
        private void btnEidt_Click(object sender, EventArgs e)
        {
          
        }
        //删除学员对象
        private void btnDel_Click(object sender, EventArgs e)
        {
           
        }
        //姓名降序
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
         
        }
        //学号降序
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
         
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        
        }
        //打印当前学员信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //导出到Excel
        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }

   
}