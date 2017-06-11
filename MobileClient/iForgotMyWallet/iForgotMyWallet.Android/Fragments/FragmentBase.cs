
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using iForgotMyWallet.Core;

using ThemedDialog = Android.Support.V7.App.AlertDialog;
using Fragment = Android.Support.V4.App.Fragment;

namespace iForgotMyWallet.Android
{
	public abstract class FragmentBase<TViewModel> : Fragment where TViewModel : BaseViewModel, new()
	{
		protected TViewModel ViewModel { get; private set; }

		//public AlphaAnimation fadeIn = new AlphaAnimation (0.0f, 1.0f);

		#region LifeCycle
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			ViewModel = new TViewModel ();

			//fadeIn.Duration = 1200;
			//fadeIn.FillAfter = true;

			RegisterEvents ();

		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			InitInterfaceViews ();
		}
		#endregion

		public virtual void InitInterfaceViews ()
		{

		}
		public virtual void RegisterEvents ()
		{
			ViewModel.IsBusyChanged += ViewModel_IsBusyChanged;
		}

		public virtual void UnregisterEvents ()
		{
			ViewModel.IsBusyChanged -= ViewModel_IsBusyChanged;
		}

		public override void OnDestroy ()
		{
			base.OnDestroy ();
			UnregisterEvents ();
		}

		#region HUD
		public void ShowHUD (string Caption)
		{
			Activity.RunOnUiThread (() => {
				try {
					AndHUD.Shared.Show (Activity, Caption, -1, MaskType.Clear);
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			});
		}

		public void HideHUD ()
		{
			Activity.RunOnUiThread (() => {
				try {
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
			ThemedDialog.Builder alert = new ThemedDialog.Builder (Activity);
			alert.SetTitle ("MenuPick");
			alert.SetMessage (Message);

			alert.SetPositiveButton ("OK", (senderAlert, args) => {
			});

			Dialog dialog = alert.Create ();
			dialog.Show ();
		}

		#endregion
	}
}
