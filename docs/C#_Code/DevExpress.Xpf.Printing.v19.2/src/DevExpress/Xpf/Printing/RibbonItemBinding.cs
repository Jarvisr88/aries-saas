namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public class RibbonItemBinding : FindAncestorBindingExtension
    {
        protected override Type AncestorType =>
            typeof(RibbonDocumentPreview);

        protected override PropertyPath BindingPath =>
            new PropertyPath(base.Path, new object[0]);
    }
}

