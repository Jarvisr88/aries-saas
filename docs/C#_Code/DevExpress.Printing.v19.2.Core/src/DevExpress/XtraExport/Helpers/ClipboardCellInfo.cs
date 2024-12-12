namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils.Text;
    using DevExpress.Utils.Text.Internal;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ClipboardCellInfo
    {
        public ClipboardCellInfo(object value, string displayValue, XlCellFormatting formatting, bool allowHtmlDraw = false)
        {
            this.Value = value;
            this.DisplayValue = displayValue.Replace("\0", string.Empty);
            this.Formatting = formatting;
            this.IsHyperlink = false;
            this.DisplayValueFormatted = null;
            this.<AllowHtmlDraw>k__BackingField = allowHtmlDraw;
            if (allowHtmlDraw && (formatting.Font != null))
            {
                this.CreateFormattedDisplayValue(displayValue, formatting.Font.Name, (float) formatting.Font.Size);
            }
        }

        private XlFont CreateFont(string fontName, StringFontSettings settings) => 
            new XlFont { 
                Color = settings.Color,
                Size = settings.Size,
                Bold = settings.Style.HasFlag(FontStyle.Bold),
                Italic = settings.Style.HasFlag(FontStyle.Italic),
                Underline = settings.Style.HasFlag(FontStyle.Underline) ? XlUnderlineType.Single : XlUnderlineType.None,
                StrikeThrough = settings.Style.HasFlag(FontStyle.Strikeout),
                Name = fontName
            };

        private void CreateFormattedDisplayValue(string formattedCaption, string fontName, float fontSize)
        {
            this.DisplayValueFormatted = new XlRichTextString();
            foreach (StringBlock block in StringParser.Parse(fontSize, formattedCaption, false))
            {
                XlRichTextRun item = new XlRichTextRun(block.Text, this.CreateFont(fontName, block.FontSettings));
                this.DisplayValueFormatted.Runs.Add(item);
            }
        }

        public void UpdateRowInfo(Color backColor, Color foreColor, XlFont font, XlBorder border, object value, string displayValue)
        {
            if (this.Formatting.Fill == null)
            {
                XlFill fill1 = new XlFill();
                fill1.PatternType = XlPatternType.Solid;
                this.Formatting.Fill = fill1;
            }
            this.Formatting.Fill.BackColor = backColor;
            this.Formatting.Fill.ForeColor = backColor;
            this.Formatting.Font = font;
            this.Formatting.Font.Color = foreColor;
            this.Formatting.Border = border;
            this.Value = value;
            this.DisplayValue = displayValue;
            if (this.DisplayValueFormatted != null)
            {
                this.DisplayValueFormatted.Runs.First<XlRichTextRun>().Text = this.DisplayValue;
            }
        }

        public bool AllowHtmlDraw { get; }

        public string DisplayValue { get; private set; }

        public XlRichTextString DisplayValueFormatted { get; private set; }

        public XlCellFormatting Formatting { get; private set; }

        public bool IsHyperlink { get; set; }

        public object Value { get; private set; }
    }
}

