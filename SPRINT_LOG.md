# Sprint 1 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-01-20 to 2026-02-01
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Completion of Sprint 1 goals which include deployment of Database Script, creation of main user interface, and addition of part number and due date search queries.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 1: Write & Deploy Database Script Development & Testing | Daniel Puharic, Josh Stanczyk, Justin Kisner, Elian Garcia | Medium | Completed |
| Sprint 1: Main User Interface – Initial Application State – Develop & Test | Daniel Puharic, Josh Stanczyk, Justin Kisner, Elian Garcia | High | Completed |
| Sprint 1: Main User Interface – Search by Part Number – Develop & Test | Daniel Puharic, Josh Stanczyk, Justin Kisner, Elian Garcia | High | Completed |
| Sprint 1: Main User Interface – Search by Due Date Range – Develop & Test | Daniel Puharic, Josh Stanczyk, Justin Kisner, Elian Garcia | Medium | Completed |
## 2. Progress & Blockers
* **Completed Work:**
* Sprint 1: Write & Deploy Database Script Development & Testing
* Sprint 1: Main User Interface – Initial Application State – Develop & Test
* Sprint 1: Main User Interface – Search by Part Number – Develop & Test
* Sprint 1: Main User Interface – Search by Due Date Range – Develop & Test
* **Incomplete Tasks:**
* All Sprint 1 Tasks Completed
* **Resolution Plan:**
* All Sprint 1 Tasks Completed

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Update Record Right Click Event] | Unit | Passed | PR #8 |
| [Letter Casing in User Input for Search by Part Number] | Unit | Passed | PR #8 |
| [Due Date Validation] | Unit | Passed | PR #9 |
| [Search by Due Date Query] | System | Passed | PR #10 |
| [Revise Where Clause/Soft Delete & Restore w/ Archive] | Unit | Passed | PR #11 & PR# 12 |
| [Part Number Archive with Soft Delete & Restore Functions] | System | Passed | PR #13 |
| [Soft Delete & Restore Table Refresh] | Usability | Passed | PR #13 |
| [Revise Addition Queries w/ Where Clause] | Unit | Passed | PR #14 |
| [Revise Stored Procedure Script Consolidate & Run as Complete Script] | Unit | Passed | PR #14 |

## 4. Bug Tracking
* **Bug Description:** [Update Record Right Click Event]
* **Severity:** [High] | **Fix Status:** [Fixed]
* **Bug Description:** [Letter Casing in User Input for Search by Part Number]
* **Severity:** [High] | **Fix Status:** [Fixed]
* **Bug Description:** [Table Refresh after Delete and Restore]
* **Severity:** [High] | **Fix Status:** [Fixed]
* **Bug Description:** [Stored Procedure Script as Complete Script]
* **Severity:** [High] | **Fix Status:** [Fixed]

# Sprint 2 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-02-02 to 2026-02-15
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Completion of Sprint 2 goals which include , Main User Interface – Search by Operator Name – Develop & Test, Main User Interface – Search by Fabrication Department – Develop & Test, and Create Record Interface – Initial State – Develop & Test.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 2 :Main User Interface Wireframe – Search by Operator Name – Query Success State | Elian Garcia, Josh Stancyzk | Medium | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Operator Name Function (DAO.cs) | Elian Garcia | Boolean returns true in test environment | Passed | 
| Operator Query Function (DAO.cs) | Elian Garcia | Boolean returns true in test environment | Passed |
| Operator Query Count Function (DAO.cs) | Elian Garcia | Boolean returns true in test environment | Passed |
| Operator Query Event (Form1.cs) | Josh Stancyzk | Page was popualted to form1.cs | Passed |
| Operator Selection on ComboBox1 Index Change (Form1.cs) | Josh Stancyzk | In test enviroment was able to return the corrent selected Index | Passed |
| Operator Selection on ComboBox2 (Form1.cs) | Josh Stancyzk | Test popuplates combobox2 with a list and filters operator datagrid object query event | Passed |
| Operatory Query Pagination (Form1.cs) | Josh Stancyzk | Test to page through forward and reverse encounted no errors | Passed |
| Sprint 2: Main User Interface Wireframe – Search by Fabrication Department – Query Success State | Daniel Puharic, Justin Kisner | Medium | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Fabrication Query (DAO.cs) | Daniel Puharic | Boolean returns true in test environment | Passed | 
| Fabrication Query Count (DAO.cs) | Daniel Puharic | Boolean returns true in test environment | Passed | 
| Fabrication Query Event (Form1.cs) | Daniel Puharic | Page was popualted to form1.cs | Passed |
| Fabrication Selection on ComboBox1 Index Change (Form1.cs) | Justin | In test environment was able to return the correct selected Index | Passed |
| Fabrication Query Pagination (Form1.cs) | Justin Kisner | Test to page through forward and reverse encountered no errors | Passed |
| Sprint 2: Create Record Interface Wireframe – Initial Empty State | Daniel Puharic, Josh Stanczyk,  | Medium | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create the CreateRecord class form (CreateRecord.cs) | Daniel Puharic | Test if the blank form appears to have the correct labels | Passed |
| Create Record Button Event (Form1.cs) | Josh Stancyzk | Pass/Fail test if the blank form open on event trigger to have the correct labels | Passed |


## 2. Progress & Blockers
* **Completed Work:**
* Sprint 2 :Main User Interface Wireframe – Search by Operator Name – Query Success State 
* Sprint 2: Main User Interface Wireframe – Search by Fabrication Department – Query Success State
* Sprint 2: Create Record Interface Wireframe – Initial Empty State
* **Incomplete Tasks:**
* All Sprint 2 Tasks Completed
* **Resolution Plan:**
* All Sprint 2 Tasks Completed

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Fabrication Query (DAO.cs)] | System | Passed | PR #20 |
| [Fabrication Query Count (DAO.cs)] | System | Passed | PR #20 |
| [Fabrication Query Event (Form1.cs)] | Unit | Passed | PR #20 |
| [Create the CreateRecord class form (CreateRecord.cs)] | System | Passed | PR #21 |
| [Create Record Button Event (Form1.cs)] | Unit | Passed | PR #22 |
| [Fabrication Selection on ComboBox1 Index Change (Form1.cs) ] | Unit | Passed | PR #23|
| [Fabrication Query Pagination (Form1.cs)] | Unit | Passed | PR #23 |
| [Operator Name Function (DAO.cs)] | System | Passed | PR #24 |
| [Operator Query Count Function (DAO.cs)] | System | Passed | PR #24 |
| [Operator Query Event (Form1.cs)] | Unit | Passed | PR #25 |
| [Operator Selection on ComboBox1 Index Change (Form1.cs)] | Usability | Passed | PR #25 |
| [Operatory Query Pagination (Form1.cs)] | Unit | Passed | PR #25 |



## 4. Bug Tracking
* **Bug Description:** [Updated the default date format
from XX/XX/XXXX-XX/XX/XXXX to MM/DD/YYYY-MM/DD/YYYY]
* **Severity:** [High] | **Fix Status:** [Fixed]


# Sprint 3 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-02-16 to 2026-03-01
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Completion of Sprint 3 goals which include, Create Record Interface – Part Number & Purchase Order Number Validation – Develop & Test & Create Record Interface – Quantity & Operations Validation – Develop & Test.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 3 :Create Record Interface Wireframe – Part Number & Purchase Order Number Validation – Develop & Test | Elian Garcia, Justin Kisner | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create Record Interface Wireframe – Part Number Error Validation State | Elian Garcia | Boolean returns true in test environment | Passed |
| Create Record Interface Wireframe – Purchase Order Number Error | Justin Kisner | String returns empty or with error message in test environment | Passed | 
| Sprint 3 :Create Record Interface – Quantity & Operations Validation – Develop & Test | Daniel Puharic, Josh Stanczyk | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create Record Interface Wireframe – Quantity Error Validation State | Josh Stanczyk | String returns empty or with error message in test environment | Passed |
| Create Record Interface Wireframe – Operations Error Validation State | Daniel Puharic | String concatenates selected items successfully or outputs error message in test environment | Passed |

## 2. Progress & Blockers
* **Completed Work:**
* Sprint 3: Create Record Interface – Part Number & Purchase Order Number Validation – Develop & Test 
* Sprint 3: Create Record Interface – Quantity & Operations Validation – Develop & Test
* **Incomplete Tasks:**
* All Sprint 3 Tasks Completed
* **Resolution Plan:**
* All Sprint 3 Tasks Completed

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Feature-Operations input and insert functionality.] | Unit | Passed | PR #29 |
| [Feature: Quantity Form Input Validation.] | Unit | Passed | PR #30 |
| [Feature: added Part Number field and validation to CreateRecord] | Unit | Passed | PR #32 |
| [Feature: Adds PO number to CreateRecord.cs and DataValidation.cs] | Unit | Passed | PR #33 |
| [Fix: Conditional Logic for Error Output] | Unit | Passed | PR #34 |

## 4. Bug Tracking
* **Bug Description:** [Updated Error Labels in Create Record Form to clear
* when the "clear" button is selected in the form]
* **Severity:** [High] | **Fix Status:** [Fixed]


# Sprint 4 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-03-01 to 2026-03-22
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Completion of Sprint 4 goals which include, Create Record Interface – Date Received/Due Validation – Develop & Test & Create Record Interface – Customer & Operator Validation/Record Insertion.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 4: Create Record Interface – Date Received/Due Validation – Develop & Test | Elian Garcia, Justin Kisner | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create Record Interface Wireframe – Date Received Format Error Validation State | Elian Garcia | String returns empty or with error message in test environment | Passed |
| Create Record Interface Wireframe – Date Due Format Error Validation State | Justin Kisner | String returns empty or with error message in test environment | Passed | 
| Sprint 4: Create Record Interface – Customer & Operator Validation/Record Insertion – Develop & Test | Daniel Puharic, Josh Stanczyk | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create Record Interface Wireframe – Customer/Operator Error Validation State | Josh Stanczyk | String returns empty or with error message in test environment | Passed |
| Create Record Interface Wireframe – Record Insertion Success State | Daniel Puharic | Record is successfully inserted into DB upon passing through form input validation constraints | Passed |

## 2. Progress & Blockers
* **Completed Work:**
* Sprint 4: Create Record Interface – Date Received/Due Validation – Develop & Test 
* Sprint 4: Create Record Interface – Customer & Operator Validation/Record Insertion – Develop & Test
* **Incomplete Tasks:**
* All Sprint 4 Tasks Completed
* **Resolution Plan:**
* All Sprint 4 Tasks Completed

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Feature: Feature/Create-Record/Due-Date] | Unit | Passed | PR #43 |
| [Feature: Create Record Date Received Functionality] | Unit | Passed | PR #42 |
| [Feature: Feature/ Create record logic and tethered stored procedure] | System | Passed | PR #41 |
| [Feature: Create Record Operator and Customer Selection] | Unit | Passed | PR #40 |

## 4. Bug Tracking
* **Bug Description:** [Else block was unreachable when updating Record Status from either Successful or Unsuccessful, providing a blank form element that did not alert user above buttons of errors existing on form.]
* **Severity:** [Medium] | **Fix Status:** [Fixed]
* **Bug Description:** [Migrate Record Insertion process from CreateRecord.cs to its own function within DAO.cs for modularity.]
* **Severity:** [Medium] | **Fix Status:** [Fixed]


# Sprint 5 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-03-23 to 2026-04-05
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Completion of Sprint 5 goals which include, Update Record Interface – Customer & Operator Validation/ Operations & Quantity Validation – Develop & Test & Update Record Interface – Part & Order Number Validation/Date Received & Date Due Validation/Update Record/Clean & Menu Buttons – Develop & Test.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 5: Update Record Interface – Customer & Operator Validation/Operations & Quantity Validation – Develop & Test | Elian Garcia, Justin Kisner | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Update Record Interface Wireframe – Customer & Operator Validation – Develop & Test | Elian Garcia | Validation methods correctly return error messages when no selection is made. ErrorProviders and labels display appropriate messages. No runtime errors occur when selections are cleared. | Passed |
| Update Record Interface Wireframe – Operations & Quantity Validation – Develop & Test | Justin Kisner | Quantity validation returns string value in the event of validation violation. Operations validation returns string value in the event of a validation violation, builds string of checked items for record insertion when succsessful. | Passed | 
| Sprint 5: Update Record Interface – Part & Order Number Validation/Date Received & Date Due Validation/Update Record/Clear & Menu Buttons – Develop & Test | Daniel Puharic, Josh Stanczyk | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Update Record Interface Wireframe – Part & Order Number Validation – Develop & Test Date Received/Date Due Validation – Develop & Test | Josh Stanczyk | Boolean returns true in test environment. String returns empty or with error message in test environment | Passed |
| Update Record Interface Wireframe – Update Insertion Success State Clear & Menu Buttons | Daniel Puharic | UpdateRecord() function in DAO.cs successfully called stored procedure UPDATE_RECORD from database and updates user selected record with passed in values from input fields. UPDATE_RECORD stored procedure successfully received pass in values from input fields and updates selected record by PART_HISTORY_ID. Clear and Menu buttons function as intended | Passed |

## 2. Progress & Blockers
* **Completed Work:**
* Sprint 5: Update Record Interface – Part & Order Number Validation/Date Received & Date Due Validation – Develop & Test
* Sprint 5: Update Record Interface – Update Record/Clear & Menu Buttons – Develop & Test
* Sprint 5: Update Record Interface – Customer & Operator Validation/Operations & Quantity Validation – Develop & Test
* **Incomplete Tasks:**
* All Sprint 5 Tasks Completed
* **Resolution Plan:**
* All Sprint 5 Tasks Completed

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Feature/Part-&-Order-Number-Validation-+-Date-Received-&-Due-Validation] | Unit | Passed | PR #47 |
| [Feature/Part-&-Order-Number-Validation-+-Date-Received-&-Due-Validation] | UI | Passed | PR #47 |
| [Feature/Update-Record_Wireframe---Successful-Entry-State] | Unit | Passed | PR #48 |
| [Feature/Update-Record_Wireframe---Successful-Entry-State] | Integration | Passed | PR #48 |
| [Feature/Update-Record_Wireframe---Successful-Entry-State] | UI | Passed | PR #48 |
| [Feature/Update-Record_Wireframe---Successful-Entry-State] | UI | Passed | PR #48 |
| [Feature/Update-Record_Wireframe---Successful-Entry-State] | System | Passed | PR #48 |
| [Feature/Update-Record/Operations-Quantity-Error-Validation] | Unit | Passed | PR #49 |
| [Feature/Update-Record/Operations-Quantity-Error-Validation] | UI | Passed | PR #49 |
| [Feature/Update-Record-Customer-&-Operator-Validation] | Unit | Passed | PR #50 |
| [Feature/Update-Record-Customer-&-Operator-Validation] | UI | Passed | PR #50 |
## 4. Bug Tracking
* **Bug Description:** [Current Application-Develop branch after merge from PR #48 is experiencing a break in the application due to missing validation for the input fields of Customer, Operator, Operations, and Quantity when "bad data" is used during testing. Once validation has been included for the four input fields, bug will be resolved]
* **Severity:** [High] | **Fix Status:** [Fixed]

# Sprint 6 Technical Log

## 1. Planning & Assignments
* **Sprint Dates:** 2026-04-06 to 2026-04-19
* **Team Name:** Team 2 | **Members Present:** Daniel Puharic, Elian Garica, Justin Kisner, Josh Stanczyk
* **Sprint Goals:** Progress or completion of Sprint 6 goals which include, User Manual Query & Delete/Restore & User Manual Create & Update Records, and User Authentication.

| Task Description | Assigned Owner | Priority | Status |
| :--- | :--- | :--- | :--- |
| Sprint 6: User Manual Query & Delete/Restore | Elian Garcia, Daniel Puharic | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| User Manual Screen Shots & Description: User Queries | Elian Garcia | Review and revise user manual content | Completed |
| User Manual Screen Shots & Description: Delete/Restore | Daniel Puharic | Review and revise user manual content | Completed | 
| Sprint 6: User Manual Create & Update Records | Justin Kisner, Josh Stanczyk | High | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| User Manual Screen Shots & Description: Create Record | Justin Kisner | Review and revise user manual content | Completed |
| User Manual Screen Shots & Description: Update Record | Josh Stanczyk | Review and revise user manual content | Completed |
| Feature Fix: User Authentication with Role Based Access | Josh Stanczyk | Application retreives OS current user name from Environment class. Verifies existence within DB, uses the username to retreive Role ID from DB to grant role based access | Completed |

## 2. Progress & Blockers
* **Completed Work:**
* Feature Fix: User Authentication with Role Based Access.
* Sprint 6: User Manual Query & Delete/Restore
* Sprint 6: User Manual Create & Update Records 
* **Incomplete Tasks:**
* All Sprint 6 Tasks Completed
* **Resolution Plan:**
* Progress through Sprint 6 Tasks related to User Manual Documentation in preparation for User Manual Documentation submission date.
* **Blockers:**
* None at this time, progress will be made continually toward completion of User Manual Documentation in preparation for designated final submission date.

## 3. System Test Report
| Test Case | Type | Result | Evidence/PR # |
| :--- | :--- | :--- | :--- |
| [Feature Fix: User Authentication with Role Based Access] | Unit | Passed | PR #55 |
| [Feature Fix: User Authentication with Role Based Access] | System | Passed | PR #55 |
| [Feature Fix: User Authentication with Role Based Access] | UI | Passed | PR #55 |

## 4. Bug Tracking
* **Bug Description:** [Application crash upon forced close when username was not found in DB, placed validation for username verification in program.cs to resolve]
* **Severity:** [High] | **Fix Status:** [Fixed]
* **Bug Description:** [Add Operations checkboxes for Mill and Lathe to UpdateRecord.cs and CreateRecord.cs]
* **Severity:** [High] | **Fix Status:** [Fixed]
* **Bug Description:** [Missing DAO.cs function and UI elements in Form1.cs and Form1.Designer.cs for Machining Department Query in DB]
* **Severity:** [High] | **Fix Status:** [Fixed]
