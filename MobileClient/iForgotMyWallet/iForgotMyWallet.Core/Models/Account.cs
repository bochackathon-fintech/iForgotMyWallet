using System;
using Newtonsoft.Json;
using SQLite;

namespace iForgotMyWallet.Core
{
	public class Account:EntityBase<string>
	{
		[PrimaryKey]
		public new string Id {
			get { return pkAccountId; }
			set { pkAccountId = value; }
		}

		private string _pkAccountId;

		[JsonProperty (PropertyName = "acc_id")]
		public string pkAccountId {
			get { return _pkAccountId; }
			set {
				var oldValue = _pkAccountId;
				if (oldValue != value) {
					_pkAccountId = value;
					base.Id = value;
				}
			}
		}

		private string _pkBankAccountId;

		[JsonProperty (PropertyName = "account_id")]
		public string pkBankAccountId {
			get { return _pkBankAccountId; }
			set {
				var oldValue = _pkBankAccountId;
				if (oldValue != value) {
					_pkBankAccountId = value;
					base.Id = value;
				}
			}
		}

		private int accountType;

		[JsonProperty (PropertyName = "account_type")]
		public int AccountType {
			get { return accountType; }
			set {
				var oldValue = accountType;
				if (oldValue != value)
					accountType = value;
			}
		}

		private String ownerName;

		[JsonProperty (PropertyName = "shared_name")]
		public String OwnerName {
			get { return ownerName; }
			set {
				var oldValue = ownerName;
				if (oldValue != value)
					ownerName = value;
			}
		}

		private String viewId;

		[JsonProperty (PropertyName = "view_id")]
		public String ViewId {
			get { return viewId; }
			set {
				var oldValue = viewId;
				if (oldValue != value)
					viewId = value;
			}
		}


		private string _pkAccountBalance;

		[JsonProperty (PropertyName = "amount")]
		public string AccountBalance {
			get { return _pkAccountBalance; }
			set {
				var oldValue = _pkAccountBalance;
				if (oldValue != value) {
					_pkAccountBalance = value;
					base.Id = value;
				}
			}
		}

		private string _IBAN;

		[JsonProperty (PropertyName = "IBAN")]
		public string IBAN {
			get { return _IBAN; }
			set {
				var oldValue = _IBAN;
				if (oldValue != value) {
					_IBAN = value;
					base.Id = value;
				}
			}
		}

		private string nickname;

		[JsonProperty (PropertyName = "nickname")]
		public string Nickname {
			get { return nickname; }
			set {
				var oldValue = nickname;
				if (oldValue != value) {
					nickname = value;
					base.Id = value;
				}
			}
		}
	}
}
