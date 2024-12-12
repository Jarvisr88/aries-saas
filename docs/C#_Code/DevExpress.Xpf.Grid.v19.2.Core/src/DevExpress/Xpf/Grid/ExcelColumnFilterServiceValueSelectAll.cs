namespace DevExpress.Xpf.Grid
{
    using System;

    public class ExcelColumnFilterServiceValueSelectAll : ExcelColumnFilterServiceValueBase
    {
        public ExcelColumnFilterServiceValueSelectAll(string displayValue, bool defaultIsChecked) : base(displayValue)
        {
            base.IsChecked = new bool?(defaultIsChecked);
        }
    }
}

