using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace StudentManager
{
    public partial class FrmAddStudent : Form
    {


        public FrmAddStudent()
        {
            InitializeComponent();
      
        }
        //添加新学员
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region 验证数据
            //姓名不能为空
            if (this.txtStudentName.Text.Trim().Length==0)
            {
                MessageBox.Show("学生姓名不能为空!", "提示信息");
                this.txtStudentName.Focus();
                return;
            }
            //考勤卡号
            if (this.txtCardNo.Text.Trim().Length==0)
            {
                MessageBox.Show("学生考勤卡号不能为空!", "提示信息");
                this.txtStudentName.Focus();
                return;
            }
            //性别
            if (!this.rdoFemale.Checked && !this.rdoMale.Checked)
            {
                MessageBox.Show("请选择学生性别!", "提示信息");
                return;
            }
            //班级
            if (this.cboClassName.SelectedIndex==-1)
            {
                MessageBox.Show("请选择班级!", "提示信息");
                return;
            }
            //验证年龄
            int age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year;
            if (age>45 && age<18)
            {
                MessageBox.Show("年龄必须在18-45之间!", "提示信息");
                return;
            }
            //验证身份证是否符合要求
            if (!Common.DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("身份证号码不符合要求!", "提示信息");
                this.txtStudentIdNo.Focus();
                return;
            }
            //验证身份证出生日期与输入的出身日期是否相同
            if (!this.txtStudentIdNo.Text.Trim().Contains(this.dtpBirthday.Value.ToString("yyyMMdd")))
            {
                MessageBox.Show("身份证和出身日期不匹配!", "验证提示");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //验证身份证号码是否已经在数据库中出现
            //验证考勤卡号是否已经在数据库中出现
            #endregion
        }
        //关闭窗体
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmAddStudent_KeyDown(object sender, KeyEventArgs e)
        {
       

        }
        //选择新照片
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            //打开文件路径
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            //接受结果
            if (result==DialogResult.OK)
            {
                //将图片显示到控件上
                this.pbStu.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        //启动摄像头
        private void btnStartVideo_Click(object sender, EventArgs e)
        {
         
        }
        //拍照
        private void btnTake_Click(object sender, EventArgs e)
        {
        
        }
        //清除照片
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.pbStu.Image = null;
        }

     
    }
}