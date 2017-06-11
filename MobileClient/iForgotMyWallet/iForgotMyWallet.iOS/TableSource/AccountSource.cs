using System;
using UIKit;
using iForgotMyWallet.Core;
using System.Collections.Generic;
using Foundation;
using System.Linq;
using System.Drawing;

namespace iForgotMyWallet.iOS
{
	public class AccountSource : UITableViewSource
	{
		public List<Account> Items  { get; set; }

		private const string CellIdentifier = "AccountCell";

		public AccountSource (List<Account> Items)
		{
			this.Items = Items;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 70;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (!Items.Any())
				return 0;
			return Items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			AccountCell cell = (AccountCell)tableView.DequeueReusableCell (CellIdentifier);
			// if there are no cells to reuse, create a new one

			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier)as AccountCell;

			Account currAccount = Items[indexPath.Row];
			cell.accountName.Text = currAccount.Nickname;
			if (currAccount.OwnerName != null && currAccount.OwnerName != "")
			{
				cell.sharedLabel.Text = "Shared account with " + currAccount.OwnerName;
				cell.img.Image = UIImage.FromBundle("tblaccounts");
			}
			else { 
				cell.img.Image = UIImage.FromBundle("tblaccount");

			}
			//var model = Items.ElementAt (indexPath.Row);


			//string Title = string.Empty;
			//string SubTitle = string.Empty;

			//if (!string.IsNullOrEmpty (model.To)){ 
			//	Title = string.Format ("Money send to {0}",model.To);
			//else
			//	Title = string.Format ("Money received from {0}", model.From);

			//SubTitle = string.Format ("€{0}", model.Amount);

			//cell.TextLabel.Text = Title;
			//cell.DetailTextLabel.Text = SubTitle;

			return cell;
		}
	}
}
