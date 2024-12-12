namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Docking.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(DevExpress.Xpf.Docking.LayoutGroup)), TargetType(false, typeof(DocumentGroup))]
    public class DockingDocumentUIService : DockingDocumentUIServiceBase<LayoutPanel, DevExpress.Xpf.Docking.LayoutGroup>
    {
        public static readonly DependencyProperty LayoutPanelStyleProperty = DependencyProperty.Register("LayoutPanelStyle", typeof(Style), typeof(DockingDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty LayoutPanelStyleSelectorProperty = DependencyProperty.Register("LayoutPanelStyleSelector", typeof(StyleSelector), typeof(DockingDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty LayoutGroupProperty = DependencyProperty.Register("LayoutGroup", typeof(DevExpress.Xpf.Docking.LayoutGroup), typeof(DockingDocumentUIService), new PropertyMetadata(null));

        protected override LayoutPanel CreateDocumentPanel() => 
            new LayoutPanel();

        protected override DevExpress.Xpf.Docking.LayoutGroup GetActualDocumentGroup() => 
            this.ActualLayoutGroup;

        protected override Style GetDocumentPanelStyle(LayoutPanel documentPanel, object documentContentView) => 
            base.GetDocumentContainerStyle(documentPanel, documentContentView, this.LayoutPanelStyle, this.LayoutPanelStyleSelector);

        public DevExpress.Xpf.Docking.LayoutGroup LayoutGroup
        {
            get => 
                (DevExpress.Xpf.Docking.LayoutGroup) base.GetValue(LayoutGroupProperty);
            set => 
                base.SetValue(LayoutGroupProperty, value);
        }

        public StyleSelector LayoutPanelStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(LayoutPanelStyleSelectorProperty);
            set => 
                base.SetValue(LayoutPanelStyleSelectorProperty, value);
        }

        public Style LayoutPanelStyle
        {
            get => 
                (Style) base.GetValue(LayoutPanelStyleProperty);
            set => 
                base.SetValue(LayoutPanelStyleProperty, value);
        }

        public DevExpress.Xpf.Docking.LayoutGroup ActualLayoutGroup =>
            (base.AssociatedObject as DevExpress.Xpf.Docking.LayoutGroup) ?? this.LayoutGroup;
    }
}

