namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Collections.Generic;

    public class ValidationScope
    {
        private RequiredParameters _parameters;
        private string _httpMethod;
        private DevExpress.Utils.OAuth.Provider.ServiceProvider _serviceProvider;
        private IList<ValidationError> _errors = new List<ValidationError>();

        public ValidationScope(string httpMethod, Uri uri, string authorizationHeader, DevExpress.Utils.OAuth.Provider.ServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(httpMethod))
            {
                throw new ArgumentException("httpMethod is null or empty.", "httpMethod");
            }
            if ((httpMethod != "GET") && ((httpMethod != "POST") && ((httpMethod != "DELETE") && ((httpMethod != "PUT") && (httpMethod != "UPDATE")))))
            {
                throw new ArgumentException("The specified http method is not supported.", "httpMethod");
            }
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider", "serviceProvider is null.");
            }
            this._httpMethod = httpMethod;
            this._serviceProvider = serviceProvider;
            this._parameters = new RequiredParameters(new Url(uri, authorizationHeader));
        }

        public void AddError(ValidationError error)
        {
            if (error == null)
            {
                throw new ArgumentNullException("error", "error is null.");
            }
            this._errors.Add(error);
        }

        public IToken ValidateAuthorizedToken()
        {
            IRequestTokenStore requestTokenStore = this.ServiceProvider.RequestTokenStore;
            if (requestTokenStore == null)
            {
                throw new InvalidOperationException("Request token store is not configured.");
            }
            IToken token = requestTokenStore.GetToken(this._parameters["oauth_token"].Value);
            if (((token != null) && !token.IsEmpty) && (((token.Value == this._parameters["oauth_token"].Value) && (!string.IsNullOrEmpty(token.AuthenticationTicket) && !string.IsNullOrEmpty(token.Verifier))) && (token.Verifier == this._parameters["oauth_verifier"].Value)))
            {
                return token;
            }
            this.AddError(new ValidationError(0x191, "Invalid / expired Token: " + this._parameters["oauth_token"].Value));
            return null;
        }

        public IConsumer ValidateConsumer()
        {
            IConsumerStore consumerStore = this.ServiceProvider.ConsumerStore;
            if (consumerStore == null)
            {
                throw new InvalidOperationException("Consumer store is not configured.");
            }
            IConsumer consumer = consumerStore.GetConsumer(this._parameters["oauth_consumer_key"].Value);
            if ((consumer != null) && (this._parameters["oauth_consumer_key"].Value == consumer.ConsumerKey))
            {
                return consumer;
            }
            this.AddError(new ValidationError(0x191, "Invalid Consumer Key"));
            return null;
        }

        public bool ValidateNonce()
        {
            if (this.Parameters["oauth_nonce"].IsNonce())
            {
                return true;
            }
            this.AddError(new ValidationError(0x191, "Invalid / used nonce: " + this.Parameters["oauth_nonce"].Value));
            return false;
        }

        public bool ValidateParameters()
        {
            bool flag;
            using (IEnumerator<ValidationError> enumerator = this.Parameters.Errors.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    ValidationError current = enumerator.Current;
                    this.AddError(new ValidationError(current.StatusCode, current.Message));
                    flag = false;
                }
                else
                {
                    return true;
                }
            }
            return flag;
        }

        public bool ValidateSignature(string consumerSecret, string tokenSecret)
        {
            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentException("consumerSecret is null or empty.", "consumerSecret");
            }
            tokenSecret ??= string.Empty;
            Parameter empty = Parameter.Empty;
            string str = this._parameters["oauth_signature_method"].Value;
            if (str == "PLAINTEXT")
            {
                empty = Signing.PlainText.Sign(consumerSecret, tokenSecret);
                if (string.Equals(Escaping.Escape(empty.Value), this._parameters["oauth_signature"].Value, StringComparison.Ordinal))
                {
                    return true;
                }
                this.AddError(new ValidationError(0x191, "Invalid signature. Signature Base: " + empty.Value));
                return false;
            }
            if (str != "HMAC-SHA1")
            {
                this.AddError(new ValidationError(400, "Unsupported signature method: " + this._parameters["oauth_signature_method"].Value));
                return false;
            }
            empty = Signing.HmaSha1.Sign(Signing.HmaSha1.Base(this.HttpMethod, this._parameters.Url.ToUrl(), null), consumerSecret, tokenSecret);
            if (string.Equals(empty.Value, this._parameters["oauth_signature"].Value, StringComparison.Ordinal))
            {
                return true;
            }
            this.AddError(new ValidationError(0x191, "Invalid signature. Signature Base: " + empty.Value));
            return false;
        }

        public bool ValidateTimestamp()
        {
            if (this.Parameters["oauth_timestamp"].IsTimestamp())
            {
                return true;
            }
            this.AddError(new ValidationError(0x191, "Invalid timestamp: " + this.Parameters["oauth_timestamp"].Value));
            return false;
        }

        public IToken ValidateUnauthorizedToken()
        {
            IRequestTokenStore requestTokenStore = this.ServiceProvider.RequestTokenStore;
            if (requestTokenStore == null)
            {
                throw new InvalidOperationException("Request token store is not configured.");
            }
            IToken token = requestTokenStore.GetToken(this._parameters["oauth_token"].Value);
            if (((token != null) && !token.IsEmpty) && ((token.Value == this._parameters["oauth_token"].Value) && !string.IsNullOrEmpty(token.Verifier)))
            {
                return token;
            }
            this.AddError(new ValidationError(0x191, "Invalid / expired Token: " + this._parameters["oauth_token"].Value));
            return null;
        }

        public RequiredParameters Parameters =>
            this._parameters;

        public string HttpMethod =>
            this._httpMethod;

        public DevExpress.Utils.OAuth.Provider.ServiceProvider ServiceProvider =>
            this._serviceProvider;

        public IEnumerable<ValidationError> Errors =>
            this._errors;

        public int ErrorCount =>
            this._errors.Count;
    }
}

