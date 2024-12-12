namespace DevExpress.Utils.OAuth
{
    using System;

    public class ConsumerBase : IConsumer
    {
        private Uri _callbackUri;
        private string _consumerKey;
        private string _consumerSecret;

        public Uri CallbackUri
        {
            get => 
                this._callbackUri;
            set => 
                this._callbackUri = value;
        }

        public string ConsumerKey
        {
            get => 
                this._consumerKey;
            set => 
                this._consumerKey = value;
        }

        public string ConsumerSecret
        {
            get => 
                this._consumerSecret;
            set => 
                this._consumerSecret = value;
        }
    }
}

