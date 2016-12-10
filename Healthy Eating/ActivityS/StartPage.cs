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
using Healthy_Eating.Classes.ProductsRange;
using System.Collections.Generic;

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
            DatabaseUser.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databasesofuser") + "UserFile.db");

            //If the list doesn't exist yet, we creae it and fill it with products.
            if (DatabaseProducts.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "ProductsData.db"))
            {
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cheese), "Dairyproducts", "general", 40, 23.4, 30, 0, 371));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Condensedmilk), "Dairyproducts", "general", 74.1, 7, 7.9, 9.5, 135));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Condensedmilkwithsugar), "Dairyproducts", "general", 26.5, 7.2, 8.5, 56, 315));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cream10), "Dairyproducts", "general", 82.2, 3, 10, 4, 118));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cream20), "Dairyproducts", "general", 72.9, 2.8, 20, 3.6, 205));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Homemadecheese), "Dairyproducts", "general", 71, 16.7, 9, 1.3, 156));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lowfatkefir), "Dairyproducts", "general", 91.4, 3, 0.1, 3.8, 30));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Milk), "Dairyproducts", "general", 4, 25.6, 25, 39.4, 475));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Naturalyoghurt), "Dairyproducts", "general", 88, 5, 1.5, 3.5, 51));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Processedcheese), "Dairyproducts", "general", 55, 24, 13.5, 0, 226));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryazhanka), "Dairyproducts", "general", 85.3, 3, 6, 4.1, 85));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourcream10), "Dairyproducts", "general", 82.7, 3, 10, 2.9, 116));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourcream20), "Dairyproducts", "general", 72.7, 2.8, 20, 3.2, 206));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitecheese), "Dairyproducts", "general", 88.4, 2.8, 3.2, 4.1, 58));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sourmilk), "Dairyproducts", "general", 52, 17.9, 20.1, 0, 260));
                
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Butter), "Fatsmargarinebutter", "general", 15.8, 0.6, 82.5, 0.9, 748));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Margarine), "Fatsmargarinebutter", "general", 15.8, 0.5, 82, 1.2, 744));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mayonnaise), "Fatsmargarinebutter", "general", 25, 3.1, 67, 2.6, 627));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Meltedbutter), "Fatsmargarinebutter", "general", 1, 0.3, 98, 0.6, 887));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Meltedfat), "Fatsmargarinebutter", "general", 0.3, 0, 99.7, 0, 897));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkfat), "Fatsmargarinebutter", "general", 5.7, 1.4, 92.8, 0, 816));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryebread), "Breadandbakeryproducts", "general", 42.4, 4.7, 0.7, 49.8, 214));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitebread), "Breadandbakeryproducts", "general", 34.3, 7.7, 2.4, 53.4, 254));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Backing), "Breadandbakeryproducts", "general", 26.1, 7.6, 4.5, 60, 297));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bagels), "Breadandbakeryproducts", "general", 17, 10.4, 1.3, 67.7, 312));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rusk), "Breadandbakeryproducts", "general", 8, 8.5, 10.6, 71.3, 397));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Wheatflour), "Breadandbakeryproducts", "general", 14, 10.6, 1.3, 73.2, 329));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ryeflour), "Breadandbakeryproducts", "general", 14, 6.9, 1.1, 76.9, 326));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Buckwheat), "Cereals", "general", 14, 12.6, 2.6, 68, 329));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Semolina), "Cereals", "general", 14, 11.3, 0.7, 73.3, 326));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Oatmeal), "Cereals", "general", 12, 11.9, 5.8, 65.4, 345));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pearlbarley), "Cereals", "general", 14, 9.3, 1.1, 73.7, 324));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Millet), "Cereals", "general", 14, 12, 2.9, 69.3, 334));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rice), "Cereals", "general", 14, 7, 0.6, 73.7, 323));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Barley), "Cereals", "general", 14, 10.4, 1.3, 71.7, 323));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Oatflakes), "Cereals", "general", 12, 13.1, 6.2, 65.7, 355));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Corngrits), "Cereals", "general", 14, 8.3, 1.2, 75, 325));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Eggplant), "Vegetables", "general", 91,  0.6, 0.1, 5.5, 24));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rutabaga), "Vegetables", "general", 87.5, 1.2, 0.1, 8.1, 37));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenpeas), "Vegetables", "general", 80, 5.0, 0.2, 13.3, 72));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Courgette), "Vegetables", "general", 93, 0.6, 0.3, 5.7, 27));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cabbage), "Vegetables", "general", 90, 1.8, 0, 5.4, 28));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cauliflower), "Vegetables", "general", 90.9, 2.5, 0, 4.9, 29));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Potato), "Vegetables", "general", 76, 2, 0.1, 19.7, 83));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenonion), "Vegetables", "general", 92.5, 1.3, 0, 4.3, 22));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Leek), "Vegetables", "general", 87, 3, 0, 7.3, 40));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Onion), "Vegetables", "general", 86, 1.7, 0, 9.5, 43));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Carrot), "Vegetables", "general", 88.5, 1.3, 0.1, 7, 33));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cucumber), "Vegetables", "general", 95, 0.8, 0, 3, 15));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenpepper), "Vegetables", "general", 92, 1.3, 0, 4.7, 23));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Redpepper), "Vegetables", "general", 91, 1.3, 0, 5.7, 27));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Parsleygreen), "Vegetables", "general", 85, 3.7, 0, 8.1, 45));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Parsleyroot), "Vegetables", "general", 85, 1.5, 0, 11, 47));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rhubarb), "Vegetables", "general", 94.5, 0.7, 0, 2.9, 16));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Radish), "Vegetables", "general", 88.6, 1.9, 0, 7, 34));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Turnip), "Vegetables", "general", 90.5, 1.5, 0, 5.9, 28));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salad), "Vegetables", "general", 95,  1.5, 0, 2.2, 14));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beet), "Vegetables", "general", 86.5, 1.7, 0, 10.8, 48));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Tomato), "Vegetables", "general", 93.5, 0.6, 0, 4.2, 19));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Greenbeans), "Vegetables", "general", 90, 4, 0, 4.3, 32));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Horseradish), "Vegetables", "general", 77, 2.5, 0, 16.3, 71));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ramson), "Vegetables", "general", 89, 2.4, 0, 6.5, 34));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Garlic), "Vegetables", "general", 70, 6.5, 0, 21.2, 106));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Spinach), "Vegetables", "general", 91.2, 2.9, 0, 2.3, 21));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kvass), "Vegetables", "general", 90, 1.5, 0, 5.3, 28));
                 
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Apricot), "Fruitsandberries", "general", 86, 0.9, 0, 10.5, 46));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Quince), "Fruitsandberries", "general", 87.5, 0.6, 0, 8.9, 38));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Plum), "Fruitsandberries", "general", 89, 0.2, 0, 7.4, 34));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pineapple), "Fruitsandberries", "general", 86, 0.4, 0, 11.8, 48));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Banana), "Fruitsandberries", "general", 74, 1.5, 0, 22.4, 91));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cherry), "Fruitsandberries", "general", 85.5, 0.8, 0, 11.3, 49));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pomegranate), "Fruitsandberries", "general", 85, 0.9, 0, 11.8, 52));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pear), "Fruitsandberries", "general", 87.5, 0.4, 0, 10.7, 42));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Fig), "Fruitsandberries", "general", 83, 0.7, 0, 13.9, 56));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kyzyl), "Fruitsandberries", "general", 85, 1, 0, 9.7, 45));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peach), "Fruitsandberries", "general", 86.5,  0.9, 0, 10.4, 44));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_RowanGarden), "Fruitsandberries", "general", 81, 1.4, 0, 12.5, 58));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_RowanAronia), "Fruitsandberries", "general", 80.5, 1.5, 0, 12, 54));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PlumGarden), "Fruitsandberries", "general", 87, 0.8, 0, 9.9, 43));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Dates), "Fruitsandberries", "general", 20, 2.5, 0, 72.1, 281));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Persimmon), "Fruitsandberries", "general", 81.5, 0.5, 0, 15.9, 62));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Merry), "Fruitsandberries", "general", 85, 1.1, 0, 12.3, 52));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mulberry), "Fruitsandberries", "general", 82.7, 0.7, 0, 12.7, 53));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Apple), "Fruitsandberries", "general", 86.5, 0.4, 0, 11.3, 46));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Orange), "Fruitsandberries", "general", 87.5, 0.9, 0, 8.4, 38));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Grapefruit), "Fruitsandberries", "general", 89, 0.9, 0, 7.3, 35));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lemon), "Fruitsandberries", "general", 87.7, 0.9, 0, 3.6, 31));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mandarin), "Fruitsandberries", "general", 88.5, 0.8, 0, 8.6, 38));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cranberry), "Fruitsandberries", "general", 87, 0.7, 0, 8.6, 40));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Grapes), "Fruitsandberries", "general", 80.2, 0.4, 0, 17.5, 69));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Blueberries), "Fruitsandberries", "general", 88.2, 1, 0, 7.7, 37));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Blackberry), "Fruitsandberries", "general", 88, 2, 0, 5.3, 33));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Strawberry), "Fruitsandberries", "general", 84.5, 1.8, 0, 8.1, 41));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Gooseberry), "Fruitsandberries", "general", 85, 0.7, 0, 9.9, 44));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Raspberry), "Fruitsandberries", "general", 87, 0.8, 0, 9, 41));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Seabuckthorn), "Fruitsandberries", "general", 75, 0.9, 0, 5.5, 30));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_WhiteCurrant), "Fruitsandberries", "general", 86, 0.3, 0, 8.7, 39));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_RedCurrant), "Fruitsandberries", "general", 85.4, 0.6, 0, 8, 38));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BlackCurrant), "Fruitsandberries", "general", 85, 1.0, 0, 8.0, 40));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rosehip), "Fruitsandberries", "general", 66, 1.6, 0, 24, 101));
                
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedApricots), "Driedfruits", "general", 20.2, 5.2, 0, 65.9, 272));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedFigs), "Driedfruits", "general", 18, 5, 0, 67.5, 278));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedRaisins), "Driedfruits", "general", 19, 1.8, 0, 70.9, 276));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedCherry), "Driedfruits", "general", 18, 1.5, 0, 73, 292));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPear), "Driedfruits", "general", 24, 2.3, 0, 62.1, 246));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPeaches), "Driedfruits", "general", 18, 3, 0, 68.5, 275));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedPrunes), "Driedfruits", "general", 25, 2.3, 0, 65.6, 264));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DriedApples), "Driedfruits", "general", 20, 3.2, 0, 68, 273));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beans), "Beans", "general", 83, 6, 0.1, 8.3, 58));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peas), "Beans", "general", 14, 23, 1.6, 57.7, 323));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Soybeans), "Beans", "general", 12, 34.9, 17.3, 26.5, 395));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Haricotbeans), "Beans", "general", 14, 22.3, 1.7, 54.5, 309));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lentils), "Beans", "general", 14, 24.8, 1.1, 53.7, 310));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Freshcep), "Mushrooms", "general", 89.9, 3.2, 0.7, 1.6, 25));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Driedcep), "Mushrooms", "general", 13, 27.6, 6.8, 10, 209));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Freshbirchbolete), "Mushrooms", "general", 91.6, 2.3, 0.9, 3.7, 31));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Driedbirchbolete), "Mushrooms", "general", 91.1, 3.3, 0.5, 3.4, 31));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Russule), "Mushrooms", "general", 83, 1.7, 0.3, 1.4, 17));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mutton), "Meatandpoultry", "general", 67.6, 16.3, 15.3, 0, 203));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beef), "Meatandpoultry", "general", 67.7, 18.9, 12.4, 0, 187));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Horse), "Meatandpoultry", "general", 72.5, 20.2, 7, 0, 143));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Rabbit), "Meatandpoultry", "general", 65.3, 20.7, 12.9, 0, 199));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Leanpork), "Meatandpoultry", "general", 54.8, 16.4, 27.8, 0, 316));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Veal), "Meatandpoultry", "general", 78, 19.7, 1.2, 0, 90));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambkidneys), "Meatandpoultry", "general", 79.7, 13.6, 2.5, 0, 77));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambliver), "Meatandpoultry", "general", 71.2, 18.7, 2.9, 0, 101));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lambheart), "Meatandpoultry", "general", 78.5, 13.5, 2.5, 0, 82));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefmarrow), "Meatandpoultry", "general", 78.9, 9.5, 9.5, 0, 124));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefliver), "Meatandpoultry", "general", 72.9, 17.4, 3.1, 0, 98));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefkidney), "Meatandpoultry", "general", 82.7, 12.5, 1.8, 0, 66));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefudder), "Meatandpoultry", "general", 72.6, 12.3, 13.7, 0, 173));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beefheart), "Meatandpoultry", "general", 79, 15, 3, 0, 87));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Beeftongue), "Meatandpoultry", "general", 71.2, 13.6, 12.1, 0, 163));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkkidneys), "Meatandpoultry", "general", 80.1, 13, 3.1, 0, 80));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkliver), "Meatandpoultry", "general", 71.4, 18.8, 3.6, 0, 108));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porkheart), "Meatandpoultry", "general", 78, 15.1, 3.2, 0, 89));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Porktongue), "Meatandpoultry", "general", 66.1, 14.2, 16.8, 0, 208));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Geese), "Meatandpoultry", "general", 49.7, 16.1, 33.3, 0, 364));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Turkey), "Meatandpoultry", "general", 64.5, 21.6, 12, 0.8, 197));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Chicken), "Meatandpoultry", "general", 68.9, 20.8, 8.8, 0.6, 165));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Duck), "Meatandpoultry", "general", 51.5, 16.5, 61.2, 0, 346));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDiabetic), "Sausageandsausageproducts", "general", 62.4, 12.1, 22.8, 0, 254));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDietary), "Sausageandsausageproducts", "general", 71.6, 12.1, 13.5, 0, 170));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageDoctorska), "Sausageandsausageproducts", "general", 60.8, 13.7, 22.8, 0, 260));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageLubitelska), "Sausageandsausageproducts", "general", 57, 12.2, 28, 0, 301));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageMolochna), "Sausageandsausageproducts", "general", 62.8, 11.7, 22.8, 0, 252));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CookedSausageVeal), "Sausageandsausageproducts", "general", 55, 12.5, 29.6, 0, 316));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PorkSausages), "Sausageandsausageproducts", "general", 53.7, 10.1, 31.6, 1.9, 332));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausagesMolochni), "Sausageandsausageproducts", "general", 60, 12.3, 25.3, 0, 277));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausagesRussian), "Sausageandsausageproducts", "general", 66.2, 12, 19.1, 0, 220));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_StewedBeef), "Meatandcannedmeat", "general", 63, 16.8, 18.3, 0, 232));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_TouristBreakfastBeef), "Meatandcannedmeat", "general", 66.9, 20.5, 10.4, 0, 176));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_TouristBreakfastPork), "Meatandcannedmeat", "general", 65.6, 16.9, 15.4 , 0, 206));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SausageStuffing), "Meatandcannedmeat", "general", 63.2, 15.2, 15.7, 2.8, 213));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_StewedPork), "Meatandcannedmeat", "general", 51.1, 14.9, 32.2, 0, 349));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SmokedBrisket), "Meatandcannedmeat", "general", 21, 7.6, 66.8, 0, 632));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Ham), "Meatandcannedmeat", "general", 53.5, 22.6, 20.9, 0, 279));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_ChickenEgg), "Eggs", "general", 74, 12.7, 11.5, 0.7, 157));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_EggPowder), "Eggs", "general", 6.8, 45, 37.3, 7.1, 542));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DryEggWhite), "Eggs", "general", 12.1, 73.3, 1.8, 7, 336));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DryEggYolk), "Eggs", "general", 5.4, 34.2, 52.2, 4.4, 623));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_QuailEggs), "Eggs", "general", 73.3, 11.9, 13.1, 0.6, 168));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bulls), "Fishandseafood", "general", 70.8, 12.8, 8.1, 5.2, 145));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PinkSalmon), "Fishandseafood", "general", 70.5, 21, 7, 0, 147));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Flounder), "Fishandseafood", "general", 79.5, 16.1, 2.6, 0, 88));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Karas), "Fishandseafood", "general", 78.9, 17.7, 1.8, 0, 87));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Carp), "Fishandseafood", "general", 79.1, 16, 3.6, 0, 96));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salmon), "Fishandseafood", "general", 71.3, 22, 5.6, 0, 138));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Bream), "Fishandseafood", "general", 77.7, 17.1, 4.1, 0, 105));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Lamprey), "Fishandseafood", "general", 75, 14.7, 11.9, 0, 166));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mintay), "Fishandseafood", "general", 80.1, 15.9, 0.7, 0, 70));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Capelin), "Fishandseafood", "general", 75, 13.4, 11.5, 0, 157));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Navaga), "Fishandseafood", "general", 81.1, 16.1, 1, 0, 73));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Burbot), "Fishandseafood", "general", 79.3, 18.8, 0.6, 0, 81));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_NototeniyaMarble), "Fishandseafood", "general", 73.4, 14.8, 10.7, 0, 156));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SeaPerch), "Fishandseafood", "general", 75.4, 17.6, 5.2, 0, 117));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_RiverPerch), "Fishandseafood", "general", 79.2, 18.5, 0.9, 0, 82));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sturgeon), "Fishandseafood", "general", 71.4, 16.4, 10.9, 0, 164));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Halibut), "Fishandseafood", "general", 76.9, 18.9, 3, 0, 103));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whiting), "Fishandseafood", "general", 81.3, 16.1, 0.9, 0, 72));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_FishSaber), "Fishandseafood", "general", 75.2, 20.3, 3.2, 0, 110));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_VimbaCaspian), "Fishandseafood", "general", 77, 19.2, 2.4, 0, 98));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SairaLarge), "Fishandseafood", "general", 59.8, 18.6, 20.8, 0, 262));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SairaSmall), "Fishandseafood", "general", 71.3, 20.4, 0.8, 0, 143));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Salak), "Fishandseafood", "general", 75.4, 17.3, 5.6, 0, 121));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Herring), "Fishandseafood", "general", 62.7, 17.7, 19.5, 0, 242));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Whitefish), "Fishandseafood", "general", 72.3, 19, 7.5, 0, 144));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Mackerel), "Fishandseafood", "general", 71.8, 18, 9, 0, 153));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Som), "Fishandseafood", "general", 75, 16.8, 8.5, 0, 144));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Scad), "Fishandseafood", "general", 74.9, 18.5, 5, 0, 119));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sterlet), "Fishandseafood", "general", 74.9, 17, 6.1, 0, 320));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sliver), "Fishandseafood", "general", 80.7, 17.5, 0.6, 0, 75));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Tuna), "Fishandseafood", "general", 74, 22.7, 0.7, 0, 96));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_EelMarine), "Fishandseafood", "general", 77.5, 19.1, 1.9, 0, 94));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Eel), "Fishandseafood", "general", 53.5, 14.5, 30.5, 0, 333));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Hake), "Fishandseafood", "general", 79.9, 16.6, 2.2, 0, 86));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pike), "Fishandseafood", "general", 70.4, 18.8, 0.7, 0, 82));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Elm), "Fishandseafood", "general", 80.1, 18.2, 0.3, 0, 117));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_ShrimpFarEast), "Fishandseafood", "general", 64.8, 28.7, 1.2, 0,134));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Kalmar), "Fishandseafood", "general", 80.3, 18, 0.3, 0, 75));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Crab), "Fishandseafood", "general", 81.5, 16, 0.5, 0, 69));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Shrimp), "Fishandseafood", "general", 77.5, 18, 0.8, 0, 83));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Seaweed), "Fishandseafood", "general", 88, 0.9, 0.2, 3, 5));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_KetyGranularCaviar), "Caviar", "general", 46.9, 31.6, 13.8, 0, 251));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BreamCaviar), "Caviar", "general", 58, 24.7, 4.8, 0, 142));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PollockRoe), "Caviar", "general", 63.2, 28.4, 1.9, 0, 131));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SturgeonGranularCaviar), "Caviar", "general", 58, 28.9, 9.7, 0, 203));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_SturgeonCaviar), "Caviar", "general", 39.5, 36, 10.2, 0, 123));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Hazelnut), "Nuts", "general", 4.8, 16.1, 66.9, 9.9, 704));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Almonds), "Nuts", "general", 4, 18.6, 57.7, 13.6, 645));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Walnut), "Nuts", "general", 5, 13.8, 61.3, 10.2, 648));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Peanuts), "Nuts", "general", 10, 26.3, 45.2, 9.7, 548));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sunflower), "Nuts", "general", 8, 20.7, 52.9, 5, 578));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Honey), "Sweets", "general", 17.2, 0.8, 0, 80.3, 308));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_FruitDrops), "Sweets", "general", 7, 3.7, 10.2, 73.1, 384));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Marshmallow), "Sweets", "general", 20, 0.8, 0, 78.3, 299));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Iris), "Sweets", "general", 6.5, 3.3, 7.5, 81.8, 387));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Marmalade), "Sweets", "general", 21, 0, 0.1, 77.7, 296));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Caramel), "Sweets", "general", 4.4, 0, 0.1, 77.7, 296));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CandyChocolatePoured), "Sweets", "general", 7.9, 2.9, 10.7, 76.6, 396));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Pastille), "Sweets", "general", 18, 0.5, 0, 80.4, 305));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Sugar), "Sweets", "general", 0.2, 0.3, 0, 99.5,  374));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Halva), "Sweets", "general", 2.9, 11.6, 29.7, 54, 516));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_DarkChocolate), "Sweets", "general", 0.8, 5.4, 35.3, 52.6, 540));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_MilkChocolate), "Sweets", "general", 0.9, 6.9, 35.7, 52.4, 547));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_WafflesFruitToppings), "Sweets", "general", 12, 3.2, 2.8, 80.1, 342));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_WafflesFattyToppings), "Sweets", "general", 1, 3.4, 30.2, 64.7, 530));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CakeCream), "Sweets", "general", 9, 5.4, 38.6, 46.4, 544));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CakeApple), "Sweets", "general", 13, 5.7, 25.6, 52.7, 454));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Gingerbread), "Sweets", "general", 14.5, 4.8, 2.8, 77.7, 336));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cake), "Sweets", "general", 25, 4.7, 20, 49.8, 386));

                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BlackTea), "Drinks", "general", 100, 0.1, 0, 0, 0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_GreenTea), "Drinks", "general", 100, 0, 0, 0, 0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_HibiscusTea), "Drinks", "general", 100, 20, 5.1, 4, 141));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_YellowTea), "Drinks", "general", 100, 20, 5.1, 6.9, 152));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CoffeeRoasted), "Drinks", "general", 100, 13.90, 14.40, 29.50, 331));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_InstantCoffee), "Drinks", "general", 100, 12.20, 0.50, 41.10, 241));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_GroundCoffee), "Drinks", "general", 100, 0.12, 0.02, 0.0, 1));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BlackCoffee), "Drinks", "general", 100, 0.2, 0.5, 0.2, 7));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Espresso), "Drinks", "general", 100, 0.12, 0.18, 0, 2));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Latte), "Drinks", "general", 100, 1.5, 1.4, 2, 29));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_FrozenCoffee), "Drinks", "general", 100, 4.0, 3.0, 19.0, 125));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Cappuccino), "Drinks", "general", 100, 1.7, 1.8, 2.6, 33));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_Americano), "Drinks", "general", 100, 0.6, 0.6, 0.7, 9.5));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_StrawberryCocktail), "Drinks", "general", 100, 2.0, 2.0, 14.0, 82.6));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BananaCocktail), "Drinks", "general", 100, 2.6, 2.4, 10.8, 72.9));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_VanillaCocktail), "Drinks", "general", 100, 9.0, 7.0, 71.0, 385.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_ChocolateCocktail), "Drinks", "general", 100, 10.0, 8.0, 70.0, 395.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PearJuice), "Drinks", "general", 100, 0.4, 0.3, 11.0, 46.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PruneJuice), "Drinks", "general", 100, 0.8, 0, 9.6, 39.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_LemonJuice), "Drinks", "general", 100, 0.9, 0.1, 3.0, 16.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CherryJuice), "Drinks", "general", 100, 0.7, 0, 10.2, 47.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_AppleJuice), "Drinks", "general", 100, 0.4, 0.4, 9.8, 42.0));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PineappleJuice), "Drinks", "general", 100, 0.3, 0.1, 11.4, 48));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_OrangeJuice), "Drinks", "general", 100, 0.9, 0.2, 8.1, 36));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BananaJuice), "Drinks", "general", 100, 0, 0, 12, 48));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_GrapefruitJuice), "Drinks", "general", 100, 0.9, 0.2, 6.5, 30));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_TomatoJuice), "Drinks", "general", 100, 1.1, 0.2, 3.8, 21));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CarrotJuice), "Drinks", "general", 100, 1.1, 0.1, 6.4, 28));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BeetJuice), "Drinks", "general", 100, 1.0, 0, 9.9, 42));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PumpkinJuice), "Drinks", "general", 100, 0, 0, 9, 38));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PlumСompote), "Drinks", "general", 100, 0.5, 0, 23.9, 96));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_CherryСompote), "Drinks", "general", 100, 0.6, 0, 24.5, 99));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PearСompote), "Drinks", "general", 100, 0.2, 0, 18.2, 70));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_AppleСompote), "Drinks", "general", 100, 0.2, 0, 22.1, 85));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_PeachСompote), "Drinks", "general", 100, 0.5, 0, 19.9, 78));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_ApricotСompote), "Drinks", "general", 100, 0.5, 0, 21.0, 85));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_GrapeСompote), "Drinks", "general", 100, 0.5, 0, 19.7, 77));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_TangerineСompote), "Drinks", "general", 100, 0.1, 0, 18.1, 69));
                DatabaseProducts.InsertProduct(new Product(Resources.GetString(Resource.String.product_BlackcurrantСompote), "Drinks", "general", 100, 0.3, 0.1, 13.9, 58));
            }

            //If the list doesn't exist yet, we creae it and fill it with products.
            if (DatabaseAlcohol.Init(Android.OS.Environment.GetExternalStoragePublicDirectory("databases") + "AlcoholData.db"))
            {
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_NonalcoholicBeer), "general", 0.5, 26));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_LightBeer), "general", 4.2, 42));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_DryWhiteWine), "general", 12, 66));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_DryRedWine), "general", 12, 75));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Сhampagne), "general", 12, 85));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_LiquorBaileys), "general", 17, 320));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_PortWine), "general", 20, 160));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Sambuca), "general", 40, 240));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Cognac), "general", 40, 242));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Tequila), "general", 40, 232));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Vodka), "general", 40, 229));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Whiskey), "general", 40, 220));
                DatabaseAlcohol.InsertAlcohol(new Alcohol(Resources.GetString(Resource.String.alcohol_Absinthe), "general", 60, 170));
            }

            //Current user, if not choosed.
            User.CurrentUser = -1;

            //Current date by defolt.
            ProductLists.CurrentDate = DateTime.Now;

            //Elements from layout.
            Button ChooseFromExistButton = FindViewById<Button>(Resource.Id.ChooseButton);
            Button ParametersListButton = FindViewById<Button>(Resource.Id.ParametersListButton);
            Button MyMenuButton = FindViewById<Button>(Resource.Id.MenuButton);
            Button AlcoholButton = FindViewById<Button>(Resource.Id.AlcoholButton);
            Button CigarettesButton = FindViewById<Button>(Resource.Id.CigarettesButton);
            Button AddUserItems = FindViewById<Button>(Resource.Id.AddUserItemsButton);

            //Actions on clicks.
            ChooseFromExistButton.Click += ChooseFromExistButton_Click;
            ParametersListButton.Click += ParametersListButton_Click;
            MyMenuButton.Click += MyMenuButton_Click;
            AlcoholButton.Click += AlcoholButton_Click;
            CigarettesButton.Click += CigarettesButton_Click;
            AddUserItems.Click += AddUserItems_Click;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /*Actions on clicks: description*/

        //Choosing from existing users in the system.
        private void ChooseFromExistButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PageUsers));
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
            StartActivity(typeof(PageMyMenu));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        private void AlcoholButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PageAlcohol));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        private void CigarettesButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PageCigarettes));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        private void AddUserItems_Click(object sender, EventArgs e)
        {
                //Creating a new layout for choosing the type of items that are going to be added.
                AlertDialog.Builder Object = new AlertDialog.Builder(this);
                LayoutInflater inflater = LayoutInflater.From(this);
                LinearLayout layout = new LinearLayout(this);
                View FormViewUserItems = inflater.Inflate(Resource.Layout.useritems_Add, layout);
                Object.SetView(FormViewUserItems);

                ListView ListForTypes = FormViewUserItems.FindViewById<ListView>(Resource.Id.ListForTypes);
                List<string> ListForNamesOfTypes = new List<string>();
                ListForNamesOfTypes.Add(Resources.GetString(Resource.String.Message_Products));
                ListForNamesOfTypes.Add(Resources.GetString(Resource.String.Message_Alcohol));

                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListForNamesOfTypes);
                ListForTypes.Adapter = adapter;

                ListForTypes.ItemClick += ListForTypes_ItemClick;

                //Action on pressing negative button.
                Object.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

                //Showing the new form for choosing the type of items that are going to be added.
                Object.Show();         
        }

        private void ListForTypes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Creating a new layout for adding new user products.
            AlertDialog.Builder Object_ = new AlertDialog.Builder(this);
            LayoutInflater inflater_ = LayoutInflater.From(this);
            LinearLayout layout_ = new LinearLayout(this);

            switch (e.Position)
            {
                case 0:
                    {
                        View FormViewUserItems = inflater_.Inflate(Resource.Layout.useritems_Product, layout_);
                        Object_.SetView(FormViewUserItems);

                        EditText ProductName = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductName);
                        EditText ProductProteins = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductProteins);
                        EditText ProductFats = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductFats);
                        EditText ProductCarbs = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductCarbs);
                        EditText ProductWater = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductWater);
                        EditText ProductCcals = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductCcals);
                        Spinner TypeChooser = FormViewUserItems.FindViewById<Spinner>(Resource.Id.TypeChooser);
                        CheckBox PrivateChooser = FormViewUserItems.FindViewById<CheckBox>(Resource.Id.PrivateChooser);

                        var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ProductLists.GetMajorList());
                        TypeChooser.Adapter = adapter;

                        Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                        {
                            String ForName = ProductName.Text;
                            String ForProteins = ProductProteins.Text;
                            String ForFats = ProductFats.Text;
                            String ForCarbs = ProductCarbs.Text;
                            String ForWater = ProductWater.Text;
                            String ForCcals = ProductCcals.Text;

                            ProductLists.SetChooser((int)TypeChooser.SelectedItemId);

                            ForProteins = ForProteins.Replace(".", ",");
                            ForFats = ForFats.Replace(".", ",");
                            ForCarbs = ForCarbs.Replace(".", ",");
                            ForWater = ForWater.Replace(".", ",");
                            ForCcals = ForCcals.Replace(".", ",");

                            if (!HelpclassDataValidation.CheckForLenth(ForName, 0, 20))
                                Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Name)), ToastLength.Long).Show();

                            else
                                 if ((!HelpclassDataValidation.CheckForLenth(ForProteins, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForProteins), 0, 100)) ||
                            (!HelpclassDataValidation.CheckForLenth(ForFats, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForFats), 0, 100)) ||
                            (!HelpclassDataValidation.CheckForLenth(ForCarbs, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForCarbs), 0, 100)) ||
                            (!HelpclassDataValidation.CheckForLenth(ForWater, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForWater), 0, 100)) ||
                            (!HelpclassDataValidation.CheckForLenth(ForCcals, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForCcals), 0, 1000)))
                                Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Amount)), ToastLength.Long).Show();

                            else
                            {
                                bool IsExisting = false;

                                //Checking if there is already a product with such name in our list.
                                foreach (Product TempProduct in DatabaseProducts.SQConnectionProduct.Table<Product>())
                                    if (TempProduct.Name.ToUpper() == ForName.ToUpper()) IsExisting = true;

                                if (!IsExisting)
                                {
                                    String temp = "general";
                                    if (PrivateChooser.Checked) temp = DatabaseUser.GetUser(User.CurrentUser).Name;

                                    if (User.CurrentUser == -1 && temp != "general")
                                    {
                                        Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();
                                    }

                                    else
                                    {
                                        DatabaseProducts.InsertProduct(new Product(ForName, ProductLists.Chooser, temp, double.Parse(ForProteins), double.Parse(ForFats), double.Parse(ForCarbs), double.Parse(ForWater), double.Parse(ForCcals)));
                                    }
                                }

                                else Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_AlreadyInSystem), ToastLength.Long).Show();
                            }

                        }));

                        Object_.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));
                    }
                    break;

                case 1:
                    {
                        View FormViewUserItems = inflater_.Inflate(Resource.Layout.useritems_Alcohol, layout_);
                        Object_.SetView(FormViewUserItems);

                        EditText ProductName = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductName);
                        EditText ProductAlcohols = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductAlcohols);
                        EditText ProductCcals = FormViewUserItems.FindViewById<EditText>(Resource.Id.ProductCcals);
                        CheckBox PrivateChooser = FormViewUserItems.FindViewById<CheckBox>(Resource.Id.PrivateChooser);

                        Object_.SetPositiveButton(Resource.String.OK, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1)
                        {
                            String ForName = ProductName.Text;
                            String ForAlcohols = ProductAlcohols.Text;
                            String ForCcals = ProductCcals.Text;

                            ForAlcohols = ForAlcohols.Replace(".", ",");
                            ForCcals = ForCcals.Replace(".", ",");

                            if (!HelpclassDataValidation.CheckForLenth(ForName, 0, 20))
                                Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Name)), ToastLength.Long).Show();

                            else
                                 if ((!HelpclassDataValidation.CheckForLenth(ForAlcohols, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForAlcohols), 0, 100)) ||
                            (!HelpclassDataValidation.CheckForLenth(ForCcals, 0, 7)) || (!HelpclassDataValidation.CheckForValue(double.Parse(ForCcals), 0, 1000)))
                                Toast.MakeText(this, HelpclassDataValidation.RequestToCorrectEnter(Resources.GetString(Resource.String.other_Amount)), ToastLength.Long).Show();

                            else
                            {
                                bool IsExisting = false;

                                //Checking if there is already a product with such name in our list.
                                foreach (Alcohol TempAlcohol in DatabaseAlcohol.SQConnectionAlcohol.Table<Alcohol>())
                                    if (TempAlcohol.Name.ToUpper() == ForName.ToUpper()) IsExisting = true;

                                if (!IsExisting)
                                {
                                    String temp = "general";
                                    if (PrivateChooser.Checked) temp = DatabaseUser.GetUser(User.CurrentUser).Name;

                                    if (User.CurrentUser == -1 && temp != "general")
                                    {
                                        Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_Unchoosed), ToastLength.Long).Show();
                                    }

                                    else
                                    {
                                        DatabaseAlcohol.InsertAlcohol(new Alcohol(ForName, temp, double.Parse(ForAlcohols), double.Parse(ForCcals)));
                                    }
                                }

                                else Toast.MakeText(this, Resources.GetString(Resource.String.ErrorMessage_AlreadyInSystem), ToastLength.Long).Show();
                            }

                        }));

                        Object_.SetNegativeButton(Resource.String.Cancel, new EventHandler<DialogClickEventArgs>(delegate (object Sender, DialogClickEventArgs e1) { }));

                    }
                    break;

            }

            Object_.Show();
        }
        }
    }