namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Utils;
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    public class FormsAuthenticationMessageInspector : IClientMessageInspector
    {
        private static readonly Uri CookieContainerUri = new Uri("https://DevExpress.ReportServer.ServiceModel.Client.FormsAuthenticationMessageInspector");

        public FormsAuthenticationMessageInspector()
        {
        }

        public FormsAuthenticationMessageInspector(Cookie cookie)
        {
            DevExpress.Utils.Guard.ArgumentNotNull(cookie, "cookie");
            this.SharedCookie = cookie.ToString();
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            HttpResponseMessageProperty property = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            if ((property != null) && (property.Headers[HttpResponseHeader.SetCookie] != null))
            {
                string cookie = FilterOutExpiredCookies(property.Headers[HttpResponseHeader.SetCookie]);
                this.OnSetCookie(cookie);
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                request.Properties.Add(HttpRequestMessageProperty.Name, new HttpRequestMessageProperty());
            }
            HttpRequestMessageProperty property = (HttpRequestMessageProperty) request.Properties[HttpRequestMessageProperty.Name];
            if (property.Headers[HttpRequestHeader.Cookie] != null)
            {
                throw new InvalidOperationException("Unexpected: HTTP Request object has cookie header already.");
            }
            property.Headers.Add(HttpRequestHeader.Cookie, this.SharedCookie);
            return null;
        }

        private static string FilterOutExpiredCookies(string cookiesHeader)
        {
            CookieContainer container = new CookieContainer();
            container.SetCookies(CookieContainerUri, cookiesHeader);
            return container.GetCookieHeader(CookieContainerUri);
        }

        protected virtual void OnSetCookie(string cookie)
        {
            this.SharedCookie = cookie;
        }

        protected string SharedCookie { get; private set; }
    }
}

