namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class SvgElementDataImportAgent : SvgElementDataAgentBase
    {
        public SvgElementDataImportAgent(XmlElement element);
        public double GetDouble(string key, IFormatProvider provider);
        public double GetDouble(string key, double defaultValue, IFormatProvider provider);
        public XmlNodeList GetElementsByTag(string name);
        public T GetEnumValue<T>(string key) where T: struct, IFormattable;
        public int GetInt(string key, IFormatProvider provider);
        public int GetInt(string key, int defaultValue, IFormatProvider provider);
        public string GetString(string key);
        private string ProcessRgbSvgColorFormat(string color);
        private string RemovePxPostfix(string data);
        public bool TryGetColor(string key, out Color result);
    }
}

