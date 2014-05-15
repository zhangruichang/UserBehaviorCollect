namespace UserView
{
    partial class mycurrent
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
            this.dgvcur = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcur)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvcur
            // 
            this.dgvcur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcur.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvcur.Location = new System.Drawing.Point(3, 2);
            this.dgvcur.Name = "dgvcur";
            this.dgvcur.RowTemplate.Height = 23;
            this.dgvcur.Size = new System.Drawing.Size(701, 332);
            this.dgvcur.TabIndex = 2;
            this.dgvcur.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvcur_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.FalseValue = "0";
            this.Column1.HeaderText = "选择插入";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(612, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(512, 340);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mycurrent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 375);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvcur);
            this.Name = "mycurrent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture Automatically";
            this.Load += new System.EventHandler(this.mycurrent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvcur)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvcur;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}