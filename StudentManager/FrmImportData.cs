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

        }
    }
}

