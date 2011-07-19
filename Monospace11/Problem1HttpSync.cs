using System;

namespace Monospace11
{
	public class HttpSyncViewController : UITableViewController
	{
		public void Initialize ()
		{
			try {				
				var resp = new Http ().Get (
					"http://api.stackoverflow.com/1.0/questions?tagged=monotouch");
			
				Data = Model.ParseQuestions (resp.Content, resp.ContentType);
				TableView.ReloadData ();
			}
			catch (Exception error)
			{
				DisplayError (error);
			}
		}
		
		void UserRefreshRequested ()
		{
			Initialize ();
		}
	}
}

