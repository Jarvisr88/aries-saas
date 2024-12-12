namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    internal static class XmlHelper
    {
        public static List<T> GetDescendants<T>(Func<XElement, bool> predicate, Func<XElement, T> selectCondition, string str) => 
            XDocument.Parse(str).Root.Descendants().Elements<XElement>().ToList<XElement>().Where<XElement>(predicate).Select<XElement, T>(selectCondition).ToList<T>();

        public static List<T> GetElements<T>(Func<XElement, bool> predicate, Func<XElement, T> selectCondition, string str) => 
            XDocument.Parse(str).Root.Elements().Elements<XElement>().ToList<XElement>().Where<XElement>(predicate).Select<XElement, T>(selectCondition).ToList<T>();
    }
}

