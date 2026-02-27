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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private void button1_Click(object sender, EventArgs e)
        {
            //Create new DataValidation Object for input validation.
            DataValidation newDV = new DataValidation();

            //Get part number value
            string partNumber = textBoxPartNumber.Text;

            //Get quantity value from textBox1
            string quantity = textBox1.Text;

            // Get purchase order number value from textBoxPO
            string poNumber = textBoxPO.Text;

            //bool flag for final check before insert.
            bool partNumberFlag = false;
            bool quantityFlag = false;
            bool checkItemsFlag = false; 
            bool poFlag = false;

            //Pass part number to validatePartNumber function for validation.
            if (newDV.validatePartNumber(partNumber) == false)
            {
                errorProviderPartNumber.SetError(textBoxPartNumber,
                    "Record not Complete!\nPart Number must not be empty\nand may only contain letters,\nnumbers, or hyphen.");
                label2.Text = "Record not Complete!\nInvalid Part Number.";
                partNumberFlag = false;
            }
            else
            {
                errorProviderPartNumber.Clear();
                partNumberFlag = true;
            }

            //Pass quantity to validateQuantity function for validation.
            if (newDV.validateQuantity(quantity) != "")
            {
                errorProvider1.SetError(textBox1, newDV.validateQuantity(quantity));
                label2.Text = newDV.validateQuantity(quantity);
                quantityFlag = false;
            }
            else if (newDV.validateQuantity(quantity) == "")
            {
                ///Clear error messages after successful quantity validation.
                errorProvider1.Clear();
                quantityFlag = true;
            }
            // Prevent form submission or continue as needed
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                errorProvider2.SetError(checkedListBox1, "Record not Complete!\nPlease select at least one option.");
                checkItemsFlag = false;
            }
            else if (checkedListBox1.CheckedItems.Count != 0)
            {
                //Clear error messages after successful operation selection validation.
                errorProvider2.Clear();
                checkItemsFlag = true;
            }
            if (newDV.validateQuantity(quantity) == "")
            {
                label2.Text = "";
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    label2.Text = "Record not Complete!\nPlease select at least one option.";
                }
            }
            // Pass purchase order number to validatePurchaseOrderNumber function for validation.
            string poError = newDV.validatePurchaseOrderNumber(poNumber);

            if (poError != "")
            {
                errorProvider3.SetError(textBoxPO, poError);
                label2.Text = poError;
                poFlag = false;
            }
            else
            {
                errorProvider3.SetError(textBoxPO, "");
                poFlag = true;
            }

            //Final logic check before record insertion and user record status update.
            if (partNumberFlag == true && quantityFlag == true && checkItemsFlag == true && poFlag == true)
            {
                label3.Text = "Record Status: Record Successfully Created!";

                //Commented out for textbox input validation testing.
                /*
                // At least one item is checked, proceed
                List<string> selectedItems = new List<string>();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    selectedItems.Add(item.ToString());
                }
                //save formated checklist options to pass as a paramater into insert statement
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
                */
            }
            else
            {
                label3.Text = "Record Status: Record Creation Error!";
            }
        }
        //Event handler for Clear button.
        private void button2_Click(object sender, EventArgs e)
        {
            textBoxPartNumber.Clear();
            textBox1.Clear();
            textBoxPO.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProviderPartNumber.Clear();
            errorProvider3.Clear();
            label2.Text = "";
            label3.Text = "";
            checkedListBox1.ClearSelected();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
        //Event handle for Back to Menu button.
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
