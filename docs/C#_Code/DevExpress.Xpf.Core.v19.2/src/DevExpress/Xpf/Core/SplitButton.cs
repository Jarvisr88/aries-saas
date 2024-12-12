namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Automation.Peers;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Common Controls")]
    public class SplitButton : DropDownButtonBase
    {
        static SplitButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new SplitButtonAutomationPeer(this);

        protected internal sealed override bool ActAsDropDown =>
            false;
    }
}

