using System;

namespace Monospace11
{
	public class HttpViewController : UITableViewController
	{
		void Initialize ()
		{
			new Http ().Get (
				"http://api.stackoverflow.com/1.0/questions?tagged=monotouch",
				(content, contentType, error) => {
				
				if (error == null) {
					Data = Model.ParseQuestions (content, contentType);					
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

