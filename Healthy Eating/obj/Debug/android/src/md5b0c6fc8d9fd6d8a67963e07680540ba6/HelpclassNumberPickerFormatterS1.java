package md5b0c6fc8d9fd6d8a67963e07680540ba6;


public class HelpclassNumberPickerFormatterS1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.widget.NumberPicker.Formatter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_format:(I)Ljava/lang/String;:GetFormat_IHandler:Android.Widget.NumberPicker/IFormatterInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Healthy_Eating.Classes.HelpclassNumberPickerFormatterS1, Healthy Eating, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HelpclassNumberPickerFormatterS1.class, __md_methods);
	}


	public HelpclassNumberPickerFormatterS1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HelpclassNumberPickerFormatterS1.class)
			mono.android.TypeManager.Activate ("Healthy_Eating.Classes.HelpclassNumberPickerFormatterS1, Healthy Eating, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String format (int p0)
	{
		return n_format (p0);
	}

	private native java.lang.String n_format (int p0);

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
