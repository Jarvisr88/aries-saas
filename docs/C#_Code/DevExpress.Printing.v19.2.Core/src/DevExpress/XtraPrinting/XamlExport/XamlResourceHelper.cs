namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class XamlResourceHelper
    {
        public static bool ExporterRequiresBorderStyle(BrickBase brick)
        {
            BrickXamlExporterBase base2 = BrickXamlExporterFactory.CreateExporter(brick);
            return ((base2 != null) ? base2.RequiresBorderStyle() : false);
        }

        public static bool ExporterRequiresImageResource(BrickBase brick)
        {
            BrickXamlExporterBase base2 = BrickXamlExporterFactory.CreateExporter(brick);
            return ((base2 != null) ? base2.RequiresImageResource() : false);
        }

        public static float GetExporterGraphicsDpi(BrickBase brick)
        {
            BrickXamlExporterBase base2 = BrickXamlExporterFactory.CreateExporter(brick);
            return ((base2 != null) ? base2.GetGraphicsDpi() : 300f);
        }
    }
}

