namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public interface IBatchUpdateable
    {
        void BeginUpdate();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        void CancelUpdate();
        void EndUpdate();

        bool IsUpdateLocked { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        DevExpress.Utils.BatchUpdateHelper BatchUpdateHelper { get; }
    }
}

