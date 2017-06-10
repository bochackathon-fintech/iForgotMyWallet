using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iForgotMyWallet.Core
{
	public class WebServicesManager
	{

		internal DataBaseManager DBManager { get; set; }

		public async Task<IEnumerable<Transaction>> GetTransactionHistory ()
		{
			await Task.Delay (1500);

			List<Transaction> ret = new List<Transaction> ();

			//Fake Transaction
			await Task.Factory.StartNew (() => 
			{
				ret.Add (new Transaction () { pkTransactionId = 0, Amount = "90,33", To = "Groceries Store"});
				ret.Add (new Transaction () { pkTransactionId = 1,  Amount = "55", From = "Oliver Smith" });
				ret.Add (new Transaction () { pkTransactionId = 2,  Amount = "110", From = "George Nic" });
				ret.Add (new Transaction () { pkTransactionId = 3, Amount = "15", To = "Cinema Central" });
			});

			return ret;
		}

		public async Task<String> GetCurrentAccountBalance (int AccountId)
		{
			String ret = "€0.00";


			//Fake Transaction
			await Task.Factory.StartNew (() => 
			{
				ret = "€789.04";
			});

			return ret;
		}

		public async Task<IEnumerable<Account>> GetAccounts (int AccountId)
		{
			await Task.Delay (1500);

			List<Account> ret = new List<Account> ();

			//Fake Transaction
			await Task.Factory.StartNew (() => {
				ret.Add (new Account () { pkAccountId = 0, pkOwnerId = 0, OwnerName = "John Smith (Personal)", AccountType = (int)AccountType.PersonalSingle });
				ret.Add (new Account () { pkAccountId = 1, pkOwnerId = 0, OwnerName = "John Smith (Family)", AccountType = (int)AccountType.PersonalShared });
				ret.Add (new Account () { pkAccountId = 2, pkOwnerId = 1, OwnerName = "Plumbing Solutions LTD", AccountType = (int)AccountType.BusinessSingle });
				ret.Add (new Account () { pkAccountId = 3, pkOwnerId = 2, OwnerName = "Universal Real Estates", AccountType = (int)AccountType.BusinessShared });

			});

			return ret;
		}
	}

}
