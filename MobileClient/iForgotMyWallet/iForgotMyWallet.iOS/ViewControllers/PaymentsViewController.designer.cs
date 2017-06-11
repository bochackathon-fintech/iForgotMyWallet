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
	[Register ("PaymentsViewController")]
	partial class PaymentsViewController
	{
		[Outlet]
		public UIKit.UILabel lblX { get; private set; }

		[Outlet]
		public UIKit.UILabel lblY { get; private set; }

		[Outlet]
		public UIKit.UILabel lblZ { get; private set; }

		[Outlet]
		public UIKit.UIButton payButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblX != null) {
				lblX.Dispose ();
				lblX = null;
			}

			if (lblY != null) {
				lblY.Dispose ();
				lblY = null;
			}

			if (lblZ != null) {
				lblZ.Dispose ();
				lblZ = null;
			}

			if (payButton != null) {
				payButton.Dispose ();
				payButton = null;
			}
		}
	}
}
