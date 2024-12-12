namespace DevExpress.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public interface IScrollBarAdapter : IBatchUpdateable, IBatchUpdateHandler
    {
        event ScrollEventHandler Scroll;

        void Activate();
        void ApplyValuesToScrollBar();
        ScrollEventArgs CreateEmulatedScrollEventArgs(ScrollEventType eventType);
        void Deactivate();
        void EnsureSynchronized();
        int GetPageDownRawScrollBarValue();
        int GetPageUpRawScrollBarValue();
        int GetRawScrollBarValue();
        void RefreshValuesFromScrollBar();
        bool SetRawScrollBarValue(int value);
        bool SynchronizeScrollBarAvoidJump();

        long Minimum { get; set; }

        long Maximum { get; set; }

        long Value { get; set; }

        long LargeChange { get; set; }

        bool Enabled { get; set; }
    }
}

