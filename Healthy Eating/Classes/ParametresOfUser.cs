using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Healthy_Eating.Classes
{
  public  class ParametresOfUser
    {
        [PrimaryKey]
        public DateTime dtEntryDate { get; set; }
        public double dWeight { get; set; }
        public double dHeight { get; set; }
        public double dIndex { get; set; }

        public ParametresOfUser() { }

        public ParametresOfUser(DateTime dtIncomeEntryDate, double dIncomeWeight, double dIncomeHeight)
        {
            dtEntryDate = dtIncomeEntryDate;
            dWeight = dIncomeWeight;
            dHeight = dIncomeHeight;

            dIndex = Math.Round(dWeight / Math.Pow(dHeight / 100.00, 2.00), 2);
        }

        public override string ToString()
        {
            return String.Format("Date: {0} Weight: {1} Height: {2} Index: {3}", dtEntryDate.ToString(), dWeight, dHeight, dIndex);
        }
    }
}