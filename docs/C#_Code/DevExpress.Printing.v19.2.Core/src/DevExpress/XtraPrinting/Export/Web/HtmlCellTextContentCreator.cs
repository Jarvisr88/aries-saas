namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class HtmlCellTextContentCreator
    {
        private readonly DXHtmlContainerControl cell;

        public HtmlCellTextContentCreator(DXHtmlContainerControl cell)
        {
            Guard.ArgumentNotNull(cell, "cell");
            this.cell = cell;
        }

        public void CreateContent(string text, BrickStyle style, RectangleF bounds, Measurer measurer)
        {
            bounds = GraphicsUnitConverter.DipToDoc(bounds);
            float dpi = GraphicsDpi.UnitToDpiI(GraphicsUnit.Document);
            text = HotkeyPrefixHelper.PreprocessHotkeyPrefixesInString(text, style);
            RectangleF ef = style.DeflateBorderWidth(bounds, dpi);
            ef.Size = style.Padding.Deflate(ef.Size, dpi);
            this.SetCellContent(text, style.StringFormat.Value, style.TextAlignment, style.Font, ef, measurer);
        }

        private static HtmlStringCreator CreateHtmlStringCreator(TextAlignment textAlignment, StringFormat stringFormat, RectangleF bounds) => 
            !IsJustified(textAlignment) ? new HtmlStringCreator(stringFormat, bounds.Width, bounds.Height) : new JustifyHtmlStringCreator(stringFormat, bounds.Width, bounds.Height);

        private static bool IsJustified(TextAlignment textAlignment) => 
            (textAlignment == TextAlignment.TopJustify) || ((textAlignment == TextAlignment.MiddleJustify) || (textAlignment == TextAlignment.BottomJustify));

        private void SetCellContent(string text, StringFormat stringFormat, TextAlignment textAlignment, Font font, RectangleF bounds, Measurer measurer)
        {
            CreateHtmlStringCreator(textAlignment, stringFormat, bounds).AddHtmlControls(this.cell, text, measurer, font);
        }
    }
}

