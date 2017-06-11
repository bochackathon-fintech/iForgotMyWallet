using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iForgotMyWallet.Core
{
	public class DataManager:WebServicesManager
	{
		private static DataManager instance;


		public static DataManager Instance {
			get {
				if (instance == null)
					instance = new DataManager ();

				return instance;
			}
		}


		private IGeneralMethods GeneralMethods;

		public Session CurrentSession = new Session ();

		private DataManager () 
		{
			GeneralMethods = (IGeneralMethods)ServiceContainer.Resolve (typeof (IGeneralMethods));// inversion of control for General Methods Interface;

			DBManager = new DataBaseManager (GeneralMethods.GetDatabasePath ());
			DBManager.Reindex ();
		}



		public Account GetCurrentAccount ()
		{
			if (CurrentSession == null || CurrentSession.Accounts == null)
				return null;
			
			var match = CurrentSession.Accounts.SingleOrDefault (x => x.IsActive);

			if (match == null) {
				match = CurrentSession.Accounts.First ();
				match.IsActive = true;
			}

			return match;
		}
	}
}
