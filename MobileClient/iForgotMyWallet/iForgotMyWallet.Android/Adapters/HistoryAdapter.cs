using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using System.Linq;
using Android.Views;
using Android.Text;
using Android.OS;
using iForgotMyWallet.Core;
using Android.Graphics;

namespace iForgotMyWallet.Android
{
	public class HistoryAdapter : RecyclerView.Adapter
	{
		public event EventHandler<int> ItemClick;

		public List<Transaction> Items;
		public HistoryAdapter (List<Transaction> Items)
		{
			this.Items = Items;
		}

		public override int ItemCount {
			get {
				return Items.Count ();
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			//Inflate our GridView Item Layout
			View itemView = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.History_Item, parent, false);

			//Create our ViewHolder to cache the layout view references and register the OnClick event.
			var viewHolder = new HistoryViewHolder (itemView, OnClick);

			return viewHolder;
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			var viewHolder = holder as HistoryViewHolder;
			var model = Items [position];

			string Title = string.Empty;
			string SubTitle = string.Empty;

			if (!string.IsNullOrEmpty (model.To)) {
				viewHolder.NameTextView.SetTextColor (Color.Red);
				Title = string.Format ("Money send to {0}", model.To);
			} else {
				Title = string.Format ("Money received from {0}", model.From);
				viewHolder.NameTextView.SetTextColor (Color.Green);
			}

			SubTitle = string.Format ("€{0}", model.Amount);

			viewHolder.TitleTextView.Text = Title;
			viewHolder.NameTextView.Text = SubTitle;
		}

		//This will fire any event handlers that are registered with our ItemClick /event.
		private void OnClick (int position)
		{
			if (ItemClick != null) {
				ItemClick (this, position);
			}
		}
	}
}
