namespace DevExpress.Xpf.Core
{
    using System;

    [Flags]
    public enum Side
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        LeftRight = 2,
        TopBottom = 5,
        All = 7
    }
}

