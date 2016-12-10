package mono.com.syncfusion.calendar;


public class SfCalendar_DrawInlineListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.DrawInlineListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_DrawInlineView:(Ljava/lang/Object;Lcom/syncfusion/calendar/AppointmentItem;)V:GetDrawInlineView_Ljava_lang_Object_Lcom_syncfusion_calendar_AppointmentItem_Handler:Com.Syncfusion.Calendar.SfCalendar/IDrawInlineListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IDrawInlineListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_DrawInlineListenerImplementor.class, __md_methods);
	}


	public SfCalendar_DrawInlineListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_DrawInlineListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IDrawInlineListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void DrawInlineView (java.lang.Object p0, com.syncfusion.calendar.AppointmentItem p1)
	{
		n_DrawInlineView (p0, p1);
	}

	private native void n_DrawInlineView (java.lang.Object p0, com.syncfusion.calendar.AppointmentItem p1);

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
