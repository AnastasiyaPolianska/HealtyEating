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
  public  class DatabaseAlcohol
    {
        static public SQLite.Net.SQLiteConnection SQConnectionAlcohol = null;

        /*Initializing*/
        public static bool Init(string path)
        {
           bool iscreated = false;
          if(!File.Exists(path))
            {
                File.Create(path);
                iscreated = true;
            }

            SQConnectionAlcohol = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);
            if (iscreated) { CreateAlcoholTable();}

            return iscreated;
        }

        /*Creating table*/
        static public  void CreateAlcoholTable()
        {
           if(SQConnectionAlcohol != null)
            {
                SQConnectionAlcohol.CreateTable<Alcohol>();
            }
        }

        /*Working with products*/
        static public  void InsertAlcohol(Alcohol income)
        {
            if (SQConnectionAlcohol != null)
            {
                SQConnectionAlcohol.Insert(income);
            }
        }

        static public Alcohol GetAlcohol(string Name)
        {
            return SQConnectionAlcohol.Get<Alcohol>(Name);
        }        
    }
}