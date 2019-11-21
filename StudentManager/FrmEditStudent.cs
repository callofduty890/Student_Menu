using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DAL;
using Models;
using Common;

namespace StudentManager
{
    public partial class FrmEditStudent : Form
    {
        //学生班级类
        StudentClassService objStudentClass = new StudentClassService();
        //学生类
        StudentService objStudentService = new StudentService();

        public FrmEditStudent()
        {
            InitializeComponent();
        }

        public FrmEditStudent(Student objStudent):this()
        {
            //初始化班级下拉框
            this.cboClassName.DataSource = objStudentClass.GetAllClasses();
            //调整对应的list
            this.cboClassName.ValueMember = "ClassId";
            this.cboClassName.DisplayMember = "ClassName";//设置下拉框文本

            //学生信息
            this.txtStudentId.Text = objStudent.StudentId.ToString();
            this.txtStudentIdNo.Text = objStudent.StudentIdNo;
            this.txtStudentName.Text = objStudent.StudentName;
            this.txtPhoneNumber.Text = objStudent.PhoneNumber;
            this.txtAddress.Text = objStudent.StudentAddress;
            if (objStudent.Gender == "男") this.rdoMale.Checked = true;
            else this.rdoFemale.Checked = true;
            this.cboClassName.Text = objStudent.ClassName;
            this.dtpBirthday.Text = objStudent.Birthday.ToShortDateString();
            this.txtCardNo.Text = objStudent.CardNo;
            //显示照片
            this.pbStu.Image = Image.FromFile("default.png"); ;

        }

        //提交修改
        private void btnModify_Click(object sender, EventArgs e)
        {
            #region 验证数据
            //学生姓名
            if (this.txtStudentName.Text.Trim().Length==0)
            {
                MessageBox.Show("学生姓名不能为空！", "提示信息");
                this.txtStudentName.Focus();
                return;
            }
            //学生性别
            if (!this.rdoFemale.Checked && !this.rdoMale.Checked)
            {
                MessageBox.Show("请选择学生性别", "提示信息");
                return;
            }
            //学生班级
            if (this.cboClassName.SelectedIndex==-1)
            {
                MessageBox.Show("请选择学生班级", "提示信息");
                return;
            }
            //验证年龄
            int age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year;
            if (age > 45 && age < 18)
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
            if (objStudentService.IsIdNoExisted(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("身份证不能与现有学员身份证号重复!", "验证提示");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //验证考勤卡号是否已经在数据库中出现
            if (objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            {
                MessageBox.Show("当前考勤卡号已存在!", "验证提示");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            #endregion

            #region 实例化学生信息
            Student objstudent = new Student()
            {
                StudentId = Convert.ToInt32(this.txtStudentId.Text.Trim()),
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "男" : "女",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                ClassName = this.cboClassName.Text,
                StudentAddress = this.txtAddress.Text.Trim() == "" ? "地址不详" : this.txtAddress.Text.Trim(),
                CardNo = this.txtCardNo.Text.Trim(),
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),//获取选择班级对应classId
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                StuImage = this.pbStu.Image != null ? new SerializeObjectToString().SerializeObject(this.pbStu.Image) : ""
            };
            #endregion

            #region 发起数据请求并UI相应
            if (objStudentService.ModifyStudent(objstudent)==1)
            {
                MessageBox.Show("学员信息修改成功", "提示信息");
                this.Close();
            }
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //选择照片
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            //OpenFileDialog objFileDialog = new OpenFileDialog();
            //DialogResult result = objFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //    this.pbStu.Image = Image.FromFile(objFileDialog.FileName);
        }
    }
}