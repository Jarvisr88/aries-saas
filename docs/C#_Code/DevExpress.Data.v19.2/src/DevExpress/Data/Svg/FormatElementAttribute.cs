namespace DevExpress.Data.Svg
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FormatElementAttribute : Attribute
    {
        private readonly string xmlElementName;

        public FormatElementAttribute(string xmlElementName);

        public string XmlElementName { get; }
    }
}

