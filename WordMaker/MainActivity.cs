using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Scrabble;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMaker
{
	[Activity (Label = "Word Maker", MainLauncher = true)]
	public class MainActivity : Activity
	{
		static List<string> Dic = new List<string>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

//			using (FileStream fs = new FileStream (@"wordmaker/dictionary.txt", FileMode.Open, 
//				                       FileAccess.Read, FileShare.Read)) 
//			string[] names = System.Reflection.Assembly.GetExecutingAssembly ().GetManifestResourceNames ();
			using (Stream fs = System.Reflection.Assembly.GetExecutingAssembly ().GetManifestResourceStream ("WordMaker.dictionary.txt"))
			{
				using (StreamReader sr = new StreamReader (fs)) {

					while (!sr.EndOfStream) {
						Dic.Add (sr.ReadLine ());
					}
				}
			}
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView resultsEditText = FindViewById<TextView> (Resource.Id.resultsTextView);
			EditText inputEditText = FindViewById<EditText> (Resource.Id.inputEditText);
			//ProgressBar progressBar = FindViewById<ProgressBar> (Resource.Id.progressBar);
			//progressBar.Visibility = ViewStates.Invisible;
			
			button.Click += delegate {

				//RunOnUiThread( () => {progressBar.Visibility = ViewStates.Visible;});

				resultsEditText.Text = "";
				//var wordlist = new Combination(inputEditText.Text).Results();
				Matches matches = new Matches();

				Task.Run( () => {
					foreach (var match in matches.GetMatches(inputEditText.Text, Dic.ToArray()).
						ToList<string>().OrderByDescending(o => o.Length))
					{
						RunOnUiThread( () => {resultsEditText.Text +=match + System.Environment.NewLine;});  
					}
//				foreach (var mixed in wordlist)
//				{
//					List<string> matches = new List<string>(Dic.AsParallel().Where(i => i.Equals(mixed)));
//					//matches.AddRange(Dic.Where(i => i.Equals(mixed)));
//
//					if (matches.Count > 0)
//					{
//						// Only show the dictionary matches to the mixes words
//						foreach (var match in matches.OrderByDescending(o => o.Length))
//						{
//							RunOnUiThread( () => {resultsEditText.Text +=match + System.Environment.NewLine;});
//						}
//					}
//				}				
				});
				//progressBar.Visibility = ViewStates.Gone;
//				ringProgressDialog.Dismiss();

//				foreach (var item in new Combination(inputEditText.Text).Results())
//				{
//					resultsEditText.Text += item + System.Environment.NewLine;
//				}
			};
		}

	}
}


