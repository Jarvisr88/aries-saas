namespace DevExpress.Xpf.Editors.Flyout
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Flyout.Native;
    using System;

    [ToolboxTabName("DX.19.2: Common Controls"), DXToolboxBrowsable]
    public class FlyoutControl : FlyoutBase
    {
        static FlyoutControl()
        {
            Type type = typeof(FlyoutControl);
        }

        public FlyoutControl()
        {
            base.DefaultStyleKey = typeof(FlyoutControl);
        }
    }
}

