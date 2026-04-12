using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace KiczanProductionInfoSystem
{
    internal class DAO
    {
        //Set the connection string to establish a conncetion to the database
        private string connectionString = "datasource=localhost;port=3306;username=root;" +
            "password=root;database=kiczan_production_system;";

        //Reads data from DB source, returns dataTable from DATE_DUE_RANGE_QUERY stored procedure. 
        //Reads beginning date and end date from user input from text box on UI.
        internal DataTable dateDueRangeQuery(string dateRange, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Create array to store date values from string input.
            //Split date range at occurence of '-' character. EX: 01/01/2024-02/01/2024
            string[] dateArray = dateRange.Split('-');

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "DATE_DUE_RANGE_QUERY" from SQL server.
            MySqlCommand command = new MySqlCommand("DATE_DUE_RANGE_QUERY", connection);
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;
        }

        //Count all records for DATE_DUE_RANGE_QUERY.
        internal int dateDueRangeQueryCount(string dateRange)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Split dateRange at the occurence of a hyphen.
            string[] dateArray = dateRange.Split('-');

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("DATE_DUE_RANGE_QUERY_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Parts date values stored in dateArray.
            DateTime bd = DateTime.Parse(dateArray[0]);
            DateTime ed = DateTime.Parse(dateArray[1]);

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("dateB", bd);
            command.Parameters.AddWithValue("dateE", ed);

            //Execute query, save result in result object
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        //Reads data from DB source, returns dataTable from PART_NUMBER_QUERY stored procedure.
        //Reads partNumber from user input in text box.
        internal DataTable partNumberQuery(string partNumber, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "PART_NUMBER_QUERY" from SQL server.
            MySqlCommand command = new MySqlCommand("PART_NUMBER_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + partNumber + "%";

            //Paramaterized to prevent SQL Injection.
            command.Parameters.AddWithValue("partNo", searchWildTerm);
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;
        }

        //Count all records for PART_NUMBER_QUERY.
        internal int partNumberQueryCount(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("PART_NUMBER_QUERY_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + partNumber + "%";

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("partNo", searchWildTerm);

            //Execute query, save result in result object.
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        //Reads data from DB source, returns dataTable from PART_NUMBER_QUERY_ARCHIVE stored procedure.
        //Reads partNumber from user input in text box.
        internal DataTable partNumberQueryArchive(string partNumber, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "PART_NUMBER_QUERY_ARCHIVE" from SQL server.
            MySqlCommand command = new MySqlCommand("PART_NUMBER_QUERY_ARCHIVE", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + partNumber + "%";

            //Paramaterized to prevent SQL Injection.
            command.Parameters.AddWithValue("partNo", searchWildTerm);
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;
        }

        //Count all records for PART_NUMBER_QUERY_ARCHIVE.
        internal int partNumberQueryCountArchive(string partNumber)
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("PART_NUMBER_QUERY_ARCHIVE_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + partNumber + "%";

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("partNo", searchWildTerm);

            //Execute query, save result in result object.
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        // Reads data from DB source, returns dataTable from OPERATOR_NAME_QUERY stored procedure.
        // Reads operatorName from user input in text box.
        internal DataTable operatorNameQuery(string operatorName, int pageSize, int currentPageIndex)

        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "OPERATOR_NAME_QUERY" from SQL server.
            MySqlCommand command = new MySqlCommand("OPERATOR_NAME_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + operatorName + "%";

            //Paramaterized to prevent SQL Injection.
            command.Parameters.AddWithValue("opName", searchWildTerm);
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))

            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;

        }

        // Count all records for OPERATOR_NAME_QUERY.
        internal int operatorNameQueryCount(string operatorName)

        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("OPERATOR_NAME_QUERY_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Add wildcard to broaden search term.
            String searchWildTerm = "%" + operatorName + "%";

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("opName", searchWildTerm);

            //Execute query, save result in result object.
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        //Execute query to mark recorded as deleted.
        internal void softDeleteQuery(int part_history_id)
        {
            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "MARK_PART_DELETED" from SQL server.
            MySqlCommand command = new MySqlCommand("MARK_PART_DELETED", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Paramaterized to prevent SQL Injection.
            command.Parameters.AddWithValue("p_part_history_id", part_history_id);

            //Execute the stored procedure to mark record as deleted.
            command.ExecuteNonQuery();

            connection.Close();
        }

        //Execute query to restore previously deleted record.
        internal void restoreRecordQuery(int part_history_id)
        {
            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "MARK_PART_DELETED" from SQL server.
            MySqlCommand command = new MySqlCommand("RESTORE_PART", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Paramaterized to prevent SQL Injection.
            command.Parameters.AddWithValue("p_part_history_id", part_history_id);

            //Execute the stored procedure to mark record as deleted.
            command.ExecuteNonQuery();

            connection.Close();
        }
        //Reads data from DB source, returns dataTable from FABRICATION_DEPARTMENT_QUERY stored procedure.
        internal DataTable fabricationDepartmentQuery(int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get stored procedure "NC_MACHINE_WORK_QUERY" from SQL server.
            MySqlCommand command = new MySqlCommand("FABRICATION_DEPARTMENT_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;
        }

        //Count all records for FABRICATION_DEPARTMENT_QUERY.
        internal int fabricationDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("FABRICATION_DEPARTMENT_QUERY_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Execute query, save result in result object.
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        //Get operator names for drop down menu comboBox2 and operatorComboBox
        internal List<Operators> GetOperators()
        {
            //Create new List object.
            List<Operators> returnList = new List<Operators>();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("GET_OPERATORS", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Read returned values from query into returnList.
            using (MySqlDataReader reader = command.ExecuteReader())
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
            connection.Close();

            return returnList;
        }

        //Get customer names for customerComboBox
        internal List<Customers> GetCustomers()
        {
            //Create new List object.
            List<Customers> returnList = new List<Customers>();

            //Connect to db.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("GET_CUSTOMERS", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Read returned values from query into returnList.
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customers op = new Customers
                    {
                        CUSTOMER_ID = reader.GetInt32(0),
                        CUSTOMER_NAME = reader.GetString(1),
                    };
                    returnList.Add(op);
                }
            }
            connection.Close();

            return returnList;
        }

        internal bool CreateRecord(int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlParameter[] pms = new MySqlParameter[10];

            //current dummy values and a varible for the operations selceted. 
            pms[0] = new MySqlParameter("PART_HISTORY_ID", MySqlDbType.Int32);
            pms[0].Value = DBNull.Value;

            //    command.Parameters.Add("@CUSTOMER_ID", MySqlDbType.Int32).Value = 17;
            pms[1] = new MySqlParameter("CUSTOMER_ID", MySqlDbType.Int32);
            pms[1].Value = custID;

            //   command.Parameters.Add("OPERATOR_ID", MySqlDbType.Int32).Value = 4;
            pms[2] = new MySqlParameter("OPERATOR_ID", MySqlDbType.Int32);
            pms[2].Value = opID;

            //  command.Parameters.Add("@PART_NUMBER", MySqlDbType.VarChar).Value = "7777";
            pms[3] = new MySqlParameter("PART_NUMBER", MySqlDbType.VarChar);
            pms[3].Value = partNumber;

            //   command.Parameters.Add("@DATE_DUE", MySqlDbType.DateTime).Value = new DateTime(2525, 12, 25);
            pms[4] = new MySqlParameter("DATE_DUE", MySqlDbType.DateTime);
            pms[4].Value = dateDue;

            //   command.Parameters.Add("@PURCHASE_ORDER_NUMBER", MySqlDbType.VarChar).Value = "7777";
            pms[5] = new MySqlParameter("PURCHASE_ORDER_NUMBER", MySqlDbType.VarChar);
            pms[5].Value = poNumber;

            //  command.Parameters.Add("@QTY", MySqlDbType.Int32).Value = 7777;
            pms[6] = new MySqlParameter("QTY", MySqlDbType.Int32);
            pms[6].Value = quantity;

            //  command.Parameters.Add("@OPERATIONS", MySqlDbType.VarChar).Value = checkedOperations;
            pms[7] = new MySqlParameter("OPERATIONS", MySqlDbType.VarChar);
            pms[7].Value = checkedOperations;

            //  command.Parameters.Add("@DATE_RECEIVED", MySqlDbType.DateTime).Value = new DateTime(2049, 6, 13);
            pms[8] = new MySqlParameter("DATE_RECEIVED", MySqlDbType.DateTime);
            pms[8].Value = dateReceived;

            // command.Parameters.Add("@TO_DELETE", MySqlDbType.Binary).Value = 0;
            pms[9] = new MySqlParameter("TO_DELETE", MySqlDbType.Binary);
            pms[9].Value = toDelete;

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CREATE_RECORD";
            //command.Parameters.Clear();
            command.Parameters.AddRange(pms);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
        internal bool UpdateRecord(int partID, int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlParameter[] pms = new MySqlParameter[10];

            //current dummy values and a varible for the operations selceted. 
            pms[0] = new MySqlParameter("p_PART_HISTORY_ID", MySqlDbType.Int32);
            pms[0].Value = partID;

            //    command.Parameters.Add("@CUSTOMER_ID", MySqlDbType.Int32).Value = 17;
            pms[1] = new MySqlParameter("p_CUSTOMER_ID", MySqlDbType.Int32);
            pms[1].Value = custID;

            //   command.Parameters.Add("OPERATOR_ID", MySqlDbType.Int32).Value = 4;
            pms[2] = new MySqlParameter("p_OPERATOR_ID", MySqlDbType.Int32);
            pms[2].Value = opID;

            //  command.Parameters.Add("@PART_NUMBER", MySqlDbType.VarChar).Value = "7777";
            pms[3] = new MySqlParameter("p_PART_NUMBER", MySqlDbType.VarChar);
            pms[3].Value = partNumber;

            //   command.Parameters.Add("@DATE_DUE", MySqlDbType.DateTime).Value = new DateTime(2525, 12, 25);
            pms[4] = new MySqlParameter("p_DATE_DUE", MySqlDbType.DateTime);
            pms[4].Value = dateDue;

            //   command.Parameters.Add("@PURCHASE_ORDER_NUMBER", MySqlDbType.VarChar).Value = "7777";
            pms[5] = new MySqlParameter("p_PURCHASE_ORDER_NUMBER", MySqlDbType.VarChar);
            pms[5].Value = poNumber;

            //  command.Parameters.Add("@QTY", MySqlDbType.Int32).Value = 7777;
            pms[6] = new MySqlParameter("p_QTY", MySqlDbType.Int32);
            pms[6].Value = quantity;

            //  command.Parameters.Add("@OPERATIONS", MySqlDbType.VarChar).Value = checkedOperations;
            pms[7] = new MySqlParameter("p_OPERATIONS", MySqlDbType.VarChar);
            pms[7].Value = checkedOperations;

            //  command.Parameters.Add("@DATE_RECEIVED", MySqlDbType.DateTime).Value = new DateTime(2049, 6, 13);
            pms[8] = new MySqlParameter("p_DATE_RECEIVED", MySqlDbType.DateTime);
            pms[8].Value = dateReceived;

            // command.Parameters.Add("@TO_DELETE", MySqlDbType.Binary).Value = 0;
            pms[9] = new MySqlParameter("p_TO_DELETE", MySqlDbType.Binary);
            pms[9].Value = toDelete;

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UPDATE_RECORD";
            //command.Parameters.Clear();
            command.Parameters.AddRange(pms);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
        //Function get get current OS username.
        internal static String getUserName()
        {
            //Get current OS username using Environment class.
            String userName = Environment.UserName;
            return userName;
        }
        //Function to check if a userName exists within the DB.
        internal bool userNameCheck(string userName)
        {
            //int variable to store number of records with matching userName.
            int userCount = 0;

            //bool variable to serve as flag for existent of userName in DB.
            bool flag = false;

            //Open connection to DB.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("USER_NAME_QUERY_COUNT", connection);
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
            return flag;
        }
        //Function to get user information for a given userName.
        internal Users getUserInfo(string userName)
        {
            //Create new Users object to store returned user information.
            Users user = new Users();

            //Open connection to DB.
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            MySqlCommand command = new MySqlCommand("USER_NAME_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("userName", userName);

            //Execute query, read returned values into user object.
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user.USER_ID = reader.GetInt32(0);
                    user.USER_NAME = reader.GetString(1);
                    user.ROLES_ID = reader.GetInt32(2);
                    user.ROLE_NAME = reader.GetString(3);
                }
            }
            connection.Close();

            return user;
        }
    }
}




