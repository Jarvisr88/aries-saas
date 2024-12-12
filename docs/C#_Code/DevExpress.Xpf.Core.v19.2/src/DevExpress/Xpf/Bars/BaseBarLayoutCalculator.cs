namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public abstract class BaseBarLayoutCalculator
    {
        private System.Windows.Controls.Orientation orientation;
        private readonly BarContainerControlPanel container;

        public BaseBarLayoutCalculator(BarContainerControlPanel container);
        public abstract Size ArrangeContainer(Size finalSize);
        public abstract bool CheckBarDocking(FloatingBarPopup popup);
        public abstract void InsertFloatBar(Bar bar, bool v);
        public abstract Size MeasureContainer(Size constraint);
        public abstract void OnBarControlDrag(IBarLayoutTableInfo barLayoutTableInfo, DragDeltaEventArgs e, bool? move = new bool?());
        public abstract void OnBarControlDragCompleted(IBarLayoutTableInfo barLayoutTableInfo, DragCompletedEventArgs e);
        public abstract void OnBarControlDragStart(IBarLayoutTableInfo barLayoutTableInfo, DragStartedEventArgs e);
        protected abstract void OnOrientationChanged(System.Windows.Controls.Orientation oldValue);

        public BarContainerControlPanel Container { get; }

        public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}

