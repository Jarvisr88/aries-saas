namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IContextActionServiceListener : IUIServiceListener
    {
        bool OnCanExecute(ILayoutElement element, ContextAction action);
        bool OnExecute(ILayoutElement element, ContextAction action);
    }
}

