namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Security.Principal;

    public class TokenIdentity : IIdentity
    {
        private string _token;
        private string _authenticationTicket;

        public TokenIdentity(string token, string authenticationTicket)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("token is null or empty.", "token");
            }
            if (string.IsNullOrEmpty(authenticationTicket))
            {
                throw new ArgumentException("authenticationTicket is null or empty.", "authenticationTicket");
            }
            this._token = token;
            this._authenticationTicket = authenticationTicket;
        }

        public string AuthenticationType =>
            "OAuth";

        public string Name =>
            this.Token;

        public bool IsAuthenticated =>
            !string.IsNullOrEmpty(this._token) && !string.IsNullOrEmpty(this._authenticationTicket);

        public string Token =>
            this._token;

        public string AuthenticationTicket =>
            this._authenticationTicket;
    }
}

