using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.PM;
using Android.Media;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using iForgotMyWallet.Core;
using Fragment = Android.Support.V4.App.Fragment;

namespace iForgotMyWallet.Android
{
	[Activity ( MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustPan)]
	public class MainActivity : BaseActivity<MainViewModel>
	{
		ViewPager PagerView;

		TabLayout tabLayout;

		ViewPagerAdapter PagerAdapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			InitInterfaceViews (Resource.Layout.ActivityMain);


		}

		public override void InitInterfaceViews (int Layout)
		{
			base.InitInterfaceViews (Layout);

			PagerView = FindViewById<ViewPager> (Resource.Id.pager);
			tabLayout = FindViewById<TabLayout> (Resource.Id.sliding_tabs);

			InitTabs ();
		}

		void InitTabs ()
		{
			PagerAdapter = new ViewPagerAdapter (this, SupportFragmentManager);

			List<Fragment> items = new List<Fragment> ();
			items.Add (new FragmentHome ());
			items.Add (new FragmentAccount ());
			items.Add (new FragmentSend ());
			items.Add (new FragmentReceive ());

			PagerAdapter.Items = items;

			PagerView.Adapter = PagerAdapter;
			tabLayout.SetupWithViewPager (PagerView); // Setup tablayout with view pager
		}
}
}

