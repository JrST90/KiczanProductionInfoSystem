using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KiczanProductionInfoSystem
{
    internal class DAO
    {
        public static string sqlConnectionString { get; private set; }

        internal static void InitializeConnection(string connectionString)
        {
            sqlConnectionString = connectionString;
        }

        //Reads data from DB source, returns dataTable from DATE_DUE_RANGE_QUERY stored procedure. 
        //Reads beginning date and end date from user input from text box on UI.
        internal async Task<DataTable> dateDueRangeQuery(string dateRange, int pageSize, int currentPageIndex)
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
                        command.Parameters.Add("@dateB", SqlDbType.DateTime).Value = bd;
                        command.Parameters.Add("@dateE", SqlDbType.DateTime).Value = ed;
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        //Use reader object to fill dataTable with query results.
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> dateDueRangeQueryCount(string dateRange)
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
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("DATE_DUE_RANGE_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Create new DateTime objects and use the Parse() function on dates
                        //from user input date range.
                        DateTime bd = DateTime.Parse(dateArray[0]);
                        DateTime ed = DateTime.Parse(dateArray[1]);

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.Add("@dateB", SqlDbType.DateTime).Value = bd;
                        command.Parameters.Add("@dateE", SqlDbType.DateTime).Value = ed;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task<DataTable> partNumberQuery(string partNumber, int pageSize, int currentPageIndex)
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
                        command.Parameters.Add("@partNo", SqlDbType.VarChar, 50).Value = searchWildTerm;
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> partNumberQueryCount(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.Add("@partNo", SqlDbType.VarChar, 50).Value = searchWildTerm;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task<DataTable> partNumberQueryArchive(string partNumber, int pageSize, int currentPageIndex)
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
                        command.Parameters.Add("@partNo", SqlDbType.VarChar, 50).Value = searchWildTerm;
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> partNumberQueryCountArchive(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_ARCHIVE_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + partNumber + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.Add("@partNo", SqlDbType.VarChar, 50).Value = searchWildTerm;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task<DataTable> operatorNameQuery(string operatorName, int pageSize, int currentPageIndex)

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
                        command.Parameters.Add("@opName", SqlDbType.VarChar, 10).Value = searchWildTerm;
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> operatorNameQueryCount(string operatorName)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("OPERATOR_NAME_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Add wildcard to broaden search term.
                        String searchWildTerm = "%" + operatorName + "%";

                        //Paramaterized to prevent SQL Injection, bind values.
                        command.Parameters.Add("@opName", SqlDbType.VarChar, 10).Value = searchWildTerm;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task softDeleteQuery(int part_history_id)
        {
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("MARK_PART_DELETED", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection.
                        command.Parameters.Add("@p_part_history_id", SqlDbType.Int).Value = part_history_id;

                        await connection.OpenAsync();

                        //Execute the stored procedure to mark record as deleted.
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Execute query to restore previously deleted record.
        internal async Task restoreRecordQuery(int part_history_id)
        {
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("RESTORE_PART", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Paramaterized to prevent SQL Injection.
                        command.Parameters.Add("@p_part_history_id", SqlDbType.Int).Value = part_history_id;

                        await connection.OpenAsync();

                        //Execute the stored procedure to mark record as deleted.
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restore record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Reads data from DB source, returns dataTable from FABRICATION_DEPARTMENT_QUERY stored procedure.
        internal async Task<DataTable> fabricationDepartmentQuery(int pageSize, int currentPageIndex)
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
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> fabricationDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("FABRICATION_DEPARTMENT_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task<DataTable> machiningDepartmentQuery(int pageSize, int currentPageIndex)
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
                        command.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                        command.Parameters.Add("@offsetNum", SqlDbType.Int).Value = offsetNum;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        internal async Task<int> machiningDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("NC_MACHINE_WORK_QUERY_COUNT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        //Execute query, save result in result object.
                        object returnedCount = await command.ExecuteScalarAsync();

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
        internal async Task<List<Operators>> GetOperators()
        {
            //Create new List object.
            List<Operators> returnList = new List<Operators>();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_OPERATORS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
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
        internal async Task<List<Customers>> GetCustomers()
        {
            //Create new List object.
            List<Customers> returnList = new List<Customers>();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("GET_CUSTOMERS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
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
        internal async Task<bool> CreateRecord(int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            int rowsAffected = 0;

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
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

                        await connection.OpenAsync();

                        command.Parameters.AddRange(pms);

                        rowsAffected = await command.ExecuteNonQueryAsync();
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
        internal async Task<bool> UpdateRecord(int partID, int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            int rowsAffected = 0;
            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
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

                        await connection.OpenAsync();

                        command.Parameters.AddRange(pms);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update selected record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected > 0;
        }

        //Function to get user information for a given userName.
        internal Users getUserInfo(string userName)
        {
            //Create new Users object and start as null. If the user doesn't exist, it stays null.
            Users user = null;

            //Open connection to DB.
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                //Get the stored procedure from the DB.
                using (SqlCommand command = new SqlCommand("USER_NAME_QUERY", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    //Paramaterized to prevent SQL Injection, bind values.
                    command.Parameters.Add("@username", SqlDbType.VarChar, 48).Value = userName;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new Users();
                            user.USER_ID = reader.GetInt32(0);
                            user.USER_NAME = reader.GetString(1);
                            user.ROLES_ID = reader.GetInt32(2);
                            user.ROLE_NAME = reader.GetString(3);
                        }
                    }
                }
            }
            return user;
        }

        //Method to run GET_CUSTOMER_QTY_LAST_6_MONTHS query from SQL server to populate chart with queried data.
        internal async Task<DataTable> LoadCustomerChartData()
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

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        //Method to run GET_OPERATOR_QTY_LAST_6_MONTHS query from SQL server to populate chart with queried data.
        internal async Task<DataTable> LoadOperatorChartData()
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

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
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
        //Method to run NEXT_SIX_MONTHS_BY_DEPARTMENT query from SQL server to populate chart with queried data.
        internal async Task<DataTable> LoadDepartmentChartData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("NEXT_SIX_MONTHS_BY_DEPARTMENT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load department dashboard chart data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
        //Method to run NEXT_SIX_MONTHS_JOBS_BY_DEPARTMENT query from SQL server to populate datagridview with queried data.
        internal async Task<DataTable> LoadDepartmentGridViewData()
        {
            //Create new datatable to store query results.
            DataTable dataTable = new DataTable();

            try
            {
                //Open connection to DB.
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    //Get the stored procedure from the DB.
                    using (SqlCommand command = new SqlCommand("NEXT_SIX_MONTHS_JOBS_BY_DEPARTMENT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load department grid view data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        //Method to run sp_GetQuarterlyProduction query from SQL server to populate chart with queried data.
        internal async Task<DataTable> LoadQuarterHistory()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetQuarterlyProduction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load last and predicted fiscal year volume dashboard data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
    }
}





