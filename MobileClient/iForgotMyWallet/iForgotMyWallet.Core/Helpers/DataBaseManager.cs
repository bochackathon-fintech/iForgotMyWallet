using System;
using SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
namespace iForgotMyWallet.Core
{
	public class DataBaseManager: SQLiteConnection
	{
		public DataBaseManager (string path) : base (path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true)
		{
			CreateTables ();
		}
		public void CreateTables ()
		{
				CreateTable<UserProfile> (CreateFlags.AutoIncPK);
				CreateTable<Transaction> (CreateFlags.None);
		}

		public void DropTables ()
		{
			DropTable<UserProfile> ();
			DropTable<Transaction> ();

			Reindex ();
		}
		/// <summary>
		/// Reindex this database instance.
		/// </summary>
		public void Reindex ()
		{
			try {
				Execute ("REINDEX;");
				Execute ("VACUUM;");

			} catch (Exception ex) {
				Debug.WriteLine (ex);
			}
		}
	}
}
