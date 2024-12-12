namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Controls;

    public abstract class ListBoxEditItemBase : ListBoxItem
    {
        protected ListBoxEditItemBase()
        {
        }

        protected internal virtual void SubscribeToSelection()
        {
        }

        protected internal virtual void UnsubscribeFromSelection()
        {
        }
    }
}

