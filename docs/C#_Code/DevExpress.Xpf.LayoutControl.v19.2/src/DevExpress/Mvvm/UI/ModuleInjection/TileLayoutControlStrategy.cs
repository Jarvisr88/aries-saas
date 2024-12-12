namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TileLayoutControlStrategy : ItemsControlStrategy<TileLayoutControl, TileLayoutControlWrapper>
    {
        private DataTemplate tileItemTemplate;
        private DataTemplate tileContentTemplate;

        private DataTemplate GetTileItemTemplate()
        {
            if (this.tileContentTemplate == null)
            {
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(ContentPresenter));
                factory.SetBinding(ContentPresenter.ContentProperty, new Binding());
                factory.SetValue(ContentPresenter.ContentTemplateSelectorProperty, base.ViewSelector);
                DataTemplate template1 = new DataTemplate();
                template1.VisualTree = factory;
                this.tileContentTemplate = template1;
                this.tileContentTemplate.Seal();
            }
            if (this.tileItemTemplate == null)
            {
                FrameworkElementFactory factory2 = new FrameworkElementFactory(typeof(Tile));
                factory2.SetBinding(ContentControlBase.ContentProperty, new Binding());
                factory2.SetValue(ContentControlBase.ContentTemplateProperty, this.tileContentTemplate);
                DataTemplate template2 = new DataTemplate();
                template2.VisualTree = factory2;
                this.tileItemTemplate = template2;
                this.tileItemTemplate.Seal();
            }
            return this.tileItemTemplate;
        }

        protected override void InitItemTemplate()
        {
            if ((base.Wrapper.ItemTemplate == null) && (base.Wrapper.ItemTemplateSelector == null))
            {
                base.Wrapper.ItemTemplate = this.GetTileItemTemplate();
            }
        }
    }
}

