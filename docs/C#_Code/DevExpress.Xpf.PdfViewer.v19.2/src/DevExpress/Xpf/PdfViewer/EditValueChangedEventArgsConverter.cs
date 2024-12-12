namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Editors;
    using System;

    public class EditValueChangedEventArgsConverter : EventArgsConverterBase<EditValueChangedEventArgs>
    {
        protected override object Convert(object sender, EditValueChangedEventArgs args) => 
            args.NewValue;
    }
}

