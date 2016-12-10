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
    public class Alcohol
    {
        [PrimaryKey]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public double CCal { get; set; }
        public double PercentageOfAlchol { get; set; }
        public double Quantity { get; set; }
        public string Visibility { get; set; }

        public Alcohol() { }

        public Alcohol(string name, string visibility, double percentage, double ccal)
        {
            Name = name;
            Visibility = visibility;
            CCal = ccal;
            PercentageOfAlchol = percentage;
        }

        public void Setter(int quantity, DateTime date)
        {
            Date = date;
            Quantity = quantity;
            CCal = Math.Round(Quantity / 100.00 * CCal, 2);
            PercentageOfAlchol = Math.Round((Quantity * (PercentageOfAlchol/100)), 2);
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, Percentage of alcohol: {1} Ccal: {2}", Name, PercentageOfAlchol, CCal);
        }

    }
}