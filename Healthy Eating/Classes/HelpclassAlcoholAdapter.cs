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
    class HelpclassAlcoholAdapter : BaseAdapter<TableRowAlcohol>
    {
        public List<TableRowAlcohol> AlcoholRowList;
        Activity context;

        /*Constructor*/
        public HelpclassAlcoholAdapter(Activity context, List<TableRowAlcohol> items) : base() 
        {
            this.context = context;
            this.AlcoholRowList = items;
        }

        /*Functions*/
        public override TableRowAlcohol this[int position]
        {
            get
            {
                return AlcoholRowList[position];
            }
        }

        public override int Count
        {
            get
            {
                return AlcoholRowList.Count();
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
                view = context.LayoutInflater.Inflate(Resource.Layout.helpform_AlcoholRow, null);
            }

            view.FindViewById<EditText>(Resource.Id.NameText).Text = AlcoholRowList.ElementAt(position).TextForData.ToString();
            view.FindViewById<EditText>(Resource.Id.PercentageText).Text = AlcoholRowList.ElementAt(position).TextForPercentage.ToString();
            view.FindViewById<EditText>(Resource.Id.CcalsText).Text = AlcoholRowList.ElementAt(position).TextForCcals.ToString();

            return view;
        }
    }
}