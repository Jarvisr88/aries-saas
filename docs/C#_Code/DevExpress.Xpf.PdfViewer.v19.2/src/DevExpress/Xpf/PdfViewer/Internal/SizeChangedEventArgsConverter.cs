namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Windows;

    public class SizeChangedEventArgsConverter : EventArgsConverterBase<SizeChangedEventArgs>
    {
        protected override object Convert(object sender, SizeChangedEventArgs args) => 
            args.NewSize;
    }
}

