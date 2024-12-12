namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Docking.Native;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window)), TargetType(typeof(DevExpress.Xpf.Docking.DocumentGroup)), TargetType(typeof(DockLayoutManager))]
    public class TabbedDocumentUIService : DockingDocumentUIServiceBase<DocumentPanel, DevExpress.Xpf.Docking.DocumentGroup>
    {
        public static readonly DependencyProperty DocumentPanelStyleProperty = DependencyProperty.Register("DocumentPanelStyle", typeof(Style), typeof(TabbedDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty UseActiveDocumentGroupAsDocumentHostProperty = DependencyProperty.Register("UseActiveDocumentGroupAsDocumentHost", typeof(bool), typeof(TabbedDocumentUIService), new PropertyMetadata(true));
        public static readonly DependencyProperty DocumentPanelStyleSelectorProperty = DependencyProperty.Register("DocumentPanelStyleSelector", typeof(StyleSelector), typeof(TabbedDocumentUIService), new PropertyMetadata(null));
        public static readonly DependencyProperty DocumentGroupProperty;

        static TabbedDocumentUIService()
        {
            DocumentGroupProperty = DependencyProperty.Register("DocumentGroup", typeof(DevExpress.Xpf.Docking.DocumentGroup), typeof(TabbedDocumentUIService), new PropertyMetadata(null, (d, e) => ((TabbedDocumentUIService) d).OnDocumentGroupChanged((DevExpress.Xpf.Docking.DocumentGroup) e.OldValue, (DevExpress.Xpf.Docking.DocumentGroup) e.NewValue)));
        }

        protected override DocumentPanel CreateDocumentPanel() => 
            new DocumentPanel();

        protected override DevExpress.Xpf.Docking.DocumentGroup GetActualDocumentGroup() => 
            this.ActualDocumentGroup;

        protected override Style GetDocumentPanelStyle(DocumentPanel documentPanel, object documentContentView) => 
            base.GetDocumentContainerStyle(documentPanel, documentContentView, this.DocumentPanelStyle, this.DocumentPanelStyleSelector);

        protected override void OnAttached()
        {
            base.OnAttached();
            Action<DevExpress.Xpf.Docking.DocumentGroup> action = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Action<DevExpress.Xpf.Docking.DocumentGroup> local1 = <>c.<>9__18_0;
                action = <>c.<>9__18_0 = x => x.DestroyOnClosingChildren = false;
            }
            (base.AssociatedObject as DevExpress.Xpf.Docking.DocumentGroup).Do<DevExpress.Xpf.Docking.DocumentGroup>(action);
        }

        protected virtual void OnDocumentGroupChanged(DevExpress.Xpf.Docking.DocumentGroup oldValue, DevExpress.Xpf.Docking.DocumentGroup newValue)
        {
            Action<DevExpress.Xpf.Docking.DocumentGroup> action = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Action<DevExpress.Xpf.Docking.DocumentGroup> local1 = <>c.<>9__19_0;
                action = <>c.<>9__19_0 = x => x.DestroyOnClosingChildren = false;
            }
            newValue.Do<DevExpress.Xpf.Docking.DocumentGroup>(action);
        }

        public DevExpress.Xpf.Docking.DocumentGroup DocumentGroup
        {
            get => 
                (DevExpress.Xpf.Docking.DocumentGroup) base.GetValue(DocumentGroupProperty);
            set => 
                base.SetValue(DocumentGroupProperty, value);
        }

        public StyleSelector DocumentPanelStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(DocumentPanelStyleSelectorProperty);
            set => 
                base.SetValue(DocumentPanelStyleSelectorProperty, value);
        }

        public Style DocumentPanelStyle
        {
            get => 
                (Style) base.GetValue(DocumentPanelStyleProperty);
            set => 
                base.SetValue(DocumentPanelStyleProperty, value);
        }

        public bool UseActiveDocumentGroupAsDocumentHost
        {
            get => 
                (bool) base.GetValue(UseActiveDocumentGroupAsDocumentHostProperty);
            set => 
                base.SetValue(UseActiveDocumentGroupAsDocumentHostProperty, value);
        }

        public DevExpress.Xpf.Docking.DocumentGroup ActualDocumentGroup
        {
            get
            {
                if (this.UseActiveDocumentGroupAsDocumentHost && (base.ActiveDocument != null))
                {
                    DevExpress.Xpf.Docking.DocumentGroup parent = ((DockingDocumentUIServiceBase<DocumentPanel, DevExpress.Xpf.Docking.DocumentGroup>.Document) base.ActiveDocument).DocumentPanel.Parent as DevExpress.Xpf.Docking.DocumentGroup;
                    if ((parent != null) && (parent.ItemType != LayoutItemType.FloatGroup))
                    {
                        return parent;
                    }
                }
                return ((this.DocumentGroup == null) ? (base.AssociatedObject as DevExpress.Xpf.Docking.DocumentGroup) : this.DocumentGroup);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabbedDocumentUIService.<>c <>9 = new TabbedDocumentUIService.<>c();
            public static Action<DocumentGroup> <>9__18_0;
            public static Action<DocumentGroup> <>9__19_0;

            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabbedDocumentUIService) d).OnDocumentGroupChanged((DocumentGroup) e.OldValue, (DocumentGroup) e.NewValue);
            }

            internal void <OnAttached>b__18_0(DocumentGroup x)
            {
                x.DestroyOnClosingChildren = false;
            }

            internal void <OnDocumentGroupChanged>b__19_0(DocumentGroup x)
            {
                x.DestroyOnClosingChildren = false;
            }
        }
    }
}

