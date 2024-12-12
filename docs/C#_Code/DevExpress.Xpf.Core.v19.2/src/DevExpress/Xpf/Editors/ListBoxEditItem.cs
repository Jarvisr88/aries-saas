namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class ListBoxEditItem : ListBoxEditItemBase
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty InternalAllowItemHighlightingProperty;
        private IListNotificationOwner notifyOwner;

        static ListBoxEditItem()
        {
            Type ownerType = typeof(ListBoxEditItem);
            InternalAllowItemHighlightingProperty = DependencyPropertyManager.Register("InternalAllowItemHighlighting", typeof(bool), ownerType, new PropertyMetadata((d, e) => ((ListBoxEditItem) d).PropertyChangedInternalAllowItemHighlighting()));
        }

        public ListBoxEditItem()
        {
            this.SetDefaultStyleKey(typeof(ListBoxEditItem));
            this.UpdateContentAction = new PostponedAction(() => ReferenceEquals(this.NotifyOwner, null));
            this.UpdateSelectionAction = new PostponedAction(() => ReferenceEquals(this.NotifyOwner, null));
            this.SelectionLocker = new Locker();
        }

        protected internal virtual void ChangeSelectionWithLock(bool isSelected)
        {
            this.SelectionLocker.DoLockedAction(() => this.SetCurrentValue(ListBoxItem.IsSelectedProperty, isSelected));
        }

        private Action CreateUpdateSelectionAction(bool isSelected)
        {
            Action notifyOwnerAction = delegate {
                this.NotifyOwner.OnCollectionChanged(new NotifyItemsProviderSelectionChangedEventArgs(this, isSelected));
            };
            return delegate {
                this.UpdateSelectionAction.PerformPostpone(notifyOwnerAction);
            };
        }

        private bool IsInnerListBox(FrameworkElement eventSource)
        {
            if (eventSource == null)
            {
                return false;
            }
            Predicate<FrameworkElement> predicate = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__27_0;
                predicate = <>c.<>9__27_0 = frameworkElement => frameworkElement is EditorListBox;
            }
            EditorListBox objA = LayoutHelper.FindElement(eventSource, predicate) as EditorListBox;
            return ((objA != null) || !ReferenceEquals(objA, this.Owner));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateContentAction.Perform();
            if (this.Owner != null)
            {
                Binding binding = new Binding(EditorListBox.AllowItemHighlightingProperty.GetName());
                binding.Source = this.Owner;
                base.SetBinding(InternalAllowItemHighlightingProperty, binding);
            }
            this.UpdateVisualState(false);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.UpdateContentAction.PerformPostpone(delegate {
                VisibleListWrapper wrapper = (this.Owner != null) ? (this.Owner.ItemsSource as VisibleListWrapper) : null;
                if (wrapper != null)
                {
                    wrapper.EventLocker.DoLockedAction(() => this.NotifyOwner.OnCollectionChanged(new NotifyItemsProviderChangedEventArgs(ListChangedType.ItemChanged, this)));
                }
                else
                {
                    this.NotifyOwner.OnCollectionChanged(new NotifyItemsProviderChangedEventArgs(ListChangedType.ItemChanged, this));
                }
            });
        }

        protected virtual void OnIsMouseOverChanged()
        {
            this.UpdateVisualState(true);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Action<EditorListBox> action = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Action<EditorListBox> local1 = <>c.<>9__28_0;
                action = <>c.<>9__28_0 = x => x.SyncSelectAllWithoutUpdate();
            }
            this.Owner.Do<EditorListBox>(action);
            base.Dispatcher.BeginInvoke(() => base.Focus(), new object[0]);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            e.SetHandled(true);
            this.Owner.Do<EditorListBox>(x => x.NotifyItemMouseMove(this, e));
            base.OnMouseMove(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if ((this.Owner != null) && this.Owner.OwnerEdit.IsReadOnly)
            {
                e.Handled = true;
            }
            else if ((e.OriginalSource != null) && !this.IsInnerListBox(e.OriginalSource as FrameworkElement))
            {
                Func<EditorListBox, bool> evaluator = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<EditorListBox, bool> local1 = <>c.<>9__26_0;
                    evaluator = <>c.<>9__26_0 = x => x.IsKeyboardFocusWithin;
                }
                this.Owner.If<EditorListBox>(evaluator).Do<EditorListBox>(x => base.Focus());
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, UIElement.IsMouseOverProperty))
            {
                this.OnIsMouseOverChanged();
            }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            this.ProcessNotifyOwner(true);
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            this.ProcessNotifyOwner(false);
        }

        protected virtual void ProcessNotifyOwner(bool isSelected)
        {
            this.UpdateContentAction.Perform();
            if (this.Owner == null)
            {
                this.SelectionLocker.DoLockedActionIfNotLocked(this.CreateUpdateSelectionAction(isSelected));
            }
        }

        private void PropertyChangedInternalAllowItemHighlighting()
        {
            this.UpdateVisualState(true);
        }

        protected virtual void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, (!base.IsMouseOver || !this.InternalAllowItemHighlighting) ? "Unhighlighted" : "Highlighted", useTransitions);
        }

        internal IListNotificationOwner NotifyOwner
        {
            get => 
                this.notifyOwner;
            set
            {
                this.notifyOwner = value;
                this.ProcessNotifyOwner(base.IsSelected);
            }
        }

        private PostponedAction UpdateContentAction { get; set; }

        private PostponedAction UpdateSelectionAction { get; set; }

        private Locker SelectionLocker { get; set; }

        public EditorListBox Owner =>
            ItemsControl.ItemsControlFromItemContainer(this) as EditorListBox;

        private bool InternalAllowItemHighlighting =>
            (bool) base.GetValue(InternalAllowItemHighlightingProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxEditItem.<>c <>9 = new ListBoxEditItem.<>c();
            public static Func<EditorListBox, bool> <>9__26_0;
            public static Predicate<FrameworkElement> <>9__27_0;
            public static Action<EditorListBox> <>9__28_0;

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ListBoxEditItem) d).PropertyChangedInternalAllowItemHighlighting();
            }

            internal bool <IsInnerListBox>b__27_0(FrameworkElement frameworkElement) => 
                frameworkElement is EditorListBox;

            internal void <OnMouseLeftButtonDown>b__28_0(EditorListBox x)
            {
                x.SyncSelectAllWithoutUpdate();
            }

            internal bool <OnPreviewMouseDown>b__26_0(EditorListBox x) => 
                x.IsKeyboardFocusWithin;
        }
    }
}

