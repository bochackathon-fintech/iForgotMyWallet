using System;
namespace iForgotMyWallet.Core
{
	public class BaseViewModel:IDisposable
	{
		public event EventHandler IsBusyChanged = delegate { };

		private bool isBusy = false;

		public bool IsBusy {
			get { return isBusy; }
			set {
				isBusy = value;

				var tmp = IsBusyChanged;
				if (tmp != null)
					tmp (this, EventArgs.Empty);
			}
		}

		public void Dispose ()
		{
			
		}
	}
}
