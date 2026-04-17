using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace KiczanProductionInfoSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        

            //Create new DAO Object for user authentication.
            DAO newDAO = new DAO();

            //Get the current user's username from the system.
            string userName = Environment.UserName;

            //Check if current user's username is in the DB.
            if (!newDAO.userNameCheck(userName))
            {
                MessageBox.Show("Unauthorized User. Application will now close.");
                Application.Exit();
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
