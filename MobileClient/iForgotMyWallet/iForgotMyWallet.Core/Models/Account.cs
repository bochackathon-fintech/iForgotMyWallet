using System;
using SQLite;

namespace iForgotMyWallet.Core
{
	public class Account:EntityBase<long>
	{
		[PrimaryKey]
		public new long Id {
			get { return pkAccountId; }
			set { pkAccountId = value; }
		}

		private long _pkAccountId;

		public long pkAccountId {
			get { return _pkAccountId; }
			set {
				var oldValue = _pkAccountId;
				if (oldValue != value) {
					_pkAccountId = value;
					base.Id = value;
				}
			}
		}


		private int accountType;

		public int AccountType {
			get { return accountType; }
			set {
				var oldValue = accountType;
				if (oldValue != value)
					accountType = value;
			}
		}

		private String ownerName;

		public String OwnerName {
			get { return ownerName; }
			set {
				var oldValue = ownerName;
				if (oldValue != value)
					ownerName = value;
			}
		}

		private long _pkOwnerId;

		public long pkOwnerId {
			get { return _pkOwnerId; }
			set {
				var oldValue = _pkOwnerId;
				if (oldValue != value) {
					_pkOwnerId = value;
					base.Id = value;
				}
			}
		}

	}
}
