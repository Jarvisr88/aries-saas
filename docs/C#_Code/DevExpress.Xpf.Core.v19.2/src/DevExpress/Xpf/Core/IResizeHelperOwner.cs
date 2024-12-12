namespace DevExpress.Xpf.Core
{
    using System;

    public interface IResizeHelperOwner
    {
        void ChangeSize(double delta);
        void OnDoubleClick();
        void SetIsResizing(bool isResizing);

        SizeHelperBase SizeHelper { get; }

        double ActualSize { get; set; }
    }
}

