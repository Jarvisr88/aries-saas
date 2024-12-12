namespace DevExpress.Xpf.Grid
{
    using System;

    public class ExcelColumnFilterServiceValueAddFilter : ExcelColumnFilterServiceValueBase
    {
        public ExcelColumnFilterServiceValueAddFilter(string displayValue) : base(displayValue)
        {
            base.SetIsVisible(false);
        }
    }
}

