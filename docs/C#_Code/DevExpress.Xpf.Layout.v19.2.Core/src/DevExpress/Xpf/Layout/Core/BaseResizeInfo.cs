namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public abstract class BaseResizeInfo
    {
        private Point resizePointCore;
        private Rect startRectCore;

        protected BaseResizeInfo(Rect rect, Point pt)
        {
            this.startRectCore = rect;
            this.resizePointCore = pt;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseResizeInfo))
            {
                return false;
            }
            BaseResizeInfo info = obj as BaseResizeInfo;
            return ((info.HResizeType == this.HResizeType) && ((info.VResizeType == this.VResizeType) && (info.ItemRect == this.ItemRect)));
        }

        public override int GetHashCode() => 
            this.ItemRect.GetHashCode() ^ this.ResizePoint.GetHashCode();

        public static bool operator ==(BaseResizeInfo left, BaseResizeInfo right) => 
            !ReferenceEquals(left, right) ? ((left != null) && left.Equals(right)) : true;

        public static bool operator !=(BaseResizeInfo left, BaseResizeInfo right) => 
            !(left == right);

        public Point ResizePoint =>
            this.resizePointCore;

        public Rect ItemRect =>
            this.startRectCore;

        public abstract bool ChangeLocationX { get; }

        public abstract bool ChangeLocationY { get; }

        public abstract bool Horizontal { get; }

        public abstract bool Vertical { get; }

        public abstract ResizeType HResizeType { get; }

        public abstract ResizeType VResizeType { get; }
    }
}

