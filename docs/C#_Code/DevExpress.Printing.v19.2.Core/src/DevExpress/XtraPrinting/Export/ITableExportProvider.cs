namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Drawing;

    public interface ITableExportProvider
    {
        void SetCellImage(System.Drawing.Image image, TableCellImageInfo imageInfo, Rectangle bounds, PaddingInfo padding);
        void SetCellShape(ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding);
        void SetCellText(object textValue);

        DevExpress.XtraPrinting.Native.ExportContext ExportContext { get; }

        BrickViewData CurrentData { get; }
    }
}

