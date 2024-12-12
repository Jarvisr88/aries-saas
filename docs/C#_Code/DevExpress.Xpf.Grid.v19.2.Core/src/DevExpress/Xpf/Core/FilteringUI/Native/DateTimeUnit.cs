namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DateTimeUnit
    {
        public DateTimeUnit(DevExpress.Xpf.Core.FilteringUI.Native.DateTimeUnitType groupType, DateTime date)
        {
            this.<DateTimeUnitType>k__BackingField = groupType;
            this.<Date>k__BackingField = date;
        }

        public DevExpress.Xpf.Core.FilteringUI.Native.DateTimeUnitType DateTimeUnitType { get; }

        public DateTime Date { get; }
    }
}

