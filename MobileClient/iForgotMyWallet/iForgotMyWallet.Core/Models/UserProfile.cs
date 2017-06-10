using System;
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

	}
}
