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
    class HelpclassCigarettesAdapter : BaseAdapter<TableRowCigarettes>
    {
        public List<TableRowCigarettes> CigarettesRowList;
        Activity context;

        /*Constructor*/
        public HelpclassCigarettesAdapter(Activity context, List<TableRowCigarettes> items) : base() 
        {
            this.context = context;
            this.CigarettesRowList = items;
        }

        /*Functions*/
        public override TableRowCigarettes this[int position]
        {
            get
            {
                return CigarettesRowList[position];
            }
        }

        public override int Count
        {
            get
            {
                return CigarettesRowList.Count();
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
                view = context.LayoutInflater.Inflate(Resource.Layout.helpform_CigarettesRow, null);
            }

            view.FindViewById<EditText>(Resource.Id.QuantityText).Text = CigarettesRowList.ElementAt(position).TextForQuantity.ToString();
            view.FindViewById<EditText>(Resource.Id.NicotineText).Text = CigarettesRowList.ElementAt(position).TextForNicotine.ToString();

            return view;
        }
    }
}