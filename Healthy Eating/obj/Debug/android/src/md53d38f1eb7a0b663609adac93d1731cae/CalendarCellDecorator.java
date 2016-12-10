package md53d38f1eb7a0b663609adac93d1731cae;


public abstract class CalendarCellDecorator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.timessquare.CalendarCellDecorator
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_decorate:(Lcom/squareup/timessquare/CalendarCellView;Ljava/util/Date;)V:GetDecorate_Lcom_squareup_timessquare_CalendarCellView_Ljava_util_Date_Handler:Square.TimesSquare.ICalendarCellDecoratorInvoker, Square.AndroidTimesSquare\n" +
			"";
		mono.android.Runtime.register ("Square.TimesSquare.CalendarCellDecorator, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", CalendarCellDecorator.class, __md_methods);
	}


	public CalendarCellDecorator () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CalendarCellDecorator.class)
			mono.android.TypeManager.Activate ("Square.TimesSquare.CalendarCellDecorator, Square.AndroidTimesSquare, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void decorate (com.squareup.timessquare.CalendarCellView p0, java.util.Date p1)
	{
		n_decorate (p0, p1);
	}

	private native void n_decorate (com.squareup.timessquare.CalendarCellView p0, java.util.Date p1);

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
