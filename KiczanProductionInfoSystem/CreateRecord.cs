using KiczanProductionInfoSystem;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiczanProductionInformationSystem
{
    public partial class CreateRecord : Form
    {
        public CreateRecord()
        {
            InitializeComponent();
        }
        //eventhandler for checkboxlist
       private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
       

        //eventhandler for create record button
            private void button1_Click(object sender, EventArgs e) {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one option.");
                // Prevent form submission or continue as needed
            }
            else
            {
                // At least one item is checked, proceed

                List<string> selectedItems = new List<string>();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    selectedItems.Add(item.ToString());
                }
                //save formated checklist options to pass as a paramater into insert statemen
                string checkedOperations = string.Join(",", selectedItems);


                //connnect to the database
                string connectionString = "datasource=localhost;port=3306;username=root;" +
                "password=root;database=kiczan_production_system;";

                //string query to set the inset statement
                string query = @" INSERT INTO PART_HISTORY 
            (PART_HISTORY_ID, CUSTOMER_ID, OPERATOR_ID, PART_NUMBER, DATE_DUE, PURCHASE_ORDER_NUMBER, QTY, OPERATIONS, DATE_RECEIVED ,TO_DELETE)
            VALUES 
                (@PART_HISTORY_ID, @CUSTOMER_ID, @OPERATOR_ID, @PART_NUMBER, @DATE_DUE, @PURCHASE_ORDER_NUMBER, @QTY, @OPERATIONS, @DATE_RECEIVED , @TO_DELETE)";

                var connection = new MySqlConnection(connectionString);

                var command = new MySqlCommand(query, connection);







                //current dummy values and a varible for the operations selceted. 
                //TO DO change from addwithcalue to add with correct database datatypes
                command.Parameters.Add("@PART_HISTORY_ID", MySqlDbType.Int32).Value = 50000;
                command.Parameters.Add("@CUSTOMER_ID", MySqlDbType.Int32).Value = 17;
                command.Parameters.Add("OPERATOR_ID", MySqlDbType.Int32).Value = 4;
                command.Parameters.Add("@PART_NUMBER", MySqlDbType.VarChar).Value = "7777";
                command.Parameters.Add("@DATE_DUE", MySqlDbType.DateTime).Value = new DateTime(2525, 12, 25);
                command.Parameters.Add("@PURCHASE_ORDER_NUMBER", MySqlDbType.VarChar).Value = "7777";
                command.Parameters.Add("@QTY", MySqlDbType.Int32).Value = 7777;
                command.Parameters.Add("@OPERATIONS", MySqlDbType.VarChar).Value = checkedOperations;
                command.Parameters.Add("@DATE_RECEIVED", MySqlDbType.DateTime).Value = new DateTime(2049, 6, 13);
                command.Parameters.Add("@TO_DELETE", MySqlDbType.Binary).Value = 0;
                //exeception catching for the insert
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Record inserted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
 