using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iForgotMyWallet.Core
{
	public  class  HomeViewModel:BaseViewModel
	{

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

			List<Account> ret = new List<Account> ();
			try {
				
				var responceJSON = await DataManager.Instance.GetAccounts (3);

				var accountsJSON = JObject.Parse (responceJSON).GetValue ("accounts").ToString(); 
				JArray array = JArray.Parse (accountsJSON);

				foreach (JObject content in array.Children<JObject> ()) {
					var buf = JsonConvert.DeserializeObject<Account> (content.ToString());
					ret.Add (buf);
				}

			} catch (Exception e) {
				Debug.WriteLine (e);
			}

			DataManager.Instance.CurrentSession.Accounts = ret;

			IsBusy = false;


			return ret;
		}

	

		public Account GetCurrentAccountBalance ()
		{
			Account ret = new Account();

			try {
				ret = DataManager.Instance.CurrentSession.ActiveAccount;
			} catch (Exception e) {
				Debug.WriteLine (e);
			}
			return ret;
		}

		public async Task<Account> GetCurrentAccountDetails ()
		{
			IsBusy = true;

			Account ret = new Account ();
			try {

				Account acc = DataManager.Instance.GetCurrentAccount ();

				if (acc != null)
				{
					var responceJSON = await DataManager.Instance.GetAccountDetail (acc);
					Account acc_iban = JsonConvert.DeserializeObject<Account> (responceJSON);

					var balanceJSON = JObject.Parse (responceJSON).GetValue ("balance").ToString ();
					Account balance = JsonConvert.DeserializeObject<Account> (balanceJSON);

					acc.IBAN = acc_iban.IBAN;
					acc.AccountBalance = balance.AccountBalance;
				}

				ret = acc;

			} catch (Exception e) {
				Debug.WriteLine (e);

			}

			IsBusy = false;
			return ret;
		}

		public async Task<UserProfile> GetUserDetails ()
		{
			IsBusy = true;

			UserProfile ret = new UserProfile ();
			try {

				var responceJson = await DataManager.Instance.GetUserData (3);
				ret = JsonConvert.DeserializeObject<UserProfile> (responceJson);

				DataManager.Instance.CurrentSession.CurrentUser = ret;

			} catch (Exception e) {
				Debug.WriteLine (e);

			}

			IsBusy = false;

			return ret;
		}


		public async Task<UserProfile> GetAccountData ()
		{
			IsBusy = true;

			UserProfile ret = new UserProfile();

			try {
				await GetAccounts ();
				await GetCurrentAccountDetails ();
				ret = await GetUserDetails ();


			} catch (Exception e) {
				Debug.WriteLine (e);
			}

			IsBusy = false;


			return ret;
		}
	}
}
