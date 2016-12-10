package mono.com.syncfusion.calendar;


public class SfCalendar_DrawYearCellListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.DrawYearCellListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_DrawYearCell:(Ljava/lang/Object;Lcom/syncfusion/calendar/YearCell;)V:GetDrawYearCell_Ljava_lang_Object_Lcom_syncfusion_calendar_YearCell_Handler:Com.Syncfusion.Calendar.SfCalendar/IDrawYearCellListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IDrawYearCellListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_DrawYearCellListenerImplementor.class, __md_methods);
	}


	public SfCalendar_DrawYearCellListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_DrawYearCellListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IDrawYearCellListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void DrawYearCell (java.lang.Object p0, com.syncfusion.calendar.YearCell p1)
	{
		n_DrawYearCell (p0, p1);
	}

	private native void n_DrawYearCell (java.lang.Object p0, com.syncfusion.calendar.YearCell p1);

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
