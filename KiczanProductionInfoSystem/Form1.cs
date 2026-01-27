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

        //Set initial value for partNumber to comparison.
        string partNumber = "";

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
                case 1:
                    //Store user input from textBox1 into partNumber variable.
                    partNumber = textBox1.Text.ToUpper();

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
    }
}
