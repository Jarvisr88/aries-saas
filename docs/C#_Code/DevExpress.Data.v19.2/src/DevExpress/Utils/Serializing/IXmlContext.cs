namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections;

    public interface IXmlContext
    {
        string ElementName { get; }

        ICollection Attributes { get; }

        ICollection Elements { get; }

        bool XmlDocumentHeader { get; }
    }
}

