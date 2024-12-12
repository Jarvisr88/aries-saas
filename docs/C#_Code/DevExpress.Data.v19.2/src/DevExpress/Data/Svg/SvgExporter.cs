namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;

    public class SvgExporter
    {
        private const string SvgFormatVersion = "1.2";
        private static CultureInfo PointsExportCulture;
        private readonly SvgSize size;
        private XmlDocument document;

        static SvgExporter();
        public SvgExporter(SvgSize size);
        private void AddElementToParent(XmlNode parent, XmlElement element);
        private XmlElement AppendNewElementToParent(XmlNode parent, string name);
        private XmlElement CreateElement(string name);
        protected virtual SvgElementDataExportAgent CreateExportAgent(XmlElement xmlElement);
        private XmlElement CreateSvgHead();
        public XmlDocument Export(IList<SvgItem> svgElements, string prefixDefinitions);
        private void Export(XmlNode definitionsContainer, IEnumerable<SvgDefinition> definitions, IDefinitionKeysGenerator keysGenerator);
        private void Export(XmlNode elementsContainer, XmlNode definitionsContainer, IList<SvgItem> svgItems, IDefinitionKeysGenerator keysGenerator);
    }
}

