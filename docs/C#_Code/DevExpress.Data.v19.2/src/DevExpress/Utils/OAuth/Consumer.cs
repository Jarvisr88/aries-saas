namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class Consumer : ConsumerBase
    {
        private string _httpMethod = "GET";
        private Uri _requestUri;
        private Uri _authorizeUri;
        private Uri _accessUri;
        private DevExpress.Utils.OAuth.Signature _signature;
        private IToken _requestToken;
        private CookieContainer _cookies = new CookieContainer();
        private bool _requireSsl;
        private IToken _accessToken;

        public static void AbortHttpRequest(WebRequest request)
        {
            try
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
            catch (Exception exception)
            {
                if ((exception is OutOfMemoryException) || ((exception is StackOverflowException) || (exception is ThreadAbortException)))
                {
                    throw;
                }
            }
        }

        public IAsyncResult BeginGetAccessToken(string code, AsyncCallback callback, object state)
        {
            HttpWebRequest request = CreateHttpRequest(this.GetAccessTokenUrl(code), this.HttpMethod, this.Cookies);
            ConsumerOperation operation = new ConsumerOperation(request, this, "2.0", false, callback, state);
            request.BeginGetResponse(new AsyncCallback(Consumer.OnHttpResponse), operation);
            return operation;
        }

        public IAsyncResult BeginGetAccessToken(IToken requestToken, string verifier, AsyncCallback callback, object state)
        {
            HttpWebRequest request = CreateHttpRequest(this.GetAccessTokenUrl(requestToken, verifier), this.HttpMethod, this.Cookies);
            ConsumerOperation operation = new ConsumerOperation(request, this, "1.0", true, callback, state);
            request.BeginGetResponse(new AsyncCallback(Consumer.OnHttpResponse), operation);
            return operation;
        }

        public IAsyncResult BeginGetRequestToken(AsyncCallback callback, object state)
        {
            HttpWebRequest request = CreateHttpRequest(this.GetRequestTokenUrl(), this.HttpMethod, this.Cookies);
            ConsumerOperation operation = new ConsumerOperation(request, this, "1.0", true, callback, state);
            request.BeginGetResponse(new AsyncCallback(Consumer.OnHttpResponse), operation);
            return operation;
        }

        public static HttpWebRequest CreateHttpRequest(Uri requestUri, string httpMethod, CookieContainer cookies)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            if ((httpMethod == null) || (httpMethod.Length == 0))
            {
                throw new ArgumentNullException("httpMethod");
            }
            if (!requestUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Request URI be absolute.");
            }
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.Method = httpMethod;
            if (string.Equals(request.Method, "POST", StringComparison.InvariantCultureIgnoreCase))
            {
                request.ContentLength = 0L;
                request.ContentType = "application/x-www-form-urlencoded";
            }
            if (cookies != null)
            {
                request.CookieContainer = cookies;
            }
            request.KeepAlive = false;
            return request;
        }

        public static HttpWebRequest CreateHttpRequest(Uri requestUri, string httpMethod, CookieContainer cookies, IToken token, DevExpress.Utils.OAuth.Signature signature, string version)
        {
            if ((version == null) || (version.Length == 0))
            {
                version = "1.0";
            }
            if (!string.Equals(version, "1.0") && !string.Equals(version, "2.0"))
            {
                throw new ArgumentException("The specified OAuth version is not supported.", "version");
            }
            if ((token == null) || token.IsEmpty)
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token'");
            }
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            if ((httpMethod == null) || (httpMethod.Length == 0))
            {
                throw new ArgumentNullException("httpMethod");
            }
            if (!requestUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Request URI be absolute.");
            }
            Url url = new Url(requestUri, string.Empty);
            HttpWebRequest request = null;
            if (version == "1.0")
            {
                request = (HttpWebRequest) WebRequest.Create(requestUri.ToString());
                request.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader(requestUri, token, httpMethod, signature);
            }
            else
            {
                if (version != "2.0")
                {
                    throw new NotImplementedException();
                }
                Parameter[] additional = new Parameter[] { new Parameter("access_token", token.Value) };
                request = (HttpWebRequest) WebRequest.Create(url.ToUri(additional));
            }
            request.Method = httpMethod;
            request.PreAuthenticate = true;
            if (cookies != null)
            {
                request.CookieContainer = cookies;
            }
            request.AllowAutoRedirect = false;
            return request;
        }

        public IToken EndGetAccessToken(IAsyncResult ar)
        {
            if (ar == null)
            {
                throw new ArgumentNullException("ar", "ar is null.");
            }
            ConsumerOperation operation = (ConsumerOperation) ar;
            byte[] bytes = operation.End(this);
            this._requestToken = new Token(Parameters.Parse(bytes, operation.Unescape), base.ConsumerKey, base.ConsumerSecret, base.CallbackUri.ToString(), operation.Version);
            return this._requestToken;
        }

        public IToken EndGetRequestToken(IAsyncResult ar)
        {
            if (ar == null)
            {
                throw new ArgumentNullException("ar", "ar is null.");
            }
            ConsumerOperation operation = (ConsumerOperation) ar;
            byte[] bytes = operation.End(this);
            this._requestToken = new Token(Parameters.Parse(bytes, operation.Unescape), base.ConsumerKey, base.ConsumerSecret, base.CallbackUri.ToString(), "1.0");
            return this._requestToken;
        }

        public IToken GetAccessToken(string code)
        {
            if ((this._accessToken != null) && !this._accessToken.IsEmpty)
            {
                return this._accessToken;
            }
            using (WebResponse response = CreateHttpRequest(this.GetAccessTokenUrl(code), this.HttpMethod, this.Cookies).GetResponse())
            {
                this._accessToken = new Token(Parameters.Parse(response, false), base.ConsumerKey, base.ConsumerSecret, base.CallbackUri.ToString(), "2.0");
                return this._accessToken;
            }
        }

        public IToken GetAccessToken(IToken requestToken, string verifier)
        {
            if ((this._accessToken != null) && !this._accessToken.IsEmpty)
            {
                return this._accessToken;
            }
            using (WebResponse response = CreateHttpRequest(this.GetAccessTokenUrl(requestToken, verifier), this.HttpMethod, this.Cookies).GetResponse())
            {
                this._accessToken = new Token(Parameters.Parse(response), base.ConsumerKey, base.ConsumerSecret, base.CallbackUri.ToString(), "1.0");
                return this._accessToken;
            }
        }

        public Uri GetAccessTokenUrl(string code)
        {
            if (this.AccessUri == null)
            {
                throw new InvalidOperationException("AccessUri is not specified.");
            }
            if (!this.AccessUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("AccessUri be an absolute uri.");
            }
            if (!string.Equals(this.AccessUri.Scheme, "HTTPS", StringComparison.OrdinalIgnoreCase) && this.RequireSsl)
            {
                throw new InvalidOperationException("AccessUri must use the HTTPS protocol.");
            }
            if (base.CallbackUri == null)
            {
                throw new InvalidOperationException("CallbackUri is not specified. 'redirect_uri' is a required attribute.");
            }
            if (!base.CallbackUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("CallbackUri be an absolute uri.");
            }
            if (string.IsNullOrEmpty(base.ConsumerKey))
            {
                throw new InvalidOperationException("ConsumerKey is not specified. 'client_id' is a required attribute.");
            }
            if (string.IsNullOrEmpty(base.ConsumerSecret))
            {
                throw new InvalidOperationException("ConsumerSecret is not specified. 'client_secret' is a required attribute.");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'code'");
            }
            List<Parameter> additional = new List<Parameter> {
                new Parameter("client_id", base.ConsumerKey),
                new Parameter("client_secret", base.ConsumerSecret),
                new Parameter("code", code),
                new Parameter("redirect_uri", base.CallbackUri.ToString())
            };
            Url url = new Url(this.AccessUri, string.Empty);
            return url.ToUri(additional);
        }

        public Uri GetAccessTokenUrl(IToken token, string verifier)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (base.CallbackUri == null)
            {
                throw new InvalidOperationException("CallbackUri is not specified. 'redirect_uri' is a required attribute.");
            }
            if (this.AccessUri == null)
            {
                throw new InvalidOperationException("AccessUri is not specified.");
            }
            if (!this.AccessUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("AccessUri be an absolute uri.");
            }
            if (string.IsNullOrEmpty(base.ConsumerKey))
            {
                throw new InvalidOperationException("ConsumerKey is not specified. 'oauth_consumer_key' is a required attribute.");
            }
            if (string.IsNullOrEmpty(base.ConsumerSecret))
            {
                throw new InvalidOperationException("ConsumerSecret is not specified. 'oauth_consumer_secret' is a required attribute.");
            }
            if (string.IsNullOrEmpty(token.Value))
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token'");
            }
            if (string.IsNullOrEmpty(token.Secret))
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token_secret'");
            }
            if (string.IsNullOrEmpty(verifier))
            {
                throw new InvalidOperationException("The specified request is invalid. Paramter: 'oauth_verifier' is missing.");
            }
            List<Parameter> parameters = new List<Parameter> {
                Parameter.Version(),
                Parameter.Nonce(),
                Parameter.Timestamp(),
                Parameter.ConsumerKey(base.ConsumerKey),
                Parameter.Token(token.Value),
                Parameter.Verifier(verifier)
            };
            Url url = new Url(this.AccessUri, string.Empty);
            DevExpress.Utils.OAuth.Signature signature = this.Signature;
            if (signature == DevExpress.Utils.OAuth.Signature.HMACSHA1)
            {
                parameters.Add(Parameter.SignatureMethod(DevExpress.Utils.OAuth.Signature.HMACSHA1));
                Signing.HmaSha1.Sign(this.HttpMethod, url, base.ConsumerSecret, token.Secret, parameters);
            }
            else
            {
                if (signature != DevExpress.Utils.OAuth.Signature.PLAINTEXT)
                {
                    throw new NotImplementedException();
                }
                parameters.Add(Parameter.SignatureMethod(DevExpress.Utils.OAuth.Signature.PLAINTEXT));
                Signing.PlainText.Sign(base.ConsumerSecret, token.Secret, parameters);
            }
            return url.ToUri(parameters);
        }

        public string GetAuthorizationHeader(Uri requestUri) => 
            GetAuthorizationHeader(requestUri, this.AccessToken, this.HttpMethod, this.Signature);

        public static string GetAuthorizationHeader(Uri requestUri, IToken token, string httpMethod, DevExpress.Utils.OAuth.Signature signature)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            if (!requestUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Request URI be absolute.");
            }
            if (token == null)
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token'");
            }
            if (string.IsNullOrEmpty(token.Value))
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token'");
            }
            if (string.IsNullOrEmpty(token.Secret))
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token_secret'");
            }
            if (string.IsNullOrEmpty(httpMethod))
            {
                throw new ArgumentNullException("httpMethod");
            }
            if (string.IsNullOrEmpty(token.ConsumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }
            if (string.IsNullOrEmpty(token.ConsumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }
            List<Parameter> parameters = new List<Parameter> {
                Parameter.Nonce(),
                Parameter.Timestamp(),
                Parameter.Version(),
                new Parameter("oauth_consumer_key", token.ConsumerKey),
                new Parameter("oauth_token", token.Value)
            };
            Url url = new Url(requestUri, string.Empty);
            if (signature == DevExpress.Utils.OAuth.Signature.HMACSHA1)
            {
                parameters.Add(new Parameter("oauth_signature_method", "HMAC-SHA1"));
                Signing.HmaSha1.Sign(httpMethod, url, token.ConsumerSecret, token.Secret, parameters);
            }
            else
            {
                if (signature != DevExpress.Utils.OAuth.Signature.PLAINTEXT)
                {
                    throw new NotImplementedException();
                }
                parameters.Add(new Parameter("oauth_signature_method", "PLAINTEXT"));
                Signing.PlainText.Sign(token.ConsumerSecret, token.Secret, parameters);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("OAuth");
            IEnumerable<Parameter>[] args = new IEnumerable<Parameter>[] { parameters };
            IList<Parameter> list2 = Parameters.Sort(args);
            for (int i = 0; i < list2.Count; i++)
            {
                if (!list2[i].IsEmpty)
                {
                    if (i == 0)
                    {
                        builder.Append(" ");
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    Parameter parameter = list2[i];
                    builder.AppendFormat("{0}=\"{1}\"", Escaping.Escape(list2[i].Name), Escaping.Escape(parameter.Value));
                }
            }
            return builder.ToString();
        }

        public Uri GetAuthorizeTokenUrl() => 
            this.GetAuthorizeTokenUrl(string.Empty);

        public Uri GetAuthorizeTokenUrl(string version)
        {
            if ((version == null) || (version.Length == 0))
            {
                version = "1.0";
            }
            if (!string.Equals(version, "1.0") && !string.Equals(version, "2.0"))
            {
                throw new ArgumentException("The specified OAuth version is not supported.", "version");
            }
            if (this.AuthorizeUri == null)
            {
                throw new InvalidOperationException("AuthorizeUri is not specified.");
            }
            if (!this.AuthorizeUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("AuthorizeUri be an absolute uri.");
            }
            if (!string.Equals(this.AuthorizeUri.Scheme, "HTTPS", StringComparison.OrdinalIgnoreCase) && this.RequireSsl)
            {
                throw new InvalidOperationException("AuthorizeUri must use the HTTPS protocol.");
            }
            if (string.IsNullOrEmpty(base.ConsumerKey))
            {
                throw new InvalidOperationException("ConsumerKey is not specified. 'client_id' is a required attribute.");
            }
            if (base.CallbackUri == null)
            {
                throw new InvalidOperationException("CallbackUri is not specified. 'redirect_uri' is a required attribute.");
            }
            if (!base.CallbackUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("CallbackUri be an absolute uri.");
            }
            if (version != "1.0")
            {
                Parameter[] parameterArray2 = new Parameter[] { new Parameter("client_id", base.ConsumerKey), new Parameter("redirect_uri", base.CallbackUri.ToString()) };
                return new Url(this.AuthorizeUri, string.Empty).ToUri(parameterArray2);
            }
            if ((this.RequestToken == null) || this.RequestToken.IsEmpty)
            {
                throw new InvalidOperationException("The specified request token is invalid. Paramter: 'oauth_token'");
            }
            Parameter[] additional = new Parameter[] { Parameter.Token(this.RequestToken.Value) };
            return new Url(this.AuthorizeUri, string.Empty).ToUri(additional);
        }

        public IToken GetRequestToken()
        {
            if ((this._requestToken != null) && !this._requestToken.IsEmpty)
            {
                return this._requestToken;
            }
            using (WebResponse response = CreateHttpRequest(this.GetRequestTokenUrl(), this.HttpMethod, this.Cookies).GetResponse())
            {
                this._requestToken = new Token(Parameters.Parse(response), base.ConsumerKey, base.ConsumerSecret, base.CallbackUri.ToString(), "1.0");
                return this._requestToken;
            }
        }

        public Uri GetRequestTokenUrl()
        {
            if (this.RequestUri == null)
            {
                throw new InvalidOperationException("RequestUri is not specified.");
            }
            if (!this.RequestUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("RequestUri be an absolute uri.");
            }
            if (string.IsNullOrEmpty(base.ConsumerKey))
            {
                throw new InvalidOperationException("ConsumerKey is not specified. 'oauth_consumer_key' is a required attribute.");
            }
            if (string.IsNullOrEmpty(base.ConsumerSecret))
            {
                throw new InvalidOperationException("ConsumerSecret is not specified. 'oauth_consumer_secret' is a required attribute.");
            }
            if (base.CallbackUri == null)
            {
                throw new InvalidOperationException("CallbackUri is not specified. 'oauth_callback' is a required attribute.");
            }
            List<Parameter> parameters = new List<Parameter> {
                Parameter.Version(),
                Parameter.Nonce(),
                Parameter.Timestamp(),
                Parameter.ConsumerKey(base.ConsumerKey),
                Parameter.Callback(base.CallbackUri.ToString())
            };
            Url url = new Url(this.RequestUri, string.Empty);
            DevExpress.Utils.OAuth.Signature signature = this.Signature;
            if (signature == DevExpress.Utils.OAuth.Signature.HMACSHA1)
            {
                parameters.Add(Parameter.SignatureMethod(DevExpress.Utils.OAuth.Signature.HMACSHA1));
                Signing.HmaSha1.Sign(this.HttpMethod, url, base.ConsumerSecret, string.Empty, parameters);
            }
            else
            {
                if (signature != DevExpress.Utils.OAuth.Signature.PLAINTEXT)
                {
                    throw new NotImplementedException();
                }
                parameters.Add(Parameter.SignatureMethod(DevExpress.Utils.OAuth.Signature.PLAINTEXT));
                Signing.PlainText.Sign(base.ConsumerSecret, string.Empty, parameters);
            }
            return url.ToUri(parameters);
        }

        public string GetResource(Uri requestUri) => 
            this.GetResource(requestUri, this.AccessToken, "1.0");

        public string GetResource(Uri requestUri, IToken token) => 
            this.GetResource(requestUri, token, "1.0");

        public string GetResource(Uri requestUri, IToken token, string version)
        {
            string str3;
            if ((version == null) || (version.Length == 0))
            {
                version = "1.0";
            }
            HttpWebRequest request = CreateHttpRequest(requestUri, this.HttpMethod, this.Cookies, token, this.Signature, version);
            try
            {
                HttpStatusCode oK = HttpStatusCode.OK;
                string message = string.Empty;
                string str2 = string.Empty;
                Trace.TraceInformation(request.RequestUri.ToString());
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    oK = response.StatusCode;
                    message = response.StatusDescription;
                    str2 = response.Headers["Location"];
                    if (oK == HttpStatusCode.OK)
                    {
                        return ParseResource(response);
                    }
                }
                if ((oK == HttpStatusCode.Found) && !string.IsNullOrEmpty(str2))
                {
                    using (HttpWebResponse response2 = (HttpWebResponse) CreateHttpRequest(new Uri(str2, UriKind.Absolute), this.HttpMethod, this.Cookies, token, this.Signature, version).GetResponse())
                    {
                        message = response2.StatusDescription;
                        if (response2.StatusCode == HttpStatusCode.OK)
                        {
                            return ParseResource(response2);
                        }
                    }
                }
                throw new WebException(message);
            }
            catch
            {
                throw;
            }
            return str3;
        }

        private static void OnHttpResponse(IAsyncResult asyncResult)
        {
            ConsumerOperation asyncState = (ConsumerOperation) asyncResult.AsyncState;
            try
            {
                using (WebResponse response = asyncState.Request.EndGetResponse(asyncResult))
                {
                    asyncState.Complete(ConsumerOperation.GetResponseBytes(response), false);
                    AbortHttpRequest(asyncState.Request);
                }
            }
            catch (Exception exception1)
            {
                asyncState.CompleteWithError(exception1, false);
            }
        }

        public static string ParseResource(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                return ParseResource(reader);
            }
        }

        public static string ParseResource(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            return reader.ReadToEnd();
        }

        public static string ParseResource(WebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            using (Stream stream = response.GetResponseStream())
            {
                return ParseResource(stream);
            }
        }

        public string HttpMethod
        {
            get => 
                this._httpMethod;
            set => 
                this._httpMethod = value;
        }

        public Uri RequestUri
        {
            get => 
                this._requestUri;
            set => 
                this._requestUri = value;
        }

        public Uri AuthorizeUri
        {
            get => 
                this._authorizeUri;
            set => 
                this._authorizeUri = value;
        }

        public Uri AccessUri
        {
            get => 
                this._accessUri;
            set => 
                this._accessUri = value;
        }

        public DevExpress.Utils.OAuth.Signature Signature
        {
            get => 
                this._signature;
            set => 
                this._signature = value;
        }

        public IToken RequestToken
        {
            get => 
                this._requestToken;
            set => 
                this._requestToken = value;
        }

        public CookieContainer Cookies
        {
            get => 
                this._cookies;
            set => 
                this._cookies = value;
        }

        public bool RequireSsl
        {
            get => 
                this._requireSsl;
            set => 
                this._requireSsl = value;
        }

        public IToken AccessToken
        {
            get => 
                this._accessToken;
            set => 
                this._accessToken = value;
        }
    }
}

