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
using Java.Lang;

namespace Healthy_Eating.Classes
{
    class HelpclassMenuAdapter : BaseAdapter<TableRowMenu>
    {
        public List<TableRowMenu> MenuRowList;
        Activity context;

        /*Constructor*/
        public HelpclassMenuAdapter(Activity context, List<TableRowMenu> items) : base() 
        {
            this.context = context;
            this.MenuRowList = items;
        }

        /*Functions*/
        public override TableRowMenu this[int position]
        {
            get
            {
                return MenuRowList[position];
            }
        }

        public override int Count
        {
            get
            {
                return MenuRowList.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.helpform_MenuRow, null);
            }

            view.FindViewById<EditText>(Resource.Id.NameText).Text = MenuRowList.ElementAt(position).TextForName.ToString();
            view.FindViewById<EditText>(Resource.Id.ProteinText).Text = MenuRowList.ElementAt(position).TextForProteins.ToString();
            view.FindViewById<EditText>(Resource.Id.FatsText).Text = MenuRowList.ElementAt(position).TextForFats.ToString();
            view.FindViewById<EditText>(Resource.Id.CarbohydratesText).Text = MenuRowList.ElementAt(position).TextForCarbohydrates.ToString();

            return view;
        }
    }
}