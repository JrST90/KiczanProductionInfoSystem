using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KiczanProductionInfoSystem
{
    internal class DAO
    {
        //Build connection string to connect to Microsoft SQL Server.
        private readonly string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KICZAN_PRODUCTION_SYSTEM;Trusted_Connection=True;TrustServerCertificate=True;";

        //Reads data from DB source, returns dataTable from DATE_DUE_RANGE_QUERY stored procedure. 
        //Reads beginning date and end date from user input from text box on UI.
        internal DataTable dateDueRangeQuery(string dateRange, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Create array to store date values from string input.
            //Split date range at occurence of '-' character. EX: 01/01/2024-02/01/2024
            string[] dateArray = dateRange.Split('-');

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("DATE_DUE_RANGE_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Create new DateTime objects and use the Parse() function on dates
                        //from user input date range.
                        //stored in dateArray[0], and dateArray[1].
                        DateTime bd = DateTime.Parse(dateArray[0]);
                        DateTime ed = DateTime.Parse(dateArray[1]);

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("dateB", bd);
                        command.Parameters.AddWithValue("dateE", ed);
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        //Use adapter object to fill dataTable with query results.
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query due date range record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Count all records for DATE_DUE_RANGE_QUERY.
        internal int dateDueRangeQueryCount(string dateRange)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Create array to store date values from string input.
            //Split date range at occurence of '-' character. EX: 01/01/2024-02/01/2024
            string[] dateArray = dateRange.Split('-');

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("DATE_DUE_RANGE_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Create new DateTime objects and use the Parse() function on dates
                        //from user input date range.
                        DateTime bd = DateTime.Parse(dateArray[0]);
                        DateTime ed = DateTime.Parse(dateArray[1]);

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("dateB", bd);
                        command.Parameters.AddWithValue("dateE", ed);

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve due date range record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        //Reads data from DB source, returns dataTable from PART_NUMBER_QUERY stored procedure.
        //Reads partNumber from user input in text box.
        internal DataTable partNumberQuery(string partNumber, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("partNo", searchWildTerm);
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query part number record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Count all records for PART_NUMBER_QUERY.
        internal int partNumberQueryCount(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("partNo", searchWildTerm);

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve part number record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        //Reads data from DB source, returns dataTable from PART_NUMBER_QUERY_ARCHIVE stored procedure.
        //Reads partNumber from user input in text box.
        internal DataTable partNumberQueryArchive(string partNumber, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_ARCHIVE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("partNo", searchWildTerm);
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query part number archive record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Count all records for PART_NUMBER_QUERY_ARCHIVE.
        internal int partNumberQueryCountArchive(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_ARCHIVE_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("partNo", searchWildTerm);

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve part number archive record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        // Reads data from DB source, returns dataTable from OPERATOR_NAME_QUERY stored procedure.
        // Reads operatorName from user input in text box.
        internal DataTable operatorNameQuery(string operatorName, int pageSize, int currentPageIndex)

        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("OPERATOR_NAME_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + operatorName + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("opName", searchWildTerm);
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query operator record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        // Count all records for OPERATOR_NAME_QUERY.
        internal int operatorNameQueryCount(string operatorName)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("OPERATOR_NAME_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + operatorName + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("opName", searchWildTerm);

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve operator record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        //Execute query to mark recorded as deleted.
        internal void softDeleteQuery(int part_history_id)
        {
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("MARK_PART_DELETED", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection.
                        command.Parameters.AddWithValue("p_part_history_id", part_history_id);

                        //Execute the stored procedure to mark record as deleted.
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Execute query to restore previously deleted record.
        internal void restoreRecordQuery(int part_history_id)
        {
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("RESTORE_PART", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection.
                        command.Parameters.AddWithValue("p_part_history_id", part_history_id);

                        //Execute the stored procedure to mark record as deleted.
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restore record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Reads data from DB source, returns dataTable from FABRICATION_DEPARTMENT_QUERY stored procedure.
        internal DataTable fabricationDepartmentQuery(int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("FABRICATION_DEPARTMENT_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query fabrication department record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Count all records for FABRICATION_DEPARTMENT_QUERY.
        internal int fabricationDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("FABRICATION_DEPARTMENT_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve fabrication department record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        //Reads data from DB source, returns dataTable from NC_MACHINE_WORK_QUERY stored procedure.
        internal DataTable machiningDepartmentQuery(int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("NC_MACHINE_WORK_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Set offset to be bound using currentPageIndex and pageSize arguments.
                        int offsetNum = ((currentPageIndex - 1) * pageSize);

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("pageSize", pageSize);
                        command.Parameters.AddWithValue("offsetNum", offsetNum);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to query machine shop record data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Count all records for NC_MACHINE_WORK_QUERY.
        internal int machiningDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("NC_MACHINE_WORK_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        totalRows = Convert.ToInt32(returnedCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve machine shop record count: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalRows;
        }

        //Get operator names for drop down menu comboBox2 and operatorComboBox
        internal List<Operators> GetOperators()
        {
            //Create new List object.
            List<Operators> returnList = new List<Operators>();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_OPERATORS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Operators op = new Operators
                                {
                                    OPERATOR_ID = reader.GetInt32(0),
                                    OPERATOR_NAME = reader.GetString(1),
                                };
                                returnList.Add(op);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load operator list: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnList;
        }

        //Get customer names for customerComboBox
        internal List<Customers> GetCustomers()
        {
            //Create new List object.
            List<Customers> returnList = new List<Customers>();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_CUSTOMERS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customers cust = new Customers
                                {
                                    CUSTOMER_ID = reader.GetInt32(0),
                                    CUSTOMER_NAME = reader.GetString(1),
                                };
                                returnList.Add(cust);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customer list: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnList;
        }

        //Method to create new record.
        internal bool CreateRecord(int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            int rowsAffected = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Build array of type SqlParameter for storage.
                    SqlParameter[] pms = new SqlParameter[9];

                    //Set array index values to inputs from CreateRecord form.
                    pms[0] = new SqlParameter("CUSTOMER_ID", SqlDbType.Int);
                    pms[0].Value = custID;

                    pms[1] = new SqlParameter("OPERATOR_ID", SqlDbType.Int);
                    pms[1].Value = opID;

                    pms[2] = new SqlParameter("PART_NUMBER", SqlDbType.VarChar);
                    pms[2].Value = partNumber;

                    pms[3] = new SqlParameter("DATE_DUE", SqlDbType.DateTime);
                    pms[3].Value = dateDue;

                    pms[4] = new SqlParameter("PURCHASE_ORDER_NUMBER", SqlDbType.VarChar);
                    pms[4].Value = poNumber;

                    pms[5] = new SqlParameter("QTY", SqlDbType.Int);
                    pms[5].Value = quantity;

                    pms[6] = new SqlParameter("OPERATIONS", SqlDbType.VarChar);
                    pms[6].Value = checkedOperations;

                    pms[7] = new SqlParameter("DATE_RECEIVED", SqlDbType.DateTime);
                    pms[7].Value = dateReceived;

                    pms[8] = new SqlParameter("TO_DELETE", SqlDbType.Bit);
                    pms[8].Value = toDelete;

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("CREATE_RECORD", connection))
                    {
                        //Build SQL command retreived from stored procedure "CREATE_RECORD".
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(pms);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create new record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected > 0;
        }

        //Method to update selected record.
        internal bool UpdateRecord(int partID, int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            int rowsAffected = 0;
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Build array of type SqlParameter for storage.
                    SqlParameter[] pms = new SqlParameter[10];

                    //Set array index values to inputs from UpdateRecord form.
                    pms[0] = new SqlParameter("p_PART_HISTORY_ID", SqlDbType.Int);
                    pms[0].Value = partID;

                    pms[1] = new SqlParameter("p_CUSTOMER_ID", SqlDbType.Int);
                    pms[1].Value = custID;

                    pms[2] = new SqlParameter("p_OPERATOR_ID", SqlDbType.Int);
                    pms[2].Value = opID;

                    pms[3] = new SqlParameter("p_PART_NUMBER", SqlDbType.VarChar);
                    pms[3].Value = partNumber;

                    pms[4] = new SqlParameter("p_DATE_DUE", SqlDbType.DateTime);
                    pms[4].Value = dateDue;

                    pms[5] = new SqlParameter("p_PURCHASE_ORDER_NUMBER", SqlDbType.VarChar);
                    pms[5].Value = poNumber;

                    pms[6] = new SqlParameter("p_QTY", SqlDbType.Int);
                    pms[6].Value = quantity;

                    pms[7] = new SqlParameter("p_OPERATIONS", SqlDbType.VarChar);
                    pms[7].Value = checkedOperations;

                    pms[8] = new SqlParameter("p_DATE_RECEIVED", SqlDbType.DateTime);
                    pms[8].Value = dateReceived;

                    pms[9] = new SqlParameter("p_TO_DELETE", SqlDbType.Bit);
                    pms[9].Value = toDelete;

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("UPDATE_RECORD", connection))
                    {
                        //Build SQL command retreived from stored procedure "UPDATE_RECORD".
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(pms);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update selected record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected > 0;
        }
        //Function to check if a userName exists within the DB.
        internal bool userNameCheck(string userName)
        {
            //int variable to store number of records with matching userName.
            int userCount = 0;

            //bool variable to serve as flag for existent of userName in DB.
            bool flag = false;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("USER_NAME_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("userName", userName);

                        //Execute query, save result in result object.
                        object returnedCount = command.ExecuteScalar();

                        //Convert to count.
                        userCount = Convert.ToInt32(returnedCount);

                        //Conditional to set flag value based on userCount.
                        if (userCount != 1)
                        {
                            flag = false;
                        }
                        else if (userCount == 1)
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to find username for authentication: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flag;
        }
        //Function to get user information for a given userName.
        internal Users getUserInfo(string userName)
        {
            //Create new Users object to store returned user information.
            Users user = new Users();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("USER_NAME_QUERY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.AddWithValue("userName", userName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user.USER_ID = reader.GetInt32(0);
                                user.USER_NAME = reader.GetString(1);
                                user.ROLES_ID = reader.GetInt32(2);
                                user.ROLE_NAME = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load user data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return user;
        }

        //Method to run GET_CUSTOMER_QTY_LAST_6_MONTHS query from SQL server to populate chart with queried data.
        internal DataTable LoadCustomerChartData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_CUSTOMER_QTY_LAST_6_MONTHS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customer dashboard chart data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
        internal DataTable LoadOperatorChartData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_OPERATOR_QTY_LAST_6_MONTHS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load operator dashboard chart data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
    }
}




