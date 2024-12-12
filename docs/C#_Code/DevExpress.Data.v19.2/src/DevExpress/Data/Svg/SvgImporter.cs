namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class SvgImporter
    {
        private readonly IList<SvgItem> svgItems;
        private SvgRect bounds;
        private SvgRect viewBox;
        private SvgSize size;
        private XmlNode svgNode;

        public SvgImporter();
        public bool Import(Stream stream);
        public bool IsSvgNode(XmlNode xmlNode);
        private void ProcessingElements(XmlNode node, IList<SvgItem> elementsContainer);
        private void ProcessingRootElement(XmlNode node);
        internal void StartProcessing(XmlNode xmlNode);
        public bool SvgProcessing(XmlNode xmlNode);
        private void UnionItemsBounds(SvgElement svgElement);

        public IList<SvgItem> SvgItems { get; }

        public SvgRect Bounds { get; }

        public SvgRect ViewBox { get; }

        public SvgSize Size { get; }

        public XmlNode SvgNode { get; }

        public bool UseGrouping { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgImporter.<>c <>9;
            public static Action<XmlReaderSettings> <>9__25_0;

            static <>c();
            internal void <Import>b__25_0(XmlReaderSettings settings);
        }
    }
}

