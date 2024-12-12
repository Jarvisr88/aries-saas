namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Drawing;

    public class PageValidator
    {
        private IList bricks;

        public PageValidator(IList bricks)
        {
            this.bricks = bricks;
        }

        private static bool BetweenTopAndBottom(float value, RectangleF rect) => 
            (value > rect.Top) && (value < rect.Bottom);

        private static unsafe RectangleF CreateChildRect(Brick childBrick, RectangleF brickRect)
        {
            RectangleF rect = childBrick.Rect;
            RectangleF* efPtr1 = &rect;
            efPtr1.X += brickRect.X;
            RectangleF* efPtr2 = &rect;
            efPtr2.Y += brickRect.Y;
            return rect;
        }

        public float ValidateBottom(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            this.ValidateBottomCore(pageBottom, rect, (b, r) => b.ValidatePageBottomInternal(pageBottom, r, context));

        public float ValidateBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            this.ValidateBottomCore(pageBounds.Bottom, rect, (b, r) => b.ValidatePageBottom(pageBounds, enforceSplitNonSeparable, r, context));

        private float ValidateBottomCore(float pageBottom, RectangleF rect, Func<Brick, RectangleF, float> validate)
        {
            float minValue = float.MinValue;
            foreach (Brick brick in this.bricks)
            {
                RectangleF ef = CreateChildRect(brick, rect);
                if (BetweenTopAndBottom(pageBottom, ef))
                {
                    float num2 = validate(brick, ef);
                    if (!FloatsComparer.Default.FirstEqualsSecond((double) num2, (double) pageBottom))
                    {
                        minValue = Math.Max(minValue, num2);
                    }
                }
            }
            return ((minValue != float.MinValue) ? Math.Min(pageBottom, minValue) : pageBottom);
        }

        public float ValidateRight(float pageRight, RectangleF rect)
        {
            if (this.bricks.Count == 0)
            {
                return rect.Left;
            }
            float num = pageRight;
            foreach (Brick brick in this.bricks)
            {
                RectangleF ef = CreateChildRect(brick, rect);
                float num2 = brick.ValidatePageRight(num, ef);
                pageRight = Math.Min(pageRight, num2);
            }
            return pageRight;
        }
    }
}

