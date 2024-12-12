namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDocument
    {
        void Close(bool force = true);
        void Hide();
        void Show();

        object Id { get; set; }

        object Content { get; }

        object Title { get; set; }

        bool DestroyOnClose { get; set; }
    }
}

