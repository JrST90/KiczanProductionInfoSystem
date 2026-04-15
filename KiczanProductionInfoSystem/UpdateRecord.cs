using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    public partial class UpdateRecord : Form
    {
        //List to hold operator names returned from function.
        private List<Operators> operators = new List<Operators>();

        //List to hold customer names returned from function.
        private List<Customers> customers = new List<Customers>();
        
        private int partHistoryID;
        public UpdateRecord(int convertedPartHistoryID, string retrievedCustomerName, string retrievedOperatorName, string retrievedPartNumber, string retrievedPurchaseOrderNumber, string retrievedQuantity, string retrievedDateReceived, string retrievedDateDue, string retrievedOperations)
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

            //Set form values to values retrieved from selected record.
            partHistoryID = convertedPartHistoryID;
            int customerIndex = customerComboBox.FindString(retrievedCustomerName);
            int operatorIndex = operatorComboBox.FindString(retrievedOperatorName);
            customerComboBox.SelectedIndex = customerIndex;
            operatorComboBox.SelectedIndex = operatorIndex;
            textBoxPartNumber.Text = retrievedPartNumber;
            textBoxPO.Text = retrievedPurchaseOrderNumber;
            textBoxQuantity.Text = retrievedQuantity;
            textBoxDateReceived.Text = retrievedDateReceived;
            textboxDueDate.Text = retrievedDateDue;
            //Get checked box values from selected record and display on UpdateRecord form.
            if(retrievedOperations.Contains("Laser"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Laser");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
            if (retrievedOperations.Contains("Water Jet"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Water Jet");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
            if (retrievedOperations.Contains("Punch"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Punch");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
            if (retrievedOperations.Contains("Press Brake"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Press Brake");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
            if (retrievedOperations.Contains("Lathe"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Lathe");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
            if (retrievedOperations.Contains("Mill"))
            {
                int checkedListBoxIndex = checkedListBox1.FindStringExact("Mill");
                if (checkedListBoxIndex != -1)
                {
                    checkedListBox1.SetItemChecked(checkedListBoxIndex, true);
                }
            }
        }
        //eventhandler for checkboxlist
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //eventhandler for update record button
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

            // Get Due Date value from textboxDueDate
            string dateDue = textboxDueDate.Text;

            // Get purchase order number value from textBoxPO
            string poNumber = textBoxPO.Text;

            //Get OPERATOR_ID from operatorComboBox
            int opID = Convert.ToInt32(operatorComboBox.SelectedValue);

            //Get the CUSTOMER_ID from customerComboBox
            int custID = Convert.ToInt32(customerComboBox.SelectedValue);
            //Set the partID for the update query.
            

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
            labelDueDateError.Text = "";
            string checkedOperations = "";
            errorProviderPartNumber.Clear();
            errorProviderOperator.Clear();
            errorProviderCustomer.Clear();
            errorProviderDateReceived.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();

            //TO DO: Customer validation.

            string customerError = newDV.validateCustomerSelection(custID);

            if (!string.IsNullOrEmpty(customerError))
            {
                labelCustomerError.Text = customerError;
                errorProviderCustomer.SetError(customerComboBox, customerError);
                isValid = false;
            }

            //TO DO: Operator validation.

            string operatorError = newDV.validateOperatorSelection(opID);

            if (!string.IsNullOrEmpty(operatorError))
            {
                labelOperatorError.Text = operatorError;
                errorProviderOperator.SetError(operatorComboBox, operatorError);
                isValid = false;
            }

            //Part number validation
            if (!newDV.validatePartNumber(partNumber))
            {
                string message = "Record not complete!\nPart Number must not be empty and may only\ncontain letters, numbers, or hyphen.";
                labelPartNumberError.Text = message;
                errorProviderPartNumber.SetError(textBoxPartNumber, message);
                isValid = false;
            }

            //Quantity validation
            string quantityError = newDV.validateQuantity(quantity);
            if (!string.IsNullOrEmpty(quantityError))
            {
                labelQuantityError.Text = quantityError;
                errorProvider1.SetError(textBoxQuantity, quantityError);
                isValid = false;
            }

            //Operations validation
            List<string> checkedItems = new List<string>();

            foreach (var item in checkedListBox1.CheckedItems)
            {
                checkedItems.Add(item.ToString());
            }

            checkedOperations = string.Join(", ", checkedItems);

            string operationsError = newDV.validateOperations(checkedOperations);
            if (!string.IsNullOrEmpty(operationsError))
            {
                labelOperationsError.Text = operationsError;
                errorProvider2.SetError(checkedListBox1, operationsError);
                isValid = false;
            }

            //Purchase order number validation
            string poError = newDV.validatePurchaseOrderNumber(poNumber);
            if (!string.IsNullOrEmpty(poError))
            {
                labelPOError.Text = poError;
                errorProvider3.SetError(textBoxPO, poError);
                isValid = false;
            }

            //Date received validation
            string dateReceivedError = newDV.validateDateReceived(dateReceived);
            if (!string.IsNullOrEmpty(dateReceivedError))
            {
                labelDateReceivedError.Text = dateReceivedError;
                errorProviderDateReceived.SetError(textBoxDateReceived, dateReceivedError);
                isValid = false;
            }

            //Due date validation
            string dateDueError = newDV.validateDueDate(dateDue);

            if (!string.IsNullOrEmpty(dateDueError))
            {
                labelDueDateError.Text = dateDueError;
                errorProvider3.SetError(textboxDueDate, dateDueError);
                isValid = false;
            }


            //final check before to confirm
            if (isValid)
            {
                DateTime parsedDateReceived = DateTime.ParseExact(dateReceived, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime parsedDueDate = DateTime.ParseExact(dateDue, "MM/dd/yyyy", CultureInfo.InvariantCulture);
       
                //TO DO: Create DAO object and Function call to Update Record.
                DAO newDAO = new DAO();
                newDAO.UpdateRecord(partHistoryID, custID, opID, partNumber, parsedDueDate, poNumber, quantity, checkedOperations, parsedDateReceived, 0);

                labelRecordStatus.Text = "Record Status: Record Successfully Updated!";

            }

            else
            {

                labelRecordStatus.Text = "Record Status: Record Update Error!";

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
            textboxDueDate.Clear();
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
            labelDueDateError.Text = "";
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

        private void UpdateRecord_Load(object sender, EventArgs e)
        {

        }
    }
}
