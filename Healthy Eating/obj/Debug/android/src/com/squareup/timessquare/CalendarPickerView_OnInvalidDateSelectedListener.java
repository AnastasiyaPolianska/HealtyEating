package com.squareup.timessquare;


public abstract class CalendarPickerView_OnInvalidDateSelectedListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.timessquare.CalendarPickerView.OnInvalidDateSelectedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInvalidDateSelected:(Ljava/util/Date;)V:GetOnInvalidDateSelected_Ljava_util_Date_Handler:Square.TimesSquare.CalendarPickerView/IOnInvalidDateSelectedListenerInvoker, Square.AndroidTimesSquare\n" +
			"";
		mono.android.Runtime.register ("Square.TimesSquare.CalendarPickerView+OnInvalidDateSelectedListener, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", CalendarPickerView_OnInvalidDateSelectedListener.class, __md_methods);
	}


	public CalendarPickerView_OnInvalidDateSelectedListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CalendarPickerView_OnInvalidDateSelectedListener.class)
			mono.android.TypeManager.Activate ("Square.TimesSquare.CalendarPickerView+OnInvalidDateSelectedListener, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onInvalidDateSelected (java.util.Date p0)
	{
		n_onInvalidDateSelected (p0);
	}

	private native void n_onInvalidDateSelected (java.util.Date p0);

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
