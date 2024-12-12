namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class DockBarContainerControl : DockBarContainerControlBase, IDisposable
    {
        public static readonly DependencyProperty BindableNameProperty;
        public static readonly DependencyProperty BindableAllowDropProperty;
        public static readonly DependencyProperty LayoutItemProperty;

        static DockBarContainerControl()
        {
            DependencyPropertyRegistrator<DockBarContainerControl> registrator = new DependencyPropertyRegistrator<DockBarContainerControl>();
            registrator.Register<string>("BindableName", ref BindableNameProperty, null, new PropertyChangedCallback(DockBarContainerControl.OnBindableNameChanged), null);
            bool? defValue = null;
            registrator.Register<bool?>("BindableAllowDrop", ref BindableAllowDropProperty, defValue, new PropertyChangedCallback(DockBarContainerControl.OnBindableAllowDropChanged), null);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, e) => ((DockBarContainerControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue), null);
        }

        public DockBarContainerControl()
        {
            base.AllowDrop = CalcAllowDrop(this.BindableAllowDrop);
            base.Focusable = false;
        }

        private static bool CalcAllowDrop(bool? value) => 
            (value == null) ? false : value.Value;

        public void Dispose()
        {
            if (!this.IsDisposing)
            {
                this.IsDisposing = true;
                this.OnDispose();
                this.Container = null;
            }
            GC.SuppressFinalize(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container = DockLayoutManager.FindManager(this);
        }

        private static void OnBindableAllowDropChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement) dObj).AllowDrop = CalcAllowDrop((bool?) e.NewValue);
        }

        private static void OnBindableNameChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement) dObj).Name = (string) e.NewValue;
        }

        protected virtual void OnDispose()
        {
            BarManagerPropertyHelper.ClearBarManager(this);
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem item)
        {
            if (item != null)
            {
                BindingHelper.SetBinding(this, BindableAllowDropProperty, item, "HeaderBarContainerControlAllowDrop");
                BindingHelper.SetBinding(this, BindableNameProperty, item, "HeaderBarContainerControlName");
            }
            else
            {
                BindingHelper.ClearBinding(this, BindableAllowDropProperty);
                BindingHelper.ClearBinding(this, BindableNameProperty);
            }
        }

        protected override void OnLoaded()
        {
        }

        protected override void OnUnloaded()
        {
            BarManagerPropertyHelper.ClearBarManager(this);
        }

        protected bool IsDisposing { get; private set; }

        public string BindableName
        {
            get => 
                (string) base.GetValue(BindableNameProperty);
            set => 
                base.SetValue(BindableNameProperty, value);
        }

        public bool? BindableAllowDrop
        {
            get => 
                (bool?) base.GetValue(BindableAllowDropProperty);
            set => 
                base.SetValue(BindableAllowDropProperty, value);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public DockLayoutManager Container { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockBarContainerControl.<>c <>9 = new DockBarContainerControl.<>c();

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockBarContainerControl) dObj).OnLayoutItemChanged((BaseLayoutItem) e.NewValue);
            }
        }
    }
}

