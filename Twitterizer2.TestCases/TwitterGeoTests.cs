﻿namespace Twitterizer2.TestCases
{
    using NUnit.Framework;
    using Twitterizer;

    [TestFixture]
    public class TwitterGeoTests
    {
        [Test]
        public static void LookupPlaces()
        {
            TwitterPlaceLookupOptions options = new TwitterPlaceLookupOptions()
            {
                Granularity = "city",
                MaxResults = 2
            };

            TwitterPlaceCollection places = TwitterPlace.Lookup(30.475012, -84.35509, options);

            Assert.IsNotNull(places);
            Assert.IsNotEmpty(places);
            Assert.That(places.Count == 2);
        }
    }
}
