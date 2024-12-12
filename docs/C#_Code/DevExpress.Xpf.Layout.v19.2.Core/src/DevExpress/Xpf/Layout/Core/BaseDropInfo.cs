namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public abstract class BaseDropInfo
    {
        private Point dropPointCore;
        private Rect itemRectCore;

        protected BaseDropInfo(Rect rect, Point pt)
        {
            this.itemRectCore = rect;
            this.dropPointCore = pt;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseDropInfo))
            {
                return false;
            }
            BaseDropInfo info = obj as BaseDropInfo;
            return ((info.Type == this.Type) && (info.ItemRect == this.ItemRect));
        }

        public override int GetHashCode() => 
            this.ItemRect.GetHashCode() ^ this.Type.GetHashCode();

        public static bool operator ==(BaseDropInfo left, BaseDropInfo right) => 
            !ReferenceEquals(left, right) ? ((left != null) && left.Equals(right)) : true;

        public static bool operator !=(BaseDropInfo left, BaseDropInfo right) => 
            !(left == right);

        public Point DropPoint =>
            this.dropPointCore;

        public Rect ItemRect =>
            this.itemRectCore;

        public abstract DropType Type { get; }

        public DevExpress.Xpf.Layout.Core.DockType DockType =>
            this.Type.ToDockType();

        public DevExpress.Xpf.Layout.Core.MoveType MoveType =>
            this.Type.ToMoveType();

        public abstract bool Horizontal { get; }

        public abstract bool Vertical { get; }

        public abstract Rect DropRect { get; }

        public virtual int InsertIndex =>
            -1;
    }
}

