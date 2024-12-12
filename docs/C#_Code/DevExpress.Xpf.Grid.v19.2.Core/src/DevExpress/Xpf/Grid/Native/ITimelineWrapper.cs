namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Animation;

    internal interface ITimelineWrapper
    {
        event EventHandler Deactivated;

        System.Windows.Media.Animation.Timeline Timeline { get; }
    }
}

