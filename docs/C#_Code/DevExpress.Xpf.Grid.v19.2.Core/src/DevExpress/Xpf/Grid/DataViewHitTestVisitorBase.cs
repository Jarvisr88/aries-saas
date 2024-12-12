namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;

    public abstract class DataViewHitTestVisitorBase : HitTestVisitorBase
    {
        internal readonly IDataViewHitInfo hitInfo;

        internal DataViewHitTestVisitorBase(IDataViewHitInfo hitInfo)
        {
            this.hitInfo = hitInfo;
        }

        internal virtual void StopHitTestingCore()
        {
        }

        public virtual void VisitBandHeader(BandBase band)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.BandHeader);
            this.hitInfo.SetBand(band);
        }

        public virtual void VisitBandHeaderPanel()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.BandHeaderPanel);
        }

        public virtual void VisitColumnHeader(ColumnBase column)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.ColumnHeader);
            this.hitInfo.SetColumn(column);
        }

        public virtual void VisitColumnHeaderFilterButton(ColumnBase column)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.ColumnHeaderFilterButton);
        }

        public virtual void VisitColumnHeaderPanel()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.ColumnHeaderPanel);
        }

        public virtual void VisitDataArea()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.DataArea);
        }

        public virtual void VisitFilterPanel()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FilterPanel);
        }

        public virtual void VisitFilterPanelActiveButton()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FilterPanelActiveButton);
        }

        public virtual void VisitFilterPanelCloseButton()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FilterPanelCloseButton);
        }

        public virtual void VisitFilterPanelCustomizeButton()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FilterPanelCustomizeButton);
        }

        public virtual void VisitFilterPanelText()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FilterPanelText);
        }

        public virtual void VisitFixedTotalSummary(string summaryText)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.FixedTotalSummary);
        }

        public virtual void VisitHorizontalScrollBar()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.HorizontalScrollBar);
        }

        public virtual void VisitMRUFilterListComboBox()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.MRUFilterListComboBox);
        }

        public virtual void VisitRow(int rowHandle)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.Row);
            this.hitInfo.SetRowHandle(rowHandle);
            this.StopHitTestingCore();
        }

        public virtual void VisitRowCell(int rowHandle, ColumnBase column)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.RowCell);
            this.hitInfo.SetRowHandle(rowHandle);
            this.hitInfo.SetColumn(column);
            this.StopHitTestingCore();
        }

        public virtual void VisitSearchPanel()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.SearchPanel);
        }

        public virtual void VisitSearchPanelShowButton()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.SearchPanelShowButton);
        }

        public virtual void VisitTotalSummary(ColumnBase column)
        {
            this.hitInfo.SetHitTest(TableViewHitTest.TotalSummary);
            this.hitInfo.SetColumn(column);
        }

        public virtual void VisitTotalSummaryPanel()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.TotalSummaryPanel);
        }

        public virtual void VisitVerticalScrollBar()
        {
            this.hitInfo.SetHitTest(TableViewHitTest.VerticalScrollBar);
        }
    }
}

