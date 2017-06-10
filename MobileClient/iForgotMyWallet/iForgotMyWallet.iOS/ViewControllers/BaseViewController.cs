using Foundation;
using System;
using UIKit;
using iForgotMyWallet.Core;
using BigTed;
using System.Diagnostics;

namespace iForgotMyWallet.iOS
{
	public abstract class BaseViewController<TViewModel> : UIViewController where TViewModel : BaseViewModel, new()
	{
		protected TViewModel ViewModel { get; private set; }

		public BaseViewController (IntPtr handle) : base (handle)
        {
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ViewModel = new TViewModel ();

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			RegisterEvents ();
		}


		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			UnregisterEvents ();
		}


		public virtual void RegisterEvents ()
		{
			ViewModel.IsBusyChanged -= ViewModel_IsBusyChanged;
			ViewModel.IsBusyChanged += ViewModel_IsBusyChanged;
		}

		public virtual void UnregisterEvents ()
		{
			ViewModel.IsBusyChanged -= ViewModel_IsBusyChanged;
		}

		#region Event Handlers
		void ViewModel_IsBusyChanged (object sender, EventArgs e)
		{
			if (ViewModel.IsBusy)
				ShowHUD ("Loading, please wait...");
			else
				HideHUD ();
		}
		#endregion


		#region Loading HUD
		public void ShowHUD (string Caption)
		{
			this.InvokeOnMainThread (() => {
				try {
					BTProgressHUD.Show (Caption);
				} catch (Exception e) {
					Debug.WriteLine (e);
				}
			});
		}

		public void HideHUD ()
		{
			this.InvokeOnMainThread (() => {
				try {
					BTProgressHUD.Dismiss ();
				} catch (Exception e) {
					Debug.WriteLine (e);
				}
			});
		}
		#endregion
	}
}
