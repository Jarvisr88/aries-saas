namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    internal class RenderContentPresenterElement : ContentPresenter, IVisualTransformOwner
    {
        private static readonly Action<ContentPresenter, DataTemplate> set_Template;

        static RenderContentPresenterElement();
        protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate);
        protected override void OnContentTemplateSelectorChanged(DataTemplateSelector oldContentTemplateSelector, DataTemplateSelector newContentTemplateSelector);

        Transform IVisualTransformOwner.VisualTransform { get; set; }
    }
}

