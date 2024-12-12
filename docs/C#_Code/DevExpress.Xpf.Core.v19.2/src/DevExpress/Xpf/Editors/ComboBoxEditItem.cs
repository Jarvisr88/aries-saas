namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [ToolboxItem(false)]
    public class ComboBoxEditItem : ListBoxEditItem
    {
        public ComboBoxEditItem()
        {
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            e.SetHandled(true);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (base.Owner != null)
            {
                this.ParentListBox.NotifyComboBoxItemMouseDown(this);
            }
            base.OnMouseLeftButtonDown(e);
            e.SetHandled(true);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (base.Owner != null)
            {
                this.ParentListBox.NotifyComboBoxItemMouseUp(this);
            }
            base.OnMouseLeftButtonUp(e);
            e.SetHandled(true);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (base.NotifyOwner == null)
            {
                base.SetCurrentValue(ListBoxItem.IsSelectedProperty, false);
            }
        }

        protected PopupListBox ParentListBox =>
            base.Owner as PopupListBox;
    }
}

