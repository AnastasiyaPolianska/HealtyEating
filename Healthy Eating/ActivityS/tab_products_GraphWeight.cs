using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Healthy_Eating.Classes;

using MikePhil.Charting.Charts;
using MikePhil.Charting.Components;
using MikePhil.Charting.Data;
using Android.Graphics;

namespace Healthy_Eating.ActivityS
{
    public class tab_products_GraphWeight : Fragment
    {
       public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View TabGraphWeight = inflater.Inflate(Resource.Layout.tab_products_GraphWeight, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            //Element from the layout.
            LineChart Graph = TabGraphWeight.FindViewById<LineChart>(Resource.Id.Graph);
            Graph.SetBackgroundColor(Color.MediumPurple);

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                //Creating lists for data, which is going to be shown.
                List<DateTime> UserDateList = new List<DateTime>();
                List<Entry> EntryList = new List<Entry>();

                //Getting parameters of currrent user from DB.
                int k = 1;
                foreach (ParametresOfUser TempParametres in database_User.GetUser(User.CurrentUser).Parameters)
                {
                    EntryList.Add(new Entry((k++), (float)TempParametres.Weight));
                    UserDateList.Add(TempParametres.EntryDate);
                }

                //Setting data to the graph.
                LineDataSet DataSet = new LineDataSet(EntryList, Resources.GetString(Resource.String.Parameterscolumn_Weight));
                DataSet.SetMode(LineDataSet.Mode.CubicBezier);

                LineData InfoAboutGraph = new LineData(DataSet);

                Graph.Data = InfoAboutGraph;
                Graph.Invalidate();
            }

            //If the user isn't choosed.
            else
            {
                Toast.MakeText(Application.Context, Resource.String.ErrorMessage_Unchoosed, ToastLength.Long).Show();
            }

            return TabGraphWeight;
        }
    }
}