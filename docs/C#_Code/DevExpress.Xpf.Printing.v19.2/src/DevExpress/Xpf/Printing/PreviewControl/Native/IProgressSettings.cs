namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.ComponentModel;

    public interface IProgressSettings : INotifyPropertyChanged
    {
        bool InProgress { get; }

        DevExpress.Xpf.Printing.PreviewControl.Native.ProgressType ProgressType { get; }

        int ProgressPosition { get; }
    }
}

