﻿namespace UserView
{
    partial class urltime
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
            this.dgvurltime = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvurltime)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvurltime
            // 
            this.dgvurltime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvurltime.Location = new System.Drawing.Point(12, 3);
            this.dgvurltime.Name = "dgvurltime";
            this.dgvurltime.RowTemplate.Height = 23;
            this.dgvurltime.Size = new System.Drawing.Size(531, 418);
            this.dgvurltime.TabIndex = 0;
            // 
            // urltime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 424);
            this.Controls.Add(this.dgvurltime);
            this.Name = "urltime";
            this.Text = "urltime";
            this.Load += new System.EventHandler(this.urltime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvurltime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvurltime;
    }
}