namespace DevExpress.XtraPrinting.Native.WebClientUIControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml.Linq;

    public static class JsonGenerator
    {
        static JsonGenerator();
        public static void AppendQuotedValue(StringBuilder builder, string value, bool isKey = false);
        private static void AppendSafeValue(StringBuilder builder, string value);
        public static string GetJsonAttributeValue(XElement element);
        public static XDocument JsonToCleanXmlDeclaration(string json);
        private static void SafeCleanXmlDeclaration(XDocument document);
        public static bool TryProcessJsonAttribute(XElement element, out string attributeName);

        public static System.Text.Encoding Encoding { get; private set; }
    }
}

