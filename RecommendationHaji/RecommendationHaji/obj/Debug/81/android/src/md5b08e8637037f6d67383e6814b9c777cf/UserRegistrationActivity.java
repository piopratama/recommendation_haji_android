package md5b08e8637037f6d67383e6814b9c777cf;


public class UserRegistrationActivity
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
		mono.android.Runtime.register ("RecommendationHaji.UserRegistrationActivity, RecommendationHaji", UserRegistrationActivity.class, __md_methods);
	}


	public UserRegistrationActivity ()
	{
		super ();
		if (getClass () == UserRegistrationActivity.class)
			mono.android.TypeManager.Activate ("RecommendationHaji.UserRegistrationActivity, RecommendationHaji", "", this, new java.lang.Object[] {  });
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