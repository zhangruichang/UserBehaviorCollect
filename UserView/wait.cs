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
    public partial class wait : Form
    {
        string text;
        public wait(string x)
        {
            text = x;
            InitializeComponent();
        }

        private void wait_Load(object sender, EventArgs e)
        {
            label1.Text = text;
        }
    }
}
