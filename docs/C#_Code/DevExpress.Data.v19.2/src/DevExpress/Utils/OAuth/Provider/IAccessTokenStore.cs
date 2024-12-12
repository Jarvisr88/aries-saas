namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;

    public interface IAccessTokenStore
    {
        IToken CreateToken(IToken requestToken);
        IToken GetToken(string token);
    }
}

