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
using ShareMemLib;

namespace UserView
{
    public partial class Form2 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        public string userid;
        public DateTime dt1;
        public int mode,open;
        int d = 0;
        private BackgroundWorker worker = new BackgroundWorker();
        wait wt; mycurrent mc; myhistory mh;
        public Form2(string a,DateTime DT,string user)
        {
            userid = a;
            dt1 = DT;
            InitializeComponent();
            //comboBox1.SelectedItem = "本次数据(已插入)";
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
            
        }
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            if(d==1)
                mh = new myhistory(userid);
            else if(d==2)
                mc = new mycurrent(userid, dt1);
        }
        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            wt.Close();
            if(d==1)
                myhistory.ShowForm();
            else if(d==2)
                mc.Show();
        }
        public Form2()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName == "moniter")
                    {
                        p.Kill();
                        MessageBox.Show("旧moniter.exe进程已关闭");
                    }
                    if (p.ProcessName == "oldmoniter")
                    {
                        p.Kill();
                        MessageBox.Show("旧moniter.exe进程已关闭");
                    }
                }
                this.Close();
                Application.Exit();
                return;
            }
            base.WndProc(ref m);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.OnLoad(null);
        }
        public bool close()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "moniter")
                {
                    p.Kill();
                    MessageBox.Show("旧moniter.exe进程已关闭");
                    return true;
                }
                if (p.ProcessName == "oldmoniter")
                {
                    p.Kill();
                    MessageBox.Show("旧moniter.exe进程已关闭");
                    return true;
                }
            }
            return false;
        }
        private void radiomyhistory_CheckedChanged(object sender, EventArgs e)
        {
            if (radiomyhistory.Checked == true)
            {
                if (rdoopen.Checked == true)
                {
                    d = 1;
                    close();
                    wt = new wait("正在打开,请稍候...");
                    wt.Show();
                    worker.RunWorkerAsync();
                    wt.ShowInTaskbar = false;
                }
                else
                {
                    MessageBox.Show("先打开监控器开关");
                    radiomyhistory.Checked = false;
                }
            }
        }
        private void radiomycurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (radiomycurrent.Checked == true)
            {
                if (rdoopen.Checked == true)
                {
                    d = 2;
                    close();
                    WinExec(@"""C:\Program Files\SHUCS\UserBehaviorCollect\oldmoniter.exe"" " + userid, 3);
                    MessageBox.Show("新的moniter.exe进程已启动");
                    //wt = new wait("正在打开,请稍候...");
                    //wt.Show();
                    //worker.RunWorkerAsync();
                    //wt.ShowInTaskbar = false;
                }
                else
                {
                    MessageBox.Show("先打开监控器开关");
                    radiomycurrent.Checked = false;
                }
            }
        }
        private void radiocurrentprivacy_CheckedChanged(object sender, EventArgs e)
        {
            if (radiocurrentprivacy.Checked == true)
            {
                if (rdoopen.Checked == true)
                {
                    close();
                    WinExec(@"""C:\Program Files\SHUCS\UserBehaviorCollect\moniter.exe"" " + userid, 3);
                    MessageBox.Show("新的moniter.exe进程已启动");
                    currentprivacy cp = new currentprivacy(userid);
                    cp.Show();
                }
                else
                {
                    MessageBox.Show("先打开监控器开关");
                    radiocurrentprivacy.Checked = false;
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e) 
        {
            Graphics g = this.CreateGraphics();
            g.DrawLine(new Pen(Color.Black, 0.00001f), new Point(0, 230), new Point(272, 230));
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("默认选择自动监控模式");
            //Thread.Sleep(3000);
            rdoopen.Checked = true;
            radiomycurrent.Checked = true;
        }
    }
}
