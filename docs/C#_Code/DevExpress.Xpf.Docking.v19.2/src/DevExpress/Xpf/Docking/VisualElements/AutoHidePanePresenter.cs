namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class AutoHidePanePresenter : psvContentControl
    {
        public static readonly DependencyProperty Win32CompatibleTemplateProperty;
        private static ControlTemplate _DefaultTemplate;
        private const string DefaultTemplateXaml = "<ControlTemplate TargetType='local:AutoHidePanePresenter' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Docking.VisualElements;assembly=DevExpress.Xpf.Docking.v19.2'><ContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}'/></ControlTemplate>";
        private AutoHideWindowHost autoHideWindowHost;

        static AutoHidePanePresenter()
        {
            DependencyPropertyRegistrator<AutoHidePanePresenter> registrator = new DependencyPropertyRegistrator<AutoHidePanePresenter>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<ControlTemplate>("Win32CompatibleTemplate", ref Win32CompatibleTemplateProperty, null, null, null);
        }

        public AutoHidePanePresenter()
        {
            base.DefaultStyleKey = typeof(AutoHidePanePresenter);
        }

        private void DisposeAutoHideWindowHost(IDisposable autoHideWindowHost)
        {
            if (autoHideWindowHost != null)
            {
                base.Dispatcher.BeginInvoke(() => autoHideWindowHost.Dispose(), new object[0]);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DisposeAutoHideWindowHost(this.autoHideWindowHost);
            this.autoHideWindowHost = LayoutItemsHelper.GetTemplateChild<AutoHideWindowHost>(this);
        }

        protected override void OnContentChanged(object content, object oldContent)
        {
            base.OnContentChanged(content, oldContent);
            base.Template = this.SelectTemplate();
        }

        protected override void OnDispose()
        {
            this.DisposeAutoHideWindowHost(this.autoHideWindowHost);
            base.OnDispose();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            base.Template = this.SelectTemplate();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            base.Template = this.SelectTemplate();
        }

        protected override void OnUnloaded()
        {
            base.Template = null;
            this.DisposeAutoHideWindowHost(this.autoHideWindowHost);
            base.OnUnloaded();
        }

        private ControlTemplate SelectTemplate()
        {
            DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(this);
            return (((dockLayoutManager == null) || !dockLayoutManager.EnableWin32Compatibility) ? DefaultTemplate : this.Win32CompatibleTemplate);
        }

        private static ControlTemplate DefaultTemplate =>
            _DefaultTemplate ??= ((ControlTemplate) XamlReader.Parse("<ControlTemplate TargetType='local:AutoHidePanePresenter' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:local='clr-namespace:DevExpress.Xpf.Docking.VisualElements;assembly=DevExpress.Xpf.Docking.v19.2'><ContentPresenter Content='{TemplateBinding Content}' ContentTemplate='{TemplateBinding ContentTemplate}'/></ControlTemplate>"));

        public ControlTemplate Win32CompatibleTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(Win32CompatibleTemplateProperty);
            set => 
                base.SetValue(Win32CompatibleTemplateProperty, value);
        }
    }
}

