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
  public  enum ENSex { Male, Female};

  public  class User
    {
        [PrimaryKey]
        public string Name { get; set; }
        public int Age { get; set; }
        public ENSex Sex { get; set; }

        [TextBlob("ParametersString")]
        public  List<ParametresOfUser> Parameters { get; set; }
        public string ParametersString { get; set; }

        [TextBlob("MenuString")]
        public List<Product> Products { get; set; }
        public string MenuString { get; set; }

        [Ignore]
        public static int CurrentUser { get; set; }

        /*Constructors*/
        public User()
        {
            Parameters = new List<ParametresOfUser>();
            Products = new List<Product>();
        }

        public User(string incomeName, int incomeAge, ENSex incomeSex)
        {
            Name = incomeName;
            Age = incomeAge;
            Sex = incomeSex;
            Parameters = new List<ParametresOfUser>();
            Products = new List<Product>();
        }

        /*Functions*/
        public override string ToString()
        {
            return String.Format("Name: {0} Age: {1} Sex: {2}", Name, Age, Sex);
        }
    }
}