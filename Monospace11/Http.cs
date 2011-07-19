using System;
using System.IO;
using System.Net;
using System.Text;

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
							var content = r.ReadToEnd ();
							try {
								continuation (content, contentType, null);
							}
							catch (Exception) {}
						}
					}
				}
				catch (Exception respErr) {
					continuation (null, null, respErr);
				}				
			}, null);
		}
	}
}

