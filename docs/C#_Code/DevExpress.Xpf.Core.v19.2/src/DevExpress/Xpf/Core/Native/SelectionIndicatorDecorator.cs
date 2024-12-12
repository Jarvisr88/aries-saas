namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class SelectionIndicatorDecorator : Decorator
    {
        public static readonly DependencyProperty SelectionIndicatorStyleProperty;
        public static readonly DependencyProperty InstanceProperty;
        public static readonly DependencyProperty EnableAnimationsProperty;
        public static readonly DependencyProperty DurationProperty;
        public static readonly DependencyProperty EasingFunctionProperty;
        private DevExpress.Xpf.Core.Native.FloatingIndicator floatingIndicator;
        private List<SelectionIndicator> indicators;

        static SelectionIndicatorDecorator();
        public SelectionIndicatorDecorator();
        protected internal virtual void AddIndicator(SelectionIndicator value);
        protected override Size ArrangeOverride(Size arrangeSize);
        protected virtual DevExpress.Xpf.Core.Native.FloatingIndicator CreateFloatingIndicator();
        protected virtual SelectionIndicatorTransition CreateTransition();
        protected internal virtual bool GetEnableAnimations();
        public static SelectionIndicatorDecorator GetInstance(DependencyObject element);
        protected override Visual GetVisualChild(int index);
        protected internal virtual void HideFloatingIndicator(SelectionIndicator toValue);
        protected internal virtual void ItemIsSelectedChanged(SelectionIndicator indicator, bool oldValue, bool newValue);
        protected override Size MeasureOverride(Size constraint);
        protected internal virtual void OnDurationChanged(int oldValue, int newValue);
        protected internal virtual void OnEasingFunctionChanged(IEasingFunction oldValue, IEasingFunction newValue);
        protected internal virtual void OnEnableAnimationsChanged(bool oldValue, bool newValue);
        protected virtual void OnLayoutUpdated(object sender, EventArgs e);
        protected virtual void OnSelectionIndicatorStyleChanged(Style oldValue, Style newValue);
        protected internal virtual void RemoveIndicator(SelectionIndicator value);
        public static void SetInstance(DependencyObject element, SelectionIndicatorDecorator value);
        protected internal virtual void ShowFloatingIndicator(SelectionIndicator fromValue);
        protected internal virtual void TransitionCompleted(SelectionIndicatorTransition transition, SelectionIndicator toValue);
        protected virtual void TranslateIndicator(SelectionIndicator oldIndicator, SelectionIndicator newIndicator);

        protected internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager { get; private set; }

        public IEasingFunction EasingFunction { get; set; }

        public int Duration { get; set; }

        public bool EnableAnimations { get; set; }

        public Style SelectionIndicatorStyle { get; set; }

        public List<SelectionIndicator> Indicators { get; }

        protected SelectionIndicatorTransition CurrentTransition { get; set; }

        protected internal DevExpress.Xpf.Core.Native.FloatingIndicator FloatingIndicator { get; }

        protected override int VisualChildrenCount { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionIndicatorDecorator.<>c <>9;

            static <>c();
            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__7_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

