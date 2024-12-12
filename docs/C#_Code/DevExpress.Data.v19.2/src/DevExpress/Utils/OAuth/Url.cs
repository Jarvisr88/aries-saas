namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    public struct Url
    {
        private string _uri;
        private string _domain;
        private IEnumerable<Parameter> _queryParams;
        public Url(System.Uri requestUri, string authorizationHeader)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            if (!requestUri.IsAbsoluteUri)
            {
                throw new ArgumentException("Request URI must be absolute.", "requestUri");
            }
            string host = requestUri.Host;
            string scheme = requestUri.Scheme;
            string str3 = string.Empty;
            string absolutePath = requestUri.AbsolutePath;
            if ((string.Equals(scheme, "http", StringComparison.InvariantCultureIgnoreCase) && (requestUri.Port == 80)) || (string.Equals(scheme, "https", StringComparison.InvariantCultureIgnoreCase) && (requestUri.Port == 0x1bb)))
            {
                str3 = string.Empty;
            }
            else
            {
                str3 = ":" + requestUri.Port;
            }
            this._domain = $"{scheme}://{host}{str3}";
            this._uri = this._domain + absolutePath;
            this._queryParams = Parameters.FromUri(requestUri, authorizationHeader);
        }

        public static explicit operator Url(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new Url(new System.Uri(value, UriKind.RelativeOrAbsolute), string.Empty);
            }
            return new Url();
        }

        public string Uri =>
            this._uri;
        public string Domain =>
            this._domain;
        public IEnumerable<Parameter> QueryParams =>
            this._queryParams;
        public IEnumerable<Parameter> GetQueryParams(string name)
        {
            List<Parameter> list = new List<Parameter>();
            if (!string.IsNullOrEmpty(name))
            {
                foreach (Parameter parameter in this._queryParams)
                {
                    if (!parameter.IsEmpty && string.Equals(name, parameter.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        list.Add(parameter);
                    }
                }
            }
            return list;
        }

        public override string ToString() => 
            string.IsNullOrEmpty(this._uri) ? "{}" : $"{this._uri}";

        public System.Uri ToUri(params Parameter[] additional)
        {
            if (string.IsNullOrEmpty(this._uri))
            {
                throw new InvalidOperationException();
            }
            return this.ToUri((IEnumerable<Parameter>) additional);
        }

        public System.Uri ToUri(IEnumerable<Parameter> additional)
        {
            if (string.IsNullOrEmpty(this._uri))
            {
                throw new InvalidOperationException();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(this._uri.ToLowerInvariant());
            bool flag = true;
            IEnumerable<Parameter>[] args = new IEnumerable<Parameter>[] { this._queryParams, additional };
            foreach (Parameter parameter in Parameters.Sort(args))
            {
                if (!flag)
                {
                    builder.Append("&");
                }
                else
                {
                    builder.Append("?");
                    flag = false;
                }
                builder.Append(Escaping.Escape(parameter.Name));
                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    builder.Append("=");
                    builder.Append(Escaping.Escape(parameter.Value));
                }
            }
            return new System.Uri(builder.ToString(), UriKind.Absolute);
        }

        public static string ToDomain(string uriString) => 
            new Url(new System.Uri(uriString), string.Empty).Domain;

        public Url ToUrl() => 
            this.ToUrl(this.QueryParams);

        public Url ToUrl(IEnumerable<Parameter> replace)
        {
            replace ??= new Parameter[0];
            return new Url { 
                _uri = this._uri,
                _queryParams = replace
            };
        }
    }
}

