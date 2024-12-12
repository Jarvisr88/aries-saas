namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class ThemedWindowHeaderItemsControlBase : ItemsControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IWindowServiceProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty AllowHeaderItemsProperty;

        static ThemedWindowHeaderItemsControlBase()
        {
            object obj1;
            FieldInfo field = typeof(System.Windows.Window).GetField("IWindowServiceProperty", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (field != null)
            {
                obj1 = field.GetValue(null);
            }
            else
            {
                FieldInfo local1 = field;
                obj1 = null;
            }
            IWindowServiceProperty = obj1 as DependencyProperty;
            AllowHeaderItemsProperty = DependencyProperty.RegisterAttached("AllowHeaderItems", typeof(bool), typeof(ThemedWindowHeaderItemsControlBase), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
            if (IWindowServiceProperty == null)
            {
                DependencyProperty iWindowServiceProperty = IWindowServiceProperty;
            }
            else
            {
                IWindowServiceProperty.OverrideMetadata(typeof(ThemedWindowHeaderItemsControlBase), new FrameworkPropertyMetadata((d, e) => ((ThemedWindowHeaderItemsControlBase) d).OnWindowServiceChanged(e.OldValue, e.NewValue)));
            }
        }

        protected ThemedWindowHeaderItemsControlBase()
        {
        }

        public static bool GetAllowHeaderItems(DependencyObject obj) => 
            (bool) obj.GetValue(AllowHeaderItemsProperty);

        protected override DependencyObject GetContainerForItemOverride() => 
            new HeaderItemControl();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SetHeaderItemsProperty();
        }

        protected virtual void OnWindowServiceChanged(object oldValue, object newValue)
        {
            this.SetHeaderItemsProperty();
        }

        public static void SetAllowHeaderItems(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowHeaderItemsProperty, value);
        }

        protected virtual void SetHeaderItemsProperty()
        {
            if (this.Window == null)
            {
                base.ClearValue(ItemsControl.ItemContainerStyleProperty);
                base.ClearValue(ItemsControl.ItemContainerStyleSelectorProperty);
                base.ClearValue(ItemsControl.ItemTemplateProperty);
                base.ClearValue(ItemsControl.ItemTemplateSelectorProperty);
                base.ClearValue(ItemsControl.ItemsSourceProperty);
            }
            else
            {
                Binding binding = new Binding {
                    Source = this,
                    Path = new PropertyPath("HasItems", new object[0]),
                    Converter = new BooleanToVisibilityConverter()
                };
                base.SetBinding(UIElement.VisibilityProperty, binding);
            }
        }

        protected IHeaderItemsControlHost Window =>
            base.GetValue(IWindowServiceProperty) as IHeaderItemsControlHost;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowHeaderItemsControlBase.<>c <>9 = new ThemedWindowHeaderItemsControlBase.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ThemedWindowHeaderItemsControlBase) d).OnWindowServiceChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

