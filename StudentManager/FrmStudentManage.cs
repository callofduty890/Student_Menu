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
        //ʵ����ѧ���༶��������
        StudentClassService objStudentClass = new StudentClassService();
        //ʵ����ѧ���Ĳ�������
        StudentService objstudentService = new StudentService();
        //����list����
        List<Student> list = null;

        public FrmStudentManage()
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClass.DataSource = objStudentClass.GetAllClasses();
            //������Ӧ��list
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";//�����������ı�
            //����ʾδ��װ����
            this.dgvStudentList.AutoGenerateColumns = false;
        }
        //���հ༶��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region ��֤����
            if (this.cboClass.SelectedIndex==-1)
            {
                MessageBox.Show("��ѡ��༶!", "��ʾ");
                return;
            }
            #endregion

            #region ִ�����ݲ�ѯ
            //��ѯ���ݲ�����ʾ
            this.dgvStudentList.DataSource = objstudentService.GetStudentByClass(this.cboClass.Text);
            #endregion

            //��������
            new Common.DataGridViewStyle().DgvStyle1(this.dgvStudentList);

        }
        //����ѧ�Ų�ѯ
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            #region ��֤����
            //�ж���������ѧ����Ϣ
            if (this.txtStudentId.Text.Trim().Length==0)
            {
                MessageBox.Show("������ѧ��", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }
            //�ж�ѧ���Ƿ�������
            if (!Common.DataValidate.IsInteger(this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("ѧ�ű�����������", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }
            #endregion

            #region �������ݽ��շ��ؽ��
            Student objstudent = objstudentService.GetStudentById(this.txtStudentId.Text.Trim());
            #endregion

            #region UI������Ӧ
            if (objstudent==null)
            {
                MessageBox.Show("ѧԱ��Ϣ������", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
            }
            else
            {
                //�����´�����ʾ
                FrmStudentInfo objstudenInfo = new FrmStudentInfo(objstudent);
                objstudenInfo.Show();
            }
            #endregion
        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //˫��ѡ�е�ѧԱ������ʾ��ϸ��Ϣ
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //�޸�ѧԱ����
        private void btnEidt_Click(object sender, EventArgs e)
        {
            #region ��֤����
            //����Ϊ0û���κ�Ҫ�޸ĵ�ѧԱ��Ϣ
            if (this.dgvStudentList.RowCount==0)
            {
                MessageBox.Show("û���κ���Ҫ�޸ĵ�ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            //û��ѡ��
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ����Ҫ�޸ĵ�ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            #endregion
            //��ȡ��Ӧѧ��-��������
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //ͨ��ѧ����ID��ȡ��Ϣ
            Student objstudent = objstudentService.GetStudentById(studentId);
            //��ʾ�޸�ѧԱ��Ϣ����
            FrmEditStudent objFrmEditStudent = new FrmEditStudent(objstudent);
            objFrmEditStudent.ShowDialog();
            //ͬ����ʾˢ��
            btnQuery_Click(null, null);
        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
            #region ��֤����
            //����Ϊ0û���κ�Ҫ�޸ĵ�ѧԱ��Ϣ
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û���κ���Ҫɾ����ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            //û��ѡ��
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ����Ҫɾ����ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            //ȷ���Ƿ����ɾ��
            DialogResult result = MessageBox.Show("ȷ��Ҫɾ����?", "ɾ��ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            #endregion
            //��ȡ��Ӧѧ��
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
            //ִ��ɾ������
            if (objstudentService.DelectStudentById(studentId) == 1)
            {
                //ͬ��ˢ��һ��
                btnQuery_Click(null, null);
            }
            else
            {
                MessageBox.Show("ɾ��ʧ��", "��ʾ");
            }
            ;
            
        }



        //��������
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
            //������֤
            if (this.dgvStudentList.RowCount==0)
            {
                return;
            }

            //����list����
            list.Sort(new NameDESC());
            //ˢ����ʾ
            this.dgvStudentList.Refresh();
        }


        //ѧ�Ž���
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
         
        }
        //����к�
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        
        }
        //��ӡ��ǰѧԱ��Ϣ
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //������Excel
        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }

    #region ʵ������
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