namespace UserView
{
    partial class myhistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (OnlyForm != null)
            {
                OnlyForm = null;
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.datagridview2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).BeginInit();
            this.SuspendLayout();
            // 
            // datagridview2
            // 
            this.datagridview2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridview2.Location = new System.Drawing.Point(12, 12);
            this.datagridview2.Name = "datagridview2";
            this.datagridview2.RowTemplate.Height = 23;
            this.datagridview2.Size = new System.Drawing.Size(626, 376);
            this.datagridview2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(542, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "有效浏览时间";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // myhistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 436);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.datagridview2);
            this.Name = "myhistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History View";
            this.Load += new System.EventHandler(this.myhistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datagridview2;
        private System.Windows.Forms.Button button1;

    }
}