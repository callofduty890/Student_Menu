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
        //创建list对象
        List<Student> list = null;

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
            #region 验证数据
            //判断有无输入学号信息
            if (this.txtStudentId.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入学号", "提示信息");
                this.txtStudentId.Focus();
                return;
            }
            //判断学号是否是整数
            if (!Common.DataValidate.IsInteger(this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("学号必须是正整数", "提示信息");
                this.txtStudentId.Focus();
                return;
            }
            #endregion

            #region 输入数据接收返回结果
            Student objstudent = objstudentService.GetStudentById(this.txtStudentId.Text.Trim());
            #endregion

            #region UI界面响应
            if (objstudent==null)
            {
                MessageBox.Show("学员信息不存在", "提示信息");
                this.txtStudentId.Focus();
            }
            else
            {
                //创建新窗体显示
                FrmStudentInfo objstudenInfo = new FrmStudentInfo(objstudent);
                objstudenInfo.Show();
            }
            #endregion
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
            #region 验证数据
            //索引为0没有任何要修改的学员信息
            if (this.dgvStudentList.RowCount==0)
            {
                MessageBox.Show("没有任何需要修改的学员信息！", "提示");
                return;
            }
            //没有选中
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的学员信息！", "提示");
                return;
            }
            #endregion
            //获取对应学号-发起请求
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //通过学生的ID获取信息
            Student objstudent = objstudentService.GetStudentById(studentId);
            //显示修改学员信息窗口
            FrmEditStudent objFrmEditStudent = new FrmEditStudent(objstudent);
            objFrmEditStudent.ShowDialog();
            //同步显示刷新
            btnQuery_Click(null, null);
        }
        //删除学员对象
        private void btnDel_Click(object sender, EventArgs e)
        {
            #region 验证数据
            //索引为0没有任何要修改的学员信息
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("没有任何需要删除的学员信息！", "提示");
                return;
            }
            //没有选中
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的学员信息！", "提示");
                return;
            }
            //确认是否真的删除
            DialogResult result = MessageBox.Show("确认要删除吗?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            #endregion
            //获取对应学号
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //执行删除操作
            if (objstudentService.DelectStudentById(studentId) == 1)
            {
                //同步刷新一下
                btnQuery_Click(null, null);
            }
            else
            {
                MessageBox.Show("删除失败", "提示");
            }
            ;
            
        }



        //姓名降序
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
            //数据验证
            if (this.dgvStudentList.RowCount==0)
            {
                return;
            }

            //调用list排序
            list.Sort(new NameDESC());
            //刷新显示
            this.dgvStudentList.Refresh();
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

    #region 实现排序
    class NameDESC : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return y.StudentName.CompareTo(x.StudentName);
        }
    }
    class StuIdDESC : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return y.StudentId.CompareTo(x.StudentId);
        }
    }
    #endregion
}