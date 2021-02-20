namespace PDFMergePlus
{
    partial class PageSelectionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pagerange = new System.Windows.Forms.TextBox();
            this.radioRange = new System.Windows.Forms.RadioButton();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_pagerange);
            this.groupBox1.Controls.Add(this.radioRange);
            this.groupBox1.Controls.Add(this.radioAll);
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Page Range";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 96);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter either a single page number or a single page range or both seperated by \",\"" +
    " or \";\"\r\n\r\nFor Example,\r\n5-12, 15, 18 \r\nor \r\n5-12; 15; 18";
            // 
            // txt_pagerange
            // 
            this.txt_pagerange.Location = new System.Drawing.Point(90, 43);
            this.txt_pagerange.Name = "txt_pagerange";
            this.txt_pagerange.Size = new System.Drawing.Size(137, 20);
            this.txt_pagerange.TabIndex = 2;
            this.txt_pagerange.TextChanged += new System.EventHandler(this.txt_pagerange_TextChanged);
            // 
            // radioRange
            // 
            this.radioRange.AutoSize = true;
            this.radioRange.Location = new System.Drawing.Point(16, 46);
            this.radioRange.Name = "radioRange";
            this.radioRange.Size = new System.Drawing.Size(55, 17);
            this.radioRange.TabIndex = 1;
            this.radioRange.TabStop = true;
            this.radioRange.Text = "Pages";
            this.radioRange.UseVisualStyleBackColor = true;
            this.radioRange.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Location = new System.Drawing.Point(16, 26);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(36, 17);
            this.radioAll.TabIndex = 0;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "All";
            this.radioAll.UseVisualStyleBackColor = true;
            this.radioAll.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(201, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PageSelectionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 216);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PageSelectionDlg";
            this.Text = "PageSelection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageSelectionDlg_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pagerange;
        private System.Windows.Forms.RadioButton radioRange;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}