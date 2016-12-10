package mono.com.syncfusion.calendar;


public class CustomHorizontalViewScroller_OnScrollStoppedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.CustomHorizontalViewScroller.OnScrollStoppedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollStopped:()V:GetOnScrollStoppedHandler:Com.Syncfusion.Calendar.CustomHorizontalViewScroller/IOnScrollStoppedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.CustomHorizontalViewScroller+IOnScrollStoppedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", CustomHorizontalViewScroller_OnScrollStoppedListenerImplementor.class, __md_methods);
	}


	public CustomHorizontalViewScroller_OnScrollStoppedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomHorizontalViewScroller_OnScrollStoppedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.CustomHorizontalViewScroller+IOnScrollStoppedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void onScrollStopped ()
	{
		n_onScrollStopped ();
	}

	private native void n_onScrollStopped ();

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
