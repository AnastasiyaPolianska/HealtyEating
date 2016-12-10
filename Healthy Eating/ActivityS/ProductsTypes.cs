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
using Healthy_Eating.Classes.ProductsRange;

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "Choose type of product: ", MainLauncher = false)]
    public class ProductsTypes : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product_Types);
            
            //Elements from the layout.
            ListView TypesOfProductsList = FindViewById<ListView>(Resource.Id.TypesOfProductsList);

            //Creating adapter for the list of product types and setting it on the form.
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ProductLists.GetMajorList());
            TypesOfProductsList.Adapter = adapter;

            //Action on item click.
            TypesOfProductsList.ItemClick += TypesOfProductsList_ItemClick;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Action on item click.
        private void TypesOfProductsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Setting a chooser.
            ProductLists.SetChooser(e.Position);           
           
            //Going to adding products activity.
            StartActivity(typeof(ProductsAdd));
        }
    }
}