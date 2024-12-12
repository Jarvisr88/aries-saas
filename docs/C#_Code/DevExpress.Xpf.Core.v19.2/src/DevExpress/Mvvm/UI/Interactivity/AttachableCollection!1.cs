namespace DevExpress.Mvvm.UI.Interactivity
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Windows;

    public abstract class AttachableCollection<T> : FreezableCollection<T>, IAttachableObject where T: DependencyObject, IAttachableObject
    {
        private DependencyObject _associatedObject;
        private List<T> snapshot;

        internal AttachableCollection()
        {
            this.snapshot = new List<T>();
            this.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
        }

        private void AddItem(T item)
        {
            if (!this.snapshot.Contains(item))
            {
                this.ItemAdded(item);
                this.snapshot.Insert(base.IndexOf(item), item);
            }
        }

        public void Attach(DependencyObject obj)
        {
            if (!ReferenceEquals(obj, this.AssociatedObject))
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException("This AttachableCollection is already attached");
                }
                this.AssociatedObject = obj;
                this.OnAttached();
            }
        }

        private void ClearItems()
        {
            foreach (T local in this.snapshot)
            {
                this.ItemRemoved(local);
            }
            this.snapshot.Clear();
        }

        protected sealed override Freezable CreateInstanceCore() => 
            (Freezable) Activator.CreateInstance(base.GetType());

        public void Detach()
        {
            this.OnDetaching();
            this.AssociatedObject = null;
        }

        internal virtual void ItemAdded(T item)
        {
            if (this.ShouldAttachItem(item))
            {
                this.AssociatedObject.Do<DependencyObject>(x => item.Attach(x));
            }
        }

        internal virtual void ItemRemoved(T item)
        {
            if (item.AssociatedObject != null)
            {
                item.Detach();
            }
        }

        private void NotifyChanged()
        {
            base.WritePostscript();
        }

        protected virtual void OnAttached()
        {
            foreach (T local in this)
            {
                if (this.ShouldAttachItem(local))
                {
                    local.Attach(this.AssociatedObject);
                }
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Move)
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    this.ClearItems();
                    foreach (T local in this)
                    {
                        this.AddItem(local);
                    }
                }
                else
                {
                    if (e.OldItems != null)
                    {
                        foreach (T local2 in e.OldItems)
                        {
                            this.RemoveItem(local2);
                        }
                    }
                    if (e.NewItems != null)
                    {
                        foreach (T local3 in e.NewItems)
                        {
                            this.AddItem(local3);
                        }
                    }
                }
            }
        }

        protected virtual void OnDetaching()
        {
            foreach (T local in this)
            {
                local.Detach();
            }
        }

        private void RemoveItem(T item)
        {
            this.ItemRemoved(item);
            this.snapshot.Remove(item);
        }

        private bool ShouldAttachItem(T item) => 
            !InteractionHelper.GetEnableBehaviorsInDesignTime(this.AssociatedObject) ? (InteractionHelper.IsInDesignMode(this.AssociatedObject) ? !InteractionHelper.IsInDesignMode(item) : true) : true;

        private void VerifyRead()
        {
            base.ReadPreamble();
        }

        private void VerifyWrite()
        {
            base.WritePreamble();
        }

        DependencyObject IAttachableObject.AssociatedObject =>
            this.AssociatedObject;

        protected internal DependencyObject AssociatedObject
        {
            get
            {
                this.VerifyRead();
                return this._associatedObject;
            }
            private set
            {
                this.VerifyWrite();
                this._associatedObject = value;
                this.NotifyChanged();
            }
        }
    }
}

