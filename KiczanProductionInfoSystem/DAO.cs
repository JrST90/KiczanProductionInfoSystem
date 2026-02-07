using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

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
    }
}
