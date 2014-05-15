using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareMemLib;
using DataAccess;
using MySQLDriverCS;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using urlcontent;
struct mydata
{
    public int flag;
    public string action;//12
    public string url;//300
    public string time;//128
    public int userid;
    public byte[] ToByte()
    {
        byte[] tp1 = BitConverter.GetBytes(flag);
        byte[] tp2 = new byte[12];
        byte[] tp3 = new byte[300];
        byte[] tp4 = new byte[128];
        byte[] tp5 = BitConverter.GetBytes(userid);
        int l1, l2, l3, l4, l5;
        l1 = tp1.Length; l2 = tp2.Length; l3 = tp3.Length; l4 = tp4.Length; l5 = tp5.Length;
        byte[] tp = new byte[l1 + l2 + l3 + l4 + l5];
        Array.Copy(tp1, tp, l1);
        Array.Copy(tp2, 0, tp, l1, l2);
        Array.Copy(tp3, 0, tp, l1 + l2, l3);
        Array.Copy(tp4, 0, tp, l1 + l2 + l3, l4);
        Array.Copy(tp5, 0, tp, l1 + l2 + l3 + l4, l5);
        return tp;
    }
    public void Get(byte[] bytData)
    {
        flag = BitConverter.ToInt32(bytData, 0);
        action = System.Text.Encoding.ASCII.GetString(bytData, 4, 12);
        url = System.Text.Encoding.ASCII.GetString(bytData, 16, 300);
        time = System.Text.Encoding.ASCII.GetString(bytData, 316, 128);
        userid = BitConverter.ToInt32(bytData, 444);
    }
};
namespace UserView
{
    public partial class currentprivacy : Form
    {
        public string urlcontent;
        ShareMem MemDB = new ShareMem(); sequence sq;
        int datasize = 448; int isseqi = 0; int[] isseq = new int[maxnum]; int[] seq = new int[maxnum]; int[,] num = new int[maxnum, maxnum];
        string userid;
        int mode = 0;
        const int maxnum = 500;
        wait wt;
        DataTable dt = new DataTable("useraction");
        DataTable dt1 = new DataTable("computetime");
        DataSet ds = new DataSet("actions");
        public const int USER = 0x0400;
        public const int MYMESSAGE = USER + 1;
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker worker1 = new BackgroundWorker();
        public currentprivacy(string x)
        {
            userid = x;
            if (MemDB.Init("MyInfo", datasize) != 0)
            {
                //初始化失败
                MessageBox.Show("初始化失败");
                Dispose();
            }
            InitializeComponent();
            DataColumn column;
            column = new DataColumn("action", Type.GetType("System.String"));
            column.Caption = "action";
            column.ReadOnly = true;
            dt.Columns.Add(column);
            column = new DataColumn("url", Type.GetType("System.String"));
            column.Caption = "url";
            column.ReadOnly = true;
            dt.Columns.Add(column);
            column = new DataColumn("time", Type.GetType("System.String"));
            column.Caption = "time";
            column.ReadOnly = true;
            dt.Columns.Add(column);
            column = new DataColumn("userid", Type.GetType("System.Int32"));
            column.Caption = "userid";
            column.ReadOnly = true;
            dt.Columns.Add(column);
            //ds.Tables.Add(dt);
            //BindingSource bds = new BindingSource();
            //bds.DataSource = ds;
            datagridview2.AutoGenerateColumns = true;
            //datagridview2.DataMember = "useraction";
            datagridview2.DataSource = dt;
            datagridview2.AllowUserToAddRows = false;
            if (column.ReadOnly == true)
                column.ReadOnly = false;

            DataColumn column1;
            column1 = new DataColumn("uid", Type.GetType("System.Int32"));
            column1.Caption = "uid";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("title", Type.GetType("System.String"));
            column1.Caption = "title";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("content", Type.GetType("System.String"));
            column1.Caption = "content";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("url", Type.GetType("System.String"));
            column1.Caption = "url";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("opentime", Type.GetType("System.String"));
            column1.Caption = "opentime";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("closetime", Type.GetType("System.String"));
            column1.Caption = "closetime";
            dt1.Columns.Add(column1);

            //column1 = new DataColumn("totaltime", Type.GetType("System.String"));
            //column1.Caption = "totaltime";
            //dt1.Columns.Add(column1);
            column1 = new DataColumn("efftime", Type.GetType("System.String"));
            column1.Caption = "efftime";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("cps", Type.GetType("System.String"));
            column1.Caption = "cps";
            dt1.Columns.Add(column1);


            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
            worker1.WorkerReportsProgress = true;
            worker1.WorkerSupportsCancellation = true;
            worker1.DoWork += new DoWorkEventHandler(DoWork1);
            worker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork1);
        }
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            int i, j,m,n,htmllen,htmlrow,start,sublen,textcount=0;
            string[] html = new string[10000];
            //for (i = 0; i < 100; i++)
            //    html[i] = new string[1000];
            string time, action, url;
            int k = 0, count = datagridview2.Rows.Count;
            int[] delete = new int[count];
            bool selected;
            MySQLConnection conn = new MySQLConnection(DB.connectionString);
            conn.Open();
            MySQLCommand commn;
            for (i = 0; i < count; i++)
            {
                selected = false;
                if (datagridview2.Rows[i].Cells[0].Value != null && datagridview2.Rows[i].Cells[0].Value.ToString() == "1")
                    selected = true;
                if (selected == true)
                {
                    time = datagridview2.Rows[i].Cells[3].Value.ToString();
                    action = datagridview2.Rows[i].Cells[1].Value.ToString();
                    url = datagridview2.Rows[i].Cells[2].Value.ToString();
                    urlcontent = GetWebContent(url);
                    htmllen=urlcontent.Length;
                    start = 0; htmlrow = 0; 
                    for (m= 0;m< htmllen-1; m++)
                    {
                        if (urlcontent[m] == '\r' && urlcontent[m + 1] == '\n')
                        {
                            sublen= m - start;
                            html[htmlrow] = urlcontent.Substring(start,sublen);
                            htmlrow++;
                            start = m + 2;
                        }
                    }
                    for (m= 0; m< htmlrow; m++)
                    {
                        sublen=html[m].Length;
                        for (n= 0; n< sublen; n++)
                        {
                            if(html[m][n]=='a')
                                textcount++;
                        }
                    }


                    j = 0;
                    string sql = "insert into useractions values(" + j + ",'" + action + "','" + url + "','" + time + "'," + datagridview2.Rows[i].Cells[4].Value + ")";
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
                dt.Rows[delete[i]].Delete();
            }
            datagridview2.Update();
        }
        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("插入成功", "提示", MessageBoxButtons.OK);
            wt.Close();
        }
        public void DoWork1(object sender, DoWorkEventArgs e)
        {
            int i, j, urli = 0, ret, copy = 0, print = 0, save = 0, ret1, starti = 0, sequrli = 0;

            bool[] enable = new bool[maxnum];


            for (i = 0; i < maxnum; i++)
            {
                enable[i] = false;
                seq[i] = 0;
            }
            double rate;
            //bool enable = false;
            string action, date = "", thisaction, lastaction;
            DateTime opentime, closetime, thistime, lasttime;
            string[] url = new string[maxnum];
            string[] starttime = new string[maxnum];
            TimeSpan[] totaltime = new TimeSpan[maxnum];
            TimeSpan[] totalhidden = new TimeSpan[maxnum];
            string[] hidden = new string[maxnum];
            //int[] match = new int[100];
            //for (i = 0; i < 100; i++)
            //    match[i] = -1;
            //string endtime;
            //TimeSpan Totaltime;
            DataRow dr;
            for (i = 0; i < datagridview2.Rows.Count; i++)
            {
                action = datagridview2.Rows[i].Cells[1].Value.ToString();
                if (action == "open")
                {
                    starti = i;
                    url[urli] = datagridview2.Rows[i].Cells[2].Value.ToString();
                    starttime[urli] = datagridview2.Rows[i].Cells[3].Value.ToString();
                    date = starttime[urli];
                    //match[urli] = 0;
                    num[urli, seq[urli]] = i;
                    seq[urli]++;
                    urli++;
                }
                else if (action == "close")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli,enable)) != -1)
                    {
                        //match[ret] = 1;
                        enable[ret] = true;
                        num[ret, seq[ret]] = i;
                        seq[ret]++;

                        opentime = DateTime.Parse(starttime[ret]);
                        closetime = DateTime.Parse(datagridview2.Rows[i].Cells[3].Value.ToString());
                        totaltime[ret] = closetime - opentime;
                        geturlcontent.GetText(url[ret]);
                        dr = dt1.NewRow();
                        dr["uid"] = userid;
                        dr["title"] = geturlcontent.Title;
                        dr["content"] = geturlcontent.Content;
                        //dr["datetime"] = date;
                        dr["url"] = url[ret];
                        dr["opentime"] = opentime; dr["closetime"] = closetime; //dr["totaltime"] = totaltime[ret];
                        dr["efftime"] = (totaltime[ret] - totalhidden[ret]).TotalSeconds;
                        rate = (double)timespantoint(totaltime[ret] - totalhidden[ret]) / timespantoint(totaltime[ret]);
                        dr["cps"] = copy * 1 + save * 2 + print * 4;
                        //dr["effrate"] = rate;
                        //effrate = Convert.ToString(rate);
                        dt1.Rows.Add(dr);
                        copy = save = print = 0;
                    }
                }
                else if (action == "hidden")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli,enable)) != -1)
                    {
                        hidden[ret] = datagridview2.Rows[i].Cells[3].Value.ToString();
                        num[ret, seq[ret]] = i;
                        seq[ret]++;
                    }
                }
                else if (action == "active")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli,enable)) != -1)
                    {
                        totalhidden[ret] += DateTime.Parse(datagridview2.Rows[i].Cells[3].Value.ToString()) - DateTime.Parse(hidden[ret]);
                        hidden[ret] = datagridview2.Rows[i].Cells[3].Value.ToString();
                        num[ret, seq[ret]] = i;
                        seq[ret]++;
                    }
                }
                else
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli,enable)) != -1)
                    {
                        num[ret, seq[ret]] = i;
                        seq[ret]++;
                    }
                    if (action == "copy")
                        copy = 1;
                    else if (action == "Printed")
                        print = 1;
                    else if (action == "Saved")
                        save = 1;
                }
                if (i >= 1)
                {
                    thistime = DateTime.Parse(datagridview2.Rows[i].Cells[3].Value.ToString());
                    lasttime = DateTime.Parse(datagridview2.Rows[i - 1].Cells[3].Value.ToString());
                    thisaction = datagridview2.Rows[i].Cells[1].Value.ToString();
                    lastaction = datagridview2.Rows[i - 1].Cells[1].Value.ToString();
                    ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli,enable);
                    ret1 = cmp(url, datagridview2.Rows[i - 1].Cells[2].Value.ToString(), urli,enable);
                    if ((thisaction != "active" || lastaction != "hidden") && beyond(thistime, lasttime) && ret != -1 && ret == ret1)
                    {
                        totalhidden[ret] += (thistime - lasttime);
                    }
                }

            }

            for (i = 0; i < urli; i++)
            {
                if (enable[i] == true)
                    isseq[isseqi++] = i;
            }

            sq = new sequence(dt1);
            sq.MyEvent += new MyDelegate(sq_MyEvent);
        }
        public void CompleteWork1(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("序列已生成", "提示", MessageBoxButtons.OK);
            wt.Close();
            sq.Show();
        }
        protected override void DefWndProc(ref Message m)
        {

            switch (m.Msg)
            {
                //接收自定义消息MYMESSAGE，并显示其参数
                case MYMESSAGE:
                    read();
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }

        }
        public void read()
        {
            
            byte[] bytData = new byte[datasize];
            int intRet = MemDB.Read(ref bytData, 0, datasize);
            string Url,Action,Time;
            if (intRet == 0)
            {
                mydata md = new mydata();
                md.Get(bytData);
                Url=md.url.Substring(0, md.url.IndexOf('\0'));
                Action = md.action.Substring(0, md.action.IndexOf('\0'));
                Time = md.time.Substring(0, md.time.IndexOf('\0'));
                DataRow dr;
                dr = dt.NewRow();
                dr["action"] = Action;
                dr["url"] = Url;
                //urlcontent = GetHtml(Url, "");
                
                //while(urlcontent1.Length)
                //sw.Write(urlcontent1);
                //sw.Close();
                dr["time"] = Time;
                dr["userid"] = userid;
                dt.Rows.Add(dr);
                md.flag = 0;
            }
            else
                MessageBox.Show("内存未初始化");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            wt = new wait("正在插入,请稍候...");
            wt.Show();
            worker.RunWorkerAsync();
            wt.ShowInTaskbar = false;
        }
        public int cmp(string[] url, string x,int len,bool[] enable)
        {
            int i,j;
            for (i = 0; i < len; i++)
            {
                if(url[i]==x&&enable[i]==false)
                    return i;
            }
            return -1;
        }
        private string GetWebContent(string sUrl)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                //声明一个HttpWebRequest请求
                request.Timeout = 3000000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ToString() != "")
                {
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("gb2312");
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    strResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                //MessageBox.Show("出错");
                MessageBox.Show(exp.Message);
            }
            return strResult;
        }
        public double timespantoint(TimeSpan ts)
        {
            int hour = ts.Hours, minute = ts.Minutes, second = ts.Seconds;
            int totalsec = hour * 3600 + minute * 60 + second;
            return totalsec;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            wt = new wait("正在生成序列,请稍候...");
            wt.Show();
            worker1.RunWorkerAsync();
            wt.ShowInTaskbar = false;
        }
        public void sq_MyEvent(int[] delete,int k)
        {
            int i, j,count,rowcount=datagridview2.Rows.Count,deletei=0;
            int[] deleterows=new int[rowcount];
            for (i = 0; i < k; i++)
            {
                count = seq[isseq[delete[i]]];
                for(j=0;j<count;j++)
                    deleterows[deletei++] = num[isseq[delete[i]], j];
            }
            Array.Sort(deleterows, 0, deletei);
            for (i = deletei-1; i >= 0;i-- )
                dt.Rows[deleterows[i]].Delete();
            
            datagridview2.Update();
        }
        public bool beyond(DateTime thi, DateTime tha)
        {
            TimeSpan ts = thi - tha;
            if (ts.TotalMinutes >= 1.0) return true;
            else return false;

        }
        public bool sameday(string date1, string date2)
        {
            DateTime dt1=DateTime.Parse(date1), dt2=DateTime.Parse(date2);
            if (dt1.Year==dt2.Year&&dt1.Month==dt2.Month&&dt1.Day == dt2.Day)
                return true;
            else return false;
        }
        //public void deleterows(int starti)
        //{
        //    bool selected;
        //    string u, date1, date2, action;
        //    int i, count = datagridview2.Rows.Count, num, j,k=0;
        //    int[] a = new int[count];
        //    int[] delete = new int[count];
        //        if (datagridview2.Rows[starti].Cells[1].Value.ToString() == "open")
        //        {
        //            datagridview2.Rows[starti].Cells[0].Value = "0";
        //            action = datagridview2.Rows[starti].Cells[1].Value.ToString();
        //            date1 = datagridview2.Rows[starti].Cells[3].Value.ToString();
        //            date2 = date1;
        //            i = starti + 1; num = 0;
        //            u = datagridview2.Rows[starti].Cells[2].Value.ToString();
        //            while (i < count && sameday(date1, date2))
        //            {
        //                if (datagridview2.Rows[i].Cells[2].Value.ToString() == u)
        //                {
        //                    action = datagridview2.Rows[i].Cells[1].Value.ToString();
        //                    if (action != "close")
        //                        a[num++] = i;
        //                    else
        //                    {
        //                        a[num++] = i;
        //                        datagridview2.Rows[starti].Cells[0].Value = "1";
        //                        for (j = 0; j < num; j++)
        //                        {
        //                            datagridview2.Rows[a[j]].Cells[0].Value = "1";
        //                        }
        //                        break;
        //                    }
        //                }
        //                i++;
        //                if (i == count) break;
        //                date2 = datagridview2.Rows[i].Cells[3].Value.ToString();
        //            }
        //        }
            
        //    for (i = 0; i < count; i++)
        //    {
        //        selected = false;
        //        if (datagridview2.Rows[i].Cells[0].Value != null && datagridview2.Rows[i].Cells[0].Value.ToString() == "1")
        //            selected = true;
        //        if (selected == true)
        //        {
        //            delete[k] = i;
        //            k++;
        //        }
        //     }
        //    for (i = k - 1; i >= 0; i--)
        //    {
        //        dt.Rows[delete[i]].Delete();
        //    }
        //    datagridview2.Update();
        //}
        private void datagridview2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string u,date1,date2,action;
            int i, count = datagridview2.Rows.Count,num,j;
            int[] a=new int[count];
            if (e.ColumnIndex == 0)
            {
                if (datagridview2.Rows[e.RowIndex].Cells[1].Value.ToString() == "open")
                {
                    datagridview2.Rows[e.RowIndex].Cells[0].Value = "0";
                    action = datagridview2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    date1=datagridview2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    date2=date1;
                    i=e.RowIndex+1;num=0;
                    u=datagridview2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    while (i<count&&sameday(date1, date2))
                    {
                        if(datagridview2.Rows[i].Cells[2].Value.ToString() == u)
                        {
                            action = datagridview2.Rows[i].Cells[1].Value.ToString();
                            if(action!="close")
                                a[num++]=i;
                            else 
                            {
                                a[num++]=i;
                                datagridview2.Rows[e.RowIndex].Cells[0].Value = "1";
                                for (j = 0; j < num; j++)
                                {
                                    datagridview2.Rows[a[j]].Cells[0].Value = "1";
                                }
                                break;
                            }
                        }
                        i++;
                        if (i == count) break;
                        date2 = datagridview2.Rows[i].Cells[3].Value.ToString();
                    }   
                }
                else
                    datagridview2.Rows[e.RowIndex].Cells[0].Value="0";
            }
        }
    }
}
