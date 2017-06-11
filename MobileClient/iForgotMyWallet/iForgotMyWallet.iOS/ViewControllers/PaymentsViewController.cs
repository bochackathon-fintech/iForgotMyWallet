using Foundation;
using System;
using UIKit;
using CoreMotion;

namespace iForgotMyWallet.iOS
{
	public partial class PaymentsViewController : UIViewController
	{
		CMMotionManager motionManager;
		bool isInitialXPosition;
		float initialXPosition;
		public PaymentsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad() {
			isInitialXPosition = false;
			initialXPosition = 0;

		    motionManager = new CMMotionManager();
			motionManager.AccelerometerUpdateInterval = 0.1;
				payButton.TouchUpInside += (object sender, EventArgs e) => {
				payAction();
    		};

		}

		private void payAction()
		{
			isInitialXPosition = true;
			motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
			{
				if (isInitialXPosition)
				{
					isInitialXPosition = false;
					initialXPosition = (float)data.Acceleration.X;
				}
				float difference = Math.Abs((float)data.Acceleration.X - initialXPosition);
				//Console.WriteLine("Difference from initial X position = " + difference);
				if ((float)data.Acceleration.X > 0.8 || (float)data.Acceleration.X < -0.8)
				{
					Console.WriteLine("Bumped");
					stopMonitoring();
					//Create Alert
					var okAlertController = UIAlertController.Create("Payment", "Payment Found", UIAlertControllerStyle.Alert);

					//Add Action
					okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));

				// Present Alert
				PresentViewController(okAlertController, true, null);
				}
				this.lblX.Text = data.Acceleration.X.ToString("0.00000000");
				this.lblY.Text = data.Acceleration.Y.ToString("0.00000000");
				this.lblZ.Text = data.Acceleration.Z.ToString("0.00000000");
			});
		}

		private void stopMonitoring() {
			motionManager.StopAccelerometerUpdates();
		}
	}
}