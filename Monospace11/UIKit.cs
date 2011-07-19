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
		public UITableView TableView;
		
		public UITableViewController ()
		{
			TableView = new UITableView (() => Data);
		}
	}
	
	public class UITableView
	{
		Func<object> GetDataSource;
		
		public UITableView (Func<object> getDataSource)
		{
			GetDataSource = getDataSource;
		}
		
		public void ReloadData ()
		{
			Console.WriteLine ("==============================");
			Console.WriteLine ("TABLE DATA " + DateTime.Now);
			Console.WriteLine (GetDataSource ());			
		}
	}
}

