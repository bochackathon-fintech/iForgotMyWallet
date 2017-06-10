using System;
using UIKit;
using iForgotMyWallet.Core;
using System.Collections.Generic;
using Foundation;
using System.Linq;
using System.Drawing;

namespace iForgotMyWallet.iOS
{
	public class TransactionHistorySource : UITableViewSource
	{
		public List<Transaction> Items  { get; set; }

		public String [] HeadersNames = { "Transaction History" };

		private const string CellIdentifier = "TransactionHistoryCell";

		public TransactionHistorySource (List<Transaction> Items)
		{
			this.Items = Items;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 70;
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (!Items.Any())
				return 0;

			return Items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			// if there are no cells to reuse, create a new one

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CellIdentifier);


			var model = Items.ElementAt (indexPath.Row);


			string Title = string.Empty;
			string SubTitle = string.Empty;

			if (!string.IsNullOrEmpty (model.To)) 
				Title = string.Format ("Money send to {0}",model.To);
			else
				Title = string.Format ("Money received from {0}", model.From);

			SubTitle = string.Format ("€{0}", model.Amount);

			cell.TextLabel.Text = Title;
			cell.DetailTextLabel.Text = SubTitle;

			return cell;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView headerView = new UIView (new RectangleF (0f, 0f, (float)tableView.Bounds.Size.Width, 35f));
			headerView.BackgroundColor = UIColor.FromRGB (231, 231, 231);

			UILabel label = new UILabel ();
			label.Opaque = false;
			label.TextColor = UIColor.Black;
			label.Font = UIFont.FromName ("Helvetica-Bold", 16f);
			label.Frame = new RectangleF (13, 7, 315, 20);
			label.Text = HeadersNames [section];
			headerView.AddSubview (label);


			return headerView;
		}
		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 35f;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}
	}
}
