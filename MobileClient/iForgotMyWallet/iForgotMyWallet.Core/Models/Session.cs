using System;
using System.Collections;
using System.Collections.Generic;

namespace iForgotMyWallet.Core
{
	public class Session
	{
		public UserProfile CurrentUser { get; set; }

		public IEnumerable<Account> Accounts { get; set; }

		public Account ActiveAccount { get; internal set; }

	}
}
