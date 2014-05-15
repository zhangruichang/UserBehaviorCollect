namespace UserView
{
    partial class sequence
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
            this.dgvseq = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvseq)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvseq
            // 
            this.dgvseq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvseq.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvseq.Location = new System.Drawing.Point(12, 12);
            this.dgvseq.Name = "dgvseq";
            this.dgvseq.RowTemplate.Height = 23;
            this.dgvseq.Size = new System.Drawing.Size(911, 406);
            this.dgvseq.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(817, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "插入数据库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Column1
            // 
            this.Column1.FalseValue = "0";
            this.Column1.HeaderText = "选择插入";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "1";
            // 
            // sequence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 449);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvseq);
            this.Name = "sequence";
            this.Text = "sequence";
            ((System.ComponentModel.ISupportInitialize)(this.dgvseq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvseq;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}