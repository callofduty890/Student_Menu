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
        //ѧ���༶��
        StudentClassService objStudentClass = new StudentClassService();
        //ѧ����
        StudentService objStudentService = new StudentService();

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
            //ѧ������
            if (this.txtStudentName.Text.Trim().Length==0)
            {
                MessageBox.Show("ѧ����������Ϊ�գ�", "��ʾ��Ϣ");
                this.txtStudentName.Focus();
                return;
            }
            //ѧ���Ա�
            if (!this.rdoFemale.Checked && !this.rdoMale.Checked)
            {
                MessageBox.Show("��ѡ��ѧ���Ա�", "��ʾ��Ϣ");
                return;
            }
            //ѧ���༶
            if (this.cboClassName.SelectedIndex==-1)
            {
                MessageBox.Show("��ѡ��ѧ���༶", "��ʾ��Ϣ");
                return;
            }
            //��֤����
            int age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year;
            if (age > 45 && age < 18)
            {
                MessageBox.Show("���������18-45֮��!", "��ʾ��Ϣ");
                return;
            }
            //��֤���֤�Ƿ����Ҫ��
            if (!Common.DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤���벻����Ҫ��!", "��ʾ��Ϣ");
                this.txtStudentIdNo.Focus();
                return;
            }
            //��֤���֤��������������ĳ��������Ƿ���ͬ
            if (!this.txtStudentIdNo.Text.Trim().Contains(this.dtpBirthday.Value.ToString("yyyMMdd")))
            {
                MessageBox.Show("���֤�ͳ������ڲ�ƥ��!", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤���֤�����Ƿ��Ѿ������ݿ��г���
            if (objStudentService.IsIdNoExisted(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤����������ѧԱ���֤���ظ�!", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤���ڿ����Ƿ��Ѿ������ݿ��г���
            if (objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            {
                MessageBox.Show("��ǰ���ڿ����Ѵ���!", "��֤��ʾ");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            #endregion

            #region ʵ����ѧ����Ϣ
            Student objstudent = new Student()
            {
                StudentId = Convert.ToInt32(this.txtStudentId.Text.Trim()),
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "��" : "Ů",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                ClassName = this.cboClassName.Text,
                StudentAddress = this.txtAddress.Text.Trim() == "" ? "��ַ����" : this.txtAddress.Text.Trim(),
                CardNo = this.txtCardNo.Text.Trim(),
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),//��ȡѡ��༶��ӦclassId
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                StuImage = this.pbStu.Image != null ? new SerializeObjectToString().SerializeObject(this.pbStu.Image) : ""
            };
            #endregion

            #region ������������UI��Ӧ
            if (objStudentService.ModifyStudent(objstudent)==1)
            {
                MessageBox.Show("ѧԱ��Ϣ�޸ĳɹ�", "��ʾ��Ϣ");
                this.Close();
            }
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