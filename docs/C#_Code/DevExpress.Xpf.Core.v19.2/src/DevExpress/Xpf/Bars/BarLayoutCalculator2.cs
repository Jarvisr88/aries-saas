namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class BarLayoutCalculator2 : BaseBarLayoutCalculator
    {
        private static double FloatBarVerticalIndent;
        private static double FloatBarHorizontalIndent;
        private readonly BarLayoutTable table;
        private DispatcherOperation updateIsVisibleOperation;

        static BarLayoutCalculator2();
        public BarLayoutCalculator2(BarContainerControlPanel container);
        public override Size ArrangeContainer(Size finalSize);
        public override bool CheckBarDocking(FloatingBarPopup popup);
        protected virtual bool CheckMakeBarFloating(BarLayoutTableCell currentCell);
        public static FloatingBarPopup CreateFloatingBar(BarDockInfo barDockInfo, Point floatBarOffset);
        protected virtual BarLayoutTable CreateLayoutTable();
        [SecuritySafeCritical]
        private static Point GetCursorPos();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool GetCursorPos(out BarLayoutCalculator2.POINT lpPoint);
        private static IBarLayoutTableInfo GetLayoutInfo(object sender);
        public override void InsertFloatBar(Bar bar, bool startDrag);
        protected virtual bool MakeFloating(IBarLayoutTableInfo layoutInfo);
        public override Size MeasureContainer(Size constraint);
        public override void OnBarControlDrag(IBarLayoutTableInfo layoutInfo, DragDeltaEventArgs e, bool? move = new bool?());
        public override void OnBarControlDragCompleted(IBarLayoutTableInfo layoutInfo, DragCompletedEventArgs e);
        public override void OnBarControlDragStart(IBarLayoutTableInfo layoutInfo, DragStartedEventArgs e);
        private void OnContainerChildrenChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnElementAdded(IBarLayoutTableInfo element);
        protected virtual void OnElementIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnElementRemoved(IBarLayoutTableInfo element);
        protected override void OnOrientationChanged(Orientation oldValue);
        protected virtual void OnRequestMeasure(object sender, EventArgs e);
        private void StartDrag(Bar bar);

        public BarLayoutTable LayoutTable { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarLayoutCalculator2.<>c <>9;
            public static Func<BarContainerControlPanel, FloatingBarContainerControl> <>9__6_0;
            public static Func<PresentationSource, Visual> <>9__20_0;
            public static Func<Visual, bool> <>9__20_1;
            public static Func<BarLayoutTableCell, IBarLayoutTableInfo> <>9__20_2;
            public static Func<Bar, BarDockInfo> <>9__25_0;
            public static Func<BarDockInfo, BarControl> <>9__25_1;
            public static Func<BarControl, DragWidget> <>9__25_2;
            public static Func<BarNameScope, UIElement> <>9__27_0;

            static <>c();
            internal Visual <CheckMakeBarFloating>b__20_0(PresentationSource x);
            internal bool <CheckMakeBarFloating>b__20_1(Visual x);
            internal IBarLayoutTableInfo <CheckMakeBarFloating>b__20_2(BarLayoutTableCell x);
            internal UIElement <CreateFloatingBar>b__27_0(BarNameScope x);
            internal FloatingBarContainerControl <CreateLayoutTable>b__6_0(BarContainerControlPanel x);
            internal BarDockInfo <StartDrag>b__25_0(Bar x);
            internal BarControl <StartDrag>b__25_1(BarDockInfo x);
            internal DragWidget <StartDrag>b__25_2(BarControl x);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y);
            public POINT(Point pt);
            public static implicit operator Point(BarLayoutCalculator2.POINT p);
            public static implicit operator BarLayoutCalculator2.POINT(Point p);
        }
    }
}

