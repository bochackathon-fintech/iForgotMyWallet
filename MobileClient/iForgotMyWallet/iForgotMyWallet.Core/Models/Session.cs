using System;
using System.Collections;
using System.Collections.Generic;

namespace iForgotMyWallet.Core
{
	public class Session
	{
		UserProfile CurrentUser { get; set; }
		List<IEnumerable> Accounts { get; set;}
	}
}
