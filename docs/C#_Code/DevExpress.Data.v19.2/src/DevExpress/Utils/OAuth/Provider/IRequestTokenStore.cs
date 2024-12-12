namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;

    public interface IRequestTokenStore
    {
        IToken AuthorizeToken(string token, string identity);
        IToken CreateUnauthorizeToken(string consumerKey, string consumerSecret, string callback);
        IToken GetToken(string token);
    }
}

