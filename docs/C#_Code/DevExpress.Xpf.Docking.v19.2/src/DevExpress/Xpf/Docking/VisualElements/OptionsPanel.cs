namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class OptionsPanel : psvControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty AllowInvisbleItemsInternalProperty;

        static OptionsPanel()
        {
            DependencyPropertyRegistrator<OptionsPanel> registrator = new DependencyPropertyRegistrator<OptionsPanel>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<bool>("AllowInvisbleItemsInternal", ref AllowInvisbleItemsInternalProperty, true, (dObj, ea) => ((OptionsPanel) dObj).OnAllowInvisibleItemsInternalChanged((bool) ea.NewValue), null);
        }

        public void OnAllowInvisibleItemsInternalChanged(bool value)
        {
            if (base.Container != null)
            {
                this.PartCheckShowAll.Visibility = VisibilityHelper.Convert(value, Visibility.Collapsed);
                if (value)
                {
                    BindingHelper.SetBinding(this.PartCheckShowAll, CheckEdit.IsCheckedProperty, base.Container, DockLayoutManager.ShowInvisibleItemsProperty, BindingMode.TwoWay);
                }
                else
                {
                    BindingHelper.ClearBinding(this.PartCheckShowAll, CheckEdit.IsCheckedProperty);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartCheckShowAll = base.GetTemplateChild("PART_CheckShowAll") as CheckEdit;
            if (this.PartCheckShowAll != null)
            {
                this.PartCheckShowAll.Content = DockingLocalizer.GetString(DockingStringId.CheckBoxShowInvisibleItems);
                if (base.Container != null)
                {
                    BindingHelper.SetBinding(this.PartCheckShowAll, CheckEdit.IsCheckedProperty, base.Container, DockLayoutManager.ShowInvisibleItemsProperty, BindingMode.TwoWay);
                    BindingHelper.SetBinding(this, AllowInvisbleItemsInternalProperty, base.Container, "ShowInvisibleItemsInCustomizationForm");
                }
            }
            base.Focusable = false;
        }

        protected override void OnDispose()
        {
            BindingHelper.ClearBinding(this, AllowInvisbleItemsInternalProperty);
            if (this.PartCheckShowAll != null)
            {
                BindingHelper.ClearBinding(this.PartCheckShowAll, CheckEdit.IsCheckedProperty);
            }
            base.OnDispose();
        }

        protected CheckEdit PartCheckShowAll { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OptionsPanel.<>c <>9 = new OptionsPanel.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((OptionsPanel) dObj).OnAllowInvisibleItemsInternalChanged((bool) ea.NewValue);
            }
        }
    }
}

