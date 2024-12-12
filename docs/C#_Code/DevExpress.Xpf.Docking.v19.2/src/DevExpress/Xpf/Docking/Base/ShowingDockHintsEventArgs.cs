namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowingDockHintsEventArgs : RoutedEventArgs
    {
        internal ShowingDockHintsEventArgs(BaseLayoutItem draggingSource, BaseLayoutItem draggingTarget)
        {
            this.DraggingSource = draggingSource;
            this.DraggingTarget = draggingTarget;
            base.RoutedEvent = DockLayoutManager.ShowingDockHintsEvent;
        }

        public void Disable(DockHint hint)
        {
            this.DockHintsConfiguration.Disable(hint);
        }

        public void DisableAll()
        {
            this.DockHintsConfiguration.DisableAll = true;
        }

        public bool GetIsEnabled(DockHint hint) => 
            this.DockHintsConfiguration.GetIsEnabled(hint);

        public bool GetIsVisible(DockGuide hint) => 
            this.DockHintsConfiguration.GetIsVisible(hint);

        public void Hide(DockGuide hint)
        {
            this.DockHintsConfiguration.Hide(hint);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void Hide(DockHint hint)
        {
            this.DockHintsConfiguration.Hide(hint);
        }

        public void HideAll()
        {
            this.DockHintsConfiguration.HideAll = true;
        }

        internal DevExpress.Xpf.Docking.DockHintsConfiguration DockHintsConfiguration { get; set; }

        internal Rect TabHeader { get; set; }

        internal Rect Tab { get; set; }

        internal int TabIndex { get; set; }

        public BaseLayoutItem DraggingSource { get; private set; }

        public BaseLayoutItem DraggingTarget { get; private set; }
    }
}

