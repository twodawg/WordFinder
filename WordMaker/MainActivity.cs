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
		static List<string> dictionaryFile = new List<string>();
		static Matches matches = new Matches();

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
						dictionaryFile.Add (sr.ReadLine ());
					}
				}
			}
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView resultsEditText = FindViewById<TextView> (Resource.Id.resultsTextView);
			EditText inputEditText = FindViewById<EditText> (Resource.Id.inputEditText);

			button.Click += delegate {

				resultsEditText.Text = "";

				Task.Run( () => {
					foreach (var match in matches.GetMatches(inputEditText.Text, 
						dictionaryFile.ToArray()).
						ToList<string>().
						OrderByDescending(o => o.Length))
					{
						RunOnUiThread( () => {resultsEditText.Text += match + System.Environment.NewLine;});  
					}	
				});

			};
		}

	}
}


