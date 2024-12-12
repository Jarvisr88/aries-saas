namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlCustomFilters : IXlFilter, IXlFilterCriteria
    {
        private XlCustomFilterCriteria first;

        public XlCustomFilters(XlCustomFilterCriteria first) : this(first, null, false)
        {
        }

        public XlCustomFilters(XlCustomFilterCriteria first, XlCustomFilterCriteria second) : this(first, second, false)
        {
        }

        public XlCustomFilters(XlCustomFilterCriteria first, XlCustomFilterCriteria second, bool and)
        {
            Guard.ArgumentNotNull(first, "fisrt");
            this.first = first;
            this.Second = second;
            this.And = and;
        }

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            bool flag = this.first.MeetCriteria(cell, cellFormatter);
            if (this.Second != null)
            {
                flag = !this.And ? (flag | this.Second.MeetCriteria(cell, cellFormatter)) : (flag & this.Second.MeetCriteria(cell, cellFormatter));
            }
            return flag;
        }

        public XlCustomFilterCriteria First
        {
            get => 
                this.first;
            set
            {
                Guard.ArgumentNotNull(value, "value");
                this.first = value;
            }
        }

        public XlCustomFilterCriteria Second { get; set; }

        public bool And { get; set; }

        public XlFilterType FilterType =>
            XlFilterType.Custom;
    }
}

