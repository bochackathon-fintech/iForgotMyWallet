using Foundation;
using System;
using UIKit;
using iForgotMyWallet.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;

namespace iForgotMyWallet.iOS
{
    public partial class HomeViewController : BaseViewController<HomeViewModel>
    {
		TransactionHistorySource Source { get; set;}

		public HomeViewController (IntPtr handle) : base (handle)
        {
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			profileImg.ClipsToBounds = true;
			profileImg.Layer.BorderWidth = 1;
			profileImg.Layer.BorderColor =new CGColor(255f, 255f, 255f);
			profileImg.Layer.MasksToBounds = true;
			profileImg.Layer.CornerRadius = 40;

			Source = new TransactionHistorySource (new List<Transaction>());
			TableView.Source = Source;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			LoadDataToHistoryList ();
			GetCurrentAccountData ();
		}


		public void GetCurrentAccountData ()
		{
			WalletBallanceLabel.Text = "Acquiring balance";

			Task.Factory.StartNew (async () => {
				try {
					UserProfile user = await ViewModel.GetAccountData ();
					this.InvokeOnMainThread (() => UpdateUserDetails (user));
				} catch (Exception e) {
					Debug.WriteLine (e);
				}
			});
		}

		public void UpdateUserDetails (UserProfile user)
		{
			Account curAccount = DataManager.Instance.GetCurrentAccount ();
			WalletBallanceLabel.Text = curAccount.AccountBalance;
			WelcomeLabel.Text = user.Name;
			ActiveWalletLabel.Text = curAccount.Nickname;


		}

		public void LoadDataToHistoryList (){

			Task.Factory.StartNew (async () => 
			{
				try {
					var ret = await ViewModel.GetTransactionHistory ();
					Source.Items = ret.ToList ();
					this.InvokeOnMainThread (() => TableView.ReloadData ());
				} catch (Exception e) {
					Debug.WriteLine (e);
				}
			});

		}

    }
}