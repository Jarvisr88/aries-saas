namespace DevExpress.Printing.Exports.RtfExport
{
    using DevExpress.XtraPrinting.Export.Rtf;
    using DevExpress.XtraPrinting.Native.TextRotation;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Text;

    public class TableRtfChunkData : RtfChunkData
    {
        public TableRtfChunkData(List<int> colWidths, List<int> rowHeights) : base(colWidths, rowHeights)
        {
        }

        protected override void AppendRowContent(StringBuilder sb, StringCollection rowDefinition, StringCollection rowContent)
        {
            if (rowDefinition.Count > 0)
            {
                foreach (string str in rowDefinition)
                {
                    sb.Append(str);
                }
                if (rowContent.Count > 0)
                {
                    sb.Append(RtfTags.NewLine);
                    sb.Append(base.RtlLayout ? RtfTags.RightToLeftRow : RtfTags.LeftToRightRow);
                    sb.Append(RtfTags.NewLine + RtfTags.SuggestToTable + RtfTags.NewLine);
                    foreach (string str2 in rowContent)
                    {
                        string str3 = str2;
                        if (str3 == RtfTags.EmptyCellContent)
                        {
                            str3 = str3.Insert(1, string.Format(RtfTags.FontSize, 1));
                        }
                        sb.Append(str3);
                    }
                }
                sb.Append(RtfTags.EndOfRow + RtfTags.NewLine + RtfTags.NewLine);
            }
        }

        private StringCollection CreateTableRowContent()
        {
            StringCollection strings = new StringCollection();
            for (int i = 0; i < base.colRights.Count; i++)
            {
                strings.Add(RtfTags.EmptyCellContent);
            }
            return strings;
        }

        private StringCollection CreateTableRowDefinition()
        {
            StringCollection strings = new StringCollection();
            for (int i = 0; i < base.colRights.Count; i++)
            {
                strings.Add(RtfTags.CellRight + base.colRights[i].ToString());
            }
            return strings;
        }

        public override void FillTemplate()
        {
            for (int i = 0; i < base.rowHeights.Count; i++)
            {
                int num2 = base.rowHeights[i];
                if (num2 == 0)
                {
                    base.AddRowDefinition(new StringCollection());
                    base.AddRowContent(new StringCollection());
                }
                else
                {
                    StringCollection rowDefinition = this.CreateTableRowDefinition();
                    StringCollection rowContent = this.CreateTableRowContent();
                    base.AddRowDefinition(rowDefinition);
                    base.AddRowContent(rowContent);
                }
            }
        }

        protected override void InsertDataInContent(int index, int offset, string tag, string data)
        {
            string str = base.rowContents[index][offset];
            base.rowContents[index][offset] = str.Insert(str.IndexOf(tag), data);
        }

        protected override void InsertDataInDefinition(int index, int offset, string tag, string data)
        {
            string str = base.rowDefinitions[index][offset];
            base.rowDefinitions[index][offset] = str.Insert(str.IndexOf(tag), data);
        }

        public override void SetAngle(float angle)
        {
            short num = Convert.ToInt16(angle);
            if ((num == 90) || (num == 270))
            {
                string tag = (angle == 270f) ? RtfTags.CellTextFlowTBRL : RtfTags.CellTextFlowBTLR;
                base.SetCellTag(base.currentColIndex, base.currentRowIndex, tag);
                StringAlignment rotatedAlignment = RotatedTextHelper.GetRotatedAlignment(base.style.TextAlignment, angle);
                this.SetContent(RtfAlignmentConverter.ToHorzRtfAlignment(rotatedAlignment));
            }
        }

        protected override void SetBottomBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.BottomCellBorder, columnIndex, rowIndex);
        }

        public override void SetContent(string content)
        {
            this.InsertDataInContent(base.currentRowIndex, base.currentColIndex, RtfTags.EndOfCell, content);
        }

        public override void SetDirection()
        {
            if (base.Rtl)
            {
                this.SetContent(RtfTags.RightToLeftParagraph);
            }
            this.SetContent(" ");
        }

        public override void SetFontString(string fontString)
        {
            this.SetContent(fontString);
        }

        public override void SetHAlign()
        {
            this.SetContent(RtfAlignmentConverter.ToHorzRtfAlignment(base.style.TextAlignment));
        }

        public override void SetIndent()
        {
        }

        protected override void SetLeftBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.LeftCellBorder, columnIndex, rowIndex);
        }

        public override void SetPadding()
        {
            if (base.Padding.Top > 0)
            {
                this.SetContent(string.Format(RtfTags.SpaceBefore, ConvertToTwips((float) base.Padding.Top, base.Padding.Dpi)));
            }
            if (base.Padding.Bottom > 0)
            {
                this.SetContent(string.Format(RtfTags.SpaceAfter, ConvertToTwips((float) base.Padding.Bottom, base.Padding.Dpi)));
            }
            if (base.Padding.Left > 0)
            {
                this.SetContent(string.Format(RtfTags.LeftIndent, ConvertToTwips((float) base.Padding.Left, base.Padding.Dpi)));
            }
            if (base.Padding.Right > 0)
            {
                this.SetContent(string.Format(RtfTags.RightIndent, ConvertToTwips((float) base.Padding.Right, base.Padding.Dpi)));
            }
        }

        protected override void SetRightBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.RightCellBorder, columnIndex, rowIndex);
        }

        protected override void SetTopBorder(int columnIndex, int rowIndex)
        {
            base.SetBorder(RtfTags.TopCellBorder, columnIndex, rowIndex);
        }

        public override void SetVAlign()
        {
            base.SetCellTag(base.currentColIndex, base.currentRowIndex, RtfAlignmentConverter.ToVertRtfAlignment(base.style.TextAlignment));
        }

        protected override void WriteHeaderRowDefinition(StringBuilder sb, int index)
        {
            if (base.rowHeights[index] > 0)
            {
                sb.Append(string.Format(RtfTags.ParagraphDefault + RtfTags.DefaultRow + base.GetSpecifyRowHeightContent(index) + RtfTags.NewLine, new object[0]));
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
        }

        protected override string BackColorTag =>
            RtfTags.BackgroundPatternBackgroundColor;
    }
}

