namespace DevExpress.Utils.OAuth
{
    using System;

    public interface IConsumer
    {
        string ConsumerKey { get; }

        string ConsumerSecret { get; }

        Uri CallbackUri { get; }
    }
}

