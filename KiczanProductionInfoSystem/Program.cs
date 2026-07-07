using System;
using System.IO;
using System.Windows.Forms;

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

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.txt");

                if (File.Exists(filePath))
                {
                    string connectionString = File.ReadAllText(filePath).Trim();

                    DAO.InitializeConnection(connectionString);
                }
                else
                {
                    MessageBox.Show("Could not find connection.txt in the application directory.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection string.\n\nMake sure 'Copy to Output Directory' is set for connection.txt.\n\nDetails: {ex.Message}", "Initialization Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check if current user's username is in the DB.
            if (!newDAO.userNameCheck(userName))
            {
                MessageBox.Show("Unauthorized User. Application will now close.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
