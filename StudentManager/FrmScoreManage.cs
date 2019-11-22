using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DAL;
using Models;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        //ʵ�����ɼ���������
        ScoreListService objScoreListService = new ScoreListService();

        //ʵ����ѧ���༶��������
        StudentClassService objStudentClass = new StudentClassService();

        public FrmScoreManage()
        {
            InitializeComponent();
            //�Ͽ��¼�
            this.cboClass.SelectedIndexChanged -=
                  new EventHandler(this.cboClass_SelectedIndexChanged);

            //��ֹ�Զ�������
            this.dgvScoreList.AutoGenerateColumns = false;

            //��ʼ���༶������
            this.cboClass.DataSource = objStudentClass.GetAllClasses();
            //������Ӧ��list
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";//�����������ı�

            //���¼�
            this.cboClass.SelectedIndexChanged +=
                  new EventHandler(this.cboClass_SelectedIndexChanged);

        }     
        //���ݰ༶��ѯ      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //�ж�������ֵ
            if (this.cboClass.SelectedIndex==-1)
            {
                MessageBox.Show("������ѡ��Ҫ��ѯ�İ༶", "��ѯ��ʾ");
                return;
            }
            //���༶��ѯ
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList(this.cboClass.Text.Trim());
            //������ʾ��ʽ
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //ͬ����ʾ�༶������Ϣ
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]���Գɼ�ͳ��";

            //���ܲ�ѯ���
            Dictionary<string, string> dic =objScoreListService.GetScoreInfoByClassId(this.cboClass.SelectedValue.ToString());

            //����ȡֵ
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //��ʾȱ����Ա����
            List<string> list = objScoreListService.GetAbsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ͳ��ȫУ���Գɼ�
        private void btnStat_Click(object sender, EventArgs e)
        {
            //��ѯȫ���Ŀ��Գɼ�
            this.dgvScoreList.DataSource = objScoreListService.GetScoreList("");
            //������ʾ��ʽ
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //��ʾ�������
            Dictionary<string, string> dic = objScoreListService.GetScoreInfo();
            //��ʾ������Ϣ
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //��ʾȱ������
            List<string> list = objScoreListService.GetAbsetList();
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }


        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //ѡ���ѡ��ı䴦��
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
         
        }

       
    }
}