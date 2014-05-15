using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySQLDriverCS;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using DataAccess;

namespace UserView
{
    
    public partial class Form1 : Form
    {
        public string userid,user;
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        wait wt;
        int login = 0;
        private BackgroundWorker worker = new BackgroundWorker();
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Thread th = new Thread(new ThreadStart(DoSplash));
            th.Start();
            Thread.Sleep(3000);
            th.Abort();
            Thread.Sleep(1000);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
        }
        private void DoSplash()
        {
            splash sp = new splash();
            sp.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            login = 0;
        }
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            user = textBox1.Text.Trim();
            //userid ="63";
            string sql = "select * from userinfo where code= '" + user + "'";
            DataSet ds = DB.getdatasetbysql(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("用户名不存在", "错误", MessageBoxButtons.OK);
                wt.Close();
                worker.CancelAsync();
                this.Activate();
                this.Show();
            }
            else if (ds.Tables[0].Rows[0]["password"].ToString() == textBox2.Text.Trim())
            {
                userid = ds.Tables[0].Rows[0]["id"].ToString();
                login = 1;
            }
            else
            {
                MessageBox.Show("密码错误", "错误", MessageBoxButtons.OK);
                wt.Close();
                worker.CancelAsync();
                this.Activate();
                this.Show();
            }
        }
        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (login == 1)
            {
                wt.Close();
                DateTime dt = DateTime.Now;
                Form2 fr = new Form2(userid, dt,user);
                fr.Show();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            wt = new wait("正在登陆...");
            wt.Show();
            worker.RunWorkerAsync();
            
            wt.ShowInTaskbar = false;
            //button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://202.121.197.75/UM/regist.action");
        }
    }
}
