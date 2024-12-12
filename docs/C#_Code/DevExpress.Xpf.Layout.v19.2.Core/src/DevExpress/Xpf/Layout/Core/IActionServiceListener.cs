namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IActionServiceListener : IUIServiceListener
    {
        void OnCollapse();
        void OnExpand();
        void OnHide(bool immediately);
        void OnHideSelection();
        void OnShowSelection();
    }
}

