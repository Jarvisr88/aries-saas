namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SvgShapeVisual : SvgItem
    {
        private SvgBrush CreateFillBrush(SvgElementDataImportAgent dataAgent, double opacity);
        private SvgBrush CreateStrokeBrush(SvgElementDataImportAgent dataAgent, double opacity);
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public override bool FillData(SvgElementDataImportAgent dataAgent);

        public SvgBrush Fill { get; set; }

        public SvgBrush Stroke { get; set; }

        public double StrokeWidth { get; set; }

        public SvgLineCap StrokeCap { get; set; }

        public SvgLineJoin StrokeJoin { get; set; }

        public SvgShapeRendering ShapeRenderingMode { get; set; }

        public float[] DashArray { get; set; }
    }
}

