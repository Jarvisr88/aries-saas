namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_MinimizeButton", Type=typeof(ControlBoxButtonPresenter)), TemplatePart(Name="PART_MaximizeButton", Type=typeof(ControlBoxButtonPresenter)), TemplatePart(Name="PART_RestoreButton", Type=typeof(ControlBoxButtonPresenter))]
    public class WindowControlBoxControl : BaseControlBoxControl
    {
        public static readonly DependencyProperty MinimizeButtonTemplateProperty;
        public static readonly DependencyProperty MaximizeButtonTemplateProperty;
        public static readonly DependencyProperty RestoreButtonTemplateProperty;

        static WindowControlBoxControl()
        {
            DependencyPropertyRegistrator<WindowControlBoxControl> registrator = new DependencyPropertyRegistrator<WindowControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("MinimizeButtonTemplate", ref MinimizeButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("MaximizeButtonTemplate", ref MaximizeButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("RestoreButtonTemplate", ref RestoreButtonTemplateProperty, null, null, null);
        }

        protected override void ClearControlBoxBindings()
        {
            if (this.PartMinimizeButton != null)
            {
                this.PartMinimizeButton.ClearValue(UIElement.VisibilityProperty);
            }
            if (this.PartMaximizeButton != null)
            {
                this.PartMaximizeButton.ClearValue(UIElement.VisibilityProperty);
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.ClearValue(UIElement.VisibilityProperty);
            }
            base.ClearControlBoxBindings();
        }

        protected override void EnsureTemplateChildren()
        {
            base.EnsureTemplateChildren();
            this.PartMinimizeButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartMinimizeButton, "PART_MinimizeButton");
            this.PartMaximizeButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartMaximizeButton, "PART_MaximizeButton");
            this.PartRestoreButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartRestoreButton, "PART_RestoreButton");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.PartMaximizeButton != null)
            {
                this.PartMaximizeButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonMaximize));
            }
            if (this.PartMinimizeButton != null)
            {
                this.PartMinimizeButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonMinimize));
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonRestore));
            }
        }

        protected override void OnDispose()
        {
            if (this.PartMinimizeButton != null)
            {
                this.PartMinimizeButton.Dispose();
                this.PartMinimizeButton = null;
            }
            if (this.PartMaximizeButton != null)
            {
                this.PartMaximizeButton.Dispose();
                this.PartMaximizeButton = null;
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.Dispose();
                this.PartRestoreButton = null;
            }
            base.OnDispose();
        }

        protected override void SetControlBoxBindings()
        {
            base.SetControlBoxBindings();
            if (this.PartMinimizeButton != null)
            {
                BindingHelper.SetBinding(this.PartMinimizeButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsMinimizeButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
            if (this.PartMaximizeButton != null)
            {
                BindingHelper.SetBinding(this.PartMaximizeButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsMaximizeButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
            if (this.PartRestoreButton != null)
            {
                BindingHelper.SetBinding(this.PartRestoreButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsRestoreButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
        }

        public DataTemplate MaximizeButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MaximizeButtonTemplateProperty);
            set => 
                base.SetValue(MaximizeButtonTemplateProperty, value);
        }

        public DataTemplate MinimizeButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MinimizeButtonTemplateProperty);
            set => 
                base.SetValue(MinimizeButtonTemplateProperty, value);
        }

        public ControlBoxButtonPresenter PartMaximizeButton { get; private set; }

        public ControlBoxButtonPresenter PartMinimizeButton { get; private set; }

        public ControlBoxButtonPresenter PartRestoreButton { get; private set; }

        public DataTemplate RestoreButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(RestoreButtonTemplateProperty);
            set => 
                base.SetValue(RestoreButtonTemplateProperty, value);
        }
    }
}

