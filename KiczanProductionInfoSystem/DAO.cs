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
    }
}
