namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class SimpleXPageContentEngine : XPageContentEngine
    {
        private IPageContentAlgorithm contentAlgorithm;

        private void AddAdditionalBricks(PSPage addingPage, BrickList bricks);
        private void AddBrickCore(PSPage page, IEnumerable<Brick> pageBricks, RectangleF sourceContentRect, bool wrapBricks);
        protected float AddBricks(PSPage page, BrickList bricks, RectangleF sourceContentRect, bool wrapBricks);
        private void AddRepeatedBricks(PSPage page, BrickList repeatedBricks);
        private Dictionary<int, RectangleF> CalculateRowRectangles(BrickList sourceBricks);
        protected virtual IPageContentAlgorithm CreatePageContentAlgorithm();
        public override List<PSPage> CreatePages(PSPage source, RectangleF usefulArea);
        private void ValidateRect(PSPage addingPage, float right);
        private CompositeBrick WrapNonRepeatedBricks(PSPage page, RectangleF sourceContentRect, IEnumerable<Brick> bricks);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleXPageContentEngine.<>c <>9;
            public static Func<KeyValuePair<int, RectangleF>, int> <>9__3_2;
            public static Func<Brick, float> <>9__3_4;
            public static Func<Brick, float> <>9__3_5;
            public static Func<Brick, bool> <>9__7_0;
            public static Func<Brick, int> <>9__10_0;
            public static Func<Brick, float> <>9__10_2;
            public static Func<Brick, float> <>9__10_3;
            public static Func<Brick, float> <>9__10_4;
            public static Func<Brick, float> <>9__10_5;

            static <>c();
            internal int <CalculateRowRectangles>b__10_0(Brick x);
            internal float <CalculateRowRectangles>b__10_2(Brick item);
            internal float <CalculateRowRectangles>b__10_3(Brick item);
            internal float <CalculateRowRectangles>b__10_4(Brick item);
            internal float <CalculateRowRectangles>b__10_5(Brick item);
            internal int <CreatePages>b__3_2(KeyValuePair<int, RectangleF> x);
            internal float <CreatePages>b__3_4(Brick brick);
            internal float <CreatePages>b__3_5(Brick brick);
            internal bool <WrapNonRepeatedBricks>b__7_0(Brick brick);
        }

        public class SimplePageContentAlgorithm : IPageContentAlgorithm
        {
            private RectangleF bounds;

            public SimplePageContentAlgorithm();
            public void OnBeforeBuildPages();
            public IList<Brick> Process(IListWrapper<Brick> bricks, RectangleF bounds);

            public RectangleF ContentBounds { get; }

            public bool ClipContent { get; }
        }
    }
}

