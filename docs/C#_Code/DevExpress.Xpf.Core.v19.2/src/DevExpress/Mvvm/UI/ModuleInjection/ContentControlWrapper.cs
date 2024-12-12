namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ContentControlWrapper : IContentPresenterWrapper<ContentControl>, ITargetWrapper<ContentControl>
    {
        public ContentControl Target { get; set; }

        public object Content
        {
            get => 
                this.Target.Content;
            set => 
                this.Target.Content = value;
        }

        public DataTemplate ContentTemplate
        {
            get => 
                this.Target.ContentTemplate;
            set => 
                this.Target.ContentTemplate = value;
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                this.Target.ContentTemplateSelector;
            set => 
                this.Target.ContentTemplateSelector = value;
        }
    }
}

