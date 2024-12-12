namespace ActiproSoftware.WinUICore
{
    using System;

    [Flags]
    public enum InvalidationTypes
    {
        Arrange = 1,
        Measure = 3,
        Layout = 4,
        Paint = 8,
        All = 15
    }
}

