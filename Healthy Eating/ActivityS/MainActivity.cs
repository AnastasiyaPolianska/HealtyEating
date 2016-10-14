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
    [Activity(Label = "Main Menu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        System.Collections.Generic.List<string> TempList;
        ListView ListOfUsers;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Classes.WorkWithDatabase.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "UserData.db");

            Button AddNewUserButton = FindViewById<Button>(Resource.Id.MyButton);
            Button ChooseFromExistButton = FindViewById<Button>(Resource.Id.ChooseButton);
            Button WeightListButton = FindViewById<Button>(Resource.Id.WeightListButton);
            Button SetParametersButton = FindViewById<Button>(Resource.Id.SetParametersButton);

            AddNewUserButton.Click += AddNewUserButton_Click;
            ChooseFromExistButton.Click += ChooseFromExistButton_Click;
            WeightListButton.Click += WeightListButton_Click;
            SetParametersButton.Click += SetParametersButton_Click;
        }

        private void ChooseFromExistButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewChoose = inflater.Inflate(Resource.Layout.ChooseUserFrom, layout);
            Object.SetView(FormViewChoose);

            ListOfUsers = FormViewChoose.FindViewById<ListView>(Resource.Id.ListOfUsers);
            TempList = new System.Collections.Generic.List<string>();

            foreach (Classes.User TempUser in Classes.WorkWithDatabase.SQConnection.Table<Classes.User>())
                TempList.Add(TempUser.sName);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
            ListOfUsers.Adapter = adapter;
            ListOfUsers.ItemClick += ListOfUsers_ItemClick;
            ListOfUsers.ItemLongClick += ListOfUsers_ItemLongClick;

            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));
            Object.Show();
        }

        private void ListOfUsers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Classes.User.CurrentUser = e.Position;
            Toast.MakeText(this, Classes.User.CurrentUser.ToString(), ToastLength.Long).Show();
        }

        private void ListOfUsers_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewDelete = inflater.Inflate(Resource.Layout.DeleteUserForm, layout);
            Object.SetView(FormViewDelete);

            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                Classes.WorkWithDatabase.SQConnection.Delete<Classes.User>(TempList.ElementAt(e.Position));
                TempList.RemoveAt(e.Position);
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
                ListOfUsers.Adapter = adapter;
            }));

            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));


            Object.Show();
        }

        private void AddNewUserButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewAdd = inflater.Inflate(Resource.Layout.AddNewUserForm, layout);
            Object.SetView(FormViewAdd);

            Object.SetPositiveButton(Resource.String.Add, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                bool bIsExisting = false;
                string TempName = FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text;
                TempName = TempName.TrimEnd(' ');
                TempName = TempName.TrimStart(' ');

                foreach (Classes.User TempUser in Classes.WorkWithDatabase.SQConnection.Table<Classes.User>())
                    if (TempUser.sName.ToUpper() == TempName.ToUpper()) bIsExisting = true;

                if (!bIsExisting)
                {
                    Classes.User TempUser = new Classes.User(FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text, int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text), FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserSex).Pressed ? Classes.ENSex.Male : Classes.ENSex.Female);
                    Classes.WorkWithDatabase.SQConnection.Insert(TempUser);
                    TempUser.Parameters = new System.Collections.Generic.List<Classes.ParametresOfUser>();
                    //       TempUser.Parameters.Add(new Classes.ParametresOfUser(DateTime.Now, 0, 0));
                    Classes.WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                }
                else Toast.MakeText(this, "Name already exists!", ToastLength.Long).Show();
            }
            ));

            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            Object.Show();
        }

        private void WeightListButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(WeightList));
        }

        private void SetParametersButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewsSetParametres = inflater.Inflate(Resource.Layout.SetParametresForm, layout);
            Object.SetView(FormViewsSetParametres);

            Object.SetPositiveButton(Resource.String.AddEntry, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //Зчитування даних, введених користувачем із заміною символів для уникнення проблем з перетворенням типів.
                string sForChangeWeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.WeightEdit).Text;
                string sForChangeHeight = FormViewsSetParametres.FindViewById<TextView>(Resource.Id.HeightEdit).Text;
                DateTime DTForData = System.DateTime.Now;

                sForChangeWeight = sForChangeWeight.Replace(".", ",");
                sForChangeHeight = sForChangeHeight.Replace(".", ",");

                //Обробка помилок про некоректно введену вагу.
                if ((sForChangeWeight.Length > 3) || (double.Parse(sForChangeWeight) > 500 || double.Parse(sForChangeWeight) < 0))
                {
                    Toast.MakeText(this, sForChangeWeight + " isn't correct weight!", ToastLength.Long).Show();

                }

                else
                //Обробка помилок про некоректно введену вагу.
                if ((sForChangeHeight.Length > 3) || (double.Parse(sForChangeHeight) > 500 || double.Parse(sForChangeHeight) < 0))
                {
                    Toast.MakeText(this, sForChangeHeight + " isn't correct height!", ToastLength.Long).Show();
                }

                else
                {
                    Classes.ParametresOfUser TempParametres = new Classes.ParametresOfUser(DTForData, double.Parse(sForChangeWeight), double.Parse(sForChangeHeight));
                    Classes.User TempUser = Classes.WorkWithDatabase.SQConnection.GetWithChildren<Classes.User>(Classes.WorkWithDatabase.SQConnection.Table<Classes.User>().ElementAt(Classes.User.CurrentUser).sName);
                    TempUser.Parameters.Add(TempParametres);
                    Classes.WorkWithDatabase.SQConnection.UpdateWithChildren(TempUser);
                }

            }));

            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));


            Object.Show();
        }
    }
}

