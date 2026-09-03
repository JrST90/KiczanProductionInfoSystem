using System;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    internal partial class FormSplash : Form
    {
        internal FormSplash()
        {
            InitializeComponent();
        }

        //Instruct the Windows OS to draw a native desktop drop shadow around this borderless form.
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        //Updates the status text of the text displayed on the splash screen safely across the threads.
        internal void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }
        }
    }
}
