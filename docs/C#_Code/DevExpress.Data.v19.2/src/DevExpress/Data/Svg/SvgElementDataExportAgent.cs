namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing;
    using System.Xml;

    public class SvgElementDataExportAgent : SvgElementDataAgentBase
    {
        public SvgElementDataExportAgent(XmlElement element);
        private static string ColorToRGBHex(Color color, bool isPrintAlpha);
        private static double GetColorOpacityString(Color color);
        protected static string GetColorString(Color color);
        public void SetColorOpacity(string key, Color value, IFormatProvider provider);
        public virtual void SetColorValue(string key, Color value);
        public void SetContent(string value);
        public void SetValue(string key, string value);
        public void SetValue(string key, object value, IFormatProvider provider);
        public void SetXmlContent(string value);
    }
}

