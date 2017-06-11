
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using iForgotMyWallet.Core;
using Fragment = Android.Support.V4.App.Fragment;

namespace iForgotMyWallet.Android
{
	public class FragmentHome : FragmentBase<HomeViewModel>
	{
		TextView UserNameTextView, AccountName , AccountBalance;
		HistoryAdapter adapter { get; set; }
		RecyclerView lstView;
		RecyclerView.LayoutManager _layoutManager;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.FragmentHome, container, false);
		}
		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			InitInterfaceViews ();

			GetCurrentAccountData ();
			LoadDataToHistoryList ();

		}
		public override void InitInterfaceViews ()
		{
			base.InitInterfaceViews ();
			UserNameTextView= Activity.FindViewById<TextView> (Resource.Id.NameLabel);
			AccountName= Activity.FindViewById<TextView> (Resource.Id.AccountNameText);
			AccountBalance= Activity.FindViewById<TextView> (Resource.Id.BalanceLabel);
			lstView = Activity.FindViewById<RecyclerView> (Resource.Id.listView1);


			_layoutManager = new LinearLayoutManager (Context, LinearLayoutManager.Vertical, false);

			if (lstView != null)
				lstView.SetLayoutManager (_layoutManager);
		}


		public void GetCurrentAccountData ()
		{
			AccountBalance.Text = "Acquiring balance";

			Task.Factory.StartNew (async () => {
				try {
					UserProfile user = await ViewModel.GetAccountData ();
					Activity.RunOnUiThread (() => UpdateUserDetails (user));
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			});
		}

		public void UpdateUserDetails (UserProfile user)
		{
			Account curAccount = DataManager.Instance.GetCurrentAccount ();
			AccountBalance.Text = curAccount.AccountBalance;
			AccountName.Text = curAccount.Nickname;
			UserNameTextView.Text = user.Name;
		}

		public void LoadDataToHistoryList ()
		{

			Task.Factory.StartNew (async () => {
			try {
				var ret = await ViewModel.GetTransactionHistory ();
				adapter = new HistoryAdapter (ret.ToList ());
					Activity.RunOnUiThread (() => lstView.SetAdapter( adapter));
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			});

		}
	}
}
