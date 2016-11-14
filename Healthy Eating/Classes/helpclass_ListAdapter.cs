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
    class helpclass_ListAdapter : BaseAdapter<tablerow_Parameters>
    {
        public List<tablerow_Parameters> ParametersRowList;
        Activity context;

        /*Constructor*/
        public helpclass_ListAdapter(Activity context, List<tablerow_Parameters> items) : base() 
        {
            this.context = context;
            this.ParametersRowList = items;
        }

        /*Functions*/
        public override tablerow_Parameters this[int position]
        {
            get
            {
                return ParametersRowList[position];
            }
        }

        public override int Count
        {
            get
            {
                return ParametersRowList.Count();
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
                view = context.LayoutInflater.Inflate(Resource.Layout.helpform_Row, null);
            }

            view.FindViewById<EditText>(Resource.Id.DateText).Text = ParametersRowList.ElementAt(position).TextForData.ToString();
            view.FindViewById<EditText>(Resource.Id.WeightText).Text = ParametersRowList.ElementAt(position).TextForWeight.ToString();
            view.FindViewById<EditText>(Resource.Id.HeightText).Text = ParametersRowList.ElementAt(position).TextForHeight.ToString();
            view.FindViewById<EditText>(Resource.Id.BMIText).Text = ParametersRowList.ElementAt(position).TextForBMI.ToString();

            return view;
        }
    }
}