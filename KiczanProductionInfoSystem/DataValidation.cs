using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace KiczanProductionInfoSystem
{
    internal class DataValidation
    {
        //Validate date range input.
        internal bool validateDateRange(string input)
        {
            //Regular expression to check format with included hyphen.
            string pattern = @"\d{2}\/\d{2}\/\d{4}\-\d{2}\/\d{2}\/\d{4}";
            Regex rg = new Regex(pattern);
            bool matchesPattern = false;
            bool isValidOne = false;
            bool isValidTwo = false;
            bool dateValid = false;

            if (rg.IsMatch(input))
            {
                matchesPattern = true;
            }
            else
            {
                matchesPattern = false;
            }

            //Check to make sure provided dates are logically sound.
            if (matchesPattern)
            {
                string format = "MM/dd/yyyy";
                string[] dateArray = input.Split('-');
                DateTime dtOne;
                DateTime dtTwo;
                isValidOne = DateTime.TryParseExact(dateArray[0], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtOne);
                isValidTwo = DateTime.TryParseExact(dateArray[1], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTwo);
            }

            if(isValidOne && isValidTwo)
            {
                dateValid = true;
            }
            else
            {
                dateValid = false;
            }
            return dateValid;
        }

        //Validate date range, ensuring beginning date occurs before end date.
        internal bool validateDateRangeBegBeforeEnd(string input)
        {
            bool dateValid = false;
            string format = "MM/dd/yyyy";
            string[] dateArray = input.Split('-');
            DateTime dtOne;
            DateTime dtTwo;

            DateTime.TryParseExact(dateArray[0], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtOne);
            DateTime.TryParseExact(dateArray[1], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTwo);

            if(dtOne < dtTwo)
            {
                dateValid = true;
            }
            else
            {
                dateValid = false;
            }

            return dateValid;
        }
        
        //Validate part number input.
        internal bool validatePartNumber(string input)
        {
            //Regular expressions to check for invalid input.
            string patternOne = @"([\s])";
            string patternTwo = @"([!@#$%^&*=])";
            string patternThree = @"([a-zA-Z0-9])";
            Regex rgOne = new Regex(patternOne);
            Regex rgTwo = new Regex(patternTwo);
            Regex rgThree = new Regex(patternThree);
            bool flag;

            //Check for whitespace and symbols, return false if exists.
            if (rgOne.IsMatch(input) || rgTwo.IsMatch(input))
            {
                flag = false;
            }
            //If whitespace and symbols do not exist, check for letters, numbers, and hyphen, return true if they exist.
            else if (rgThree.IsMatch(input))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        //Validate quantity input.
        internal string validateQuantity(string input)
        {
            string message = "";
            int quantity;


            if (string.IsNullOrEmpty(input))
            {
                message = "Record not complete!\nQuantity must not be empty,\nplease enter a value.";
            }
            else if (int.TryParse(input, out quantity) == false)
            {
                message = "Record not complete!\nQuantity must be\na numerical value.";
            }
            else if (quantity <= 0)
            {
                message = "Record not complete!\nQuantity must be an integer \ngreater than or equal to 1.";
            }
            return message;
        }

        // Validate purchase order number input.
        internal string validatePurchaseOrderNumber(string poNumber)
        {
            string message = "";
            poNumber = poNumber.Trim();

            if (string.IsNullOrWhiteSpace(poNumber))
                message = "Record not complete!\nPurchase Order Number \nis required.";

            else if (poNumber.Length < 4 || poNumber.Length > 20)
                message = "Record not complete!\nPurchase Order Number must be between \n4 and 20 characters.";

            else if (!Regex.IsMatch(poNumber, @"^[a-zA-Z0-9\-]+$"))
                message = "Record not complete!\nPurchase Order Number can only contain \nletters, numbers, and hyphens.";

            return message;
        }

        // Validate operator selection input.
        internal string validateOperatorSelection(int input)
        {
            string message  = "";

            if(input == 0)
            {
                message = "Record not complete!\nPlease select an Operator.";
            }

            return message;
        }

        // Validate Operations selection input.
        internal string validateOperations(string input)
        {
            string message = "";

            if (string.IsNullOrWhiteSpace(input))
            {
                message = "Record not complete!\nPlease select at least one Operation.";
            }

            return message;
        }

        // Validate date received input.
        internal string validateDateReceived(string input)
        {
            string message = "";
            DateTime parsedDate;

            if (string.IsNullOrWhiteSpace(input))
            {
                message = "Record not complete!\nDate must not be empty,\nplease enter a value.";
            }
            else if (!DateTime.TryParseExact(
                input,
                "MM/dd/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDate))
            {
                message = "Record not complete!\nDate must be in MM/DD/YYYY format.";
            }

            return message;
        }

        // Validate Due Date input.
        internal string validateDueDate(string dueDateText)
        {
            string message = "";
            DateTime dueDate;

            if (string.IsNullOrWhiteSpace(dueDateText))
            {
                message = "Record not complete!\nDate must not be empty,\nplease enter a value.";
            }
            else if (!DateTime.TryParseExact(
                dueDateText,
                "MM/dd/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dueDate))
            {
                message = "Record not complete!\nDate must be in MM/DD/YYYY format.";
            }
            return message;
        }

        // Validate customer selection input.
        internal string validateCustomerSelection(int input)
        {
            string message = "";

            if (input == 0)
            {
                message = "Record not complete!\nPlease select a Customer.";
            }

            return message;
        }
    }
}
