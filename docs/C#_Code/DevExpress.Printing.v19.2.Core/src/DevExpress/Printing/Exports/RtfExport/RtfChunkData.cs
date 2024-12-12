namespace DevExpress.Printing.Exports.RtfExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export.Rtf;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;

    public abstract class RtfChunkData
    {
        protected List<StringCollection> rowContents = new List<StringCollection>();
        protected List<StringCollection> rowDefinitions = new List<StringCollection>();
        protected List<int> colRights;
        protected List<int> rowHeights;
        private RtfMultiColumn mc;
        private RtfPageBreakInfo pageBreakInfo;
        private bool rtlLayout;
        private bool isHeaderFooterContent;
        private List<SpecifyRowHeightType> specifyRowHeightTypes = new List<SpecifyRowHeightType>();
        private int columnCount;
        private int usefulPageWidth;
        protected int currentColIndex;
        protected int currentRowIndex;
        protected int currentColSpan;
        protected int currentRowSpan;
        protected int backColorIndex;
        protected int borderColorIndex;
        protected int foreColorIndex;
        protected BrickStyle style;

        public RtfChunkData(List<int> colWidths, List<int> rowHeights)
        {
            this.rowHeights = rowHeights;
            this.InitColRights(colWidths);
            this.FillDefaultRowAdjustmentValues();
        }

        protected void AddRowContent(StringCollection rowContent)
        {
            this.rowContents.Add(rowContent);
        }

        protected void AddRowDefinition(StringCollection rowDefinition)
        {
            this.rowDefinitions.Add(rowDefinition);
        }

        protected abstract void AppendRowContent(StringBuilder sb, StringCollection rowDefinition, StringCollection rowContent);
        private void AppendZeroRowContent(StringBuilder sb)
        {
            StringCollection rowDefinition = new StringCollection {
                RtfTags.ParagraphDefault + RtfTags.DefaultRow + RtfTags.NewLine
            };
            StringCollection rowContent = new StringCollection {
                RtfTags.EmptyCellContent
            };
            this.AppendRowContent(sb, rowDefinition, rowContent);
        }

        protected static int ConvertDIPToTwips(float value) => 
            (int) GraphicsUnitConverter.Convert(value, (float) 96f, (float) 1440f);

        protected static int ConvertToTwips(float value, float dpi) => 
            (int) GraphicsUnitConverter.Convert(value, dpi, (float) 1440f);

        private void FillContent(StringBuilder sb)
        {
            if (this.rowDefinitions.Count <= 0)
            {
                this.AppendZeroRowContent(sb);
            }
            else
            {
                for (int i = 0; i < this.rowDefinitions.Count; i++)
                {
                    this.WriteHeaderRowDefinition(sb, i);
                    this.AppendRowContent(sb, this.rowDefinitions[i], this.rowContents[i]);
                }
            }
        }

        private void FillDefaultRowAdjustmentValues()
        {
            for (int i = 0; i < this.rowHeights.Count; i++)
            {
                this.specifyRowHeightTypes.Add(SpecifyRowHeightType.Exact);
            }
        }

        public abstract void FillTemplate();
        public StringBuilder GetResultContent(int headerY, int footerY)
        {
            StringBuilder sb = new StringBuilder();
            if ((this.mc & RtfMultiColumn.End) > RtfMultiColumn.None)
            {
                bool flag = false;
                if ((this.PageBreakInfo != null) && (!this.PageBreakInfo.InsideMultiColumn || ((this.mc & (RtfMultiColumn.None | RtfMultiColumn.Start)) > RtfMultiColumn.None)))
                {
                    flag = true;
                    this.PageBreakInfo = null;
                }
                string str = flag ? string.Empty : RtfTags.SectionNoBreak;
                string[] textArray1 = new string[] { "}", RtfTags.SectionStart, RtfTags.SectionDefault, str, string.Format(RtfTags.HeaderY, headerY), string.Format(RtfTags.FooterY, footerY), string.Format(RtfTags.ColumnCount, 1), "{" };
                sb.Append(string.Concat(textArray1));
            }
            if ((this.mc & (RtfMultiColumn.None | RtfMultiColumn.Start)) <= RtfMultiColumn.None)
            {
                this.FillContent(sb);
            }
            else
            {
                int index = ((this.mc & RtfMultiColumn.End) > RtfMultiColumn.None) ? sb.Length : 0;
                bool flag2 = false;
                if (this.PageBreakInfo != null)
                {
                    flag2 = true;
                    this.PageBreakInfo = null;
                }
                string str2 = flag2 ? string.Empty : RtfTags.SectionNoBreak;
                string[] textArray2 = new string[] { RtfTags.SectionStart, RtfTags.SectionDefault, str2, string.Format(RtfTags.HeaderY, headerY), string.Format(RtfTags.FooterY, footerY), string.Format(RtfTags.ColumnCount, this.columnCount), RtfTags.SpaceBetweenColumns };
                string str3 = string.Concat(textArray2);
                if (this.RtlLayout)
                {
                    str3 = str3 + RtfTags.RightToLeftSection;
                }
                this.FillContent(sb);
                sb.Insert(index, "}" + str3 + "{");
            }
            return sb;
        }

        protected string GetSpecifyRowHeightContent(int index)
        {
            string str = (this.isHeaderFooterContent || (((SpecifyRowHeightType) this.specifyRowHeightTypes[index]) == SpecifyRowHeightType.Exact)) ? string.Format(RtfTags.ExactlyRowHeight, ConvertDIPToTwips((float) this.rowHeights[index])) : ((((SpecifyRowHeightType) this.specifyRowHeightTypes[index]) != SpecifyRowHeightType.AtLeast) ? "" : string.Format(RtfTags.AtLeastRowHeight, ConvertDIPToTwips((float) this.rowHeights[index])));
            return (str + RtfTags.RowAutofit);
        }

        private void InitColRights(List<int> colWidths)
        {
            int num = 0;
            this.colRights = new List<int>();
            for (int i = 0; i < colWidths.Count; i++)
            {
                num += colWidths[i];
                this.colRights.Add(ConvertDIPToTwips((float) num));
            }
        }

        protected abstract void InsertDataInContent(int index, int offset, string tag, string data);
        protected abstract void InsertDataInDefinition(int index, int offset, string tag, string data);
        private void MarkCellHMerged(int columnIndex, int rowIndex)
        {
            this.SetBorders(columnIndex, rowIndex);
            this.SetCellTag(columnIndex, rowIndex, RtfTags.MergedCell);
        }

        private void MarkCellVMerged(int rowIndex)
        {
            this.SetBorders(this.currentColIndex, rowIndex);
            this.SetCellTag(this.currentColIndex, rowIndex, RtfTags.VerticalMergedCell);
        }

        private void MarkFirstCellHMerged(int rowIndex, int colIndex, int colSpan)
        {
            string str = this.rowDefinitions[rowIndex][colIndex];
            this.rowDefinitions[rowIndex][colIndex] = str.Substring(0, str.IndexOf(RtfTags.CellRight) + RtfTags.CellRight.Length) + this.colRights[(colIndex + colSpan) - 1].ToString();
            this.SetCellTag(colIndex, rowIndex, RtfTags.FirstMergedCell);
        }

        private void MarkFirstCellVMerged()
        {
            this.SetCellTag(this.currentColIndex, this.currentRowIndex, RtfTags.FirstVerticalMergedCell);
        }

        public abstract void SetAngle(float angle);
        public void SetBackColor()
        {
            this.SetCellTag(this.currentColIndex, this.currentRowIndex, string.Format(this.BackColorTag, this.backColorIndex));
        }

        protected void SetBorder(string border, int columnIndex, int rowIndex)
        {
            BorderDashStyle borderDashStyle = this.style.BorderDashStyle;
            this.SetCellTag(columnIndex, rowIndex, RtfTags.NewLine + border);
            this.SetCellTag(columnIndex, rowIndex, RtfExportBorderHelper.GetFullBorderStyle(borderDashStyle, ConvertDIPToTwips(this.style.BorderWidth), this.borderColorIndex));
        }

        public void SetBorders(int columnIndex, int rowIndex)
        {
            if ((this.style != null) && ((this.style.Sides != BorderSide.None) && (this.style.BorderWidth != 0f)))
            {
                BorderSide sides = this.style.Sides;
                if ((sides & BorderSide.Top) > BorderSide.None)
                {
                    this.SetTopBorder(columnIndex, rowIndex);
                }
                if ((sides & BorderSide.Bottom) > BorderSide.None)
                {
                    this.SetBottomBorder(columnIndex, rowIndex);
                }
                if ((sides & BorderSide.Left) > BorderSide.None)
                {
                    this.SetLeftBorder(columnIndex, rowIndex);
                }
                if ((sides & BorderSide.Right) > BorderSide.None)
                {
                    this.SetRightBorder(columnIndex, rowIndex);
                }
            }
        }

        protected abstract void SetBottomBorder(int columnIndex, int rowIndex);
        protected void SetCellTag(int columnIndex, int rowIndex, string tag)
        {
            this.InsertDataInDefinition(rowIndex, columnIndex, RtfTags.CellRight, tag);
        }

        public void SetCellUnion()
        {
            if (this.currentColSpan > 1)
            {
                int columnIndex = this.currentColIndex + 1;
                while (true)
                {
                    if (columnIndex >= (this.currentColIndex + this.currentColSpan))
                    {
                        for (int j = this.currentRowIndex; j < (this.currentRowIndex + this.currentRowSpan); j++)
                        {
                            this.MarkFirstCellHMerged(j, this.currentColIndex, this.currentColSpan);
                        }
                        break;
                    }
                    int currentRowIndex = this.currentRowIndex;
                    while (true)
                    {
                        if (currentRowIndex >= (this.currentRowIndex + this.currentRowSpan))
                        {
                            columnIndex++;
                            break;
                        }
                        this.MarkCellHMerged(columnIndex, currentRowIndex);
                        currentRowIndex++;
                    }
                }
            }
            if (this.currentRowSpan > 1)
            {
                this.MarkFirstCellVMerged();
            }
            for (int i = this.currentRowIndex + 1; i < (this.currentRowIndex + this.currentRowSpan); i++)
            {
                this.MarkCellVMerged(i);
            }
        }

        public abstract void SetContent(string content);
        public abstract void SetDirection();
        public abstract void SetFontString(string fontString);
        public void SetForeColor()
        {
            this.SetContent(string.Format(RtfTags.Color, this.foreColorIndex));
        }

        public abstract void SetHAlign();
        public abstract void SetIndent();
        protected abstract void SetLeftBorder(int columnIndex, int rowIndex);
        public abstract void SetPadding();
        protected abstract void SetRightBorder(int columnIndex, int rowIndex);
        public void SetSpecifyRowHeightType(SpecifyRowHeightType type, int startRow, int rowCount)
        {
            if (type == SpecifyRowHeightType.None)
            {
                for (int i = (startRow + rowCount) - 1; i >= startRow; i--)
                {
                    this.specifyRowHeightTypes[i] = SpecifyRowHeightType.None;
                }
            }
            else if (type == SpecifyRowHeightType.AtLeast)
            {
                int num4 = 7;
                for (int i = (startRow + rowCount) - 1; i >= startRow; i--)
                {
                    if (this.rowHeights[i] > num4)
                    {
                        this.specifyRowHeightTypes[i] = SpecifyRowHeightType.AtLeast;
                        return;
                    }
                }
            }
        }

        protected abstract void SetTopBorder(int columnIndex, int rowIndex);
        public abstract void SetVAlign();
        public void UpdateColors(int backColorIndex, int borderColorIndex, int foreColorIndex)
        {
            this.backColorIndex = backColorIndex;
            this.borderColorIndex = borderColorIndex;
            this.foreColorIndex = foreColorIndex;
        }

        public void UpdateDataIndexes(int colIndex, int rowIndex, int colSpan, int rowSpan)
        {
            this.currentColIndex = colIndex;
            this.currentRowIndex = rowIndex;
            this.currentColSpan = colSpan;
            this.currentRowSpan = rowSpan;
        }

        public void UpdateStyle(BrickStyle style)
        {
            this.style = style;
        }

        protected abstract void WriteHeaderRowDefinition(StringBuilder sb, int index);

        public RtfMultiColumn MultiColumn
        {
            get => 
                this.mc;
            set => 
                this.mc = value;
        }

        public RtfPageBreakInfo PageBreakInfo
        {
            get => 
                this.pageBreakInfo;
            set => 
                this.pageBreakInfo = value;
        }

        public bool IsHeaderFooterContent
        {
            get => 
                this.isHeaderFooterContent;
            set => 
                this.isHeaderFooterContent = value;
        }

        public bool RtlLayout
        {
            get => 
                this.rtlLayout;
            set => 
                this.rtlLayout = value;
        }

        protected bool Rtl =>
            this.style.StringFormat.RightToLeft;

        protected PaddingInfo Padding =>
            this.style.Padding;

        public int UsefulPageWidth
        {
            get => 
                this.usefulPageWidth;
            set => 
                this.usefulPageWidth = value;
        }

        public int ColumnCount
        {
            get => 
                this.columnCount;
            set => 
                this.columnCount = value;
        }

        protected abstract string BackColorTag { get; }
    }
}

