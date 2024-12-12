namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabPanelStretchView : TabPanelStretchViewBase, IDragPanelVisual
    {
        public static readonly DependencyProperty OwnerProperty;
        public static readonly DependencyProperty DragDropModeProperty;
        private TabPanelStretchView.PinnedLeftDragPanel pinnedLeftDragPanel;
        private TabPanelStretchView.PinnedRightDragPanel pinnedRightDragPanel;
        private TabPanelStretchView.PinnedNoneDragPanel pinnedNoneDragPanel;

        static TabPanelStretchView();
        public TabPanelStretchView();
        IDragPanel IDragPanelVisual.GetDragPanel(IDragPanel sourceDragPanel);
        protected override TabPinMode GetPinMode(FrameworkElement child);
        protected override int GetVisibleIndex(FrameworkElement child);
        private void OnDragDropModeChanged();
        protected override void UpdateVisibleChildren();

        public TabControlStretchView Owner { get; set; }

        public TabControlDragDropMode DragDropMode { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelStretchView.<>c <>9;

            static <>c();
            internal void <.cctor>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }

        private class DragPanelBase : TabControlStretchViewDragPanelBase
        {
            private DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView> controller;

            public DragPanelBase(TabPanelStretchView panel);
            protected override int CorrectIndexWhenMove(FrameworkElement child, int index);
            public void Init(TabControlDragDropMode mode);

            protected override IEnumerable<FrameworkElement> Children { get; }

            protected override DragControllerBase Controller { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly TabPanelStretchView.DragPanelBase.<>c <>9;
                public static Action<DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView>> <>9__6_0;
                public static Action<DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView>> <>9__6_1;
                public static Action<DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView>> <>9__6_2;

                static <>c();
                internal void <Init>b__6_0(DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView> x);
                internal void <Init>b__6_1(DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView> x);
                internal void <Init>b__6_2(DragPanelControllerBase<TabControlStretchViewDragPanelBase, TabPanelStretchView> x);
            }
        }

        private class PinnedLeftDragPanel : TabPanelStretchView.DragPanelBase
        {
            public PinnedLeftDragPanel(TabPanelStretchView panel);

            protected override IEnumerable<FrameworkElement> Children { get; }
        }

        private class PinnedNoneDragPanel : TabPanelStretchView.DragPanelBase
        {
            public PinnedNoneDragPanel(TabPanelStretchView panel);

            protected override IEnumerable<FrameworkElement> Children { get; }
        }

        private class PinnedRightDragPanel : TabPanelStretchView.DragPanelBase
        {
            public PinnedRightDragPanel(TabPanelStretchView panel);

            protected override IEnumerable<FrameworkElement> Children { get; }
        }
    }
}

