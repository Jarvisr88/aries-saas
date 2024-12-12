namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;

    public class BrickList : Collection<Brick>, IDisposable, IListWrapper<Brick>, IEnumerable<Brick>, IEnumerable
    {
        private RectangleF bounds;

        public BrickList()
        {
            this.bounds = RectangleF.Empty;
        }

        public BrickList(IList<Brick> list) : base(list)
        {
            this.bounds = RectangleF.Empty;
        }

        public virtual void AddRange(IEnumerable<Brick> bricks)
        {
            foreach (Brick brick in bricks)
            {
                base.Add(brick);
            }
        }

        protected virtual RectangleF CalcBounds()
        {
            RectangleF a = (base.Count > 0) ? base[0].InitialRect : RectangleF.Empty;
            for (int i = 1; i < base.Count; i++)
            {
                a = RectangleF.Union(a, base[i].InitialRect);
            }
            return a;
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            this.InvalidateBounds();
        }

        void IListWrapper<Brick>.Add(Brick brick)
        {
            base.Add(brick);
        }

        void IListWrapper<Brick>.Insert(Brick brick, int index)
        {
            throw new NotSupportedException();
        }

        internal int InsertAfter(Brick brick, Brick previous)
        {
            int index = (previous == null) ? 0 : (base.IndexOf(previous) + 1);
            base.Insert(index, brick);
            return index;
        }

        protected override void InsertItem(int index, Brick value)
        {
            base.InsertItem(index, value);
            this.InvalidateBounds();
        }

        public void InvalidateBounds()
        {
            this.bounds = RectangleF.Empty;
        }

        internal void Offset(float yOffset)
        {
            foreach (Brick brick in this)
            {
                brick.Y += yOffset;
            }
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this.InvalidateBounds();
        }

        void IDisposable.Dispose()
        {
            for (int i = 0; i < base.Count; i++)
            {
                base[i].Dispose();
            }
            base.Clear();
        }

        public RectangleF Bounds
        {
            get
            {
                if (this.bounds == RectangleF.Empty)
                {
                    this.bounds = this.CalcBounds();
                }
                return this.bounds;
            }
        }

        public PointF Location =>
            this.Bounds.Location;

        public SizeF Size =>
            this.Bounds.Size;

        public float Height =>
            this.Bounds.Height;

        public float Width =>
            this.Bounds.Width;

        public float Left =>
            this.Bounds.Left;

        public float Top =>
            this.Bounds.Top;

        public float Right =>
            this.Bounds.Right;

        public float Bottom =>
            this.Bounds.Bottom;
    }
}

