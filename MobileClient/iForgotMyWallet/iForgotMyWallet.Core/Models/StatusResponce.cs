using System;
using Newtonsoft.Json;

namespace iForgotMyWallet.Core
{
	public class StatusResponce
	{

		[JsonProperty (PropertyName = "status")]
		public string status { get; set;}

		[JsonProperty (PropertyName = "response")]
		public string response { get; set; }

	}
}
