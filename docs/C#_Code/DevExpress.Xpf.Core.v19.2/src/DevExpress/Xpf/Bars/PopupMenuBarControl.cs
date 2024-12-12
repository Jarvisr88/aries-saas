namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PopupMenuBarControl : SubMenuBarControl
    {
        static PopupMenuBarControl();
        public PopupMenuBarControl();
        internal PopupMenuBarControl(PopupMenuBase popup);
        protected override void OnContextMenuOpening(ContextMenuEventArgs e);
        protected internal virtual void OnFrameworkElementLostMouseCapture(MouseEventArgs e);
        private void OnHaveVisibleInfosChanged(object sender, EventArgs e);
        protected internal override void UpdateItemsSource(BarItemLinkCollection itemLinks, CustomizedBarItemLinkCollection customLinks);
    }
}

