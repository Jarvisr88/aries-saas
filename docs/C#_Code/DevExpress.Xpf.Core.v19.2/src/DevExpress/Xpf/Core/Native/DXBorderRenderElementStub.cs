namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class DXBorderRenderElementStub : RenderElementStub
    {
        protected override bool CalcNeverMeasured(FrameworkElement control);
        protected override FrameworkRenderElementContext CreateContextInstance();
    }
}

