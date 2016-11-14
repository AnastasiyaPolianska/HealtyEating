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
    [Activity(Label = "My parameters", MainLauncher = false)]
    public class ListOfParameters : Activity
    {
        public static Activity activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            activity = this;

            /*Creation of tabs.*/

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.Tab tab = ActionBar.NewTab();

            //Tab for list of parameters.
            tab.SetText(Resource.String.tab_List);
            tab.TabSelected += Tab_TabSelectedList;
            ActionBar.AddTab(tab);

            //Tab for weight plot.
            tab = ActionBar.NewTab();
            tab.SetText(Resource.String.tab_GraphWeight);
            tab.TabSelected += Tab_TabSelectedWeight;
            ActionBar.AddTab(tab);

            //Tab for height plot.
            tab = ActionBar.NewTab();
            tab.SetText(Resource.String.tab_GraphHeight);
            tab.TabSelected += Tab_TabSelectedHeight;
            ActionBar.AddTab(tab);

            //Going to selected tab.
            SetContentView(Resource.Layout.helpform_ParametersConnector);         
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
            e.FragmentTransaction.Add(Resource.Id.Connector,new tab_products_List());
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for weight plot.
        private void Tab_TabSelectedWeight(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new tab_products_GraphWeight());
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for height plot.
        private void Tab_TabSelectedHeight(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new tab_products_GraphHeight());
        }     
    }
}