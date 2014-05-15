using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserView
{
    public partial class urltime : Form
    {
        DataTable dt;
        public urltime(DataTable dt1)
        {
            dt=dt1;
            InitializeComponent();
        }

        private void urltime_Load(object sender, EventArgs e)
        {
            dgvurltime.AutoGenerateColumns = true;
            //dgvtime.DataMember = "computetime";
            dgvurltime.DataSource = dt;
        }

    }
}
