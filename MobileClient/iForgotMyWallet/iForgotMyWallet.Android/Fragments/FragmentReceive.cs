
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using iForgotMyWallet.Core;
using Fragment = Android.Support.V4.App.Fragment;

namespace iForgotMyWallet.Android
{
	public class FragmentReceive : FragmentBase<ReceiveViewModel>
	{
		AlertDialog AwatingPaymentDialog;

		Button ConfirmButton;

		EditText AmountEditText, DescriptionEditText;

		Button OKButton;

		TextView Status;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.FragmentReceive, container, false);
		}

		public override void InitInterfaceViews ()
		{
			base.InitInterfaceViews ();

			ConfirmButton= Activity.FindViewById<Button> (Resource.Id.btnConfirm);
			AmountEditText= Activity.FindViewById<EditText> (Resource.Id.edAmount);
			DescriptionEditText= Activity.FindViewById<EditText> (Resource.Id.edDescription);

			ConfirmButton.Click += ConfirmButton_Click;

			ViewModel.OnTransactionStatusChange+= ViewModel_OnTransactionStatusChange;
		}

		async void ConfirmButton_Click (object sender, EventArgs e)
		{
			bool success = await ViewModel.SetPaymentRequest (AmountEditText.Text,DescriptionEditText.Text);

			if (success) {
				ViewModel.StartTransactionAwaitProcess ();

				ShowAwaitingPaymentDialog ();
			}
		}

		void ViewModel_OnTransactionStatusChange (object sender, EventArgs e)
		{

			Activity.RunOnUiThread (() => {
					OKButton.Text = "Done";
					Status.Text = "Payment Success!";

					ViewModel.StopTransactionAwaitProcess ();
				});
		}

		void ShowAwaitingPaymentDialog ()
		{
			var inputView = Activity.LayoutInflater.Inflate (Resource.Layout.AlertAwaitingPayment, null);
			 OKButton = inputView.FindViewById<Button> (Resource.Id.btnOKAlert);
			 Status = inputView.FindViewById<TextView> (Resource.Id.txtStatus);


			OKButton.Text = "Cancel";
			Status.Text = "Awaiting Payment....";
			AlertDialog.Builder builder = new AlertDialog.Builder (Activity);
			builder.SetCancelable (false);
			builder.SetView (inputView);

			AwatingPaymentDialog = builder.Create ();

			//Button OK
			OKButton.Click += (sender, e) => {
				AwatingPaymentDialog.Dismiss ();

			};

			AwatingPaymentDialog.Show ();

		}
		void HideAwaitingPaymentDialog ()
		{
			AwatingPaymentDialog.Dismiss ();

		}
	}
}
