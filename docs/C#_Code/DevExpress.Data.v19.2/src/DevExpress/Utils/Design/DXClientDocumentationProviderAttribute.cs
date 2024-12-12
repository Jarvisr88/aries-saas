namespace DevExpress.Utils.Design
{
    using System;

    public class DXClientDocumentationProviderAttribute : DXDocumentationProviderAttribute
    {
        public DXClientDocumentationProviderAttribute(string link) : base("Client-Side API Documentation", link)
        {
        }
    }
}

