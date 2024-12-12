namespace DMEWorks.Forms.Maps
{
    using DMEWorks.Core;
    using DMEWorks.Forms.Properties;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public abstract class MapProvider
    {
        protected MapProvider()
        {
        }

        public abstract Uri GetHomePage();
        public abstract Uri GetPointUri(Address address);
        public static ICollection<MapProvider> GetProviders() => 
            new MapProvider[] { new BingMaps(), new GoogleMaps(), new MapQuestMaps(), new YahooMaps() };

        public abstract Uri GetRouteUri(Address from, Address to);

        public abstract string Name { get; }

        public abstract System.Drawing.Image Image { get; }

        private sealed class BingMaps : MapProvider
        {
            public override Uri GetHomePage() => 
                new Uri("http://www.bing.com/maps/");

            public override Uri GetPointUri(Address address)
            {
                string[] textArray1 = new string[] { address.Address1, ",", address.City, ",", address.State, ",", address.Zip };
                return new Uri("http://www.bing.com/maps/?where1=" + HttpUtility.UrlEncode(string.Concat(textArray1)));
            }

            public override Uri GetRouteUri(Address from, Address to) => 
                null;

            public override string Name =>
                "Bing Maps";

            public override System.Drawing.Image Image =>
                Resources.ImageBingMaps;
        }

        private sealed class GoogleMaps : MapProvider
        {
            public override Uri GetHomePage() => 
                new Uri("http://maps.google.com/");

            public override Uri GetPointUri(Address address)
            {
                string[] textArray1 = new string[] { address.Address1, ",", address.City, ",", address.State, ",", address.Zip };
                return new Uri("https://maps.google.com/maps?q=" + HttpUtility.UrlEncode(string.Concat(textArray1)));
            }

            public override Uri GetRouteUri(Address from, Address to) => 
                null;

            public override string Name =>
                "Google Maps";

            public override System.Drawing.Image Image =>
                Resources.ImageGoogleMaps;
        }

        private sealed class MapQuestMaps : MapProvider
        {
            public override Uri GetHomePage() => 
                new Uri("http://www.mapquest.com/");

            public override Uri GetPointUri(Address address)
            {
                string[] textArray1 = new string[] { address.Address1, ",", address.City, ",", address.State, ",", address.Zip };
                return new Uri("http://www.mapquest.com/?q=" + HttpUtility.UrlEncode(string.Concat(textArray1)));
            }

            public override Uri GetRouteUri(Address from, Address to) => 
                null;

            public override string Name =>
                "MapQuest Maps";

            public override System.Drawing.Image Image =>
                Resources.ImageMapQuest;
        }

        private sealed class YahooMaps : MapProvider
        {
            public override Uri GetHomePage() => 
                new Uri("https://search.yahoo.com/search/?p=maps");

            public override Uri GetPointUri(Address address)
            {
                string[] textArray1 = new string[] { address.Address1, ",", address.City, ",", address.State, ",", address.Zip };
                return new Uri("https://search.yahoo.com/search?p=" + HttpUtility.UrlEncode(string.Concat(textArray1)));
            }

            public override Uri GetRouteUri(Address from, Address to) => 
                null;

            public override string Name =>
                "Yahoo! Maps";

            public override System.Drawing.Image Image =>
                Resources.ImageYahooMaps;
        }
    }
}

