namespace DevExpress.Xpf.Core.HitTest
{
    using System;

    public enum HitTestFilterBehavior
    {
        ContinueSkipSelfAndChildren = 0,
        ContinueSkipChildren = 2,
        ContinueSkipSelf = 4,
        Continue = 6,
        Stop = 8
    }
}

