using System;
using System.Threading;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Linq;
using System.Collections.Generic;

namespace Examen2p
{
    public class linQtoTwitter
    {

        SingleUserAuthorizer authorizer;
        CancellationTokenSource cancellationTokenSource;
        TwitterContext twitterContext;

        public linQtoTwitter()
        {
            authorizer = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "WJR5O66K01t8RI5u0FpW3knCI",
                    ConsumerSecret = "wPrkLNj3X6fwM721umUZCAQYo8LRzKODWkYEqXkEiDDRPwkUgB",
                    AccessToken = "935685413624254464-sZ7ZPXYCwEtFAQfWWT7NBFh8b2WJvpf",
                    AccessTokenSecret = "6hnc6O7Wcmhna0IOepfR74LYa5T5PHuxTAQhFAvHbXq29"

                }
            };

            twitterContext = new TwitterContext(authorizer);
            cancellationTokenSource = new CancellationTokenSource();
        }

        static Lazy<linQtoTwitter> lazy = new Lazy<linQtoTwitter>(() => new linQtoTwitter()); 
        public static linQtoTwitter SharedInstance { get => lazy.Value; }


        public event EventHandler<TweetsFetchedEventArgs> TweetsFetched;
        public event EventHandler<FetchTweetsFailedEventArgs> FetchTweetsFailed;

        public class TweetsFetchedEventArgs : EventArgs {
            public List<Status> tweetd { get; private set; }

            public TweetsFetchedEventArgs(List<Status> tweets){
                tweetd = tweets;
            }
        }

        public class FetchTweetsFailedEventArgs : EventArgs
        {
            public string ErrorMessage { get; private set; }

            public FetchTweetsFailedEventArgs(string errorMesage)
            {
                ErrorMessage = errorMesage;
            }
        }


        public void BurcarTweets (string query, ulong sinceId){
            if (cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
            cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = cancellationTokenSource.Token;

            Task.Factory.StartNew(async () => {
                try
                {
                    var tweets = await BurcarTweetsAsync(query, sinceId, cancellationToken);

                    var e = new TweetsFetchedEventArgs(tweets);
                    TweetsFetched?.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    var e = new FetchTweetsFailedEventArgs(ex.Message);
                    FetchTweetsFailed?.Invoke (this, e);
                }
            }, cancellationToken);
        }

        async Task<List<Status>> BurcarTweetsAsync(string query, ulong sinceId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Status> ();
            }

            cancellationToken.ThrowIfCancellationRequested();

            Search searchResponse = await
                    (from Search in twitterContext.Search
                     where Search.Type == SearchType.Search &&
                     Search.Query == query &&
                     Search.SinceID == sinceId &&
                     Search.TweetMode == TweetMode.Extended
                     select Search)
                .SingleOrDefaultAsync();


            cancellationToken.ThrowIfCancellationRequested();

            return searchResponse?.Statuses;

        }
    }
}
