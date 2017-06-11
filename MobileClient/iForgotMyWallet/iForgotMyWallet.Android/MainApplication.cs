
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iForgotMyWallet.Core;

namespace iForgotMyWallet.Android
{
	[Application (Theme = "@style/AppTheme")]
	public class MainApplication : Application
	{
		public MainApplication (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{

			// Create your application here
		}

		public override void OnCreate ()
		{
			base.OnCreate ();


			AppDomain.CurrentDomain.UnhandledException += (o, e) => {

				#if DEBUG
					Debugger.Break ();
				#endif
			};


			TaskScheduler.UnobservedTaskException += (o, e) => {
				#if DEBUG
					Debugger.Break ();
				#endif
			};

			//Configure the SQlite as Serialized to be thread safe
			SQLitePCL.Batteries.Init ();
			var result = SQLitePCL.raw.sqlite3_config (SQLitePCL.raw.SQLITE_CONFIG_SERIALIZED);
			if (result != SQLitePCL.raw.SQLITE_OK) {
				System.Diagnostics.Debug.WriteLine ($"Unable to set SQLIte in Serialized mode (error code = {result})");
			}

			//Set the SQlitePCL provider (Android)
			SQLitePCL.raw.SetProvider (new SQLitePCL.SQLite3Provider_e_sqlite3 ());


			//Inject the Classes that have platform specific methods
			ServiceContainer.Register<IGeneralMethods> (() => new GeneralMethods ());

		}
	}
}
