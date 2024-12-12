namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Editors;
    using System;

    public class TextEditEditValueChangedConverter : EventArgsConverterBase<EditValueChangedEventArgs>
    {
        protected override object Convert(object sender, EditValueChangedEventArgs args) => 
            args.NewValue;
    }
}

