using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using SQLite.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace Healthy_Eating.Classes
{
  public  class WorkWithDatabase
    {
        static public SQLite.Net.SQLiteConnection SQConnection =null;

        public static void Init(string sPath)
        {
            bool iscreated = false;
            if(!File.Exists(sPath))
            {
                File.Create(sPath);
                iscreated = true;
            }

            SQConnection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),sPath);
            if (iscreated) { CreateUserTable(); CreateParametresTable(); }
        }

        static public  void CreateUserTable ()
        {
           if(SQConnection!=null)
            {
                SQConnection.CreateTable<User>();
            }
        }

        static public void CreateParametresTable()
        {
            if (SQConnection != null)
            {
                SQConnection.CreateTable<ParametresOfUser>();
            }
        }

        static public  void InsertUser(User Income)
        {
            if (SQConnection != null)
            {
                 SQConnection.Insert(Income);
            }
        }
    }
}