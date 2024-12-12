namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;

    public interface IDragElementController
    {
        void Hide();
        void Show(object[] source);
        void UpdatePosition();

        DragDropHintData Data { get; }
    }
}

