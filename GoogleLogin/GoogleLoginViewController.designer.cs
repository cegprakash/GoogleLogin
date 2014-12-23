// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace GoogleLogin
{
	[Register ("GoogleLoginViewController")]
	partial class GoogleLoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton Login { get; set; }

		[Action ("OnClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnClick (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (Login != null) {
				Login.Dispose ();
				Login = null;
			}
		}
	}
}
