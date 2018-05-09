using System;

using Foundation;
using UIKit;

namespace Examen2p
{
    public partial class MyTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MyTableViewCell");
        public static readonly UINib Nib;

        public string tweet
        {
            get => lblTweet.Text;
            set => lblTweet.Text = value;
        }

        public string likes
        {
            get => lblLike.Text;
            set => lblLike.Text = value;
        }

        public string retweet
        {
            get => lblRetweet.Text;
            set => lblRetweet.Text = value;
        }


        protected MyTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
