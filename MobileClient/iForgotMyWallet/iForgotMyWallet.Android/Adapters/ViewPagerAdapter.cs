using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;

namespace iForgotMyWallet.Android
{
	public class ViewPagerAdapter : FragmentStatePagerAdapter
	{
		public Fragment CurrentJobFragment { get; set; }

		public IEnumerable<Fragment> Items { get; set; }

		string [] tabTitles = { "Home", "Account" , "Send" , "Receive" };

		readonly Context context;

		public ViewPagerAdapter (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{

		}

		public ViewPagerAdapter (Context context, FragmentManager fm) : base (fm)
		{
			this.context = context;
		}

		public override int Count {
			get { return tabTitles.Length; }
		}

		public override int GetItemPosition (Java.Lang.Object itemObject)
		{
			return PositionNone;
		}

		public override Fragment GetItem (int position)
		{
			
			return Items.ElementAt(position);

		}

		public override ICharSequence GetPageTitleFormatted (int position)
		{
			// Generate title based on item position
			return CharSequence.ArrayFromStringArray (tabTitles) [position];
		}

		public View GetTabView (int position)
		{
			var tv = (TextView)LayoutInflater.From (context).Inflate (Resource.Layout.Tab_Item, null);
			tv.Text = tabTitles [position];
			return tv;
		}
	}
}
