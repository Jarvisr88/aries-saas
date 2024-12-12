namespace DevExpress.Data.Mask
{
    using System;

    [Flags]
    public enum DateTimePart
    {
        public const DateTimePart None = DateTimePart.None;,
        public const DateTimePart Date = DateTimePart.Date;,
        public const DateTimePart Time = DateTimePart.Time;,
        public const DateTimePart Both = DateTimePart.Both;
    }
}

