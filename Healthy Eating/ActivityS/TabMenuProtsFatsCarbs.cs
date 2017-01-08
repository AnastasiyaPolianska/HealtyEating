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
using Healthy_Eating.Classes.ProductsRange;

namespace Healthy_Eating.ActivityS
{
    public class TabMenuProtsFatsCarbs : Fragment
    {
        //List for displaying user products for the date.
        List<TableRowMenu> ListForUserProducts;

        //Elements from the layout.
        TextView CurrentDateText;
        ListView MenuList;
        TextView IdentifierOfAUser;
        View List;
        View Footer;
        EditText ForSumProteins;
        EditText ForSumFats;
        EditText ForSumCarbs;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            Footer = inflater_.Inflate(Resource.Layout.helpform_MenuRowFooter, null);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List = inflater.Inflate(Resource.Layout.tab_menu_ProtsFatsCarbs, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            //Elements from the layout.
            Button SetProductsButton = List.FindViewById<Button>(Resource.Id.SetProductsButton);
            Button ChooseDataButton = List.FindViewById<Button>(Resource.Id.ChooseDateButton);
            IdentifierOfAUser = List.FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            CurrentDateText = List.FindViewById<TextView>(Resource.Id.CurrentDateText);
            MenuList = List.FindViewById<ListView>(Resource.Id.MenuList);

            //Allocating memory. Initializing.
            ListForUserProducts = new List<TableRowMenu>();

            //Action on button clicks.
            SetProductsButton.Click += SetProductsButton_Click;
            ChooseDataButton.Click += ChooseDataButton_Click;

            return List;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Updating the layout.
        public override void OnResume()
        {
            base.OnResume();

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                //Variables for sums.
                double SumOfProteins = 0;
                double SumOfFats = 0;
                double SumOfCarbs = 0;

                //Setting the texts.
                CurrentDateText.Text = Resources.GetString(Resource.String.other_Date)+" " + ProductLists.CurrentDate.ToShortDateString();
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_MenuOfUser) + " " + DatabaseUser.GetUser(User.CurrentUser).Name;

                //Clearing the list.
                ListForUserProducts.Clear();

                //Getting products of currrent user for the date from DB.
                foreach (Product TempProduct in DatabaseUser.GetUser(User.CurrentUser).Products)
                {
                    //If the date matches the choosed date.
                    if (TempProduct.Date.ToShortDateString() == ProductLists.CurrentDate.ToShortDateString())
                    {
                        //Making he sums.
                        ListForUserProducts.Add(new TableRowMenu(TempProduct.Name, TempProduct.Protein, TempProduct.Fats, TempProduct.Carbohydrates));
                        SumOfProteins += TempProduct.Protein;
                        SumOfFats += TempProduct.Fats;
                        SumOfCarbs += TempProduct.Carbohydrates;
                    }
                    }

                //Showing in the list.
                HelpclassMenuAdapter AdapterForUserParameters = new HelpclassMenuAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserProducts);
                MenuList.Adapter = AdapterForUserParameters;

                //Working with footer.
                ForSumProteins = Footer.FindViewById<EditText>(Resource.Id.Col1);
                ForSumFats = Footer.FindViewById<EditText>(Resource.Id.Col2);
                ForSumCarbs = Footer.FindViewById<EditText>(Resource.Id.Col3);

                ForSumProteins.Text = SumOfProteins.ToString();
                ForSumFats.Text = SumOfFats.ToString();
                ForSumCarbs.Text = SumOfCarbs.ToString();

                //If there isn't a footer, adding one.
                if (MenuList.FooterViewsCount==0)
                    MenuList.AddFooterView(Footer);
            }

            //If the user is not choosed.
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
                HelpclassDataValidation.MakingErrorToast(Resource.String.ErrorMessage_Unchoosed);

            //If the user is choosed, moving to adding products.
            else
            {
                var intent = new Intent(Activity, typeof(ProductsTypes));
                StartActivity(intent);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing data.
        private void ChooseDataButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                HelpclassDataValidation.MakingErrorToast(Resource.String.ErrorMessage_Unchoosed);

            //If the user is choosed, moving to adding products.
            else
            {
                //Creating a new layout for choosing date.
                AlertDialog.Builder Object = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View ChooseDateForm = inflater.Inflate(Resource.Layout.product_ChooseDate, layout);
                Object.SetView(ChooseDateForm);

                DatePicker DateChooser = ChooseDateForm.FindViewById<DatePicker>(Resource.Id.DateChooser);

                //Action on pressing positive button.
                Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, DateChooser.DateTime.ToShortDateString(), ToastLength.Long).Show();
                    ProductLists.CurrentDate = DateChooser.DateTime;
                    OnResume();
                }
                ));

                //Showing the form.
                Object.Show();
            }
        }
    }
}