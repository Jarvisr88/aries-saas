namespace DevExpress.Utils.About
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class XmlHttpRequest
    {
        public static WebRequest Create(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentOutOfRangeException("uri", $"The Request URI must be absolute. URI: {uri}");
            }
            return WebRequest.Create(uri.ToString());
        }

        public static string GetRequestUriString(Uri baseUri, string relativeUri)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentNullException("relativeUri");
            }
            int startIndex = 0;
            while ((startIndex < relativeUri.Length) && ((relativeUri[startIndex] == '/') || (relativeUri[startIndex] == ' ')))
            {
                startIndex++;
            }
            string str = baseUri.ToString();
            int length = str.Length;
            while ((length > 0) && ((str[length - 1] == '/') || (str[length - 1] == ' ')))
            {
                length--;
            }
            return $"{str.Substring(0, length)}/{relativeUri.Substring(startIndex)}";
        }

        public static string GetRequestUriString(Uri baseUri, string relativeUri, params object[] args) => 
            GetRequestUriString(baseUri, string.Format(relativeUri, args));

        public static byte[] GetResponseBytes(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                byte[] buffer = null;
                GetResponseBytes(stream, ref buffer);
                return buffer;
            }
        }

        public static void GetResponseBytes(Stream source, ref byte[] buffer)
        {
            int num;
            byte[] buffer2 = new byte[0x1000];
            while ((num = source.Read(buffer2, 0, buffer2.Length)) > 0)
            {
                byte[] dst = new byte[num + (((buffer == null) || (buffer.Length == 0)) ? 0 : buffer.Length)];
                if ((buffer != null) && (buffer.Length != 0))
                {
                    Buffer.BlockCopy(buffer, 0, dst, 0, buffer.Length);
                }
                Buffer.BlockCopy(buffer2, 0, dst, dst.Length - num, num);
                buffer = dst;
            }
        }

        public static XmlHttpResponse Submit(string uriString, string method, string authorization) => 
            Submit(uriString, method, authorization, null);

        public static XmlHttpResponse Submit(Uri uri, string method, string authorization) => 
            Submit(uri.ToString(), method, null);

        public static XmlHttpResponse Submit(string uriString, string method, string authorization, byte[] content)
        {
            HttpWebRequest request = (HttpWebRequest) Create(new Uri(uriString, UriKind.RelativeOrAbsolute));
            request.Method = method;
            request.Headers["X-HTTP-Method-Override"] = method;
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers["X-Authorization"] = authorization;
            }
            request.ContentType = "app/x-www-form-urlencoded";
            if ((content != null) && (content.Length != 0))
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                }
            }
            using (WebResponse response = request.GetResponse())
            {
                return new XmlHttpResponse(GetResponseBytes(response), response.ContentType);
            }
        }

        public static XmlHttpResponse Submit(Uri uri, string method, string authorization, byte[] content) => 
            Submit(uri.ToString(), method, authorization, content);

        public static XmlHttpResponse Submit(Uri baseUri, string relativeUri, string method, string authorization, byte[] content, params object[] args) => 
            Submit(GetRequestUriString(baseUri, relativeUri, args), method, authorization, content);

        [StructLayout(LayoutKind.Sequential)]
        public struct XmlHttpResponse
        {
            public static readonly XmlHttpRequest.XmlHttpResponse Empty;
            private byte[] _content;
            private string _contentType;
            public XmlHttpResponse(byte[] content, string contentType)
            {
                this._content = content;
                this._contentType = contentType;
            }

            public byte[] Content =>
                this._content;
            public string ContentType =>
                this._contentType;
            public string ContentText =>
                (string.IsNullOrEmpty(this._contentType) || ((this._content == null) || !this._contentType.ToLower().StartsWith("text/"))) ? string.Empty : Encoding.UTF8.GetString(this._content, 0, this._content.Length);
            public override string ToString() => 
                this.ContentText;

            static XmlHttpResponse()
            {
            }
        }
    }
}

