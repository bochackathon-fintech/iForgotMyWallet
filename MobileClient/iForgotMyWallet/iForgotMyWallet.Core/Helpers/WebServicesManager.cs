using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace iForgotMyWallet.Core
{
	public abstract class WebServicesManager
	{
		internal ServiceHelper BOC_Service;

		internal ServiceHelper IForgot_Service;

		internal DataBaseManager DBManager { get; set; }

		public WebServicesManager ()
		{
			BOC_Service = new ServiceHelper (AppConfigs.BaseURI_BOC);
			IForgot_Service = new ServiceHelper (AppConfigs.BaseURI);
		}


		public async Task<IEnumerable<Transaction>> GetTransactionHistory ()
		{
			List<Transaction> ret = new List<Transaction> ();

			//Fake Transaction
			await Task.Factory.StartNew (() => 
			{
				ret.Add (new Transaction () { pkTransactionId = 0, Amount = "90.33", To = "Groceries Store"});
				ret.Add (new Transaction () { pkTransactionId = 1,  Amount = "55", From = "Oliver Smith" });
				ret.Add (new Transaction () { pkTransactionId = 2,  Amount = "110", From = "George Nic" });
				ret.Add (new Transaction () { pkTransactionId = 3, Amount = "15", To = "Cinema Central" });
			});

			return ret;
		}

		public async Task<string> SetPaymentRequest (Transaction transaction)
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "setPaymentRequest");
			request.AddParameter ("api", "1");
			request.AddParameter ("user_id",transaction.UserId);
			request.AddParameter ("acc_id", transaction.Acc_id);
			request.AddParameter ("account_id", transaction.Account_id);
			request.AddParameter ("description", transaction.Description);
			request.AddParameter ("amount", transaction.Amount);
			request.AddParameter ("name", transaction.From);


			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}

		public async Task<string> GetMostRecentPaymentRequest ()
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "getMostRecentPaymentRequest");
			request.AddParameter ("api", "1");

			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}

		public async Task<string> GetPaymentStatus (int paymentId)
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "getPaymentStatus");
			request.AddParameter ("api", "1");
			request.AddParameter ("payment_id", paymentId.ToString ());


			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}
		public async Task<string> SetPaymentRequestStatus (int paymentId,int status)
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "setPaymentRequestStatus");
			request.AddParameter ("api", "1");
			request.AddParameter ("payment_id", paymentId.ToString());
			request.AddParameter ("status",paymentId.ToString ());


			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}
		public async Task<String> GetAccounts (int UserId)
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "getAccounts");
			request.AddParameter ("api", "1");
			request.AddParameter ("user_id", UserId.ToString ());


			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}


		public async Task<String> GetUserData (int UserId)
		{
			var request = new RestRequest ("index.php", Method.POST);
			request.AddParameter ("method", "getUserData");
			request.AddParameter ("api", "1");
			request.AddParameter ("user_id", UserId.ToString());

			var response = await IForgot_Service.Web.Get (request);

			return response.Content;
		}

		public async Task<String> GetAccountDetail (Account Account)
		{
			string URL = string.Format ("banks/{0}/accounts/{1}/{2}/account","bda8eb884efcef7082792d45",Account.pkBankAccountId,Account.ViewId);

			var request = new RestRequest (URL, Method.GET);
			request.AddHeader ("Auth-Provider-Name", "01210900497700");
			request.AddHeader ("Auth-ID", "123456789");
			request.AddHeader ("Ocp-Apim-Subscription-Key", "8c8ba56b8e694753903f7856bfb67ffb");

			var response = await BOC_Service.Web.Get (request);

			return response.Content;
		}

		public async Task<String> MakePayment (Account account,string amount)
		{
			string URL = string.Format ("banks/{0}/accounts/{1}/make-transaction", "bda8eb884efcef7082792d45", account.pkBankAccountId);
			//var uri = "http://api.bocapi.net/v1/api/banks/{BANK_ID}/accounts/{ACCOUNT_ID}/make-transaction?" + queryString;

			var request = new RestRequest (URL, Method.POST);
			request.AddHeader ("Track-ID", "b214b7044e9111e7b114b234");
			request.AddHeader ("Auth-Provider-Name", "01210900497700");
			request.AddHeader ("Auth-ID", "123456789");
			request.AddHeader ("Ocp-Apim-Subscription-Key", "8c8ba56b8e694753903f7856bfb67ffb");

			string json = "{\n  \"to\": {\n    \"bank_id\": \"bda8eb884efcef7082792d45\",\n    \"account_id\": \"d978e467ea2b00b1483d2d33\"\n  },\n  \"value\": {\n    \"amount\": 10,\n    \"currency\": \"EUR\"\n  }";

			request.AddBody (json);

			try {
				var response = await BOC_Service.Web.Get (request);
			} catch (Exception e) 
			{
			}


			return "1";//response.Content;
		}
	}

}
