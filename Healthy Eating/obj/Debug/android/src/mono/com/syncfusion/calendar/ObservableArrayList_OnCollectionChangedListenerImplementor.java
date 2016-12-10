package mono.com.syncfusion.calendar;


public class ObservableArrayList_OnCollectionChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.calendar.ObservableArrayList.OnCollectionChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAddAll:(I)V:GetOnAddAll_IHandler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onAddIndex:(ILjava/lang/Object;)V:GetOnAddIndex_ILjava_lang_Object_Handler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onAddObject:(Ljava/lang/Object;)V:GetOnAddObject_Ljava_lang_Object_Handler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onClearList:()V:GetOnClearListHandler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onRemoveIndex:(ILjava/lang/Object;)V:GetOnRemoveIndex_ILjava_lang_Object_Handler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onRemoveObject:(Ljava/lang/Object;)V:GetOnRemoveObject_Ljava_lang_Object_Handler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"n_onSetObject:(ILjava/lang/Object;)V:GetOnSetObject_ILjava_lang_Object_Handler:Com.Syncfusion.Calendar.ObservableArrayList/IOnCollectionChangedListenerInvoker, Syncfusion.SfCalendar.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Calendar.ObservableArrayList+IOnCollectionChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", ObservableArrayList_OnCollectionChangedListenerImplementor.class, __md_methods);
	}


	public ObservableArrayList_OnCollectionChangedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ObservableArrayList_OnCollectionChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Calendar.ObservableArrayList+IOnCollectionChangedListenerImplementor, Syncfusion.SfCalendar.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void onAddAll (int p0)
	{
		n_onAddAll (p0);
	}

	private native void n_onAddAll (int p0);


	public void onAddIndex (int p0, java.lang.Object p1)
	{
		n_onAddIndex (p0, p1);
	}

	private native void n_onAddIndex (int p0, java.lang.Object p1);


	public void onAddObject (java.lang.Object p0)
	{
		n_onAddObject (p0);
	}

	private native void n_onAddObject (java.lang.Object p0);


	public void onClearList ()
	{
		n_onClearList ();
	}

	private native void n_onClearList ();


	public void onRemoveIndex (int p0, java.lang.Object p1)
	{
		n_onRemoveIndex (p0, p1);
	}

	private native void n_onRemoveIndex (int p0, java.lang.Object p1);


	public void onRemoveObject (java.lang.Object p0)
	{
		n_onRemoveObject (p0);
	}

	private native void n_onRemoveObject (java.lang.Object p0);


	public void onSetObject (int p0, java.lang.Object p1)
	{
		n_onSetObject (p0, p1);
	}

	private native void n_onSetObject (int p0, java.lang.Object p1);

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
