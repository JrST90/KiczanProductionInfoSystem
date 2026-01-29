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
    }
}
