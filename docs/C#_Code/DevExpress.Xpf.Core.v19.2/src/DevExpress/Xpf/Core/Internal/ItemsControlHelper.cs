namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class ItemsControlHelper
    {
        public static readonly DependencyProperty ScrollUnitProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsPixelBasedProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty scrollUnitPropertyValue;
        private static readonly Type scrollType;
        private static readonly Action<object, bool> set_IsPixelBased;
        private static readonly Func<ItemsControl, Panel> get_itemsHost;

        static ItemsControlHelper()
        {
            ScrollUnitProperty = DependencyPropertyManager.RegisterAttached("ScrollUnit", typeof(DevExpress.Xpf.Editors.ScrollUnit), typeof(ItemsControlHelper), new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ScrollUnit.Item, FrameworkPropertyMetadataOptions.None, (o, args) => OnScrollUnit2Changed(o, (DevExpress.Xpf.Editors.ScrollUnit) args.NewValue)));
            IsPixelBasedProperty = DependencyPropertyManager.RegisterAttached("IsPixelBased", typeof(bool), typeof(ItemsControlHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, (o, args) => OnIsPixelBasedChanged(o, (bool) args.NewValue)));
            get_itemsHost = ReflectionHelper.CreateFieldGetter<ItemsControl, Panel>(typeof(ItemsControl), "_itemsHost", BindingFlags.NonPublic | BindingFlags.Instance);
            if (Net45Detector.IsNet45())
            {
                scrollUnitPropertyValue = (DependencyProperty) typeof(VirtualizingPanel).GetField("ScrollUnitProperty").GetValue(null);
                scrollType = typeof(VirtualizingPanel).Assembly.GetType("System.Windows.Controls.ScrollUnit");
            }
            else
            {
                int? parametersCount = null;
                set_IsPixelBased = ReflectionHelper.CreateInstanceMethodHandler<Action<object, bool>>(null, "set_IsPixelBased", BindingFlags.NonPublic | BindingFlags.Instance, typeof(VirtualizingStackPanel), parametersCount, null, true);
            }
        }

        private static bool GetIsPixelBased(DependencyObject obj) => 
            (bool) obj.GetValue(IsPixelBasedProperty);

        public static Panel GetItemsHost(ItemsControl owner) => 
            (owner != null) ? get_itemsHost(owner) : null;

        public static DevExpress.Xpf.Editors.ScrollUnit GetScrollUnit(DependencyObject obj) => 
            (DevExpress.Xpf.Editors.ScrollUnit) obj.GetValue(ScrollUnitProperty);

        private static void OnIsPixelBasedChanged(DependencyObject obj, bool newValue)
        {
            if (obj is VirtualizingStackPanel)
            {
                set_IsPixelBased(obj, newValue);
            }
        }

        private static void OnScrollUnit2Changed(DependencyObject obj, DevExpress.Xpf.Editors.ScrollUnit newValue)
        {
            if (Net45Detector.IsNet45())
            {
                obj.SetValue(scrollUnitPropertyValue, Enum.Parse(scrollType, newValue.ToString()));
            }
            else
            {
                SetIsPixelBased(obj, newValue == DevExpress.Xpf.Editors.ScrollUnit.Pixel);
            }
        }

        private static void SetIsPixelBased(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPixelBasedProperty, value);
        }

        public static void SetScrollUnit(DependencyObject obj, DevExpress.Xpf.Editors.ScrollUnit value)
        {
            obj.SetValue(ScrollUnitProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsControlHelper.<>c <>9 = new ItemsControlHelper.<>c();

            internal void <.cctor>b__10_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ItemsControlHelper.OnScrollUnit2Changed(o, (DevExpress.Xpf.Editors.ScrollUnit) args.NewValue);
            }

            internal void <.cctor>b__10_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ItemsControlHelper.OnIsPixelBasedChanged(o, (bool) args.NewValue);
            }
        }
    }
}

