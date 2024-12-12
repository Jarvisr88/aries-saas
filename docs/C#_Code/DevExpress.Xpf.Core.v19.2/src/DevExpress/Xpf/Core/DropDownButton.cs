namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Common Controls")]
    public class DropDownButton : DropDownButtonBase
    {
        static DropDownButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        protected internal sealed override bool ActAsDropDown =>
            true;
    }
}

