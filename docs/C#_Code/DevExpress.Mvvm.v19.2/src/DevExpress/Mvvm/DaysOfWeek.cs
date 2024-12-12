namespace DevExpress.Mvvm
{
    using System;

    [Flags]
    public enum DaysOfWeek
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 0x10,
        Friday = 0x20,
        Saturday = 0x40,
        Weekends = 0x41
    }
}

