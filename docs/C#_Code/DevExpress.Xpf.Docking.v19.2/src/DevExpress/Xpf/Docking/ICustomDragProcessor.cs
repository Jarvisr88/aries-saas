namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface ICustomDragProcessor
    {
        void CancelDragging();
        void StartDragging();

        bool IsInEvent { get; }
    }
}

