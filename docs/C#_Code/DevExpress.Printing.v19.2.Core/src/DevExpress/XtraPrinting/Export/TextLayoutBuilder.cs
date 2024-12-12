namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Drawing;
    using System.Globalization;

    public class TextLayoutBuilder : LayoutBuilder, ITableExportProvider
    {
        private ExportContext exportContext;
        protected TextBrickViewData fCurrentData;
        private TextExportMode mode;
        private string separator;
        private bool quoteStringsWithSeparators;
        private bool shouldSplitText;
        private bool encodeExecutableContent;

        public TextLayoutBuilder(Document document, ExportContext exportContext, string separator, bool quoteStringsWithSeparators, TextExportMode mode, bool shouldSplitText) : this(document, exportContext, separator, quoteStringsWithSeparators, mode, shouldSplitText, false)
        {
        }

        public TextLayoutBuilder(Document document, ExportContext exportContext, string separator, bool quoteStringsWithSeparators, TextExportMode mode, bool shouldSplitText, bool encodeExecutableContent) : base(document)
        {
            this.exportContext = exportContext;
            this.separator = separator;
            this.quoteStringsWithSeparators = quoteStringsWithSeparators;
            this.mode = mode;
            this.shouldSplitText = shouldSplitText;
            this.encodeExecutableContent = encodeExecutableContent;
        }

        protected override ILayoutControl CreateLayoutControl(BrickViewData data, Brick brick)
        {
            this.fCurrentData = (TextBrickViewData) data;
            ((BrickExporter) BrickBaseExporter.GetExporter(this.exportContext, this.fCurrentData.TableCell)).FillTextTableCell(this, this.shouldSplitText);
            ITextLayoutTable texts = this.fCurrentData.Texts;
            if (texts == null)
            {
                return null;
            }
            HotkeyPrefixHelper.PreprocessHotkeyPrefixesInTextLayoutTable(texts, data.Style);
            if (this.quoteStringsWithSeparators || this.encodeExecutableContent)
            {
                ProcessTexts(texts, this.separator, this.quoteStringsWithSeparators, this.encodeExecutableContent);
            }
            return data;
        }

        void ITableExportProvider.SetCellImage(System.Drawing.Image image, TableCellImageInfo imageInfo, Rectangle bounds, PaddingInfo padding)
        {
        }

        void ITableExportProvider.SetCellShape(ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding)
        {
        }

        void ITableExportProvider.SetCellText(object textValue)
        {
            if (textValue == string.Empty)
            {
                this.fCurrentData.Texts = SimpleTextLayoutTable.Empty;
            }
            if ((textValue is string[]) && (((string[]) textValue).Length > 1))
            {
                this.fCurrentData.Texts = new TextLayoutTable((string[]) textValue);
            }
            if (textValue is string)
            {
                this.fCurrentData.Texts = new SimpleTextLayoutTable((string) textValue);
            }
        }

        protected override ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect)
        {
            BrickExporter exporter = BrickBaseExporter.GetExporter(this.exportContext, brick) as BrickExporter;
            return base.ToLayoutControls(exporter.GetTextData(this.exportContext, rect), brick);
        }

        protected internal string GetText(string text, object textValue) => 
            ((this.mode != TextExportMode.Value) || (textValue == null)) ? text : textValue.ToString();

        private static string ProcessText(string text, string separator, bool quoteStringsWithSeparators, bool encodeExecutableContent)
        {
            string str = text;
            if (quoteStringsWithSeparators && (text.Contains(separator) || (text.Contains("\"") || (text.Contains(Environment.NewLine) || text.Contains("\n")))))
            {
                str = string.Format("{1}{0}{1}", text.Replace("\"", "\"\""), "\"");
            }
            if (encodeExecutableContent)
            {
                str = CsvExportUtils.Encode(str, '"', CultureInfo.CurrentCulture);
            }
            return str;
        }

        private static void ProcessTexts(ITextLayoutTable texts, string separator, bool quoteStringsWithSeparators, bool encodeExecutableContent)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i] = ProcessText(texts[i], separator, quoteStringsWithSeparators, encodeExecutableContent);
            }
        }

        ExportContext ITableExportProvider.ExportContext =>
            this.exportContext;

        BrickViewData ITableExportProvider.CurrentData =>
            this.fCurrentData;
    }
}

