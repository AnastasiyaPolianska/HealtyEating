package mono.com.syncfusion.calendar;


public class SfCalendar_ViewModeChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.ViewModeChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_TypeChanged:(Ljava/lang/Object;Lcom/syncfusion/calendar/enums/ViewMode;)V:GetTypeChanged_Ljava_lang_Object_Lcom_syncfusion_calendar_enums_ViewMode_Handler:Com.Syncfusion.Calendar.SfCalendar/IViewModeChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+IViewModeChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_ViewModeChangedListenerImplementor.class, __md_methods);
	}


	public SfCalendar_ViewModeChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_ViewModeChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+IViewModeChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void TypeChanged (java.lang.Object p0, com.syncfusion.calendar.enums.ViewMode p1)
	{
		n_TypeChanged (p0, p1);
	}

	private native void n_TypeChanged (java.lang.Object p0, com.syncfusion.calendar.enums.ViewMode p1);

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
