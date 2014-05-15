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
namespace UserView
{
    public partial class mycurrent : Form
    {
        private BackgroundWorker worker = new BackgroundWorker();
        public string userid;
        public DateTime now;
        public DataSet ds2;
        wait wt;
        int d=0;
        DataTable dt = new DataTable("useraction");
        public mycurrent(string x,DateTime dt1)
        {
            userid = x;
            now = dt1;
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
        }
        public void DoWork(object sender, DoWorkEventArgs e)
        {
            if (d == 1)
            {
                bool selected;
                int count = dgvcur.Rows.Count;
                int[] delete = new int[count];
                int i, k = 0;
                MySQLConnection conn = new MySQLConnection(DB.connectionString);
                conn.Open();
                MySQLCommand commn;
                for (i = 0; i < count; i++)
                {
                    selected = false;
                    if (dgvcur.Rows[i].Cells[0].Value != null && dgvcur.Rows[i].Cells[0].Value.ToString() == "1")
                        selected = true;
                    if (selected == false)
                    {
                        string sql = "delete from useractions where id='" + dgvcur.Rows[i].Cells[1].Value + "'";
                        commn = new MySQLCommand(sql, conn);
                        if (commn.ExecuteNonQuery() <= 0)
                        {
                            MessageBox.Show("更新失败", "提示", MessageBoxButtons.OK);
                            return;
                        }
                        delete[k] = i;
                        k++;
                    }

                }
                conn.Close();
                for (i = k - 1; i >= 0; i--)
                {
                    ds2.Tables[0].Rows[delete[i]].Delete();
                    //dt.Rows[delete[i]].Delete();
                }
                dgvcur.Update();
                ds2.Tables[0].AcceptChanges();
            }
            else if (d == 2)
                this.OnLoad(null);
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            wt.Close();
            if(d==1)
                MessageBox.Show("插入成功", "提示", MessageBoxButtons.OK);
        }
        private void mycurrent_Load(object sender, EventArgs e)
        {
            string sql1 = "select * from useractions where UserID='" + userid + "' and time>'" +now + "'";
            ds2 = DB.getdatasetbysql(sql1);
            dgvcur.DataSource = ds2.Tables[0];
            dgvcur.AllowUserToAddRows = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            d = 1;
            wt = new wait("正在插入,请稍候...");
            wt.Show();
            worker.RunWorkerAsync();
            wt.ShowInTaskbar = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            d = 2;
            wt = new wait("正在刷新,请稍候...");
            wt.Show();
            worker.RunWorkerAsync();
            wt.ShowInTaskbar = false;
            
        }

        private void dgvcur_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dgvcur.Rows[e.RowIndex].Cells[0].Value = "0";
            }
        }

    }
}
