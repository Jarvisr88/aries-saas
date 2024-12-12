namespace DevExpress.Xpf.Editors
{
    using System.Windows;
    using System.Windows.Controls;

    public interface IPopupSource : IEditorSource
    {
        DataTemplate ContentTemplate { get; }

        DataTemplateSelector ContentTemplateSelector { get; }
    }
}

