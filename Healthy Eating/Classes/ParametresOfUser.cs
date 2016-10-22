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
        public DateTime EntryDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Index { get; set; }

        /*Constructors*/
        public ParametresOfUser() { }

        public ParametresOfUser(DateTime incomeEntryDate, double incomeWeight, double incomeHeight)
        {
            EntryDate = incomeEntryDate;
            Weight = incomeWeight;
            Height = incomeHeight;

            Index = Math.Round(Weight / Math.Pow(Height / 100.00, 2.00), 3);
        }

        /*Functions*/
        public override string ToString()
        {
            return String.Format("Date: {0} Weight: {1} Height: {2} Index: {3}", EntryDate.ToString(), Weight, Height, Index);
        }
    }
}