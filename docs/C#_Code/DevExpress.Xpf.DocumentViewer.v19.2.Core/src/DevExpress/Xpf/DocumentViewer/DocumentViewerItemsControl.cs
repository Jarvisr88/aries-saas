namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class DocumentViewerItemsControl : ItemsControl
    {
        public static readonly DependencyProperty PageDisplayModeProperty;

        static DocumentViewerItemsControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerItemsControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            PageDisplayModeProperty = DependencyPropertyRegistrator.Register<DocumentViewerItemsControl, DevExpress.Xpf.DocumentViewer.PageDisplayMode>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerItemsControl, DevExpress.Xpf.DocumentViewer.PageDisplayMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerItemsControl.get_PageDisplayMode)), parameters), DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single, (owner, oldValue, newValue) => owner.OnPageDisplayModeChanged(newValue));
        }

        public DocumentViewerItemsControl()
        {
            base.DefaultStyleKey = typeof(DocumentViewerItemsControl);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PageControl();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PageControl;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PresenterDecorator = (DocumentPresenterDecorator) base.GetTemplateChild("PART_Decorator");
            this.ItemsPresenter = (System.Windows.Controls.ItemsPresenter) base.GetTemplateChild("PART_ItemsPresenter");
            this.ScrollViewer = (System.Windows.Controls.ScrollViewer) base.GetTemplateChild("PART_ScrollViewer");
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Panel = VisualTreeHelper.GetChild(this.ItemsPresenter, 0) as DocumentViewerPanel;
        }

        private void OnPageDisplayModeChanged(DevExpress.Xpf.DocumentViewer.PageDisplayMode newValue)
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                Action<PageControl> <>9__0;
                PageControl input = (PageControl) base.ItemContainerGenerator.ContainerFromIndex(i);
                Action<PageControl> action = <>9__0;
                if (<>9__0 == null)
                {
                    Action<PageControl> local1 = <>9__0;
                    action = <>9__0 = x => x.PageDisplayMode = newValue;
                }
                input.Do<PageControl>(action);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            ((PageControl) element).PageDisplayMode = this.PageDisplayMode;
        }

        protected internal System.Windows.Controls.ItemsPresenter ItemsPresenter { get; private set; }

        protected internal System.Windows.Controls.ScrollViewer ScrollViewer { get; private set; }

        protected internal DocumentViewerPanel Panel { get; private set; }

        protected internal DocumentPresenterDecorator PresenterDecorator { get; private set; }

        private IDocumentViewerControl DocumentViewer =>
            DocumentViewerControl.GetActualViewer(this);

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PageDisplayMode) base.GetValue(PageDisplayModeProperty);
            set => 
                base.SetValue(PageDisplayModeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewerItemsControl.<>c <>9 = new DocumentViewerItemsControl.<>c();

            internal void <.cctor>b__29_0(DocumentViewerItemsControl owner, PageDisplayMode oldValue, PageDisplayMode newValue)
            {
                owner.OnPageDisplayModeChanged(newValue);
            }
        }
    }
}

