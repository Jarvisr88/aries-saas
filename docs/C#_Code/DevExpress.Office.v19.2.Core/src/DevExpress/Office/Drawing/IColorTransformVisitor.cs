namespace DevExpress.Office.Drawing
{
    using System;

    public interface IColorTransformVisitor
    {
        void Visit(AlphaColorTransform transform);
        void Visit(AlphaModulationColorTransform transform);
        void Visit(AlphaOffsetColorTransform transform);
        void Visit(BlueColorTransform transform);
        void Visit(BlueModulationColorTransform transform);
        void Visit(BlueOffsetColorTransform transform);
        void Visit(ComplementColorTransform transform);
        void Visit(GammaColorTransform transform);
        void Visit(GrayscaleColorTransform transform);
        void Visit(GreenColorTransform transform);
        void Visit(GreenModulationColorTransform transform);
        void Visit(GreenOffsetColorTransform transform);
        void Visit(HueColorTransform transform);
        void Visit(HueModulationColorTransform transform);
        void Visit(HueOffsetColorTransform transform);
        void Visit(InverseColorTransform transform);
        void Visit(InverseGammaColorTransform transform);
        void Visit(LuminanceColorTransform transform);
        void Visit(LuminanceModulationColorTransform transform);
        void Visit(LuminanceOffsetColorTransform transform);
        void Visit(RedColorTransform transform);
        void Visit(RedModulationColorTransform transform);
        void Visit(RedOffsetColorTransform transform);
        void Visit(SaturationColorTransform transform);
        void Visit(SaturationModulationColorTransform transform);
        void Visit(SaturationOffsetColorTransform transform);
        void Visit(ShadeColorTransform transform);
        void Visit(TintColorTransform transform);
    }
}

