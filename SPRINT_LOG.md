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
| Operator Name Function (DAO.cs) | Elian Garcia | Boolean returns true in test enviromoent | Passed | 
| Operator Query Function (DAO.cs) | Elian Garcia | Boolean returns true in test enviromoent | Passed |
| Operator Query Count Function (DAO.cs) | Elian Garcia | Boolean returns true in test enviromoent | Passed |
| Operator Query Event (Form1.cs) | Josh Stancyzk | Page was popualted to form1.cs | Passed |
| Operator Selection on ComboBox1 Index Change (Form1.cs) | Josh Stancyzk | In test enviroment was able to return the corrent selected Index | Passed |
| Operator Selection on ComboBox2 (Form1.cs) | Josh Stancyzk | Test popuplates combobox2 with a list and filters operator datagrid object query event | Passed |
| Operatory Query Pagination (Form1.cs) | Josh Stancyzk | Test to page through forard and reverse encounted no errors | Passed |
| Sprint 2: Main User Interface Wireframe – Search by Fabrication Department – Query Success State | Daniel Puharic, Justin Kisner | Medium | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Fabrication Query (DAO.cs) | Daniel Puharic | Boolean returns true in test enviromoent | Passed | 
| Fabrication Query Count (DAO.cs) | Daniel Puharic | Boolean returns true in test enviromoent | Passed | 
| Fabrication Query Event (Form1.cs) | Daniel Puharic | Page was popualted to form1.cs | Passed |
| Fabrication Selection on ComboBox1 Index Change (Form1.cs) | Justin | In test enviroment was able to return the correct selected Index | Passed |
| Fabrication Query Pagination (Form1.cs) | Justin Kisner | Test to page through forward and reverse encountered no errors | Passed |
| Sprint 2: Create Record Interface Wireframe – Initial Empty State | Daniel Puharic, Josh Stanczyk,  | Medium | Completed |
| Sub Task |  Assigned Owner | Test Procedure | Status |
| Create the CreateRecord class form (CreateRecord.cs) | Daniel Puharic | test if the blank form appear to have the correct labels | Passed |
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
