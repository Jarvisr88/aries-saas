namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelDateDayColumnFilterValue : ExcelColumnFilterValue
    {
        public ExcelDateDayColumnFilterValue(object editValue, DateTime date) : base(editValue)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
    }
}

