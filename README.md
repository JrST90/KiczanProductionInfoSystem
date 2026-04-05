# CMIS 4920 Capstone: Kiczan Production Information System

This project utilizes a database written in SQL and an application with a user interface written in C#.
The goal of this project is to increase efficiency and productivity at Kiczan Manufacturing, by optimizing
the collection of work order related data as Kiczan Manufacturing receives work orders derived from customer
purchase orders. Work orders are stored as records within the database, and are retrieved by the user interface
application to assist department managers with the shop floor production process. Managers can retrieve records
utilizing a variety of search parameters, delete existing records in the event of errors or redundancies, add new records
upon receiving new work orders, and update existing records in the event of errors or other necessary scenarios.

## Setup & Installation
* **Dependencies**:
  -  MySql.Data.MySqlClient
  -  System.Text.RegularExpressions
  -  System.Globalization
  -  System.Collections.Generic
  -  System.Data
  -  System.Drawing
  -  System.Runtime.InteropServices
  -  System.Windows.Forms
  -  System.Windows.Forms
* **Build Environment**:
  - MySQL Workbench CE
  - MAMP to host SQL Server
  - IDE (Microsoft Visual Studio or Visual Studio Code)
  - MySQL.Data package version 9.6.0 by Oracle Corporation
  - Microsoft.Office.Interop.Excel by Microsoft
* **Set-Up Instructions (Production Code from Development)**:
  - Install MAMP to local device.
      - During installation, ensure virtual servers are specified as private and not public when prompted.
  - Install MySQL Workbench CE to local device.
      - Copy SQL code from DPS DB Script and execute to deploy schema and DB.
      - Copy SQL code from Delimiter KPS Stored Procedures to deploy stored procedures for query and record retrieval.
  - Install Microsoft Visual Studio
      - Using Git features, clone repository using URL https://github.com/JrST90/KiczanProductionInfoSystem
      - Use right click Checkout on remote repository branch Application-Main and pull to local repository.
      - Run the solution, application will run in its current production state. 
## Features & Usage
  - **Feature:** Initialized DB using build scripts
      - **Description:** Deploy SQL scripts to build DB tables/relationships and populate stored procedures, establish connection between application interface and DB.
      - **Usage Instructions:**
          - Open MAMP and start Apache and MySQL virtual servers.
          - Ensure that the username and password are set to root in the string variable connectionString in DAO.cs
          - Import the SQL files KPS DB Script & Delimiter KPS Stored Procedres using MySQL Workbench CE
      - **Usage Example:**
          - private string connectionString = "datasource=localhost;port=3306;username=root;" +
            "password=root;database=kiczan_production_system;";
  - **Feature:** Main User Interface with layout
      - **Description:** A Landing Page displaying production information functions through the use of drop down selectors and a text input box. Various types of queries are available to the user within the first drop down selector. Queries such as                                 "Search by Part Number" and "Search by Due Date Range" require the use of the text input box and Search button for query execution. Queries such as "Search By Operator Name" require the selection of a controlled pick using the second drop down selector                   and then pressing the Search button for query execution. Other queries, such as "Search by Fabrication Department" require only the selection of that query, and then pressing the Search button for query execution. All queries are supported by pagination for             users to page through returned results.
      - **Usage Instructions:**
          - **Search by Part Number:**
            - Select the "Search by Part Number" query from the left most drop down selector.
            - Enter the the entire part number, or partial part number, into the text input box.
            - Click the Search button to execute the query, and populate the UI with results returned from the DB.

          - **Search by Due Date Range:**
            - Select the "Search by Due Date Range" query from the left most drop down selector.
            - Enter the desired date range as "MM/DD/YYYY-MM/DD/YYYY".
            - Click the Search button to execute the query, and populate the UI with results returned from the DB.
   
          - **Search by Operator Name:**
            - Select the "Search by Operator Name" query from the left most drop down selector.
            - Select the desired operator name from the selector box to the right of the first selector box.
            - Click the Search button to execute the query, and populate the UI with results returned from the DB.
   
          - **Search by Fabrication Department:**
            - Select the "Search by Fabrication Department" query from the left most drop down selector.
            - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** 315
          - **Result:** The system displays all records whose PART_NUMBER attribute contain 315 with their text values.

          - **Input:** 01/01/2025-06/01/2025
          - **Result:** The system displays all records whose DUE_DATE attribute fall within the input range for the date values.

          - **Input:** Select "Search by Operator Name" for first drop down selector selection, select "Brandon" for second drop down selector selection.
          - **Result:** The system displays all records whose OPERATOR_NAME match the operator name selection from the second drop down selector.

          - **Input:** Select "Search by Fabrication Department" for first drop down selector selection.
          - **Result:** The system displays all records whose JOB_ID are equal to 1,2,3, & 4, representing job roles assigned to the Fabrication Department.
  - **Feature:** Search by Part Number Query with input validation
      - **Description:** A query selection that selects records from the DB whose PART_NUMBER attribute match or contain the input provided by the user from the input text box on the UI. User input is validated to prevent bad input containing restricted characters.
      - **Usage Instructions:**
          - Select the "Search by Part Number" query from the left most drop down selector.
          - Enter the the entire part number, or partial part number, into the text input box.
          - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** 315%
          - **Result:** The system displays an error message to the user stating "Enter Part Number containing letters, numbers, and hyphens only".
  - **Feature:** Search by Due Date Range query with input validation
      - **Description:** A query selection that selects records from the DB whose DUE_DATE attribute fall within the date range input provided by the user from the input text box on the UI. User input is validated to enforce format and sequencing constraints.
      - **Usage Instructions:**
          - Select the "Search by Due Date Range" query from the left most drop down selector.
          - Enter the desired date range as "MM/DD/YYYY-MM/DD/YYYY".
          - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** 0101/2025-06/01/2025
          - **Result:** The system displays an error message to the user stating "Due Date Range must be of the format: mm/dd/yyyy-mm/dd/yyyy".

          - **Input:** 06/01/2025-01/01/2025
          - **Result:** The system displays an error message to the user stating "Beginning Date occurs after ending date. Please enter as (Beginning Date)mm/dd/yyyy-(Ending Date)mm/dd/yyyy".
  - **Feature:** Soft Delete & Restore
      - **Description:** A right click context menu that appears when the user right clicks on a record in the UI, the delete function is selectable for every query except the "Part Number in Archive" query. Deleting a record from a query search "soft deletes" the                 query, changing the TO_DELETE attribute to TRUE, removing it from the UI and subsequent queries. The record can be restored from the "Search by Part Number in Archive" query search, removing it from the soft delete archive, and restoring it to the main table            for future queries.
      - **Usage Instructions:**
          - Run a query from the first drop down selector.
          - Right click on a selected record and click "Delete Record (Archive)", make note of the part number for reference during restoration.
          - Select the query "Search by Part Number in Archive", type the noted part number into the input text box and press the Search button.
          - Right click on the deleted record within the archive, click "Restore Record" restoring the record to the PART_HISTORY table.
      - **Usage Example:**
          - **Input:** 315
          - **Result:** The system displays all records whose PART_NUMBER attribute contain 315 within their text values. Right clicking on the first record and clicking "Delete Record (Archive)" soft deletes the record and refreshes the current table to reflect its                  deleted status.
  - **Feature:** Update Record (Initial State)
      - **Description:** A right click context menu that appears when the user right clicks on a record in the UI, the update function is selectable for every query except the "Part Number in Archive" query. Updating a record opens a new UI form, allowing the user to             input new values into record attributes, with validation, to change the current state of a record in the event of entry error during record creation.
      - **Usage Instructions:**
          - Run a query from the first drop down selector.
          - Right click on a selected query and click "Update Record".
          - The Update Record form is brought up (Inital State).
      - **Usage Example:**
          - **Input:** 315
          - **Result:** The system displays all records whose PART_NUMBER attribute contain 315 with their text values. Right clicking on the first record and clicking "Update Record" brings up the Update Record form (Initial State).
  - **Feature:** Search by Part Number in Archive
      - **Description:** A query selection that searchs the Archive for soft deleted records by entire or part of a specified PART_NUMBER text value, records can be restored from here, refreshing the archive and restoring the record to the PART_HISTORY table.
      - **Usage Instructions:**
          - Select the query "Search by Part Number in Archive" from the first drop down selector.
          - Enter the entire or partial PART_NUMBER.
          - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** 801
          - **Result:** The system displays all records whose PART_NUMBER attribute contain 801 with their text values.
  - **Feature:** Search by Operator Name
      - **Description:** A query selection that searchs the DB for records whose OPERATOR_NAME match the operator name selected by the user from the second drop down selector.
      - **Usage Instructions:**
          - Select the "Search by Operator Name" query from the left most drop down selector.
          - Select the desired operator name from the selector box to the right of the first selector box.
          - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** Select "Search by Operator Name" for first drop down selector selection, select "Brandon" for second drop down selector selection.
          - **Result:** The system displays all records whose OPERATOR_NAME match the operator name selection from the second drop down selector.
  - **Feature:** Search by Fabrication Department
      - **Description:** A query selection that searchs the DB for records whose JOB_ID are equal to values representing job roles assigned to the Fabrication Department.
      - **Usage Instructions:**
          - Select the "Search by Fabrication Department" query from the left most drop down selector.
          - Click the Search button to execute the query, and populate the UI with results returned from the DB.
      - **Usage Example:**
          - **Input:** Select "Search by Fabrication Department" for first drop down selector selection.
          - **Result:** The system displays all records whose JOB_ID are equal to 1,2,3, & 4, representing job roles assigned to the Fabrication Department.
  - **Feature:** Create Record
      - **Description:** A button on the main UI "Create Record" that upon clicking, opens a new UI form for the creation of new records to be entered into the system. Record attribute fields are input by the user and are supported by input validation.
      - **Usage Instructions:**
          - Click on the Create Record button.
          - The Create Record form is brought up.
          - Available to the user are all input fields: Customer, Operator, Part Number, Quantity, Operations, Order Number, Date Received, Due Date.
      - **Usage Example:**
          - **Input:** Selecting a Customer of "Alstom," selecting an Operator of "Dave," inputting a Part Number of "3156," a Quantity of "10," selecting the Operations check box items of Laser and Press Brake, inputting a Order Number of "7190," inputting a Date Received of "03/22/2026," inputting a Due Date of "03/22/2026" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon successful field input "Record Status: Record Successfully Created!" The record is added to the PART_HISTORY table and the ID value is autoincremented.
          - **Input:** Leaving the Part Number field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Part Number must not be empty and may only contain letters, numbers, or hyphens."
          - **Input:** Inputting a Part Number of "3156$" and pressing the "Submit" button.
          - - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Part Number must not be empty and may only contain letters, numbers, or hyphens."
          - **Input:** Leaving the Quantity field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must not be empty, please enter a value."
          - **Input:** Inputting a Quantity of "-1" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must be an integer greater than or equal to 1."
          - **Input:** Inputting a Quantity of "abc" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must be a numerical value."
          - **Input:** Leaving the Operations check box selection area blank and not selecting atleast one operation, then pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Please select at least one operation"
          - **Input:** Leaving the Order Number field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Purchase Order Number is required."
          - **Input:** Inputting an Order Number of "717" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number must be between 4 and 20 characters."
          - **Input:** Inputting an Order Number of "717890999415886009343" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number must be between 4 and 20 characters."
          - **Input:** Inputting an Order Number of "7175&" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number can only contain letters, numbers, and hyphens."
          - **Input:** Pressing the "Clear" button on the Create Record form.
          - **Result:** All input fields on the Create Record form are cleared of inputs.
          - **Input:** Pressing the "Back to Menu" button the Create Record form.
          - **Result:** The Create Record form is closed, and the user is returned to the main user interface.
          - **Input:** Leaving the Customer drop down selector blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Please select a Customer."
          - **Input:** Leaving the Operator drop down selector blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Please select an Operator."
          - **Input:** Leaving the Date Received or Due Date field blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must not be empty, please enter a value."
          - **Input:** Entering 03222026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
          - **Input:** Entering 03-22-2026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
          - **Input:** Entering 3/22/2026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Creation Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
 - **Feature:** Update Record
      - **Description:** A right click context menu that appears when the user right clicks on a record in the UI, selecting "Update Record" opens a new UI form for the updating of existing records already entered into the system. Record attribute fields are input by the user and are supported by input validation.
      - **Usage Instructions:**
          - Right click on a record after a query.
          - Click "Update Record".
          - the "Update Record" Interface is presented to the user.
          - Available to the user are all input fields: Customer, Operator, Part Number, Quantity, Operations, Order Number, Date Received, Due Date.
      - **Usage Example:**
          - **Input:** Selecting a Customer of "Alstom," selecting an Operator of "Dave," inputting a Part Number of "3156," a Quantity of "10," selecting the Operations check box items of Laser and Press Brake, inputting a Order Number of "7190," inputting a Date Received of "03/22/2026," inputting a Due Date of "03/22/2026" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon successful field input "Record Status: Record Successfully Updated!" The record is added to the PART_HISTORY table and the ID value is autoincremented.
          - **Input:** Leaving the Part Number field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Part Number must not be empty and may only contain letters, numbers, or hyphens."
          - **Input:** Inputting a Part Number of "3156$" and pressing the "Submit" button.
          - - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Part Number must not be empty and may only contain letters, numbers, or hyphens."
          - **Input:** Leaving the Quantity field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must not be empty, please enter a value."
          - **Input:** Inputting a Quantity of "-1" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must be an integer greater than or equal to 1."
          - **Input:** Inputting a Quantity of "abc" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Quantity must be a numerical value."
          - **Input:** Leaving the Operations check box selection area blank and not selecting atleast one operation, then pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Please select at least one operation"
          - **Input:** Leaving the Order Number field empty during form input and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Purchase Order Number is required."
          - **Input:** Inputting an Order Number of "717" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number must be between 4 and 20 characters."
          - **Input:** Inputting an Order Number of "717890999415886009343" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number must be between 4 and 20 characters."
          - **Input:** Inputting an Order Number of "7175&" and pressing the "Submit" button.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Order Number can only contain letters, numbers, and hyphens."
          - **Input:** Pressing the "Clear" button on the Create Record form.
          - **Result:** All input fields on the Create Record form are cleared of inputs.
          - **Input:** Pressing the "Back to Menu" button the Create Record form.
          - **Result:** The Create Record form is closed, and the user is returned to the main user interface.
          - **Input:** Leaving the Customer drop down selector blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Please select a Customer."
          - **Input:** Leaving the Operator drop down selector blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Please select an Operator."
          - **Input:** Leaving the Date Received or Due Date field blank.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must not be empty, please enter a value."
          - **Input:** Entering 03222026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
          - **Input:** Entering 03-22-2026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
          - **Input:** Entering 3/22/2026 as the date into the Date Received or Due Date fields.
          - **Result:** The system displays a status update to the user upon unsuccessful field input "Record Status: Record Update Error!", and next to the input field on the form an error provider and message is displayed to the user stating "Record not Complete! Date must be in MM/DD/YYYY format."
       
## Test Credentials
  - Prior to production, testing will be performed with all developers on Team 2 having full CRUD access to ensure the expected functionality exsists for Create, Read, Update, and Delete processes.
  - Once the application has been deployed to production, and the applications associated database has been deployed and populated with existing data authentication will be performed as follows:
    - Current Kiczan management personel usernames will be added to the USERS table within the database.
    - An authentication function will exist within Form1.cs that:
      - Retrieves the current Windows session username.
      - Queries the USERS table to check if the current Windows session username exists within the table.
      - If the Windows session username exists within the USERS table, the function will then retrieve the associated USER_ID from the USERS table to query the USER_ROLES table.
      - Finally, the associated usernames USER_ID will be used to query and retrieve the USER_ID's associated ROLE_ID from the USER_ROLES table, and use the ROLE_ID to query the ROLES table to verify which role the current user has, which will regulate permissions and access when using the application as it interacts with the data contained within the database.

## Development Workflow
# Planning
  - Plan and assign develop and test tasks on designated Sprint project board defined by project management plan.
  - Assign team members to tasks.
  - Move Sprint tasks into To Do column and through In Progess, Review, Requested Changes and Complete phases.
# Development
  - Utilize the branches Main and Application-Main to contain fully developed and tested production code. Main contains production SQL code for DB deployment, Application-Main contains production C# code for the applications front and backend.
  - Utilize the branches Develop and Application-Develop to integrate new features from sub branches dedicated to additional feature development and bug fixes, before merging into the production branches of Main and Application-Main.
  - Derive sub branches from the branches of Develop and Application-Develop, utilize these sub branches to develop and test new functionalities and to hold them in isolation before merging.
  - Utilize Pull Requests with required team member reviews and approvals before merging any sub branch into its parent branch. Sub branches dedicated to new features require assigned team member reviews and approvals before merging can take place.
  - Utilize a final Pull Request for each Sprint with required team member reviews and approvals before merging the integration branches of Develop and Application-Develop into the production branches of Main and Application-Main.
  - Update README.md and SPRINT_LOG.md with development progress at the end of each scheduled Sprint, detailing accomplished tasks and added features/functionalities for stakeholder review.
  - Move tasks to appropriate columns for each phase of development
# Review
  - Open Pull Requests to review, approve, and merge feature sub branches into Application-Develop and Develop integration branches.
  - Open Pull Requests to review, approve, and merge integration branches into Main and Application-Main production branches.
  - Submit assigned team member review and approval for each Pull Request.
  - Perform testing prior to merge approval to ensure user story criteria has been met, and that feature sub branches are bug/error free prior to merging.
  - Request that changes in the Pull Request be made in the event that user story critieria is not met during the current Sprint, or upon the discovery of bugs/errors.
  - Submit merge approval for integration and production merges once appropriate team member review has been made, and changes for corrective action during review have been implemented.
# Wrap-Up
  - Complete SPRINT_LOG.md and README.md with current Sprint updates, reflecting the status of Sprint tasks, feature descriptions, usage instructions/examples, and test cases to ensure the proper documentation of current Sprint tasks.
  - Record Sprint Retrospective video with new feature demonstration, and a discussion of what went well, challenges, lessons learned, and future goals.
## Sprint Technical Logs
### See SPRINT_LOG.md
