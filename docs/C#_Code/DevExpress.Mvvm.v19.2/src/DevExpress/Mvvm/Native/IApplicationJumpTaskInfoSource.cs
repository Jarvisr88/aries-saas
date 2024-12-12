namespace DevExpress.Mvvm.Native
{
    using System;

    public interface IApplicationJumpTaskInfoSource : IApplicationJumpItemInfoSource
    {
        System.Action Action { get; }
    }
}

