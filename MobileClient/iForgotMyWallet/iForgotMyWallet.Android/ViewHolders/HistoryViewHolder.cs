using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
namespace iForgotMyWallet.Android
{
	public class HistoryViewHolder: RecyclerView.ViewHolder
	{
		public TextView TitleTextView { get; set; }
		public TextView NameTextView { get; set; }

		public HistoryViewHolder (View itemView, Action<int> listener) : base (itemView)
		{
			TitleTextView = itemView.FindViewById<TextView> (Resource.Id.edTitle);
			NameTextView = itemView.FindViewById<TextView> (Resource.Id.edSubtitle);

			itemView.Click += (sender, e) => listener (base.AdapterPosition);
		}
	}
}
