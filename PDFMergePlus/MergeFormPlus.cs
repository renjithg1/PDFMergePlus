// 12/18/17 - Modified the method get_pageCcount() to correctly return the coorect page count.
// 08/16/19 - Added support for page selection. 
// 08/27/19 - Upgraded from ITextSharp library to iText7 (7.1.6)
using System;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using iText.Kernel.Pdf;

using System.Windows.Input;

namespace PDFMergePlus
{
    public partial class PDFMergeFrmPlus : Form
    {
        //private Array pdfListArray;
        //private int index;
        private OpenFileDialog pdfOpenFileDlg;
        private FolderBrowserDialog pdfOutputFolderDlg;
        Point pos;
        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;

        private bool ctrlKeyPressed = false;


        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);


        public PDFMergeFrmPlus()
        {
            InitializeComponent();
            // initilize the open file dialog properties
            InitializeOpenFileDialog();
            InitilizeForm();
            CreateMenu();
        }


        /*
            Initilization of all menus associated with this application. 
         *  Right now I have only "remove item:
         */
        private void CreateMenu()
        {

            MenuItem menuItem1 = new MenuItem("&Remove");
            MenuItem menuItem2 = new MenuItem("Remove All");
            MenuItem menuItem3 = new MenuItem("Select All");
            MenuItem menuItem4 = new MenuItem("Invert");
            MenuItem menuItem5 = new MenuItem("Merge Selected");
            MenuItem menuItem6 = new MenuItem("Select Pages");

            cm = new ContextMenu();

            // Clear all previously added MenuItems.
            cm.MenuItems.Clear();

            // Add MenuItems to display for the TextBox.
            cm.MenuItems.Add(menuItem1);
            cm.MenuItems.Add(menuItem2);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add(menuItem3);
            cm.MenuItems.Add(menuItem4);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add(menuItem5);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add(menuItem6);
            //Add menu handler
            menuItem1.Click += new EventHandler(this.Remove_Click);
            menuItem2.Click += new EventHandler(this.RemoveAll_Click);
            menuItem3.Click += new EventHandler(this.SelectAll_Click);
            menuItem4.Click += new EventHandler(this.InvertSelection_Click);
            menuItem5.Click += new EventHandler(this.Merge_Menu_Click);
            menuItem6.Click += new EventHandler(SelectPagesMenuItem_Click);
            // pdfList.ContextMenu = cm;


            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About…");
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                AboutPDFMerge abt = new AboutPDFMerge();
                abt.ShowDialog();
            }

        }

        private void InitilizeForm()
        {
            outputFolder.Text = @"C:\temp\new.pdf";

            pdfOutputFolderDlg = new FolderBrowserDialog();

            // Set the help text description for the FolderBrowserDialog.
            this.pdfOutputFolderDlg.Description =
                    "Select the directory that you want to use as the default.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            //this.pdfOutputFolderDlg.ShowNewFolderButton = false;

            // Default to the My Documents folder.
            //this.pdfOutputFolderDlg.RootFolder = Environment.SpecialFolder.Personal;

            // Set the SelectionMode to select multiple items.
            //pdfList.SelectionMode = SelectionMode.MultiExtended;


            btnMerge.Enabled = false;
            btnmergeSelected.Enabled = false;

            //Renjith ListView
            pdf_listView.View = View.Details;
            pdf_listView.GridLines = true;
            pdf_listView.FullRowSelect = true;
            pdf_listView.Columns.Add("File Name", 390);
            pdf_listView.Columns.Add("Pages", 50);
        }

        private void InitializeOpenFileDialog()
        {
            pdfOpenFileDlg = new OpenFileDialog();
            // Set the file dialog to filter for graphics files.
            this.pdfOpenFileDlg.Filter = "(*.PDF)|*.pdf";

            // Allow the user to select multiple images.
            this.pdfOpenFileDlg.Multiselect = true;
            this.pdfOpenFileDlg.Title = "PDF Browser";

        }

        private void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        /*
            Browse PDF files with multi selection capablity
         */
        private void Browse(object sender, EventArgs e)
        {
            DialogResult result = pdfOpenFileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Read the files (multi selected)
                foreach (string file in pdfOpenFileDlg.FileNames)
                {
                    try
                    {
                        //pdfList.Items.Add(file);
                        btnMerge.Enabled = true;

                        //Renjith ListView
                        string[] row = { file, "All" };
                        var lvItem = new ListViewItem(row);
                        pdf_listView.Items.Add(lvItem);
                    }
                    catch (IOException)
                    {

                    }
                }
            }
        }


        private void MergeBtn(object sender, EventArgs e)
        {
            Merge(sender, e);
        }

        /*
           Merge all the PDF files which was added to the list control
         * starting from 1. 
        */
        private void Merge(object sender, EventArgs e, bool bSelected = false)
        {
            // Key value pair <Pdfpath, Pages>
            List<KeyValuePair<string, string>> pdfListToMergeWithPages = new List<KeyValuePair<string, string>>();


            //Renjith ListView
            if (bSelected)
            {
                if (pdf_listView.SelectedItems.Count <= 1)
                    throw new Exception("No PDF or only one PDF is Selected");

                foreach (ListViewItem item in pdf_listView.SelectedItems)
                {
                    string path = item.Text;
                    string pages = item.SubItems[1].Text;
                    pdfListToMergeWithPages.Add(new KeyValuePair<string, string>(path, pages));
                }
            }
            else
            {
                foreach (ListViewItem item in pdf_listView.Items)
                {
                    string path = item.Text;
                    string pages = item.SubItems[1].Text;
                    pdfListToMergeWithPages.Add(new KeyValuePair<string, string>(path, pages));
                }
            }

            try
            {
                CreatePDFPlus(pdfListToMergeWithPages);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }          
        }


        private void CreatePDFPlus(List<KeyValuePair<string, string>> pdfListToMergeWithPages)
        {
            if (pdfListToMergeWithPages.Count == 0)
                throw new Exception("The pdf list is empty");

            string outputPdfPath = outputFolder.Text;
            string outputBackupPdfPath = Directory.GetParent(outputPdfPath).FullName + "\\bak" + Path.GetFileName(outputPdfPath);// outputPdfPath + ".back.pdf";
            bool bOverWriteOutput = true;

            if (System.IO.File.Exists(outputPdfPath))
            {
                DialogResult dialogResult = MessageBox.Show(String.Format("Output file <{0}> already exists. Do you want to overwrite??", outputPdfPath), "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    bOverWriteOutput = false;
            }

            if (!bOverWriteOutput)
            {
                // Take a copy
                System.IO.File.Copy(outputPdfPath, outputBackupPdfPath);

                pdfListToMergeWithPages.Add(new KeyValuePair<string, string>(outputBackupPdfPath, "All"));

            }

            if (pdfListToMergeWithPages.Count == 1 && !bOverWriteOutput)
                throw new Exception("The pdf list is empty");

            PdfDocument destination_pdf = new PdfDocument(new PdfWriter(outputPdfPath));

            foreach (KeyValuePair<string, string> keyval in pdfListToMergeWithPages)
            {
                bool bAllPages = true;
                string sourcePDFPath = keyval.Key;
                string sourcePages = keyval.Value;

                if (string.IsNullOrEmpty(sourcePDFPath))
                    continue;

                List<int> neededPage = new List<int>();
                if (sourcePages != "All")
                {
                    bAllPages = false;
                    char[] delimiterChars = { ';', ',' };

                    string[] strPageTokens = sourcePages.Split(delimiterChars);


                    foreach (var token in strPageTokens)
                    {
                        string tokenValue = token.Trim();
                        if (tokenValue.Contains("-"))
                        {
                            string[] range = tokenValue.Split('-');
                            int start = int.Parse(range[0].Trim());
                            int end = int.Parse(range[1].Trim());
                            if (start > end)
                                throw new Exception("Page range not valid");

                            for (int nPos = start; nPos <= end; nPos++)
                            {
                                neededPage.Add(nPos);
                            }
                        }
                        else
                        {
                            neededPage.Add(int.Parse(tokenValue));
                        }
                    }
                }



                PdfReader sourceReader = new PdfReader(sourcePDFPath);
                PdfDocument sourcePDFDoc = new PdfDocument(sourceReader);
                int totalPages = sourcePDFDoc.GetNumberOfPages();

                for (int pageNum = 1; pageNum <= totalPages; pageNum++)
                {
                    //Original page size
                    PdfPage sourcePage = sourcePDFDoc.GetPage(pageNum);

                    if (bAllPages)
                    {
                        // Add page with original size
                        destination_pdf.AddPage(sourcePage.CopyTo(destination_pdf));
                    }
                    else
                    {
                        // Break the loop if no pages are specified or the pages are already processed.  
                        if (neededPage.Count == 0)
                            break;

                        if (neededPage.Exists(x => x == pageNum))
                        {
                            neededPage.Remove(pageNum);

                            // Add page with original size
                            destination_pdf.AddPage(sourcePage.CopyTo(destination_pdf));
                        }
                    }
                }

                sourcePDFDoc.Close();

            }

            destination_pdf.Close();

            DeleteFile(outputBackupPdfPath);
            MessageBox.Show(String.Format("Merging completed {0} Success", outputPdfPath));
        }


        private void Close(object sender, EventArgs e)
		{            
			Application.Exit();
		}

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            // Uses the Keyboard.IsKeyDown to determine if a key is down.
            // e is an instance of KeyEventArgs.
            if (ctrlKeyPressed == true)
            {
                // Change the property of the openfile dialog 
                pdfOpenFileDlg.CheckFileExists = false;
                pdfOpenFileDlg.Multiselect = false;

                DialogResult result = pdfOpenFileDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    outputFolder.Text = pdfOpenFileDlg.FileName;
                }
                // Change the property of the openfile dialog back to defaults
                ctrlKeyPressed = false;
                pdfOpenFileDlg.CheckFileExists = true;
                pdfOpenFileDlg.Multiselect = true;
            }
            else
            {
                if (DialogResult.OK == pdfOutputFolderDlg.ShowDialog())
                {
                    outputFolder.Text = pdfOutputFolderDlg.SelectedPath + "\\merged_out.pdf";
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            pdf_listView.Items.Clear();
            //pdfList.Items.Clear();
            btnMerge.Enabled = false;
            btnmergeSelected.Enabled = false;
        }



        private void Remove_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in pdf_listView.SelectedItems)
            {
                pdf_listView.Items.Remove(item);
            }

            if (pdf_listView.Items.Count == 0)
                btnMerge.Enabled = false;
        }

        private void RemoveAll_Click(object sender, EventArgs e)
        {
            Clear_Click(sender, e);        
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in pdf_listView.Items)
            {
                item.Selected = true;
            }
        }

        private void InvertSelection_Click(object sender, EventArgs e)
        {
            List<int> toselect_indexes = new List<int>();
            for (int i = 0; i < pdf_listView.Items.Count; ++i)
            {
                if(pdf_listView.Items[i].Selected == false)
                {
                    toselect_indexes.Add(i);
                }
            }

            pdf_listView.SelectedItems.Clear();
            pdf_listView.FocusedItem.Selected = false;
            pdf_listView.TopItem.Selected = true;

            foreach (int index in toselect_indexes)
            {
                pdf_listView.Items[index].Selected = true;
                pdf_listView.Items[index].Focused = true;
            }
        }

        private void Merge_Menu_Click(object sender, EventArgs e)
        {
            this.Merge(sender, e, true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Browse(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnBrowseFolder_Click(sender, e);
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Merge(sender, e);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(sender, e);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Clear_Click(sender, e);
        }

        private void SelectPagesMenuItem_Click(object sender, EventArgs e)
        {
            this.SelectPages(sender, e);
        }

        private void SelectPages(object sender, EventArgs e)
        {            
            PageSelectionForm pageDlg = new PageSelectionForm();
            // place the form in the center of the parent form
            pageDlg.StartPosition = FormStartPosition.CenterParent;
            if (pageDlg.ShowDialog() == DialogResult.OK)
            {
                ChangePageRangeinlistView(pageDlg.Range);
            }
        }

        private void ChangePageRangeinlistView(string range)
        {
            foreach (ListViewItem item in pdf_listView.SelectedItems)
            {
                //renjith
                //string path = item.ToString();
                item.SubItems[1].Text = range.Trim();
            }
        }


        private void mergeSelectedBtn_Click(object sender, EventArgs e)
        {
            this.Merge(sender, e, true);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPDFMerge abt = new AboutPDFMerge();
            abt.ShowDialog();
        }

        private void pdf_listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu only if there is some contents 
                // in the list box. If there is no contents in the list box IndexFromPoint(X, Y) 
                // will return -1

                ContextMenu cmnew = cm.GetContextMenu();
                cmnew.MenuItems[0].Enabled = false;
                cmnew.MenuItems[6].Enabled = false;
                cmnew.MenuItems[8].Enabled = false;
                pos = new Point(e.X, e.Y);
                if (pdf_listView.SelectedIndices.Count > 0)
                {
                    cmnew.MenuItems[0].Enabled = true;
                    cmnew.MenuItems[8].Enabled = true;
                }
                if (pdf_listView.SelectedIndices.Count  > 1)
                {
                    cmnew.MenuItems[6].Enabled = true;
                    btnmergeSelected.Enabled = true;
                }

                cm.Show(pdf_listView, pos);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (pdf_listView.SelectedIndices.Count > 1)
                    btnmergeSelected.Enabled = true;
                else
                    btnmergeSelected.Enabled = false;
            }
        }

        private void pdf_listView_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pdf_listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(pdf_listView.SelectedIndices.Count > 1)
                    btnmergeSelected.Enabled = true;
                else
                    btnmergeSelected.Enabled = false;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //handle your keys here
            //MessageBox.Show(msg.ToString());

            var value = Keys.ControlKey | Keys.Control;
            if (keyData == value)
            {
                ctrlKeyPressed = true;
              //  return true;
            }
        
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PDFMergeFrmPlus_KeyUp(object sender, KeyEventArgs e)
        {
           if(e.KeyData == Keys.ControlKey)
                ctrlKeyPressed = false;
        }
    }


}
