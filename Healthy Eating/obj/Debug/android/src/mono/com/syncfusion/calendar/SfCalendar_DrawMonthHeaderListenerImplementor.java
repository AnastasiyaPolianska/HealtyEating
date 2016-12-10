package mono.com.syncfusion.calendar;


public class SfCalendar_DrawMonthHeaderListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.DrawMonthHeaderListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_DrawMonthHeader:(Ljava/lang/Object;Lcom/syncfusion/calendar/CalendarHeader;)V:GetDrawMonthHeader_Ljava_lang_Object_Lcom_syncfusion_calendar_CalendarHeader_Handler:Com.Syncfusion.Calendar.SfCalendar/IDrawMonthHeaderListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IDrawMonthHeaderListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_DrawMonthHeaderListenerImplementor.class, __md_methods);
	}


	public SfCalendar_DrawMonthHeaderListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_DrawMonthHeaderListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IDrawMonthHeaderListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void DrawMonthHeader (java.lang.Object p0, com.syncfusion.calendar.CalendarHeader p1)
	{
		n_DrawMonthHeader (p0, p1);
	}

	private native void n_DrawMonthHeader (java.lang.Object p0, com.syncfusion.calendar.CalendarHeader p1);

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
