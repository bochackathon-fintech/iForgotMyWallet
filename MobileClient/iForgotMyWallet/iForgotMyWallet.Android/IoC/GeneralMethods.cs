﻿using System;
using iForgotMyWallet.Core;

namespace iForgotMyWallet.Android
{
	public class GeneralMethods:IGeneralMethods
	{
		public string GetDatabasePath ()
		{
			string folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			return System.IO.Path.Combine (folder, AppConfigs.DatabaseName);
		}
	}
}
