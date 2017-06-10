using System;
using System.Collections.Generic;
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

		private DataManager () 
		{ 
			GeneralMethods = (IGeneralMethods)ServiceContainer.Resolve (typeof (IGeneralMethods));// inversion of control for General Methods Interface;

			DBManager = new DataBaseManager (GeneralMethods.GetDatabasePath ());
			DBManager.Reindex ();
		}


	}
}
