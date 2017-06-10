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
		
	}
}
