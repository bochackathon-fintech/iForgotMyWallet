using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amoenus.PclTimer;

namespace iForgotMyWallet.Core
{
	public class ReceiveViewModel:BaseViewModel
	{

		public event EventHandler OnTransactionStatusChange = delegate { };

		public int PendingTransactionId;

		private PCLTimer TheTimer;

		int DelayTimeInSeconds = 1;

		public async Task<bool> SetPaymentRequest (string Amount , string Description)
		{

			if (string.IsNullOrEmpty (Amount) || string.IsNullOrEmpty (Description))
				return false;
			
			var acc = DataManager.Instance.GetCurrentAccount ();
			var user = DataManager.Instance.CurrentSession.CurrentUser;

			Transaction theTransaction = new Transaction ();
			theTransaction.Amount = Amount;
			theTransaction.Description = Description;
			theTransaction.Acc_id = acc.pkAccountId;
			theTransaction.Account_id = acc.pkBankAccountId;
			theTransaction.From = user.Name;
			theTransaction.UserId = "3";

			string responce = await DataManager.Instance.SetPaymentRequest (theTransaction);

			StatusResponce buf = JsonConvert.DeserializeObject<StatusResponce> (responce);


			int.TryParse (buf.response, out PendingTransactionId);

			if (PendingTransactionId == 0)
				return false;

			return true;
		}

		public void CheckTransaction ()
		{
			Task.Factory.StartNew (async () =>
			{
				string responce = await DataManager.Instance.GetPaymentStatus (PendingTransactionId);

				StatusResponce buf = JsonConvert.DeserializeObject<StatusResponce> (responce);

				int status;
				int.TryParse (buf.status, out status);

				if (status > 0)
					TransactionStatusTrigger ();
			});
		}

		public void StartTransactionAwaitProcess ()
		{
			TheTimer = new PCLTimer (new Action (CheckTransaction), DelayTimeInSeconds);

		}
		public void StopTransactionAwaitProcess ()
		{
			TheTimer.Dispose();
		}

		void TransactionStatusTrigger ()
		{

			var tmp = OnTransactionStatusChange;
			if (tmp != null)
				tmp (this, EventArgs.Empty);
		}

}
}
