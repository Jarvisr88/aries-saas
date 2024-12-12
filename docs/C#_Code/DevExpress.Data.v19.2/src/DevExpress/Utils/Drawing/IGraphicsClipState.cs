namespace DevExpress.Utils.Drawing
{
    using System;
    using System.Drawing;

    public interface IGraphicsClipState : IDisposable
    {
        Region ClipRegion { get; }

        IntPtr ClipRegionAPI { get; }

        Rectangle? SavedMaximumClipBounds { get; }

        RectangleF DirectXClipRegion { get; }
    }
}

