// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iForgotMyWallet.iOS
{
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UILabel ActiveWalletLabel { get; set; }

		[Outlet]
		UIKit.UIImageView profileImg { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.UILabel WalletBallanceLabel { get; set; }

		[Outlet]
		UIKit.UILabel WelcomeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActiveWalletLabel != null) {
				ActiveWalletLabel.Dispose ();
				ActiveWalletLabel = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (WalletBallanceLabel != null) {
				WalletBallanceLabel.Dispose ();
				WalletBallanceLabel = null;
			}

			if (WelcomeLabel != null) {
				WelcomeLabel.Dispose ();
				WelcomeLabel = null;
			}

			if (profileImg != null) {
				profileImg.Dispose ();
				profileImg = null;
			}
		}
	}
}
