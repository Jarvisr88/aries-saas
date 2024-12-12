namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Windows;

    internal class FormatConverter
    {
        private readonly DevExpress.Xpf.Core.ConditionalFormatting.Format Format;

        public FormatConverter(DevExpress.Xpf.Core.ConditionalFormatting.Format format)
        {
            this.Format = format;
        }

        public XlDifferentialFormatting GetXlDifferentialFormatting() => 
            new XlDifferentialFormatting { 
                Fill = this.GetXlFill(),
                Font = this.GetXlFont()
            };

        private XlFill GetXlFill()
        {
            if ((this.Format == null) || (this.Format.Background == null))
            {
                return null;
            }
            return new XlFill { 
                BackColor = FormatConditionRuleBase.GetColor(this.Format.Background, true),
                PatternType = XlPatternType.Solid
            };
        }

        private XlFont GetXlFont()
        {
            XlFont font = new XlFont();
            if (this.Format != null)
            {
                if (this.Format.FontSize > 1.0)
                {
                    font.Size = this.Format.FontSize;
                }
                if (this.Format.FontFamily != null)
                {
                    font.Name = this.Format.FontFamily.Source;
                }
                font.Color = FormatConditionRuleBase.GetColor(this.Format.Foreground, true);
                font.Bold = this.Format.FontWeight >= FontWeights.Bold;
                font.StrikeThrough = this.IsStrikeThrough();
                font.Italic = (this.Format.FontStyle == FontStyles.Italic) || (this.Format.FontStyle == FontStyles.Oblique);
                font.Extend = this.Format.FontStretch >= FontStretches.Expanded;
                font.Condense = this.Format.FontStretch <= FontStretches.Condensed;
                if (this.IsUnderline())
                {
                    font.Underline = XlUnderlineType.Single;
                }
            }
            return font;
        }

        private bool IsStrikeThrough() => 
            (this.Format.TextDecorations != null) && this.Format.TextDecorations.Contains(TextDecorations.Strikethrough[0]);

        private bool IsUnderline() => 
            (this.Format.TextDecorations != null) && this.Format.TextDecorations.Contains(TextDecorations.Underline[0]);
    }
}

