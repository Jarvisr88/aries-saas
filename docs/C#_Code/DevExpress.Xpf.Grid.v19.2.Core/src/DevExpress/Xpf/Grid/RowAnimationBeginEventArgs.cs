namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;

    public class RowAnimationBeginEventArgs : RoutedEventArgs
    {
        public RowAnimationBeginEventArgs(DataViewBase source)
        {
            this.Source = source;
        }

        public bool IsGroupRow { get; internal set; }

        public Timeline AnimationTimeline { get; set; }

        public System.Windows.PropertyPath PropertyPath { get; set; }

        public Effect AddedEffect { get; set; }

        public DataViewBase Source { get; private set; }
    }
}

