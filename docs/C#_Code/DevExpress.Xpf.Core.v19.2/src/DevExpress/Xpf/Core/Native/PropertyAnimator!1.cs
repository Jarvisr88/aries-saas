namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public abstract class PropertyAnimator<T> : DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ProgressProperty;
        private Storyboard _storyboard;
        private bool _silentProgressUpdate;

        static PropertyAnimator();
        public PropertyAnimator(int duration);
        private void BeginStoryboard(System.Windows.Duration duration);
        protected abstract T GetCurrentValue();
        protected abstract void OnProgress(bool completed);
        private static void OnProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private void OnStoryboardCompleted(object sender, EventArgs e);
        public void Set(T value, bool enableAnimations, int duration = -1);
        private void SetProgressSilently(double value);
        private void StopStoryboard();

        public double Progress { get; protected set; }

        public T From { get; protected set; }

        public T To { get; protected set; }

        public System.Windows.Duration Duration { get; protected set; }
    }
}

