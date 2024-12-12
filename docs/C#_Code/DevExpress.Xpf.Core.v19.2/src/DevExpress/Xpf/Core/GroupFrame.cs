namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows.Controls;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the standard GroupBox control instead")]
    public class GroupFrame : GroupBox
    {
        public GroupFrame()
        {
            base.DefaultStyleKey = typeof(GroupFrame);
        }
    }
}

