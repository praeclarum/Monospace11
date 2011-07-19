using System;

namespace Monospace11
{
	public class UIViewController
	{
		public object Data;
		
		public void DisplayError (Exception error)
		{
			Console.WriteLine ("==============================");
			Console.WriteLine ("ERROR " + DateTime.Now);
			Console.WriteLine (error);
		}
	}
	
	public class UITableViewController : UIViewController
	{
		public UITableView TableView = new UITableView ();		
	}
	
	public class UITableView
	{
		public void ReloadData ()
		{
		}
	}
}

