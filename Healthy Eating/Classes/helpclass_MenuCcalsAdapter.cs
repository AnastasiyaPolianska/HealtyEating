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
    class helpclass_MenuCcalsAdapter : BaseAdapter<tablerow_MenuCcals>
    {
        public List<tablerow_MenuCcals> MenuRowList;
        Activity context;

        /*Constructor*/
        public helpclass_MenuCcalsAdapter(Activity context, List<tablerow_MenuCcals> items) : base() 
        {
            this.context = context;
            this.MenuRowList = items;
        }

        /*Functions*/
        public override tablerow_MenuCcals this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.helpform_MenuRowCcals, null);
            }

            view.FindViewById<EditText>(Resource.Id.NameText).Text = MenuRowList.ElementAt(position).TextForName.ToString();
            view.FindViewById<EditText>(Resource.Id.QuantityText).Text = MenuRowList.ElementAt(position).TextForQuantity.ToString();
            view.FindViewById<EditText>(Resource.Id.WaterText).Text = MenuRowList.ElementAt(position).TextForWater.ToString();
            view.FindViewById<EditText>(Resource.Id.CcalsText).Text = MenuRowList.ElementAt(position).TextForCcals.ToString();

            return view;
        }
    }
}