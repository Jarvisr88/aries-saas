namespace DevExpress.Utils.OAuth
{
    using System;

    public interface IToken
    {
        bool IsEmpty { get; }

        string Value { get; }

        string Secret { get; }

        bool IsCallbackConfirmed { get; }

        string ConsumerKey { get; }

        string ConsumerSecret { get; }

        string AuthenticationTicket { get; }

        string Callback { get; }

        string Verifier { get; }
    }
}

