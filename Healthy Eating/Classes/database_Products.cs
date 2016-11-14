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
  public  class database_Products
    {
        static public SQLite.Net.SQLiteConnection SQConnectionProduct =null;

        /*Initializing*/
        public static bool Init(string path)
        {
           bool iscreated = false;
           if(!File.Exists(path))
            {
                File.Create(path);
                iscreated = true;
            }

            SQConnectionProduct = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);
            if (iscreated) { CreateProductTable();}

            return iscreated;
        }

        /*Creating table*/
        static public  void CreateProductTable()
        {
           if(SQConnectionProduct != null)
            {
                SQConnectionProduct.CreateTable<Product>();
            }
        }

        /*Working with products*/
        static public  void InsertProduct(Product income)
        {
            if (SQConnectionProduct != null)
            {
                SQConnectionProduct.Insert(income);
            }
        }

        static public Product GetProduct(string Name)
        {
            return SQConnectionProduct.Get<Product>(Name);
        }        
    }
}