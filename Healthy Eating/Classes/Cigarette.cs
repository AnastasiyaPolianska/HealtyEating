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
using SQLite;

namespace Healthy_Eating.Classes
{
    public class Cigarette
    {
        [PrimaryKey]
        public DateTime Date { get; set; }

        public double Nicotine { get; set; }
        public double Quantity { get; set; }

        public Cigarette() { }

        public Cigarette(DateTime date, double quantity, double nicotine)
        {
            Date = date;
            Quantity = quantity;
            Nicotine = quantity*nicotine;         
        }

        public override string ToString()
        {
            return String.Format("Nicotine: {0} Quantity: {1}", Nicotine, Quantity);
        }
    }
}