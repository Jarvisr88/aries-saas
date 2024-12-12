namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class DocumentSelectorContainer : psvControl
    {
        static DocumentSelectorContainer()
        {
            new DependencyPropertyRegistrator<DocumentSelectorContainer>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }
    }
}

