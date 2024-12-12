namespace DevExpress.Printing.Exports.RtfExport
{
    using DevExpress.XtraPrinting.Export.Rtf;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;

    public class PlainRtfChunkData : RtfChunkData
    {
        public PlainRtfChunkData(List<int> colWidths, List<int> rowHeights) : base(colWidths, rowHeights)
        {
        }

        protected override void AppendRowContent(StringBuilder sb, StringCollection rowDefinition, StringCollection rowContent)
        {
            if (rowDefinition.Count > 0)
            {
                sb.Append(rowDefinition[0]);
                if (rowContent.Count > 0)
                {
                    foreach (string str in rowContent)
                    {
                        sb.Append(str);
                    }
                }
                sb.Append(RtfTags.ParagraphEnd + RtfTags.NewLine + RtfTags.NewLine);
            }
        }

        public override void FillTemplate()
        {
            StringCollection rowContent = new StringCollection { "{}" };
            StringCollection rowDefinition = new StringCollection { "" };
            base.AddRowDefinition(rowDefinition);
            base.AddRowContent(rowContent);
        }

        protected override void InsertDataInContent(int index, int offset, string tag, string data)
        {
            string str = base.rowContents[0][0];
            base.rowContents[0][0] = str.Insert(str.LastIndexOf(tag), data);
        }

        protected override void InsertDataInDefinition(int index, int offset, string tag, string data)
        {
            StringCollection strings = base.rowDefinitions[0];
            strings[0] = strings[0] + data;
        }

        public override void SetAngle(float angle)
        {
        }

        protected override void SetBottomBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.BottomBorder, columnIndex, rowIndex);
        }

        public override void SetContent(string content)
        {
            this.InsertDataInContent(base.currentRowIndex, base.currentColIndex, "}", content);
        }

        public override void SetDirection()
        {
            if (!base.Rtl)
            {
                if (base.RtlLayout)
                {
                    this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", RtfTags.MirrorIndents);
                }
            }
            else if (base.RtlLayout)
            {
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", RtfTags.RightToLeftParagraph);
            }
            else
            {
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", RtfTags.MirrorIndents + RtfTags.RightToLeftParagraph);
            }
        }

        public override void SetFontString(string fontString)
        {
            this.SetContent(fontString + " ");
        }

        public override void SetHAlign()
        {
            this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", RtfAlignmentConverter.ToHorzRtfAlignment(base.style.TextAlignment));
        }

        public override void SetIndent()
        {
            if (base.rowHeights.Count > 1)
            {
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.SpaceBefore, ConvertDIPToTwips((float) base.rowHeights[0])));
            }
            if (base.colRights.Count > 1)
            {
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.LeftIndent, base.colRights[0]));
            }
        }

        protected override void SetLeftBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.LeftBorder, columnIndex, rowIndex);
        }

        public override void SetPadding()
        {
            if ((base.Padding.Top > 0) || (base.rowHeights.Count > 1))
            {
                int num2 = 0;
                if (base.rowHeights.Count > 1)
                {
                    num2 += ConvertDIPToTwips((float) base.rowHeights[0]);
                }
                if (base.Padding.Top > 0)
                {
                    num2 += ConvertToTwips((float) base.Padding.Top, base.Padding.Dpi);
                }
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.SpaceBefore, num2));
            }
            if (base.Padding.Bottom > 0)
            {
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.SpaceAfter, ConvertToTwips((float) base.Padding.Bottom, base.Padding.Dpi)));
            }
            if ((base.colRights.Count > 1) || (base.Padding.Left > 0))
            {
                int num3 = 0;
                if (base.colRights.Count > 1)
                {
                    num3 += base.colRights[0];
                }
                if (base.Padding.Left > 0)
                {
                    num3 += ConvertToTwips((float) base.Padding.Left, base.Padding.Dpi);
                }
                this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.LeftIndent, num3));
            }
            int num = base.UsefulPageWidth - base.colRights[base.colRights.Count - 1];
            if (base.Padding.Right > 0)
            {
                num += ConvertToTwips((float) base.Padding.Right, base.Padding.Dpi);
            }
            this.InsertDataInDefinition(base.currentRowIndex, base.currentColIndex, "", string.Format(RtfTags.RightIndent, num));
        }

        protected override void SetRightBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.RightBorder, columnIndex, rowIndex);
        }

        protected override void SetTopBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.TopBorder, columnIndex, rowIndex);
        }

        public override void SetVAlign()
        {
        }

        protected override void WriteHeaderRowDefinition(StringBuilder sb, int index)
        {
            sb.Append(RtfTags.ParagraphDefault + RtfTags.PlainText);
            if (base.PageBreakInfo != null)
            {
                if (!base.PageBreakInfo.InsideMultiColumn)
                {
                    sb.Append(RtfTags.PageBreakBefore);
                }
                else
                {
                    for (int i = 0; i < base.PageBreakInfo.Count; i++)
                    {
                        sb.Append(RtfTags.ColumnBreakParagraph);
                    }
                }
                base.PageBreakInfo = null;
            }
        }

        protected override string BackColorTag =>
            RtfTags.BackgroundPatternColor;
    }
}

