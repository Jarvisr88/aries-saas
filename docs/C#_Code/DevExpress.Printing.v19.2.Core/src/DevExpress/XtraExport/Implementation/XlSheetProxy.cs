namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlSheetProxy : IXlSheet, IDisposable, IXlShapeContainer
    {
        private IXlExport exporter;
        private readonly IXlSheet subject;

        public XlSheetProxy(IXlExport exporter, IXlSheet subject)
        {
            this.exporter = exporter;
            this.subject = subject;
        }

        public void BeginFiltering(XlCellRange autoFilterRange)
        {
            this.subject.BeginFiltering(autoFilterRange);
        }

        public int BeginGroup(bool collapsed) => 
            this.subject.BeginGroup(collapsed);

        public int BeginGroup(int outlineLevel, bool collapsed) => 
            this.subject.BeginGroup(outlineLevel, collapsed);

        public IXlColumn CreateColumn() => 
            this.subject.CreateColumn();

        public IXlColumn CreateColumn(int columnIndex) => 
            this.subject.CreateColumn(columnIndex);

        public IXlPicture CreatePicture() => 
            this.subject.CreatePicture();

        public IXlRow CreateRow() => 
            this.subject.CreateRow();

        public IXlRow CreateRow(int rowIndex) => 
            this.subject.CreateRow(rowIndex);

        void IXlShapeContainer.AddShape(XlShape shape)
        {
            IXlShapeContainer subject = this.subject as IXlShapeContainer;
            if (subject != null)
            {
                subject.AddShape(shape);
            }
        }

        public void Dispose()
        {
            if (this.exporter != null)
            {
                this.exporter.EndSheet();
                this.exporter = null;
            }
        }

        public void EndFiltering()
        {
            this.subject.EndFiltering();
        }

        public void EndGroup()
        {
            this.subject.EndGroup();
        }

        public void SkipColumns(int count)
        {
            this.subject.SkipColumns(count);
        }

        public void SkipRows(int count)
        {
            this.subject.SkipRows(count);
        }

        public string Name
        {
            get => 
                this.subject.Name;
            set => 
                this.subject.Name = value;
        }

        public IXlMergedCells MergedCells =>
            this.subject.MergedCells;

        public XlCellPosition SplitPosition
        {
            get => 
                this.subject.SplitPosition;
            set => 
                this.subject.SplitPosition = value;
        }

        public XlCellRange AutoFilterRange
        {
            get => 
                this.subject.AutoFilterRange;
            set => 
                this.subject.AutoFilterRange = value;
        }

        public IXlFilterColumns AutoFilterColumns =>
            this.subject.AutoFilterColumns;

        public IList<XlConditionalFormatting> ConditionalFormattings =>
            this.subject.ConditionalFormattings;

        public IList<XlDataValidation> DataValidations =>
            this.subject.DataValidations;

        public XlSheetVisibleState VisibleState
        {
            get => 
                this.subject.VisibleState;
            set => 
                this.subject.VisibleState = value;
        }

        public XlPageMargins PageMargins
        {
            get => 
                this.subject.PageMargins;
            set => 
                this.subject.PageMargins = value;
        }

        public XlPageSetup PageSetup
        {
            get => 
                this.subject.PageSetup;
            set => 
                this.subject.PageSetup = value;
        }

        public XlHeaderFooter HeaderFooter =>
            this.subject.HeaderFooter;

        public XlPrintTitles PrintTitles =>
            this.subject.PrintTitles;

        public XlCellRange PrintArea
        {
            get => 
                this.subject.PrintArea;
            set => 
                this.subject.PrintArea = value;
        }

        public XlPrintOptions PrintOptions
        {
            get => 
                this.subject.PrintOptions;
            set => 
                this.subject.PrintOptions = value;
        }

        public IXlPageBreaks ColumnPageBreaks =>
            this.subject.ColumnPageBreaks;

        public IXlPageBreaks RowPageBreaks =>
            this.subject.RowPageBreaks;

        public IList<XlHyperlink> Hyperlinks =>
            this.subject.Hyperlinks;

        public XlCellRange DataRange =>
            this.subject.DataRange;

        public XlCellRange ColumnRange =>
            this.subject.ColumnRange;

        public XlIgnoreErrors IgnoreErrors
        {
            get => 
                this.subject.IgnoreErrors;
            set => 
                this.subject.IgnoreErrors = value;
        }

        public IXlOutlineProperties OutlineProperties =>
            this.subject.OutlineProperties;

        public IXlSheetViewOptions ViewOptions =>
            this.subject.ViewOptions;

        public IList<XlSparklineGroup> SparklineGroups =>
            this.subject.SparklineGroups;

        public IXlTableCollection Tables =>
            this.subject.Tables;

        public IXlSheetSelection Selection =>
            this.subject.Selection;

        public int CurrentOutlineLevel =>
            this.subject.CurrentOutlineLevel;

        public int CurrentRowIndex =>
            this.subject.CurrentRowIndex;

        public int CurrentColumnIndex =>
            this.subject.CurrentColumnIndex;
    }
}

