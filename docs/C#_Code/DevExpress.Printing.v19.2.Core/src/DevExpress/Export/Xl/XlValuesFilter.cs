namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlValuesFilter : IXlFilter, IXlFilterCriteria
    {
        private readonly List<string> values = new List<string>();
        private readonly List<XlDateGroupItem> dateGroups = new List<XlDateGroupItem>();

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            bool flag;
            if ((cell == null) || cell.Value.IsEmpty)
            {
                return this.FilterByBlank;
            }
            if (cell.Value.IsNumeric && ((cell.Formatting != null) && cell.Formatting.IsDateTimeNumberFormat()))
            {
                DateTime dateTimeValue = cell.Value.DateTimeValue;
                using (List<XlDateGroupItem>.Enumerator enumerator = this.dateGroups.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        XlDateGroupItem current = enumerator.Current;
                        if (current.MeetCriteria(dateTimeValue))
                        {
                            return true;
                        }
                    }
                }
            }
            string str = (cellFormatter != null) ? cellFormatter.GetFormattedValue(cell) : cell.Value.ToText().TextValue;
            if (string.IsNullOrEmpty(str) && this.FilterByBlank)
            {
                return true;
            }
            using (List<string>.Enumerator enumerator2 = this.values.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        string current = enumerator2.Current;
                        if (StringExtensions.CompareInvariantCultureIgnoreCase(current, str) != 0)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public IList<string> Values =>
            this.values;

        public IList<XlDateGroupItem> DateGroups =>
            this.dateGroups;

        public bool FilterByBlank { get; set; }

        public XlCalendarType CalendarType { get; set; }

        public XlFilterType FilterType =>
            XlFilterType.Values;
    }
}

