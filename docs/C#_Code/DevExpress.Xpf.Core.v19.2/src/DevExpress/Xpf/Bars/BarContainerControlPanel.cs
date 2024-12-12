namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class BarContainerControlPanel : Panel, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement
    {
        private BaseBarLayoutCalculator layoutCalculator;
        private BarContainerControl owner;
        private System.Windows.Controls.Orientation orientation;

        public event NotifyCollectionChangedEventHandler VisualChildrenChanged;

        static BarContainerControlPanel();
        protected override Size ArrangeOverride(Size finalSize);
        protected virtual BaseBarLayoutCalculator CreateLayoutCalculator();
        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent);
        object IMultipleElementRegistratorSupport.GetName(object registratorKey);
        private static IBarLayoutTableInfo GetLayoutInfo(object sender);
        private static IEnumerable<BarContainerControlPanel> GetPanels(object sender);
        protected override Size MeasureOverride(Size constraint);
        private static void OnDragWidgetDragCompleted(object sender, DragCompletedEventArgs e);
        private static void OnDragWidgetDragDelta(object sender, DragDeltaEventArgs e);
        private static void OnDragWidgetDragStarted(object sender, DragStartedEventArgs e);
        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation oldValue);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);

        public BarContainerControl Owner { get; set; }

        public double ColumnIndent { get; }

        public double RowIndent { get; }

        protected internal BaseBarLayoutCalculator LayoutCalculator { get; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys { [IteratorStateMachine(typeof(BarContainerControlPanel.<DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__32))] get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarContainerControlPanel.<>c <>9;
            public static Func<BarContainerControl, bool> <>9__28_0;
            public static Func<bool> <>9__28_1;

            static <>c();
            internal bool <CreateLayoutCalculator>b__28_0(BarContainerControl x);
            internal bool <CreateLayoutCalculator>b__28_1();
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__32 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__32(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

