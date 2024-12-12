namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;

    public class PopupListBoxNavigationHelper : ListBoxNavigationHelper
    {
        public PopupListBoxNavigationHelper(PopupListBox listBox) : base(listBox)
        {
        }

        protected override void Move(int startIndex, int stopIndex, int delta)
        {
            if (base.Items.Count != 0)
            {
                if (!this.ListBox.IsAsyncServerMode)
                {
                    base.Move(startIndex, stopIndex, delta);
                }
                else if (this.ListBox.ItemsSource is IServerModeCollectionView)
                {
                    this.ListBox.SelectedIndex = startIndex;
                    DXVirtualizingStackPanel panel = (DXVirtualizingStackPanel) LayoutHelper.FindElementByType(this.ListBox, typeof(DXVirtualizingStackPanel));
                    if (panel != null)
                    {
                        panel.BringIndexIntoView(startIndex);
                    }
                }
            }
        }

        private PopupListBox ListBox =>
            base.ListBox as PopupListBox;
    }
}

