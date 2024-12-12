namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class SelectionAttachedBehavior : DependencyObject, IWeakEventListener
    {
        public static readonly DependencyProperty SelectedItemsSourceProperty = DependencyProperty.RegisterAttached("SelectedItemsSource", typeof(IList), typeof(SelectionAttachedBehavior), new PropertyMetadata(new PropertyChangedCallback(SelectionAttachedBehavior.OnSelectedItemsSourcePropertyChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty SelectionAttachedBehaviorProperty = DependencyProperty.RegisterAttached("SelectionAttachedBehavior", typeof(SelectionAttachedBehavior), typeof(SelectionAttachedBehavior), new PropertyMetadata(new PropertyChangedCallback(SelectionAttachedBehavior.OnSelectionControlAttachedBehaviorChanged)));
        private bool lockSelection;
        private IList selectedItemsSource;

        public SelectionAttachedBehavior(IList selectedItemsSource)
        {
            this.SelectedItemsSource = selectedItemsSource;
        }

        protected virtual void ConnectToSelectionControl(object control)
        {
            if (this.SelectionControl != null)
            {
                this.SelectionControl.UnsubscribeSelectionChanged();
            }
            this.SelectionControl = SelectionControlWrapper.Create(control);
            this.SelectionControl.SubscribeSelectionChanged(new Action<IList, IList>(this.SelectionChanged));
            this.SelectedItemsSourceChanged();
        }

        protected virtual void DisconnectFromSelectionControl()
        {
            if (this.SelectionControl != null)
            {
                this.SelectionControl.UnsubscribeSelectionChanged();
            }
            this.SelectionControl = null;
        }

        private void DoSelectAction(IList list, Action<object> action)
        {
            foreach (object obj2 in list)
            {
                action(obj2);
            }
        }

        [AttachedPropertyBrowsableForType(typeof(ListBox)), AttachedPropertyBrowsableForType(typeof(ListBoxEdit)), AttachedPropertyBrowsableWhenAttributePresent(typeof(SelectedItemsSourceBrowsableAttribute))]
        public static IList GetSelectedItemsSource(DependencyObject obj) => 
            (IList) obj.GetValue(SelectedItemsSourceProperty);

        private void OnSelectedItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((this.SelectionControl != null) && !this.lockSelection)
            {
                this.lockSelection = true;
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.DoSelectAction(e.NewItems, item => this.SelectionControl.SelectItem(item));
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        this.DoSelectAction(e.OldItems, item => this.SelectionControl.UnselectItem(item));
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        this.SelectionControl.ClearSelection();
                        if (this.SelectedItemsSource != null)
                        {
                            this.DoSelectAction(this.SelectedItemsSource, item => this.SelectionControl.SelectItem(item));
                        }
                        break;

                    default:
                        break;
                }
                this.lockSelection = false;
            }
        }

        private static void OnSelectedItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IList newValue = e.NewValue as IList;
            SetSelectionAttachedBehavior(d, new SelectionAttachedBehavior(newValue));
        }

        private static void OnSelectionControlAttachedBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectionAttachedBehavior oldValue = (SelectionAttachedBehavior) e.OldValue;
            if (oldValue != null)
            {
                oldValue.DisconnectFromSelectionControl();
            }
            SelectionAttachedBehavior newValue = (SelectionAttachedBehavior) e.NewValue;
            if (newValue != null)
            {
                newValue.ConnectToSelectionControl(d);
            }
        }

        private void SelectedItemsSourceChanged()
        {
            this.OnSelectedItemsSourceCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void SelectionChanged(IList addedItems, IList removedItems)
        {
            if (!this.lockSelection && (this.SelectedItemsSource != null))
            {
                this.lockSelection = true;
                ILockable selectedItemsSource = this.SelectedItemsSource as ILockable;
                if (selectedItemsSource != null)
                {
                    selectedItemsSource.BeginUpdate();
                }
                if ((addedItems == null) && (removedItems == null))
                {
                    this.SelectedItemsSource.Clear();
                    foreach (object obj2 in this.SelectionControl.GetSelectedItems())
                    {
                        this.SelectedItemsSource.Add(obj2);
                    }
                }
                else
                {
                    if (addedItems != null)
                    {
                        foreach (object obj3 in addedItems)
                        {
                            this.SelectedItemsSource.Add(obj3);
                        }
                    }
                    if (removedItems != null)
                    {
                        foreach (object obj4 in removedItems)
                        {
                            this.SelectedItemsSource.Remove(obj4);
                        }
                    }
                }
                if (selectedItemsSource != null)
                {
                    selectedItemsSource.EndUpdate();
                }
                this.lockSelection = false;
            }
        }

        public static void SetSelectedItemsSource(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsSourceProperty, value);
        }

        private static void SetSelectionAttachedBehavior(DependencyObject obj, SelectionAttachedBehavior value)
        {
            obj.SetValue(SelectionAttachedBehaviorProperty, value);
        }

        private void SubscribeSelectedItemsSource()
        {
            INotifyCollectionChanged selectedItemsSource = this.SelectedItemsSource as INotifyCollectionChanged;
            if (selectedItemsSource != null)
            {
                CollectionChangedEventManager.AddListener(selectedItemsSource, this);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(CollectionChangedEventManager)))
            {
                return false;
            }
            this.OnSelectedItemsSourceCollectionChanged(sender, (NotifyCollectionChangedEventArgs) e);
            return true;
        }

        private void UnsubscribeSelectedItemsSource()
        {
            INotifyCollectionChanged selectedItemsSource = this.SelectedItemsSource as INotifyCollectionChanged;
            if (selectedItemsSource != null)
            {
                CollectionChangedEventManager.RemoveListener(selectedItemsSource, this);
            }
        }

        private SelectionControlWrapper SelectionControl { get; set; }

        private IList SelectedItemsSource
        {
            get => 
                this.selectedItemsSource;
            set
            {
                this.UnsubscribeSelectedItemsSource();
                this.selectedItemsSource = value;
                this.SubscribeSelectedItemsSource();
            }
        }
    }
}

