namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public sealed class DrawingEffect : IDrawingEffect
    {
        public static DrawingEffect AlphaCeilingEffect = new DrawingEffect(0);
        public static DrawingEffect AlphaFloorEffect = new DrawingEffect(1);
        public static DrawingEffect GrayScaleEffect = new DrawingEffect(2);
        private const int alphaCeilingEffectIndex = 0;
        private const int alphaFloorEffectIndex = 1;
        private const int grayScaleEffectIndex = 2;
        private readonly int index;

        private DrawingEffect(int index)
        {
            this.index = index;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            (this.index != 0) ? ((this.index != 1) ? GrayScaleEffect : AlphaFloorEffect) : AlphaCeilingEffect;

        public override bool Equals(object obj)
        {
            DrawingEffect effect = obj as DrawingEffect;
            return ((effect != null) ? (this.index == effect.index) : false);
        }

        public override int GetHashCode() => 
            this.index;

        public void Visit(IDrawingEffectVisitor visitor)
        {
            if (this.index == 0)
            {
                visitor.AlphaCeilingEffectVisit();
            }
            else if (this.index == 1)
            {
                visitor.AlphaFloorEffectVisit();
            }
            else
            {
                visitor.GrayScaleEffectVisit();
            }
        }
    }
}

