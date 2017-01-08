using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Healthy_Eating.Classes;
using Healthy_Eating.Classes.ProductsRange;
using Healthy_Eating.ActivityS;

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "Alcohol Consumption", MainLauncher = false)]
    public class PageAlcohol : Activity
    {
        public static Activity activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            activity = this;

            /*Creation of tabs.*/

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.Tab tab = ActionBar.NewTab();

            //Tab for ñcals.
            tab.SetText(Resource.String.tab_ListAlcohol);
            tab.TabSelected += Tab_TabSelectedListAlcohol;
            ActionBar.AddTab(tab);

            //Tab for alcohol calendar.
            tab = ActionBar.NewTab();
            tab.SetText(Resource.String.tab_CalendarAlcohol);
            tab.TabSelected += Tab_TabSelectedCalendarAlcohol;
            ActionBar.AddTab(tab);

            //Going to selected tab.
            SetContentView(Resource.Layout.helpform_ParametersConnector);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for ccals.
        private void Tab_TabSelectedListAlcohol(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new TabAlcoholList());
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for alcohol calendar.
        private void Tab_TabSelectedCalendarAlcohol(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new TabAlcoholCalendar());
        }
    }
}