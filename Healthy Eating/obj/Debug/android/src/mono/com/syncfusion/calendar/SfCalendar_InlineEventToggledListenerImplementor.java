package mono.com.syncfusion.calendar;


public class SfCalendar_InlineEventToggledListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.InlineEventToggledListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_InlineToggled:(Ljava/lang/Object;Lcom/syncfusion/calendar/CalendarEventCollection;)V:GetInlineToggled_Ljava_lang_Object_Lcom_syncfusion_calendar_CalendarEventCollection_Handler:Com.Syncfusion.Calendar.SfCalendar/IInlineEventToggledListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IInlineEventToggledListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_InlineEventToggledListenerImplementor.class, __md_methods);
	}


	public SfCalendar_InlineEventToggledListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_InlineEventToggledListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IInlineEventToggledListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void InlineToggled (java.lang.Object p0, com.syncfusion.calendar.CalendarEventCollection p1)
	{
		n_InlineToggled (p0, p1);
	}

	private native void n_InlineToggled (java.lang.Object p0, com.syncfusion.calendar.CalendarEventCollection p1);

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
