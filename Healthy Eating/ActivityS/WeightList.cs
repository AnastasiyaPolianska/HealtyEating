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
        ListView ListOfWeight;
        ListView ListOfHeight;
        ListView ListOfBMI;

        List<double> dListOfWeight = new List<double>();
        List<double> dListOfHeight = new List<double>();
        List<double> dListOfBMI = new List<double>();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            //Встановлення вигляду головної форми.
            SetContentView(Resource.Layout.WeightList);

            //Присвоєння значення змінним, що відповідають за списки на формі.
            ListOfWeight = FindViewById<ListView>(Resource.Id.WeightList);
            ListOfHeight = FindViewById<ListView>(Resource.Id.HeightList);
            ListOfBMI = FindViewById<ListView>(Resource.Id.BMIList);

            //Toast.MakeText(this, Classes.User.CurrentUser.Parameters.ElementAt(0).ToString(), ToastLength.Short).Show();

            foreach (Classes.ParametresOfUser TempParametres in Classes.WorkWithDatabase.SQConnection.GetWithChildren<Classes.User>(Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).sName).Parameters)
            {
                dListOfWeight.Add(TempParametres.dWeight);
                dListOfHeight.Add(TempParametres.dHeight);
                dListOfBMI.Add(TempParametres.dIndex);
            }

            //Адаптер для виведення даних у список.
    
            var adapterForWeight = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleListItem1, dListOfWeight);
            ListOfWeight.Adapter = adapterForWeight;

            var adapterForHeight = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleListItem1, dListOfHeight);
            ListOfHeight.Adapter = adapterForHeight;

            var adapterForBMI = new ArrayAdapter<double>(this, Android.Resource.Layout.SimpleExpandableListItem1, dListOfBMI);
            ListOfBMI.Adapter = adapterForBMI;
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

    }

}