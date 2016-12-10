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
    class TableRowMenu
    {
        public object TextForName;
        public object TextForProteins;
        public object TextForFats;
        public object TextForCarbohydrates;

        /*Constructor*/
        public TableRowMenu(object textForName, object textForProteins, object textForFats, object textForCarbohydrates)
        {
            TextForName = textForName;
            TextForProteins = textForProteins;
            TextForFats = textForFats;
            TextForCarbohydrates = textForCarbohydrates;
    }
    }
}