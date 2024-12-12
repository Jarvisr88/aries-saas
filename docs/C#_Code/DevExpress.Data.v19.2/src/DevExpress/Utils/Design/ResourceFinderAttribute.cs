namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class ResourceFinderAttribute : Attribute
    {
        private Type resourceFinder;
        private string resourceFile;

        public ResourceFinderAttribute(Type resourceFinder) : this(resourceFinder, "PropertyNamesRes")
        {
        }

        public ResourceFinderAttribute(Type resourceFinder, string resourceFile)
        {
            this.resourceFinder = resourceFinder;
            this.resourceFile = resourceFile;
        }

        public string ResourceFile =>
            this.resourceFile;

        public Type ResourceFinder =>
            this.resourceFinder;
    }
}

