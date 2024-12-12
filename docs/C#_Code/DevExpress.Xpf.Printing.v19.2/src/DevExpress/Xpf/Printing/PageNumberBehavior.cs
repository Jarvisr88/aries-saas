namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class PageNumberBehavior : Behavior<BarEditItem>
    {
        public static readonly DependencyProperty FocusTargetProperty = DependencyProperty.Register("FocusTarget", typeof(UIElement), typeof(PageNumberBehavior), new PropertyMetadata(null));

        private void FocusEditor()
        {
            if (this.FocusTarget != null)
            {
                this.FocusTarget.Focus();
            }
        }

        private BindingExpression GetTextBindingExpression() => 
            BindingOperations.GetBindingExpression(base.AssociatedObject, BarEditItem.EditValueProperty);

        private void link_LinkControlLoaded(object sender, BarItemLinkControlLoadedEventArgs e)
        {
            BarEditItemLink link = sender as BarEditItemLink;
            if (link != null)
            {
                link.Editor.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditorGotFocus);
                link.Editor.KeyUp += new KeyEventHandler(this.OnKeyUp);
                link.Editor.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditorLostFocus);
            }
        }

        private void Links_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (BarEditItemLink link in e.NewItems)
                {
                    link.LinkControlLoaded -= new BarItemLinkControlLoadedEventHandler(this.link_LinkControlLoaded);
                    link.LinkControlLoaded += new BarItemLinkControlLoadedEventHandler(this.link_LinkControlLoaded);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BarEditItemLink link2 in e.OldItems)
                {
                    link2.LinkControlLoaded -= new BarItemLinkControlLoadedEventHandler(this.link_LinkControlLoaded);
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Links.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Links_CollectionChanged);
            foreach (BarEditItemLink link in base.AssociatedObject.Links)
            {
                link.LinkControlLoaded += new BarItemLinkControlLoadedEventHandler(this.link_LinkControlLoaded);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.Links.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Links_CollectionChanged);
            foreach (BarEditItemLink link in base.AssociatedObject.Links)
            {
                link.LinkControlLoaded -= new BarItemLinkControlLoadedEventHandler(this.link_LinkControlLoaded);
                link.Editor.Do<BaseEdit>(delegate (BaseEdit x) {
                    x.GotKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditorGotFocus);
                    x.KeyUp -= new KeyEventHandler(this.OnKeyUp);
                    x.LostKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditorLostFocus);
                });
            }
        }

        private void OnEditorGotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as BaseEdit).SelectAll();
        }

        private void OnEditorLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.RevertEditValue(base.AssociatedObject);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.FocusEditor();
            }
            else if (e.Key == Key.Return)
            {
                this.GetTextBindingExpression().UpdateSource();
                this.FocusEditor();
            }
        }

        private void RevertEditValue(BarEditItem item)
        {
            item.GetBindingExpression(BarEditItem.EditValueProperty).UpdateTarget();
        }

        public UIElement FocusTarget
        {
            get => 
                (FrameworkElement) base.GetValue(FocusTargetProperty);
            set => 
                base.SetValue(FocusTargetProperty, value);
        }
    }
}

