namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;

    [FormatElement("g")]
    public class SvgXmlContentItem : SvgElement
    {
        private string content;
        private SvgPoint location;
        private SvgSize size;

        public SvgXmlContentItem(string content, SvgPoint location, SvgSize size);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public override SvgRect GetBoundaryPoints();
    }
}

