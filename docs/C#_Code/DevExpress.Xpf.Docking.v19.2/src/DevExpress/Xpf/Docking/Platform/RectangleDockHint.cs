namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RectangleDockHint : DockHintElement
    {
        static RectangleDockHint()
        {
            new DependencyPropertyRegistrator<RectangleDockHint>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public RectangleDockHint() : base(DockVisualizerElement.DockZone)
        {
            base.IsHitTestVisible = false;
        }

        protected override Rect CalcBounds(DockingHintAdornerBase adorner) => 
            adorner.HintRect;

        protected override bool CalcVisibleState(DockingHintAdornerBase adorner) => 
            !adorner.HintRect.IsEmpty;

        public override void UpdateEnabledState(DockingHintAdornerBase adorner)
        {
            this.IsEnabled = (this.HotButton != null) && this.HotButton.IsAvailable;
        }

        public override void UpdateHotTrack(DockHintButton button)
        {
            this.HotButton = button;
        }

        protected DockHintButton HotButton { get; private set; }
    }
}

