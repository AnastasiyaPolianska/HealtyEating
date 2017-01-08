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
    [Activity(Label = "My Menu", MainLauncher = false)]
    public class PageMyMenu : Activity
    {
        public static Activity activity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            activity = this;

            /*Creation of tabs.*/

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.Tab tab = ActionBar.NewTab();

            //Tab for ProtsFatsCarbs.
            tab.SetText(Resource.String.tab_ProtsFatsCarbs);
            tab.TabSelected += Tab_TabSelectedProtsFatsCarbs;
            ActionBar.AddTab(tab);

            //Tab for WaterCcals.
            tab = ActionBar.NewTab();
            tab.SetText(Resource.String.tab_WaterCcals);
            tab.TabSelected += Tab_TabSelectedWaterCcals;
            ActionBar.AddTab(tab);

            //Going to selected tab.
            SetContentView(Resource.Layout.helpform_ParametersConnector);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for ProtsFatsCarbs.
        private void Tab_TabSelectedProtsFatsCarbs(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new TabMenuProtsFatsCarbs());
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Tab for WaterCcals.
        private void Tab_TabSelectedWaterCcals(object sender, ActionBar.TabEventArgs e)
        {
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.Connector);
            if (fragment != null) e.FragmentTransaction.Remove(fragment);
            e.FragmentTransaction.Add(Resource.Id.Connector, new TabMenuWaterCcals());
        }
    }
}