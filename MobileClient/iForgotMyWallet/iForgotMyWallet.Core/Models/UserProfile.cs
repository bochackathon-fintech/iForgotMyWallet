using System;
using Newtonsoft.Json;
using SQLite;

namespace iForgotMyWallet.Core
{
	public class UserProfile: EntityBase<long>
	{
		private long id;

		[PrimaryKey, AutoIncrement]
		public new long Id {
			get { return id; }
			set { 
				base.Id = value; 
				id = value;}
		}



		private string name;

		[JsonProperty (PropertyName = "name")]
		public string Name {
			get { return name; }
			set {
				var oldValue = name;
				if (oldValue != value)
					name = value;
			}
		}

	}
}
