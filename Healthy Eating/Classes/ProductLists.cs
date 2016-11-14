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
using Android.Content.Res;

namespace Healthy_Eating.Classes.ProductsRange
{
    class ProductLists
    {
        static public string Chooser { get; set; }
        static public int Counter { get; set; }
        static public DateTime CurrentDate { get; set; }

        //Major list of types of products.
        static public List<string> GetMajorList()
        {
            List<string> Temp = new List<string>();

            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Dairyproducts));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Fatsmargarinebutter));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Breadandbakeryproducts));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Cereals));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Vegetables));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Fruitsandberries));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Driedfruits));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Beans));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Mushrooms));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Meatandpoultry));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Sausageandsausageproducts));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Meatandcannedmeat));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Eggs));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Fishandseafood));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Caviar));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Nuts));
            Temp.Add(Application.Context.Resources.GetString(Resource.String.producttype_Sweets));

            return Temp;
        }

        static public void SetChooser(int n)
        {
            switch(n)
            {
                case 0:
                    Chooser = "Dairyproducts";
                    break;

                case 1:
                    Chooser = "Fatsmargarinebutter";
                    break;

                case 2:
                    Chooser = "Breadandbakeryproducts";
                    break;

                case 3:
                    Chooser = "Cereals";
                    break;

                case 4:
                    Chooser = "Vegetables";
                    break;

                case 5:
                    Chooser = "Fruitsandberries";
                    break;

                case 6:
                    Chooser = "Driedfruits";
                    break;

                case 7:
                    Chooser = "Beans";
                    break;

                case 8:
                    Chooser = "Mushrooms";
                    break;

                case 9:
                    Chooser = "Meatandpoultry";
                    break;

                case 10:
                    Chooser = "Sausageandsausageproducts";
                    break;

                case 11:
                    Chooser = "Meatandcannedmeat";
                    break;

                case 12:
                    Chooser = "Eggs";
                    break;

                case 13:
                    Chooser = "Fishandseafood";
                    break;

                case 14:
                    Chooser = "Caviar";
                    break;

                case 15:
                    Chooser = "Nuts";
                    break;

                case 16:
                    Chooser = "Sweets";
                    break;
            }
        }
    }
}