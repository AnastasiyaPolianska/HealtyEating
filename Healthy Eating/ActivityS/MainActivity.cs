using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using Healthy_Eating.Classes;

namespace Healthy_Eating
{
    /*Main Menu*/
    [Activity(Label = "Main Menu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //List of users, used for displaying users and deleting them.
        System.Collections.Generic.List<string> TempList;
        //List of users from layout, used for choosing a user and deleting them.
        ListView ListOfUsers;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            
            //Initiation if File (if required), SQConnection and Tables (is required).
            Classes.WorkWithDatabase.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "UserData.db");

            //Current user, if not choosed.
            Classes.User.CurrentUser = -1;

            //Elements from layout.
            Button AddNewUserButton = FindViewById<Button>(Resource.Id.MyButton);
            Button ChooseFromExistButton = FindViewById<Button>(Resource.Id.ChooseButton);
            Button ParametersListButton = FindViewById<Button>(Resource.Id.ParametersListButton);
            Button SetParametersButton = FindViewById<Button>(Resource.Id.SetParametersButton);
            
            //Actions on clicks.
            AddNewUserButton.Click += AddNewUserButton_Click;
            ChooseFromExistButton.Click += ChooseFromExistButton_Click;
            ParametersListButton.Click += ParametersListButton_Click;
            SetParametersButton.Click += SetParametersButton_Click;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /*Actions on clicks: description*/

        //Adding a new user to the system.
        private void AddNewUserButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for entering parameters of a new user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewAdd = inflater.Inflate(Resource.Layout.AddNewUserForm, layout);
            Object.SetView(FormViewAdd);
            
            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.Add, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                bool bIsExisting = false;
                string TempName = FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text;
                //Destroying spaces on the beginning and in the end of the name.
                TempName = TempName.TrimEnd(' ');
                TempName = TempName.TrimStart(' ');

                //If the name of the user wasn't entered.
                if (!CheckingData.CheckForLenth(TempName, 0, 20))
                    Toast.MakeText(this, CheckingData.RequestToCorrectEnter("name"), ToastLength.Long).Show();

                //If the name was entered.
                else
                {
                    //Checking if there is already a user with such name in our list.
                    foreach (Classes.User TempUser in Classes.WorkWithDatabase.SQConnection.Table<Classes.User>())
                        if (TempUser.Name.ToUpper() == TempName.ToUpper()) bIsExisting = true;

                    //If there isn't such user.
                    if (!bIsExisting)
                    {
                        
                        //Checking if the age is correct.
                        if (!CheckingData.CheckForLenth(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text, 0, 4) || 
                            CheckingData.CheckForSymbol(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text, '.') || 
                           !CheckingData.CheckForValue (int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text), 0, 120))
                            Toast.MakeText(this, CheckingData.RequestToCorrectEnter("age"), ToastLength.Long).Show();

                        //If the age is correct.
                        else
                        {
                            int a = int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text);
                            Toast.MakeText(this, FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text, ToastLength.Long).Show();
                            User TempUser = new User(FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text, a, FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserSex).Checked ? Classes.ENSex.Male : Classes.ENSex.Female);
                            //Toast.MakeText(this, TempUser.Age, ToastLength.Long).Show();
                            WorkWithDatabase.SQConnection.Insert(TempUser);
                            TempUser.Parameters = new System.Collections.Generic.List<ParametresOfUser>();
                           // WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                        }
                    }

                    //If there is such user.
                    else Toast.MakeText(this, Resource.String.AlreadyInSystem, ToastLength.Long).Show();
                }
            }
            ));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form of adding a new user.
            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing from existing users in the system.
        private void ChooseFromExistButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for choosing from existing users.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewChoose = inflater.Inflate(Resource.Layout.ChooseUserFrom, layout);
            Object.SetView(FormViewChoose);

            //Element from layout.
            ListOfUsers = FormViewChoose.FindViewById<ListView>(Resource.Id.ListOfUsers);
            TempList = new System.Collections.Generic.List<string>();

            //Displaying users on the layout.
            foreach (Classes.User TempUser in Classes.WorkWithDatabase.SQConnection.Table<Classes.User>())
                TempList.Add(TempUser.Name);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
            ListOfUsers.Adapter = adapter;

            //Actions on clicks on the items.
            ListOfUsers.ItemClick += ListOfUsers_ItemClick;
            ListOfUsers.ItemLongClick += ListOfUsers_ItemLongClick;

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));
            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Action on short click on the item in choosing the use list.
        private void ListOfUsers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Setting the current user and displaying info about him/her.
            Classes.User.CurrentUser = e.Position;
            Toast.MakeText(this, WorkWithDatabase.GetUser(User.CurrentUser).ToString() , ToastLength.Long).Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Action on long click on the item in choosing the use list.
        private void ListOfUsers_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //Creating a new layout for deleting one user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewDelete = inflater.Inflate(Resource.Layout.DeleteUserForm, layout);
            Object.SetView(FormViewDelete);

            TextView DeleteUserTextView = FormViewDelete.FindViewById<TextView>(Resource.Id.DeleteUserText);
            DeleteUserTextView.Text = Resources.GetString(Resource.String.DeleteUser) + Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(e.Position).Name + " ?";
                
            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If current user is deleted, then the current user isn't choosed.
                if (Classes.User.CurrentUser == e.Position) Classes.User.CurrentUser = -1;

                //Deleting fromm the DB.
               WorkWithDatabase.DeleteUser(TempList.ElementAt(e.Position));

                //Deleting from the list.
                TempList.RemoveAt(e.Position);
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
                ListOfUsers.Adapter = adapter;
            }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form for deleting a user.
            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Going to list of parametres of the user.
        private void ParametersListButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(WeightList));
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Setting parametres for a user.
        private void SetParametersButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for setting parameters of a user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewsSetParametres = inflater.Inflate(Resource.Layout.SetParametresForm, layout);
            Object.SetView(FormViewsSetParametres);

            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.AddEntry, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {

                //If the user is choosed.
                if (Classes.User.CurrentUser != -1)
                {

                    //Getting data and changing the symbols. 
                    string sForChangeWeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.WeightEdit).Text;
                    string sForChangeHeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.HeightEdit).Text;
                    DateTime DTForData = System.DateTime.Now;

                    sForChangeWeight = sForChangeWeight.Replace(".", ",");
                    sForChangeHeight = sForChangeHeight.Replace(".", ",");

                    //If weight wasn't entered correctly.
                    if (!CheckingData.CheckForLenth(sForChangeWeight,0,8) ||  !CheckingData.CheckForValue(double.Parse(sForChangeWeight), 0, 500))                
                    {
                        Toast.MakeText(this, CheckingData.RequestToCorrectEnter("weight"), ToastLength.Long).Show();
                    }

                    else
                    //If height wasn't entered correctly.
                    if (!CheckingData.CheckForLenth(sForChangeHeight, 0, 8) || !CheckingData.CheckForValue(double.Parse(sForChangeHeight), 0, 300))
                    {
                        Toast.MakeText(this, CheckingData.RequestToCorrectEnter("height"), ToastLength.Long).Show();
                    }

                    //If everything was entered correctly.
                    else
                    {
                        //Creating temporary parameters for a new user.
                        Classes.ParametresOfUser TempParametres = new Classes.ParametresOfUser(DTForData, double.Parse(sForChangeWeight), double.Parse(sForChangeHeight));

                        //For showing results of changes.
                        string sForIndexResults = "";
                        string sForParametersResults = "";

                        //Creating a new layout for showing user changes.
                        AlertDialog.Builder Object_ = new AlertDialog.Builder(this);
                        LayoutInflater inflater_ = LayoutInflater.From(this);
                        LinearLayout layout_ = new LinearLayout(this);
                        View FormViewChangeParameters = inflater.Inflate(Resource.Layout.ParametersChangesForm, layout_);
                        Object_.SetView(FormViewChangeParameters);

                        //Change of user's BMI.
                        if (!CheckingData.ComparingValues(TempParametres.Index, 15.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". ATTENTION: you are very severely underweight!";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 16.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". ATTENTION: you are severely underweight!";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 18.5))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". You are underweight!";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 25.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". You have healthy weight for your height.";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 30.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". You are overweight!";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 35.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". ATTENTION: you are moderately overweight!";
                        }

                        else if (!CheckingData.ComparingValues(TempParametres.Index, 40.0))
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". ATTENTION: you are severely overweight!";
                        }

                        else
                        {
                            sForIndexResults = Resources.GetString(Resource.String.BMIout) + " " + TempParametres.Index + ". ATTENTION: you are very severely overweight!";
                        }

                        Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender_, DialogClickEventArgs e1_)
                        {
                        }));

                        FormViewChangeParameters.FindViewById<TextView>(Resource.Id.TextForIndex).Text = sForIndexResults;

                        //Change of user's weight and height.

                        //If the list of parameters isn't empty.
                        if (WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Count != 0)
                        {
                            //Сhange of weight.
                            if (WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Weight > TempParametres.Weight)
                            {
                                sForParametersResults = "You've lost " + Math.Abs(WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " kilograms.";
                            }

                            else if (WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Weight < TempParametres.Weight)
                            {
                                sForParametersResults = "You've gained " + Math.Abs(WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Weight - TempParametres.Weight) + " kilograms.";
                            }

                            //Сhange of height.
                            if (WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Height < TempParametres.Height)
                            {
                                sForParametersResults = sForParametersResults + " You're " + Math.Abs(WorkWithDatabase.GetUser(User.CurrentUser).Parameters.Last().Height - TempParametres.Height) + " centimeters taller now.";
                            }

                        }

                        FormViewChangeParameters.FindViewById<TextView>(Resource.Id.TextForParameters).Text = sForParametersResults;

                        Object_.Show();

                        Classes.User TempUser = WorkWithDatabase.GetUser(User.CurrentUser);
                        TempUser.Parameters.Add(TempParametres);
                        Classes.WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                    }
                }

                else
                {                  
                Toast.MakeText(this, Resource.String.Unchoosed, ToastLength.Long).Show();
                }

            }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form for entering the paatametres.
            Object.Show();
        }
    }
}