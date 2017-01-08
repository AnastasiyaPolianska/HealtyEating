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

using Com.Syncfusion.Charts;
using Com.Syncfusion.Charts.Enums;
using Android.Graphics;

namespace Healthy_Eating.ActivityS
{
    public class TabParametersGraphWeight : Fragment
    {
       public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Element from the layout.
            SfChart Graph = new SfChart(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);         

            //Axes settings.
            CategoryAxis AxeX = new CategoryAxis();
            NumericalAxis AxeY = new NumericalAxis();

            Graph.PrimaryAxis = AxeX;
            Graph.SecondaryAxis = AxeY;

            AxeX.LabelRotationAngle = 315;
            AxeX.LabelStyle.TextColor = Android.Graphics.Color.Black;
            AxeY.LabelStyle.TextColor = Android.Graphics.Color.Black;

            AxeX.EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Shift;

            //Graph settings.
            Graph.SetBackgroundColor(Android.Graphics.Color.DarkGreen);

            ChartZoomPanBehavior Zoom = new ChartZoomPanBehavior();
            Graph.Behaviors.Add(Zoom);

            ObservableArrayList ListForData = new ObservableArrayList();

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                //Getting parameters of currrent user from DB.
                int k = 1;
                foreach (ParametresOfUser TempParametres in DatabaseUser.GetUser(User.CurrentUser).Parameters)
                    ListForData.Add(new ChartDataPoint(TempParametres.EntryDate.ToShortDateString(), TempParametres.Weight));

                //Setting data to the list
                SplineSeries Seria = new SplineSeries();
                Seria.DataSource = ListForData;

                //Settings oof lables.
                Seria.DataMarker.ShowMarker = true;
                Seria.DataMarker.ShowLabel = true;
                Seria.DataMarker.MarkerColor = Android.Graphics.Color.Yellow;
                Seria.DataMarker.ConnectorLineStyle.ConnectorHeight = 50;
                Seria.DataMarker.ConnectorLineStyle.ConnectorRotationAngle = 175;
                Seria.DataMarker.ConnectorLineStyle.PathEffect = new DashPathEffect(new float[] { 3, 3 }, 3);

                //Adding graph to form.
                Graph.Series.Add(Seria);
            }

            //If the user isn't choosed.
            else HelpclassDataValidation.MakingErrorToast(Resource.String.ErrorMessage_Unchoosed);

            return Graph;
        }
    }
}