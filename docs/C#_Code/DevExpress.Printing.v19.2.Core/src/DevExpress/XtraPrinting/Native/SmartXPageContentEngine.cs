namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class SmartXPageContentEngine : SimpleXPageContentEngine
    {
        protected override IPageContentAlgorithm CreatePageContentAlgorithm();

        private class ContentAlgorithmByX : ContentAlgorithmBase
        {
            protected internal ContentAlgorithmByX();
            protected override bool ContainsFunction(RectangleF rect1, RectangleF rect2);
            private RectangleF CorrectBrickRect(RectangleF brickRect);
            protected override void FillPage(out float maxBrickBound);
            protected override float GetBrickBound(Brick brick, bool forceSplit, float maxBrickBound);
            protected override float GetMaxBound(RectangleF rect);
            protected override bool IntersectFunction(RectangleF rect1, RectangleF rect2);

            protected override float MinBound { get; }

            protected override float MaxBound { get; set; }
        }
    }
}

