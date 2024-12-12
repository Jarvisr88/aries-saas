namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_ItemsControl", Type=typeof(psvItemsControl))]
    public class DocumentPaneContentPresenter : BasePanePresenter<DocumentPane, DocumentGroup>
    {
        public static readonly DependencyProperty MDIStyleProperty;

        static DocumentPaneContentPresenter()
        {
            new DependencyPropertyRegistrator<DocumentPaneContentPresenter>().Register<DevExpress.Xpf.Docking.MDIStyle>("MDIStyle", ref MDIStyleProperty, DevExpress.Xpf.Docking.MDIStyle.Default, (dObj, e) => ((DocumentPaneContentPresenter) dObj).OnStylePropertyChanged(), null);
        }

        protected override bool CanSelectTemplate(DocumentGroup group) => 
            group.ActualGroupTemplateSelector != null;

        protected override DocumentGroup ConvertToLogicalItem(object content) => 
            (LayoutItemData.ConvertToBaseLayoutItem(content) as DocumentGroup) ?? base.ConvertToLogicalItem(content);

        public override void OnApplyTemplate()
        {
            if ((this.PartItemsContainer != null) && !LayoutItemsHelper.IsTemplateChild<DocumentPaneContentPresenter>(this.PartItemsContainer, this))
            {
                this.PartItemsContainer.Dispose();
            }
            base.OnApplyTemplate();
            this.PartItemsContainer = base.GetTemplateChild("PART_ItemsControl") as psvItemsControl;
        }

        protected override void OnDispose()
        {
            if (this.PartItemsContainer != null)
            {
                this.PartItemsContainer.Dispose();
                this.PartItemsContainer = null;
            }
            base.OnDispose();
        }

        protected override void OnStylePropertyChanged()
        {
            MDIControllerHelper.UnMergeMDITitles(this);
            BaseLayoutItem[] items = new BaseLayoutItem[] { this.ConvertToLogicalItem(base.Content) };
            using (new LogicalTreeLocker(DockLayoutManager.GetDockLayoutManager(this), items))
            {
                base.OnStylePropertyChanged();
            }
            MDIControllerHelper.UnMergeMDIMenuItems(this);
        }

        protected override DataTemplate SelectTemplateCore(DocumentGroup group) => 
            group.ActualGroupTemplateSelector.SelectTemplate(group, this);

        public DevExpress.Xpf.Docking.MDIStyle MDIStyle
        {
            get => 
                (DevExpress.Xpf.Docking.MDIStyle) base.GetValue(MDIStyleProperty);
            set => 
                base.SetValue(MDIStyleProperty, value);
        }

        public psvItemsControl PartItemsContainer { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPaneContentPresenter.<>c <>9 = new DocumentPaneContentPresenter.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentPaneContentPresenter) dObj).OnStylePropertyChanged();
            }
        }
    }
}

