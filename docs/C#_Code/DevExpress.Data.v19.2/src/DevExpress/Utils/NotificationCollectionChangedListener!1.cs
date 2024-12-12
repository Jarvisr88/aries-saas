namespace DevExpress.Utils
{
    using System;
    using System.ComponentModel;

    public class NotificationCollectionChangedListener<T> : NotificationCollectionChangedListenerBase<T>
    {
        public NotificationCollectionChangedListener(NotificationCollection<T> collection) : base(collection)
        {
        }

        protected internal virtual void OnObjectChanged(object sender, EventArgs e)
        {
            this.RaiseChanged();
        }

        protected internal virtual void OnObjectChanging(object sender, CancelEventArgs e)
        {
            e.Cancel = this.RaiseChanging();
        }

        protected override void SubscribeObjectEvents(T obj)
        {
            ISupportObjectChanged changed = obj as ISupportObjectChanged;
            if (changed != null)
            {
                changed.Changed += new EventHandler(this.OnObjectChanged);
            }
            ISupportObjectChanging changing = obj as ISupportObjectChanging;
            if (changing != null)
            {
                changing.Changing += new CancelEventHandler(this.OnObjectChanging);
            }
        }

        protected override void UnsubscribeObjectEvents(T obj)
        {
            ISupportObjectChanged changed = obj as ISupportObjectChanged;
            if (changed != null)
            {
                changed.Changed -= new EventHandler(this.OnObjectChanged);
            }
            ISupportObjectChanging changing = obj as ISupportObjectChanging;
            if (changing != null)
            {
                changing.Changing -= new CancelEventHandler(this.OnObjectChanging);
            }
        }
    }
}

