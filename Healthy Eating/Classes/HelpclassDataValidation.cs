using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;

namespace Healthy_Eating.Classes
{
    class HelpclassDataValidation
    {
        /*Checking information*/
        public static bool CheckForLenth(string income, int from, int to)
        {
            if ((income.Length > from) && (income.Length < to)) return true;
            return false;
        }

        public static bool CheckForSymbol(string income, char symbol)
        {
            if (income.Contains(symbol)) return true;
            return false;
        }

        public static bool CheckForValue(double income, double from, double to)
        {
            if ((income > from) && (income < to)) return true;
            return false;
        }
        
        /*Comparing values*/
        public static bool ComparingValues (double income, double value)
        {
            if (income > value) return true;
            return false;
        }
        
        /*Showing information*/
        public static string RequestToCorrectEnter(string type)
        {
            return String.Format(Application.Context.Resources.GetString(Resource.String.ErrorMessage_EnterCorrect)+ " {0}", type);
        }
       
    }
}