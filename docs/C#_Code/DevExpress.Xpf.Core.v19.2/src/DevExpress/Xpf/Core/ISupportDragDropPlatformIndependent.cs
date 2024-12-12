namespace DevExpress.Xpf.Core
{
    using System;

    public interface ISupportDragDropPlatformIndependent
    {
        bool CanStartDrag(object sender, IndependentMouseButtonEventArgs e);
    }
}

