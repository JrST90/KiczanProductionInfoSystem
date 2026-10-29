using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    internal partial class Form1 : Form
    {
        //Properties and Field for existing instances.
        private readonly Users _currentUser;
        private DataValidation newDV = new DataValidation();
        private DAO newDAO = new DAO();
        private BindingSource dataBaseSource = new BindingSource();
        private List<Operators> operators = new List<Operators>();

        //Pagination configuration.
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        
        //State tracking strings.
        string partNumber = "";
        string dateRange = "";
        string opName = "";

        //State tracking backups for Auto-Reset.
        private SearchType _activeSearchType;
        private string _activePartNumber = "";
        private string _activeDateRangeString = "";
        private string _activeOperatorName = "";
        private DateTime _activeStartDate;
        private DateTime _activeEndDate;

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
            await ExecuteSelectedSearchAsync();
        }

        //Helper function to determine which query to execute based on user selection.
        private async Task ExecuteSelectedSearchAsync()
        {
            SearchType selectedSearch = (SearchType)comboBox1.SelectedIndex;

            bool isArchiveCase = (selectedSearch == SearchType.PartNumberArchive);
            UpdateUserMenuPermissions(isArchiveCase);

            switch(selectedSearch)
            {
                case SearchType.DueDateRange:
                    await HandleDueDateRangeSearchAsync();
                break;

                case SearchType.PartNumber:
                    await HandlePartNumberSearchAsync();
                break;

                case SearchType.OperatorName:
                    await HandleOperatorNameSearchAsync();
                break;

                case SearchType.FabricationDepartment:
                    await HandleFabricationDepartmentSearchAsync();
                break;

                case SearchType.MachiningDepartment:
                    await HandleMachiningDepartmentSearchAsync();
                break;

                case SearchType.PartNumberArchive:
                    await HandlePartNumberArchiveSearchAsync();
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
            if(dataBaseSource.DataSource == null)
            {
                return;
            }

            if(HasSearchInputChanged())
            {
                await ExecuteSelectedSearchAsync();
            }
            else
            {
                if(currentPageIndex < totalPages)
                {
                    currentPageIndex++;
                    await FetchCurrentPageDataAsync();
                }
            }
        }
        //Event handler for Previous Button.
        private async void button5_Click(object sender, EventArgs e)
        {
            if (dataBaseSource.DataSource == null || currentPageIndex <= 1)
            {
                return;
            }
            currentPageIndex--;
            await FetchCurrentPageDataAsync();
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
        
        //Helper function to handle Due Date Range Search.
        private async Task HandleDueDateRangeSearchAsync()
        {
            dateRange = textBox1.Text;

            if(newDV.validateDateRange(dateRange, out string errorType, out DateTime cleanStartDate, out DateTime cleanEndDate))
            {
                _activeSearchType = SearchType.DueDateRange;
                _activeDateRangeString = dateRange;
                _activeStartDate = cleanStartDate;
                _activeEndDate = cleanEndDate;
                currentPageIndex = 1;

                try
                {
                    var data = await newDAO.dateDueRangeQuery(_activeStartDate, _activeEndDate, pageSize, currentPageIndex);
                    int count = await newDAO.dateDueRangeQueryCount(_activeStartDate, _activeEndDate);
                    ApplyQueryResultToUI(data, count);
                }
                catch(DatabaseQueryException dbEx)
                {
                    HandleDataBaseException(dbEx);
                }
            }
            else
            {
                ResetUIToFailureState();
                dateRange = "";

                string errorMsg = errorType == "FORMAT_ERROR"
                    ? "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy"
                    : "Beginning Date occurs after ending date. \nPlease enter as\n(Beginning Date)mm/dd/yyyy-(Ending Date)mm/dd/yyyy";
                SetInputError(errorMsg);
            }
        }

        //Helper function to handle Part Number Search.
        private async Task HandlePartNumberSearchAsync()
        {
            partNumber = textBox1.Text;

            if(newDV.validatePartNumber(partNumber, out string errorType))
            {
                _activeSearchType = SearchType.PartNumber;
                _activePartNumber = partNumber;
                currentPageIndex = 1;

                try
                {
                    var data = await newDAO.partNumberQuery(_activePartNumber, pageSize, currentPageIndex);
                    int count = await newDAO.partNumberQueryCount(_activePartNumber);
                    ApplyQueryResultToUI(data, count);
                }
                catch(DatabaseQueryException dbEx)
                {
                    HandleDataBaseException(dbEx);
                }
            }
            else
            {
                ResetUIToFailureState();
                partNumber = "";

                string errorMsg = errorType switch
                {
                    "EMPTY_ERROR" => "Part Number cannot be empty.\n Please enter a Part Number.",
                    "WHITE_SPACE_ERROR" => "Part Number cannot contain spaces.",
                    _ => "Part Number can contain only letters,\nnumbers, and hyphens (-)."
                };
                SetInputError(errorMsg);
            }
        }

        //Helper function to handle Operator Name Search.
        private async Task HandleOperatorNameSearchAsync()
        {
            opName = comboBox2.Text;

            if(!string.IsNullOrEmpty(opName))
            {
                _activeSearchType = SearchType.OperatorName;
                _activeOperatorName = opName;
                currentPageIndex = 1;

                try
                {
                    var data = await newDAO.operatorNameQuery(_activeOperatorName, pageSize, currentPageIndex);
                    int count = await newDAO.operatorNameQueryCount(_activeOperatorName);
                    ApplyQueryResultToUI(data, count);
                }
                catch(DatabaseQueryException dbEx)
                {
                    HandleDataBaseException(dbEx);
                }
            }
            else
            {
                ResetUIToFailureState();
                opName = "";
                SetInputError("Please select and Operator Name.");
            }
        }

        //Helper function to handle Fabrication Department Search.
        private async Task HandleFabricationDepartmentSearchAsync()
        {
            _activeSearchType = SearchType.FabricationDepartment;
            currentPageIndex = 1;

            try
            {
                var data = await newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);
                int count = await newDAO.fabricationDepartmentQueryCount();
                ApplyQueryResultToUI(data, count);
            }
            catch(DatabaseQueryException dbEx)
            {
                HandleDataBaseException(dbEx);
            }
        }

        //Helper function to handle Machining Department Search.
        private async Task HandleMachiningDepartmentSearchAsync()
        {
            _activeSearchType = SearchType.MachiningDepartment;
            currentPageIndex = 1;

            try
            {
                var data = await newDAO.machiningDepartmentQuery(pageSize, currentPageIndex);
                int count = await newDAO.machiningDepartmentQueryCount();
                ApplyQueryResultToUI(data, count);
            }
            catch(DatabaseQueryException dbEx)
            {
                HandleDataBaseException(dbEx);
            }
        }

        //Helper function to handle Part Number Archive Search.
        private async Task HandlePartNumberArchiveSearchAsync()
        {
            partNumber = textBox1.Text;

            if (newDV.validatePartNumber(partNumber, out string errorType))
            {
                _activeSearchType = SearchType.PartNumberArchive;
                _activePartNumber = partNumber;
                currentPageIndex = 1;

                try
                {
                    var data = await newDAO.partNumberQueryArchive(_activePartNumber, pageSize, currentPageIndex);
                    int count = await newDAO.partNumberQueryCountArchive(_activePartNumber);
                    ApplyQueryResultToUI(data, count);
                }
                catch (DatabaseQueryException dbEx)
                {
                    HandleDataBaseException(dbEx);
                }
            }
            else
            {
                ResetUIToFailureState();
                partNumber = "";

                string errorMsg = errorType switch
                {
                    "EMPTY_ERROR" => "Part Number cannot be empty.\n Please enter a Part Number.",
                    "WHITE_SPACE_ERROR" => "Part Number cannot contain spaces.",
                    _ => "Part Number can contain only letters,\nnumbers, and hyphens (-)."
                };
                SetInputError(errorMsg);
            }
        }

        //Helper function to fetch the current pages data.
        private async Task FetchCurrentPageDataAsync()
        {
            try
            {
                object data = null;

                switch(_activeSearchType)
                {
                    case SearchType.DueDateRange:
                        data = await newDAO.dateDueRangeQuery(_activeStartDate, _activeEndDate, pageSize, currentPageIndex);
                    break;

                    case SearchType.PartNumber:
                        data = await newDAO.partNumberQuery(_activePartNumber, pageSize, currentPageIndex);
                    break;

                    case SearchType.OperatorName:
                        data = await newDAO.operatorNameQuery(_activeOperatorName, pageSize, currentPageIndex);
                    break;

                    case SearchType.FabricationDepartment:
                        data = await newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);
                    break;

                    case SearchType.MachiningDepartment:
                        data = await newDAO.machiningDepartmentQuery(pageSize, currentPageIndex);
                    break;

                    case SearchType.PartNumberArchive:
                        data = await newDAO.partNumberQueryArchive(_activePartNumber, pageSize, currentPageIndex);
                    break;
                }

                dataBaseSource.DataSource = data;
                dataGridView1.DataSource = dataBaseSource;

                label6.Text = $"Current Page: {currentPageIndex}";
            }
            catch(DatabaseQueryException dbEx)
            {
                HandleDataBaseException(dbEx);
            }
        }

        //Helper function to set menu strip visibility.
        private void UpdateUserMenuPermissions(bool isArchive)
        {
            bool hasCrudAccess = _currentUser.ROLE_NAME == "Fabrication Manager" || _currentUser.ROLE_NAME == "Machining Manager";

            updateRecordToolStripMenuItem.Enabled = !isArchive && hasCrudAccess;
            updateRecordToolStripMenuItem.Visible = !isArchive && hasCrudAccess;

            deleteRecordArchiveToolStripMenuItem.Enabled = !isArchive && hasCrudAccess;
            deleteRecordArchiveToolStripMenuItem.Visible = !isArchive && hasCrudAccess;

            restoreRecordMainTableToolStripMenuItem.Enabled = isArchive && hasCrudAccess;
            restoreRecordMainTableToolStripMenuItem.Visible = isArchive && hasCrudAccess;
        }
        

        //Helper function to bind UI to successful query.
        private void ApplyQueryResultToUI(object dataSource, int rowCount)
        {
            totalRows = rowCount;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            dataBaseSource.DataSource = dataSource;
            dataGridView1.DataSource = dataBaseSource;

            if(dataGridView1.Columns.Contains("ROW_VERSION"))
            {
                dataGridView1.Columns["ROW_VERSION"].Visible = false;
            }

            label2.Text = $"Number of Records: {totalRows}";
            label5.Text = $"Total Pages: {totalPages}";

            if(totalRows > 0)
            {
                label6.Text = $"Current Page: {currentPageIndex}";
                errorProvider1.Clear();
                label3.Text = "";
            }
            else
            {
                label6.Text = "";
                SetInputError("No Records Found.");
            }
            SetStatusLabel("QUERY SUCCESS", Color.Green);
        }

        //Helper function to detect search input change.
        private bool HasSearchInputChanged()
        {
            SearchType currentSelection = (SearchType)comboBox1.SelectedIndex;
            if(currentSelection != _activeSearchType)
            {
                return true;
            }
            return currentSelection switch
            {
                SearchType.DueDateRange => textBox1.Text != _activeDateRangeString,
                SearchType.PartNumber => textBox1.Text != _activePartNumber,
                SearchType.PartNumberArchive => textBox1.Text != _activePartNumber,
                SearchType.OperatorName => comboBox2.Text != _activeOperatorName,
                _ => false
            };
        }

        //Helper function to Clean Failure/Clear UI state.
        private void ResetUIToFailureState()
        {
            totalRows = 0;
            totalPages = 0;
            currentPageIndex = 1;

            label2.Text = "Number of Records: 0";
            label5.Text = "Total Pages: 0";
            label6.Text = "";

            dataBaseSource.DataSource = null;
            dataGridView1.DataSource = null;

            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            SetStatusLabel("QUERY FAILURE", Color.Red);
        }


        //Helper function to set input error during failed validation.
        private void SetInputError(string  message)
        {
            errorProvider1.SetError(textBox1, message);
            label3.Text = message;
        }

        //Helper function to set status labels during system error.
        private void SetStatusLabel(string text, Color color)
        {
            label4.Text = text;
            label4.ForeColor = color;
        }

        //Type Safe Search Enums Matrix to elegantly handle user search selection.
        public enum SearchType
        {
            DueDateRange = 0,
            PartNumber = 1,
            OperatorName = 2,
            FabricationDepartment = 3,
            MachiningDepartment = 4,
            PartNumberArchive = 5

        }

        //Helper function to handle DB Exceptions.
        private void HandleDataBaseException(DatabaseQueryException dbEx)
        {
            MessageBox.Show($"{dbEx.Message}\nDetails: {dbEx.InnerException?.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SetStatusLabel("SYSTEM ERROR", Color.Red);
        }
    }
}
