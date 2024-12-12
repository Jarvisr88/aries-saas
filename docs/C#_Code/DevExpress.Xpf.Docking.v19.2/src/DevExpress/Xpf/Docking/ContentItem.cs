namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public abstract class ContentItem : BaseLayoutItem, IItemContainer
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty IsDataBoundProperty;
        internal static readonly DependencyPropertyKey IsDataBoundPropertyKey;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty FocusContentOnActivatingProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;
        public static readonly DependencyProperty ActivateOnFocusingProperty;
        private BindingExpressionBase DataContextBinding;

        static ContentItem()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<ContentItem> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<ContentItem>();
            registrator.Register<object>("Content", ref ContentProperty, null, (dObj, e) => ((ContentItem) dObj).OnContentChanged(e.NewValue, e.OldValue), null);
            registrator.RegisterReadonly<bool>("IsDataBound", ref IsDataBoundPropertyKey, ref IsDataBoundProperty, false, (dObj, e) => ((ContentItem) dObj).OnIsDataBoundChanged((bool) e.NewValue), null);
            registrator.Register<DataTemplate>("ContentTemplate", ref ContentTemplateProperty, null, null, null);
            registrator.Register<DataTemplateSelector>("ContentTemplateSelector", ref ContentTemplateSelectorProperty, null, null, null);
            registrator.Register<bool>("FocusContentOnActivating", ref FocusContentOnActivatingProperty, true, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ContentItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<ContentItem>.New().Register<DevExpress.Xpf.Docking.ActivateOnFocusing>(System.Linq.Expressions.Expression.Lambda<Func<ContentItem, DevExpress.Xpf.Docking.ActivateOnFocusing>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ContentItem.get_ActivateOnFocusing)), parameters), out ActivateOnFocusingProperty, DevExpress.Xpf.Docking.ActivateOnFocusing.Keyboard, frameworkOptions);
        }

        public ContentItem()
        {
            base.GotKeyboardFocus += (s, e) => this.OnGotKeyboardOrLogicalFocus(DevExpress.Xpf.Docking.ActivateOnFocusing.Keyboard, e);
            base.GotFocus += (s, e) => this.OnGotKeyboardOrLogicalFocus(DevExpress.Xpf.Docking.ActivateOnFocusing.Logical, e);
        }

        internal virtual void Control_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        void IItemContainer.ClearContainer(object item)
        {
            if (DockLayoutManagerParameters.DisposePanelContentAfterRemovingPanel)
            {
                DisposeHelper.DisposeVisualTree(this.Content as DependencyObject);
            }
            base.ClearValue(ContentProperty);
        }

        void IItemContainer.PrepareContainer(object item)
        {
            if (!ReferenceEquals(this, item))
            {
                this.Content = item;
                if (item is FrameworkElement)
                {
                    base.DataContext = ((FrameworkElement) item).DataContext;
                }
            }
        }

        protected virtual DependencyObject GetDataBoundContainer()
        {
            ContentPresenter target = new ContentPresenter();
            BindingHelper.SetBinding(target, ContentPresenter.ContentProperty, this, ContentProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, ContentPresenter.ContentTemplateProperty, this, ContentTemplateProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, ContentPresenter.ContentTemplateSelectorProperty, this, ContentTemplateSelectorProperty, BindingMode.OneWay);
            return target;
        }

        protected virtual void OnContentChanged(object content, object oldContent)
        {
        }

        private void OnGotKeyboardOrLogicalFocus(DevExpress.Xpf.Docking.ActivateOnFocusing reason, RoutedEventArgs e)
        {
            if (reason == this.ActivateOnFocusing)
            {
                this.Control_GotFocus(this, e);
            }
        }

        protected virtual void OnIsDataBoundChanged(bool value)
        {
            if (base.GetBindingExpression(ContentProperty) == null)
            {
                BindingExpression bindingExpression = base.GetBindingExpression(FrameworkElement.DataContextProperty);
                if (!this.IsPropertySet(FrameworkElement.DataContextProperty) || ((bindingExpression != null) && ReferenceEquals(bindingExpression, this.DataContextBinding)))
                {
                    if (value)
                    {
                        this.DataContextBinding = BindingHelper.SetBinding(this, FrameworkElement.DataContextProperty, this, "Content");
                    }
                    else
                    {
                        BindingHelper.ClearBinding(this, FrameworkElement.DataContextProperty);
                    }
                }
            }
        }

        [Description("Gets or sets whether the content item is activated on getting logical or keyboard focus. This is a dependency property."), Category("Behavior")]
        public DevExpress.Xpf.Docking.ActivateOnFocusing ActivateOnFocusing
        {
            get => 
                (DevExpress.Xpf.Docking.ActivateOnFocusing) base.GetValue(ActivateOnFocusingProperty);
            set => 
                base.SetValue(ActivateOnFocusingProperty, value);
        }

        [Description("Gets or sets the item's content. This is a dependency property."), Category("Content")]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Description("Gets or sets the template used to display the item's ContentItem.Content. This is a dependency property.")]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses the item's ContentItem.ContentTemplate based on custom logic.")]
        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }

        [Description("Gets or sets whether the ContentItem's content should be focused on ContentItem activation."), Category("Behavior")]
        public bool FocusContentOnActivating
        {
            get => 
                (bool) base.GetValue(FocusContentOnActivatingProperty);
            set => 
                base.SetValue(FocusContentOnActivatingProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDataBound
        {
            get => 
                (bool) base.GetValue(IsDataBoundProperty);
            private set => 
                base.SetValue(IsDataBoundPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentItem.<>c <>9 = new ContentItem.<>c();

            internal void <.cctor>b__7_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ContentItem) dObj).OnContentChanged(e.NewValue, e.OldValue);
            }

            internal void <.cctor>b__7_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((ContentItem) dObj).OnIsDataBoundChanged((bool) e.NewValue);
            }
        }
    }
}

