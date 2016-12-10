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
using SQLiteNetExtensions.Extensions;

namespace Healthy_Eating.ActivityS
{
    public class TabCigaretteList : Fragment
    {
        //List for displaying user alcohol drinks for the date.
        List<TableRowCigarettes> ListForUserCigarettes;

        //Elements from the layout.
        TextView CurrentDateText;
        ListView CigarettesList;
        TextView IdentifierOfAUser;
        View List;
        View Footer;

        EditText ForSumQuantity;
        EditText ForSumNicotine;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            Footer = inflater_.Inflate(Resource.Layout.helpform_CigarettesRowFooter, null);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List = inflater.Inflate(Resource.Layout.tab_cigarette_List, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            //Elements from the layout.
            Button SetCigarettesButton = List.FindViewById<Button>(Resource.Id.SetCigarettesButton);
            Button ChooseDataButton = List.FindViewById<Button>(Resource.Id.ChooseDateButton);
            IdentifierOfAUser = List.FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            CurrentDateText = List.FindViewById<TextView>(Resource.Id.CurrentDateText);
            CigarettesList = List.FindViewById<ListView>(Resource.Id.CigarettesList);

            //Allocating memory. Initializing.
            ListForUserCigarettes = new List<TableRowCigarettes>();

            //Action on button clicks.

            SetCigarettesButton.Click += SetCigarettesButton_Click;
            ChooseDataButton.Click += ChooseDataButton_Click;

            return List;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Updating the layout.
        public override void OnResume()
        {
            base.OnResume();

            double SumOfQuantity = 0;
            double SumOfNicotine = 0;

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                CurrentDateText.Text = Resources.GetString(Resource.String.other_Date) + " " + ProductLists.CurrentDate.ToShortDateString();
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_MenuOfUser) + " " + DatabaseUser.GetUser(User.CurrentUser).Name;

                //Clearing the list.
                ListForUserCigarettes.Clear();

                //Getting products of currrent user for the date from DB.
                foreach (Cigarette TempCigarette in DatabaseUser.GetUser(User.CurrentUser).Cigarettes)
                {
                    //If the date matches the choosed date.
                    if (TempCigarette.Date.ToShortDateString() == ProductLists.CurrentDate.ToShortDateString())
                    {
                        ListForUserCigarettes.Add(new TableRowCigarettes(TempCigarette.Quantity, TempCigarette.Nicotine));
                        SumOfQuantity += TempCigarette.Quantity;
                        SumOfNicotine += TempCigarette.Nicotine;
                    }
                }

                //Showing in the list.
                HelpclassCigarettesAdapter AdapterForUserCigarettes = new HelpclassCigarettesAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserCigarettes);
                CigarettesList.Adapter = AdapterForUserCigarettes;

                ForSumQuantity = Footer.FindViewById<EditText>(Resource.Id.Col1);
                ForSumNicotine = Footer.FindViewById<EditText>(Resource.Id.Col2);

                ForSumQuantity.Text = SumOfQuantity.ToString();
                ForSumNicotine.Text = SumOfNicotine.ToString();

                if (CigarettesList.FooterViewsCount == 0)
                    CigarettesList.AddFooterView(Footer);
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
                Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();

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

                Object.Show();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        private void SetCigarettesButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();

            //If the user is choosed, moving to adding products.
            else
            {
                //Creating a new layout for choosing date.
                AlertDialog.Builder Object_ = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout_ = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View SetAlcoholForm = inflater_.Inflate(Resource.Layout.cigarettes_Add, layout_);
                Object_.SetView(SetAlcoholForm);

                NumberPicker NumberOfCigarettesPicker = SetAlcoholForm.FindViewById<NumberPicker>(Resource.Id.CigarettesPicker);
                NumberOfCigarettesPicker.MinValue = 1;
                NumberOfCigarettesPicker.MaxValue = 100;

                EditText AmountOfNicotine = SetAlcoholForm.FindViewById<EditText>(Resource.Id.AmountOfNicotine);          

                Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    String ForAmountOfNicotine = AmountOfNicotine.Text;
                    ForAmountOfNicotine = ForAmountOfNicotine.Replace(".", ",");

                    if ((!HelpclassDataValidation.CheckForLenth(ForAmountOfNicotine, 0, 4)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForAmountOfNicotine), 0, 2)))
                    Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Nicotine)), ToastLength.Long).Show();

                    else
                    {
                        User TempUser = DatabaseUser.GetUser(User.CurrentUser);
                        TempUser.Cigarettes.Add(new Cigarette(DateTime.Now, NumberOfCigarettesPicker.Value, double.Parse(ForAmountOfNicotine)));
                        DatabaseUser.SQConnection.UpdateWithChildren(TempUser);
                        OnResume();
                    }
                }));

                Object_.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

                Object_.Show();
            }
        }
    }
}