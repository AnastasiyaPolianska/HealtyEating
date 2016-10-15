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
        public string sName { get; set; }
        public int nAge { get; set; }
        public ENSex Sex { get; set; }

        [TextBlob("sParameters")]
        public  List<ParametresOfUser> Parameters { get; set; }
        public string sParameters { get; set; }

        [Ignore]
        public static int CurrentUser { get; set; }

        /*Constructors*/
        public User() { Parameters = new List<ParametresOfUser>(); }

        public User(string sIncomeName, int nIncomeAge, ENSex IncomeSex)
        {
            sName = sIncomeName;
            nAge = nIncomeAge;
            Sex = IncomeSex;
            Parameters = new List<ParametresOfUser>();
        }

        /*Functions*/
        public override string ToString()
        {
            return String.Format("Name: {0} Age: {1} Sex: {2}", sName, nAge, Sex);
        }
    }
}