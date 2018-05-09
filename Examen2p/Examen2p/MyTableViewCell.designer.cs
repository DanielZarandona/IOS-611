// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Examen2p
{
	[Register ("MyTableViewCell")]
	partial class MyTableViewCell
	{
		[Outlet]
		UIKit.UIImageView imgFavorited { get; set; }

		[Outlet]
		UIKit.UIImageView imgRetweet { get; set; }

		[Outlet]
		UIKit.UILabel lblLike { get; set; }

		[Outlet]
		UIKit.UILabel lblRetweet { get; set; }

		[Outlet]
		UIKit.UILabel lblTweet { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblLike != null) {
				lblLike.Dispose ();
				lblLike = null;
			}

			if (lblRetweet != null) {
				lblRetweet.Dispose ();
				lblRetweet = null;
			}

			if (lblTweet != null) {
				lblTweet.Dispose ();
				lblTweet = null;
			}

			if (imgFavorited != null) {
				imgFavorited.Dispose ();
				imgFavorited = null;
			}

			if (imgRetweet != null) {
				imgRetweet.Dispose ();
				imgRetweet = null;
			}
		}
	}
}
