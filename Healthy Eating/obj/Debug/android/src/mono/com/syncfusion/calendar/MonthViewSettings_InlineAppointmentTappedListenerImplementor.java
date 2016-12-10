package mono.com.syncfusion.calendar;


public class MonthViewSettings_InlineAppointmentTappedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.MonthViewSettings.InlineAppointmentTappedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInlineAppointmentTapped:(Ljava/lang/Object;Ljava/util/Calendar;Lcom/syncfusion/calendar/CalendarInlineEvent;)V:GetOnInlineAppointmentTapped_Ljava_lang_Object_Ljava_util_Calendar_Lcom_syncfusion_calendar_CalendarInlineEvent_Handler:Com.Syncfusion.Calendar.MonthViewSettings/IInlineAppointmentTappedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.MonthViewSettings+IInlineAppointmentTappedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", MonthViewSettings_InlineAppointmentTappedListenerImplementor.class, __md_methods);
	}


	public MonthViewSettings_InlineAppointmentTappedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MonthViewSettings_InlineAppointmentTappedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.MonthViewSettings+IInlineAppointmentTappedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void onInlineAppointmentTapped (java.lang.Object p0, java.util.Calendar p1, com.syncfusion.calendar.CalendarInlineEvent p2)
	{
		n_onInlineAppointmentTapped (p0, p1, p2);
	}

	private native void n_onInlineAppointmentTapped (java.lang.Object p0, java.util.Calendar p1, com.syncfusion.calendar.CalendarInlineEvent p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
