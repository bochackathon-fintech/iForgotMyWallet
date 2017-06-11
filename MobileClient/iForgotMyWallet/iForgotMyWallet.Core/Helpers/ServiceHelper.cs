using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using RestSharp;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;

namespace iForgotMyWallet.Core
{
	public class ThrottledHttp
	{
		private Semaphore _throttle;

		private RestClient _client;

		private int _timeout;

		public ThrottledHttp (RestClient client, int maxConcurrent, int timeout)
		{
			_throttle = new Semaphore (maxConcurrent, maxConcurrent);
			_client = client;
			_timeout = timeout;
		}

		public Task<IRestResponse<TResult>> Get<TResult> (IRestRequest request) where TResult : new()
		{
			return Get<TResult> (request, _timeout, null);
		}

		public Task<IRestResponse<TResult>> Get<TResult> (IRestRequest request, object authToken) where TResult : new()
		{
			return Get<TResult> (request, _timeout, authToken);
		}

		public Task<IRestResponse<TResult>> Get<TResult> (IRestRequest request, int timeout, object authToken) where TResult : new()
		{
			_throttle.WaitOne ();

			var response = _client.Execute<TResult> (request);

			_throttle.Release ();

			return response;
		}

		public Task<IRestResponse> Get (IRestRequest request)
		{
			return Get (request, _timeout, null);
		}

		public Task<IRestResponse> Get (IRestRequest request, object authToken)
		{
			return Get (request, _timeout, authToken);
		}

		public Task<IRestResponse> Get (IRestRequest request, int timeout, object authToken)
		{

			_throttle.WaitOne ();


			var response = _client.Execute (request);

			_throttle.Release ();

			return response;
		}

		public bool Failed (IRestResponse response)
		{
			return false;// response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.OK;
		}
	}

	public class ServiceHelper
	{
		#region Declarations

		protected string BaseUri;

		private int _timeout = 60 * 3;  // seconds

		protected int Timeout { get { return _timeout; } set { _timeout = value; } }

		private int _maxConcurrent = 1;

		public int MaxConcurrent {
			get { return _maxConcurrent; }
			set {
				_maxConcurrent = value;
				Client = new RestClient (BaseUri);
				_http = new ThrottledHttp (Client, MaxConcurrent, Timeout);
			}
		}

		public ThrottledHttp Web { get { return _http; } }

		protected RestClient Client { get; private set; }

		private ThrottledHttp _http;

		#endregion Declarations

		#region Constructors

		public ServiceHelper ()
		{
			Init (BaseUri, MaxConcurrent);
		}

		public ServiceHelper (string uri)
		{
			Init (uri, MaxConcurrent);
		}

		public ServiceHelper (string uri, int maxConcurrent)
		{
			Init (uri, maxConcurrent);
		}

		#endregion Constructors

		public void Init (string uri)
		{
			Init (uri, MaxConcurrent);
		}

		public void Init (string uri, int maxConcurrent)
		{
			Init (uri, maxConcurrent, Timeout);
		}

		public void Init (string uri, int maxConcurrent, int timeout)
		{

			Timeout = timeout;
			BaseUri = uri;
			MaxConcurrent = maxConcurrent;

		}
	}

}
