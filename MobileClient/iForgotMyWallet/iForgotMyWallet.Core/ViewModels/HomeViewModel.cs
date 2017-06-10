using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iForgotMyWallet.Core
{
	public  class  HomeViewModel:BaseViewModel
	{
		public HomeViewModel ()
		{
			
		}


		public async Task<IEnumerable<Transaction>> GetTransactionHistory ()
		{
			IsBusy = true;

			IEnumerable<Transaction> ret = new List<Transaction>();
			try {
				ret = await DataManager.Instance.GetTransactionHistory ();
			} catch (Exception e) {
				Debug.WriteLine (e);
			}

			IsBusy = false;

			return ret;
		}

		public async Task<IEnumerable<Account>> GetAccounts ()
		{
			IsBusy = true;

			IEnumerable<Account> ret = new List<Account> ();
			try {
				ret = await DataManager.Instance.GetAccounts (0);
			} catch (Exception e) {
				Debug.WriteLine (e);
			}

			IsBusy = false;

			return ret;
		}

		public async Task<String> GetCurrentAccountBalance ()
		{
			IsBusy = true;

			String ret = "€0.00";
			try {
				ret = await DataManager.Instance.GetCurrentAccountBalance (0);
			} catch (Exception e) {
				Debug.WriteLine (e);
			}

			IsBusy = false;

			return ret;
		}
	}
}
