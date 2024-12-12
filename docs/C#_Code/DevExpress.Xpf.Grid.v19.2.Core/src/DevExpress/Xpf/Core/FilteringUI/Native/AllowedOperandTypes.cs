namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;

    [Flags]
    public enum AllowedOperandTypes
    {
        Value = 1,
        Property = 2,
        Parameter = 4,
        LocalDateTimeFunction = 8,
        All = 15
    }
}

