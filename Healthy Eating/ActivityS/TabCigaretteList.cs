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
        AlertDialog DialogForAdding;
        EditText AmountOfNicotine;
        NumberPicker NumberOfCigarettesPicker;
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

            //Making the footer.
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
                //Setting texts.
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

                //For sums.
                ForSumQuantity = Footer.FindViewById<EditText>(Resource.Id.Col1);
                ForSumNicotine = Footer.FindViewById<EditText>(Resource.Id.Col2);

                ForSumQuantity.Text = SumOfQuantity.ToString();
                ForSumNicotine.Text = SumOfNicotine.ToString();

                //If there isn't a footer, adding one.
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
                    ProductLists.CurrentDate = DateChooser.DateTime;
                    OnResume();
                }
                ));

                //Showing the form.
                Object.Show();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        
        //Making a new cigarettes entry.
        private void SetCigarettesButton_Click(object sender, EventArgs e)
        {
            //If the user isn't choosed.
            if (Classes.User.CurrentUser == -1)
                HelpclassDataValidation.MakingErrorToast(Resource.String.ErrorMessage_Unchoosed);

            //If the user is choosed, moving to adding products.
            else
            {
                //Creating a new layout for choosing date.
                AlertDialog.Builder Object_ = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout_ = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View SetAlcoholForm = inflater_.Inflate(Resource.Layout.cigarettes_Add, layout_);
                Object_.SetView(SetAlcoholForm);

                //Picker attributes.
                NumberOfCigarettesPicker = SetAlcoholForm.FindViewById<NumberPicker>(Resource.Id.CigarettesPicker);
                NumberOfCigarettesPicker.MinValue = 1;
                NumberOfCigarettesPicker.MaxValue = 100;

                //Element from the layout.
                AmountOfNicotine = SetAlcoholForm.FindViewById<EditText>(Resource.Id.AmountOfNicotine);

                //On pressing positive button.
                Object_.SetPositiveButton(Resource.String.OK, (EventHandler<DialogClickEventArgs>)null);
                Object_.SetNegativeButton(Resource.String.Cancel, (EventHandler<DialogClickEventArgs>)null);

                //Saving dialog to variable
                DialogForAdding = Object_.Create();
                //Showing a form.
                DialogForAdding.Show();

                //Saving button to variable.
                var positiveButton = DialogForAdding.GetButton((int)DialogButtonType.Positive);
                positiveButton.Click += PositiveButton_Click;

                //Saving button to variable.
                var negativeButton = DialogForAdding.GetButton((int)DialogButtonType.Negative);
                negativeButton.Click += NegativeButton_Click;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //On pressing positive button.
        private void PositiveButton_Click(object sender, EventArgs e)
        {
            //Setting amount of nicotine.
            String ForAmountOfNicotine = AmountOfNicotine.Text;
            ForAmountOfNicotine = ForAmountOfNicotine.Replace(".", ",");

            //If it was error while entering.
            if ((!HelpclassDataValidation.CheckForLenth(ForAmountOfNicotine, 0, 4)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForAmountOfNicotine), 0, 2)))
                HelpclassDataValidation.RequestCorrectEnter(Resource.String.other_Nicotine);

            //If everything is OK.
            else
            {
                //Making a new entry.
                User TempUser = DatabaseUser.GetUser(User.CurrentUser);
                TempUser.Cigarettes.Add(new Cigarette(DateTime.Now, NumberOfCigarettesPicker.Value, double.Parse(ForAmountOfNicotine)));
                DatabaseUser.SQConnection.UpdateWithChildren(TempUser);
                OnResume();
                DialogForAdding.Dismiss();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //On pressing negative button.
        private void NegativeButton_Click(object sender, EventArgs e)
        {
            DialogForAdding.Dismiss();
        }
   }
}