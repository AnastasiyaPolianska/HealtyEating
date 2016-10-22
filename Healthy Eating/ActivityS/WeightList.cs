using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Healthy_Eating.Classes;
using SQLiteNetExtensions.Extensions;

namespace Healthy_Eating
{
    [Activity(Label = "Weight List", MainLauncher = false)]
    public class WeightList : Activity
    {
        //Elements from layout.
        ListView ListOfWeight;
        ListView ListOfHeight;
        ListView ListOfBMI;
        ListView ListOfDate;
        TextView IdentifierOfAUser;

        //Lists for user parameters.    
        List<double> dListOfWeight = new List<double>();
        List<double> dListOfHeight = new List<double>();
        List<double> sListOfBMI = new List<double>();
        List<string> sListOfDate = new List<string>();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeightList);

            //Initializing ists for user parameters.
            ListOfWeight = FindViewById<ListView>(Resource.Id.WeightList);
            ListOfHeight = FindViewById<ListView>(Resource.Id.HeightList);
            ListOfBMI = FindViewById<ListView>(Resource.Id.BMIList);
            ListOfDate = FindViewById<ListView>(Resource.Id.DateList);

            IdentifierOfAUser = FindViewById<TextView>(Resource.Id.IdentifierOfAUser);

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.ParametersOfUser) + " " + Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).Name;

                //Getting parameters of currrent user from DB.
                foreach (Classes.ParametresOfUser TempParametres in Classes.WorkWithDatabase.SQConnection.GetWithChildren<Classes.User>(Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).Name).Parameters)
                {
                    dListOfWeight.Add(TempParametres.Weight);
                    dListOfHeight.Add(TempParametres.Height);
                    sListOfBMI.Add(TempParametres.Index);
                    sListOfDate.Add(TempParametres.EntryDate.ToShortDateString());
                }

                //Displaying parameters on the layout.
                var adapterForWeight = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleListItem1, dListOfWeight);
                ListOfWeight.Adapter = adapterForWeight;

                var adapterForHeight = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleListItem1, dListOfHeight);
                ListOfHeight.Adapter = adapterForHeight;

                var adapterForBMI = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleListItem1, sListOfBMI);
                ListOfBMI.Adapter = adapterForBMI;

                var adapterForDate = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, sListOfDate);
                ListOfDate.Adapter = adapterForDate;
            }

            //If the user isn't choosed.
            else
            {
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.Unchoosed);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        protected override void OnStop()
        {
            base.OnStop();
        }

    }

}