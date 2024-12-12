namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Collections.Generic;

    public class RequestTokenStore : TokenStore, IRequestTokenStore
    {
        private static Dictionary<string, IToken> s_cache = new Dictionary<string, IToken>();

        public virtual IToken AuthorizeToken(string token, string authenticationTicket)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("token is null or empty.", "token");
            }
            if (string.IsNullOrEmpty(authenticationTicket))
            {
                throw new ArgumentException("authenticationTicket is null or empty.", "authenticationTicket");
            }
            IToken token2 = this.GetToken(token);
            if ((token2 == null) || token2.IsEmpty)
            {
                return null;
            }
            IToken token3 = new Token(token2.ConsumerKey, token2.ConsumerSecret, token2.Value, token2.Secret, authenticationTicket, token2.Verifier, token2.Callback);
            Dictionary<string, IToken> dictionary = s_cache;
            lock (dictionary)
            {
                s_cache[token3.Value] = token3;
            }
            return token3;
        }

        public virtual IToken CreateUnauthorizeToken(string consumerKey, string consumerSecret, string callback)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentException("consumerKey is null or empty.", "consumerKey");
            }
            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentException("consumerSecret is null or empty.", "consumerSecret");
            }
            if (string.IsNullOrEmpty(callback))
            {
                throw new ArgumentException("callback is null or empty.", "callback");
            }
            IToken token = new Token(consumerKey, consumerSecret, Token.NewToken(TokenLength.Long), Token.NewToken(TokenLength.Long), string.Empty, Token.NewToken(TokenLength.Short), callback);
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
    }
}

