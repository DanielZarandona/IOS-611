using System;
using System.Collections.Generic;
using Foundation;
using LinqToTwitter;
using UIKit;

namespace Examen2p
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchResultsUpdating    {

        UISearchController searchController;
        List<Status> tweets;
        bool lazyLoads;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            inicializarComponentes();
        }

        void inicializarComponentes()
        {
            tweets = new List<Status>();

            linQtoTwitter.SharedInstance.TweetsFetched += SharedInstance_TweetsFetched;
            linQtoTwitter.SharedInstance.FetchTweetsFailed += SharedInstance_FetchTweetsFailed;

            searchController = new UISearchController(searchResultsController: null)
            {
                SearchResultsUpdater = this,
                DimsBackgroundDuringPresentation = false
            };

            tableViewTewwt.DataSource = this;
            tableViewTewwt.Delegate = this;
            tableViewTewwt.TableHeaderView = searchController.SearchBar;
            tableViewTewwt.RowHeight = UITableView.AutomaticDimension;
            tableViewTewwt.EstimatedRowHeight = 70;

        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return tweets.Count;     
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var tweet = tweets[indexPath.Row];

            if (tweets.Count - 1 >= indexPath.Row)
            {
                lazyLoads = true;
                linQtoTwitter.SharedInstance.BurcarTweets(searchController.SearchBar.Text, tweet.ID);
            }

            var cell = tableView.DequeueReusableCell(MyTableViewCell.Key, indexPath) as MyTableViewCell;
            cell.tweet = tweet.FullText;
            cell.likes = tweet.FavoriteCount?.ToString() ?? "0";
            cell.retweet = tweet.RetweetCount.ToString();

            return cell;
        }

		public override UIStatusBarStyle PreferredStatusBarStyle()
		{
            return UIStatusBarStyle.LightContent;
		}

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            linQtoTwitter.SharedInstance.BurcarTweets(searchController.SearchBar.Text, 1);
        }


        void SharedInstance_TweetsFetched(object sender, linQtoTwitter.TweetsFetchedEventArgs e)
        {
            if (lazyLoads)
            {
                lazyLoads = false;
                tweets.AddRange(e.tweetd);
            }else
            {
                tweets = e.tweetd;
            }
            InvokeOnMainThread(() => tableViewTewwt.ReloadData());
        }

        void SharedInstance_FetchTweetsFailed(object sender, linQtoTwitter.FetchTweetsFailedEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

		partial void BtnIconos(NSObject sender)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://iconos8.es/"));
        }
	}
}
