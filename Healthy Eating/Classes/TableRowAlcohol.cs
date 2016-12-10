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

namespace Healthy_Eating.Classes
{
    class TableRowAlcohol
    {
        public object TextForData;
        public object TextForPercentage;
        public object TextForCcals;

        /*Constructor*/
        public TableRowAlcohol(object textForData, object textForPercentage, object textForCcals)
        {
            TextForData = textForData;
            TextForPercentage = textForPercentage;
            TextForCcals = textForCcals;
        }
    }
}