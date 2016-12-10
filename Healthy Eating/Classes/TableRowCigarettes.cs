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
    class TableRowCigarettes
    {
        public object TextForQuantity;
        public object TextForNicotine;

        /*Constructor*/
        public TableRowCigarettes(object textForQuantity, object textForNicotine)
        {
            TextForQuantity = textForQuantity;
            TextForNicotine = textForNicotine;
        }
    }
}