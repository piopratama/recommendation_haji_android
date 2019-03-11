package md5b08e8637037f6d67383e6814b9c777cf;


public class FinishBookingActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("RecommendationHaji.FinishBookingActivity, RecommendationHaji", FinishBookingActivity.class, __md_methods);
	}


	public FinishBookingActivity ()
	{
		super ();
		if (getClass () == FinishBookingActivity.class)
			mono.android.TypeManager.Activate ("RecommendationHaji.FinishBookingActivity, RecommendationHaji", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
