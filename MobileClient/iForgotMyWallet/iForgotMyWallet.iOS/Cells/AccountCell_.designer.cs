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
	[Register ("AccountCell")]
	partial class AccountCell
	{
		[Outlet]
		public UIKit.UILabel accountName { get; private set; }

		[Outlet]
		public UIKit.UILabel ibalLabel { get; private set; }

		[Outlet]
		public UIKit.UIImageView img { get; set; }

		[Outlet]
		public UIKit.UILabel sharedLabel { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (accountName != null) {
				accountName.Dispose ();
				accountName = null;
			}

			if (ibalLabel != null) {
				ibalLabel.Dispose ();
				ibalLabel = null;
			}

			if (sharedLabel != null) {
				sharedLabel.Dispose ();
				sharedLabel = null;
			}

			if (img != null) {
				img.Dispose ();
				img = null;
			}
		}
	}
}
