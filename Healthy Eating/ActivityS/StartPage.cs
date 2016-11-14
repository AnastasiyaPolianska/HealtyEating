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
using Healthy_Eating.ActivityS;

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
            SetContentView(Resource.Layout.StartPage);
            
            //Initiation if File (if required), SQConnection and Tables (is required).
            database_User.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "UserData.db");

            //If the list doesn't exist yet, we creae it and fill it with products.
            if (database_Products.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "ProductsData.db"))
            {
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cheese), "Dairyproducts", 40, 23.4, 30, 0, 371));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Condensedmilk), "Dairyproducts", 74.1, 7, 7.9, 9.5, 135));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Condensedmilkwithsugar), "Dairyproducts", 26.5, 7.2, 8.5, 56, 315));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cream10), "Dairyproducts", 82.2, 3, 10, 4, 118));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cream20), "Dairyproducts", 72.9, 2.8, 20, 3.6, 205));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Homemadecheese), "Dairyproducts", 71, 16.7, 9, 1.3, 156));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lowfatkefir), "Dairyproducts", 91.4, 3, 0.1, 3.8, 30));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Milk), "Dairyproducts", 4, 25.6, 25, 39.4, 475));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Naturalyoghurt), "Dairyproducts", 88, 5, 1.5, 3.5, 51));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Processedcheese), "Dairyproducts", 55, 24, 13.5, 0, 226));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryazhanka), "Dairyproducts", 85.3, 3, 6, 4.1, 85));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourcream10), "Dairyproducts", 82.7, 3, 10, 2.9, 116));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourcream20), "Dairyproducts", 72.7, 2.8, 20, 3.2, 206));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitecheese), "Dairyproducts", 88.4, 2.8, 3.2, 4.1, 58));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourmilk), "Dairyproducts", 52, 17.9, 20.1, 0, 260));
                
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Butter), "Fatsmargarinebutter", 15.8, 0.6, 82.5, 0.9, 748));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Margarine), "Fatsmargarinebutter", 15.8, 0.5, 82, 1.2, 744));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mayonnaise), "Fatsmargarinebutter", 25, 3.1, 67, 2.6, 627));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Meltedbutter), "Fatsmargarinebutter", 1, 0.3, 98, 0.6, 887));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Meltedfat), "Fatsmargarinebutter", 0.3, 0, 99.7, 0, 897));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkfat), "Fatsmargarinebutter", 5.7, 1.4, 92.8, 0, 816));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryebread), "Breadandbakeryproducts", 42.4, 4.7, 0.7, 49.8, 214));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitebread), "Breadandbakeryproducts", 34.3, 7.7, 2.4, 53.4, 254));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Backing), "Breadandbakeryproducts", 26.1, 7.6, 4.5, 60, 297));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bagels), "Breadandbakeryproducts", 17, 10.4, 1.3, 67.7, 312));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rusk), "Breadandbakeryproducts", 8, 8.5, 10.6, 71.3, 397));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Wheatflour), "Breadandbakeryproducts", 14, 10.6, 1.3, 73.2, 329));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryeflour), "Breadandbakeryproducts", 14, 6.9, 1.1, 76.9, 326));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Buckwheat), "Cereals", 14, 12.6, 2.6, 68, 329));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Semolina), "Cereals", 14, 11.3, 0.7, 73.3, 326));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Oatmeal), "Cereals", 12, 11.9, 5.8, 65.4, 345));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pearlbarley), "Cereals", 14, 9.3, 1.1, 73.7, 324));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Millet), "Cereals", 14, 12, 2.9, 69.3, 334));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rice), "Cereals", 14, 7, 0.6, 73.7, 323));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Barley), "Cereals", 14, 10.4, 1.3, 71.7, 323));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Oatflakes), "Cereals", 12, 13.1, 6.2, 65.7, 355));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Corngrits), "Cereals", 14, 8.3, 1.2, 75, 325));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Eggplant), "Vegetables", 91,  0.6, 0.1, 5.5, 24));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rutabaga), "Vegetables", 87.5, 1.2, 0.1, 8.1, 37));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenpeas), "Vegetables", 80, 5.0, 0.2, 13.3, 72));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Courgette), "Vegetables", 93, 0.6, 0.3, 5.7, 27));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cabbage), "Vegetables", 90, 1.8, 0, 5.4, 28));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cauliflower), "Vegetables", 90.9, 2.5, 0, 4.9, 29));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Potato), "Vegetables", 76, 2, 0.1, 19.7, 83));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenonion), "Vegetables", 92.5, 1.3, 0, 4.3, 22));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Leek), "Vegetables", 87, 3, 0, 7.3, 40));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Onion), "Vegetables", 86, 1.7, 0, 9.5, 43));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Carrot), "Vegetables", 88.5, 1.3, 0.1, 7, 33));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cucumber), "Vegetables", 95, 0.8, 0, 3, 15));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenpepper), "Vegetables", 92, 1.3, 0, 4.7, 23));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Redpepper), "Vegetables", 91, 1.3, 0, 5.7, 27));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Parsleygreen), "Vegetables", 85, 3.7, 0, 8.1, 45));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Parsleyroot), "Vegetables", 85, 1.5, 0, 11, 47));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rhubarb), "Vegetables", 94.5, 0.7, 0, 2.9, 16));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Radish), "Vegetables", 88.6, 1.9, 0, 7, 34));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Turnip), "Vegetables", 90.5, 1.5, 0, 5.9, 28));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salad), "Vegetables", 95,  1.5, 0, 2.2, 14));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beet), "Vegetables", 86.5, 1.7, 0, 10.8, 48));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Tomato), "Vegetables", 93.5, 0.6, 0, 4.2, 19));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenbeans), "Vegetables", 90, 4, 0, 4.3, 32));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Horseradish), "Vegetables", 77, 2.5, 0, 16.3, 71));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ramson), "Vegetables", 89, 2.4, 0, 6.5, 34));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Garlic), "Vegetables", 70, 6.5, 0, 21.2, 106));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Spinach), "Vegetables", 91.2, 2.9, 0, 2.3, 21));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kvass), "Vegetables", 90, 1.5, 0, 5.3, 28));
                 
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Apricot), "Fruitsandberries", 86, 0.9, 0, 10.5, 46));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Quince), "Fruitsandberries", 87.5, 0.6, 0, 8.9, 38));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Plum), "Fruitsandberries", 89, 0.2, 0, 7.4, 34));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pineapple), "Fruitsandberries", 86, 0.4, 0, 11.8, 48));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Banana), "Fruitsandberries", 74, 1.5, 0, 22.4, 91));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cherry), "Fruitsandberries", 85.5, 0.8, 0, 11.3, 49));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pomegranate), "Fruitsandberries", 85, 0.9, 0, 11.8, 52));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pear), "Fruitsandberries", 87.5, 0.4, 0, 10.7, 42));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Fig), "Fruitsandberries", 83, 0.7, 0, 13.9, 56));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kyzyl), "Fruitsandberries", 85, 1, 0, 9.7, 45));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peach), "Fruitsandberries", 86.5,  0.9, 0, 10.4, 44));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_RowanGarden), "Fruitsandberries", 81, 1.4, 0, 12.5, 58));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_RowanAronia), "Fruitsandberries", 80.5, 1.5, 0, 12, 54));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_PlumGarden), "Fruitsandberries", 87, 0.8, 0, 9.9, 43));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Dates), "Fruitsandberries", 20, 2.5, 0, 72.1, 281));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Persimmon), "Fruitsandberries", 81.5, 0.5, 0, 15.9, 62));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Merry), "Fruitsandberries", 85, 1.1, 0, 12.3, 52));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mulberry), "Fruitsandberries", 82.7, 0.7, 0, 12.7, 53));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Apple), "Fruitsandberries", 86.5, 0.4, 0, 11.3, 46));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Orange), "Fruitsandberries", 87.5, 0.9, 0, 8.4, 38));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Grapefruit), "Fruitsandberries", 89, 0.9, 0, 7.3, 35));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lemon), "Fruitsandberries", 87.7, 0.9, 0, 3.6, 31));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mandarin), "Fruitsandberries", 88.5, 0.8, 0, 8.6, 38));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cranberry), "Fruitsandberries", 87, 0.7, 0, 8.6, 40));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Grapes), "Fruitsandberries", 80.2, 0.4, 0, 17.5, 69));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Blueberries), "Fruitsandberries", 88.2, 1, 0, 7.7, 37));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Blackberry), "Fruitsandberries", 88, 2, 0, 5.3, 33));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Strawberry), "Fruitsandberries", 84.5, 1.8, 0, 8.1, 41));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Gooseberry), "Fruitsandberries", 85, 0.7, 0, 9.9, 44));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Raspberry), "Fruitsandberries", 87, 0.8, 0, 9, 41));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Seabuckthorn), "Fruitsandberries", 75, 0.9, 0, 5.5, 30));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_WhiteCurrant), "Fruitsandberries", 86, 0.3, 0, 8.7, 39));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_RedCurrant), "Fruitsandberries", 85.4, 0.6, 0, 8, 38));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_BlackCurrant), "Fruitsandberries", 85, 1.0, 0, 8.0, 40));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rosehip), "Fruitsandberries", 66, 1.6, 0, 24, 101));
                
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedApricots), "Driedfruits", 20.2, 5.2, 0, 65.9, 272));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedFigs), "Driedfruits", 18, 5, 0, 67.5, 278));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedRaisins), "Driedfruits", 19, 1.8, 0, 70.9, 276));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedCherry), "Driedfruits", 18, 1.5, 0, 73, 292));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPear), "Driedfruits", 24, 2.3, 0, 62.1, 246));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPeaches), "Driedfruits", 18, 3, 0, 68.5, 275));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPrunes), "Driedfruits", 25, 2.3, 0, 65.6, 264));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedApples), "Driedfruits", 20, 3.2, 0, 68, 273));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beans), "Beans", 83, 6, 0.1, 8.3, 58));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peas), "Beans", 14, 23, 1.6, 57.7, 323));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Soybeans), "Beans", 12, 34.9, 17.3, 26.5, 395));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Haricotbeans), "Beans", 14, 22.3, 1.7, 54.5, 309));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lentils), "Beans", 14, 24.8, 1.1, 53.7, 310));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Freshcep), "Mushrooms", 89.9, 3.2, 0.7, 1.6, 25));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Driedcep), "Mushrooms", 13, 27.6, 6.8, 10, 209));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Freshbirchbolete), "Mushrooms", 91.6, 2.3, 0.9, 3.7, 31));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Driedbirchbolete), "Mushrooms", 91.1, 3.3, 0.5, 3.4, 31));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Russule), "Mushrooms", 83, 1.7, 0.3, 1.4, 17));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mutton), "Meatandpoultry", 67.6, 16.3, 15.3, 0, 203));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beef), "Meatandpoultry", 67.7, 18.9, 12.4, 0, 187));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Horse), "Meatandpoultry", 72.5, 20.2, 7, 0, 143));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rabbit), "Meatandpoultry", 65.3, 20.7, 12.9, 0, 199));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Leanpork), "Meatandpoultry", 54.8, 16.4, 27.8, 0, 316));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Veal), "Meatandpoultry", 78, 19.7, 1.2, 0, 90));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambkidneys), "Meatandpoultry", 79.7, 13.6, 2.5, 0, 77));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambliver), "Meatandpoultry", 71.2, 18.7, 2.9, 0, 101));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambheart), "Meatandpoultry", 78.5, 13.5, 2.5, 0, 82));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefmarrow), "Meatandpoultry", 78.9, 9.5, 9.5, 0, 124));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefliver), "Meatandpoultry", 72.9, 17.4, 3.1, 0, 98));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefkidney), "Meatandpoultry", 82.7, 12.5, 1.8, 0, 66));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefudder), "Meatandpoultry", 72.6, 12.3, 13.7, 0, 173));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefheart), "Meatandpoultry", 79, 15, 3, 0, 87));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beeftongue), "Meatandpoultry", 71.2, 13.6, 12.1, 0, 163));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkkidneys), "Meatandpoultry", 80.1, 13, 3.1, 0, 80));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkliver), "Meatandpoultry", 71.4, 18.8, 3.6, 0, 108));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkheart), "Meatandpoultry", 78, 15.1, 3.2, 0, 89));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porktongue), "Meatandpoultry", 66.1, 14.2, 16.8, 0, 208));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Geese), "Meatandpoultry", 49.7, 16.1, 33.3, 0, 364));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Turkey), "Meatandpoultry", 64.5, 21.6, 12, 0.8, 197));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Chicken), "Meatandpoultry", 68.9, 20.8, 8.8, 0.6, 165));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Duck), "Meatandpoultry", 51.5, 16.5, 61.2, 0, 346));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDiabetic), "Sausageandsausageproducts", 62.4, 12.1, 22.8, 0, 254));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDietary), "Sausageandsausageproducts", 71.6, 12.1, 13.5, 0, 170));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDoctorska), "Sausageandsausageproducts", 60.8, 13.7, 22.8, 0, 260));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageLubitelska), "Sausageandsausageproducts", 57, 12.2, 28, 0, 301));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageMolochna), "Sausageandsausageproducts", 62.8, 11.7, 22.8, 0, 252));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageVeal), "Sausageandsausageproducts", 55, 12.5, 29.6, 0, 316));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_PorkSausages), "Sausageandsausageproducts", 53.7, 10.1, 31.6, 1.9, 332));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausagesMolochni), "Sausageandsausageproducts", 60, 12.3, 25.3, 0, 277));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausagesRussian), "Sausageandsausageproducts", 66.2, 12, 19.1, 0, 220));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_StewedBeef), "Meatandcannedmeat", 63, 16.8, 18.3, 0, 232));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_TouristBreakfastBeef), "Meatandcannedmeat", 66.9, 20.5, 10.4, 0, 176));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_TouristBreakfastPork), "Meatandcannedmeat", 65.6, 16.9, 15.4 , 0, 206));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausageStuffing), "Meatandcannedmeat", 63.2, 15.2, 15.7, 2.8, 213));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_StewedPork), "Meatandcannedmeat", 51.1, 14.9, 32.2, 0, 349));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SmokedBrisket), "Meatandcannedmeat", 21, 7.6, 66.8, 0, 632));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ham), "Meatandcannedmeat", 53.5, 22.6, 20.9, 0, 279));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_ChickenEgg), "Eggs", 74, 12.7, 11.5, 0.7, 157));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_EggPowder), "Eggs", 6.8, 45, 37.3, 7.1, 542));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DryEggWhite), "Eggs", 12.1, 73.3, 1.8, 7, 336));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DryEggYolk), "Eggs", 5.4, 34.2, 52.2, 4.4, 623));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_QuailEggs), "Eggs", 73.3, 11.9, 13.1, 0.6, 168));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bulls), "Fishandseafood", 70.8, 12.8, 8.1, 5.2, 145));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_PinkSalmon), "Fishandseafood", 70.5, 21, 7, 0, 147));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Flounder), "Fishandseafood", 79.5, 16.1, 2.6, 0, 88));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Karas), "Fishandseafood", 78.9, 17.7, 1.8, 0, 87));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Carp), "Fishandseafood", 79.1, 16, 3.6, 0, 96));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salmon), "Fishandseafood", 71.3, 22, 5.6, 0, 138));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bream), "Fishandseafood", 77.7, 17.1, 4.1, 0, 105));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lamprey), "Fishandseafood", 75, 14.7, 11.9, 0, 166));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mintay), "Fishandseafood", 80.1, 15.9, 0.7, 0, 70));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Capelin), "Fishandseafood", 75, 13.4, 11.5, 0, 157));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Navaga), "Fishandseafood", 81.1, 16.1, 1, 0, 73));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Burbot), "Fishandseafood", 79.3, 18.8, 0.6, 0, 81));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_NototeniyaMarble), "Fishandseafood", 73.4, 14.8, 10.7, 0, 156));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SeaPerch), "Fishandseafood", 75.4, 17.6, 5.2, 0, 117));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_RiverPerch), "Fishandseafood", 79.2, 18.5, 0.9, 0, 82));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sturgeon), "Fishandseafood", 71.4, 16.4, 10.9, 0, 164));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Halibut), "Fishandseafood", 76.9, 18.9, 3, 0, 103));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whiting), "Fishandseafood", 81.3, 16.1, 0.9, 0, 72));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_FishSaber), "Fishandseafood", 75.2, 20.3, 3.2, 0, 110));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_VimbaCaspian), "Fishandseafood", 77, 19.2, 2.4, 0, 98));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SairaLarge), "Fishandseafood", 59.8, 18.6, 20.8, 0, 262));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SairaSmall), "Fishandseafood", 71.3, 20.4, 0.8, 0, 143));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salak), "Fishandseafood", 75.4, 17.3, 5.6, 0, 121));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Herring), "Fishandseafood", 62.7, 17.7, 19.5, 0, 242));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitefish), "Fishandseafood", 72.3, 19, 7.5, 0, 144));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mackerel), "Fishandseafood", 71.8, 18, 9, 0, 153));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Som), "Fishandseafood", 75, 16.8, 8.5, 0, 144));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Scad), "Fishandseafood", 74.9, 18.5, 5, 0, 119));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sterlet), "Fishandseafood", 74.9, 17, 6.1, 0, 320));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sliver), "Fishandseafood", 80.7, 17.5, 0.6, 0, 75));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Tuna), "Fishandseafood", 74, 22.7, 0.7, 0, 96));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_EelMarine), "Fishandseafood", 77.5, 19.1, 1.9, 0, 94));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Eel), "Fishandseafood", 53.5, 14.5, 30.5, 0, 333));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Hake), "Fishandseafood", 79.9, 16.6, 2.2, 0, 86));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pike), "Fishandseafood", 70.4, 18.8, 0.7, 0, 82));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Elm), "Fishandseafood", 80.1, 18.2, 0.3, 0, 117));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_ShrimpFarEast), "Fishandseafood", 64.8, 28.7, 1.2, 0,134));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kalmar), "Fishandseafood", 80.3, 18, 0.3, 0, 75));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Crab), "Fishandseafood", 81.5, 16, 0.5, 0, 69));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Shrimp), "Fishandseafood", 77.5, 18, 0.8, 0, 83));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Seaweed), "Fishandseafood", 88, 0.9, 0.2, 3, 5));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_KetyGranularCaviar), "Caviar", 46.9, 31.6, 13.8, 0, 251));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_BreamCaviar), "Caviar", 58, 24.7, 4.8, 0, 142));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_PollockRoe), "Caviar", 63.2, 28.4, 1.9, 0, 131));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SturgeonGranularCaviar), "Caviar", 58, 28.9, 9.7, 0, 203));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_SturgeonCaviar), "Caviar", 39.5, 36, 10.2, 0, 123));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Hazelnut), "Nuts", 4.8, 16.1, 66.9, 9.9, 704));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Almonds), "Nuts", 4, 18.6, 57.7, 13.6, 645));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Walnut), "Nuts", 5, 13.8, 61.3, 10.2, 648));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peanuts), "Nuts", 10, 26.3, 45.2, 9.7, 548));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sunflower), "Nuts", 8, 20.7, 52.9, 5, 578));

                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Honey), "Sweets", 17.2, 0.8, 0, 80.3, 308));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_FruitDrops), "Sweets", 7, 3.7, 10.2, 73.1, 384));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Marshmallow), "Sweets", 20, 0.8, 0, 78.3, 299));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Iris), "Sweets", 6.5, 3.3, 7.5, 81.8, 387));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Marmalade), "Sweets", 21, 0, 0.1, 77.7, 296));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Caramel), "Sweets", 4.4, 0, 0.1, 77.7, 296));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CandyChocolatePoured), "Sweets", 7.9, 2.9, 10.7, 76.6, 396));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pastille), "Sweets", 18, 0.5, 0, 80.4, 305));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sugar), "Sweets", 0.2, 0.3, 0, 99.5,  374));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Halva), "Sweets", 2.9, 11.6, 29.7, 54, 516));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_DarkChocolate), "Sweets", 0.8, 5.4, 35.3, 52.6, 540));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_MilkChocolate), "Sweets", 0.9, 6.9, 35.7, 52.4, 547));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_WafflesFruitToppings), "Sweets", 12, 3.2, 2.8, 80.1, 342));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_WafflesFattyToppings), "Sweets", 1, 3.4, 30.2, 64.7, 530));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CakeCream), "Sweets", 9, 5.4, 38.6, 46.4, 544));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_CakeApple), "Sweets", 13, 5.7, 25.6, 52.7, 454));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Gingerbread), "Sweets", 14.5, 4.8, 2.8, 77.7, 336));
                database_Products.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cake), "Sweets", 25, 4.7, 20, 49.8, 386));
            }

            //Current user, if not choosed.
            User.CurrentUser = -1;

            //Elements from layout.
            Button AddNewUserButton = FindViewById<Button>(Resource.Id.MyButton);
            Button ChooseFromExistButton = FindViewById<Button>(Resource.Id.ChooseButton);
            Button ParametersListButton = FindViewById<Button>(Resource.Id.ParametersListButton);
            Button MyMenuButton = FindViewById<Button>(Resource.Id.MenuButton);
            
            //Actions on clicks.
            AddNewUserButton.Click += AddNewUserButton_Click;
            ChooseFromExistButton.Click += ChooseFromExistButton_Click;
            ParametersListButton.Click += ParametersListButton_Click;
            MyMenuButton.Click += MyMenuButton_Click;
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
            View FormViewAdd = inflater.Inflate(Resource.Layout.user_Add, layout);
            Object.SetView(FormViewAdd);
            
            //Action on pressing positive button.
            Object.SetPositiveButton(Resource.String.Action_Add, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                bool IsExisting = false;
                string TempName = FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text;
                //Destroying spaces on the beginning and in the end of the name.
                TempName = TempName.TrimEnd(' ');
                TempName = TempName.TrimStart(' ');

                //If the name of the user was entered incorrectly.
                if (!helpclass_DataValidation.CheckForLenth(TempName, 0, 20))
                    Toast.MakeText(this, helpclass_DataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Name)), ToastLength.Long).Show();

                //If the name was entered.
                else
                {
                    //Checking if there is already a user with such name in our list.
                    foreach (User TempUser in database_User.SQConnection.Table<User>())
                        if (TempUser.Name.ToUpper() == TempName.ToUpper()) IsExisting = true;

                    //If there isn't such user.
                    if (!IsExisting)
                    {                     
                        //Checking if the age is correct.
                        if (!helpclass_DataValidation.CheckForLenth(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text, 0, 4) || 
                            helpclass_DataValidation.CheckForSymbol(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text, '.') || 
                           !helpclass_DataValidation.CheckForValue (int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text), 0, 120))
                            Toast.MakeText(this, helpclass_DataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Age)), ToastLength.Long).Show();

                        //If the age is correct.
                        else
                        {
                            User TempUser = new User(FormViewAdd.FindViewById<EditText>(Resource.Id.UserName).Text, int.Parse(FormViewAdd.FindViewById<EditText>(Resource.Id.UserAge).Text), FormViewAdd.FindViewById<ToggleButton>(Resource.Id.UserSex).Checked ? Classes.ENSex.Male : Classes.ENSex.Female);                    
                            database_User.SQConnection.Insert(TempUser);
                            TempUser.Parameters = new System.Collections.Generic.List<ParametresOfUser>();
                            TempUser.Products = new System.Collections.Generic.List<Product>();
                            database_User.SQConnection.UpdateWithChildren(TempUser);
                        }
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

        //Choosing from existing users in the system.
        private void ChooseFromExistButton_Click(object sender, EventArgs e)
        {
            //Creating a new layout for choosing from existing users.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewChoose = inflater.Inflate(Resource.Layout.user_Choose, layout);
            Object.SetView(FormViewChoose);

            //Element from layout.
            ListOfUsers = FormViewChoose.FindViewById<ListView>(Resource.Id.ListOfUsers);
            TempList = new System.Collections.Generic.List<string>();

            //Displaying users on the layout.
            foreach (User TempUser in database_User.SQConnection.Table<Classes.User>())
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
            User.CurrentUser = e.Position;
            Toast.MakeText(this, database_User.GetUser(User.CurrentUser).ToString(), ToastLength.Long).Show();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        //Action on long click on the item in choosing the use list.
        private void ListOfUsers_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //Creating a new layout for deleting one user.
            AlertDialog.Builder Object = new AlertDialog.Builder(this);
            LayoutInflater inflater = LayoutInflater.From(this);
            LinearLayout layout = new LinearLayout(this);
            View FormViewDelete = inflater.Inflate(Resource.Layout.user_Delete, layout);
            Object.SetView(FormViewDelete);

            FormViewDelete.FindViewById<TextView>(Resource.Id.DeleteUserText).Text = Resources.GetString(Resource.String.Message_DeleteUser) + database_User.GetUser(e.Position).Name + " ?";
                
            //Action on pressing posititve button.
            Object.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
            {
                //If current user is deleted, then the current user isn't choosed.
                if (User.CurrentUser == e.Position) User.CurrentUser = -1;

                //Deleting fromm the DB.
                database_User.DeleteUser(TempList.ElementAt(e.Position));

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
            StartActivity(typeof(ListOfParameters));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        //Going to list of products of the user.
        private void MyMenuButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(page_MyMenu));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------
    }
}