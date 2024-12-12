namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [TemplatePart(Name="PART_Caption", Type=typeof(CaptionControl)), TemplatePart(Name="PART_CaptionBackground", Type=typeof(Border)), TemplatePart(Name="PART_ControlBox", Type=typeof(BaseControlBoxControl))]
    public class DockPaneHeaderPresenter : BasePanePresenter<DockPane, LayoutPanel>
    {
        public static readonly DependencyProperty IsCaptionVisibleProperty;
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty ForegroundProperty;
        private DataTemplateSelector defaultCaptionTemplateSelector;

        static DockPaneHeaderPresenter()
        {
            DependencyPropertyRegistrator<DockPaneHeaderPresenter> registrator = new DependencyPropertyRegistrator<DockPaneHeaderPresenter>();
            registrator.Register<bool>("IsCaptionVisible", ref IsCaptionVisibleProperty, true, (dObj, e) => ((DockPaneHeaderPresenter) dObj).OnIsCaptionVisibleChanged((bool) e.NewValue), null);
            registrator.Register<Brush>("Background", ref BackgroundProperty, null, null, null);
            registrator.Register<Brush>("Foreground", ref ForegroundProperty, null, null, null);
        }

        protected override bool CanSelectTemplate(LayoutPanel panel) => 
            this._DefaultCaptionTemplateSelector != null;

        protected override LayoutPanel ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as LayoutPanel) ?? base.ConvertToLogicalItem(content);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartCaptionPresenter = base.GetTemplateChild("PART_CaptionControlPresenter") as TemplatedCaptionControl;
            if (this.PartCaptionPresenter != null)
            {
                this.Forward(this.PartCaptionPresenter, Control.ForegroundProperty, "Foreground", BindingMode.OneWay);
            }
            this.PartCaptionBackground = base.GetTemplateChild("PART_CaptionBackground") as Border;
            if (this.PartCaptionBackground != null)
            {
                this.Forward(this.PartCaptionBackground, Border.BackgroundProperty, "Background", BindingMode.OneWay);
            }
            if ((this.PartControlBox != null) && !LayoutItemsHelper.IsTemplateChild<DockPaneHeaderPresenter>(this.PartControlBox, this))
            {
                this.PartControlBox.Dispose();
            }
            this.PartControlBox = base.GetTemplateChild("PART_ControlBox") as BaseControlBoxControl;
        }

        protected override void OnDispose()
        {
            BindingHelper.ClearBinding(this, IsCaptionVisibleProperty);
            BindingHelper.ClearBinding(this, BackgroundProperty);
            BindingHelper.ClearBinding(this, ForegroundProperty);
            if (this.PartCaptionPresenter != null)
            {
                this.PartCaptionPresenter.Dispose();
                this.PartCaptionPresenter = null;
            }
            if (this.PartControlBox != null)
            {
                this.PartControlBox.Dispose();
                this.PartControlBox = null;
            }
            base.OnDispose();
        }

        protected virtual void OnIsCaptionVisibleChanged(bool visible)
        {
            base.Visibility = VisibilityHelper.Convert(visible, Visibility.Collapsed);
        }

        protected override DataTemplate SelectTemplateCore(LayoutPanel panel) => 
            this._DefaultCaptionTemplateSelector.SelectTemplate(panel, this);

        private DataTemplateSelector _DefaultCaptionTemplateSelector
        {
            get
            {
                this.defaultCaptionTemplateSelector ??= new DefaultHeaderTemplateSelector();
                return this.defaultCaptionTemplateSelector;
            }
        }

        public bool IsCaptionVisible
        {
            get => 
                (bool) base.GetValue(IsCaptionVisibleProperty);
            set => 
                base.SetValue(IsCaptionVisibleProperty, value);
        }

        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        protected TemplatedCaptionControl PartCaptionPresenter { get; private set; }

        protected Border PartCaptionBackground { get; private set; }

        protected BaseControlBoxControl PartControlBox { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockPaneHeaderPresenter.<>c <>9 = new DockPaneHeaderPresenter.<>c();

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DockPaneHeaderPresenter) dObj).OnIsCaptionVisibleChanged((bool) e.NewValue);
            }
        }

        private class DefaultHeaderTemplateSelector : DefaultItemTemplateSelectorWrapper.DefaultItemTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                DockPaneHeaderPresenter presenter = container as DockPaneHeaderPresenter;
                return (((presenter == null) || (presenter.Owner == null)) ? null : presenter.Owner.HeaderTemplate);
            }
        }
    }
}

