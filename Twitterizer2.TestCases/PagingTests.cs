﻿namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    
    [TestFixture()]
    public class PagingTests
    {
        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.Mentions(tokens).ResponseObject;
            
            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void UserTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.UserTimeline(tokens).ResponseObject;

            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Friends()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.FriendTimeline(tokens).ResponseObject;

            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Home()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.HomeTimeline(tokens).ResponseObject;

            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Followers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUserCollection followers = TwitterFriendship.Followers(tokens, new FollowersOptions()
            {
                ScreenName = "twitterapi"
            }).ResponseObject;

            int pagenumber = 1;
            decimal firstId = followers[0].Id;

            while (followers != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(followers);

                if (pagenumber > 1)
                    Assert.That(followers[0].Id != firstId);

                followers = followers.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }
    }
}
