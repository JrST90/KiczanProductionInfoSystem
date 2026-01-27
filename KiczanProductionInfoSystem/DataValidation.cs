using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace KiczanProductionInfoSystem
{
    internal class DataValidation
    {
        //Validate part number input.
        internal bool validatePartNumber(string input)
        {
            //Regular expressions to check for invalid input.
            string patternOne = @"([\s])";
            string patternTwo = @"([!@#$%^&*=])";
            string patternThree = @"([a-zA-Z0-9-])";
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
