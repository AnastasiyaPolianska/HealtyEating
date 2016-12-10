package mono.com.syncfusion.calendar;


public class SfCalendar_MonthChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.MonthChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_MonthChanged:(Ljava/lang/Object;Lcom/syncfusion/calendar/MonthListenerArgs;)V:GetMonthChanged_Ljava_lang_Object_Lcom_syncfusion_calendar_MonthListenerArgs_Handler:Com.Syncfusion.Calendar.SfCalendar/IMonthChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IMonthChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_MonthChangedListenerImplementor.class, __md_methods);
	}


	public SfCalendar_MonthChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_MonthChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IMonthChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void MonthChanged (java.lang.Object p0, com.syncfusion.calendar.MonthListenerArgs p1)
	{
		n_MonthChanged (p0, p1);
	}

	private native void n_MonthChanged (java.lang.Object p0, com.syncfusion.calendar.MonthListenerArgs p1);

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
