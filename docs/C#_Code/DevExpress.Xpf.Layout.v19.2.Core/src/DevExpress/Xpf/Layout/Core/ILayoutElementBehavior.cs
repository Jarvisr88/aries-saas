namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ILayoutElementBehavior
    {
        bool CanDrag(OperationType operation);
        bool CanSelect();

        bool AllowDragging { get; }
    }
}

