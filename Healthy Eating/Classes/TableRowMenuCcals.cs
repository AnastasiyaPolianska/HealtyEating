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
    class TableRowMenuCcals
    {
        public object TextForName;
        public object TextForQuantity;
        public object TextForWater;
        public object TextForCcals;

        /*Constructor*/
        public TableRowMenuCcals(object textForName, object textForQuantity, object textForWater, object textForCcals)
        {
            TextForName = textForName;
            TextForQuantity = textForQuantity;
            TextForWater = textForWater;
            TextForCcals = textForCcals;
        }
    }
}