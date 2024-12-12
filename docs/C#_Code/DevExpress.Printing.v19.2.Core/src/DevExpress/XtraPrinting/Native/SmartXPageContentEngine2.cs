namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class SmartXPageContentEngine2 : SimpleXPageContentEngine
    {
        protected override IPageContentAlgorithm CreatePageContentAlgorithm() => 
            new ContentAlgorithmByX();

        private class ContentAlgorithmByX : ContentAlgorithmBase
        {
            private IDictionary<Brick, float> printedLengths = new Dictionary<Brick, float>();

            protected internal ContentAlgorithmByX()
            {
            }

            protected override void AddBricks(List<Brick> bricks)
            {
                base.AddBricks(bricks);
                foreach (Brick brick in bricks)
                {
                    RectangleF rect = brick.Rect;
                    this.printedLengths[brick.GetRealBrick()] = rect.Width;
                }
            }

            private static float CalcPrintedLength(BrickContainer brick, float right) => 
                Math.Abs(brick.BrickOffsetX) + (right - brick.Rect.Left);

            protected override bool ContainsFunction(RectangleF rect1, RectangleF rect2) => 
                PageSizeAccuracyComparer.Instance.ContainsByX(rect1, rect2);

            protected override unsafe void FillPage(out float maxBrickBound)
            {
                maxBrickBound = this.MinBound;
                Brick previous = null;
                foreach (Brick brick2 in base.bricks)
                {
                    float num;
                    RectangleF rect = brick2.Rect;
                    if ((!this.printedLengths.TryGetValue(brick2.GetRealBrick(), out num) || (rect.Width != num)) && (brick2.CanAddToPage && this.IntersectFunction(base.bounds, rect)))
                    {
                        if (this.ContainsFunction(base.bounds, rect))
                        {
                            previous = brick2;
                            base.newPageBricks.Add(brick2);
                            continue;
                        }
                        RectangleF b = rect;
                        RectangleF* efPtr1 = &b;
                        efPtr1.X += num;
                        RectangleF* efPtr2 = &b;
                        efPtr2.Width -= num;
                        RectangleF ef3 = RectangleF.Intersect(base.bounds, b);
                        BrickContainer brick = base.CreateBrickContainer(brick2.GetRealBrick(), ef3);
                        brick.PageBuilderOffset = PointF.Empty;
                        brick.BrickOffsetX = -num;
                        base.intersectItems.Add(new ContentAlgorithmBase.BrickItem(brick, previous));
                    }
                }
            }

            protected override float GetBrickBound(Brick brick, bool forceSplit, float maxBrickBound)
            {
                if (forceSplit)
                {
                    return base.bounds.Right;
                }
                float num = brick.ValidatePageRight(base.bounds.Right, brick.Rect);
                if (FloatsComparer.Default.FirstEqualsSecond((double) brick.Rect.Left, (double) num))
                {
                    RectangleF rect = brick.GetRealBrick().Rect;
                    float num3 = brick.GetRealBrick().Rect.Right - brick.Rect.Left;
                    if (FloatsComparer.Default.FirstGreaterOrEqualSecond((double) num3, (double) base.bounds.Width) || this.printedLengths.ContainsKey(brick.GetRealBrick()))
                    {
                        return base.bounds.Right;
                    }
                }
                if (num > base.bounds.Left)
                {
                    return num;
                }
                float num2 = num;
                return (FloatsComparer.Default.FirstGreaterOrEqualSecond((double) num2, (double) maxBrickBound) ? num : base.bounds.Right);
            }

            protected override float GetMaxBound(RectangleF rect) => 
                rect.Right;

            protected override bool IntersectFunction(RectangleF rect1, RectangleF rect2) => 
                PageSizeAccuracyComparer.Instance.IntersectByX(rect1, rect2);

            protected override void OnIntersectedBrickAdded(Brick brick, float brickBound)
            {
                float num2;
                float num = CalcPrintedLength((BrickContainer) brick, brickBound);
                RectangleF rect = brick.Rect;
                rect.Width = ((BrickContainer) brick).BrickOffsetX + num;
                if (this.printedLengths.TryGetValue(brick.GetRealBrick(), out num2))
                {
                    float num3 = num - num2;
                    float num4 = rect.Width - num3;
                    rect.X = rect.Right - num3;
                    BrickContainer container1 = (BrickContainer) brick;
                    container1.BrickOffsetX -= num4;
                }
                this.printedLengths[brick.GetRealBrick()] = num;
                brick.Rect = rect;
            }

            protected override float MinBound =>
                base.bounds.Left;

            protected override float MaxBound
            {
                get => 
                    base.bounds.Right;
                set => 
                    base.bounds.Width = value - base.bounds.X;
            }
        }
    }
}

