using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace iForgotMyWallet.Core
{
	public interface IDataEntity<TKey> : INotifyPropertyChanged
	{
		TKey Id { get; set; }

		//DateTime Created { get; set; }

		DateTime ModifiedClient { get; set; }

		//DateTime ModifiedServer { get; set; }

		bool IsActive { get; set; }

		bool IsLocked { get; set; }

		int EntityState { get; set; }

		IEnumerable<PropertyInfo> GetChanges (IDataEntity<TKey> original, bool UseIgnoreList = false);

		void SetEntityState (ClientEntityState state);

		ClientEntityState GetEntityState ();

		//void SetEntityState (int entityStateId);
	}
}
