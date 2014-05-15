namespace UserView
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.rdoclose = new System.Windows.Forms.RadioButton();
            this.rdoopen = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radiomyhistory = new System.Windows.Forms.RadioButton();
            this.radiomycurrent = new System.Windows.Forms.RadioButton();
            this.radiocurrentprivacy = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(19, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Modes:";
            // 
            // rdoclose
            // 
            this.rdoclose.AutoSize = true;
            this.rdoclose.BackColor = System.Drawing.Color.Transparent;
            this.rdoclose.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoclose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoclose.Location = new System.Drawing.Point(0, 35);
            this.rdoclose.Name = "rdoclose";
            this.rdoclose.Size = new System.Drawing.Size(46, 20);
            this.rdoclose.TabIndex = 10;
            this.rdoclose.TabStop = true;
            this.rdoclose.Text = "Off";
            this.rdoclose.UseVisualStyleBackColor = false;
            // 
            // rdoopen
            // 
            this.rdoopen.AutoSize = true;
            this.rdoopen.BackColor = System.Drawing.Color.Transparent;
            this.rdoopen.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoopen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoopen.Location = new System.Drawing.Point(0, 9);
            this.rdoopen.Name = "rdoopen";
            this.rdoopen.Size = new System.Drawing.Size(43, 20);
            this.rdoopen.TabIndex = 9;
            this.rdoopen.TabStop = true;
            this.rdoopen.Text = "On";
            this.rdoopen.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Moniter:";
            // 
            // radiomyhistory
            // 
            this.radiomyhistory.AutoSize = true;
            this.radiomyhistory.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiomyhistory.Location = new System.Drawing.Point(87, 117);
            this.radiomyhistory.Name = "radiomyhistory";
            this.radiomyhistory.Size = new System.Drawing.Size(99, 20);
            this.radiomyhistory.TabIndex = 15;
            this.radiomyhistory.TabStop = true;
            this.radiomyhistory.Text = "History View";
            this.radiomyhistory.UseVisualStyleBackColor = true;
            this.radiomyhistory.CheckedChanged += new System.EventHandler(this.radiomyhistory_CheckedChanged);
            // 
            // radiomycurrent
            // 
            this.radiomycurrent.AutoSize = true;
            this.radiomycurrent.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiomycurrent.Location = new System.Drawing.Point(87, 143);
            this.radiomycurrent.Name = "radiomycurrent";
            this.radiomycurrent.Size = new System.Drawing.Size(150, 20);
            this.radiomycurrent.TabIndex = 16;
            this.radiomycurrent.TabStop = true;
            this.radiomycurrent.Text = "Capture Automatically";
            this.radiomycurrent.UseVisualStyleBackColor = true;
            this.radiomycurrent.CheckedChanged += new System.EventHandler(this.radiomycurrent_CheckedChanged);
            // 
            // radiocurrentprivacy
            // 
            this.radiocurrentprivacy.AutoSize = true;
            this.radiocurrentprivacy.Font = new System.Drawing.Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiocurrentprivacy.Location = new System.Drawing.Point(87, 169);
            this.radiocurrentprivacy.Name = "radiocurrentprivacy";
            this.radiocurrentprivacy.Size = new System.Drawing.Size(125, 20);
            this.radiocurrentprivacy.TabIndex = 17;
            this.radiocurrentprivacy.TabStop = true;
            this.radiocurrentprivacy.Text = "Capture Manually";
            this.radiocurrentprivacy.UseVisualStyleBackColor = true;
            this.radiocurrentprivacy.CheckedChanged += new System.EventHandler(this.radiocurrentprivacy_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoopen);
            this.panel1.Controls.Add(this.rdoclose);
            this.panel1.Location = new System.Drawing.Point(87, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(125, 58);
            this.panel1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 37);
            this.label4.TabIndex = 19;
            this.label4.Text = "Copyright © Shanghai University,All Rights Reserved";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 299);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radiocurrentprivacy);
            this.Controls.Add(this.radiomycurrent);
            this.Controls.Add(this.radiomyhistory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Action Capturer";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoclose;
        private System.Windows.Forms.RadioButton rdoopen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radiomyhistory;
        private System.Windows.Forms.RadioButton radiomycurrent;
        private System.Windows.Forms.RadioButton radiocurrentprivacy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
    }
}