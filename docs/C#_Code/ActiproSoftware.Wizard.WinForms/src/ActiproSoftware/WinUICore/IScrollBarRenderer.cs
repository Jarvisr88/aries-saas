namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [TypeConverter(typeof(GenericExpandableNullableObjectConverter))]
    public interface IScrollBarRenderer : IDisposable, IUIRenderer
    {
        void DrawScrollBarBackground(PaintEventArgs e, Rectangle bounds, ActiproSoftware.WinUICore.ScrollBar scrollBar);
        void DrawScrollBarButton(PaintEventArgs e, Rectangle bounds, ScrollBarButton button);
        void DrawScrollBarThumb(PaintEventArgs e, Rectangle bounds, ScrollBarThumb thumb);
    }
}

