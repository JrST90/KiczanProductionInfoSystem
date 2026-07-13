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
        
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Could not find connection.txt in the application directory.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string connectionString = File.ReadAllText(filePath).Trim();
                DAO.InitializeConnection(connectionString);

                DAO newDAO = new DAO();

                //Get the current OS username.
                string userName = Environment.UserName;

                //Initialize a Users object to the returned object from getUserInfo().
                Users currentUser = newDAO.getUserInfo(userName);

                //Catch unauthorized users who either do not exist in the DB, or those who have not been assigned a role.
                if(currentUser == null || currentUser.USER_ID == 0 || currentUser.ROLES_ID == 0)
                {
                    MessageBox.Show("Unauthorized User. Application will now close.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization failed.\n\nDetails: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
