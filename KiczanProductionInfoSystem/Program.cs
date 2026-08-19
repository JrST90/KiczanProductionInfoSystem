using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;
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
        
            FormSplash splash = new FormSplash();
            splash.Show();
            Application.DoEvents();

            Users currentUser = null;
            bool initializationFailed = false;
            string errorMessage = "";

            Task initTask = Task.Run(() =>
            {
                try
                {
                    //Update loading screen text before reading config file.
                    splash.UpdateStatus("Reading connection configuration...");

                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.txt");

                    if (!File.Exists(filePath))
                    {
                        errorMessage = "Could not find connection.txt in the application directory.";
                        return;
                    }

                    //Update loading screen text before connection process.
                    splash.UpdateStatus("Connection to SQL Server Database...");

                    string connectionString = File.ReadAllText(filePath).Trim();
                    DAO.InitializeConnection(connectionString);

                    //Update loading screen text before verifying user permissions.
                    splash.UpdateStatus("Verifying user permissions and roles...");

                    DAO newDAO = new DAO();

                    //Get the current OS username.
                    string userName = Environment.UserName;

                    //Initialize a Users object to the returned object from getUserInfo().
                    currentUser = newDAO.getUserInfo(userName);
                }
                catch (Exception ex)
                {
                    initializationFailed = true;
                    errorMessage = $"Initialization failed.\n\nDetails: {ex.Message}";

                }
            });

            //Keep the loading screen active and animated while waiting.
            while (!initTask.IsCompleted)
            {
                //Keeps marquee animation running and UI responsive.
                Application.DoEvents();

                //Prevents 100% CPU usage spike.
                System.Threading.Thread.Sleep(50);
            }

            //SQL Server DB handshake is complete, now close the splash screen.
            splash.Close();
            splash.Dispose();

            //Check to see if any file or database errors occured on the background thread
            if(!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check if the current user is authorized.
            if (currentUser == null || currentUser.USER_ID == 0 || currentUser.ROLES_ID == 0)
            {
                MessageBox.Show("Unauthorized User. Application will now close.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new Form1(currentUser));


            /*
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

                Application.Run(new Form1(currentUser));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization failed.\n\nDetails: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */
        }
    }
}
