namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public sealed class DrawingUnderlineFill : IUnderlineFill
    {
        public static DrawingUnderlineFill FollowsText = new DrawingUnderlineFill();

        private DrawingUnderlineFill()
        {
        }

        public IUnderlineFill CloneTo(IDocumentModel documentModel) => 
            FollowsText;

        public override bool Equals(object obj) => 
            obj is DrawingUnderlineFill;

        public override int GetHashCode() => 
            base.GetType().GetHashCode();

        public DrawingUnderlineFillType Type =>
            DrawingUnderlineFillType.FollowsText;
    }
}

