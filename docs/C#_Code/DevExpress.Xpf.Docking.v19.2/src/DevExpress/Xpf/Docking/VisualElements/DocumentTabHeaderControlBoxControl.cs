namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_PinButton", Type=typeof(ControlBoxButtonPresenter))]
    public class DocumentTabHeaderControlBoxControl : BaseControlBoxControl
    {
        public static readonly DependencyProperty PinButtonTemplateProperty;

        static DocumentTabHeaderControlBoxControl()
        {
            DependencyPropertyRegistrator<DocumentTabHeaderControlBoxControl> registrator = new DependencyPropertyRegistrator<DocumentTabHeaderControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("PinButtonTemplate", ref PinButtonTemplateProperty, null, null, null);
        }

        protected override void ClearControlBoxBindings()
        {
            if (this.PartPinButton != null)
            {
                this.PartPinButton.ClearValue(UIElement.VisibilityProperty);
            }
            base.ClearControlBoxBindings();
        }

        protected override void EnsureTemplateChildren()
        {
            base.EnsureTemplateChildren();
            this.PartPinButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartPinButton, "PART_PinButton");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.PartPinButton != null)
            {
                this.PartPinButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonTogglePinStatus));
            }
        }

        protected override void OnDispose()
        {
            if (this.PartPinButton != null)
            {
                this.PartPinButton.Dispose();
                this.PartPinButton = null;
            }
            base.OnDispose();
        }

        protected override void SetControlBoxBindings()
        {
            base.SetControlBoxBindings();
            if (this.PartPinButton != null)
            {
                BindingHelper.SetBinding(this.PartPinButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsPinButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
        }

        public ControlBoxButtonPresenter PartPinButton { get; private set; }

        public DataTemplate PinButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PinButtonTemplateProperty);
            set => 
                base.SetValue(PinButtonTemplateProperty, value);
        }
    }
}

