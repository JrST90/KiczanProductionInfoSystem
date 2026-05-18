using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace KiczanProductionInfoSystem
{
    internal class DAO
    {
        //Build connection string to connect to Microsoft SQL Server.
        private string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KICZAN_PRODUCTION_SYSTEM;Trusted_Connection=True;TrustServerCertificate=True;";

        //Reads data from DB source, returns dataTable from DATE_DUE_RANGE_QUERY stored procedure. 
        //Reads beginning date and end date from user input from text box on UI.
        internal DataTable dateDueRangeQuery(string dateRange, int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Create array to store date values from string input.
            //Split date range at occurence of '-' character. EX: 01/01/2024-02/01/2024
            string[] dateArray = dateRange.Split('-');

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("DATE_DUE_RANGE_QUERY", connection);
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

            //connection.Close();
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("DATE_DUE_RANGE_QUERY_COUNT", connection);
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "PART_NUMBER_QUERY" from SQL server.
            SqlCommand command = new SqlCommand("PART_NUMBER_QUERY", connection);
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_COUNT", connection);
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "PART_NUMBER_QUERY_ARCHIVE" from SQL server.
            SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_ARCHIVE", connection);
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("PART_NUMBER_QUERY_ARCHIVE_COUNT", connection);
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "OPERATOR_NAME_QUERY" from SQL server.
            SqlCommand command = new SqlCommand("OPERATOR_NAME_QUERY", connection);
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
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("OPERATOR_NAME_QUERY_COUNT", connection);
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
            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "MARK_PART_DELETED" from SQL server.
            SqlCommand command = new SqlCommand("MARK_PART_DELETED", connection);
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
            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "MARK_PART_DELETED" from SQL server.
            SqlCommand command = new SqlCommand("RESTORE_PART", connection);
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "NC_MACHINE_WORK_QUERY" from SQL server.
            SqlCommand command = new SqlCommand("FABRICATION_DEPARTMENT_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("FABRICATION_DEPARTMENT_QUERY_COUNT", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Execute query, save result in result object.
            object result = command.ExecuteScalar();

            //Convert result to Int and save in totalRows.
            totalRows = Convert.ToInt32(result);

            connection.Close();

            return totalRows;
        }

        //Reads data from DB source, returns dataTable from NC_MACHINE_WORK_QUERY stored procedure.
        internal DataTable machiningDepartmentQuery(int pageSize, int currentPageIndex)
        {
            //Create new dataTable to store query results.
            DataTable dataTable = new DataTable();

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get stored procedure "NC_MACHINE_WORK_QUERY" from SQL server.
            SqlCommand command = new SqlCommand("NC_MACHINE_WORK_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Set offset to be bound using currentPageIndex and pageSize arguments.
            int offsetNum = ((currentPageIndex - 1) * pageSize);

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("pageSize", pageSize);
            command.Parameters.AddWithValue("offsetNum", offsetNum);

            //Use adapter object to fill dataTable with query results.
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            connection.Close();

            return dataTable;
        }

        //Count all records for NC_MACHINE_WORK_QUERY.
        internal int machiningDepartmentQueryCount()
        {
            //Set initial value of totalRows.
            int totalRows = 0;

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("NC_MACHINE_WORK_QUERY_COUNT", connection);
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

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("GET_OPERATORS", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Read returned values from query into returnList.
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
            connection.Close();

            return returnList;
        }

        //Get customer names for customerComboBox
        internal List<Customers> GetCustomers()
        {
            //Create new List object.
            List<Customers> returnList = new List<Customers>();

            //Connect to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("GET_CUSTOMERS", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Read returned values from query into returnList.
            using (SqlDataReader reader = command.ExecuteReader())
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
            //Connect to DB.
            SqlConnection sqlconnect = new SqlConnection(sqlConnectionString);

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

            //Build SQL command retreived from stored procedure "CREATE_RECORD".
            SqlCommand command = new SqlCommand();
            command.Connection = sqlconnect;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CREATE_RECORD";
            command.Parameters.AddRange(pms);

            //Open connection to DB and execute command with parameter array.
            sqlconnect.Open();
            int rowsAffected = command.ExecuteNonQuery();
            sqlconnect.Close();
            return rowsAffected > 0;
        }
        internal bool UpdateRecord(int partID, int custID, int opID, string partNumber, DateTime dateDue, string poNumber, string quantity, string checkedOperations, DateTime dateReceived, int toDelete)
        {
            //Connect to DB.
            SqlConnection sqlconnect = new SqlConnection(sqlConnectionString);

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

            //Build SQL command retreived from stored procedure "UPDATE_RECORD".
            SqlCommand command = new SqlCommand();
            command.Connection = sqlconnect;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UPDATE_RECORD";
            command.Parameters.AddRange(pms);

            //Open connection to DB and execute command with parameter array.
            sqlconnect.Open();
            int rowsAffected = command.ExecuteNonQuery();
            sqlconnect.Close();
            return rowsAffected > 0;
        }
        //Function to check if a userName exists within the DB.
        internal bool userNameCheck(string userName)
        {
            //int variable to store number of records with matching userName.
            int userCount = 0;

            //bool variable to serve as flag for existent of userName in DB.
            bool flag = false;

            //Open connection to DB.
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("USER_NAME_QUERY_COUNT", connection);
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
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            //Get the stored procedure from the DB.
            SqlCommand command = new SqlCommand("USER_NAME_QUERY", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Paramaterized to prevent SQL Injection, bind values.
            command.Parameters.AddWithValue("userName", userName);

            //Execute query, read returned values into user object.
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
            //connection.Close();
            connection.Close();

            return user;
        }
    }
}




