namespace DevExpress.ClipboardSource.SpreadsheetML
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    internal static class Extensions
    {
        internal static TResult Get<T, TResult>(this T @this, Func<T, TResult> get, TResult defaultValue = null, string exceptionMessage = "Unknown exception")
        {
            TResult local;
            if ((@this == null) || (get == null))
            {
                return defaultValue;
            }
            try
            {
                local = get(@this);
            }
            catch
            {
                throw new Exception(exceptionMessage);
            }
            return local;
        }

        public static XAttribute GetAttribute(this XElement element, string attribute)
        {
            try
            {
                return element.Get<XElement, XAttribute>(x => x.Attribute(element.Document.Root.Name.Namespace + attribute), null, "Unknown exception");
            }
            catch
            {
                return null;
            }
        }

        public static XElement GetElement(this XElement element, string elementName)
        {
            try
            {
                return element.Get<XElement, XElement>(x => x.Element(element.Document.Root.Name.Namespace + elementName), null, "Unknown exception");
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<XElement> GetElements(this XElement element, string elementName)
        {
            try
            {
                return element.Get<XElement, IEnumerable<XElement>>(x => x.Elements(element.Document.Root.Name.Namespace + elementName), null, "Unknown exception");
            }
            catch
            {
                return null;
            }
        }
    }
}

