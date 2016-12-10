package mono.com.syncfusion.calendar;


public class SfCalendar_DrawMonthCellListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.DrawMonthCellListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_DrawMonthCell:(Ljava/lang/Object;Lcom/syncfusion/calendar/MonthCell;)V:GetDrawMonthCell_Ljava_lang_Object_Lcom_syncfusion_calendar_MonthCell_Handler:Com.Syncfusion.Calendar.SfCalendar/IDrawMonthCellListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IDrawMonthCellListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_DrawMonthCellListenerImplementor.class, __md_methods);
	}


	public SfCalendar_DrawMonthCellListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_DrawMonthCellListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IDrawMonthCellListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void DrawMonthCell (java.lang.Object p0, com.syncfusion.calendar.MonthCell p1)
	{
		n_DrawMonthCell (p0, p1);
	}

	private native void n_DrawMonthCell (java.lang.Object p0, com.syncfusion.calendar.MonthCell p1);

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
