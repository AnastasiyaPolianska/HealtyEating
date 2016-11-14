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
using System.Reflection;
using SQLiteNetExtensions.Extensions;
using Healthy_Eating.Classes;
using Healthy_Eating.Classes.ProductsRange;

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "Choose product: ", MainLauncher = false)]
    public class products_Add : Activity
    {
        //Initializing a list of products.
        List<string> ListForProducts;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product_Add);

            //Main list of types of products from layout, used for choosing products.
            ListView ListForChoosingProducts = FindViewById<ListView>(Resource.Id.ListForChoosingProducts);
            ListForProducts = new List<string>();

            //Setting products to the list.
            foreach (Product TempProduct in database_Products.SQConnectionProduct.Table<Product>())
            {
                //If tthe type of product matches the chosen type, we add it to the list.
                if (TempProduct.Type == ProductLists.Chooser)
                    ListForProducts.Add(TempProduct.Name);
            }

            //Adapter for the main list.
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListForProducts);
            ListForChoosingProducts.Adapter = adapter;

            //Actions on item clicks.     
            ListForChoosingProducts.ItemLongClick += ListForChoosingProducts_ItemLongClick;
            ListForChoosingProducts.ItemClick += ListForChoosingProducts_ItemClick;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Showing product information.
        private void ListForChoosingProducts_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //Creating a new layout for showing information about product.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View product_Info = inflater.Inflate(Resource.Layout.product_Info, layout);
            Object.SetView(product_Info);

            //Elements from the layout.
            TextView NameText = product_Info.FindViewById<TextView>(Resource.Id.NameText);
            TextView ProteinsText = product_Info.FindViewById<TextView>(Resource.Id.ProteinsText);
            TextView FatsText = product_Info.FindViewById<TextView>(Resource.Id.FatsText);
            TextView CarbsText = product_Info.FindViewById<TextView>(Resource.Id.CarbsText);
            TextView WaterText = product_Info.FindViewById<TextView>(Resource.Id.WaterText);
            TextView CcalsText = product_Info.FindViewById<TextView>(Resource.Id.CcalsText);

            //Temporary product for getting information about choosed product.
            Product TempProduct = database_Products.GetProduct(ListForProducts.ElementAt(e.Position));

            //Showing information.
            NameText.Text = TempProduct.Name;
            ProteinsText.Text = TempProduct.Protein.ToString();
            FatsText.Text = TempProduct.Fats.ToString();
            CarbsText.Text = TempProduct.Carbohydrates.ToString();
            WaterText.Text = TempProduct.Water.ToString();
            CcalsText.Text = TempProduct.Ccal.ToString();

            //Action on positive button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) {}));

            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Adding a new product.
        private void ListForChoosingProducts_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Creating a new layout for entering a number of grams.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View product_Quantity = inflater.Inflate(Resource.Layout.product_Quantity, layout);
            Object.SetView(product_Quantity);

            //Elements from the layout.
            EditText AmountText = product_Quantity.FindViewById<EditText>(Resource.Id.AmountText);

            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If the number is entered correctly.
                if ((helpclass_DataValidation.CheckForLenth(AmountText.Text, 0, 6) && (helpclass_DataValidation.CheckForValue(int.Parse(AmountText.Text), 0, 10000))))
                {
                    //Creating a new product and adding it to the user's list of products.
                    Product TempProduct = database_Products.GetProduct(ListForProducts.ElementAt(e.Position));
                    TempProduct.Setter(int.Parse(AmountText.Text), DateTime.Now);

                    User TempUser = database_User.GetUser(User.CurrentUser);
                    TempUser.Products.Add(TempProduct);
                    database_User.SQConnection.UpdateWithChildren(TempUser);
                    
                    //Showing information about the hew product added.
                    Toast.MakeText(this, database_User.GetUser(User.CurrentUser).Products.Last().ToString(), ToastLength.Long).Show();
                }

                //If the number isn't entered correctly.
                else
                {
                    //Showing the error message.
                    Toast.MakeText(this, helpclass_DataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Amount)), ToastLength.Long).Show();
                }
            }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1){}));

            Object.Show();     
        }
    }
}