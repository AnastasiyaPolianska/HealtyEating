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
    class TableRowParameters
    {
        public object TextForData;
        public object TextForWeight;
        public object TextForHeight;
        public object TextForBMI;

        /*Constructor*/
        public TableRowParameters(object textForData, object textForWeight, object textForHeight, object textForBMI)
        {
            TextForData = textForData;
            TextForWeight = textForWeight;
            TextForHeight = textForHeight;
            TextForBMI = textForBMI;
        }
    }
}