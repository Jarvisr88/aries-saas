namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;

    public class GridTableViewLayoutCalculatorFactory
    {
        public virtual ColumnsLayoutCalculator CreateCalculator(GridViewInfo viewInfo, bool autoWidth) => 
            (viewInfo.GridView.DataControl.BandsLayoutCore == null) ? (autoWidth ? ((ColumnsLayoutCalculator) new AutoWidthColumnsLayoutCalculator(viewInfo)) : new ColumnsLayoutCalculator(viewInfo)) : (!autoWidth ? ((ColumnsLayoutCalculator) new BandedViewColumnsLayoutCalculator(viewInfo)) : ((ColumnsLayoutCalculator) new BandedViewAutoWidthColumnsLayoutCalculator(viewInfo)));
    }
}

