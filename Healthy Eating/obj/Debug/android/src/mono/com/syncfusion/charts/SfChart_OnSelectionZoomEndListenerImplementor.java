package mono.com.syncfusion.charts;


public class SfChart_OnSelectionZoomEndListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.charts.SfChart.OnSelectionZoomEndListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnSelectionZoomEndListener:(Lcom/syncfusion/charts/SfChart;Lcom/syncfusion/charts/ChartSelectionZoomEvent;)V:GetOnSelectionZoomEndListener_Lcom_syncfusion_charts_SfChart_Lcom_syncfusion_charts_ChartSelectionZoomEvent_Handler:Com.Syncfusion.Charts.SfChart/IOnSelectionZoomEndListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Charts.SfChart+IOnSelectionZoomEndListenerImplementor, Syncfusion.SfChart.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfChart_OnSelectionZoomEndListenerImplementor.class, __md_methods);
	}


	public SfChart_OnSelectionZoomEndListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfChart_OnSelectionZoomEndListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Charts.SfChart+IOnSelectionZoomEndListenerImplementor, Syncfusion.SfChart.Android, Version=14.3451.0.49, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void OnSelectionZoomEndListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartSelectionZoomEvent p1)
	{
		n_OnSelectionZoomEndListener (p0, p1);
	}

	private native void n_OnSelectionZoomEndListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartSelectionZoomEvent p1);

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
