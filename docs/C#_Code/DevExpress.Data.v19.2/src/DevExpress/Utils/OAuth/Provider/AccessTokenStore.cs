namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Collections.Generic;

    public class AccessTokenStore : TokenStore, IAccessTokenStore
    {
        private static Dictionary<string, IToken> s_cache = new Dictionary<string, IToken>();

        public virtual IToken CreateToken(IToken requestToken)
        {
            if ((requestToken == null) || requestToken.IsEmpty)
            {
                throw new ArgumentException("requestToken is null or empty.", "requestToken");
            }
            Token token = new Token(requestToken.ConsumerKey, requestToken.ConsumerSecret, Token.NewToken(TokenLength.Long), Token.NewToken(TokenLength.Long), requestToken.AuthenticationTicket, requestToken.Verifier, requestToken.Callback);
            Dictionary<string, IToken> dictionary = s_cache;
            lock (dictionary)
            {
                s_cache[token.Value] = token;
            }
            return token;
        }

        public override IToken GetToken(string token)
        {
            IToken token2;
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            Dictionary<string, IToken> dictionary = s_cache;
            lock (dictionary)
            {
                if (!s_cache.TryGetValue(token, out token2))
                {
                    token2 = null;
                }
            }
            return token2;
        }

        public virtual void RevokeToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                Dictionary<string, IToken> dictionary = s_cache;
                lock (dictionary)
                {
                    s_cache.Remove(token);
                }
            }
        }
    }
}

