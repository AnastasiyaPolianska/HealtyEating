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
using SQLiteNetExtensions.Extensions;

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "Users in system: ")]
    public class PageUsers : Activity
    {
        //Elements from the layout.       
        ListView ListOfUsers;
        ListView CategoryChooser;
        Button AddNewUserButton;
        Button Change;
        ToggleButton UserLicence;
        ToggleButton ChildDiseases;
        ToggleButton ChronicDiseases;

        //Global variables.
        List<string> TempList;
        DateTime Entry;
        List <ENDriverLicence> TempLicences;
        List<ENChildDiseases> TempChildDiseases;
        List <ENChronicDiseases> TempChronicDiseases;

        int UserChoosed = -1;

        //On creating the form.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.user_Choose);

            //Element from layout.
            ListOfUsers = FindViewById<ListView>(Resource.Id.ListOfUsers);
            AddNewUserButton = FindViewById<Button>(Resource.Id.AddUserButton);

            //Allocating memory for the list of users.
            TempList = new System.Collections.Generic.List<string>();

            //Type of licence.
            TempLicences = new List<ENDriverLicence>();
            TempChildDiseases = new List<ENChildDiseases>();
            TempChronicDiseases = new List<ENChronicDiseases>();

            //Actions on clicks.
            ListOfUsers.ItemClick += ListOfUsers_ItemClick;
            ListOfUsers.ItemLongClick += ListOfUsers_ItemLongClick;
            AddNewUserButton.Click += AddNewUserButton_Click;
        }

        //On renewingthe view.
        protected override void OnResume()
        {
            base.OnResume();

            TempList.Clear();

            /*Displaying users on the layout.*/
            foreach (User TempUser in DatabaseUser.SQConnection.Table<Classes.User>())
                TempList.Add(TempUser.Name);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
            ListOfUsers.Adapter = adapter;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /*Actions on clicks*/

        //Action on short click on the item in choosing the user list.
        private void ListOfUsers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Setting the current user and displaying info about him/her.
            User.CurrentUser = e.Position;

            //Information about the user.
            Toast.MakeText(this, DatabaseUser.GetUser(User.CurrentUser).ToString(), ToastLength.Long).Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Action on long click on the item in choosing the user list.
        private void ListOfUsers_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            UserChoosed = e.Position;

            //Creating a new layout for deleting one user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewMenu = inflater.Inflate(Resource.Layout.user_MiniMenu, layout);
            Object.SetView(FormViewMenu);

            Button ShowingInfo = FormViewMenu.FindViewById<Button>(Resource.Id.ShowingInfo);
            Button DeleteUser = FormViewMenu.FindViewById<Button>(Resource.Id.DeleteUser);

            ShowingInfo.Click += ShowingInfo_Click;
            DeleteUser.Click += DeleteUser_Click;

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form for deleting a user.
            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            //Creating a new layout for deleting one user.
            AlertDialog.Builder Object1 = new AlertDialog.Builder(this);
            LayoutInflater inflater1 = LayoutInflater.From(this);
            LinearLayout layout1 = new LinearLayout(this);
            View FormViewDelete = inflater1.Inflate(Resource.Layout.user_Delete, layout1);
            Object1.SetView(FormViewDelete);

            //Text for displaying the message.
            FormViewDelete.FindViewById<TextView>(Resource.Id.DeleteUserText).Text = Resources.GetString(Resource.String.Message_DeleteUser) + DatabaseUser.GetUser(UserChoosed).Name + " ?";

            //Action on pressing negative button.
            Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If current user is deleted, then the current user isn't choosed.
                if (User.CurrentUser == UserChoosed) User.CurrentUser = -1;
                else if (UserChoosed < User.CurrentUser) User.CurrentUser--;

                //Deleting the products for the user that is being deleted.
                foreach (Product TempProduct in DatabaseProducts.SQConnectionProduct.Table<Product>())
                {
                    if (TempProduct.Visibility == DatabaseUser.SQConnection.Table<User>().ElementAt(UserChoosed).Name)
                        DatabaseProducts.DeleteProduct(TempProduct.Name);
                }

                //Deleting fromm the DB.
                DatabaseUser.DeleteUser(TempList.ElementAt(UserChoosed));

                //Deleting from the list.
                TempList.RemoveAt(UserChoosed);
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, TempList);
                ListOfUsers.Adapter = adapter;
            }));

            //Action on pressing negative button.
            Object1.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form for deleting a user.
            Object1.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        private void ShowingInfo_Click(object sender, EventArgs e)
        {
            //Creating a new layout for showing information about the user.
            AlertDialog.Builder Object1 = new AlertDialog.Builder(this);
            LayoutInflater inflater1 = LayoutInflater.From(this);
            LinearLayout layout1 = new LinearLayout(this);
            View FormViewInfo = inflater1.Inflate(Resource.Layout.user_Info, layout1);
            Object1.SetView(FormViewInfo);

            TextView NameText = FormViewInfo.FindViewById<TextView>(Resource.Id.NameText);
            TextView AgeText = FormViewInfo.FindViewById<TextView>(Resource.Id.AgeText);
            TextView SexText = FormViewInfo.FindViewById<TextView>(Resource.Id.SexText);
            TextView CountryText = FormViewInfo.FindViewById<TextView>(Resource.Id.CountryText);

            NameText.Text = DatabaseUser.GetUser(UserChoosed).Name;

            TimeSpan temp = DateTime.Now - DatabaseUser.GetUser(UserChoosed).Age;

            AgeText.Text =  Math.Round((double)((temp.Days) / 365.25), 0).ToString();

            SexText.Text = DatabaseUser.GetUser(UserChoosed).Sex.ToString();
            CountryText.Text = DatabaseUser.GetUser(UserChoosed).Country.ToString();

            Button ChildDiseases = FormViewInfo.FindViewById<Button>(Resource.Id.ChildDiseases);
            Button ChronicDiseases = FormViewInfo.FindViewById<Button>(Resource.Id.ChronicDiseases);
            Button DrivingLicences = FormViewInfo.FindViewById<Button>(Resource.Id.DrivingLicences);

            ChildDiseases.Click += ChildDiseases_Click1;
            ChronicDiseases.Click += ChronicDiseases_Click1;
            DrivingLicences.Click += DrivingLicences_Click;

            //Action on pressing negative button.
            Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form.
            Object1.Show();
        }

        private void DrivingLicences_Click(object sender, EventArgs e)
        {
            //Creating a new layout for choosing category.
            AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View ChooseCategoryForm = inflater1.Inflate(Resource.Layout.user_ChangeCategory, layout1);
            Object1.SetView(ChooseCategoryForm);

            //List for categories of licence.
            CategoryChooser = ChooseCategoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

            //Text for showing the message.
            TextView ForText = ChooseCategoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
            ForText.Text = Resources.GetString(Resource.String.Column_DrivingLicences);

            Change = ChooseCategoryForm.FindViewById<Button>(Resource.Id.ChangeButton);

            //List for holding items for CountryChooser.
            List<string> ListForCategories = new List<string>();

            //Adding categories to the list.
            ListForCategories.Add(Resources.GetString(Resource.String.Category_A1));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_A));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_B1));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_B));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_C1));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_C));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_D1));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_D));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_BE));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_CE));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_C1E));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_D1E));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_DE));
            ListForCategories.Add(Resources.GetString(Resource.String.Category_T));

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
            CategoryChooser.Adapter = adapter;

            CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.A1)) CategoryChooser.SetItemChecked(0, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.A)) CategoryChooser.SetItemChecked(1, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.B1)) CategoryChooser.SetItemChecked(2, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.B)) CategoryChooser.SetItemChecked(3, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.C1)) CategoryChooser.SetItemChecked(4, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.C)) CategoryChooser.SetItemChecked(5, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.D1)) CategoryChooser.SetItemChecked(6, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.D)) CategoryChooser.SetItemChecked(7, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.BE)) CategoryChooser.SetItemChecked(8, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.CE)) CategoryChooser.SetItemChecked(9, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.C1E)) CategoryChooser.SetItemChecked(10, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.D1E)) CategoryChooser.SetItemChecked(11, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.DE)) CategoryChooser.SetItemChecked(12, true);
            if (DatabaseUser.GetUser(UserChoosed).DriverLicence.Contains(ENDriverLicence.T)) CategoryChooser.SetItemChecked(13, true);

            CategoryChooser.ItemClick += CategoryChooser_ItemClick;
            Change.Click += Change_ClickDrivingLicence;

            //Action on pressing positive button.
            Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
            }));

            Object1.Show();
        }

        private void Change_ClickDrivingLicence(object sender, EventArgs e)
        {
            TempLicences.Clear();

            var sparseArray = CategoryChooser.CheckedItemPositions;
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                if (sparseArray.KeyAt(i) == 0 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.A1);
                if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.A);
                if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.B1);
                if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.B);
                if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C1);
                if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C);
                if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D1);
                if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D);
                if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.BE);
                if (sparseArray.KeyAt(i) == 9 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.CE);
                if (sparseArray.KeyAt(i) == 10 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C1E);
                if (sparseArray.KeyAt(i) == 11 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D1E);
                if (sparseArray.KeyAt(i) == 12 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.DE);
                if (sparseArray.KeyAt(i) == 13 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.T);
            }           

            bool temp;
            if (TempLicences.Count == 0) temp = false;
            else temp = true;
            User tempuser = DatabaseUser.GetUser(UserChoosed);

            tempuser.DrivingLicence = temp;
            tempuser.DriverLicence = TempLicences;

            DatabaseUser.SQConnection.UpdateWithChildren(tempuser);
        }

        private void ChronicDiseases_Click1(object sender, EventArgs e)
        {
            //Creating a new layout for choosing category.
            AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View ChooseCtegoryForm = inflater1.Inflate(Resource.Layout.user_ChangeCategory, layout1);
            Object1.SetView(ChooseCtegoryForm);

            //List for categories of diseases.
            CategoryChooser = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

            //Text for showing the message.
            TextView ForText = ChooseCtegoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
            ForText.Text = Resources.GetString(Resource.String.Column_ChronicDiseases);

            Change = ChooseCtegoryForm.FindViewById<Button>(Resource.Id.ChangeButton);

            //List for holding items for CountryChooser.
            List<string> ListForCategories = new List<string>();

            //Adding countries to the list.
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Atherosclerosis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_CardiacIschemia));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicMyocarditis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Cardiomyopathy));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Herpes));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Cytomegalovirus));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_HPV));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_COPD));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicLungAbscess));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicalBronchitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_BronchialAsthma));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Pyelonephritis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicCystitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_StonesInTheKidneys));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicGastritis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicPancreatitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicColitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Urethritis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Prostatitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Orchitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Epididymitis));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Adnexitis));

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
            CategoryChooser.Adapter = adapter;

            CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Atherosclerosis)) CategoryChooser.SetItemChecked(0, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.CardiacIschemia)) CategoryChooser.SetItemChecked(1, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicMyocarditis)) CategoryChooser.SetItemChecked(2, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Cardiomyopathy)) CategoryChooser.SetItemChecked(3, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Herpes)) CategoryChooser.SetItemChecked(4, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Cytomegalovirus)) CategoryChooser.SetItemChecked(5, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.HPV)) CategoryChooser.SetItemChecked(6, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.COPD)) CategoryChooser.SetItemChecked(7, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicLungAbscess)) CategoryChooser.SetItemChecked(8, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicalBronchitis)) CategoryChooser.SetItemChecked(9, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.BronchialAsthma)) CategoryChooser.SetItemChecked(10, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Pyelonephritis)) CategoryChooser.SetItemChecked(11, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicCystitis)) CategoryChooser.SetItemChecked(12, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.StonesInTheKidneys)) CategoryChooser.SetItemChecked(13, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicGastritis)) CategoryChooser.SetItemChecked(14, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicPancreatitis)) CategoryChooser.SetItemChecked(15, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.ChronicColitis)) CategoryChooser.SetItemChecked(16, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Urethritis)) CategoryChooser.SetItemChecked(17, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Prostatitis)) CategoryChooser.SetItemChecked(18, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Orchitis)) CategoryChooser.SetItemChecked(19, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Epididymitis)) CategoryChooser.SetItemChecked(20, true);
            if (DatabaseUser.GetUser(UserChoosed).ChronicDiseasesList.Contains(ENChronicDiseases.Adnexitis)) CategoryChooser.SetItemChecked(21, true);

            CategoryChooser.ItemClick += CategoryChooser_ItemClick;
            Change.Click += Change_ClickChronicDiseases;

            //Action on pressing positive button.
            Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {}));

            Object1.Show();
        }

        private void Change_ClickChronicDiseases(object sender, EventArgs e)
        {
            TempChronicDiseases.Clear();

            var sparseArray = CategoryChooser.CheckedItemPositions;
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                if (sparseArray.KeyAt(i) == 0 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Atherosclerosis);
                if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.CardiacIschemia);
                if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicMyocarditis);
                if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Cardiomyopathy);
                if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Herpes);
                if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Cytomegalovirus);
                if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.HPV);
                if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.COPD);
                if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicLungAbscess);
                if (sparseArray.KeyAt(i) == 9 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicalBronchitis);
                if (sparseArray.KeyAt(i) == 10 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.BronchialAsthma);
                if (sparseArray.KeyAt(i) == 11 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Pyelonephritis);
                if (sparseArray.KeyAt(i) == 12 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicCystitis);
                if (sparseArray.KeyAt(i) == 13 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.StonesInTheKidneys);
                if (sparseArray.KeyAt(i) == 14 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicGastritis);
                if (sparseArray.KeyAt(i) == 15 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicPancreatitis);
                if (sparseArray.KeyAt(i) == 16 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicColitis);
                if (sparseArray.KeyAt(i) == 17 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Urethritis);
                if (sparseArray.KeyAt(i) == 18 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Prostatitis);
                if (sparseArray.KeyAt(i) == 19 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Orchitis);
                if (sparseArray.KeyAt(i) == 20 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Epididymitis);
                if (sparseArray.KeyAt(i) == 21 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Adnexitis);
            }

            bool temp;
            if (TempChronicDiseases.Count == 0) temp = false;
            else temp = true;
            User tempuser = DatabaseUser.GetUser(UserChoosed);

            tempuser.ChronicDiseases = temp;
            tempuser.ChronicDiseasesList = TempChronicDiseases;

            DatabaseUser.SQConnection.UpdateWithChildren(tempuser);
        }

        private void ChildDiseases_Click1(object sender, EventArgs e)
        {
            //Creating a new layout for choosing category.
            AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View ChooseCtegoryForm = inflater1.Inflate(Resource.Layout.user_ChangeCategory, layout1);
            Object1.SetView(ChooseCtegoryForm);

            //List for categories of diseases.
            CategoryChooser = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

            //Text for showing the message.
            TextView ForText = ChooseCtegoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
            ForText.Text = Resources.GetString(Resource.String.Column_ChildDiseases);

            Change = ChooseCtegoryForm.FindViewById<Button>(Resource.Id.ChangeButton);

            //List for holding items for CountryChooser.
            List<string> ListForCategories = new List<string>();

            //Adding countries to the list.
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Measles));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChickenPox));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_WhoopingCough));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Diphtheria));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_ScarletFever));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Mumps));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_Polio));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_PneumococcalDisease));
            ListForCategories.Add(Resources.GetString(Resource.String.Disease_HaemophilusInfluenzae));

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
            CategoryChooser.Adapter = adapter;

            CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Measles)) CategoryChooser.SetItemChecked(0, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.ChickenPox)) CategoryChooser.SetItemChecked(1, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.WhoopingCough)) CategoryChooser.SetItemChecked(2, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Diphtheria)) CategoryChooser.SetItemChecked(3, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.ScarletFever)) CategoryChooser.SetItemChecked(4, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Mumps)) CategoryChooser.SetItemChecked(5, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Polio)) CategoryChooser.SetItemChecked(6, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Pneumococcal)) CategoryChooser.SetItemChecked(7, true);
            if (DatabaseUser.GetUser(UserChoosed).ChildrenDiseasesList.Contains(ENChildDiseases.Haemophilus)) CategoryChooser.SetItemChecked(8, true);

            CategoryChooser.ItemClick += CategoryChooser_ItemClick;
            Change.Click += Change_ClickChildDiseases;

            //Action on pressing positive button.
            Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {}));

            Object1.Show();
        }

        private void Change_ClickChildDiseases(object sender, EventArgs e)
        {
            TempChildDiseases.Clear();

            var sparseArray = CategoryChooser.CheckedItemPositions;
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                if (sparseArray.KeyAt(i) == 0 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Measles);
                if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.ChickenPox);
                if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.WhoopingCough);
                if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Diphtheria);
                if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.ScarletFever);
                if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Mumps);
                if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Polio);
                if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Pneumococcal);
                if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Haemophilus);
            }

            bool temp;
            if (TempChildDiseases.Count == 0) temp = false;
            else temp = true;
            User tempuser = DatabaseUser.GetUser(UserChoosed);

            tempuser.ChildDiseases = temp;
            tempuser.ChildrenDiseasesList = TempChildDiseases;

            DatabaseUser.SQConnection.UpdateWithChildren(tempuser);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Clicking on one item in the list makes the button of applying changes enabled.
        private void CategoryChooser_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Change.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Adding a new user to the system.
        private void AddNewUserButton_Click(object sender, EventArgs e)
        {
            //Time of adding a user.
            Entry = DateTime.Now;

            //Creating a new layout for entering parameters of a new user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewAdd = inflater.Inflate(Resource.Layout.user_Add, layout);
            Object.SetView(FormViewAdd);

            //List for countries.
            Spinner CountryChooser = FormViewAdd.FindViewById<Spinner>(Resource.Id.CountryChooser);
            Button ChooseDateButton = FormViewAdd.FindViewById<Button>(Resource.Id.ChooseDateButton);

            //Button for choosing the date.
            ChooseDateButton.Click += ChooseDateButton_Click;

            //User's driver lisence.
            UserLicence = FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserLicence);
            UserLicence.Click += UserLicence_Click;

            //User's child diseases.
            ChildDiseases = FormViewAdd.FindViewById<ToggleButton>(Resource.Id.ChildDiseases);
            ChildDiseases.Click += ChildDiseases_Click;

            //User's chronic diseases.
            ChronicDiseases = FormViewAdd.FindViewById<ToggleButton>(Resource.Id.ChronicDiseases);
            ChronicDiseases.Click += ChronicDiseases_Click;

            //List for holding items for CountryChooser.
            List<string> ListForCountries = new List<string>();

            //Adding countries to the list.
            ListForCountries.Add(Resources.GetString(Resource.String.country_Ukraine));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Russia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Albania));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Andorra));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Armenia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Azerbaijan));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Austria));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Belarus));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Belgium));
            ListForCountries.Add(Resources.GetString(Resource.String.country_BosniaAndHerzegovina));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Bulgaria));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Croatia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Cyprus));
            ListForCountries.Add(Resources.GetString(Resource.String.country_CzechRepublic));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Denmark));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Estonia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Finland));
            ListForCountries.Add(Resources.GetString(Resource.String.country_France));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Georgia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Germany));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Greece));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Hungary));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Iceland));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Ireland));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Italy));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Latvia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Liechtenstein));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Lithuania));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Luxembourg));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Macedonia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Malta));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Moldova));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Montenegro));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Netherlands));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Norway));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Poland));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Portugal));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Romania));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Serbia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Slovakia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Slovenia));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Spain));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Sweden));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Switzerland));
            ListForCountries.Add(Resources.GetString(Resource.String.country_Turkey));
            ListForCountries.Add(Resources.GetString(Resource.String.country_UK));

            //Setting the list to the view.
            var adapter_ = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListForCountries);
            CountryChooser.Adapter = adapter_;

            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.Action_Add, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //bool for checking is there is such name in database.
                bool IsExisting = false;

                //Name of the user that is going to be added.
                string TempName = FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text;
                
                //Destroying spaces on the beginning and in the end of the name.
                TempName = TempName.TrimEnd(' ');
                TempName = TempName.TrimStart(' ');

                //If the name of the user was entered incorrectly.
                if (!HelpclassDataValidation.CheckForLenth(TempName, 0, 20))
                    Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Name)), ToastLength.Long).Show();

                //If the name was entered.
                else
                {
                    //Checking if there is already a user with such name in our list.
                    foreach (User TempUser in DatabaseUser.SQConnection.Table<User>())
                        if (TempUser.Name.ToUpper() == TempName.ToUpper()) IsExisting = true;

                    //If there isn't such user.
                    if (!IsExisting)
                    {
                        /*Setting the country.*/
                        ENCountry TempCountry = ENCountry.Ukraine;

                        switch (CountryChooser.SelectedItemId)
                        {
                            case 0:
                                {
                                    TempCountry = ENCountry.Ukraine;
                                }

                                break;

                            case 1:
                                {
                                    TempCountry = ENCountry.Russia;
                                }

                                break;

                            case 2:
                                {
                                    TempCountry = ENCountry.Albania;
                                }

                                break;

                            case 3:
                                {
                                    TempCountry = ENCountry.Andorra;
                                }

                                break;

                            case 4:
                                {
                                    TempCountry = ENCountry.Armenia;
                                }

                                break;

                            case 5:
                                {
                                    TempCountry = ENCountry.Azerbaijan;
                                }

                                break;

                            case 6:
                                {
                                    TempCountry = ENCountry.Austria;
                                }

                                break;

                            case 7:
                                {
                                    TempCountry = ENCountry.Belarus;
                                }

                                break;

                            case 8:
                                {
                                    TempCountry = ENCountry.Belgium;
                                }

                                break;

                            case 9:
                                {
                                    TempCountry = ENCountry.BosniaAndHerzegovina;
                                }

                                break;

                            case 10:
                                {
                                    TempCountry = ENCountry.Bulgaria;
                                }

                                break;

                            case 11:
                                {
                                    TempCountry = ENCountry.Croatia;
                                }

                                break;

                            case 12:
                                {
                                    TempCountry = ENCountry.Cyprus;
                                }

                                break;

                            case 13:
                                {
                                    TempCountry = ENCountry.CzechRepublic;
                                }

                                break;

                            case 14:
                                {
                                    TempCountry = ENCountry.Denmark;
                                }

                                break;

                            case 15:
                                {
                                    TempCountry = ENCountry.Estonia;
                                }

                                break;

                            case 16:
                                {
                                    TempCountry = ENCountry.Finland;
                                }

                                break;

                            case 17:
                                {
                                    TempCountry = ENCountry.France;
                                }

                                break;

                            case 18:
                                {
                                    TempCountry = ENCountry.Georgia;
                                }

                                break;

                            case 19:
                                {
                                    TempCountry = ENCountry.Germany;
                                }

                                break;

                            case 20:
                                {
                                    TempCountry = ENCountry.Greece;
                                }

                                break;

                            case 21:
                                {
                                    TempCountry = ENCountry.Hungary;
                                }

                                break;

                            case 22:
                                {
                                    TempCountry = ENCountry.Iceland;
                                }

                                break;

                            case 23:
                                {
                                    TempCountry = ENCountry.Ireland;
                                }

                                break;

                            case 24:
                                {
                                    TempCountry = ENCountry.Italy;
                                }

                                break;

                            case 25:
                                {
                                    TempCountry = ENCountry.Latvia;
                                }

                                break;

                            case 26:
                                {
                                    TempCountry = ENCountry.Liechtenstein;
                                }

                                break;

                            case 27:
                                {
                                    TempCountry = ENCountry.Lithuania;
                                }

                                break;

                            case 28:
                                {
                                    TempCountry = ENCountry.Luxembourg;
                                }

                                break;

                            case 29:
                                {
                                    TempCountry = ENCountry.Macedonia;
                                }

                                break;

                            case 30:
                                {
                                    TempCountry = ENCountry.Malta;
                                }

                                break;

                            case 31:
                                {
                                    TempCountry = ENCountry.Moldova;
                                }

                                break;

                            case 32:
                                {
                                    TempCountry = ENCountry.Montenegro;
                                }

                                break;

                            case 33:
                                {
                                    TempCountry = ENCountry.Netherlands;
                                }

                                break;

                            case 34:
                                {
                                    TempCountry = ENCountry.Norway;
                                }

                                break;

                            case 35:
                                {
                                    TempCountry = ENCountry.Poland;
                                }

                                break;

                            case 36:
                                {
                                    TempCountry = ENCountry.Portugal;
                                }

                                break;

                            case 37:
                                {
                                    TempCountry = ENCountry.Romania;
                                }

                                break;

                            case 38:
                                {
                                    TempCountry = ENCountry.Serbia;
                                }

                                break;

                            case 39:
                                {
                                    TempCountry = ENCountry.Slovakia;
                                }

                                break;

                            case 40:
                                {
                                    TempCountry = ENCountry.Slovenia;
                                }

                                break;

                            case 41:
                                {
                                    TempCountry = ENCountry.Spain;
                                }

                                break;

                            case 42:
                                {
                                    TempCountry = ENCountry.Sweden;
                                }

                                break;

                            case 43:
                                {
                                    TempCountry = ENCountry.Switzerland;
                                }

                                break;

                            case 44:
                                {
                                    TempCountry = ENCountry.Turkey;
                                }

                                break;

                            case 45:
                                {
                                    TempCountry = ENCountry.UK;
                                }

                                break;
                        }

                        //If the date isn't bigger than today's.
                        if (Entry.ToShortDateString() != DateTime.Now.ToShortDateString() && Entry < DateTime.Now)
                        {
                            //User's sex.
                            ToggleButton UserSex = FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserSex);

                            if (!UserLicence.Checked) TempLicences.Clear();
                            if (!ChildDiseases.Checked) TempChildDiseases.Clear();
                            if (!ChronicDiseases.Checked) TempChronicDiseases.Clear();

                            //Creating a new user.
                            User TempUser = new User(FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text, Entry, UserSex.Checked ? Classes.ENSex.Male : Classes.ENSex.Female, TempCountry, UserLicence.Checked ? true : false, TempLicences, ChildDiseases.Checked ? true : false, TempChildDiseases, ChronicDiseases.Checked ? true : false, TempChronicDiseases);
                            DatabaseUser.SQConnection.Insert(TempUser);
                            TempUser.Parameters = new System.Collections.Generic.List<ParametresOfUser>();
                            TempUser.Products = new System.Collections.Generic.List<Product>();
                            TempUser.Alcohols = new System.Collections.Generic.List<Alcohol>();
                            TempUser.Cigarettes = new List<Cigarette>();
                            DatabaseUser.SQConnection.UpdateWithChildren(TempUser);

                            //Renewing the list.
                            OnResume();
                        }

                        //Error message.
                        else Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_date)), ToastLength.Long).Show();
                    }

                    //If there is such user.
                    else Toast.MakeText(this, Resource.String.ErrorMessage_AlreadyInSystem, ToastLength.Long).Show();
                }
            }
            ));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

            //Showing the new form of adding a new user.
            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing the date of user's birth.
        private void ChooseDateButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for choosing date.
            AlertDialog.Builder Object = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LayoutInflater inflater = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            LinearLayout layout = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
            View ChooseDateForm = inflater.Inflate(Resource.Layout.product_ChooseDate, layout);
            Object.SetView(ChooseDateForm);

            //Element from the layout.
            DatePicker DateChooser = ChooseDateForm.FindViewById<DatePicker>(Resource.Id.DateChooser);

            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If the date isn't correct.
                if (DateChooser.DateTime > DateTime.Now)
                    Toast.MakeText(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_date)), ToastLength.Long).Show();
                
                //If everything is correct.
                else
                {
                    //Setting the date.
                    Entry = DateChooser.DateTime;
                    //Renewing the layout.
                    OnResume();
                }
            }
            ));

            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing the type of licence.
        private void UserLicence_Click(object sender, EventArgs e)
        {
            if (UserLicence.Checked)
            {
                //Creating a new layout for choosing category.
                AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View ChooseCtegoryForm = inflater1.Inflate(Resource.Layout.user_ChooseCategory, layout1);
                Object1.SetView(ChooseCtegoryForm);

                //List for categories of licence.
                CategoryChooser = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

                //Text for showing the message.
                TextView ForText = ChooseCtegoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
                ForText.Text = Resources.GetString(Resource.String.Message_DrivingLicence);

                //List for holding items for CountryChooser.
                List<string> ListForCategories = new List<string>();

                //Adding categories to the list.
                ListForCategories.Add(Resources.GetString(Resource.String.Category_A1));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_A));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_B1));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_B));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_C1));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_C));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_D1));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_D));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_BE));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_CE));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_C1E));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_D1E));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_DE));
                ListForCategories.Add(Resources.GetString(Resource.String.Category_T));

                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
                CategoryChooser.Adapter = adapter;

                CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

                 if (TempLicences.Contains(ENDriverLicence.A1)) CategoryChooser.SetItemChecked(0, true);
                if (TempLicences.Contains(ENDriverLicence.A)) CategoryChooser.SetItemChecked(1, true);
                if (TempLicences.Contains(ENDriverLicence.B1)) CategoryChooser.SetItemChecked(2, true);
                if (TempLicences.Contains(ENDriverLicence.B)) CategoryChooser.SetItemChecked(3, true);
                if (TempLicences.Contains(ENDriverLicence.C1)) CategoryChooser.SetItemChecked(4, true);
                if (TempLicences.Contains(ENDriverLicence.C)) CategoryChooser.SetItemChecked(5, true);
                if (TempLicences.Contains(ENDriverLicence.D1)) CategoryChooser.SetItemChecked(6, true);
                if (TempLicences.Contains(ENDriverLicence.D)) CategoryChooser.SetItemChecked(7, true);
                if (TempLicences.Contains(ENDriverLicence.BE)) CategoryChooser.SetItemChecked(8, true);
                if (TempLicences.Contains(ENDriverLicence.CE)) CategoryChooser.SetItemChecked(9, true);
                if (TempLicences.Contains(ENDriverLicence.C1E)) CategoryChooser.SetItemChecked(10, true);
                if (TempLicences.Contains(ENDriverLicence.D1E)) CategoryChooser.SetItemChecked(11, true);
                if (TempLicences.Contains(ENDriverLicence.DE)) CategoryChooser.SetItemChecked(12, true);
                if (TempLicences.Contains(ENDriverLicence.T)) CategoryChooser.SetItemChecked(13, true);

                //Action on pressing positive button.
                Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    TempLicences.Clear();

                    var sparseArray = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser).CheckedItemPositions;
                    for (var i = 0; i < sparseArray.Size(); i++)
                    {
                        if(sparseArray.KeyAt(i)==0 && sparseArray.ValueAt(i)==true) TempLicences.Add(ENDriverLicence.A1);
                        if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.A);
                        if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.B1);
                        if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.B);
                        if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C1);
                        if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C);
                        if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D1);
                        if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D);
                        if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.BE);
                        if (sparseArray.KeyAt(i) == 9 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.CE);
                        if (sparseArray.KeyAt(i) == 10 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.C1E);
                        if (sparseArray.KeyAt(i) == 11 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.D1E);
                        if (sparseArray.KeyAt(i) == 12 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.DE);
                        if (sparseArray.KeyAt(i) == 13 && sparseArray.ValueAt(i) == true) TempLicences.Add(ENDriverLicence.T);
                    }

                    if (TempLicences.Count == 0) UserLicence.Checked = false;
                }));

                Object1.Show();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing child diseases.
        private void ChildDiseases_Click(object sender, EventArgs e)
        {
            if (ChildDiseases.Checked)
            {
                //Creating a new layout for choosing category.
                AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View ChooseCtegoryForm = inflater1.Inflate(Resource.Layout.user_ChooseCategory, layout1);
                Object1.SetView(ChooseCtegoryForm);

                //List for categories of diseases.
                CategoryChooser = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

                //Text for showing the message.
                TextView ForText = ChooseCtegoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
                ForText.Text = Resources.GetString(Resource.String.Message_ChildDeseases);

                //List for holding items for CountryChooser.
                List<string> ListForCategories = new List<string>();

                //Adding countries to the list.
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Measles));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChickenPox));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_WhoopingCough));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Diphtheria));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ScarletFever));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Mumps));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Polio));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_PneumococcalDisease));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_HaemophilusInfluenzae));

                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
                CategoryChooser.Adapter = adapter;

                CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

                if (TempChildDiseases.Contains(ENChildDiseases.Measles)) CategoryChooser.SetItemChecked(0, true);
                if (TempChildDiseases.Contains(ENChildDiseases.ChickenPox)) CategoryChooser.SetItemChecked(1, true);
                if (TempChildDiseases.Contains(ENChildDiseases.WhoopingCough)) CategoryChooser.SetItemChecked(2, true);
                if (TempChildDiseases.Contains(ENChildDiseases.Diphtheria)) CategoryChooser.SetItemChecked(3, true);
                if (TempChildDiseases.Contains(ENChildDiseases.ScarletFever)) CategoryChooser.SetItemChecked(4, true);
                if (TempChildDiseases.Contains(ENChildDiseases.Mumps)) CategoryChooser.SetItemChecked(5, true);
                if (TempChildDiseases.Contains(ENChildDiseases.Polio)) CategoryChooser.SetItemChecked(6, true);
                if (TempChildDiseases.Contains(ENChildDiseases.Pneumococcal)) CategoryChooser.SetItemChecked(7, true);
                if (TempChildDiseases.Contains(ENChildDiseases.Haemophilus)) CategoryChooser.SetItemChecked(8, true);

                //Action on pressing positive button.
                Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    TempChildDiseases.Clear();

                    var sparseArray = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser).CheckedItemPositions;
                    for (var i = 0; i < sparseArray.Size(); i++)
                    {
                        if (sparseArray.KeyAt(i) == 0 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Measles);
                        if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.ChickenPox);
                        if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.WhoopingCough);
                        if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Diphtheria);
                        if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.ScarletFever);
                        if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Mumps);
                        if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Polio);
                        if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Pneumococcal);
                        if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempChildDiseases.Add(ENChildDiseases.Haemophilus);
                    }

                    if (TempChildDiseases.Count == 0) ChildDiseases.Checked = false;
                }));

                Object1.Show();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        private void ChronicDiseases_Click(object sender, EventArgs e)
        {
            if (ChronicDiseases.Checked)
            {
                //Creating a new layout for choosing category.
                AlertDialog.Builder Object1 = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LayoutInflater inflater1 = LayoutInflater.From(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                LinearLayout layout1 = new LinearLayout(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                View ChooseCtegoryForm = inflater1.Inflate(Resource.Layout.user_ChooseCategory, layout1);
                Object1.SetView(ChooseCtegoryForm);

                //List for categories of diseases.
                CategoryChooser = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser);

                //Text for showing the message.
                TextView ForText = ChooseCtegoryForm.FindViewById<TextView>(Resource.Id.TextForMessage);
                ForText.Text = Resources.GetString(Resource.String.Message_ChronicDeseases);

                //List for holding items for CountryChooser.
                List<string> ListForCategories = new List<string>();

                //Adding countries to the list.
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Atherosclerosis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_CardiacIschemia));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicMyocarditis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Cardiomyopathy));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Herpes));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Cytomegalovirus));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_HPV));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_COPD));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicLungAbscess));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicalBronchitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_BronchialAsthma));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Pyelonephritis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicCystitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_StonesInTheKidneys));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicGastritis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicPancreatitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_ChronicColitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Urethritis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Prostatitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Orchitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Epididymitis));
                ListForCategories.Add(Resources.GetString(Resource.String.Disease_Adnexitis));

                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, ListForCategories);
                CategoryChooser.Adapter = adapter;

                CategoryChooser.ChoiceMode = Android.Widget.ChoiceMode.Multiple;

                if (TempChronicDiseases.Contains(ENChronicDiseases.Atherosclerosis)) CategoryChooser.SetItemChecked(0, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.CardiacIschemia)) CategoryChooser.SetItemChecked(1, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicMyocarditis)) CategoryChooser.SetItemChecked(2, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Cardiomyopathy)) CategoryChooser.SetItemChecked(3, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Herpes)) CategoryChooser.SetItemChecked(4, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Cytomegalovirus)) CategoryChooser.SetItemChecked(5, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.HPV)) CategoryChooser.SetItemChecked(6, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.COPD)) CategoryChooser.SetItemChecked(7, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicLungAbscess)) CategoryChooser.SetItemChecked(8, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicalBronchitis)) CategoryChooser.SetItemChecked(9, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.BronchialAsthma)) CategoryChooser.SetItemChecked(10, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Pyelonephritis)) CategoryChooser.SetItemChecked(11, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicCystitis)) CategoryChooser.SetItemChecked(12, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.StonesInTheKidneys)) CategoryChooser.SetItemChecked(13, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicGastritis)) CategoryChooser.SetItemChecked(14, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicPancreatitis)) CategoryChooser.SetItemChecked(15, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.ChronicColitis)) CategoryChooser.SetItemChecked(16, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Urethritis)) CategoryChooser.SetItemChecked(17, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Prostatitis)) CategoryChooser.SetItemChecked(18, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Orchitis)) CategoryChooser.SetItemChecked(19, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Epididymitis)) CategoryChooser.SetItemChecked(20, true);
                if (TempChronicDiseases.Contains(ENChronicDiseases.Adnexitis)) CategoryChooser.SetItemChecked(21, true);

                //Action on pressing positive button.
                Object1.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                {
                    TempChronicDiseases.Clear();

                    var sparseArray = ChooseCtegoryForm.FindViewById<ListView>(Resource.Id.CategoryChooser).CheckedItemPositions;
                    for (var i = 0; i < sparseArray.Size(); i++)
                    {
                        if (sparseArray.KeyAt(i) == 0 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Atherosclerosis);
                        if (sparseArray.KeyAt(i) == 1 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.CardiacIschemia);
                        if (sparseArray.KeyAt(i) == 2 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicMyocarditis);
                        if (sparseArray.KeyAt(i) == 3 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Cardiomyopathy);
                        if (sparseArray.KeyAt(i) == 4 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Herpes);
                        if (sparseArray.KeyAt(i) == 5 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Cytomegalovirus);
                        if (sparseArray.KeyAt(i) == 6 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.HPV);
                        if (sparseArray.KeyAt(i) == 7 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.COPD);
                        if (sparseArray.KeyAt(i) == 8 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicLungAbscess);
                        if (sparseArray.KeyAt(i) == 9 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicalBronchitis);
                        if (sparseArray.KeyAt(i) == 10 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.BronchialAsthma);
                        if (sparseArray.KeyAt(i) == 11 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Pyelonephritis);
                        if (sparseArray.KeyAt(i) == 12 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicCystitis);
                        if (sparseArray.KeyAt(i) == 13 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.StonesInTheKidneys);
                        if (sparseArray.KeyAt(i) == 14 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicGastritis);
                        if (sparseArray.KeyAt(i) == 15 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicPancreatitis);
                        if (sparseArray.KeyAt(i) == 16 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.ChronicColitis);
                        if (sparseArray.KeyAt(i) == 17 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Urethritis);
                        if (sparseArray.KeyAt(i) == 18 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Prostatitis);
                        if (sparseArray.KeyAt(i) == 19 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Orchitis);
                        if (sparseArray.KeyAt(i) == 20 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Epididymitis);
                        if (sparseArray.KeyAt(i) == 21 && sparseArray.ValueAt(i) == true) TempChronicDiseases.Add(ENChronicDiseases.Adnexitis);
                }

                    if (TempChronicDiseases.Count == 0) ChronicDiseases.Checked = false;
                }));

                Object1.Show();
            }
        }
    }
}