namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;

    public interface IConsumerStore
    {
        IConsumer GetConsumer(string consumerKey);
    }
}

