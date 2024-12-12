namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class DXWindowBackgroundPanel : BackgroundPanel
    {
        public DXWindowBackgroundPanel()
        {
            ContentControlHelper.SetContentIsNotLogical(this, true);
        }
    }
}

