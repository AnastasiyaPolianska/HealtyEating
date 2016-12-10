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
using Healthy_Eating.ActivityS;

namespace Healthy_Eating
{
    [Activity(Label = "Cigarettes Consumption", MainLauncher = false)]
    public class PageCigarettes : Activity
    {
        public static Activity activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            activity = this;

            /*Creation of tabs.*/

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.Tab tab = ActionBar.NewTab();

            //Tab for list of cigarettes.
            tab.SetText(Resource.String.tab_ListCigarettes);
            tab.TabSelected += Tab_TabSelectedList;
            ActionBar.AddTab(tab);

            //Tab for weight plot.
            tab = ActionBar.NewTab();
            tab.SetText(Resource.String.tab_CalendarCigarettes);
            tab.TabSelected += Tab_TabSelectedCalendar;
            ActionBar.AddTab(tab);

            //Going to selected tab.
            SetContentView(Resource.Layout.helpform_CigarettesConnector);         
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        protected override void OnStop()
        {
            base.OnStop();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for list of parameters.
        private void Tab_TabSelectedList(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector,new TabCigaretteList());
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for weight plot.
        private void Tab_TabSelectedCalendar(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new TabCigaretteCalendar());
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------
    }
}