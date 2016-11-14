using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace Healthy_Eating.Classes
{
  public  class database_User
    {
        static public SQLite.Net.SQLiteConnection SQConnection =null;

        /*Initializing*/
        public static void Init(string path)
        {
           bool iscreated = false;
           if(!File.Exists(path))
            {
                File.Create(path);
                iscreated = true;
            }

            SQConnection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);
            if (iscreated) { CreateUserTable(); CreateParametresTable(); }
        }

        /*Creating tables*/
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

        /*Working with user*/
        static public  void InsertUser(User income)
        {
            if (SQConnection != null)
            {
                 SQConnection.Insert(income);
            }
        }

        static public void DeleteUser(string name)
        {
            SQConnection.Delete<User>(name);
        }

        static public User GetUser(int id)
        {
            return SQConnection.GetWithChildren<User>(SQConnection.Table<User>().ElementAt(id).Name);
        }        
    }
}