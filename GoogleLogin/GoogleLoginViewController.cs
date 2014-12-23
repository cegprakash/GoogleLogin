using System;
using System.Drawing;
using Xamarin.Auth;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace GoogleLogin {

	public class GoogleUserInfo
	{
		public string id { get; set; }
		public string email { get; set; }
		public bool verified_email { get; set; }
		public string name { get; set; }
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string link { get; set; }
		public string picture { get; set; }
		public string gender { get; set; }
	}


	public partial class GoogleLoginViewController : UIViewController {
		public GoogleLoginViewController (IntPtr handle) : base (handle) {
		}
		
		public override void DidReceiveMemoryWarning () {
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad () {
			Console.WriteLine ("ViewDidLoad");
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
		}
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender) {
			base.PrepareForSegue (segue, sender);
		}
		public override void ViewWillAppear (bool animated) {
			Console.WriteLine ("ViewWillAppear");
			base.ViewWillAppear (animated);
		}
		
		public override void ViewDidAppear (bool animated) {
			Console.WriteLine ("ViewDidAppear");
			base.ViewDidAppear (animated);
		}
		
		public override void ViewWillDisappear (bool animated) {
			Console.WriteLine ("ViewWillDisappear");
			base.ViewWillDisappear (animated);
		}
		
		public override void ViewDidDisappear (bool animated) {
			Console.WriteLine ("ViewDidDisappear");
			base.ViewDidDisappear (animated);
		}
			

		partial void OnClick (UIButton sender){
			Console.WriteLine("Button Cicked");
			LoginByGoogle();
		}


		string access_token;
		void LoginByGoogle ()
		{


			var auth = new OAuth2Authenticator (
				clientId: "157179555213-lilehanb1rrj759d9m151hnuqb4vt83b.apps.googleusercontent.com",
				scope: "https://www.googleapis.com/auth/userinfo.email", 
				authorizeUrl: new Uri ("https://accounts.google.com/o/oauth2/auth"),
				redirectUrl: new Uri ("https://www.example.com/oauth2callback"), 
				getUsernameAsync: null);  

			PresentViewController (auth.GetUI(), true, null);
			auth.Completed += (sender, e) => {  

				Console.WriteLine (e.IsAuthenticated);
				if (e.IsAuthenticated){
					e.Account.Properties.TryGetValue ("access_token", out access_token); 
					Console.WriteLine("access token : "+access_token);
				}
				//get user full information
				getInfo();
				DismissViewController (true, null);

			};




		}


		async void getInfo()
		{   
			//do RESP request,by appending token
			string userInfo = await GetDataFromGoogle (access_token ); 
			Console.WriteLine (  userInfo);
			if ( userInfo != "Exception" )
			{
				//Deserialize  to objet
				GoogleUserInfo cegprakash = JsonConvert.DeserializeObject<GoogleUserInfo> ( userInfo );  
				Console.WriteLine ("name : " + cegprakash.name);
			}
			else
			{  
				Console.WriteLine("exception");
			}
		}


		async Task<string> GetDataFromGoogle(string accessTokenValue)
		{    
			string  strURL =    "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + accessTokenValue + "";   
			string strResult;
			WebClient client = new WebClient (); 
			try
			{
				strResult=await client.DownloadStringTaskAsync (new Uri(strURL));
				Console.WriteLine("downloaded");
			}
			catch
			{
				strResult="Exception";
			}
			finally
			{
				client.Dispose ();
				client = null; 
			}
			return strResult;
		}

		#endregion
	}
}

