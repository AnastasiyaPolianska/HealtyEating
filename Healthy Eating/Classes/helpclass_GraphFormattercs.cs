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
using MikePhil.Charting.Components;
using MikePhil.Charting.Formatter;

namespace Healthy_Eating.Classes
{
    public class helpclass_GraphFormattercs : IAxisValueFormatter
    {
        string[] RezultingStrings;

        public helpclass_GraphFormattercs(string[] incomeStrings)
        {
            RezultingStrings = incomeStrings;
        }

        public int DecimalDigits { get; }

        public IntPtr Handle { get; }

        public void Dispose()
        {
        }

        public string GetFormattedValue(float value, AxisBase axis)
        {
            return RezultingStrings[(int)(value)];
        }
    }
}