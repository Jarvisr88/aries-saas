namespace DevExpress.Emf
{
    using System;

    public interface IDXBrushVisitor
    {
        void Visit(DXHatchBrush brush);
        void Visit(DXLinearGradientBrush brush);
        void Visit(DXPathGradientBrush brush);
        void Visit(DXSolidBrush brush);
        void Visit(DXTextureBrush brush);
    }
}

