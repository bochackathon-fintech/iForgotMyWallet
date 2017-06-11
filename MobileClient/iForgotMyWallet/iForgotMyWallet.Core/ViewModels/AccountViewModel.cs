using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iForgotMyWallet.Core
{
	public class AccountViewModel : BaseViewModel
	{
		public IEnumerable<Account> GetAccounts() {
			IsBusy = true;

			IEnumerable<Account> ret = DataManager.Instance.CurrentSession.Accounts;

			IsBusy = false;

			return ret;
		}
	}
}
