using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmImportData : Form
    {
        //定义全局变量
        private List<Student> list = null;

        public FrmImportData()
        {
            InitializeComponent();
        }
        //从外部导入Excel
        private void btnChoseExcel_Click(object sender, EventArgs e)
        {
            //打开文件
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult result = openFile.ShowDialog();
            //接收返回结果
            if (result==DialogResult.OK)
            {
                //拿到Excle文件路径
                string path = openFile.FileName;
                //拿到Excle的全部内容---[定义一个处理函数传入路径获取全部数据]
                list = new ImportDataFromExcel().GetStudentsByExcel(path);

                //显示数据
                this.dgvStudentList.DataSource = null;
                this.dgvStudentList.AutoGenerateColumns = false;
                this.dgvStudentList.DataSource = list;
                //显示调试风格
                new Common.DataGridViewStyle().DgvStyle3(this.dgvStudentList);
            }
        }
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
        //保存到数据库
        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            //验证数据
            if (list==null||list.Count==0)
            {
                MessageBox.Show("目前没有要导入的数据!", "导入提示");
            }
            //导入数据
            if (new ImportDataFromExcel().Import(this.list))
            {
                MessageBox.Show("数据导入成功!", "导入提示");
                this.dgvStudentList.DataSource = null;
                this.list.Clear();
            }
            else
            {
                MessageBox.Show("数据导入失败!", "导入提示");
            }
        }
    }
}

