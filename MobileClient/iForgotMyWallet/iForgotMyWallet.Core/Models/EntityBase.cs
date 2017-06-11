using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;

namespace iForgotMyWallet.Core
{
public abstract class EntityBase<TKey> : IDataEntity<TKey>
	{
		//Used to ignore modifications changes while useing the GetChanges method

		public event PropertyChangedEventHandler PropertyChanged;

		private TKey id;

		public TKey Id {
			get { return id; }
			set {
				id = value;
				OnPropertyChanged ("Id");
			}
		}

		private DateTime modifiedClient = DateTime.Now;

		public DateTime ModifiedClient {
			get { return modifiedClient; }
			set {
				var oldValue = modifiedClient;
				if (oldValue != value) {
					modifiedClient = value;
					OnPropertyChanged ("ModifiedClient");
				}
			}
		}

		private bool isActive = false;

		public bool IsActive {
			get { return isActive; }
			set {
				var oldValue = isActive;
				if (oldValue != value) {
					isActive = value;
					OnPropertyChanged ("IsActive");
				}
			}
		}

		private bool isLocked = false;

		public bool IsLocked {
			get { return isLocked; }
			set {
				var oldValue = isLocked;
				if (oldValue != value) {
					isLocked = value;
					OnPropertyChanged ("IsLocked");
				}
			}
		}


		private int entityState = (int)ClientEntityState.New;

		public int EntityState {
			get { return entityState; }
			set {
				var oldValue = entityState;
				if (oldValue != value) {
					entityState = value;
					OnPropertyChanged ("ClientEntityState");
				}
			}
		}


		public virtual void OnPropertyChanged (string name)
		{
			//EntityState = (int)ClientEntityState.Modified;
			//ModifiedClient = DateTime.Now;

			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (name));
			}
		}

		public IEnumerable<PropertyInfo> GetChanges (IDataEntity<TKey> original, bool UseIgnoreList = false)
		{
			var changes = new List<PropertyInfo> ();

			if (original == null)
				throw new InvalidOperationException ("Comparison object is null");

			PropertyInfo [] properties = this.GetType ().GetRuntimeProperties ().ToArray();

			foreach (var prop in properties) {

				var origVal = prop.GetValue (original, null);
				var propVal = prop.GetValue (this, null);

				if (prop.PropertyType.GetTypeInfo ().IsPrimitive) {
					if (!origVal.Equals (propVal))
						changes.Add (prop);
				} else {
					if (origVal == null && propVal == null)
						continue;
					else if (origVal == null && propVal != null)
						changes.Add (prop);
					else if (propVal == null && origVal != null)
						changes.Add (prop);
					else if (!origVal.Equals (propVal))
						changes.Add (prop);
				}
			}

			return changes;
		}

		public void SetEntityState (ClientEntityState state)
		{
			EntityState = (int)state;
		}

		public ClientEntityState GetEntityState ()
		{
			return (ClientEntityState)EntityState;
		}
	}
}
