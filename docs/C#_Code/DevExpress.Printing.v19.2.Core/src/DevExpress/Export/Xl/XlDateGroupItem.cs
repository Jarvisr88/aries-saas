namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlDateGroupItem
    {
        public XlDateGroupItem(DateTime value, XlDateTimeGroupingType groupingType)
        {
            this.Value = value;
            this.GroupingType = groupingType;
        }

        internal bool MeetCriteria(DateTime date) => 
            (date.Year == this.Value.Year) ? (((this.GroupingType < XlDateTimeGroupingType.Month) || (date.Month == this.Value.Month)) ? (((this.GroupingType < XlDateTimeGroupingType.Day) || (date.Day == this.Value.Day)) ? (((this.GroupingType < XlDateTimeGroupingType.Hour) || (date.Hour == this.Value.Hour)) ? (((this.GroupingType < XlDateTimeGroupingType.Minute) || (date.Minute == this.Value.Minute)) ? ((this.GroupingType != XlDateTimeGroupingType.Second) || (date.Second == this.Value.Second)) : false) : false) : false) : false) : false;

        public DateTime Value { get; private set; }

        public XlDateTimeGroupingType GroupingType { get; private set; }
    }
}

