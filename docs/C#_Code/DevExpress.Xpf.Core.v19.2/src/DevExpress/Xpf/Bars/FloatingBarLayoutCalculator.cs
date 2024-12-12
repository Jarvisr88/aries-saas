namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class FloatingBarLayoutCalculator : BaseBarLayoutCalculator
    {
        public FloatingBarLayoutCalculator(BarContainerControlPanel container);
        public override Size ArrangeContainer(Size finalSize);
        public override bool CheckBarDocking(FloatingBarPopup popup);
        private object GetChildren();
        public override void InsertFloatBar(Bar bar, bool v);
        public override Size MeasureContainer(Size constraint);
        public override void OnBarControlDrag(IBarLayoutTableInfo barLayoutTableInfo, DragDeltaEventArgs e, bool? move = new bool?());
        public override void OnBarControlDragCompleted(IBarLayoutTableInfo barLayoutTableInfo, DragCompletedEventArgs e);
        public override void OnBarControlDragStart(IBarLayoutTableInfo barLayoutTableInfo, DragStartedEventArgs e);
        protected override void OnOrientationChanged(Orientation oldValue);
    }
}

