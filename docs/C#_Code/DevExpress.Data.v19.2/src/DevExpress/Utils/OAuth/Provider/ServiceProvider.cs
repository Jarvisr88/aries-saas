namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Runtime.InteropServices;

    public class ServiceProvider : ConfigurationSection
    {
        private IConsumerStore _consumerStore;
        private IAccessTokenStore _accessTokenStore;
        private IRequestTokenStore _requestTokenStore;

        public static IToken AuthorizeRequestToken(string httpMethod, Uri uri, string authenticationTicket, out ValidationScope scope)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return AuthorizeRequestToken(serviceProvider, httpMethod, uri, authenticationTicket, out scope);
        }

        public static IToken AuthorizeRequestToken(ServiceProvider serviceProvider, string httpMethod, Uri uri, string authenticationTicket, out ValidationScope scope)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            scope = new ValidationScope(httpMethod, uri, string.Empty, serviceProvider);
            string[] names = new string[] { "oauth_token" };
            scope.Parameters.Require(names);
            scope.Parameters.Match("oauth_version", false, "1.0");
            if (scope.ValidateParameters())
            {
                IRequestTokenStore requestTokenStore = serviceProvider.RequestTokenStore;
                if (requestTokenStore == null)
                {
                    throw new InvalidOperationException("Request token store is not configured.");
                }
                IToken token = requestTokenStore.AuthorizeToken(scope.Parameters["oauth_token"].Value, authenticationTicket);
                if (((token != null) && !token.IsEmpty) && ((token.Value == scope.Parameters["oauth_token"].Value) && (!string.IsNullOrEmpty(token.AuthenticationTicket) && ((token.AuthenticationTicket == authenticationTicket) && !string.IsNullOrEmpty(token.Verifier)))))
                {
                    scope = null;
                    return token;
                }
                scope.AddError(new ValidationError(0x191, "Invalid / expired Token: " + scope.Parameters["oauth_token"].Value));
            }
            return null;
        }

        public static Response GetAccessToken(string httpMethod, Uri uri)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return GetAccessToken(serviceProvider, httpMethod, uri);
        }

        public static Response GetAccessToken(ServiceProvider serviceProvider, string httpMethod, Uri uri)
        {
            ValidationScope scope;
            IToken token = GetAccessToken(serviceProvider, httpMethod, uri, out scope);
            if ((token != null) && !token.IsEmpty)
            {
                return new Response(200, $"oauth_token={Escaping.Escape(token.Value)}&oauth_token_secret={Escaping.Escape(token.Secret)}&oauth_callback_confirmed=true", "text/plain");
            }
            if (scope != null)
            {
                using (IEnumerator<ValidationError> enumerator = scope.Errors.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ValidationError current = enumerator.Current;
                        return new Response(current.StatusCode, current.Message, "text/plain");
                    }
                }
            }
            return new Response(500, "Internal server error", "text/plain");
        }

        public static IToken GetAccessToken(string httpMethod, Uri uri, out ValidationScope scope)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return GetAccessToken(serviceProvider, httpMethod, uri, out scope);
        }

        public static IToken GetAccessToken(ServiceProvider serviceProvider, string httpMethod, Uri uri, out ValidationScope scope)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            scope = new ValidationScope(httpMethod, uri, string.Empty, serviceProvider);
            string[] names = new string[] { "oauth_verifier", "oauth_token", "oauth_consumer_key", "oauth_nonce", "oauth_timestamp", "oauth_signature_method", "oauth_signature" };
            scope.Parameters.Require(names);
            scope.Parameters.Match("oauth_version", false, "1.0");
            if (!scope.ValidateParameters())
            {
                return null;
            }
            if (!scope.ValidateTimestamp())
            {
                return null;
            }
            if (!scope.ValidateNonce())
            {
                return null;
            }
            IToken requestToken = scope.ValidateAuthorizedToken();
            if (requestToken == null)
            {
                return null;
            }
            if (!scope.ValidateSignature(requestToken.ConsumerSecret, requestToken.Secret))
            {
                return null;
            }
            IAccessTokenStore accessTokenStore = serviceProvider.AccessTokenStore;
            if (accessTokenStore == null)
            {
                throw new InvalidOperationException("Access token store is not configured.");
            }
            return accessTokenStore.CreateToken(requestToken);
        }

        public static Response GetRequestToken(string httpMethod, Uri uri)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return GetRequestToken(serviceProvider, httpMethod, uri);
        }

        public static Response GetRequestToken(ServiceProvider serviceProvider, string httpMethod, Uri uri)
        {
            ValidationScope scope;
            IToken token = GetRequestToken(serviceProvider, httpMethod, uri, out scope);
            if ((token != null) && !token.IsEmpty)
            {
                return new Response(200, $"oauth_token={Escaping.Escape(token.Value)}&oauth_token_secret={Escaping.Escape(token.Secret)}&oauth_callback_confirmed=true", "text/plain");
            }
            if (scope != null)
            {
                using (IEnumerator<ValidationError> enumerator = scope.Errors.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ValidationError current = enumerator.Current;
                        return new Response(current.StatusCode, current.Message, "text/plain");
                    }
                }
            }
            return new Response(500, "Internal server error", "text/plain");
        }

        public static IToken GetRequestToken(string httpMethod, Uri uri, out ValidationScope scope)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return GetRequestToken(serviceProvider, httpMethod, uri, out scope);
        }

        public static IToken GetRequestToken(ServiceProvider serviceProvider, string httpMethod, Uri uri, out ValidationScope scope)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            scope = new ValidationScope(httpMethod, uri, string.Empty, serviceProvider);
            string[] names = new string[] { "oauth_consumer_key", "oauth_nonce", "oauth_timestamp", "oauth_signature_method", "oauth_signature", "oauth_callback" };
            scope.Parameters.Require(names);
            scope.Parameters.Match("oauth_version", false, "1.0");
            if (!scope.ValidateParameters())
            {
                return null;
            }
            if (!scope.ValidateTimestamp())
            {
                return null;
            }
            if (!scope.ValidateNonce())
            {
                return null;
            }
            IConsumer consumer = scope.ValidateConsumer();
            if (consumer == null)
            {
                return null;
            }
            if (!scope.ValidateSignature(consumer.ConsumerSecret, string.Empty))
            {
                return null;
            }
            IRequestTokenStore requestTokenStore = serviceProvider.RequestTokenStore;
            if (requestTokenStore == null)
            {
                throw new InvalidOperationException("Request token store is not configured.");
            }
            return requestTokenStore.CreateUnauthorizeToken(consumer.ConsumerKey, consumer.ConsumerSecret, scope.Parameters["oauth_callback"].Value);
        }

        public static TokenIdentity GetTokenIdentity(string httpMethod, Uri uri, NameValueCollection headers)
        {
            ValidationScope scope;
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            if (headers == null)
            {
                return null;
            }
            string str = headers["Authorization"];
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            TokenIdentity identity = GetTokenIdentity(serviceProvider, httpMethod, uri, str, out scope);
            return (((scope == null) || (scope.ErrorCount <= 0)) ? identity : null);
        }

        public static TokenIdentity GetTokenIdentity(string httpMethod, Uri uri, string authorizationHeader, out ValidationScope scope)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return GetTokenIdentity(serviceProvider, httpMethod, uri, authorizationHeader, out scope);
        }

        public static TokenIdentity GetTokenIdentity(ServiceProvider serviceProvider, string httpMethod, Uri uri, string authorizationHeader, out ValidationScope scope)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new ArgumentException("authorizationHeader is null or empty.", "authorizationHeader");
            }
            if (!authorizationHeader.StartsWith("OAuth", StringComparison.InvariantCultureIgnoreCase))
            {
                scope = null;
                return null;
            }
            scope = new ValidationScope(httpMethod, uri, authorizationHeader, serviceProvider);
            string[] names = new string[] { "oauth_token", "oauth_consumer_key", "oauth_nonce", "oauth_timestamp", "oauth_signature_method", "oauth_signature" };
            scope.Parameters.Require(names);
            scope.Parameters.Match("oauth_version", false, "1.0");
            if (!scope.ValidateParameters())
            {
                return null;
            }
            if (!scope.ValidateTimestamp())
            {
                return null;
            }
            if (!scope.ValidateNonce())
            {
                return null;
            }
            IAccessTokenStore accessTokenStore = serviceProvider.AccessTokenStore;
            if (accessTokenStore == null)
            {
                throw new InvalidOperationException("Access token store is not configured.");
            }
            IToken token = accessTokenStore.GetToken(scope.Parameters["oauth_token"].Value);
            if (((token == null) || token.IsEmpty) || ((token.Value != scope.Parameters["oauth_token"].Value) || string.IsNullOrEmpty(token.AuthenticationTicket)))
            {
                scope.AddError(new ValidationError(0x191, "Invalid / expired Token: " + scope.Parameters["oauth_token"].Value));
                return null;
            }
            if (!scope.ValidateSignature(token.ConsumerSecret, token.Secret))
            {
                return null;
            }
            scope = null;
            return new TokenIdentity(token.Value, token.AuthenticationTicket);
        }

        public static IToken VerifyRequestToken(string httpMethod, Uri uri, out ValidationScope scope)
        {
            ServiceProvider serviceProvider = Default;
            if (serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider not configured.");
            }
            return VerifyRequestToken(serviceProvider, httpMethod, uri, out scope);
        }

        public static IToken VerifyRequestToken(ServiceProvider serviceProvider, string httpMethod, Uri uri, out ValidationScope scope)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            scope = new ValidationScope(httpMethod, uri, string.Empty, serviceProvider);
            string[] names = new string[] { "oauth_token" };
            scope.Parameters.Require(names);
            scope.Parameters.Match("oauth_version", false, "1.0");
            if (!scope.ValidateParameters())
            {
                return null;
            }
            IToken token = scope.ValidateUnauthorizedToken();
            return ((token != null) ? token : null);
        }

        public static ServiceProvider Default
        {
            get
            {
                ServiceProvider section = ConfigurationManager.GetSection("oauth.serviceProvider") as ServiceProvider;
                return new ServiceProvider();
            }
        }

        public virtual IConsumerStore ConsumerStore
        {
            get
            {
                if (this._consumerStore == null)
                {
                    this._consumerStore = ConfigurationManager.GetSection("oauth.consumerStore") as IConsumerStore;
                    this._consumerStore ??= new DevExpress.Utils.OAuth.Provider.ConsumerStore();
                }
                return this._consumerStore;
            }
        }

        public virtual IAccessTokenStore AccessTokenStore
        {
            get
            {
                if (this._accessTokenStore == null)
                {
                    this._accessTokenStore = ConfigurationManager.GetSection("oauth.accessTokenStore") as IAccessTokenStore;
                    this._accessTokenStore ??= new DevExpress.Utils.OAuth.Provider.AccessTokenStore();
                }
                return this._accessTokenStore;
            }
        }

        public virtual IRequestTokenStore RequestTokenStore
        {
            get
            {
                if (this._requestTokenStore == null)
                {
                    this._requestTokenStore = ConfigurationManager.GetSection("oauth.requestTokenStore") as IRequestTokenStore;
                    this._requestTokenStore ??= new DevExpress.Utils.OAuth.Provider.RequestTokenStore();
                }
                return this._requestTokenStore;
            }
        }
    }
}

