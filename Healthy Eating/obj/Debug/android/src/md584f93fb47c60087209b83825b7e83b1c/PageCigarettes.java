package md584f93fb47c60087209b83825b7e83b1c;


public class PageCigarettes
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onStop:()V:GetOnStopHandler\n" +
			"";
		mono.android.Runtime.register ("Healthy_Eating.PageCigarettes, Healthy Eating, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PageCigarettes.class, __md_methods);
	}


	public PageCigarettes () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PageCigarettes.class)
			mono.android.TypeManager.Activate ("Healthy_Eating.PageCigarettes, Healthy Eating, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onStop ()
	{
		n_onStop ();
	}

	private native void n_onStop ();

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
