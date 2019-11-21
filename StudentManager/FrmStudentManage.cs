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
          
        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
           
        }
        //��������
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
         
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

   
}