namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class MergeBrickHelper
    {
        protected void CalculateLTRB(RectangleDF rect, ref double left, ref double top, ref double right, ref double bottom)
        {
            if (rect.Left < left)
            {
                left = rect.Left;
            }
            if (rect.Top < top)
            {
                top = rect.Top;
            }
            if (rect.Right > right)
            {
                right = rect.Right;
            }
            if (rect.Bottom > bottom)
            {
                bottom = rect.Bottom;
            }
        }

        private static object CombineMergeValue(object mergeValue, VisualBrick visualBrick, RectangleDF brickRect)
        {
            MergeDirection mergeDirection = GetMergeDirection(visualBrick);
            return ((mergeDirection == MergeDirection.Vertical) ? CombineMergeValueByVertical(mergeValue, brickRect) : ((mergeDirection == MergeDirection.Horizontal) ? CombineMergeValueByHorizontal(mergeValue, brickRect) : mergeValue));
        }

        private static object CombineMergeValueByHorizontal(object mergeValue, RectangleDF brickRect)
        {
            RectangleDF edf = GraphicsUnitConverter.Convert(brickRect, (float) 300f, (float) 96f);
            object[] keyParts = new object[] { mergeValue, Math.Round(edf.Y, 2), Math.Round((double) edf.Height, 2) };
            return new MultiKey(keyParts);
        }

        private static object CombineMergeValueByVertical(object mergeValue, RectangleDF brickRect)
        {
            RectangleDF edf = GraphicsUnitConverter.Convert(brickRect, (float) 300f, (float) 96f);
            object[] keyParts = new object[] { mergeValue, Math.Round(edf.X, 2), Math.Round((double) edf.Width, 2) };
            return new MultiKey(keyParts);
        }

        private static Dictionary<object, BrickLayoutInfo> CreateBrickInfos(CompositeBrick innerPageBrick)
        {
            Dictionary<object, BrickLayoutInfo> brickInfo = new Dictionary<object, BrickLayoutInfo>();
            NestedBrickIterator iterator = new NestedBrickIterator(innerPageBrick.InnerBrickList);
            while (iterator.MoveNext())
            {
                VisualBrick currentBrick = iterator.CurrentBrick as VisualBrick;
                if (currentBrick != null)
                {
                    UpdateBrickInfos(brickInfo, currentBrick, RectangleDF.FromRectangleF(iterator.CurrentBrickRectangle));
                }
            }
            return brickInfo;
        }

        private static RectangleF GetClippedRect(MergeDirection mergeDirection, RectangleF rect, RectangleF clipRect)
        {
            if (!clipRect.IsEmpty)
            {
                if (mergeDirection == MergeDirection.Vertical)
                {
                    return GetVerticalIntersection(rect, clipRect);
                }
                if (mergeDirection == MergeDirection.Horizontal)
                {
                    return GetHorizontalIntersection(rect, clipRect);
                }
                rect.Intersect(clipRect);
            }
            return rect;
        }

        protected virtual VisualBrick GetCommonPrototypeBrick(List<BrickLayoutInfo> bricksToMerge)
        {
            Comparison<BrickLayoutInfo> comparison = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Comparison<BrickLayoutInfo> local1 = <>c.<>9__18_0;
                comparison = <>c.<>9__18_0 = (x, y) => Comparer<double>.Default.Compare(x.Rect.Top, y.Rect.Top);
            }
            bricksToMerge.Sort(comparison);
            return (VisualBrick) bricksToMerge.First<BrickLayoutInfo>().Brick;
        }

        private static RectangleF GetHorizontalIntersection(RectangleF baseRect, RectangleF clipRect)
        {
            float x = Math.Max(baseRect.X, clipRect.X);
            float num2 = Math.Min(baseRect.Right, clipRect.Right);
            return new RectangleF(x, baseRect.Y, Math.Max((float) (num2 - x), (float) 0f), baseRect.Height);
        }

        private static MergeDirection GetMergeDirection(VisualBrick brick) => 
            (brick != null) ? brick.SafeGetAttachedValue<MergeDirection>(BrickAttachedProperties.MergeDirection, MergeDirection.Vertical) : MergeDirection.Vertical;

        protected RectangleDF GetUnionRect(List<BrickLayoutInfo> brickInfos)
        {
            double maxValue = double.MaxValue;
            double top = double.MaxValue;
            double minValue = double.MinValue;
            double bottom = double.MinValue;
            foreach (BrickLayoutInfo info in brickInfos)
            {
                this.CalculateLTRB(info.Rect, ref maxValue, ref top, ref minValue, ref bottom);
            }
            return RectangleDF.FromLTRB(maxValue, top, minValue, bottom);
        }

        private static RectangleF GetVerticalIntersection(RectangleF baseRect, RectangleF clipRect)
        {
            float y = Math.Max(baseRect.Y, clipRect.Y);
            float num2 = Math.Min(baseRect.Bottom, clipRect.Bottom);
            return new RectangleF(baseRect.X, y, baseRect.Width, Math.Max((float) (num2 - y), (float) 0f));
        }

        protected virtual void MergeBricks(List<BrickLayoutInfo> bricksToMerge, Action<Brick, RectangleDF> addBrick, PSPage page, BrickModifier pageBrickModifier)
        {
            if (bricksToMerge.Count >= 1)
            {
                VisualBrick commonPrototypeBrick = this.GetCommonPrototypeBrick(bricksToMerge);
                RectangleDF unionRect = this.GetUnionRect(bricksToMerge);
                if ((!FloatsComparer.Default.FirstLessSecond((double) unionRect.Height, (double) commonPrototypeBrick.Height) || (bricksToMerge.Count != 1)) && (unionRect.Height > 0f))
                {
                    VisualBrick brick2 = (VisualBrick) commonPrototypeBrick.Clone();
                    addBrick(brick2, unionRect);
                    brick2.OnAfterMerge();
                    brick2.BrickOwner.AddToSummaryUpdater(brick2, commonPrototypeBrick);
                    Action<BrickLayoutInfo> action = <>c.<>9__19_0;
                    if (<>c.<>9__19_0 == null)
                    {
                        Action<BrickLayoutInfo> local1 = <>c.<>9__19_0;
                        action = <>c.<>9__19_0 = delegate (BrickLayoutInfo item) {
                            if (item.Brick != null)
                            {
                                item.Brick.IsVisible = false;
                            }
                        };
                    }
                    bricksToMerge.ForEach(action);
                }
            }
        }

        protected virtual void OnEndProcess(PSPage page)
        {
        }

        protected virtual void OnStartProcess(PSPage page)
        {
        }

        public void ProcessBricks(PrintingSystemBase ps, Dictionary<Brick, RectangleDF> bricks)
        {
            this.OnStartProcess(null);
            Dictionary<object, List<BrickLayoutInfo>> brickListContainer = new Dictionary<object, List<BrickLayoutInfo>>();
            foreach (KeyValuePair<Brick, RectangleDF> pair in bricks)
            {
                double offsetX = pair.Value.Left - pair.Key.Location.X;
                RectangleDF edf = pair.Value;
                PointF location = pair.Key.Location;
                double offsetY = edf.Top - location.Y;
                Brick[] brickArray1 = new Brick[] { pair.Key };
                NestedBrickIterator iterator = new NestedBrickIterator(brickArray1);
                while (iterator.MoveNext())
                {
                    VisualBrick currentBrick = iterator.CurrentBrick as VisualBrick;
                    if (currentBrick != null)
                    {
                        this.UpdateMergeBricks(brickListContainer, currentBrick, iterator.CurrentBrickRectangle, iterator.CurrentClipRectangle, offsetX, offsetY);
                    }
                }
            }
            Action<Brick, RectangleDF> addBrick = delegate (Brick brick, RectangleDF rect) {
                brick.Initialize(ps, RectangleF.Empty);
                brick.SetBounds(rect.ToRectangleF(), 300f);
                bricks.Add(brick, rect);
            };
            foreach (KeyValuePair<object, List<BrickLayoutInfo>> pair2 in brickListContainer)
            {
                this.MergeBricks(pair2.Value, addBrick, null, BrickModifier.None);
            }
            this.OnEndProcess(null);
        }

        public void ProcessPage(PrintingSystemBase ps, PSPage page)
        {
            if (page.InnerBrickList.Count != 0)
            {
                this.OnStartProcess(page);
                Dictionary<object, BrickLayoutInfo> dictionary = null;
                Dictionary<object, BrickLayoutInfo> dictionary2 = null;
                for (int i = 0; i < page.InnerBrickList.Count; i++)
                {
                    CompositeBrick innerPageBrick = page.InnerBrickList[i] as CompositeBrick;
                    if (innerPageBrick.Modifier == BrickModifier.MarginalHeader)
                    {
                        dictionary = CreateBrickInfos(innerPageBrick);
                    }
                    else if (innerPageBrick.Modifier == BrickModifier.MarginalFooter)
                    {
                        dictionary2 = CreateBrickInfos(innerPageBrick);
                    }
                }
                int num2 = 0;
                while (num2 < page.InnerBrickList.Count)
                {
                    CompositeBrick innerPageBrick = page.InnerBrickList[num2] as CompositeBrick;
                    Dictionary<object, List<BrickLayoutInfo>> brickListContainer = new Dictionary<object, List<BrickLayoutInfo>>();
                    RectangleF initialRect = page.InitialRect;
                    if (innerPageBrick.RightToLeftLayout)
                    {
                        initialRect.X = -initialRect.X;
                    }
                    NestedBrickIterator iterator1 = new NestedBrickIterator(innerPageBrick);
                    iterator1.ClipRect = initialRect;
                    NestedBrickIterator iterator = iterator1;
                    while (true)
                    {
                        if (!iterator.MoveNext())
                        {
                            Action<Brick, RectangleDF> addBrick = delegate (Brick brick, RectangleDF rect) {
                                brick.Initialize(ps, RectangleF.Empty);
                                RectangleF bounds = rect.ToRectangleF();
                                if (innerPageBrick.RightToLeftLayout)
                                {
                                    bounds.X = innerPageBrick.Width - bounds.Right;
                                }
                                brick.SetBounds(bounds, 300f);
                                innerPageBrick.InnerBrickList.Add(brick);
                            };
                            foreach (KeyValuePair<object, List<BrickLayoutInfo>> pair in brickListContainer)
                            {
                                this.MergeBricks(pair.Value, addBrick, page, innerPageBrick.Modifier);
                            }
                            if ((dictionary != null) && (dictionary2 != null))
                            {
                                foreach (KeyValuePair<object, BrickLayoutInfo> pair2 in dictionary)
                                {
                                    if (!brickListContainer.ContainsKey(pair2.Key) && dictionary2.ContainsKey(pair2.Key))
                                    {
                                        RectangleF ef3 = pair2.Value.Rect.ToRectangleF();
                                        addBrick((VisualBrick) pair2.Value.Brick.Clone(), new RectangleDF((double) ef3.X, 0.0, ef3.Width, innerPageBrick.Height));
                                    }
                                }
                            }
                            num2++;
                            break;
                        }
                        VisualBrick currentBrick = iterator.CurrentBrick as VisualBrick;
                        if (currentBrick != null)
                        {
                            this.UpdateMergeBricks(brickListContainer, currentBrick, iterator.CurrentBrickRectangle, iterator.CurrentClipRectangle, 0.0, 0.0);
                        }
                    }
                }
                this.OnEndProcess(page);
            }
        }

        private static bool TryGetMergeValue(VisualBrick brick, out object value) => 
            brick.TryGetAttachedValue<object>(BrickAttachedProperties.MergeValue, out value);

        private static void UpdateBrickInfos(Dictionary<object, BrickLayoutInfo> brickInfo, VisualBrick visualBrick, RectangleDF brickRect)
        {
            object obj2 = null;
            if (TryGetMergeValue(visualBrick, out obj2))
            {
                obj2 = CombineMergeValue(obj2, visualBrick, brickRect);
                brickInfo[obj2] = new BrickLayoutInfo(visualBrick, brickRect);
            }
        }

        private void UpdateMergeBricks(Dictionary<object, List<BrickLayoutInfo>> brickListContainer, VisualBrick visualBrick, RectangleF rect, RectangleF clipRect, double offsetX, double offsetY)
        {
            object obj2 = null;
            if (TryGetMergeValue(visualBrick, out obj2))
            {
                RectangleDF brickRect = RectangleDF.Offset(RectangleDF.FromRectangleF(GetClippedRect(GetMergeDirection(visualBrick), rect, clipRect)), offsetX, offsetY);
                obj2 = CombineMergeValue(obj2, visualBrick, brickRect);
                List<BrickLayoutInfo> list = null;
                if (!brickListContainer.TryGetValue(obj2, out list))
                {
                    list = new List<BrickLayoutInfo>();
                    brickListContainer[obj2] = list;
                }
                list.Add(new BrickLayoutInfo(visualBrick, brickRect));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MergeBrickHelper.<>c <>9 = new MergeBrickHelper.<>c();
            public static Comparison<BrickLayoutInfo> <>9__18_0;
            public static Action<BrickLayoutInfo> <>9__19_0;

            internal int <GetCommonPrototypeBrick>b__18_0(BrickLayoutInfo x, BrickLayoutInfo y) => 
                Comparer<double>.Default.Compare(x.Rect.Top, y.Rect.Top);

            internal void <MergeBricks>b__19_0(BrickLayoutInfo item)
            {
                if (item.Brick != null)
                {
                    item.Brick.IsVisible = false;
                }
            }
        }
    }
}

