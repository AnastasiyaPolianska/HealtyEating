package mono.com.squareup.timessquare;


public class CalendarPickerView_CellClickInterceptorImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.timessquare.CalendarPickerView.CellClickInterceptor
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCellClicked:(Ljava/util/Date;)Z:GetOnCellClicked_Ljava_util_Date_Handler:Square.TimesSquare.CalendarPickerView/ICellClickInterceptorInvoker, Square.AndroidTimesSquare\n" +
			"";
		mono.android.Runtime.register ("Square.TimesSquare.CalendarPickerView+CellClickInterceptorImplementor, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", CalendarPickerView_CellClickInterceptorImplementor.class, __md_methods);
	}


	public CalendarPickerView_CellClickInterceptorImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CalendarPickerView_CellClickInterceptorImplementor.class)
			mono.android.TypeManager.Activate ("Square.TimesSquare.CalendarPickerView+CellClickInterceptorImplementor, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onCellClicked (java.util.Date p0)
	{
		return n_onCellClicked (p0);
	}

	private native boolean n_onCellClicked (java.util.Date p0);

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
