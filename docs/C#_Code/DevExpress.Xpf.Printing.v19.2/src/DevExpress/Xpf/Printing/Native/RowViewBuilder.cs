namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class RowViewBuilder
    {
        private readonly bool rightToLeftLayout;
        private readonly IVisualTreeRoot visualTreeRoot;
        internal readonly StackPanel panel;
        internal readonly Dictionary<DataTemplate, ContentPresenter> presenterDictionary;

        public RowViewBuilder(bool rightToLeftLayout) : this(rightToLeftLayout, CreateVisualTreeRoot())
        {
        }

        internal RowViewBuilder(bool rightToLeftLayout, IVisualTreeRoot visualTreeRoot)
        {
            this.presenterDictionary = new Dictionary<DataTemplate, ContentPresenter>();
            Guard.ArgumentNotNull(visualTreeRoot, "visualTreeRoot");
            this.rightToLeftLayout = rightToLeftLayout;
            this.visualTreeRoot = visualTreeRoot;
            this.panel = new StackPanel();
            visualTreeRoot.Content = this.panel;
        }

        public void Clear()
        {
            this.visualTreeRoot.Active = false;
            this.panel.Children.Clear();
            this.presenterDictionary.Clear();
        }

        public FrameworkElement Create(DataTemplate rowTemplate, RowContent rowContent)
        {
            ContentPresenter presenter;
            Guard.ArgumentNotNull(rowTemplate, "rowTemplate");
            Guard.ArgumentNotNull(rowContent, "rowContent");
            this.visualTreeRoot.Active = true;
            if (!this.presenterDictionary.TryGetValue(rowTemplate, out presenter))
            {
                presenter = new ContentPresenter {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    ContentTemplate = rowTemplate,
                    Content = new RowContent(),
                    FlowDirection = this.rightToLeftLayout ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
                };
                Viewbox viewbox1 = new Viewbox();
                viewbox1.Child = presenter;
                viewbox1.Width = 0.0;
                viewbox1.Height = 0.0;
                Viewbox element = viewbox1;
                this.presenterDictionary.Add(rowTemplate, presenter);
                this.panel.Children.Add(element);
            }
            RowContent content = (RowContent) presenter.Content;
            content.Content = rowContent.Content;
            content.UsablePageWidth = rowContent.UsablePageWidth;
            content.UsablePageHeight = rowContent.UsablePageHeight;
            content.IsEven = rowContent.IsEven;
            LayoutUpdatedHelper.GlobalLocker.DoLockedAction(new Action(presenter.UpdateLayout));
            return presenter;
        }

        private static IVisualTreeRoot CreateVisualTreeRoot() => 
            new PopupVisualTreeRoot();
    }
}

