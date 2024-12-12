namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DXListBox : ListBox
    {
        public EventHandler ItemLeftButtonDoubleClick;

        protected override DependencyObject GetContainerForItemOverride()
        {
            DependencyObject containerForItemOverride = base.GetContainerForItemOverride();
            if (containerForItemOverride is Control)
            {
                MouseHelper.SubscribeLeftButtonDoubleClick((Control) containerForItemOverride, delegate (object sender, EventArgs e) {
                    if (this.ItemLeftButtonDoubleClick != null)
                    {
                        this.ItemLeftButtonDoubleClick(this, EventArgs.Empty);
                    }
                });
            }
            return containerForItemOverride;
        }
    }
}

