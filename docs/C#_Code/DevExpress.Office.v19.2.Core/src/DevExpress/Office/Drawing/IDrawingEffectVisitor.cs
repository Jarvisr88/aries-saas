namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingEffectVisitor
    {
        void AlphaCeilingEffectVisit();
        void AlphaFloorEffectVisit();
        void GrayScaleEffectVisit();
        void Visit(AlphaBiLevelEffect drawingEffect);
        void Visit(AlphaInverseEffect drawingEffect);
        void Visit(AlphaModulateEffect drawingEffect);
        void Visit(AlphaModulateFixedEffect drawingEffect);
        void Visit(AlphaOutsetEffect drawingEffect);
        void Visit(AlphaReplaceEffect drawingEffect);
        void Visit(BiLevelEffect drawingEffect);
        void Visit(BlendEffect drawingEffect);
        void Visit(BlurEffect drawingEffect);
        void Visit(ColorChangeEffect drawingEffect);
        void Visit(ContainerEffect drawingEffect);
        void Visit(DuotoneEffect drawingEffect);
        void Visit(Effect drawingEffect);
        void Visit(FillEffect drawingEffect);
        void Visit(FillOverlayEffect drawingEffect);
        void Visit(GlowEffect drawingEffect);
        void Visit(HSLEffect drawingEffect);
        void Visit(InnerShadowEffect drawingEffect);
        void Visit(LuminanceEffect drawingEffect);
        void Visit(OuterShadowEffect drawingEffect);
        void Visit(PresetShadowEffect drawingEffect);
        void Visit(ReflectionEffect drawingEffect);
        void Visit(RelativeOffsetEffect drawingEffect);
        void Visit(SoftEdgeEffect drawingEffect);
        void Visit(SolidColorReplacementEffect drawingEffect);
        void Visit(TintEffect drawingEffect);
        void Visit(TransformEffect drawingEffect);
    }
}

