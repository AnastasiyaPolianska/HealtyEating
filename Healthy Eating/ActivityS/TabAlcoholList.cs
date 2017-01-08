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
using Healthy_Eating.Classes.ProductsRange;
using Healthy_Eating.Classes;

namespace Healthy_Eating.ActivityS
{
    public class TabAlcoholList : Fragment
    {
        //List for displaying user alcohol drinks for the date.
        List<TableRowAlcohol> ListForUserAlcohols;

        //Elements from the layout.
        TextView CurrentDateText;
        ListView AlcoholList;
        TextView IdentifierOfAUser;
        View List;
        View Footer;
        EditText ForSumPercentage;
        EditText ForSumCcals;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Making the footer.
            LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            Footer = inflater_.Inflate(Resource.Layout.helpform_AlcoholRowFooter, null);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List = inflater.Inflate(Resource.Layout.tab_alcohol_List, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            //Elements from the layout.
            Button SetAlcoholButton = List.FindViewById<Button>(Resource.Id.SetAlcoholButton);
            Button ChooseDataButton = List.FindViewById<Button>(Resource.Id.ChooseDateButton);
            IdentifierOfAUser = List.FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            CurrentDateText = List.FindViewById<TextView>(Resource.Id.CurrentDateText);
            AlcoholList = List.FindViewById<ListView>(Resource.Id.AlcoholList);

            //Allocating memory. Initializing.
            ListForUserAlcohols = new List<TableRowAlcohol>();

            //Action on button clicks.
            SetAlcoholButton.Click += SetAlcoholButton_Click;
            ChooseDataButton.Click += ChooseDataButton_Click;

            return List;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Updating the layout.
        public override void OnResume()
        {
            base.OnResume();

            double SumOfPercentage = 0;
            double SumOfCcals = 0;

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                //Setting texts.
                CurrentDateText.Text = Resources.GetString(Resource.String.other_Date) + " " + ProductLists.CurrentDate.ToShortDateString();
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_MenuOfUser) + " " + DatabaseUser.GetUser(User.CurrentUser).Name;

                //Clearing the list.
                ListForUserAlcohols.Clear();

                //Getting products of currrent user for the date from DB.
                foreach (Alcohol TempAlcohol in DatabaseUser.GetUser(User.CurrentUser).Alcohols)
                {
                    //If the date matches the choosed date.
                    if (TempAlcohol.Date.ToShortDateString() == ProductLists.CurrentDate.ToShortDateString())
                    {
                        ListForUserAlcohols.Add(new TableRowAlcohol(TempAlcohol.Name, TempAlcohol.PercentageOfAlchol, TempAlcohol.CCal));
                        SumOfPercentage += TempAlcohol.PercentageOfAlchol;
                        SumOfCcals += TempAlcohol.CCal;
                    }
                }

                //Showing in the list.
                HelpclassAlcoholAdapter AdapterForUserAlcohols = new HelpclassAlcoholAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserAlcohols);
                AlcoholList.Adapter = AdapterForUserAlcohols;

                //For sums.
                ForSumPercentage = Footer.FindViewById<EditText>(Resource.Id.Col1);
                ForSumCcals = Footer.FindViewById<EditText>(Resource.Id.Col2);

                ForSumPercentage.Text = SumOfPercentage.ToString();
                ForSumCcals.Text = SumOfCcals.ToString();

                //If there isn't a footer, adding one.
                if (AlcoholList.FooterViewsCount == 0)
                    AlcoholList.AddFooterView(Footer);
            }

            //If the user is not choosed.
            else
            {
                CurrentDateText.Text = "";
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.ErrorMessage_Unchoosed);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing the data.
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
                    ProductLists.CurrentDate = DateChooser.DateTime;
                    OnResume();
                }
                ));

                //Showing the form.
                Object.Show();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Making a new alcohol entry.
        private void SetAlcoholButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                HelpclassDataValidation.MakingErrorToast(Resource.String.ErrorMessage_Unchoosed);

            //If the user is choosed, moving to adding products.
            else
            {
                var intent = new Intent(Activity, typeof(AlcoholAdd));
                StartActivity(intent);
            }
        }
    }
}