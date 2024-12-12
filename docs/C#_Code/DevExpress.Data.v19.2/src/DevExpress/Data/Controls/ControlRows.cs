namespace DevExpress.Data.Controls
{
    using System;
    using System.ComponentModel;

    public enum ControlRows
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public const ControlRows Source = ControlRows.Source;,
        public const ControlRows Visible = ControlRows.Visible;,
        public const ControlRows Selected = ControlRows.Selected;
    }
}

