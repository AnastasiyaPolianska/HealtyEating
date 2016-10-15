using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace Healthy_Eating
{
    /*Main Menu*/
    [Activity(Label = "Main Menu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //List of users, used fordisplaying users and deleting one of them.
        System.Collections.Generic.List<string> TempList;
        //List of users from layout, used for choosing a user and deleting one of them.
        ListView ListOfUsers;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            
            //Initiation if File (if required), SQConnection and Tables (is required).
            Classes.WorkWithDatabase.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "UserData.db");
            
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
                if (TempName.Length == 0) Toast.MakeText(this, "Enter your name.", ToastLength.Long).Show();

                //If the name was entered.
                else
                {
                    //Checking if there is already a user with such name in our list.
                    foreach (Classes.User TempUser in Classes.WorkWithDatabase.SQConnection.Table<Classes.User>())
                        if (TempUser.sName.ToUpper() == TempName.ToUpper()) bIsExisting = true;

                    //If there isn't such user.
                    if (!bIsExisting)
                    {

                        //Checking if the age is correct.
                        if (FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text.Length > 2 || FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text.Contains('.') || int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text) < 0)
                            Toast.MakeText(this, "Enter correct age.", ToastLength.Long).Show();

                        //If the age is correct.
                        else
                        {
                            Classes.User TempUser = new Classes.User(FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text, int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text), FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserSex).Checked ? Classes.ENSex.Male : Classes.ENSex.Female);
                            Classes.WorkWithDatabase.SQConnection.Insert(TempUser);
                            TempUser.Parameters = new System.Collections.Generic.List<Classes.ParametresOfUser>();
                            Classes.WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                        }
                    }

                    //If there is such user.
                    else Toast.MakeText(this, "Name already exists.", ToastLength.Long).Show();
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
                TempList.Add(TempUser.sName);

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
            Toast.MakeText(this, Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).ToString() , ToastLength.Long).Show();
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
            DeleteUserTextView.Text = "Do you want to delete this user: " + Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(e.Position).sName + " ?";
                
            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //Deleting fromm the DB.
                Classes.WorkWithDatabase.SQConnection.Delete<Classes.User>(TempList.ElementAt(e.Position));

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
            //Creating a new layout for deleting one user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewsSetParametres = inflater.Inflate(Resource.Layout.SetParametresForm, layout);
            Object.SetView(FormViewsSetParametres);

            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.AddEntry, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //Getting data and changing the symbols. 
                string sForChangeWeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.WeightEdit).Text;
                string sForChangeHeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.HeightEdit).Text;
                DateTime DTForData = System.DateTime.Now;

                sForChangeWeight = sForChangeWeight.Replace(".", ",");
                sForChangeHeight = sForChangeHeight.Replace(".", ",");

                //If weight wasn't entered correctly.
                if ((sForChangeWeight.Split('.').Length - 1 > 1) || (sForChangeWeight.Length > 3) || (sForChangeWeight.Length == 0) || (double.Parse(sForChangeWeight) > 500 || double.Parse(sForChangeWeight) < 0))
                {
                    Toast.MakeText(this, sForChangeWeight + " isn't correct weight!", ToastLength.Long).Show();

                }

                else
                //If height wasn't entered correctly.
                if ((sForChangeHeight.Split('.').Length - 1 > 1) || (sForChangeHeight.Length > 3) || (sForChangeHeight.Length == 0) || (double.Parse(sForChangeHeight) > 500 || double.Parse(sForChangeHeight) < 0))
                {
                    Toast.MakeText(this, sForChangeHeight + " isn't correct height!", ToastLength.Long).Show();
                }

                //If everything was entered correctly.
                else
                {
                    Classes.ParametresOfUser TempParametres = new Classes.ParametresOfUser(DTForData, double.Parse(sForChangeWeight), double.Parse(sForChangeHeight));
                    Classes.User TempUser = Classes.WorkWithDatabase.SQConnection.GetWithChildren<Classes.User>(Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).sName);
                    TempUser.Parameters.Add(TempParametres);
                    Classes.WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                }

            }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form for entering the paatametres.
            Object.Show();
        }
    }
}

