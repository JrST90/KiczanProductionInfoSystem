using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    internal partial class Form1 : Form
    {
        //Set pageSize and currentPageIndex.
        private const int pageSize = 20;
        private int currentPageIndex = 1;

        //Set initial values for totalpages and totalRows.
        private int totalPages = 0;
        private int totalRows = 0;

        //Create and initialize a new BindingSource object.
        private BindingSource dataBaseSource = new BindingSource();

        //List to hold operator names returned from function.
        private List<Operators> operators = new List<Operators>();

        //set initial values for dateRange and partNumber to store user input for comparison.
        string partNumber = "";
        string dateRange = "";
        string opName = "";

        //Create new DAO Object.
        private DAO newDAO = new DAO();

        //Create new DataValidation Object for input validation.
        private DataValidation newDV = new DataValidation();

        //Create new Users object for authorization.
        private readonly Users _currentUser;
        
        internal Form1(Users currentUser)
        {
            InitializeComponent();
            this.Text = "Kiczan Production Information System";
            this.WindowState = FormWindowState.Normal;
            this.ClientSize = new System.Drawing.Size(1205, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            //Assign the passed currentUser for privileged access.
            _currentUser = currentUser;

            //If the user name exists, use the returned object properties to set role privileges.
            if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
            {
                this.Text = "Kiczan Production Information System | Current User: " + _currentUser.USER_NAME + " | " + "User Role: " + _currentUser.ROLE_NAME;

                //Enable Create Record Button.
                button2.Enabled = true;
                button2.Visible = true;
            }
            else
            {
                this.Text = "Kiczan Production Information System | Current User: " + _currentUser.USER_NAME + " | " + "User Role: " + _currentUser.ROLE_NAME;

                //Enable Create Record Button.
                button2.Enabled = false;
                button2.Visible = false;
            }

            //Set up the DataError event for "ROW_VERSION" dynamically.
            dataGridView1.DataError += DataGridView1_DataError;
        }

        //Handles the DataGridView error caused by "ROW_VERSION" byte[] datatype.
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Ignore formatting errors from "ROW_VERSION" byte[] datatype that is hidden from the user.
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ROW_VERSION")
            {
                e.ThrowException = false;
                e.Cancel = true;
            }
        }


        //Events to occur on button click for selection from comboBox1.
        private async void button1_Click(object sender, EventArgs e)
        {
            string errorType = string.Empty;

            //Get index value from comboBox1 after user selection.
            int searchTypeIndex = comboBox1.SelectedIndex;

            //Button click event is dependent on selected index from comboBox1.
            switch (searchTypeIndex)
            {
                //Search By Due Date Range, gets value from textBox1.
                case 0:
                    //Store user input from textBox1 into dateRange variable.
                    dateRange = textBox1.Text;

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    { 
                        //Enable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = true;
                        updateRecordToolStripMenuItem.Visible = true;

                        //Enable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = true;
                        deleteRecordArchiveToolStripMenuItem.Visible = true;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable update record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    
                    //if dateRange is of the right format and the beginning date is before the end date, execute the search query.
                    if (newDV.validateDateRange(dateRange, out errorType, out DateTime cleanStartDate, out DateTime cleanEndDate))
                    {
                        //Set currentPageIndex for query.
                        currentPageIndex = 1;

                        try
                        {
                            //Bind dataBaseSource to dateDueRangeQuery results, passing the arguments dateRange, pageSize, and currentPageIndex.
                            dataBaseSource.DataSource = await newDAO.dateDueRangeQuery(cleanStartDate, cleanEndDate, pageSize, currentPageIndex);

                            //Bind dataGridView1 to dataBaseSource.
                            dataGridView1.DataSource = dataBaseSource;

                            //Hide the "ROW_VERSION" column.
                            if (dataGridView1.Columns.Contains("ROW_VERSION"))
                            {
                                dataGridView1.Columns["ROW_VERSION"].Visible = false;
                            }

                            //Get the total rows returned by dateDueRangeQueryCount with passed argument dateRange and set to variable totalRows.
                            totalRows = await newDAO.dateDueRangeQueryCount(cleanStartDate, cleanEndDate);

                            //Divide the totalRows variable by the pageSize variable and set value to totalPages variable.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set label2's text value to the value of totalRows.
                            label2.Text = "Number of Records: " + totalRows;

                            //Set label5's text value to the value of totalPages.
                            label5.Text = "Total Pages: " + totalPages;

                            //Set label6's text value to the value of currentPageIndex.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Clear errorProvider1.
                            errorProvider1.Clear();

                            //Clear label3's text value of error state.
                            label3.Text = "";

                            //Set label4's value to alert the user of a succsessful query.
                            label4.Text = "QUERY SUCCESS";

                            //Set label4's color to green.
                            label4.ForeColor = Color.Green;
                        }
                        catch(DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        totalRows = 0;
                        totalPages = 0;

                        label2.Text = "Number of Records: " + totalRows;
                        label5.Text = "Total Pages: " + totalPages;
                        label6.Text = "";

                        dateRange = "";
                        currentPageIndex = 1;
                        dataBaseSource.DataSource = null;
                        dataGridView1.DataSource = null;
                        label4.Text = "QUERY FAILURE";
                        label4.ForeColor = Color.Red;

                        if(errorType == "FORMAT_ERROR")
                        {
                            string msg = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";
                            errorProvider1.SetError(textBox1, msg);
                            label3.Text = msg;
                        }
                        else if(errorType == "ORDER_ERROR")
                        {
                            string msg = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";
                            errorProvider1.SetError(textBox1, msg);
                            label3.Text = msg;
                        }
                    }
                break;

                //Search By Part Number, gets value from textBox1.
                case 1:
                    //Store user input from textBox1 into partNumber variable.
                    partNumber = textBox1.Text;

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    {
                        //Enable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = true;
                        updateRecordToolStripMenuItem.Visible = true;

                        //Enable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = true;
                        deleteRecordArchiveToolStripMenuItem.Visible = true;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable update record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    try
                    {
                        //If the partNumber is of the right format and the number of matching records is greater than 0, execute the search query.
                        if (newDV.validatePartNumber(partNumber, out errorType))
                        {
                            //Cache row count locally.
                            totalRows = await newDAO.partNumberQueryCount(partNumber);
                            //Set currentPageIndex for query.
                            currentPageIndex = 1;

                            try
                            {
                                //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                                dataBaseSource.DataSource = await newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);

                                //Bind dateGridView1 to dataBaseSource.
                                dataGridView1.DataSource = dataBaseSource;

                                //Hide the "ROW_VERSION" column.
                                if (dataGridView1.Columns.Contains("ROW_VERSION"))
                                {
                                    dataGridView1.Columns["ROW_VERSION"].Visible = false;
                                }

                                //Calculate total pages based on the cached totalRows.
                                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                //Set UI element text values.
                                label2.Text = "Number of Records: " + totalRows;
                                label4.Text = "QUERY SUCCESS";
                                label4.ForeColor = Color.Green;
                                label5.Text = "Total Pages: " + totalPages;

                                if (totalRows > 0)
                                {
                                    label6.Text = "Current Page: " + currentPageIndex;
                                    errorProvider1.Clear();
                                    label3.Text = "";
                                }
                                else
                                {
                                    label6.Text = "";
                                    string noRecordsMsg = "No Records Found.";
                                    errorProvider1.SetError(textBox1, noRecordsMsg);
                                    label3.Text = noRecordsMsg;
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                        //Alert the user that the input partNumber is not of the right format.
                        else
                        {
                            totalRows = 0;
                            totalPages = 0;
                            currentPageIndex = 1;
                            partNumber = "";

                            label2.Text = "Number of Records: " + totalRows;
                            label4.Text = "QUERY FAILURE";
                            label4.ForeColor = Color.Red;
                            label5.Text = "Total Pages: " + totalPages;
                            label6.Text = "";

                            dataBaseSource.DataSource = null;
                            dataGridView1.DataSource = null;

                            if (errorType == "EMPTY_ERROR")
                            {
                                string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                            else if (errorType == "WHITESPACE_ERROR")
                            {
                                string msg = "Part Number cannot contain spaces.";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                            else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                            {
                                string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                        }
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;

                //Search By Operator Name, gets value from comboBox2.
                case 2:
                    //Store user input from comboBox2 into opName variable.
                    opName = comboBox2.Text;

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    {
                        //Enable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = true;
                        updateRecordToolStripMenuItem.Visible = true;

                        //Enable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = true;
                        deleteRecordArchiveToolStripMenuItem.Visible = true;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable update record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(opName))
                        {
                            //Cache row count locally.
                            totalRows = await newDAO.operatorNameQueryCount(opName);
                            //Set currentPageIndex for query.
                            currentPageIndex = 1;

                            try
                            {
                                //Bind dataBaseSource to operatorQuery results passing the arguments of the operator name from comboBox2, pageSize, and currentPageIndex.
                                dataBaseSource.DataSource = await newDAO.operatorNameQuery(opName, pageSize, currentPageIndex);

                                //Bind dateGridView to dataBaseSource.
                                dataGridView1.DataSource = dataBaseSource;

                                //Hide the "ROW_VERSION" column.
                                if (dataGridView1.Columns.Contains("ROW_VERSION"))
                                {
                                    dataGridView1.Columns["ROW_VERSION"].Visible = false;
                                }

                                //Calculate the total pages based on the cached total rows.
                                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                //Set UI element text values.
                                label2.Text = "Number of Records: " + totalRows;
                                label4.Text = "QUERY SUCCESS";
                                label4.ForeColor = Color.Green;
                                label5.Text = "Total Pages: " + totalPages;

                                if (totalRows > 0)
                                {
                                    label6.Text = "Current Page: " + currentPageIndex;
                                    errorProvider1.Clear();
                                    label3.Text = "";
                                }
                                else
                                {
                                    label6.Text = "";
                                    string noRecordsMsg = "No Records Found.";
                                    errorProvider1.SetError(textBox1, noRecordsMsg);
                                    label3.Text = noRecordsMsg;
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                        //Alert the user that an operator name must be selected.
                        else
                        {
                            totalRows = 0;
                            totalPages = 0;
                            currentPageIndex = 1;
                            opName = "";

                            label2.Text = "Number of Records: " + totalRows;
                            label4.Text = "QUERY FAILURE";
                            label4.ForeColor = Color.Red;
                            label5.Text = "Total Pages: " + totalPages;
                            label6.Text = "";

                            dataBaseSource.DataSource = null;
                            dataGridView1.DataSource = null;

                            string msg = "Please select an Operator Name.";
                            errorProvider1.SetError(textBox1, msg);
                            label3.Text = msg;
                        }
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;

                //Search by Fabrication Department.
                case 3:

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    {
                        //Enable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = true;
                        updateRecordToolStripMenuItem.Visible = true;

                        //Enable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = true;
                        deleteRecordArchiveToolStripMenuItem.Visible = true;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable update record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }

                    //Set currentPageIndex for query.
                    currentPageIndex = 1;

                    try
                    {
                        //Bind dataBaseSource to fabricationDepartmentQuery results passing the arguments of pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = await newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                        //Bind dateGridView to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Hide the "ROW_VERSION" column.
                        if (dataGridView1.Columns.Contains("ROW_VERSION"))
                        {
                            dataGridView1.Columns["ROW_VERSION"].Visible = false;
                        }

                        //Get the total rows returned by fabricationDepartmentQueryCount.
                        totalRows = await newDAO.fabricationDepartmentQueryCount();

                        //Divide the totalRows variable by the pageSize variable and set value to totalPages variable.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Set label6's text value to the value of currentPageIndex.
                        label6.Text = "Current Page: " + currentPageIndex;

                        //Clear errorProvider1.
                        errorProvider1.Clear();

                        //Set label4's value to alert the user of a succsessful query.
                        label4.Text = "QUERY SUCCESS";

                        //Set label4's color to green.
                        label4.ForeColor = Color.Green;
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;

                //Search by Machining Department.
                case 4:

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    {
                        //Enable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = true;
                        updateRecordToolStripMenuItem.Visible = true;

                        //Enable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = true;
                        deleteRecordArchiveToolStripMenuItem.Visible = true;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable update record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;

                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;
                    }

                    //Set currentPageIndex for query.
                    currentPageIndex = 1;

                    try
                    {
                        //Bind dataBaseSource to fabricationDepartmentQuery results passing the arguments of pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = await newDAO.machiningDepartmentQuery(pageSize, currentPageIndex);

                        //Bind dateGridView to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Hide the "ROW_VERSION" column.
                        if (dataGridView1.Columns.Contains("ROW_VERSION"))
                        {
                            dataGridView1.Columns["ROW_VERSION"].Visible = false;
                        }

                        //Get the total rows returned by fabricationDepartmentQueryCount.
                        totalRows = await newDAO.machiningDepartmentQueryCount();

                        //Divide the totalRows variable by the pageSize variable and set value to totalPages variable.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Set label6's text value to the value of currentPageIndex.
                        label6.Text = "Current Page: " + currentPageIndex;

                        //Clear errorProvider1.
                        errorProvider1.Clear();

                        //Set label4's value to alert the user of a succsessful query.
                        label4.Text = "QUERY SUCCESS";

                        //Set label4's color to green.
                        label4.ForeColor = Color.Green;
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;

                //Search By Part Number in Archive, gets value from textBox1.
                case 5:
                    //Store user input from textBox1 into partNumber variable.
                    partNumber = textBox1.Text;

                    //User Role check for CRUD access
                    if (_currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager")
                    {
                        //Enable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = true;
                        restoreRecordMainTableToolStripMenuItem.Visible = true;

                        //Disable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        //Disable restore record option in menu strip.
                        restoreRecordMainTableToolStripMenuItem.Enabled = false;
                        restoreRecordMainTableToolStripMenuItem.Visible = false;

                        //Disable update and delete record options in menu strip.
                        updateRecordToolStripMenuItem.Enabled = false;
                        updateRecordToolStripMenuItem.Visible = false;

                        //Disable delete record option in menu strip.
                        deleteRecordArchiveToolStripMenuItem.Enabled = false;
                        deleteRecordArchiveToolStripMenuItem.Visible = false;
                    }
                    try
                    {
                        //If the partNumber is of the right format, execute the search query.
                        if (newDV.validatePartNumber(partNumber, out errorType))
                        {
                            //Cache row count locally.
                            totalRows = await newDAO.partNumberQueryCountArchive(partNumber);
                            //Set currentPageIndex for query.
                            currentPageIndex = 1;

                            try
                            {
                                //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                                dataBaseSource.DataSource = await newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);

                                //Bind dateGridView1 to dataBaseSource.
                                dataGridView1.DataSource = dataBaseSource;

                                //Hide the "ROW_VERSION" column.
                                if (dataGridView1.Columns.Contains("ROW_VERSION"))
                                {
                                    dataGridView1.Columns["ROW_VERSION"].Visible = false;
                                }

                                //Calculate total pages based on the cached total rows.
                                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                //Set UI element text values.
                                label2.Text = "Number of Records: " + totalRows;
                                label4.Text = "QUERY SUCCESS";
                                label4.ForeColor = Color.Green;
                                label5.Text = "Total Pages: " + totalPages;

                                if(totalRows > 0)
                                {
                                    label6.Text = "Current Page: " + currentPageIndex;
                                    errorProvider1.Clear();
                                    label3.Text = "";
                                }
                                else
                                {
                                    label6.Text = "";
                                    string noRecordMsg = "No Records Found.";
                                    errorProvider1.SetError(textBox1, noRecordMsg);
                                    label3.Text = noRecordMsg;
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            totalRows = 0;
                            totalPages = 0;
                            currentPageIndex = 1;
                            partNumber = "";

                            label2.Text = "Number of Records: " + totalRows;
                            label4.Text = "QUERY FAILURE";
                            label4.ForeColor = Color.Red;
                            label5.Text = "Total Pages: " + totalPages;
                            label6.Text = "";

                            dataBaseSource.DataSource = null;
                            dataGridView1.DataSource = null;                            

                            if (errorType == "EMPTY_ERROR")
                            {
                                string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                            else if (errorType == "WHITESPACE_ERROR")
                            {
                                string msg = "Part Number cannot contain spaces.";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                            else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                            {
                                string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                errorProvider1.SetError(textBox1, msg);
                                label3.Text = msg;
                            }
                        }
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;
            }
        }

        //Events to occur on drop down selector change for comboBox1.
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get index value from comboBox1 after user selection.
            int searchTypeIndex = comboBox1.SelectedIndex;

            switch (searchTypeIndex)
            {
                //Search By Due Date Range.
                case 0:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Input example for date range search.
                    textBox1.Text = "MM/DD/YYYY-MM/DD/YYYY";

                    //Clear the list storing operators.
                    operators.Clear();

                    //Clear the datasource for comboBox2.
                    comboBox2.DataSource = null;

                    //Clear the dataBaseSource.
                    dataBaseSource.DataSource = null;

                    //Clear the dataGridView1 source.
                    dataGridView1.DataSource = null;

                    //Clear the datagridView1 rows.
                    dataGridView1.Rows.Clear();

                    //Clear errorProvider1.
                    errorProvider1.Clear();

                    //Reset currentPageIndex.
                    currentPageIndex = 1;

                    //Clear all label text values.
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                break;
                //Search By Part Number.
                case 1:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Clear the list storing operators.
                    operators.Clear();

                    //Clear the datasource for comboBox2.
                    comboBox2.DataSource = null;

                    //Clear the dataBaseSource.
                    dataBaseSource.DataSource = null;

                    //Clear the dataGridView1 source.
                    dataGridView1.DataSource = null;

                    //Clear the datagridView1 rows.
                    dataGridView1.Rows.Clear();

                    //Clear errorProvider1.
                    errorProvider1.Clear();

                    //Reset currentPageIndex.
                    currentPageIndex = 1;

                    //Clear all label text values.
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                break;
                //Search By Operator Name.
                case 2:
                    //Clear all label text values.
                    textBox1.Clear();

                    try
                    {
                        //Set the operators List object to be populated with the list returned by GetOperators().
                        operators = await newDAO.GetOperators();

                        //Bind comboBox2's datasource to the operators List.
                        comboBox2.DataSource = operators;

                        //Set the display values.
                        comboBox2.DisplayMember = "OPERATOR_NAME";

                        //Set the stored values.
                        comboBox2.ValueMember = "OPERATOR_NAME";

                        //Clear the dataBaseSource when the operator name is changed.
                        dataBaseSource.DataSource = null;

                        //Clear the dataGridView1 source when the operator name is changed.
                        dataGridView1.DataSource = null;

                        //Clear the rows when the operator name is changed.
                        dataGridView1.Rows.Clear();

                        //Clear errorProvider1.
                        errorProvider1.Clear();

                        //Reset currentPageIndex.
                        currentPageIndex = 1;

                        //Clear all label text values.
                        label2.Text = "";
                        label3.Text = "";
                        label4.Text = "";
                        label5.Text = "";
                        label6.Text = "";
                    }
                    catch (DatabaseQueryException dbEx)
                    {
                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        label4.Text = "SYSTEM ERROR";
                        label4.ForeColor = Color.Red;
                    }
                break;
                //Search by Fabrication Department.
                case 3:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Clear the list storing operators.
                    operators.Clear();

                    //Clear the datasource for comboBox2.
                    comboBox2.DataSource = null;

                    //Clear the dataBaseSource.
                    dataBaseSource.DataSource = null;

                    //Clear the dataGridView1 source.
                    dataGridView1.DataSource = null;

                    //Clear the datagridView1 rows.
                    dataGridView1.Rows.Clear();

                    //Clear errorProvider1.
                    errorProvider1.Clear();

                    //Reset currentPageIndex.
                    currentPageIndex = 1;

                    //Clear all label text values.
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                break;
                //Search by Machining Department.
                case 4:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Clear the list storing operators.
                    operators.Clear();

                    //Clear the datasource for comboBox2.
                    comboBox2.DataSource = null;

                    //Clear the dataBaseSource.
                    dataBaseSource.DataSource = null;

                    //Clear the dataGridView1 source.
                    dataGridView1.DataSource = null;

                    //Clear the datagridView1 rows.
                    dataGridView1.Rows.Clear();

                    //Clear errorProvider1.
                    errorProvider1.Clear();

                    //Reset currentPageIndex.
                    currentPageIndex = 1;

                    //Clear all label text values.
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                break;

                //Search by Part Number in Archive.
                case 5:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Clear the list storing operators.
                    operators.Clear();

                    //Clear the datasource for comboBox2.
                    comboBox2.DataSource = null;

                    //Clear the dataBaseSource.
                    dataBaseSource.DataSource = null;

                    //Clear the dataGridView1 source.
                    dataGridView1.DataSource = null;

                    //Clear the datagridView1 rows.
                    dataGridView1.Rows.Clear();

                    //Clear errorProvider1.
                    errorProvider1.Clear();

                    //Reset currentPageIndex.
                    currentPageIndex = 1;

                    //Clear all label text values.
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                break;
            }
        }

        //Event handler for the changing of an operator name in comboBox2.
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear the dataBaseSource.
            dataBaseSource.DataSource = null;

            //Clear the dataGridView1 source.
            dataGridView1.DataSource = null;

            //Clear the datagridView1 rows.
            dataGridView1.Rows.Clear();

            //Clear errorProvider1.
            errorProvider1.Clear();

            //Reset currentPageIndex.
            currentPageIndex = 1;

            //Clear all label text values.
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
        }

        //Event handler for Next Button.
        private async void button4_Click(object sender, EventArgs e)
        {
            string errorType = string.Empty;

            if (currentPageIndex < totalPages && dataBaseSource.DataSource != null)
            {
                //Get index value from comboBox1 after user selection.
                int searchType = comboBox1.SelectedIndex;

                switch (searchType)
                {
                    case 0:
                        //If the dateRange variable is still the same value as textBox1.Text and the next button is pressed.
                        if (dateRange == textBox1.Text)
                        {
                            if (newDV.validateDateRange(dateRange, out _, out DateTime cleanStartDate, out DateTime cleanEndDate))
                            {
                                currentPageIndex++;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.dateDueRangeQuery(cleanStartDate, cleanEndDate, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Datebase Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the date range input into textBox1 has changed, apply Auto-Reset pattern.
                        else
                        {
                            //Validate new input.
                            if (newDV.validateDateRange(textBox1.Text, out errorType, out DateTime newStartDate, out DateTime newEndDate))
                            {
                                //Sync with new text box input.
                                dateRange = textBox1.Text;

                                //Reset page counter.
                                currentPageIndex = 1;

                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    //Execute fresh query.
                                    dataBaseSource.DataSource = await newDAO.dateDueRangeQuery(newStartDate, newEndDate, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;

                                    //Recalculate row counts and total pages.
                                    totalRows = await newDAO.dateDueRangeQueryCount(newStartDate, newEndDate);

                                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                    //Update UI.
                                    label2.Text = "Number of Records: " + totalRows;
                                    label3.Text = "";
                                    label4.Text = "QUERY SUCCESS";
                                    label4.ForeColor = Color.Green;
                                    label5.Text = "Total Pages: " + totalPages;
                                    errorProvider1.Clear();
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                            //The input changed and is invalid.
                            else
                            {
                                //Wipe state and clear datagrid.
                                dateRange = "";
                                currentPageIndex = 1;
                                dataBaseSource.DataSource = null;
                                dataGridView1.DataSource = null;

                                totalRows = 0;
                                totalPages = 0;

                                label2.Text = "Number of Records: " + totalRows;
                                label4.Text = "QUERY FAILURE";
                                label4.ForeColor = Color.Red;
                                label5.Text = "Total Pages: " + totalPages;
                                label6.Text = "";

                                //Target specific UI error.
                                if (errorType == "FORMAT_ERROR")
                                {
                                    string msg = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";
                                    errorProvider1.SetError(textBox1, msg);
                                    label3.Text = msg;
                                }
                                else if (errorType == "ORDER_ERROR")
                                {
                                    string msg = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";
                                    errorProvider1.SetError(textBox1, msg);
                                    label3.Text = msg;
                                }
                            }
                        }
                    break;

                    case 1:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            if (newDV.validatePartNumber(partNumber, out errorType))
                            {
                                currentPageIndex++;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Datebase Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the part number input into textBox1.Text has changed, apply auto-reset.
                        else
                        {
                            try
                            {
                                //Validate new input.
                                if (newDV.validatePartNumber(textBox1.Text, out errorType))
                                {
                                    //Sync with new text box input.
                                    partNumber = textBox1.Text;
                                    //Cache row count locally, reset page counter..
                                    totalRows = await newDAO.partNumberQueryCount(partNumber);
                                    currentPageIndex = 1;

                                    try
                                    {
                                        //Execute fresh query.
                                        dataBaseSource.DataSource = await newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);
                                        dataGridView1.DataSource = dataBaseSource;

                                        //Calculate total pages based on the cached total rows.
                                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                        //Update UI.
                                        label2.Text = "Number of Records: " + totalRows;
                                        label4.Text = "QUERY SUCCESS";
                                        label4.ForeColor = Color.Green;
                                        label5.Text = "Total Pages: " + totalPages;

                                        if(totalRows > 0)
                                        {
                                            label6.Text = "Current Page: " + currentPageIndex;
                                            errorProvider1.Clear();
                                            label3.Text = "";
                                        }
                                        else
                                        {
                                            label6.Text = "";
                                            string noRecordsMsg = "No Records Found";
                                            errorProvider1.SetError(textBox1 , noRecordsMsg);
                                            label3 .Text = noRecordsMsg;
                                        }
                                    }
                                    catch (DatabaseQueryException dbEx)
                                    {
                                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        label4.Text = "SYSTEM ERROR";
                                        label4.ForeColor = Color.Red;
                                    }
                                }
                                //The input changed and is invalid.
                                else
                                {
                                    //Wipe state and clear datagrid.
                                    partNumber = "";
                                    totalRows = 0;
                                    totalPages = 0;
                                    currentPageIndex = 1;
                                    dataBaseSource.DataSource = null;
                                    dataGridView1.DataSource = null;

                                    label2.Text = "Number of Records: " + totalRows;
                                    label4.Text = "QUERY FAILURE";
                                    label4.ForeColor = Color.Red;
                                    label5.Text = "Total Pages: " + totalPages;
                                    label6.Text = "";
                                    
                                    //Target specific UI error.
                                    if (errorType == "EMPTY_ERROR")
                                    {
                                        string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "WHITESPACE_ERROR")
                                    {
                                        string msg = "Part Number cannot contain spaces.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                                    {
                                        string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }               
                    break;

                    case 2:
                        //Incremement page counter index.
                        currentPageIndex++;

                        //Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            //Query to update page.
                            dataBaseSource.DataSource = await newDAO.operatorNameQuery(comboBox2.Text, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 3:
                        //Incremement page counter index.
                        currentPageIndex++;

                        //Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            //Query to update page.
                            dataBaseSource.DataSource = await newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 4:
                        //Incremement page counter index.
                        currentPageIndex++;

                        //Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            //Query to update page.
                            dataBaseSource.DataSource = await newDAO.machiningDepartmentQuery(pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 5:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            if (newDV.validatePartNumber(partNumber, out errorType))
                            {
                                currentPageIndex++;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the part number input into textBox1.Text has changed, apply auto-reset.
                        else
                        {
                            try
                            {
                                //Validate new input.
                                if (newDV.validatePartNumber(textBox1.Text, out errorType))
                                {
                                    //Sync with new text box input.
                                    partNumber = textBox1.Text;
                                    //Cache row count locally, reset page counter..
                                    totalRows = await newDAO.partNumberQueryCountArchive(partNumber);
                                    currentPageIndex = 1;

                                    try
                                    {
                                        //Execute fresh query.
                                        dataBaseSource.DataSource = await newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);
                                        dataGridView1.DataSource = dataBaseSource;

                                        //Calculate total pages based on the cached total rows.
                                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                        //Update UI.
                                        label2.Text = "Number of Records: " + totalRows;
                                        label4.Text = "QUERY SUCCESS";
                                        label4.ForeColor = Color.Green;
                                        label5.Text = "Total Pages: " + totalPages;

                                        if (totalRows > 0)
                                        {
                                            label6.Text = "Current Page: " + currentPageIndex;
                                            errorProvider1.Clear();
                                            label3.Text = "";
                                        }
                                        else
                                        {
                                            label6.Text = "";
                                            string noRecordsMsg = "No Records Found";
                                            errorProvider1.SetError(textBox1, noRecordsMsg);
                                            label3.Text = noRecordsMsg;
                                        }
                                    }
                                    catch (DatabaseQueryException dbEx)
                                    {
                                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        label4.Text = "SYSTEM ERROR";
                                        label4.ForeColor = Color.Red;
                                    }
                                }
                                //The input changed and is invalid.
                                else
                                {
                                    //Wipe state and clear datagrid.
                                    partNumber = "";
                                    totalRows = 0;
                                    totalPages = 0;
                                    currentPageIndex = 1;
                                    dataBaseSource.DataSource = null;
                                    dataGridView1.DataSource = null;

                                    label2.Text = "Number of Records: " + totalRows;
                                    label4.Text = "QUERY FAILURE";
                                    label4.ForeColor = Color.Red;
                                    label5.Text = "Total Pages: " + totalPages;
                                    label6.Text = "";
                                    
                                    //Target specific UI error.
                                    if (errorType == "EMPTY_ERROR")
                                    {
                                        string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "WHITESPACE_ERROR")
                                    {
                                        string msg = "Part Number cannot contain spaces.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                                    {
                                        string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                    break;
                }
            }
        }
        //Event handler for Previous Button.
        private async void button5_Click(object sender, EventArgs e)
        {
            string errorType = string.Empty;

            if (currentPageIndex > 1)
            {
                //Get index value from comboBox1 after user selection.
                int searchType = comboBox1.SelectedIndex;

                switch (searchType)
                {
                    case 0:
                        //If the dateRange variable is still the same value as textBox1.Text and the next button is pressed.
                        if (dateRange == textBox1.Text)
                        {
                            if (newDV.validateDateRange(dateRange, out _, out DateTime cleanStartDate, out DateTime cleanEndDate))
                            {
                                currentPageIndex--;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.dateDueRangeQuery(cleanStartDate, cleanEndDate, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the date range input into textBox1 has changed. Apply Auto-Reset pattern.
                        else
                        {
                            //Validate new input.
                            if (newDV.validateDateRange(textBox1.Text, out errorType, out DateTime newStartDate, out DateTime newEndDate))
                            {
                                //Sync with new text box input.
                                dateRange = textBox1.Text;

                                //Reset page counter.
                                currentPageIndex = 1;

                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    //Execute fresh query.
                                    dataBaseSource.DataSource = await newDAO.dateDueRangeQuery(newStartDate, newEndDate, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;

                                    //Recalculate row counts and total pages.
                                    totalRows = await newDAO.dateDueRangeQueryCount(newStartDate, newEndDate);

                                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                    //Update UI.
                                    label2.Text = "Number of Records: " + totalRows;
                                    label5.Text = "Total Pages: " + totalPages;
                                    label4.Text = "QUERY SUCCESS";
                                    label4.ForeColor = Color.Green;

                                    errorProvider1.Clear();
                                    label3.Text = "";
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                            //The input changed and is invalid.
                            else
                            {
                                //Wipe state and clear datagrid.
                                dateRange = "";
                                currentPageIndex = 1;
                                dataBaseSource.DataSource = null;
                                dataGridView1.DataSource = null;

                                totalRows = 0;
                                totalPages = 0;

                                label2.Text = "Number of Records: " + totalRows;
                                label5.Text = "Total Pages: " + totalPages;
                                label6.Text = "";
                                label4.Text = "QUERY FAILURE";
                                label4.ForeColor = Color.Red;

                                //Target specific UI error.
                                if (errorType == "FORMAT_ERROR")
                                {
                                    string msg = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";
                                    errorProvider1.SetError(textBox1, msg);
                                    label3.Text = msg;
                                }
                                else if (errorType == "ORDER_ERROR")
                                {
                                    string msg = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";
                                    errorProvider1.SetError(textBox1, msg);
                                    label3.Text = msg;
                                }
                            }
                        }
                    break;

                    case 1:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            if (newDV.validatePartNumber(partNumber, out errorType))
                            {
                                currentPageIndex--;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the part number input into textBox1.Text has changed, apply auto-reset.
                        else
                        {
                            try
                            {
                                //Validate new input.
                                if (newDV.validatePartNumber(textBox1.Text, out errorType))
                                {
                                    //Sync with new text box input.
                                    partNumber = textBox1.Text;
                                    //Cache row count locally, reset page counter..
                                    totalRows = await newDAO.partNumberQueryCount(partNumber);
                                    currentPageIndex = 1;

                                    try
                                    {
                                        //Execute fresh query.
                                        dataBaseSource.DataSource = await newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);
                                        dataGridView1.DataSource = dataBaseSource;

                                        //Calculate total pages based on the cached total rows.
                                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                        //Update UI.
                                        label2.Text = "Number of Records: " + totalRows;
                                        label4.Text = "QUERY SUCCESS";
                                        label4.ForeColor = Color.Green;
                                        label5.Text = "Total Pages: " + totalPages;

                                        if (totalRows > 0)
                                        {
                                            label6.Text = "Current Page: " + currentPageIndex;
                                            errorProvider1.Clear();
                                            label3.Text = "";
                                        }
                                        else
                                        {
                                            label6.Text = "";
                                            string noRecordsMsg = "No Records Found";
                                            errorProvider1.SetError(textBox1, noRecordsMsg);
                                            label3.Text = noRecordsMsg;
                                        }
                                    }
                                    catch (DatabaseQueryException dbEx)
                                    {
                                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        label4.Text = "SYSTEM ERROR";
                                        label4.ForeColor = Color.Red;
                                    }
                                }
                                //The input changed and is invalid.
                                else
                                {
                                    //Wipe state and clear datagrid.
                                    partNumber = "";
                                    totalRows = 0;
                                    totalPages = 0;
                                    currentPageIndex = 1;
                                    dataBaseSource.DataSource = null;
                                    dataGridView1.DataSource = null;

                                    label2.Text = "Number of Records: " + totalRows;
                                    label4.Text = "QUERY FAILURE";
                                    label4.ForeColor = Color.Red;
                                    label5.Text = "Total Pages: " + totalPages;
                                    label6.Text = "";
                                    
                                    //Target specific UI error.
                                    if (errorType == "EMPTY_ERROR")
                                    {
                                        string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "WHITESPACE_ERROR")
                                    {
                                        string msg = "Part Number cannot contain spaces.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                                    {
                                        string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                    break;

                    case 2:
                        //Decrement page counter index.
                        currentPageIndex--;

                        //Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            //Query to update page.
                            dataBaseSource.DataSource = await newDAO.operatorNameQuery(comboBox2.Text, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 3:
                        // Decrement page counter index.
                        currentPageIndex--;

                        // Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            // Query to update page.
                            dataBaseSource.DataSource = await newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                            // Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 4:
                        // Decrement page counter index.
                        currentPageIndex--;

                        // Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        try
                        {
                            // Query to update page.
                            dataBaseSource.DataSource = await newDAO.machiningDepartmentQuery(pageSize, currentPageIndex);

                            // Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        catch (DatabaseQueryException dbEx)
                        {
                            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            label4.Text = "SYSTEM ERROR";
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 5:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            if (newDV.validatePartNumber(partNumber, out errorType))
                            {
                                currentPageIndex--;
                                label6.Text = "Current Page: " + currentPageIndex;

                                try
                                {
                                    dataBaseSource.DataSource = await newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);
                                    dataGridView1.DataSource = dataBaseSource;
                                }
                                catch (DatabaseQueryException dbEx)
                                {
                                    MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    label4.Text = "SYSTEM ERROR";
                                    label4.ForeColor = Color.Red;
                                }
                            }
                        }
                        //If the part number input into textBox1.Text has changed, apply auto-reset.
                        else
                        {
                            try
                            {
                                //Validate new input.
                                if (newDV.validatePartNumber(textBox1.Text, out errorType))
                                {
                                    //Sync with new text box input.
                                    partNumber = textBox1.Text;
                                    //Cache row count locally, reset page counter..
                                    totalRows = await newDAO.partNumberQueryCountArchive(partNumber);
                                    currentPageIndex = 1;

                                    try
                                    {
                                        //Execute fresh query.
                                        dataBaseSource.DataSource = await newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);
                                        dataGridView1.DataSource = dataBaseSource;

                                        //Calculate total pages based on the cached total rows.
                                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                                        //Update UI.
                                        label2.Text = "Number of Records: " + totalRows;
                                        label4.Text = "QUERY SUCCESS";
                                        label4.ForeColor = Color.Green;
                                        label5.Text = "Total Pages: " + totalPages;

                                        if (totalRows > 0)
                                        {
                                            label6.Text = "Current Page: " + currentPageIndex;
                                            errorProvider1.Clear();
                                            label3.Text = "";
                                        }
                                        else
                                        {
                                            label6.Text = "";
                                            string noRecordsMsg = "No Records Found";
                                            errorProvider1.SetError(textBox1, noRecordsMsg);
                                            label3.Text = noRecordsMsg;
                                        }
                                    }
                                    catch (DatabaseQueryException dbEx)
                                    {
                                        MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        label4.Text = "SYSTEM ERROR";
                                        label4.ForeColor = Color.Red;
                                    }
                                }
                                //The input changed and is invalid.
                                else
                                {
                                    //Wipe state and clear datagrid.
                                    partNumber = "";
                                    totalRows = 0;
                                    totalPages = 0;
                                    currentPageIndex = 1;
                                    dataBaseSource.DataSource = null;
                                    dataGridView1.DataSource = null;

                                    label2.Text = "Number of Records: " + totalRows;
                                    label4.Text = "QUERY FAILURE";
                                    label4.ForeColor = Color.Red;
                                    label5.Text = "Total Pages: " + totalPages;
                                    label6.Text = "";

                                    //Target specific UI error.
                                    if (errorType == "EMPTY_ERROR")
                                    {
                                        string msg = "Part Number cannot be empty.\nPlease enter a part number.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "WHITESPACE_ERROR")
                                    {
                                        string msg = "Part Number cannot contain spaces.";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                    else if (errorType == "SYMBOL_ERROR" || errorType == "INVALID_CHAR_ERROR")
                                    {
                                        string msg = "Part Number can contain only letters,\nnumbers, and hyphens (-).";
                                        errorProvider1.SetError(textBox1, msg);
                                        label3.Text = msg;
                                    }
                                }
                            }
                            catch (DatabaseQueryException dbEx)
                            {
                                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label4.Text = "SYSTEM ERROR";
                                label4.ForeColor = Color.Red;
                            }
                        }
                    break;
                }
            }
        }

        //Event handler for right click event on dataGridView1.
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                //Select the row that was right-clicked, and set as CurrentCell.
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];

                //Show right click menu and right click position on dataGridView1.
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        //Event handler for click event for Update Record on right click menu.
        private void updateRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Retrieve the selected row.
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //Initialize variable to hold part history id as a string.
            string retrievedPartHistoryID;

            //Initialize variable to hold party history id converted from string to int.
            int convertedPartHistoryID;

            //Initialize variable to hold customer name as a string.
            string retrievedCustomerName;

            //Initialize variable to holde operator name as a string.
            string retrievedOperatorName;

            //Initialize variable to hold part number as a string.
            string retrievedPartNumber;

            //Initialize variable to hold quantity as a string.
            string retrievedQuantity;

            //Initialize variable to hold date received as a string.
            string retrievedDateReceived;

            //Initialize variable to hold date due as a string.
            string retrievedDateDue;

            //Initialize variable to hold operations as a string.
            string retrievedOperations;

            //Initialize variable to hold purchase order number as a string.
            string retrievedPurchaseOrderNumber;

            //Initialize variable to hold row version as byte[]
            byte[] retrievedRowVersion = null;

            //Retrieve the value of the first column of the selected row (PART_HISTORY_ID).
            object cellValueByIndexZero = selectedRow.Cells[0].Value;

            //Retrieve the value of the second column of the selected row (CUSTOMER_NAME).
            object cellValueByIndexOne = selectedRow.Cells[1].Value;

            //Retrieve the value of the second column of the selected row (OPERATOR_NAME).
            object cellValueByIndexTwo = selectedRow.Cells[2].Value;

            //Retrieve the value of the third column of the selected row (PART_NUMBER).
            object cellValueByIndexThree = selectedRow.Cells[3].Value;

            //Retrieve the value of the fourth column of the selected row (QTY).
            object cellValueByIndexFour = selectedRow.Cells[4].Value;

            //Retrieve the value of the fifth column of the selected row (DATE_RECEIVED).
            object cellValueByIndexFive = selectedRow.Cells[5].Value;

            //Retrieve the value of the sixth column of the selected row (DATE_DUE).
            object cellValueByIndexSix = selectedRow.Cells[6].Value;

            //Retrieve the value of the seventh column of the selected row (OPERATIONS).
            object cellValueByIndexSeven = selectedRow.Cells[7].Value;

            //Retrieve the value of the eighth column of the selected row (PURCHASE_ORDER_NUMBER).
            object cellValueByIndexEight = selectedRow.Cells[8].Value;

            //Retrieve the value of the ninth (hidden) column of the selected row (ROW_VERSION).
            object cellValueByIndexNine = selectedRow.Cells[9].Value;

            //Validate, convert PART_HISTORY_ID to string if not null.
            if (cellValueByIndexZero != null)
            {
                retrievedPartHistoryID = cellValueByIndexZero.ToString();
            }
            else
            {
                retrievedPartHistoryID = string.Empty;
            }

            //Validate, convert CUSTOMER_NAME to string if not null.
            if (cellValueByIndexOne != null)
            {
                retrievedCustomerName = cellValueByIndexOne.ToString();
            }
            else
            {
                retrievedCustomerName = string.Empty;
            }

            //Validate, convert OPERATOR_NAME to string if not null.
            if (cellValueByIndexTwo != null)
            {
                retrievedOperatorName = cellValueByIndexTwo.ToString();
            }
            else
            {
                retrievedOperatorName = string.Empty;
            }

            //Validate, convert PART_NUMBER to string if not null.
            if (cellValueByIndexThree != null)
            {
                retrievedPartNumber = cellValueByIndexThree.ToString();
            }
            else
            {
                retrievedPartNumber = string.Empty;
            }

            //Validate, convert QTY to string if not null.
            if (cellValueByIndexFour != null)
            {
                retrievedQuantity = cellValueByIndexFour.ToString();
            }
            else
            {
                retrievedQuantity = string.Empty;
            }

            //Validate, convert DATE_RECEIVED to string if not null.
            if (cellValueByIndexFive != null)
            {
                retrievedDateReceived = Convert.ToDateTime(cellValueByIndexFive).ToString("MM/dd/yyyy");
            }
            else
            {
                retrievedDateReceived = string.Empty;
            }

            //Validate, convert DATE_DUE to string if not null.
            if (cellValueByIndexSix != null)
            {
                retrievedDateDue = Convert.ToDateTime(cellValueByIndexSix).ToString("MM/dd/yyyy");
            }
            else
            {
                retrievedDateDue = string.Empty;
            }

            //Validate, convert OPERATIONS to string if not null.
            if (cellValueByIndexSeven != null)
            {
                retrievedOperations = cellValueByIndexSeven.ToString();
            }
            else
            {
                retrievedOperations = string.Empty;
            }

            //Validate, convert PURCHASE_ORDER_NUMBER to string if not null.
            if (cellValueByIndexEight != null)
            {
                retrievedPurchaseOrderNumber = cellValueByIndexEight.ToString();
            }
            else
            {
                retrievedPurchaseOrderNumber = string.Empty;
            }

            //Validate and cast the ROW_VERSION directly to a byte array.
            if (cellValueByIndexNine != null && cellValueByIndexNine != DBNull.Value)
            {
                retrievedRowVersion = (byte[])cellValueByIndexNine;
            }

            //Convert the string value to an int for processing.
            convertedPartHistoryID = int.Parse(retrievedPartHistoryID);

            //Create new UpdateRecord form object, pass retrieved values to UpdateRecord form.
            UpdateRecord updateRecord = new UpdateRecord(convertedPartHistoryID, retrievedCustomerName, retrievedOperatorName, retrievedPartNumber, retrievedPurchaseOrderNumber, retrievedQuantity, retrievedDateReceived, retrievedDateDue, retrievedOperations, retrievedRowVersion);

            //Show the Update Record UI.
            updateRecord.Show();
        }

        //Event handler for the click event for the Create Record button.
        private void button2_Click(object sender, EventArgs e)
        {
            CreateRecord createRecord = new CreateRecord();

            createRecord.Show();
        }

        //Event handler for the click event for the Dashboard button.
        private void button6_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        //Event handler for the click event for the Export button.
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataBaseSource.DataSource != null)
            {
                //Call the copyRows() method.
                copyRows();

                //Instantiate the Microsoft Excel objects to use for application start and the creation of a new workbook and worksheet.
                Microsoft.Office.Interop.Excel.Application excel = null;
                Microsoft.Office.Interop.Excel.Workbook workbook = null;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                Microsoft.Office.Interop.Excel.Range cellRange = null;

                try
                {
                    object misValue = System.Reflection.Missing.Value;

                    //Start excel.
                    excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.Visible = true;

                    //Add the new workbook.
                    workbook = excel.Workbooks.Add(misValue);

                    //Add a new worksheet.
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

                    //Select the starting cell to copy clipboard contents to.
                    cellRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1];
                    cellRange.Select();

                    //Set the column alignment for the worksheet.
                    worksheet.Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    //Set the column width for the worksheet.
                    worksheet.Columns.ColumnWidth = 15;

                    //Paste the clipboard contents into the worksheet.
                    worksheet.PasteSpecial(cellRange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                }
                //Release all objects and end threads associated with Excel
                finally
                {
                    if (cellRange != null)
                    {
                        Marshal.ReleaseComObject(cellRange);
                    }
                    if (worksheet != null)
                    {
                        Marshal.ReleaseComObject(worksheet);
                    }
                    if (workbook != null)
                    {
                        Marshal.ReleaseComObject(workbook);
                    }
                    if (excel != null)
                    {
                        Marshal.ReleaseComObject(excel);
                    }
                    cellRange = null;
                    worksheet = null;
                    workbook = null;
                    excel = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        //Function to copy all rows from the current page of the dataGridView.
        private void copyRows()
        {
            //Get the column headers for dataGridView1.
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            //Turn off row indicators for grid copy.
            dataGridView1.RowHeadersVisible = false;

            //Enable MultiSelect for all visible cells
            dataGridView1.MultiSelect = true;

            //Select all the visible cells on the current page.
            dataGridView1.SelectAll();

            //Copy the selected cells into dataObject.
            DataObject dataObject = dataGridView1.GetClipboardContent();
            if (dataObject != null)
            {
                Clipboard.SetDataObject(dataObject);
            }

            //Turn row indicators back on after copy.
            dataGridView1.RowHeadersVisible = true;
        }

        //Event handler for click event for Delete Record on right click menu.
        private async void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if a row is actually selected.
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            try
            {
                //Ask for confirmation before deleting.
                DialogResult confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.No)
                {
                    return;
                }

                dataGridView1.Enabled = false;

                //Retrieve the selected row.
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                //Initialize variable to hold part history id as a string.
                string retreivedPartHistoryID;

                //Initialize variable to hold party history id converted from string to int.
                int convertedPartHistoryID;

                //Retrieve the value of the first column of the selected row (PART_HISTORY_ID).
                object cellValueByIndex = selectedRow.Cells[0].Value;

                //Validate, convert PART_HISTORY_ID to string if not null.
                if (cellValueByIndex != null)
                {
                    retreivedPartHistoryID = cellValueByIndex.ToString();
                }
                else
                {
                    retreivedPartHistoryID = string.Empty;
                }

                //Convert the string value to an int for processing.
                convertedPartHistoryID = int.Parse(retreivedPartHistoryID);

                //Pass the PART_HISTORY_ID value to the delete query for record update to mark for delete.
                await newDAO.softDeleteQuery(convertedPartHistoryID);

                //Refresh the grid.
                button1_Click(sender, e);
            }
            catch (DatabaseQueryException dbEx)
            {
                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label4.Text = "SYSTEM ERROR";
                label4.ForeColor = Color.Red;
            }
            finally
            {
                //Re-enable the grid.
                dataGridView1.Enabled = true;
            }
        }

        //Event handler for click event for Restore Record on right click menu.
        private async void restoreRecordMainTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            try
            {
                //Ask for confirmation before restoring.
                DialogResult confirmation = MessageBox.Show("Are you sure you want to restore this record?", "Confirm Restoration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.No)
                {
                    return;
                }

                dataGridView1.Enabled = false;

                //Retrieve the selected row.
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                //Initialize variable to hold part history id as a string.
                string retreivedPartHistoryID;

                //Initialize variable to hold party history id converted from string to int.
                int convertedPartHistoryID;

                //Retrieve the value of the first column of the selected row (PART_HISTORY_ID).
                object cellValueByIndex = selectedRow.Cells[0].Value;

                //Validate, convert PART_HISTORY_ID to string if not null.
                if (cellValueByIndex != null)
                {
                    retreivedPartHistoryID = cellValueByIndex.ToString();
                }
                else
                {
                    retreivedPartHistoryID = string.Empty;
                }

                //Convert the string value to an int for processing.
                convertedPartHistoryID = int.Parse(retreivedPartHistoryID);

                //Pass the PART_HISTORY_ID value to the restore query for record update to restore record.
                await newDAO.restoreRecordQuery(convertedPartHistoryID);

                //Refresh the grid.
                button1_Click(sender, e);
            }
            catch (DatabaseQueryException dbEx)
            {
                MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label4.Text = "SYSTEM ERROR";
                label4.ForeColor = Color.Red;
            }
            finally
            {
                //Re-enable the grid.
                dataGridView1.Enabled = true;
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "MM/DD/YYYY-MM/DD/YYYY")
            {
                textBox1.Text = "";
            }
        }
    }
}
