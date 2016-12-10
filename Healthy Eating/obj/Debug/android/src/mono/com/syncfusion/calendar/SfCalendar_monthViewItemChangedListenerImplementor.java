package mono.com.syncfusion.calendar;


public class SfCalendar_monthViewItemChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.monthViewItemChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMonthViewItemChanged:(Lcom/syncfusion/calendar/MonthItem;)V:GetOnMonthViewItemChanged_Lcom_syncfusion_calendar_MonthItem_Handler:Com.Syncfusion.Calendar.SfCalendar/IMonthViewItemChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IMonthViewItemChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_monthViewItemChangedListenerImplementor.class, __md_methods);
	}


	public SfCalendar_monthViewItemChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_monthViewItemChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IMonthViewItemChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void onMonthViewItemChanged (com.syncfusion.calendar.MonthItem p0)
	{
		n_onMonthViewItemChanged (p0);
	}

	private native void n_onMonthViewItemChanged (com.syncfusion.calendar.MonthItem p0);

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
