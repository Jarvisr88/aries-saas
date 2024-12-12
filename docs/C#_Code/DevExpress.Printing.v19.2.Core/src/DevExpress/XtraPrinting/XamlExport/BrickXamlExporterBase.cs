namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class BrickXamlExporterBase
    {
        protected BrickXamlExporterBase()
        {
        }

        protected abstract XamlBrickExportMode GetBrickExportMode();
        public virtual float GetGraphicsDpi() => 
            300f;

        public virtual bool RequiresBorderStyle() => 
            false;

        public virtual bool RequiresImageResource() => 
            false;

        public abstract void WriteBrickToXaml(XamlWriter writer, BrickBase brick, XamlExportContext exportContext, RectangleF clipRect, Action<XamlWriter> declareNamespaces, Action<XamlWriter, object> writeCustomProperties);
        public abstract void WriteEndTags(XamlWriter writer);

        public XamlBrickExportMode BrickExportMode =>
            this.GetBrickExportMode();
    }
}

