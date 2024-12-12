namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public interface IScrollBarThumbDragDeltaListener
    {
        void OnScrollBarThumbDragDelta(DragDeltaEventArgs e);
        void OnScrollBarThumbMouseMove(MouseEventArgs e);

        System.Windows.Controls.Orientation Orientation { get; }

        System.Windows.Controls.Primitives.ScrollBar ScrollBar { get; set; }
    }
}

