using Foundation;
using System;
using UIKit;
using iForgotMyWallet.Core;
using System.Collections.Generic;
using System.Linq;

namespace iForgotMyWallet.iOS {
    public partial class AccountViewController :  BaseViewController<AccountViewModel> {
		AccountSource Source { get; set;}

		public AccountViewController (IntPtr handle) : base (handle) {
        }

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			tableV.TableFooterView = new UIView();
			List<Account> accountList =ViewModel.GetAccounts().ToList();
			Source = new AccountSource(accountList);
			tableV.Source = Source;
		}
    }
}