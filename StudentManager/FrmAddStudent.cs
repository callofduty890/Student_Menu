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
        //�����ѧԱ
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region ��֤����
            //��������Ϊ��
            if (this.txtStudentName.Text.Trim().Length==0)
            {
                MessageBox.Show("ѧ����������Ϊ��!", "��ʾ��Ϣ");
                this.txtStudentName.Focus();
                return;
            }
            //���ڿ���
            if (this.txtCardNo.Text.Trim().Length==0)
            {
                MessageBox.Show("ѧ�����ڿ��Ų���Ϊ��!", "��ʾ��Ϣ");
                this.txtStudentName.Focus();
                return;
            }
            //�Ա�
            if (!this.rdoFemale.Checked && !this.rdoMale.Checked)
            {
                MessageBox.Show("��ѡ��ѧ���Ա�!", "��ʾ��Ϣ");
                return;
            }
            //�༶
            if (this.cboClassName.SelectedIndex==-1)
            {
                MessageBox.Show("��ѡ��༶!", "��ʾ��Ϣ");
                return;
            }
            //��֤����
            int age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year;
            if (age>45 && age<18)
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
            //��֤���ڿ����Ƿ��Ѿ������ݿ��г���
            #endregion
        }
        //�رմ���
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmAddStudent_KeyDown(object sender, KeyEventArgs e)
        {
       

        }
        //ѡ������Ƭ
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            //���ļ�·��
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            //���ܽ��
            if (result==DialogResult.OK)
            {
                //��ͼƬ��ʾ���ؼ���
                this.pbStu.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        //��������ͷ
        private void btnStartVideo_Click(object sender, EventArgs e)
        {
         
        }
        //����
        private void btnTake_Click(object sender, EventArgs e)
        {
        
        }
        //�����Ƭ
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.pbStu.Image = null;
        }

     
    }
}