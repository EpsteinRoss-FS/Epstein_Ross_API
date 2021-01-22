using System;
using System.Collections.Generic;
using System.Text;

namespace Epstein_Ross_API
{
    class Validation
    {

        //validate that string provided is > 1
        public static bool ValidateString(string validateString)
        {
            bool stringValid = validateString.Length >= 1 ? true : false;
            return stringValid;
        }

        //verify that passed string is an int
        public static bool CheckInt(string intCheck)
        {
            bool isItInt = int.TryParse(intCheck, out _);
            return (isItInt);
        }
        public static bool CheckDecimal(string decCheck)
        {
            bool isItDec = decimal.TryParse(decCheck, out _);
            return (isItDec);
        }

        //verify that the provided int is in range
        public static bool CheckRange(int num, int maxNum)
        {
            bool isInRange = (num >= 0 && num <= maxNum);
            return (isInRange);
        }
    }
}
