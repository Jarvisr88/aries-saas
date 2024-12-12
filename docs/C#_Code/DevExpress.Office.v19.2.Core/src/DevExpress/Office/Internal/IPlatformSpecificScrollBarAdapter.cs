namespace DevExpress.Office.Internal
{
    using System;
    using System.Windows.Forms;

    public interface IPlatformSpecificScrollBarAdapter
    {
        void ApplyValuesToScrollBarCore(ScrollBarAdapter adapter);
        ScrollEventArgs CreateLastScrollEventArgs(ScrollBarAdapter adapter);
        int GetPageDownRawScrollBarValue(ScrollBarAdapter adapter);
        int GetPageUpRawScrollBarValue(ScrollBarAdapter adapter);
        int GetRawScrollBarValue(ScrollBarAdapter adapter);
        void OnScroll(ScrollBarAdapter adapter, object sender, ScrollEventArgs e);
        bool SetRawScrollBarValue(ScrollBarAdapter adapter, int value);
    }
}

