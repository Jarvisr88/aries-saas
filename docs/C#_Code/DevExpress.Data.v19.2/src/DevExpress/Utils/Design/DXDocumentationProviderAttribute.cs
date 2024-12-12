namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class DXDocumentationProviderAttribute : Attribute
    {
        private string description;
        private string link;

        public DXDocumentationProviderAttribute(string description, string link)
        {
            this.description = description;
            this.link = link;
        }

        public string GetUrl() => 
            $"{"http://documentation.devexpress.com/"}{this.Link}";

        public string Description =>
            this.description;

        public string Link =>
            this.link;
    }
}

