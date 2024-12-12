namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class TabControlStretchViewDragPanelBase : IDragPanel
    {
        private EventHandler childrenChanged;

        event EventHandler IDragPanel.ChildrenChanged;

        public TabControlStretchViewDragPanelBase(TabPanelStretchView panel);
        protected abstract int CorrectIndexWhenMove(FrameworkElement child, int index);
        bool IDragPanel.CanStartDrag(FrameworkElement child);
        DragWidgetWindow IDragPanel.CreateDragWidget(FrameworkElement child);
        void IDragPanel.DropOnEmptySpace(FrameworkElement child);
        FrameworkElement IDragPanel.Insert(FrameworkElement child, int index);
        FrameworkElement IDragPanel.Move(FrameworkElement child, int index);
        void IDragPanel.OnDragFinished();
        void IDragPanel.Remove(FrameworkElement child);
        void IDragPanel.SetVisibility(FrameworkElement child, Visibility visibility);
        private void FullUpdate();
        public void RaiseChildrenChanged();

        public TabPanelStretchView Panel { get; private set; }

        public TabControlStretchView View { get; }

        protected abstract IEnumerable<FrameworkElement> Children { get; }

        protected abstract DragControllerBase Controller { get; }

        IDragPanelVisual IDragPanel.VisualPanel { get; }

        string IDragPanel.Region { get; }

        Orientation IDragPanel.Orientation { get; }

        IEnumerable<FrameworkElement> IDragPanel.Children { get; }

        DragControllerBase IDragPanel.Controller { get; }
    }
}

