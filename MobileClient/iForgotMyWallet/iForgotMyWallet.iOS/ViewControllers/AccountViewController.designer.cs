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
	[Register ("AccountViewController")]
	partial class AccountViewController
	{
		[Outlet]
		UIKit.UITableView tableV { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tableV != null) {
				tableV.Dispose ();
				tableV = null;
			}
		}
	}
}
