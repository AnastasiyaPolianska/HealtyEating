package mono.com.syncfusion.calendar;


public class SfCalendar_SelectionChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.SfCalendar.SelectionChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDateSelected:(Ljava/lang/Object;Ljava/util/ArrayList;Ljava/util/ArrayList;)V:GetOnDateSelected_Ljava_lang_Object_Ljava_util_ArrayList_Ljava_util_ArrayList_Handler:Com.Syncfusion.Calendar.SfCalendar/ISelectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.SfCalendar+ISelectionChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfCalendar_SelectionChangedListenerImplementor.class, __md_methods);
	}


	public SfCalendar_SelectionChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfCalendar_SelectionChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.SfCalendar+ISelectionChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void onDateSelected (java.lang.Object p0, java.util.ArrayList p1, java.util.ArrayList p2)
	{
		n_onDateSelected (p0, p1, p2);
	}

	private native void n_onDateSelected (java.lang.Object p0, java.util.ArrayList p1, java.util.ArrayList p2);

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
