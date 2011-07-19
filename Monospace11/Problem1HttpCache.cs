using System;

namespace Monospace11
{
	public class HttpCacheViewController : UITableViewController
	{
		public IHttpCache HttpCache { get; set; }
		
		public void Initialize ()
		{
			new Http (HttpCache).Get (
				"http://api.stackoverflow.com/1.0/questions?tagged=monotouch",
				(resp, error) => {
				
				if (error == null) {
					Data = Model.ParseQuestions (resp.Content, resp.ContentType);
					TableView.ReloadData ();
				}
				else {
					DisplayError (error);
				}
			});
		}
		
		void UserRefreshRequested ()
		{
			Initialize ();
		}
	}
}

