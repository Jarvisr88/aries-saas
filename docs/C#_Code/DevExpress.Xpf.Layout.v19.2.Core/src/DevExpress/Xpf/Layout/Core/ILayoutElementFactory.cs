namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ILayoutElementFactory
    {
        ILayoutElement CreateLayoutHierarchy(object rootKey);
        ILayoutElement GetElement(object key);
    }
}

