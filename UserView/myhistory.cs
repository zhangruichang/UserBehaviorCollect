using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccess;
namespace UserView
{
    public partial class myhistory : Form
    {
        const int maxnum = 100000;
        public static string userid;
        private static myhistory OnlyForm { get; set; }
        DataTable dt1 = new DataTable("computetime");
        public myhistory(string u)
        {
            userid = u;
            InitializeComponent();
            datagridview2.AutoGenerateColumns = true;
            datagridview2.AllowUserToAddRows = false;
            DataColumn column1;
            column1 = new DataColumn("datetime", Type.GetType("System.String"));
            column1.Caption = "datetime";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("url", Type.GetType("System.String"));
            column1.Caption = "url";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("totaltime", Type.GetType("System.String"));
            column1.Caption = "totaltime";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("efftime", Type.GetType("System.String"));
            column1.Caption = "efftime";
            dt1.Columns.Add(column1);
            column1 = new DataColumn("effrate", Type.GetType("System.Double"));
            column1.Caption = "effrate";
            dt1.Columns.Add(column1);
        }
        public static void ShowForm()
        {
            if (OnlyForm == null)
            {
                myhistory Fm = new myhistory(userid);
                OnlyForm = Fm;
                Fm.Show();
            }
            OnlyForm.Activate();
        }
        private void myhistory_Load(object sender, EventArgs e)
        {
            string sql = "select * from useractions where UserID='" + userid + "'";
            DataSet ds1 = DB.getdatasetbysql(sql);
            this.datagridview2.DataSource = ds1.Tables[0];
        }
        public int cmp(string[] url, string x, int len)
        {
            int i, j;
            for (i = 0; i < len; i++)
            {
                if (url[i] == x)
                    return i;
            }
            return -1;
        }
        public double timespantoint(TimeSpan ts)
        {
            int hour = ts.Hours, minute = ts.Minutes, second = ts.Seconds;
            int totalsec = hour * 3600 + minute * 60 + second;
            return totalsec;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i, j, urli = 0, ret;
            double rate;
            string action, date = "";
            string[] url = new string[maxnum];
            string[] starttime = new string[maxnum];
            TimeSpan[] totaltime = new TimeSpan[maxnum];
            TimeSpan[] totalhidden = new TimeSpan[maxnum];
            string[] hidden = new string[maxnum];
            DataRow dr;
            for (i = 0; i < datagridview2.Rows.Count; i++)
            {
                action = datagridview2.Rows[i].Cells[1].Value.ToString();
                if (action == "open")
                {
                    url[urli] = datagridview2.Rows[i].Cells[2].Value.ToString();
                    starttime[urli] = datagridview2.Rows[i].Cells[3].Value.ToString();
                    date = starttime[urli];
                    //match[urli] = 0;
                    urli++;
                }
                else if (action == "close")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli)) != -1)
                    {
                        //match[ret] = 1;
                        totaltime[ret] = DateTime.Parse(datagridview2.Rows[i].Cells[3].Value.ToString()) - DateTime.Parse(starttime[ret]);
                        dr = dt1.NewRow();
                        dr["datetime"] = date;
                        dr["url"] = url[ret]; dr["totaltime"] = totaltime[ret];
                        dr["efftime"] = totaltime[ret] - totalhidden[ret];
                        rate = (double)timespantoint(totaltime[ret] - totalhidden[ret]) / timespantoint(totaltime[ret]);
                        dr["effrate"] = rate;
                        //effrate = Convert.ToString(rate);
                        dt1.Rows.Add(dr);
                    }
                }
                else if (action == "hidden")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli)) != -1)
                    {
                        hidden[ret] = datagridview2.Rows[i].Cells[3].Value.ToString();
                    }
                }
                else if (action == "active")
                {
                    if ((ret = cmp(url, datagridview2.Rows[i].Cells[2].Value.ToString(), urli)) != -1)
                    {
                        totalhidden[ret] += DateTime.Parse(datagridview2.Rows[i].Cells[3].Value.ToString()) - DateTime.Parse(hidden[ret]);
                        hidden[ret] = datagridview2.Rows[i].Cells[3].Value.ToString();
                    }
                }
            }
            urltime ut = new urltime(dt1);
            ut.Show();
        }
    }
}
