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
using SQLiteNetExtensions.Extensions;

namespace Healthy_Eating.ActivityS
{
    public class TabParametersList : Fragment
    {
        //Elements from layout.
        ListView ListForParameters;
        TextView IdentifierOfAUser;
        Button SetParametersButton;
        View Footer;

        EditText WeightText, WeightText1;
        EditText HeightText, HeightText1;
        EditText BMIText, BMIText1;

        double maxWeight = 0, maxHeight = 0;
        double minWeight = -1, minHeight = -1;

        //List for user parameters.    
        List<TableRowParameters> ListForUserParameters = new List<TableRowParameters>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LayoutInflater inflater_ = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            Footer = inflater_.Inflate(Resource.Layout.helpform_ParametersRowFooter, null);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View List = inflater.Inflate(Resource.Layout.tab_parameters_List, null);
            base.OnCreateView(inflater, container, savedInstanceState);
            
            //Initializing elements from the layout.
            IdentifierOfAUser = List.FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            SetParametersButton = List.FindViewById<Button>(Resource.Id.SetParametersButton);
            ListForParameters = List.FindViewById<ListView>(Resource.Id.ListForParameters);

            WeightText = Footer.FindViewById<EditText>(Resource.Id.WeightText);
            WeightText1 = Footer.FindViewById<EditText>(Resource.Id.WeightText1);
            HeightText = Footer.FindViewById<EditText>(Resource.Id.HeightText);
            HeightText1 = Footer.FindViewById<EditText>(Resource.Id.HeightText1);
            BMIText = Footer.FindViewById<EditText>(Resource.Id.BMIText);
            BMIText1 = Footer.FindViewById<EditText>(Resource.Id.BMIText1);

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_ParametersOfUser) + " " + DatabaseUser.GetUser(User.CurrentUser).Name;

                //Getting parameters of currrent user from DB.
                foreach (ParametresOfUser TempParametres in DatabaseUser.GetUser(User.CurrentUser).Parameters)
                {
                    ListForUserParameters.Add(new TableRowParameters(TempParametres.EntryDate.ToShortDateString(), TempParametres.Weight, TempParametres.Height, TempParametres.Index));
                    if (TempParametres.Weight > maxWeight) maxWeight = TempParametres.Weight;
                    if (TempParametres.Height > maxHeight) maxHeight = TempParametres.Height;
                    if (TempParametres.Weight < minWeight || minWeight==-1) minWeight = TempParametres.Weight;
                    if (TempParametres.Height < minHeight || minHeight==-1) minHeight = TempParametres.Height;
                }

                if (maxWeight > 0) WeightText.Text = maxWeight.ToString();
                if (maxHeight > 0) HeightText.Text = maxHeight.ToString();
                if (minWeight > 0) WeightText1.Text = minWeight.ToString();
                if (minHeight > 0) HeightText1.Text = minHeight.ToString();
                BMIText.Text = "-";
                BMIText1.Text = "-";

                //Showing in the list.
                HelpclassListAdapter AdapterForUserParameters = new HelpclassListAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserParameters);
                ListForParameters.Adapter = AdapterForUserParameters;

                if (ListForParameters.FooterViewsCount == 0)
                    ListForParameters.AddFooterView(Footer);
            }

            //If the user isn't choosed.
            else
            {
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.ErrorMessage_Unchoosed);
            }

            //Actions on clicks.
            SetParametersButton.Click += SetParametersButton_Click;

            return List;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Setting parametres for a user.
        private void SetParametersButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for setting parameters of a user.
            AlertDialog.Builder Object = new AlertDialog.Builder(ListOfParameters.activity);
            LayoutInflater inflater = LayoutInflater.From(ListOfParameters.activity);
            LinearLayout layout = new LinearLayout(ListOfParameters.activity);
            View FormViewsSetParametres = inflater.Inflate(Resource.Layout.parametres_Set, layout);
            Object.SetView(FormViewsSetParametres);

            NumberPicker HeightPicker = FormViewsSetParametres.FindViewById<NumberPicker>(Resource.Id.HeightPicker);
            NumberPicker WeightPicker = FormViewsSetParametres.FindViewById<NumberPicker>(Resource.Id.WeightPicker);

            HeightPicker.MinValue = 50;
            HeightPicker.MaxValue = 250;

            if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Count != 0)
                HeightPicker.Value = (int)DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Height;
            else HeightPicker.Value = 150;

            WeightPicker.MinValue = 30;
            WeightPicker.MaxValue = 500;

            if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Count != 0)
                WeightPicker.Value = (int)DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight;
            else WeightPicker.Value = 60;

            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.Action_AddEntry, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If the user is choosed.
                if (Classes.User.CurrentUser != -1)
                {
                    //Getting data and changing the symbols. 
                    DateTime DTForData = System.DateTime.Now;

                    //Creating temporary parameters for a new user.
                    ParametresOfUser TempParametres = new ParametresOfUser(DTForData, WeightPicker.Value, HeightPicker.Value);

                    //For showing results of changes.
                    string ForIndexResults = "";
                    string ForParametersResults = "";

                        //Creating a new layout for showing user changes.
                        AlertDialog.Builder Object_ = new AlertDialog.Builder(ListOfParameters.activity);
                        LayoutInflater inflater_ = LayoutInflater.From(ListOfParameters.activity);
                        LinearLayout layout_ = new LinearLayout(ListOfParameters.activity);
                        View parameters_Changes = inflater.Inflate(Resource.Layout.parameters_Changes, layout_);
                        Object_.SetView(parameters_Changes);

                        //Change of user's BMI.
                        if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 15.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_VerySeverelyUnderweight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 16.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_SeverelyUnderweight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 18.5))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_Underweight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 25.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_HealthyWeight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 30.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_Overweight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 35.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_ModeratelyOverweight);
                        }

                        else if (!HelpclassDataValidation.ComparingValues(TempParametres.Index, 40.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_SeverelyOverweight);
                        }

                        else
                        {
                            ForIndexResults = Resources.GetString(Resource.String.MessageParameters_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.MessageParameters_VerySeverelyOverweight);
                        }

                        //Actions on pressing positive button.
                        Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender_, DialogClickEventArgs e1_){}));

                        //
                        parameters_Changes.FindViewById<TextView>(Resource.Id.TextForIndex).Text = ForIndexResults;

                    //Change of user's weight and height.

                    //If the list of parameters isn't empty.
                    if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Count != 0)
                    {
                        //Ñhange of weight.
                        if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight > TempParametres.Weight)
                        {
                            ForParametersResults = Resources.GetString(Resource.String.MessageParameters_Lost) + " " + Math.Abs(DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " " + Resources.GetString(Resource.String.other_Kilograms);
                        }

                        else if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight < TempParametres.Weight)
                        {
                            ForParametersResults = Resources.GetString(Resource.String.MessageParameters_Gained) + " " + Math.Abs(DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " " + Resources.GetString(Resource.String.other_Kilograms);
                        }

                        //Ñhange of height.
                        if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Height < TempParametres.Height)
                        {
                            ForParametersResults = ForParametersResults + " " + Resources.GetString(Resource.String.MessageGeneral_YouAre) + " " + Math.Abs(DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Height - TempParametres.Height) + " " + Resources.GetString(Resource.String.other_Centimetres);
                        }
                    }
                        //Setting the text in the field.
                        parameters_Changes.FindViewById<TextView>(Resource.Id.TextForParameters).Text = ForParametersResults;

                        Object_.Show();

                        //Updating user parameters.
                        User TempUser = DatabaseUser.GetUser(User.CurrentUser);
                        TempUser.Parameters.Add(TempParametres);
                        DatabaseUser.SQConnection.UpdateWithChildren(TempUser);

                        //Getting parameters of currrent user from DB.
                        ListForUserParameters.Add(new TableRowParameters(TempParametres.EntryDate.ToShortDateString(), TempParametres.Weight, TempParametres.Height, TempParametres.Index));

                        if (TempParametres.Weight > maxWeight) maxWeight = TempParametres.Weight;
                        if (TempParametres.Height > maxHeight) maxHeight = TempParametres.Height;
                        if (TempParametres.Weight < minWeight || minWeight == -1) minWeight = TempParametres.Weight;
                        if (TempParametres.Height < minHeight || minHeight == -1) minHeight = TempParametres.Height;

                        if (maxWeight > 0) WeightText.Text = maxWeight.ToString();
                        if (maxHeight > 0) HeightText.Text = maxHeight.ToString();
                        if (minWeight > 0) WeightText1.Text = minWeight.ToString();
                        if (minHeight > 0) HeightText1.Text = minHeight.ToString();
                        BMIText.Text = "-";
                        BMIText1.Text = "-";     

                        //Setting the parameters to the list.
                        HelpclassListAdapter AdapterForUserParameters = new HelpclassListAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserParameters);
                        ListForParameters.Adapter = AdapterForUserParameters;

                        if (ListForParameters.FooterViewsCount == 0)
                            ListForParameters.AddFooterView(Footer);
                    }

                //If the user is not choosed.
                else
                {
                    Toast.MakeText(Application.Context, Resource.String.ErrorMessage_Unchoosed, ToastLength.Long).Show();
                }

            }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) {}));

            //Showing the new form for entering the paatametres.
            Object.Show();
        }
        }
}