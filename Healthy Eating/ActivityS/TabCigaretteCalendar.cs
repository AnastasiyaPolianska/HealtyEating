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
        //----------------------------------------------------------------------------------------------------------------------------------------------------

        //Elements from the layout.
        CalendarPickerView UserCalendar;
        List <DateTime> DatesUsingALcohol;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Calendar view.
            View Calendar = inflater.Inflate(Resource.Layout.tab_cigarette_Calendar, null);
            base.OnCreateView(inflater, container, savedInstanceState);

            UserCalendar = Calendar.FindViewById<CalendarPickerView>(Resource.Id.Calendar);

            //Elements from the layout.
            TimeSpan ChangeMinus = TimeSpan.FromDays(365);
            TimeSpan ChangePlus = TimeSpan.FromDays(365);
            DateTime Beginning = DateTime.Now - ChangeMinus;
            DateTime Ending = DateTime.Now + ChangePlus;

            //List with dates to be marked.
            DatesUsingALcohol = new List<DateTime>();

            //Adding dates to the list.
            foreach (Cigarette TempCigarette in DatabaseUser.GetUser(User.CurrentUser).Cigarettes)
                if (TempCigarette.Date >= Beginning) DatesUsingALcohol.Add(TempCigarette.Date);

            //Selecting dates.
            UserCalendar.Init(Beginning, Ending).WithSelectedDates(DateTime.Now).WithHighlightedDates(DatesUsingALcohol);
            
            return Calendar;
        }
    }
}