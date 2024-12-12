namespace DevExpress.Data.Utils
{
    using System;

    public interface IToolItem : IDisposable
    {
        void Close();
        void Hide();
        void InitTool();
        void ShowActivate();
        void ShowNoActivate();
        void UpdateView();

        Guid Kind { get; }
    }
}

