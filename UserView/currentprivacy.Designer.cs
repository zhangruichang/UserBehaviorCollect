namespace UserView
{
    partial class currentprivacy
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
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).BeginInit();
            this.SuspendLayout();
            // 
            // datagridview2
            // 
            this.datagridview2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridview2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.datagridview2.Location = new System.Drawing.Point(12, 12);
            this.datagridview2.Name = "datagridview2";
            this.datagridview2.RowTemplate.Height = 23;
            this.datagridview2.Size = new System.Drawing.Size(567, 320);
            this.datagridview2.TabIndex = 1;
            this.datagridview2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridview2_CellContentClick);
            // 
            // Column2
            // 
            this.Column2.FalseValue = "0";
            this.Column2.HeaderText = "选择插入";
            this.Column2.Name = "Column2";
            this.Column2.TrueValue = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(411, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "插入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(492, 338);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "产生行为序列";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // currentprivacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 374);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.datagridview2);
            this.Name = "currentprivacy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture Manually";
            ((System.ComponentModel.ISupportInitialize)(this.datagridview2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datagridview2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}