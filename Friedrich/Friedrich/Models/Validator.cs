using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class Validator
    {
        public static String CheckPassword(String pass)
        {
            if (pass.Length < 3)
            {
                return "Too short password, there must be 3 and more characters";
            }
            return "";
        }
        public static String CheckNumber(String num)
        {
            if(CheckInt(num)=="")
            {
                if (num.Length > 6)
                {
                    return "";
                }
                else
                {
                    return "Too short number";
                }
            }
            else
            {
                return CheckInt(num);
            }
        }
        public static String CheckInt(String year)
        {
            int digit;
            bool isNumerical = int.TryParse(year, out digit);
            if(isNumerical)
            {
                if(digit>=0)
                    return "";
                else
                {
                    return "There must be positive numeric";
                }
            }
            
            return "There must be numeric";

        }
    }
}