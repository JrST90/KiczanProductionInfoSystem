using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    public partial class Form1 : Form
    {
        //Set pageSize and currentPageIndex.
        private const int pageSize = 20;
        private int currentPageIndex = 1;

        //Set initial values for totalpages and totalRows.
        private int totalPages = 0;
        private int totalRows = 0;

        //Create and initialize a new BindingSource object.
        private BindingSource dataBaseSource = new BindingSource();

        //set initial values for dateRange and partNumber to store user input for comparison.
        string partNumber = "";
        string dateRange = "";

        public Form1()
        {
            InitializeComponent();
            this.Text = "Kiczan Production Information System";
        }

        //Events to occur on button click for selection from comboBox1.
        private void button1_Click(object sender, EventArgs e)
        {
            //Get index value from comboBox1 after user selection.
            int searchTypeIndex = comboBox1.SelectedIndex;

            //Create new DAO Object for query
            DAO newDAO = new DAO();

            //Create new DataValidation Object for input validation.
            DataValidation newDV = new DataValidation();

            //Button click event is dependent on selected index from comboBox1.
            switch (searchTypeIndex)
            {
                //Search By Due Date Range, gets value from textBox1.
                case 0:
                    //Store user input from textBox1 into dateRange variable.
                    dateRange = textBox1.Text;

                    //Enable update and delete record options in menu strip.
                    updateRecordToolStripMenuItem.Enabled = true;
                    updateRecordToolStripMenuItem.Visible = true;

                    //Enable delete record option in menu strip.
                    deleteRecordArchiveToolStripMenuItem.Enabled = true;
                    deleteRecordArchiveToolStripMenuItem.Visible = true;

                    //Disable restore record option in menu strip.
                    restoreRecordMainTableToolStripMenuItem.Enabled = false;
                    restoreRecordMainTableToolStripMenuItem.Visible = false;

                    //if dateRange is of the right format and the beginning date is before the end date, execute the search query.
                    if (newDV.validateDateRange(dateRange) == true && newDV.validateDateRangeBegBeforeEnd(dateRange) == true)
                    {
                        //Set currentPageIndex for query.
                        currentPageIndex = 1;

                        //Bind dataBaseSource to dateDueRangeQuery results, passing the arguments dateRange, pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = newDAO.dateDueRangeQuery(dateRange, pageSize, currentPageIndex);

                        //Bind dataGridView1 to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Get the total rows returned by dateDueRangeQueryCount with passed argument dateRange and set to variable totalRows.
                        totalRows = newDAO.dateDueRangeQueryCount(dateRange);

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
                    //If the dateRange is not of the right format, do not execute the query.
                    else if (newDV.validateDateRange(dateRange) == false)
                    {
                        //Reset dateRange variable value.
                        dateRange = "";

                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Clear dataBaseSource.
                        dataBaseSource.DataSource = null;

                        //Clear dataGridView1.
                        dataGridView1.DataSource = null;

                        //Clear the rows in dataGridView1.
                        dataGridView1.Rows.Clear();

                        //Save the number of rows to ensure an output of 0.
                        totalRows = dataGridView1.Rows.Count;

                        //Save the number of pages to ensure an output of 0.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1.
                        errorProvider1.SetError(textBox1, "Due Date Range must be of the format: mm/dd/yyyy-mm/dd/yyyy");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's text value to alert the user of an unsuccsessful query.
                        label4.Text = "QUERY FAILURE";

                        //Set label4's color to red.
                        label4.ForeColor = Color.Red;
                    }
                    else if (newDV.validateDateRangeBegBeforeEnd(dateRange) == false)
                    {
                        //Reset dateRange variable value.
                        dateRange = "";

                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Clear dataBaseSource.
                        dataBaseSource.DataSource = null;

                        //Clear dataGridView1.
                        dataGridView1.DataSource = null;

                        //Clear the rows in dataGridView1.
                        dataGridView1.Rows.Clear();

                        //Save the number of rows to ensure an output of 0.
                        totalRows = dataGridView1.Rows.Count;

                        //Save the number of pages to ensure an output of 0.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1.
                        errorProvider1.SetError(textBox1, "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's text value to alert the user of an unsuccsessful query.
                        label4.Text = "QUERY FAILURE";

                        //Set label4's color to red.
                        label4.ForeColor = Color.Red;
                    }
                break;

                //Search By Part Number, gets value from textBox1.
                case 1:
                    //Store user input from textBox1 into partNumber variable.
                    partNumber = textBox1.Text;

                    //Enable update and delete record options in menu strip.
                    updateRecordToolStripMenuItem.Enabled = true;
                    updateRecordToolStripMenuItem.Visible = true;

                    //Enable delete record option in menu strip.
                    deleteRecordArchiveToolStripMenuItem.Enabled = true;
                    deleteRecordArchiveToolStripMenuItem.Visible = true;

                    //Disable restore record option in menu strip.
                    restoreRecordMainTableToolStripMenuItem.Enabled = false;
                    restoreRecordMainTableToolStripMenuItem.Visible = false;

                    //If the partNumber is of the right format and the number of matching records is greater than 0, execute the search query.
                    if (newDV.validatePartNumber(partNumber) == true && newDAO.partNumberQueryCount(partNumber) > 0)
                    {
                        //Set currentPageIndex for query.
                        currentPageIndex = 1;

                        //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);

                        //Bind dateGridView1 to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Get the total rows returned by partNumberQueryCount with passed argument partNumber and set to variable totalRows.
                        totalRows = newDAO.partNumberQueryCount(partNumber);

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
                    //If the partNumber is of the right format and the number of matching records is 0, execute the search query.
                    else if (newDV.validatePartNumber(partNumber) == true && newDAO.partNumberQueryCount(partNumber) == 0)
                    {
                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);

                        //Bind dateGridView1 to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Get the total rows returned by partNumberQueryCount with passed argument partNumber and set to variable totalRows.
                        totalRows = newDAO.partNumberQueryCount(partNumber);

                        //Save the number of pages to ensure an output of 0.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1.
                        errorProvider1.SetError(textBox1, "No Records Found.");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "No Records Found.";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's value to alert the user of a succsessful query.
                        label4.Text = "QUERY SUCCESS";

                        //Set label4's color to green.
                        label4.ForeColor = Color.Green;
                    }
                    //Alert the user that the input partNumber is not of the right format.
                    else
                    {
                        //Reset partNumber variable value.
                        partNumber = "";

                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Clear dataBaseSource.
                        dataBaseSource.DataSource = null;

                        //Clear dataGridView1.
                        dataGridView1.DataSource = null;

                        //Clear the rows in dataGridView1.
                        dataGridView1.Rows.Clear();

                        //Save the number of rows to ensure an output of 0.
                        totalRows = dataGridView1.Rows.Count;

                        //Save the number of pages to ensure an output of 0.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1.
                        errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's text value to alert the user of an unsuccsessful query.
                        label4.Text = "QUERY FAILURE";

                        //Set label4's color to red.
                        label4.ForeColor = Color.Red;
                    }
                break;
                //Search by Fabrication Department.
                case 3:
                    //Enable update and delete record options in menu strip.
                    updateRecordToolStripMenuItem.Enabled = true;
                    updateRecordToolStripMenuItem.Visible = true;

                    //Enable delete record option in menu strip.
                    deleteRecordArchiveToolStripMenuItem.Enabled = true;
                    deleteRecordArchiveToolStripMenuItem.Visible = true;

                    //Disable restore record option in menu strip.
                    restoreRecordMainTableToolStripMenuItem.Enabled = false;
                    restoreRecordMainTableToolStripMenuItem.Visible = false;
                    //Set currentPageIndex for query.
                    currentPageIndex = 1;

                    //Bind dataBaseSource to fabricationDepartmentQuery results passing the arguments of pageSize, and currentPageIndex.
                    dataBaseSource.DataSource = newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                    //Bind dateGridView to dataBaseSource.
                    dataGridView1.DataSource = dataBaseSource;

                    //Get the total rows returned by fabricationDepartmentQueryCount.
                    totalRows = newDAO.fabricationDepartmentQueryCount();

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
                    break;

                //Search By Part Number in Archive, gets value from textBox1.
                case 8:
                    //Store user input from textBox1 into partNumber variable.
                    partNumber = textBox1.Text;

                    //Enable restore record option in menu strip.
                    restoreRecordMainTableToolStripMenuItem.Enabled = true;
                    restoreRecordMainTableToolStripMenuItem.Visible = true;

                    //Disable update and delete record options in menu strip.
                    updateRecordToolStripMenuItem.Enabled = false;
                    updateRecordToolStripMenuItem.Visible = false;

                    //Disable delete record option in menu strip.
                    deleteRecordArchiveToolStripMenuItem.Enabled = false;
                    deleteRecordArchiveToolStripMenuItem.Visible = false;

                    //If the partNumber is of the right format, execute the search query.
                    if (newDV.validatePartNumber(partNumber) == true && newDAO.partNumberQueryCountArchive(partNumber) > 0)
                    {
                        //Set currentPageIndex for query.
                        currentPageIndex = 1;

                        //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);

                        //Bind dateGridView1 to dataBaseSource.
                        dataGridView1.DataSource = dataBaseSource;

                        //Get the total rows returned by partNumberQueryCount with passed argument partNumber and set to variable totalRows.
                        totalRows = newDAO.partNumberQueryCountArchive(partNumber);

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
                    //If the partNumber is of the right format and the number of matching records is 0, execute the search query.
                    else if (newDV.validatePartNumber(partNumber) == true && newDAO.partNumberQueryCountArchive(partNumber) == 0)
                    {
                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Bind dataBaseSource to partNumberQuery results, passing the arguments partNumber, pageSize, and currentPageIndex.
                        dataBaseSource.DataSource = newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);

                        //Bind dataGridView1 to dataBaseSource
                        dataGridView1.DataSource = dataBaseSource;

                        //Get the total rows returned by partNumberQueryCount with passed argument partNumber and set to variable totalRows.
                        totalRows = newDAO.partNumberQueryCountArchive(partNumber);

                        //Save the number of pages to ensure an output of 0
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1
                        errorProvider1.SetError(textBox1, "No Records Found.");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "No Records Found.";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's value to alert the user of a succsessful query.
                        label4.Text = "Query Success";

                        //Set label4's color to green.
                        label4.ForeColor = Color.Green;
                    }
                    else
                    {
                        //Reset partNumber variable value.
                        partNumber = "";

                        //Reset the currentPageIndex variable value.
                        currentPageIndex = 1;

                        //Clear dataBaseSource.
                        dataBaseSource.DataSource = null;

                        //Clear dataGridView1.
                        dataGridView1.DataSource = null;

                        //Clear the rows in dataGridView1.
                        dataGridView1.Rows.Clear();

                        //Save the number of rows to ensure an output of 0.
                        totalRows = dataGridView1.Rows.Count;

                        //Save the number of pages to ensure an output of 0.
                        totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                        //Set the error message for errorProvider1.
                        errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                        //Clear label3's text value.
                        label3.Text = "";

                        //Set label3's text value to the error message.
                        label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                        //Set label2's text value to the value of totalRows.
                        label2.Text = "Number of Records: " + totalRows;

                        //Set label5's text value to the value of totalPages.
                        label5.Text = "Total Pages: " + totalPages;

                        //Clear label6's value.
                        label6.Text = "";

                        //Set label4's text value to alert the user of an unsuccsessful query.
                        label4.Text = "QUERY FAILURE";

                        //Set label4's color to red.
                        label4.ForeColor = Color.Red;
                    }
                break;
            }
        }

        //Events to occur on drop down selector change for comboBox1.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get index value from comboBox1 after user selection.
            int searchTypeIndex = comboBox1.SelectedIndex;

            //Create new DAO Object for query.
            DAO newDAO = new DAO();

            //Create new DataValidation Object for input validation.
            DataValidation newDV = new DataValidation();

            switch (searchTypeIndex)
            {
                //Search By Due Date Range.
                case 0:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Input example for date range search.
                    textBox1.Text = "XX/XX/XXXX-XX/XX/XXXX";

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
                    //Search by Fabrication Department.
                case 3:
                    //Clear textbox1.
                    textBox1.Clear();

                    //Clear the list storing operators.
                    //operators.Clear();

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
                case 8:
                    //Clear textbox1.
                    textBox1.Clear();

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

        //Event handler for Next Button.
        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages && dataBaseSource.DataSource != null)
            {
                //Get index value from comboBox1 after user selection.
                int searchType = comboBox1.SelectedIndex;

                //Create new DAO Object for query.
                DAO newDAO = new DAO();

                //Create new DataValidation object.
                DataValidation newDV = new DataValidation();

                switch (searchType)
                {
                    case 0:
                        //If the dateRange variable is still the same value as textBox1.Text and the next button is pressed.
                        if (dateRange == textBox1.Text)
                        {
                            //Incremement page counter index.
                            currentPageIndex++;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.dateDueRangeQuery(dateRange, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        //If the date range input into textBox1 has changed, does not meet format validation constraint, and the next button is pressed.
                        else if (newDV.validateDateRange(textBox1.Text) == false)
                        {
                            //Reset the value of the dateRange variable.
                            dateRange = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Due Date Range must be of the format: mm/dd/yyyy-mm/dd/yyyy");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                        else if (newDV.validateDateRangeBegBeforeEnd(textBox1.Text) == false)
                        {
                            //Reset the value of the dateRange variable.
                            dateRange = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 1:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            //Incremement page counter index.
                            currentPageIndex++;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        //If the part number input into textBox1.Text has changed, does not meet format validation constraints, and the next button is pressed.
                        else if (newDV.validatePartNumber(textBox1.Text) == false)
                        {
                            //Reset the value of the partNumber variable.
                            partNumber = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 3:
                        //Incremement page counter index.
                        currentPageIndex++;

                        //Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        //Query to update page.
                        dataBaseSource.DataSource = newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                        //Bind results to dataGridView1.
                        dataGridView1.DataSource = dataBaseSource;

                        break;

                    case 8:
                        //If the partNumber variable is still the same value as textBox1.Text and the next button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            //Incremement page counter index.
                            currentPageIndex++;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        //If the part number input into textBox1.Text has changed, does not meet format validation constraints, and the next button is pressed.
                        else if (newDV.validatePartNumber(textBox1.Text) == false)
                        {
                            //Reset the value of the partNumber variable.
                            partNumber = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                    break;
                }
            }
        }
        //Event handler for Previous Button.
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                //Get index value from comboBox1 after user selection.
                int searchType = comboBox1.SelectedIndex;

                //Create new DAO Object for query.
                DAO newDAO = new DAO();

                //Create new DataValidation object.
                DataValidation newDV = new DataValidation();

                switch (searchType)
                {
                    case 0:
                        if (dateRange == textBox1.Text)
                        {
                            //Decrement page counter index.
                            currentPageIndex--;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.dateDueRangeQuery(textBox1.Text, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        else if (newDV.validateDateRange(textBox1.Text) == false)
                        {
                            //Reset the value of the dateRange variable.
                            dateRange = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1 .
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Due Date Range must be of the format: mm/dd/yyyy-mm/dd/yyyy");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Due Date Range must be of the \nformat: \nmm/dd/yyyy-mm/dd/yyyy";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                        else if (newDV.validateDateRangeBegBeforeEnd(textBox1.Text) == false)
                        {
                            //Reset the value of the dateRange variable.
                            dateRange = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1. 
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Beginning Date occurs after ending date. \nPlease enter as \n(Beginning Date)mm/dd/yyyy-\n(Ending Date)mm/dd/yyyy";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 1:
                        //If the partNumber variable is still the same value as textBox1.Text and the previous button is pressed.
                        if (partNumber == textBox1.Text)
                        {
                            //Decrement page counter index.
                            currentPageIndex--;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.partNumberQuery(partNumber, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        //If the part number input into textBox1.Text has changed, does not meet format validation constraints, and the previous button is pressed
                        else if (newDV.validatePartNumber(textBox1.Text) == false)
                        {
                            //Reset the value of the partNumber variable.
                            partNumber = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
                        }
                    break;

                    case 3:
                        // Decrement page counter index.
                        currentPageIndex--;

                        // Update page.
                        label6.Text = "Current Page: " + currentPageIndex;

                        // Query to update page.
                        dataBaseSource.DataSource = newDAO.fabricationDepartmentQuery(pageSize, currentPageIndex);

                        // Bind results to dataGridView1.
                        dataGridView1.DataSource = dataBaseSource;

                    break;

                    case 8:
                        if (partNumber == textBox1.Text)
                        {
                            //Decrement page counter index.
                            currentPageIndex--;

                            //Update page.
                            label6.Text = "Current Page: " + currentPageIndex;

                            //Query to update page.
                            dataBaseSource.DataSource = newDAO.partNumberQueryArchive(partNumber, pageSize, currentPageIndex);

                            //Bind results to dataGridView1.
                            dataGridView1.DataSource = dataBaseSource;
                        }
                        else if (newDV.validatePartNumber(textBox1.Text) == false)
                        {
                            //Reset the value of the partNumber variable.
                            partNumber = "";

                            //Reset currentPageIndex.
                            currentPageIndex = 1;

                            //Clear dataBaseSource.
                            dataBaseSource.DataSource = null;

                            //Clear dataGridView1.
                            dataGridView1.DataSource = null;

                            //Clear the rows in dataGridView1.
                            dataGridView1.Rows.Clear();

                            //Update totalRows variable to 0.
                            totalRows = dataGridView1.Rows.Count;

                            //Update totalPages to 0.
                            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                            //Set errorProvider1 message.
                            errorProvider1.SetError(textBox1, "Enter Part Number containing letters, numbers, and hyphens only.");

                            //Clear label3's text value.
                            label3.Text = "";

                            //Set label3's text value to error state.
                            label3.Text = "Please enter Part Number containing letters, numbers, and hyphens only.";

                            //Update label2's text value.
                            label2.Text = "Number of Records: " + totalRows;

                            //Update label5's text value.
                            label5.Text = "Total Pages: " + totalPages;

                            //Clear label6's text value.
                            label6.Text = "";

                            //Alert user to query failure.
                            label4.Text = "QUERY FAILURE";

                            //Set label4's text color to red.
                            label4.ForeColor = Color.Red;
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
            //Create new UpdateRecord form object.
            UpdateRecord updateRecord = new UpdateRecord();

            //Retrieve the selected row.
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //Initialize variable to hold part history id as a string.
            string retreivedPartHistoryID;

            //Initialize variable to hold party history id converted from string to int.
            int convertedPartyHistoryID;

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
            convertedPartyHistoryID = int.Parse(retreivedPartHistoryID);

            //Show the Update Record UI.
            updateRecord.Show();
        }

        //Event handler for click event for Delete Record on right click menu.
        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new DAO Object for query.
            DAO newDAO = new DAO();

            //Retrieve the selected row.
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //Initialize variable to hold part history id as a string.
            string retreivedPartHistoryID;

            //Initialize variable to hold party history id converted from string to int.
            int convertedPartyHistoryID;

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
            convertedPartyHistoryID = int.Parse(retreivedPartHistoryID);

            //Pass the PART_HISTORY_ID value to the delete query for record update to mark for delete.
            newDAO.softDeleteQuery(convertedPartyHistoryID);
            button1_Click(sender, e);
          
        }

        //Event handler for click event for Restore Record on right click menu.
        private void restoreRecordMainTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new DAO Object for query.
            DAO newDAO = new DAO();

            //Retrieve the selected row.
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //Initialize variable to hold part history id as a string.
            string retreivedPartHistoryID;

            //Initialize variable to hold party history id converted from string to int.
            int convertedPartyHistoryID;

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
            convertedPartyHistoryID = int.Parse(retreivedPartHistoryID);

            //Pass the PART_HISTORY_ID value to the restore query for record update to restore record.
            newDAO.restoreRecordQuery(convertedPartyHistoryID);
            button1_Click(sender, e);
        }
    }
}
