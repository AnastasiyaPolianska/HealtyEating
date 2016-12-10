using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Square.TimesSquare;
using Healthy_Eating.Classes;

namespace Healthy_Eating.ActivityS
{
    public class TabCigaretteCalendar : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        CalendarPickerView UserCalendar;
        List <DateTime> DatesUsingALcohol;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View Calendar = inflater.Inflate(Resource.Layout.tab_cigarette_Calendar, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            UserCalendar = Calendar.FindViewById<CalendarPickerView>(Resource.Id.Calendar);

            TimeSpan ChangeMinus = TimeSpan.FromDays(365);
            TimeSpan ChangePlus = TimeSpan.FromDays(365);
            DateTime Beginning = DateTime.Now - ChangeMinus;
            DateTime Ending = DateTime.Now + ChangePlus;

            DatesUsingALcohol = new List<DateTime>();

            foreach (Cigarette TempCigarette in DatabaseUser.GetUser(User.CurrentUser).Cigarettes)
                if (TempCigarette.Date >= Beginning) DatesUsingALcohol.Add(TempCigarette.Date);

            UserCalendar.Init(Beginning, Ending).WithSelectedDates(DateTime.Now).WithHighlightedDates(DatesUsingALcohol);
            

            return Calendar;
        }
    }
}