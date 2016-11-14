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
    public class Product
    {
        [PrimaryKey]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }

        public double Water { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public double Ccal { get; set; }

        public Product() { }

        public Product(string name, string type, double water, double protein, double fats, double carbohydrates, double ccal)
        {
            Name = name;
            Type = type;

            Water = water;
            Protein = protein;
            Fats = fats;
            Carbohydrates = carbohydrates;       
            Ccal = ccal;
        }

        public void Setter(double incomeQuantity, DateTime incomeDate)
        {
            Date = incomeDate;
            Quantity = incomeQuantity;

            Protein = Math.Round(Quantity / 100.00 * Protein, 2);
            Fats = Math.Round(Quantity / 100.00 * Fats, 2);
            Carbohydrates = Math.Round(Quantity / 100 * Carbohydrates, 2);
            Water = Math.Round(Quantity / 100.00 * Water, 2);
            Ccal = Math.Round(Quantity / 100.00 * Ccal, 2);
        }

        public override string ToString()
        {
            return String.Format("Name: {0} Protein: {1} Fats: {2} Carbohydrates: {3}  Water: {4} Ccal: {5} Quantity: {6}", Name, Protein, Fats, Carbohydrates, Water, Ccal, Quantity);
        }

    }
}