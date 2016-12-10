package mono.com.syncfusion.calendar;


public class MonthViewSettings_inlineTappedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.MonthViewSettings.inlineTappedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInlineTapped:(Lcom/syncfusion/calendar/Inline;Landroid/view/View;)Landroid/view/View;:GetOnInlineTapped_Lcom_syncfusion_calendar_Inline_Landroid_view_View_Handler:Com.Syncfusion.Calendar.MonthViewSettings/IInlineTappedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.MonthViewSettings+IInlineTappedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", MonthViewSettings_inlineTappedListenerImplementor.class, __md_methods);
	}


	public MonthViewSettings_inlineTappedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MonthViewSettings_inlineTappedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.MonthViewSettings+IInlineTappedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public android.view.View onInlineTapped (com.syncfusion.calendar.Inline p0, android.view.View p1)
	{
		return n_onInlineTapped (p0, p1);
	}

	private native android.view.View n_onInlineTapped (com.syncfusion.calendar.Inline p0, android.view.View p1);

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
