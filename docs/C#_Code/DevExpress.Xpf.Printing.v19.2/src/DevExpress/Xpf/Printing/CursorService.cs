namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    public class CursorService : ICursorService
    {
        private readonly CursorContainer cursorContainer;
        private bool isServiceBlocked;
        private bool isCursorChangingSuppressed;
        private string blockId;

        public CursorService(CursorContainer cursorContainer)
        {
            Guard.ArgumentNotNull(cursorContainer, "cursorContainer");
            this.cursorContainer = cursorContainer;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool BlockService(string id)
        {
            Guard.ArgumentIsNotNullOrEmpty(id, "id");
            if (this.isServiceBlocked)
            {
                return false;
            }
            this.blockId = id;
            this.isServiceBlocked = true;
            this.isCursorChangingSuppressed = false;
            return true;
        }

        public bool HideCustomCursor() => 
            this.HideCustomCursor(string.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool HideCustomCursor(string id)
        {
            if ((this.isServiceBlocked && (this.blockId != id)) || this.isCursorChangingSuppressed)
            {
                return false;
            }
            this.cursorContainer.Visibility = Visibility.Collapsed;
            return true;
        }

        public bool SetCursor(FrameworkElement control, CustomCursor customCursor) => 
            this.SetCursor(control, customCursor, string.Empty);

        public bool SetCursor(FrameworkElement control, Cursor cursor) => 
            this.SetCursor(control, cursor, string.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetCursor(FrameworkElement control, CustomCursor customCursor, string id)
        {
            if ((this.isServiceBlocked && (this.blockId != id)) || this.isCursorChangingSuppressed)
            {
                return false;
            }
            control.Cursor = Cursors.None;
            this.cursorContainer.CustomCursor = customCursor;
            this.cursorContainer.Visibility = Visibility.Visible;
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetCursor(FrameworkElement control, Cursor cursor, string id)
        {
            if ((this.isServiceBlocked && (this.blockId != id)) || this.isCursorChangingSuppressed)
            {
                return false;
            }
            Guard.ArgumentNotNull(cursor, "cursor");
            this.cursorContainer.Visibility = Visibility.Collapsed;
            this.cursorContainer.CustomCursor = null;
            control.Cursor = cursor;
            return true;
        }

        public bool SetCursorPosition(Point relativePosition, FrameworkElement relativeTo) => 
            this.SetCursorPosition(relativePosition, relativeTo, string.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetCursorPosition(Point relativePosition, FrameworkElement relativeTo, string id)
        {
            if ((this.isServiceBlocked && (this.blockId != id)) || this.isCursorChangingSuppressed)
            {
                return false;
            }
            if ((this.cursorContainer.CustomCursor == null) || (this.cursorContainer.Visibility == Visibility.Collapsed))
            {
                throw new InvalidOperationException();
            }
            new OnLoadedScheduler().Schedule(delegate {
                Point point = relativeTo.TranslatePoint(relativePosition, this.cursorContainer);
                this.cursorContainer.CursorPosition = point;
            }, relativeTo);
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SetSuppressCursorChanging(bool value)
        {
            if (this.isServiceBlocked)
            {
                return false;
            }
            this.isCursorChangingSuppressed = value;
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool UnblockService(string id)
        {
            Guard.ArgumentIsNotNullOrEmpty(id, "id");
            if (!this.isServiceBlocked)
            {
                return false;
            }
            if (this.blockId != id)
            {
                return false;
            }
            this.blockId = string.Empty;
            this.isServiceBlocked = false;
            return true;
        }
    }
}

