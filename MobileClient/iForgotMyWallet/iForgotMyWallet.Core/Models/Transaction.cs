using System;
using SQLite;

namespace iForgotMyWallet.Core
{
	public class Transaction: EntityBase<long>
	{
		[PrimaryKey]
		public new long Id {
			get { return pkTransactionId; }
			set { pkTransactionId= value; }
		}

		private long _pkTransactionId;

		public long pkTransactionId {
			get { return _pkTransactionId; }
			set {
				var oldValue = _pkTransactionId;
				if (oldValue != value) {
					_pkTransactionId = value;
					base.Id = value;
				}
			}
		}


		private string from;

		public string From {
			get { return from; }
			set {
				var oldValue = from;
				if (oldValue != value)
					from = value;
			}
		}


		private string to;

		public string To {
			get { return to; }
			set {
				var oldValue = to;
				if (oldValue != value) 
					to = value;
			}
		}
		
	
		private string amount;

		public string Amount {
			get { return amount; }
			set {
				var oldValue = amount;
				if (oldValue != value)
					amount = value;
			}
		}


		private string description;

		public string Description {
			get { return description; }
			set {
				var oldValue = description;
				if (oldValue != value)
					description = value;
			}
		}

		private string userId;

		public string UserId {
			get { return userId; }
			set {
				var oldValue = userId;
				if (oldValue != value)
					userId = value;
			}
		}

		private string acc_id;

		public string Acc_id {
			get { return acc_id; }
			set {
				var oldValue = acc_id;
				if (oldValue != value)
					acc_id = value;
			}
		}


		private string account_id;

		public string Account_id {
			get { return account_id; }
			set {
				var oldValue = account_id;
				if (oldValue != value)
					account_id = value;
			}
		}
	}
}
