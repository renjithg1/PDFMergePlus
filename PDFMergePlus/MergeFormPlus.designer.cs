namespace PDFMergePlus
{
	partial class PDFMergeFrmPlus
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.outputFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.btnmergeSelected = new System.Windows.Forms.Button();
            this.pdf_listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(474, 65);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(104, 42);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&Browse PDF";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.Browse);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(474, 130);
            this.btnMerge.Margin = new System.Windows.Forms.Padding(4);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(104, 42);
            this.btnMerge.TabIndex = 2;
            this.btnMerge.Text = "&Merge All PDF";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.MergeBtn);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(474, 325);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 42);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Close);
            // 
            // outputFolder
            // 
            this.outputFolder.Location = new System.Drawing.Point(16, 26);
            this.outputFolder.Name = "outputFolder";
            this.outputFolder.ReadOnly = true;
            this.outputFolder.Size = new System.Drawing.Size(396, 20);
            this.outputFolder.TabIndex = 4;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(422, 27);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(39, 19);
            this.btnBrowseFolder.TabIndex = 5;
            this.btnBrowseFolder.Text = "***";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(474, 260);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(104, 42);
            this.Clear.TabIndex = 6;
            this.Clear.Text = "&Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // btnmergeSelected
            // 
            this.btnmergeSelected.Location = new System.Drawing.Point(474, 195);
            this.btnmergeSelected.Margin = new System.Windows.Forms.Padding(4);
            this.btnmergeSelected.Name = "btnmergeSelected";
            this.btnmergeSelected.Size = new System.Drawing.Size(104, 42);
            this.btnmergeSelected.TabIndex = 9;
            this.btnmergeSelected.Text = "&Merge Selected";
            this.btnmergeSelected.UseVisualStyleBackColor = true;
            this.btnmergeSelected.Click += new System.EventHandler(this.mergeSelectedBtn_Click);
            // 
            // pdf_listView
            // 
            this.pdf_listView.Location = new System.Drawing.Point(20, 63);
            this.pdf_listView.Name = "pdf_listView";
            this.pdf_listView.Size = new System.Drawing.Size(447, 304);
            this.pdf_listView.TabIndex = 10;
            this.pdf_listView.UseCompatibleStateImageBehavior = false;
            this.pdf_listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.pdf_listView_ItemSelectionChanged);
            this.pdf_listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pdf_listView_MouseClick);
            this.pdf_listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pdf_listView_MouseDown);
            // 
            // PDFMergeFrmPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(588, 379);
            this.Controls.Add(this.pdf_listView);
            this.Controls.Add(this.btnmergeSelected);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.outputFolder);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnBrowse);
            this.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "PDFMergeFrmPlus";
            this.Text = "PDFMergePlus";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PDFMergeFrmPlus_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnMerge;
		private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox outputFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ContextMenu cm;
        private System.Windows.Forms.Button btnmergeSelected;
        private System.Windows.Forms.ListView pdf_listView;
    }
}

