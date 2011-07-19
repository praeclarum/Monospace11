using System;
using System.Net;
using System.IO;

namespace Monospace11
{
	public class Http
	{
		public Http ()
		{
		}
		
		public void Get (string url, Action<string, string, Exception> continuation)
		{
			var req = WebRequest.Create (url);
			req.BeginGetResponse (respR => {
				WebResponse resp;
				try {
					resp = req.EndGetResponse (respR);
					var contentType = resp.ContentType;
					using (var r = new StreamReader (resp.GetResponseStream ())) {
						var content = r.ReadToEnd ();
						try {
							continuation (content, contentType, null);
						}
						catch (Exception) {}
					}
				}
				catch (Exception respErr) {
					continuation (null, null, respErr);
				}				
			}, null);
		}
	}
}

