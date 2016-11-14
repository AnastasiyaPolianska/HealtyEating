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
    public class tab_products_List : Fragment
    {
        //Elements from layout.
        ListView ListForParameters;
        TextView IdentifierOfAUser;
        Button SetParametersButton;

        //List for user parameters.    
        List<tablerow_Parameters> ListForUserParameters = new List<tablerow_Parameters>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);     
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View List = inflater.Inflate(Resource.Layout.tab_products_List, null);
            base.OnCreateView(inflater, container, savedInstanceState);
            
            //Initializing elements from the layout.
            IdentifierOfAUser = List.FindViewById<TextView>(Resource.Id.IdentifierOfAUser);
            SetParametersButton = List.FindViewById<Button>(Resource.Id.SetParametersButton);
            ListForParameters = List.FindViewById<ListView>(Resource.Id.ListForParameters);

            //If the user is choosed.
            if (Classes.User.CurrentUser != -1)
            {
                IdentifierOfAUser.Text = Resources.GetString(Resource.String.UserCharacteristic_ParametersOfUser) + " " + database_User.GetUser(User.CurrentUser).Name;

                //Getting parameters of currrent user from DB.
                foreach (ParametresOfUser TempParametres in database_User.GetUser(User.CurrentUser).Parameters)
                    ListForUserParameters.Add(new tablerow_Parameters(TempParametres.EntryDate.ToShortDateString(), TempParametres.Weight, TempParametres.Height, TempParametres.Index));

                //Showing in the list.
                helpclass_ListAdapter AdapterForUserParameters = new helpclass_ListAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserParameters);
                ListForParameters.Adapter = AdapterForUserParameters;
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

            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.Action_AddEntry, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {

                //If the user is choosed.
                if (Classes.User.CurrentUser != -1)
                {
                    //Getting data and changing the symbols. 
                    string ForChangeWeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.WeightEdit).Text;
                    string ForChangeHeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.HeightEdit).Text;
                    DateTime DTForData = System.DateTime.Now;

                    ForChangeWeight = ForChangeWeight.Replace(".", ",");
                    ForChangeHeight = ForChangeHeight.Replace(".", ",");

                    //If weight wasn't entered correctly.
                    if (!helpclass_DataValidation.CheckForLenth(ForChangeWeight, 0, 8) || !helpclass_DataValidation.CheckForValue(double.Parse(ForChangeWeight), 0, 250))
                    {
                        Toast.MakeText(Application.Context, helpclass_DataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Weight)), ToastLength.Long).Show();
                    }

                    else
                    //If height wasn't entered correctly.
                    if (!helpclass_DataValidation.CheckForLenth(ForChangeHeight, 0, 8) || !helpclass_DataValidation.CheckForValue(double.Parse(ForChangeHeight), 0, 300))
                    {
                        Toast.MakeText(Application.Context, helpclass_DataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Height)), ToastLength.Long).Show();
                    }

                    //If everything was entered correctly.
                    else
                    {
                        //Creating temporary parameters for a new user.
                        ParametresOfUser TempParametres = new ParametresOfUser(DTForData, double.Parse(ForChangeWeight), double.Parse(ForChangeHeight));

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
                        if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 15.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_VerySeverelyUnderweight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 16.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_SeverelyUnderweight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 18.5))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_Underweight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 25.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_HealthyWeight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 30.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_Overweight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 35.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_ModeratelyOverweight);
                        }

                        else if (!helpclass_DataValidation.ComparingValues(TempParametres.Index, 40.0))
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_SeverelyOverweight);
                        }

                        else
                        {
                            ForIndexResults = Resources.GetString(Resource.String.Message_BMIout) + " " + TempParametres.Index + Resources.GetString(Resource.String.Message_VerySeverelyOverweight);
                        }

                        //Actions on pressing positive button.
                        Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender_, DialogClickEventArgs e1_){}));

                        //
                        parameters_Changes.FindViewById<TextView>(Resource.Id.TextForIndex).Text = ForIndexResults;

                        //Change of user's weight and height.

                        //If the list of parameters isn't empty.
                        if (database_User.GetUser(User.CurrentUser).Parameters.Count != 0)
                        {
                            //Ñhange of weight.
                            if (database_User.GetUser(User.CurrentUser).Parameters.Last().Weight > TempParametres.Weight)
                            {
                                ForParametersResults = "You've lost " + Math.Abs(database_User.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " kilograms.";
                            }

                            else if (database_User.GetUser(User.CurrentUser).Parameters.Last().Weight < TempParametres.Weight)
                            {
                                ForParametersResults = "You've gained " + Math.Abs(database_User.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " kilograms.";
                            }

                            //Ñhange of height.
                            if (database_User.GetUser(User.CurrentUser).Parameters.Last().Height < TempParametres.Height)
                            {
                                ForParametersResults = ForParametersResults + " You're " + Math.Abs(database_User.GetUser(User.CurrentUser).Parameters.Last().Height - TempParametres.Height) + " centimeters taller now.";
                            }

                        }

                        //Setting the text in the field.
                        parameters_Changes.FindViewById<TextView>(Resource.Id.TextForParameters).Text = ForParametersResults;

                        Object_.Show();

                        //Updating user parameters.
                        User TempUser = database_User.GetUser(User.CurrentUser);
                        TempUser.Parameters.Add(TempParametres);
                        database_User.SQConnection.UpdateWithChildren(TempUser);

                        //Getting parameters of currrent user from DB.
                        ListForUserParameters.Add(new tablerow_Parameters(TempParametres.EntryDate.ToShortDateString(), TempParametres.Weight, TempParametres.Height, TempParametres.Index));

                        //Setting the parameters to the list.
                        helpclass_ListAdapter AdapterForUserParameters = new helpclass_ListAdapter(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, ListForUserParameters);
                        ListForParameters.Adapter = AdapterForUserParameters;
                    }
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