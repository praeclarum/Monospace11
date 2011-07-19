using System;
using System.IO;
using System.Net;
using System.Text;

namespace Monospace11
{
	public class Http
	{
		IHttpCache cache;
		
		public Http (IHttpCache cache = null)
		{
			this.cache = cache;
		}
		
		public void Get (string url, Action<HttpResponse, Exception> continuation)
		{			
			if (cache != null && cache.ContainsFreshData (url)) {
				continuation (cache.Get (url), null);
			}
			
			var req = WebRequest.Create (url);
			var hreq = req as HttpWebRequest;
			if (hreq != null) {
				hreq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			}
			req.BeginGetResponse (respR => {
				WebResponse resp;
				try {
					resp = req.EndGetResponse (respR);
					var contentType = resp.ContentType;
					using (var s = resp.GetResponseStream ()) {
						using (var r = new StreamReader (s, Encoding.UTF8)) {

							var hr = new HttpResponse {
								Content = r.ReadToEnd (),
								ContentType = contentType,
							};
								
							if (cache != null) {
								cache.Add (url, hr);
							}
							
							try {
								continuation (hr, null);
							}
							catch (Exception) {}
						}
					}
				}
				catch (Exception respErr) {
					continuation (null, respErr);
				}				
			}, null);
		}
		
		public HttpResponse Get (string url)
		{
			var req = WebRequest.Create (url);
			var hreq = req as HttpWebRequest;
			if (hreq != null) {
				hreq.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			}
			var resp = req.GetResponse ();
			var contentType = resp.ContentType;
			using (var s = resp.GetResponseStream ()) {
				using (var r = new StreamReader (s, Encoding.UTF8)) {

					var hr = new HttpResponse {
						Content = r.ReadToEnd (),
						ContentType = contentType,
					};
					
					if (cache != null) {
						cache.Add (url, hr);
					}
					
					return hr;
				}
			}
		}
	}
	
	public interface IHttpCache
	{
		bool ContainsFreshData (string url);
		HttpResponse Get (string url);
		void Add (string url, HttpResponse resp);
	}
	
	public class HttpResponse
	{
		public string Content;
		public string ContentType;
		public DateTime ExpirationDate;
	}
}

