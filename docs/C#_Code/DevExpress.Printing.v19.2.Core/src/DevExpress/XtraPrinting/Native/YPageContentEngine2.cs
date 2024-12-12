namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class YPageContentEngine2 : YPageContentEngine
    {
        public YPageContentEngine2(PSPage psPage, PrintingSystemBase ps, YPageContentEngine2 previous) : base(psPage, ps)
        {
            if (previous != null)
            {
                this.PrintedLengths = previous.PrintedLengths;
            }
            else
            {
                this.PrintedLengths = new Dictionary<Brick, float>();
            }
        }

        private bool AllBricksAddedToPages(DocumentBand docBand) => 
            docBand.Bricks.All<Brick>(brick => BrickFullyAddedToPages(this.PrintedLengths, brick));

        private static bool BrickFullyAddedToPages(IDictionary<Brick, float> printedLengths, Brick brick)
        {
            float num;
            return (TryGetValue(printedLengths, brick, out num) && (brick.Rect.Height == num));
        }

        protected override IPageContentAlgorithm CreateAlgorithm(DocumentBand docBand, PointF offset, bool forceSplit) => 
            new ContentAlgorithmByY2(this, docBand, offset, forceSplit, base.ps);

        public override void OnBuildDocumentBand(DocumentBand docBand)
        {
            base.OnBuildDocumentBand(docBand);
            if (this.AllBricksAddedToPages(docBand))
            {
                foreach (Brick brick in docBand.Bricks)
                {
                    this.PrintedLengths.Remove(brick);
                }
            }
        }

        private static bool TryGetValue(IDictionary<Brick, float> dictionary, Brick brick, out float value)
        {
            if ((dictionary != null) && dictionary.TryGetValue(brick, out value))
            {
                return true;
            }
            value = 0f;
            return false;
        }

        private IDictionary<Brick, float> PrintedLengths { get; set; }

        protected class ContentAlgorithmByY2 : YPageContentEngine.ContentAlgorithmByY
        {
            public ContentAlgorithmByY2(YPageContentEngine2 pageContentEngine, DocumentBand documentBand, PointF offset, bool forceSplit, IPrintingSystemContext ps) : base(pageContentEngine, documentBand, offset, forceSplit, ps)
            {
            }

            private static float CalcPrintedLength(BrickContainer brick, float bottom) => 
                Math.Abs(brick.BrickOffsetY) + (bottom - brick.Rect.Top);

            protected override void FillPage(out float maxBrickBound)
            {
                maxBrickBound = this.MinBound;
                Brick previous = null;
                for (int i = 0; i < base.bricks.Count; i++)
                {
                    Brick brick = base.bricks[i];
                    if (brick.CanAddToPage && !YPageContentEngine2.BrickFullyAddedToPages(this.PrintedLengths, brick))
                    {
                        RectangleF rect = RectFBase.Offset(brick.InitialRect, base.bounds.X + base.offset.X, base.bounds.Y + base.offset.Y);
                        if (RectContains(base.bounds, rect))
                        {
                            if (brick.CanOverflow)
                            {
                                brick = base.CreateBrickContainer(brick, brick.InitialRect);
                            }
                            previous = brick;
                            brick.PageBuilderOffset = new PointF(base.bounds.X + base.offset.X, base.bounds.Y + base.offset.Y);
                            base.newPageBricks.Add(brick);
                            this.PrintedLengths[brick] = rect.Height;
                        }
                        else if (base.bounds.IntersectsWith(rect) && ((brick is PanelBrick) && ((PanelBrick) brick).Merged))
                        {
                            PanelBrick item = (PanelBrick) brick.Clone();
                            item.InitialRect = RectangleF.Intersect(base.bounds, rect);
                            item.CenterChildControls();
                            base.newPageBricks.Add(item);
                            this.PrintedLengths[brick] = rect.Height;
                        }
                        else if (base.bounds.IntersectsWith(rect))
                        {
                            brick.PageBuilderOffset = new PointF(base.offset.X, 0f);
                            BrickContainer container = base.CreateBrickContainer(brick, RectangleF.Intersect(base.bounds, rect));
                            container.PageBuilderOffset = PointF.Empty;
                            if (BetweenTopAndBottom(base.bounds.Top, rect))
                            {
                                container.BrickOffsetY = rect.Y - base.bounds.Y;
                            }
                            base.intersectItems.Add(new ContentAlgorithmBase.BrickItem(container, previous));
                        }
                    }
                }
            }

            protected override void OnIntersectedBrickAdded(Brick brick, float brickBound)
            {
                if (!base.forceSplitY)
                {
                    float num2;
                    float num = CalcPrintedLength((BrickContainer) brick, brickBound);
                    RectangleF rect = brick.Rect;
                    rect.Height = ((BrickContainer) brick).BrickOffsetY + num;
                    if (YPageContentEngine2.TryGetValue(this.PrintedLengths, ((BrickContainer) brick).Brick, out num2))
                    {
                        float num3 = num - num2;
                        float num4 = rect.Height - num3;
                        rect.Y = rect.Bottom - num3;
                        BrickContainer container1 = (BrickContainer) brick;
                        container1.BrickOffsetY -= num4;
                    }
                    this.PrintedLengths[((BrickContainer) brick).Brick] = num;
                    brick.Rect = rect;
                }
            }

            private IDictionary<Brick, float> PrintedLengths =>
                ((YPageContentEngine2) base.pageContentEngine).PrintedLengths;
        }
    }
}

