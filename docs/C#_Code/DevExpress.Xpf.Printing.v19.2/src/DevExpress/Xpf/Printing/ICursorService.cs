namespace DevExpress.Xpf.Printing
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    public interface ICursorService
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool BlockService(string id);
        bool HideCustomCursor();
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool HideCustomCursor(string id);
        bool SetCursor(FrameworkElement control, CustomCursor customCursor);
        bool SetCursor(FrameworkElement control, Cursor cursor);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool SetCursor(FrameworkElement control, CustomCursor customCursor, string id);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool SetCursor(FrameworkElement control, Cursor cursor, string id);
        bool SetCursorPosition(Point relativePosition, FrameworkElement relativeTo);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool SetCursorPosition(Point relativePosition, FrameworkElement relativeTo, string id);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool SetSuppressCursorChanging(bool value);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool UnblockService(string id);
    }
}

