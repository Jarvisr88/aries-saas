namespace DevExpress.Data.Svg
{
    using System;
    using System.Globalization;
    using System.Xml;

    public class SvgElementDataAgentBase
    {
        protected static readonly System.Globalization.NumberStyles DecimalStyles;
        protected static readonly System.Globalization.NumberStyles NumberStyles;
        private readonly XmlElement element;

        static SvgElementDataAgentBase();
        public SvgElementDataAgentBase(XmlElement element);

        public XmlElement Element { get; }
    }
}

