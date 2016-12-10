package mono.com.squareup.timessquare;


public class CalendarPickerView_OnDateSelectedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.timessquare.CalendarPickerView.OnDateSelectedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDateSelected:(Ljava/util/Date;)V:GetOnDateSelected_Ljava_util_Date_Handler:Square.TimesSquare.CalendarPickerView/IOnDateSelectedListenerInvoker, Square.AndroidTimesSquare\n" +
			"n_onDateUnselected:(Ljava/util/Date;)V:GetOnDateUnselected_Ljava_util_Date_Handler:Square.TimesSquare.CalendarPickerView/IOnDateSelectedListenerInvoker, Square.AndroidTimesSquare\n" +
			"";
		mono.android.Runtime.register ("Square.TimesSquare.CalendarPickerView+IOnDateSelectedListenerImplementor, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", CalendarPickerView_OnDateSelectedListenerImplementor.class, __md_methods);
	}


	public CalendarPickerView_OnDateSelectedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CalendarPickerView_OnDateSelectedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Square.TimesSquare.CalendarPickerView+IOnDateSelectedListenerImplementor, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDateSelected (java.util.Date p0)
	{
		n_onDateSelected (p0);
	}

	private native void n_onDateSelected (java.util.Date p0);


	public void onDateUnselected (java.util.Date p0)
	{
		n_onDateUnselected (p0);
	}

	private native void n_onDateUnselected (java.util.Date p0);

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
