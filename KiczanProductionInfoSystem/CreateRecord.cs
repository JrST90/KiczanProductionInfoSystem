using KiczanProductionInfoSystem;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static KiczanProductionInfoSystem.DAO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KiczanProductionInformationSystem
{
    public partial class CreateRecord : Form
    {
        //List to hold operator names returned from function.
        private List<Operators> operators = new List<Operators>();

        //List to hold customer names returned from function.
        private List<Customers> customers = new List<Customers>();
        public CreateRecord()
        {
            InitializeComponent();

            //Create new DAO object for query.
            DAO newDAO = new DAO();

            //Set the operators List object to be populated with the list returned by GetOperators().
            operators = newDAO.GetOperators();

            //Set the customers List object to be populated with the list returned by GetCustomers().
            customers = newDAO.GetCustomers();

            //Bind operatorComboBox datasource to the operators list.
            operatorComboBox.DataSource = operators;

            //Bind customerComboBox datasource to the customers list.
            customerComboBox.DataSource = customers;

            //Set the display values for operatorComboBox.
            operatorComboBox.DisplayMember = "OPERATOR_NAME";

            //Set the stored values for operatorComboBox.
            operatorComboBox.ValueMember = "OPERATOR_ID";

            //Set the display values for customerComboBox.
            customerComboBox.DisplayMember = "CUSTOMER_NAME";

            //Set the stored values for customerComboBox.
            customerComboBox.ValueMember = "CUSTOMER_ID";

            //Set the initial value of operatorComboBox to empty.
            operatorComboBox.SelectedIndex = -1;

            //Set the initial value of customerComboBox to empty.
            customerComboBox.SelectedIndex = -1;
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
            string quantity = textBoxQuantity.Text;

            // Get Date Received value
            string dateReceived = textBoxDateReceived.Text;

            // Get purchase order number value from textBoxPO
            string poNumber = textBoxPO.Text;

            //Get OPERATOR_ID from operatorComboBox
            int opID = Convert.ToInt32(operatorComboBox.SelectedValue);

            //Get the CUSTOMER_ID from customerComboBox
            int custID = Convert.ToInt32(customerComboBox.SelectedValue);

            //validation flag
            bool isValid = true;

            Console.WriteLine(opID);
            Console.WriteLine(custID);

            // Clear all error labels first
            labelPartNumberError.Text = "";
            labelQuantityError.Text = "";
            labelOperationsError.Text = "";
            labelPOError.Text = "";
            labelOperatorError.Text = "";
            labelCustomerError.Text = "";
            labelDateReceivedError.Text = "";
            string checkedOperations = "";
            errorProviderPartNumber.Clear();
            errorProviderOperator.Clear();
            errorProviderCustomer.Clear();
            errorProviderDateReceived.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();

            //Customer validation.
            string customerError = newDV.validateCustomerSelection(custID);
            if (!string.IsNullOrEmpty(customerError))
            {
                labelCustomerError.Text = customerError;
                errorProviderCustomer.SetError(customerComboBox, customerError);
                isValid = false;
            }

            //Operator validation.
            string operatorError = newDV.validateOperatorSelection(opID);
            if (!string.IsNullOrEmpty(operatorError))
            {
                labelOperatorError.Text = operatorError;
                errorProviderOperator.SetError(operatorComboBox, operatorError);
                isValid = false;
            }

            //part number validation
            if (!newDV.validatePartNumber(partNumber))
            {
                string message = "Part Number must not be empty and may only \n contain letters, numbers, or hyphen.";
                labelPartNumberError.Text = message;
                errorProviderPartNumber.SetError(textBoxPartNumber, message);
                isValid = false;
            }

            //quantity validation
            string quantityError = newDV.validateQuantity(quantity);
            if (!string.IsNullOrEmpty(quantityError))
            {
                labelQuantityError.Text = quantityError;
                errorProvider1.SetError(textBoxQuantity, quantityError);
                isValid = false;
            }

            //operations validation
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                string message = "Please select at least one operation.";
                labelOperationsError.Text = message;
                errorProvider2.SetError(checkedListBox1, message);
                isValid = false;
            }
            else
            {
                // At least one item is checked, proceed
                List<string> checkedItems = new List<string>();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    checkedItems.Add(item.ToString());
                    //save formated checklist options to pass as a paramater into insert statement
                    checkedOperations = string.Join(", ", checkedItems);
                    Console.WriteLine(checkedOperations);
                }

            }

            //purchase order number validation
            string poError = newDV.validatePurchaseOrderNumber(poNumber);
            if (!string.IsNullOrEmpty(poError))
            {
                labelPOError.Text = poError;
                errorProvider3.SetError(textBoxPO, poError);
                isValid = false;
            }

            // date received validation
            string dateReceivedError = newDV.validateDateReceived(dateReceived);
            if (!string.IsNullOrEmpty(dateReceivedError))
            {
                labelDateReceivedError.Text = dateReceivedError;
                errorProviderDateReceived.SetError(textBoxDateReceived, dateReceivedError);
                isValid = false;
            }


            //final check before to comfirm
            if (isValid)
            {
                DateTime parsedDateReceived = DateTime.ParseExact(dateReceived, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                DAO newDAO = new DAO();
                newDAO.CreateRecord(custID, opID, partNumber, parsedDateReceived, poNumber, quantity, checkedOperations, DateTime.Now, 0);

                labelRecordStatus.Text = "Record Status: Record Successfully Created!";
                   
            }

            else
            {

                labelRecordStatus.Text = "Record Status: Record Creation Error!";

            }
            
        }





private void button2_Click(object sender, EventArgs e)
        {
            operatorComboBox.SelectedIndex = -1;
            customerComboBox.SelectedIndex = -1;
            errorProviderOperator.Clear();
            errorProviderCustomer.Clear();
            textBoxPartNumber.Clear();
            textBoxQuantity.Clear();
            textBoxPO.Clear();
            textBoxDateReceived.Clear();
            errorProviderDateReceived.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProviderPartNumber.Clear();
            errorProvider3.Clear();
            labelPartNumberError.Text = "";
            labelPOError.Text = "";
            labelQuantityError.Text = "";
            labelOperationsError.Text = "";
            labelOperatorError.Text = "";
            labelCustomerError.Text = "";
            labelRecordStatus.Text = "";
            labelDateReceivedError.Text = "";
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

        private void CreateRecord_Load(object sender, EventArgs e)
        {

        }
        
    }
}
