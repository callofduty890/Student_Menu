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

namespace StudentManager
{
    public partial class FrmEditStudent : Form
    {
        //ѧ���༶��
        StudentClassService objStudentClass = new StudentClassService();

        public FrmEditStudent()
        {
            InitializeComponent();
        }

        public FrmEditStudent(Student objStudent):this()
        {
            //��ʼ���༶������
            this.cboClassName.DataSource = objStudentClass.GetAllClasses();
            //������Ӧ��list
            this.cboClassName.ValueMember = "ClassId";
            this.cboClassName.DisplayMember = "ClassName";//�����������ı�

            //ѧ����Ϣ
            this.txtStudentId.Text = objStudent.StudentId.ToString();
            this.txtStudentIdNo.Text = objStudent.StudentIdNo;
            this.txtStudentName.Text = objStudent.StudentName;
            this.txtPhoneNumber.Text = objStudent.PhoneNumber;
            this.txtAddress.Text = objStudent.StudentAddress;
            if (objStudent.Gender == "��") this.rdoMale.Checked = true;
            else this.rdoFemale.Checked = true;
            this.cboClassName.Text = objStudent.ClassName;
            this.dtpBirthday.Text = objStudent.Birthday.ToShortDateString();
            this.txtCardNo.Text = objStudent.CardNo;
            //��ʾ��Ƭ
            this.pbStu.Image = Image.FromFile("default.png"); ;

        }

        //�ύ�޸�
        private void btnModify_Click(object sender, EventArgs e)
        {
            #region ��֤����
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ѡ����Ƭ
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            //OpenFileDialog objFileDialog = new OpenFileDialog();
            //DialogResult result = objFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //    this.pbStu.Image = Image.FromFile(objFileDialog.FileName);
        }
    }
}