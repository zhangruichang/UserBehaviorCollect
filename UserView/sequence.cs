using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccess;
using MySQLDriverCS;
using urlcontent;
namespace UserView
{
    public delegate void MyDelegate(int[] delete,int k);
    public partial class sequence : Form
    {
        public event MyDelegate MyEvent;
        DataTable dt1;
        wait wt;
        private BackgroundWorker worker = new BackgroundWorker();
        
        public sequence(DataTable  dt)
        {
            InitializeComponent();
            dt1 = dt;
            dgvseq.DataSource = dt;
            dgvseq.AutoGenerateColumns = true;
            dgvseq.AllowUserToAddRows = false;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
        }
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            int i, j = 0, m, n, htmllen, htmlrow, start, sublen, cps, textcount = 0, uid, efftime;
            string time, action, url, opentime, closetime, now,title,content;
            double jacobi = 0.2;
            int k = 0, count = dgvseq.Rows.Count, copy, save, print;
            int[] delete = new int[count];
            bool[] selectedseq = new bool[count];
            bool selected;
            MySQLConnection conn = new MySQLConnection(DB.connectionString);
            conn.Open();
            MySQLCommand commn = new MySQLCommand("set names gb2312", conn);
            commn.ExecuteNonQuery();
            for (i = 0; i < count; i++)
            {
                selected = false;
                if (dgvseq.Rows[i].Cells[0].Value != null && dgvseq.Rows[i].Cells[0].Value.ToString() == "1")
                    selected = true;
                if (selected == true)
                {
                    selectedseq[i] = true;
                    uid = Convert.ToInt32(dgvseq.Rows[i].Cells[1].Value);
                    title = dgvseq.Rows[i].Cells[2].Value.ToString();
                    content = dgvseq.Rows[i].Cells[3].Value.ToString();
                    url = dgvseq.Rows[i].Cells[4].Value.ToString();
                    opentime = dgvseq.Rows[i].Cells[5].Value.ToString();
                    closetime = dgvseq.Rows[i].Cells[6].Value.ToString();
                    efftime = Convert.ToInt32(dgvseq.Rows[i].Cells[7].Value);
                    cps = Convert.ToInt32(dgvseq.Rows[i].Cells[8].Value);
                    print = cps / 4;
                    save = (cps - print * 4) / 2;
                    copy = cps - print * 4 - save * 2;

                    //urlcontent = GetWebContent(url);
                    //htmllen = urlcontent.Length;
                    //start = 0; htmlrow = 0;
                    //for (m = 0; m < htmllen - 1; m++)
                    //{
                    //    if (urlcontent[m] == '\r' && urlcontent[m + 1] == '\n')
                    //    {
                    //        sublen = m - start;
                    //        html[htmlrow] = urlcontent.Substring(start, sublen);
                    //        htmlrow++;
                    //        start = m + 2;
                    //    }
                    //}
                    //for (m = 0; m < htmlrow; m++)
                    //{
                    //    sublen = html[m].Length;
                    //    for (n = 0; n < sublen; n++)
                    //    {
                    //        if (html[m][n] == 'a')
                    //            textcount++;
                    //    }
                    //}
                    now = DateTime.Now.ToString();
                    string sql = "insert into sequence values(" + j + "," + uid + ",'" +title+ "','" +content+ "','" + url + "','" + opentime + "'," + j + ",'" + closetime + "','" + now + "'," + efftime + "," + copy + "," + print + "," + save + "," + jacobi + ")";
                    commn = new MySQLCommand(sql, conn);
                    if (commn.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("更新失败", "提示", MessageBoxButtons.OK);
                        return;
                    }
                    delete[k] = i;
                    k++;
                    //dt.Rows[i].Delete();
                    //datagridview2.Update();
                }
            }
            conn.Close();
            for (i = k - 1; i >= 0; i--)
            {
                dt1.Rows[delete[i]].Delete();
            }
            dgvseq.Update();
            MyEvent(delete,k);
        }
        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("插入成功", "提示", MessageBoxButtons.OK);
            wt.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            wt = new wait("正在插入序列,请稍候...");
            wt.Show();
            worker.RunWorkerAsync();
            wt.ShowInTaskbar = false;
        }
    }
}
