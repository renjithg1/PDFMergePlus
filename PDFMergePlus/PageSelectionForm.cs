using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PDFMergePlus
{
    public partial class PageSelectionForm : Form
    {
        public string Range { get; set; }
        bool cancel_event = false;
        public PageSelectionForm()
        {
            InitializeComponent();
            txt_pagerange.Enabled = false;
            Range = "All";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_pagerange.Enabled = true;
        }

        /*
            Renjith - below is an override to windows to avoid the close button in the form.
        */
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel_event = false;
            this.DialogResult = DialogResult.OK;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txt_pagerange.Enabled = false;
        }

        private void txt_pagerange_TextChanged(object sender, EventArgs e)
        {

        }

        private bool ValidatePageRange()
        {
            string str_pagerange = txt_pagerange.Text;
            if (string.IsNullOrEmpty(str_pagerange))
                return false;


            return true; 
            /* TODO 
            Regex regex = new Regex(@"^[1-9]*$");
            Match match = regex.Match(str_pagerange);
            if (!match.Success)
            {
                throw new Exception("You must enter a valid page range");
            }

            return true;
            */
        }

        private void PageSelectionDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioAll.Checked == true)
                return;

            if (cancel_event)
                return;


            if (!this.ValidatePageRange())
            {
                if (MessageBox.Show("The page range is not valid", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    e.Cancel = true;
                else
                {
                    e.Cancel = false;
                    Range = "All";
                }
            }
            else
            {
                Range = txt_pagerange.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel_event = true;
        }
    }
}
