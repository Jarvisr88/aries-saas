namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public static class Signing
    {
        public static class HmaSha1
        {
            public static string Base(string httpMethod, Url url, IEnumerable<Parameter> parameters) => 
                Base(httpMethod, url, true, parameters);

            public static string Base(string httpMethod, Url url, bool lowerCase, IEnumerable<Parameter> parameters)
            {
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(httpMethod))
                {
                    builder.Append(Escaping.Escape(httpMethod.ToUpperInvariant()));
                }
                if (!string.IsNullOrEmpty(url.Uri))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }
                    if (lowerCase)
                    {
                        builder.Append(Escaping.Escape(url.Uri.ToLowerInvariant()));
                    }
                    else
                    {
                        builder.Append(Escaping.Escape(url.Uri));
                    }
                }
                bool flag = true;
                IEnumerable<Parameter>[] args = new IEnumerable<Parameter>[] { url.QueryParams, parameters };
                foreach (Parameter parameter in Parameters.Sort(args))
                {
                    if (!string.IsNullOrEmpty(parameter.Name) && (!string.Equals(parameter.Name, "oauth_signature", StringComparison.InvariantCultureIgnoreCase) && (!string.Equals(parameter.Name, "oauth_token_secret", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(parameter.Name, "oauth_consumer_secret", StringComparison.InvariantCultureIgnoreCase))))
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(flag ? "&" : Escaping.Escape("&"));
                        }
                        flag = false;
                        builder.Append(Escaping.Escape(parameter.Name));
                        if (!string.IsNullOrEmpty(parameter.Value))
                        {
                            builder.Append(Escaping.Escape("="));
                            builder.Append(Escaping.Escape(Escaping.Escape(parameter.Value)));
                        }
                    }
                }
                return builder.ToString();
            }

            public static string Hash(string sigBase, string key)
            {
                using (HMACSHA1 hmacsha = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
                {
                    return Convert.ToBase64String(hmacsha.ComputeHash(Encoding.UTF8.GetBytes(sigBase)));
                }
            }

            public static string Hash(string sigBase, string consumerSecret, string tokenSecret) => 
                Hash(sigBase, $"{Escaping.Escape(consumerSecret)}&{Escaping.Escape(tokenSecret)}");

            public static Parameter Sign(string sigBase, string consumerSecret, string tokenSecret) => 
                new Parameter("oauth_signature", Hash(sigBase, consumerSecret, tokenSecret));

            public static void Sign(string httpMethod, Url url, string consumerSecret, string tokenSecret, ICollection<Parameter> parameters)
            {
                parameters.Add(Sign(Base(httpMethod, url, parameters), consumerSecret, tokenSecret));
            }
        }

        public static class PlainText
        {
            public static Parameter Sign(string consumerSecret, string tokenSecret)
            {
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(consumerSecret))
                {
                    builder.Append(Escaping.Escape(consumerSecret));
                }
                builder.Append("&");
                if (!string.IsNullOrEmpty(tokenSecret))
                {
                    builder.Append(Escaping.Escape(tokenSecret));
                }
                return new Parameter("oauth_signature", Escaping.Escape(builder.ToString()));
            }

            public static void Sign(string consumerSecret, string tokenSecret, ICollection<Parameter> parameters)
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException("params");
                }
                parameters.Add(Sign(consumerSecret, tokenSecret));
            }
        }
    }
}

