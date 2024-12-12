namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabControlContentPresenter : ContentHostContentPresenter
    {
        public static readonly DependencyProperty ActualHorizontalAlignmentProperty;
        public static readonly DependencyProperty ActualVerticalAlignmentProperty;

        static TabControlContentPresenter();
        private bool IsForegroundSet(DependencyObject obj);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        private void UpdateAlignment();

        public HorizontalAlignment ActualHorizontalAlignment { get; set; }

        public VerticalAlignment ActualVerticalAlignment { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlContentPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__9_1(DependencyObject d, object e);
            internal void <.cctor>b__9_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <.cctor>b__9_3(DependencyObject d, object e);
            internal void <.cctor>b__9_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__9_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

