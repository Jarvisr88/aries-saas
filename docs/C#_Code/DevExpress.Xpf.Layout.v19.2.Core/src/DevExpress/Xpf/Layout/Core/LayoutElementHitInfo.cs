namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutElementHitInfo : HitInfo<LayoutElementHitTest>
    {
        private readonly BaseLayoutElement elementCore;
        public static readonly LayoutElementHitInfo Empty = new EmptyHitInfo();

        public LayoutElementHitInfo(Point point, BaseLayoutElement element) : base(point)
        {
            if ((element != null) && this.CheckAndSetHitTest(element, element.Bounds, point, LayoutElementHitTest.Bounds))
            {
                this.elementCore = element;
            }
        }

        protected override LayoutElementHitTest[] GetValidHotTests() => 
            new LayoutElementHitTest[] { LayoutElementHitTest.ControlBox };

        protected override LayoutElementHitTest[] GetValidPressedTests() => 
            new LayoutElementHitTest[] { LayoutElementHitTest.ControlBox, LayoutElementHitTest.Header };

        public ILayoutElement Element =>
            this.elementCore;

        public object Tag { get; set; }

        public override bool IsEmpty =>
            this is EmptyHitInfo;

        public bool IsEnabled =>
            (this.Element != null) && this.Element.IsEnabled;

        public bool InBounds =>
            ((((LayoutElementHitTest) base.HitTest) == LayoutElementHitTest.Bounds) || (this.InHeader || (this.InContent || (this.InBorder || this.InControlBox)))) ? this.IsEnabled : false;

        public bool InHeader =>
            (((LayoutElementHitTest) base.HitTest) == LayoutElementHitTest.Header) && this.IsEnabled;

        public bool InContent =>
            (((LayoutElementHitTest) base.HitTest) == LayoutElementHitTest.Content) && this.IsEnabled;

        public bool InBorder =>
            (((LayoutElementHitTest) base.HitTest) == LayoutElementHitTest.Border) && this.IsEnabled;

        public bool InControlBox =>
            (((LayoutElementHitTest) base.HitTest) == LayoutElementHitTest.ControlBox) && this.IsEnabled;

        public virtual bool InDragBounds =>
            this.InHeader;

        public virtual bool InReorderingBounds =>
            false;

        public virtual bool InResizeBounds =>
            this.InBorder;

        public virtual bool InMenuBounds =>
            this.InHeader;

        public virtual bool InClickBounds =>
            this.InControlBox || this.InHeader;

        public virtual bool InClickPreviewBounds =>
            this.InBounds;

        public virtual bool InDoubleClickBounds =>
            this.InHeader;

        public bool IsDragging =>
            (this.elementCore != null) && this.elementCore.IsDragging;

        private class EmptyHitInfo : LayoutElementHitInfo
        {
            public EmptyHitInfo() : base(new Point(double.NaN, double.NaN), null)
            {
            }

            public override object HitResult =>
                null;
        }
    }
}

