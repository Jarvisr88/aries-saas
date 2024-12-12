namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class CustomScrollAnimationEventArgs : RoutedEventArgs
    {
        public CustomScrollAnimationEventArgs(double oldOffset, double newOffset)
        {
            this.OldOffset = oldOffset;
            this.NewOffset = newOffset;
        }

        public System.Windows.Media.Animation.Storyboard Storyboard { get; set; }

        public double OldOffset { get; private set; }

        public double NewOffset { get; private set; }
    }
}

