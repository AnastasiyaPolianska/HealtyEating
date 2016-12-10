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
using System.Reflection;
using SQLiteNetExtensions.Extensions;
using Healthy_Eating.Classes;
using Healthy_Eating.Classes.ProductsRange;

namespace Healthy_Eating.ActivityS
{
    [Activity(Label = "Choose drink: ", MainLauncher = false)]
    public class AlcoholAdd : Activity
    {
        //Initializing a list of products.
        List<string> ListForAlcohols;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.alcohol_Add);

            //Main list of types of products from layout, used for choosing products.
            ListView ListForChoosingAlcohols = FindViewById<ListView>(Resource.Id.ListForChoosingAlcohols);
            ListForAlcohols = new List<string>();

            //Setting products to the list.
            foreach (Alcohol TemAlcohol in DatabaseAlcohol.SQConnectionAlcohol.Table<Alcohol>())
            {
                if(TemAlcohol.Visibility == "general" || TemAlcohol.Visibility == DatabaseUser.SQConnection.Table<User>().ElementAt(User.CurrentUser).Name)
                ListForAlcohols.Add(TemAlcohol.Name);
            }

            //Adapter for the main list.
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListForAlcohols);
            ListForChoosingAlcohols.Adapter = adapter;

            //Actions on item clicks.     
            ListForChoosingAlcohols.ItemLongClick += ListForChoosingAlcohols_ItemLongClick;
            ListForChoosingAlcohols.ItemClick += ListForChoosingAlcohols_ItemClick;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Showing product information.
        private void ListForChoosingAlcohols_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //Creating a new layout for showing information about product.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View alcohol_Info = inflater.Inflate(Resource.Layout.alcohol_Info, layout);
            Object.SetView(alcohol_Info);

            //Elements from the layout.
            TextView NameText = alcohol_Info.FindViewById<TextView>(Resource.Id.NameText);
            TextView PercentageText = alcohol_Info.FindViewById<TextView>(Resource.Id.PercentageText);
            TextView CcalsText = alcohol_Info.FindViewById<TextView>(Resource.Id.CcalsText);

            //Temporary product for getting information about choosed product.
            Alcohol TempAlcohol = DatabaseAlcohol.GetAlcohol(ListForAlcohols.ElementAt(e.Position));

            //Showing information.
            NameText.Text = TempAlcohol.Name;
            PercentageText.Text = TempAlcohol.PercentageOfAlchol.ToString();
            CcalsText.Text = TempAlcohol.CCal.ToString();

            //Action on positive button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) {}));

            Object.Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Adding a new product.
        private void ListForChoosingAlcohols_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Creating a new layout for entering a number of grams.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View alcohol_Quantity = inflater.Inflate(Resource.Layout.alcohol_Quantity, layout);
            Object.SetView(alcohol_Quantity);

            //Elements from the layout.
            NumberPicker AlcoholAmount = alcohol_Quantity.FindViewById<NumberPicker>(Resource.Id.AmountPicker);
            AlcoholAmount.MinValue = 1;
            AlcoholAmount.MaxValue = 1000;

            AlcoholAmount.SetFormatter(new HelpclassNumberPickerFormatter());

            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                    //Creating a new product and adding it to the user's list of products.
                    Alcohol TempAlcohol = DatabaseAlcohol.GetAlcohol(ListForAlcohols.ElementAt(e.Position));
                    TempAlcohol.Setter(AlcoholAmount.Value*10, DateTime.Now);

                    User TempUser = DatabaseUser.GetUser(User.CurrentUser);
                    TempUser.Alcohols.Add(TempAlcohol);
                    DatabaseUser.SQConnection.UpdateWithChildren(TempUser);

                    //Showing information about the hew product added.
                    Toast.MakeText(this, DatabaseUser.GetUser(User.CurrentUser).Alcohols.Last().ToString(), ToastLength.Long).Show();

                    AlertDialog.Builder Object_ = new AlertDialog.Builder(this);
                    LayoutInflater inflater_ = LayoutInflater.From(this);
                    LinearLayout layout_ = new LinearLayout(this);
                    View alcohol_Warnings = inflater.Inflate(Resource.Layout.alcohol_Warnings, layout_);
                    Object_.SetView(alcohol_Warnings);

                    TextView TextForLevel = alcohol_Warnings.FindViewById<TextView>(Resource.Id.TextForLevel);
                    TextView TextForWarning = alcohol_Warnings.FindViewById<TextView>(Resource.Id.TextForWarning);

                    double amountOfAlcohol = 0;
                    String forAmountOfAlcohol = "";
                    String forWarning = "";

                    if (DatabaseUser.GetUser(User.CurrentUser).Parameters.Count == 0) forAmountOfAlcohol = "No information about weight!";

                    else
                    {
                        foreach (Alcohol Temp in DatabaseUser.GetUser(User.CurrentUser).Alcohols)
                        {
                            //If the date matches the choosed date.
                            if ((DateTime.Now.DayOfYear - Temp.Date.DayOfYear) <= 1)
                            {
                                double dodamountOfAlcohol = 0;

                                double coef;
                                if (DatabaseUser.GetUser(User.CurrentUser).Sex == ENSex.Male) coef = 0.68;
                                else coef = 0.55;

                                TimeSpan X = DateTime.Now - Temp.Date;

                                dodamountOfAlcohol = Math.Round((Temp.PercentageOfAlchol / (DatabaseUser.GetUser(User.CurrentUser).Parameters.Last().Weight * coef) - (X.Hours * 0.1)),2);

                                if (dodamountOfAlcohol > 0) amountOfAlcohol += dodamountOfAlcohol;
                            }

                            forAmountOfAlcohol = Resources.GetString(Resource.String.Message_YouHave) + " " + amountOfAlcohol + " " + Resources.GetString(Resource.String.Message_ppmOfAlcohol);
                        }

                        if (DatabaseUser.GetUser(User.CurrentUser).Country==ENCountry.Ukraine && amountOfAlcohol > 0.2 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Russia && amountOfAlcohol > 0.35 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Albania && amountOfAlcohol > 0.1 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Andorra && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Armenia && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Austria && amountOfAlcohol > 0.49 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Azerbaijan && amountOfAlcohol > 0.29 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Belarus && amountOfAlcohol > 0.3 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Belgium && amountOfAlcohol > 0.49 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.BosniaAndHerzegovina && amountOfAlcohol > 0.3 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Bulgaria && amountOfAlcohol > 0.49 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Croatia && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Cyprus && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.CzechRepublic && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Denmark && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Estonia && amountOfAlcohol > 0.2 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Finland && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.France && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Georgia && amountOfAlcohol > 0.3 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Germany && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Greece && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Hungary && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Iceland && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Ireland && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Italy && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Latvia && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Liechtenstein && amountOfAlcohol > 0.8 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Lithuania && amountOfAlcohol > 0.4 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Luxembourg && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Macedonia && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Malta && amountOfAlcohol > 0.8 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Moldova && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Montenegro && amountOfAlcohol > 0.3 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Netherlands && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Norway && amountOfAlcohol > 0.2 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Poland && amountOfAlcohol > 0.2 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Portugal && amountOfAlcohol > 0.49 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Romania && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Serbia && amountOfAlcohol > 0.3 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Slovakia && amountOfAlcohol > 0 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Slovenia && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Spain && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Sweden && amountOfAlcohol > 0.2 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Switzerland && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.Turkey && amountOfAlcohol > 0.5 ||
                        DatabaseUser.GetUser(User.CurrentUser).Country == ENCountry.UK && amountOfAlcohol > 0.8)

                        forWarning = Resources.GetString(Resource.String.Message_WarningAlcohol) + " " + DatabaseUser.GetUser(User.CurrentUser).Country + ". " + Resources.GetString(Resource.String.Message_WarningAlcoholTime) + " " + Math.Ceiling(amountOfAlcohol / 1.15) + " " + Resources.GetString(Resource.String.other_Hours) + " ";
                    if (amountOfAlcohol >= 3.5) forWarning += Resources.GetString(Resource.String.Message_WarningAlcoholDeath);
                }

                    TextForLevel.Text = forAmountOfAlcohol;
                    TextForWarning.Text = forWarning;

                    //Action on pressing positive button.
                    Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender_, DialogClickEventArgs e1_) { }));

                Object_.Show();

                }));

            //Action on pressing negative button.
            Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1){}));

            Object.Show();     
        }
    }
}