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

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "My Menu", MainLauncher = false)]
    public class page_MyMenu : Activity
    {
        //List for displaying user products for the date.
        List<tablerow_Menu> ListForUserProducts;
        List<tablerow_MenuCcals> ListForUserProductsCcals;

        //Elements from the layout.
        TextView CurrentDateText;
        ListView MenuList;
        ToggleButton Mode;
        TextView IdentifierOfAUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product_MyMenu);

            //Elements from the layout.
            Button SetProductsButton = FindViewById<Button>(Resource.Id.SetProductsButton);
            Button ChooseDataButton = FindViewById<Button>(Resource.Id.ChooseDateButton);
            IdentifierOfAUser = FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            CurrentDateText = FindViewById<TextView>(Resource.Id.CurrentDateText);
            MenuList = FindViewById<ListView>(Resource.Id.MenuList);
            Mode = FindViewById<ToggleButton>(Resource.Id.Mode);

            //Allocating memory. Initializing.
            ListForUserProducts = new List<tablerow_Menu>();
            ListForUserProductsCcals = new List<tablerow_MenuCcals>();

            //Current date by defolt.
            ProductLists.CurrentDate = DateTime.Now;

            //Action on button clicks.
            SetProductsButton.Click += SetProductsButton_Click;
            ChooseDataButton.Click += ChooseDataButton_Click;
            Mode.CheckedChange += Mode_CheckedChange; 
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Updating the layout.
        protected override void OnResume()
        {
            base.OnResume();

            //Elements from the layout.
            TextView Column1 = FindViewById<TextView>(Resource.Id.Column1);
            TextView Column2 = FindViewById<TextView>(Resource.Id.Column2);
            TextView Column3 = FindViewById<TextView>(Resource.Id.Column3);

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                CurrentDateText.Text = "Date: " + ProductLists.CurrentDate.ToShortDateString();
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_MenuOfUser) + " " + database_User.GetUser(User.CurrentUser).Name;

                //Changing the mode.
                switch (Mode.Checked)
                {
                    case (false):
                        {
                            //Change of captions.
                            Column1.Text = Resources.GetString(Resource.String.Productcolumn_Proteins);
                            Column2.Text = Resources.GetString(Resource.String.Productcolumn_Fats);
                            Column3.Text = Resources.GetString(Resource.String.Productcolumn_Carbs);

                            //Clearing the list.
                            ListForUserProducts.Clear();

                            //Getting products of currrent user for the date from DB.
                            foreach (Product TempProduct in database_User.GetUser(User.CurrentUser).Products)
                            {
                                //If the date matches the choosed date.
                                if (TempProduct.Date.ToShortDateString() == ProductLists.CurrentDate.ToShortDateString())
                                    ListForUserProducts.Add(new tablerow_Menu(TempProduct.Name, TempProduct.Protein, TempProduct.Fats, TempProduct.Carbohydrates));
                            }

                            //Showing in the list.
                            helpclass_MenuAdapter AdapterForUserParameters = new helpclass_MenuAdapter(this, ListForUserProducts);
                            MenuList.Adapter = AdapterForUserParameters;
                        }

                        break;

                    case (true):
                        {
                            //Change of captions.
                            Column1.Text = Resources.GetString(Resource.String.Productcolumn_Quantity);
                            Column2.Text = Resources.GetString(Resource.String.Productcolumn_Water);
                            Column3.Text = Resources.GetString(Resource.String.Productcolumn_Ccals);

                            ListForUserProductsCcals.Clear();
                            //Getting products of currrent user for the date from DB.
                            foreach (Product TempProduct in database_User.GetUser(User.CurrentUser).Products)
                            {
                                //If the date matches the choosed date.
                                if (TempProduct.Date.ToShortDateString() == ProductLists.CurrentDate.ToShortDateString())
                                    ListForUserProductsCcals.Add(new tablerow_MenuCcals(TempProduct.Name, TempProduct.Quantity, TempProduct.Water, TempProduct.Ccal));
                            }

                            //Showing in the list.
                            helpclass_MenuCcalsAdapter AdapterForUserParametersCcals = new helpclass_MenuCcalsAdapter(this, ListForUserProductsCcals);
                            MenuList.Adapter = AdapterForUserParametersCcals;
                        }

                        break;
                }
            }
            
            //If the user isn't choosed.
            else
            {
                CurrentDateText.Text = "";
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.ErrorMessage_Unchoosed);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

            //Setting products.
        private void SetProductsButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();

            //If the user is choosed, moving to adding products.
            else StartActivity(typeof(products_Types));
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing data.
        private void ChooseDataButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();

            //If the user is choosed, moving to adding products.
            else {
                //Creating a new layout for choosing date.
                AlertDialog.Builder Object = new AlertDialog.Builder(this);
                LayoutInflater inflater = LayoutInflater.From(this);
                LinearLayout layout = new LinearLayout(this);
                View ChooseDateForm = inflater.Inflate(Resource.Layout.product_ChooseDate, layout);
                Object.SetView(ChooseDateForm);

                DatePicker DateChooser = ChooseDateForm.FindViewById<DatePicker>(Resource.Id.DateChooser);


                //Action on pressing positive button.
                Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    Toast.MakeText(this, DateChooser.DateTime.ToShortDateString(), ToastLength.Long).Show();
                    ProductLists.CurrentDate = DateChooser.DateTime;
                    OnResume();
                }
                ));

                Object.Show();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Changing the mode.
        private void Mode_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            OnResume();
        }
    }
}