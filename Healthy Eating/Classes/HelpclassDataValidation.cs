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
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool CheckForSymbol(string income, char symbol)
        {
            if (income.Contains(symbol)) return true;
            return false;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public static bool CheckForValue(double income, double from, double to)
        {
            if ((income > from) && (income < to)) return true;
            return false;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /*Comparing values*/
        public static bool ComparingValues (double income, double value)
        {
            if (income > value) return true;
            return false;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Request to correct enter.
        public static void RequestCorrectEnter(int id)
        {
            LayoutInflater inflater = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View view = inflater.Inflate(Resource.Layout.message_Error, null);
            var txt = view.FindViewById<TextView>(Resource.Id.ex);
            txt.Text = String.Format(Application.Context.Resources.GetString(Resource.String.ErrorMessage_EnterCorrect) + " {0}", Application.Context.Resources.GetString(id));

            var toast = new Toast(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity)
            {
                Duration = ToastLength.Long,
                View = view
            };
            toast.Show();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Showing errors.
        public static void MakingErrorToast(int id)
        {
            LayoutInflater inflater = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);           
            View view = inflater.Inflate(Resource.Layout.message_Error, null);
            var txt = view.FindViewById<TextView>(Resource.Id.ex);
            txt.Text = Application.Context.Resources.GetString(id);

            var toast = new Toast(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity)
            {
                Duration = ToastLength.Long,
                View = view
            };
            toast.Show();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Showing info.
        public static void MakingInfoToast(int id)
        {
            LayoutInflater inflater = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View view = inflater.Inflate(Resource.Layout.message_Info, null);
            var txt = view.FindViewById<TextView>(Resource.Id.text);
            txt.Text = Application.Context.Resources.GetString(id);

            var toast = new Toast(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity)
            {
                Duration = ToastLength.Long,
                View = view
            };
            toast.Show();
        }
    }
}