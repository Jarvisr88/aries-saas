namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BrickBaseEnumerator : IEnumerator
    {
        private IEnumerator en;
        private EnumStack stack;

        internal BrickBaseEnumerator(BrickBase brick)
        {
            this.en = this.CreateEnumerator(brick);
            this.stack = new EnumStack();
        }

        private CompositeBrickEnumerator CreateEnumerator(BrickBase brick) => 
            new CompositeBrickEnumerator(brick, this.GetEnumerator(brick.InnerBrickList));

        public RectangleF GetCurrentBrickBounds()
        {
            RectangleF empty = RectangleF.Empty;
            for (int i = 0; i < this.stack.Count; i++)
            {
                RectangleF brickRectangle = this.stack[i].GetBrickRectangle();
                empty = RectFBase.Offset(brickRectangle, empty.X, empty.Y);
            }
            return empty;
        }

        public RectangleF GetCurrentBrickVisibleBounds()
        {
            RectangleF empty = RectangleF.Empty;
            if (this.stack.Count > 0)
            {
                empty = this.stack[0].GetBrickRectangle();
                for (int i = 1; i < this.stack.Count; i++)
                {
                    CompositeBrickEnumerator enumerator2 = this.stack[i];
                    RectangleF brickRectangle = enumerator2.GetBrickRectangle();
                    brickRectangle.Offset(empty.Location);
                    if (!enumerator2.BrickContainer.NoClip)
                    {
                        brickRectangle.Intersect(empty);
                    }
                    empty = brickRectangle;
                }
            }
            return empty;
        }

        protected virtual IEnumerator GetEnumerator(IList bricks) => 
            bricks.GetEnumerator();

        public int[] GetStackIndices()
        {
            int[] numArray = new int[this.stack.Count];
            for (int i = 0; i < this.stack.Count; i++)
            {
                numArray[i] = this.stack[i].CurrentIndex;
            }
            return numArray;
        }

        public virtual bool MoveNext()
        {
            if (this.stack.IsEmpty)
            {
                this.stack.Push(this.en);
                return this.stack.Top.MoveNext();
            }
            if (this.CurrentBrick is CompositeBrick)
            {
                this.stack.Push(this.CreateEnumerator((CompositeBrick) this.CurrentBrick));
                return this.stack.Top.MoveNext();
            }
            while (!this.stack.Top.MoveNext())
            {
                this.stack.Pop();
                if (this.stack.IsEmpty)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void Reset()
        {
            this.stack.Clear();
        }

        public virtual object Current =>
            this.CurrentBrick;

        public BrickBase CurrentBrick =>
            this.stack.Top.Current;

        protected BrickBase ParentBrick =>
            this.stack.Top.BrickContainer;

        private class CompositeBrickEnumerator : SimpleEnumerator
        {
            public CompositeBrickEnumerator(BrickBase brickContainer, IEnumerator en) : base(en)
            {
                this.BrickContainer = brickContainer;
            }

            public RectangleF GetBrickRectangle() => 
                (this.Current != null) ? this.GetBrickRectangle(this.Current, this.BrickContainer.InnerBrickListOffset) : RectangleF.Empty;

            private RectangleF GetBrickRectangle(BrickBase brick, PointF offset)
            {
                RectangleF viewRectangle = brick.GetViewRectangle();
                viewRectangle.Offset(offset);
                if (this.BrickContainer.RightToLeftLayout)
                {
                    viewRectangle.X = this.BrickContainer.InitialRect.Width - viewRectangle.Right;
                }
                return viewRectangle;
            }

            public BrickBase BrickContainer { get; private set; }

            public BrickBase Current =>
                this.Current as BrickBase;
        }

        private class EnumStack : StackBase
        {
            public BrickBaseEnumerator.CompositeBrickEnumerator Top =>
                (BrickBaseEnumerator.CompositeBrickEnumerator) base.Peek();

            public BrickBaseEnumerator.CompositeBrickEnumerator this[int index] =>
                (BrickBaseEnumerator.CompositeBrickEnumerator) base.list[index];
        }
    }
}

