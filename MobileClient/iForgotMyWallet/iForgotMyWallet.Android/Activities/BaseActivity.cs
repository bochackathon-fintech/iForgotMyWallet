
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using ThemedDialog = Android.Support.V7.App.AlertDialog;
using iForgotMyWallet.Core;
using AndroidHUD;

namespace iForgotMyWallet.Android
{
	[Activity]
	public abstract class BaseActivity <TViewModel> : AppCompatActivity where TViewModel : BaseViewModel, new()
	{
		protected TViewModel ViewModel { get; private set; }

		public Context _Context;

		public Toolbar toolbar;

		#region Life Cycle
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);


			ViewModel = new TViewModel ();
			_Context = this;

		}

		protected override void OnResume ()
		{
			base.OnResume ();
			RegisterEvents ();
		}

		protected override void OnPause ()
		{
			UnregisterEvents ();
			base.OnPause ();
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			UnregisterEvents ();
		}


		public virtual void InitInterfaceViews (int Layout)
		{
			SetContentView (Layout);

			toolbar = FindViewById<Toolbar> (Resource.Id.Toolbar);
			SetSupportActionBar (toolbar);

			toolbar.Title = "iForgotMyWallet";

		}

		public virtual void RegisterEvents ()
		{
			ViewModel.IsBusyChanged += ViewModel_IsBusyChanged;
		}

		public virtual void UnregisterEvents ()
		{
			ViewModel.IsBusyChanged -= ViewModel_IsBusyChanged;
		}
		#endregion


		#region Fragment Managment

		public void InflateFragment<T> () where T : Fragment, new()
		{
			FragmentTransaction ft = SupportFragmentManager.BeginTransaction ();

			RemoveFragments (ft);

			Fragment fragment = new T ();

			SupportFragmentManager.BeginTransaction ().Add (Resource.Id.FragmentContainer, fragment).CommitNow ();
		}

		public void InflateFragment (Fragment fragment)
		{
			FragmentTransaction ft = SupportFragmentManager.BeginTransaction ();

			RemoveFragments (ft);

			SupportFragmentManager.BeginTransaction ().Add (Resource.Id.FragmentContainer, fragment).CommitNow ();
		}

		public void RemoveFragments (FragmentTransaction ft)
		{
			if (SupportFragmentManager.Fragments != null)
				foreach (Fragment item in SupportFragmentManager.Fragments)
					ft.Remove (item).CommitNow ();

		}

		public int FragmentsCount ()
		{
			var tmp = SupportFragmentManager.Fragments;

			if (tmp != null && tmp.Any ())
				return tmp.Count;

			return 0;
		}

		public Fragment GetActiveFragment ()
		{
			return SupportFragmentManager.Fragments.Last ();
		}
		#endregion


		#region Loading HUD
		public void ShowHUD (string Caption)
		{
			RunOnUiThread (() => {
				try {
					AndHUD.Shared.Show (_Context, Caption, -1, MaskType.Clear);
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			});
		}

		public void HideHUD ()
		{
			RunOnUiThread (() => {
				try {
					if (AndHUD.Shared.CurrentDialog != null)
						AndHUD.Shared.CurrentDialog.Hide ();
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			});
		}
		#endregion
		#region Event Handlers
		void ViewModel_IsBusyChanged (object sender, EventArgs e)
		{
			if (ViewModel.IsBusy)
				ShowHUD ("Loading, please wait...");
			else
				HideHUD ();
		}
		#endregion

		#region Alerts
		public void ShowAlertOK (String Message)
		{
			ThemedDialog.Builder alert = new ThemedDialog.Builder (_Context);
			alert.SetTitle ("Menu Pick");
			alert.SetMessage (Message);

			alert.SetPositiveButton ("OK", (senderAlert, args) => {
			});

			Dialog dialog = alert.Create ();
			dialog.Show ();

		}
		#endregion
	}
}
